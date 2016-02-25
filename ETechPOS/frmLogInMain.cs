using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using ETech.cls;
using MySql.Data.MySqlClient;
using str_encode_decode;
using System.Threading;
using System.IO;
using ETech.fnc;

namespace ETech
{
    public partial class frmLogInMain : Form
    {
        private string serverDateTime;
        private string branchNameTerminalNumber;
        private bool isconnected;
        public cls_user cashier;

        public frmLogInMain()
        {
            InitializeComponent();

            serverDateTime = "";
            branchNameTerminalNumber = "";
            isconnected = false;
            cashier = null;

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
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
            else if (e.Alt && e.KeyCode == Keys.F8)
                lblApplicationVersion.Visible = lblApplicationVersion.Visible ? false : true;
        }
        private void frmLogInMain_Load(object sender, EventArgs e)
        {
            try_connection();
            updateConnectionControls();

            txtUsername.Focus();
            txtUsername.SelectAll();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            lblApplicationVersion.Text = "v" + Application.ProductVersion;
            string logInImagePath = Application.StartupPath + "/resources/images/Log In Image.jpg";
            if (File.Exists(logInImagePath))
            {
                Bitmap bitmap = new Bitmap(logInImagePath);
                pbLogo.Image = bitmap;
            }
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
            updateConnectionControls();
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

            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(txtPassword.Text);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            string pass = BitConverter.ToString(encodedBytes).Replace("-", "");

            string SQL = @"SELECT * FROM `user` WHERE 
                            `show`=1 AND username = @username AND password = @password";
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
            string wid = dt.Rows[0]["wid"].ToString();
            string SQLpermissions = @"SELECT * FROM `userpermission` WHERE `userid` = @userwid";
            parameters = new List<string>();
            values = new List<string>();
            parameters.Add("@userwid");
            values.Add(wid);
            DataTable dtpermission = mySQLFunc.getdb(SQLpermissions, parameters, values);
            List<int> permissions = new List<int>();
            foreach (DataRow dr in dtpermission.Rows)
                permissions.Add(Convert.ToInt32(dr["code"]));

            if (!fncFilter.check_permission_login(permissions))
            {
                fncFilter.alert(cls_globalvariables.warning_userpermission_denied);
                txtUsername.Focus();
                txtUsername.SelectAll();
                btnLogIn.Enabled = true;
                return;
            }

            this.cashier.setcls_user(code, fullname, permissions, Convert.ToInt32(wid));

            //Does not continue if Mac address is incorrect
            if (!cls_globalfunc.CheckMacAddress() && cashier.getwid() != 0 && cashier.getwid() != 1)
            {
                fncFilter.alert("INCORRECT MAC Address SETUP!");
                cashier = new cls_user();
                txtUsername.Focus();
                txtUsername.SelectAll();
                btnLogIn.Enabled = true;
                return;
            }

            this.Close();
        }
        private void try_connection()
        {
            try
            {
                string server = cls_globalvariables.server_v;
                string userid = cls_globalvariables.userid_v;
                string password = cls_globalvariables.password_v;
                string database = cls_globalvariables.database_v;
                string cs = @"server=" + server
                            + ";userid=" + userid
                            + ";password=" + password
                            + ";database=" + database
                            + ";Connect Timeout=3000";

                MySqlConnection conn = new MySqlConnection(cs);
                conn.Open();
                conn.Close();

                string branchid = cls_globalvariables.branchid_v;
                string sql = "Select Now() AS `now`, `name` as 'branchname' FROM branch WHERE `wid`=" + branchid;
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
                branchNameTerminalNumber = branchName + @" POS: " + terminalNumber;
                isconnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
                isconnected = false;
            }
        }
        private void updateConnectionControls()
        {
            if (isconnected)
            {
                lblStatus_d.Text = "Online";
                lblStatus_d.ForeColor = Color.Green;
                lblServerDateTime.Text = serverDateTime;
                lblBranchNameTerminalNumber.Text = branchNameTerminalNumber;
            }
            else
            {
                lblStatus_d.Text = "Offline";
                lblStatus_d.ForeColor = Color.Red;
                lblServerDateTime.Text = "MMM dd, yyyy hh:mm tt, wwwww";
                lblBranchNameTerminalNumber.Text = "BRANCH / POS";
            }
        }
    }
}
