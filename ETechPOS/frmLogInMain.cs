using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;
using ETech.cls;
using ETech.fnc;
using ETech;
using ETECHPOS.Helpers;
using ETECHPOS.fnc;

namespace ETECHPOS
{
    public partial class frmLogInMain : Form
    {
        private string serverDateTime;
        private bool isconnected;
        public cls_user cashier;

        public frmLogInMain()
        {
            InitializeComponent();

            serverDateTime = "";
            isconnected = false;
            cashier = null;

            //fncFilter.set_theme_color(this);
            //cls_globalfunc.formaddkbkpevent(this);
        }

        private void frmLogInMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                login();
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
        }
        private void frmLogInMain_Load(object sender, EventArgs e)
        {
            try_connection();

            RefreshServerDateTime();

            txtUsername.Focus();
            txtUsername.SelectAll();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            string logInImagePath = Application.StartupPath + "/resources/images/Log In Image.jpg";
            if (File.Exists(logInImagePath))
            {
                Bitmap bitmap = new Bitmap(logInImagePath);
            }
            fncFilter.set_theme_color(this);
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            login();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPassword.Focus();
                txtPassword.SelectAll();
                e.Handled = true;
            }
        }
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                login();
                e.Handled = true;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.SelectAll();
        }

        private void tmrConnecting_Tick(object sender, EventArgs e)
        {
            if (!bgwConnecting.IsBusy)
                bgwConnecting.RunWorkerAsync();
        }

        private void bgwConnecting_DoWork(object sender, DoWorkEventArgs e)
        {
            try_connection();
        }
        private void bgwConnecting_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshServerDateTime();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void login()
        {
            try_connection();
            txtUsername.Text = txtUsername.Text.Trim();

            if (!isconnected)
            {
                fncFilter.alert("This device is not connected to the server.");
                this.Close();
                return;
            }

            if (btnLogIn.Enabled == false)
                return;

            btnLogIn.Enabled = false;

            string pass = MD5EncryptionFunction.Encrypt(txtPassword.Text);

            string SQL = @"SELECT * FROM `user` WHERE `status`=1 AND username = @username AND password = @password";
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            parameters.Add("@username");
            parameters.Add("@password");
            values.Add(txtUsername.Text);
            values.Add(pass);

            DataTable dt = mySQLFunc.getdb(SQL, parameters, values);
            if (dt.Rows.Count <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_userpass_invalid);
                txtUsername.Focus();
                txtUsername.SelectAll();
                btnLogIn.Enabled = true;
                return;
            }

            string code = dt.Rows[0]["usercode"].ToString();
            string fullname = dt.Rows[0]["fullname"].ToString();
            string SyncId = dt.Rows[0]["SyncId"].ToString();
            string SQLauth = @"SELECT * FROM `userauth` WHERE `userid` = @userwid";
            parameters = new List<string>();
            values = new List<string>();
            parameters.Add("@userwid");
            values.Add(SyncId);
            DataTable dtauth = mySQLFunc.getdb(SQLauth, parameters, values);
            List<string> Authorizations = new List<string>();
            foreach (DataRow dr in dtauth.Rows)
                Authorizations.Add(dr["authorization"].ToString());

            this.cashier.setcls_user(code, fullname, Authorizations, Convert.ToInt32(SyncId));

            //Does not continue if Mac address is incorrect
            //if (!cls_globalfunc.CheckMacAddress() && cashier.getwid() != 0 && cashier.getwid() != 1)
            //{
            //    fncFilter.alert("INCORRECT MAC Address SETUP!");
            //    cashier = new cls_user();
            //    txtUsername.Focus();
            //    txtUsername.SelectAll();
            //    btnLogIn.Enabled = true;
            //    return;
            //}

            this.Close();
        }
        private void try_connection()
        {
            try
            {
                string server = cls_globalvariables.ConnectionSettings.Server;
                string userid = cls_globalvariables.ConnectionSettings.UserId;
                string password = cls_globalvariables.ConnectionSettings.Password;
                string database = cls_globalvariables.ConnectionSettings.Database;
                string cs = @"server=" + server
                            + ";userid=" + userid
                            + ";password=" + password
                            + ";database=" + database
                            + ";Connect Timeout=3000";

                MySqlConnection conn = new MySqlConnection(cs);
                conn.Open();
                conn.Close();

                string branchid = cls_globalvariables.BranchCode;
                string sql = "Select Now() AS `now`, `name` as 'branchname' FROM branch WHERE `Id`=" + branchid;
                DataTable dt = mySQLFunc.getdb(sql);
                if (dt != null && dt.Rows.Count <= 0)
                {
                    isconnected = false;
                    return;
                }

                DateTime dateTime = Convert.ToDateTime(dt.Rows[0]["now"]);
                string branchName = dt.Rows[0]["branchname"].ToString();
                string terminalNumber = cls_globalvariables.terminalno_v;
                serverDateTime = dateTime.ToString("MMM dd, yyyy hh:mm tt, ") + dateTime.DayOfWeek.ToString();

                lbl_BranchCode.Text = "Branch: " + cls_globalvariables.BranchCode + @"-" + branchName + @"";
                lbl_Terminalno.Text = "Terminal#: " + cls_globalvariables.terminalno_v + @"";

                isconnected = true;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialog(ex.ToString());
                isconnected = false;
            }
        }

        private void RefreshServerDateTime()
        {
            lblServerDateTime.Text = DateTime.Now.ToString("MMM dd, yyyy hh:mm tt, ") + DateTime.Now.DayOfWeek.ToString();
        }
    }
}
