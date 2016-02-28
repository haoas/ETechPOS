using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using System.Security.Cryptography;
using ETech.fnc;

namespace ETech
{
    public partial class frmLogIn : Form
    {
        public cls_user cashier;
        public cls_user checker;
        public cls_user salesman;
        public string user = "checker";

        public frmLogIn()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            login();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void login()
        {
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
                            username = @username AND password = @password";
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
                return;
            }

            string code = dt.Rows[0]["usercode"].ToString();
            string fullname = dt.Rows[0]["fullname"].ToString();
            string wid = dt.Rows[0]["wid"].ToString();

            //if (Convert.ToInt32(wid) == this.cashier.getwid())
            //{
            //    fncFilter.alert(cls_globalvariables.warning_samecashierchecker);
            //    txtUsername.Focus();
            //    txtUsername.SelectAll();
            //    return;
            //}


            string SQLpermissions = @"SELECT * FROM `userpermission` WHERE `userid` = @userwid";
            parameters = new List<string>();
            values = new List<string>();
            parameters.Add("@userwid");
            values.Add(wid);
            DataTable dtpermission = mySQLFunc.getdb(SQLpermissions, parameters, values);
            List<int> permissions = new List<int>();
            foreach (DataRow dr in dtpermission.Rows)
            {
                permissions.Add(Convert.ToInt32(dr["code"]));
            }

            if (!fncFilter.check_permission_login(permissions))
            {
                fncFilter.alert(cls_globalvariables.warning_userpermission_denied);
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            if (user == "checker")
                this.checker.setcls_user(code, fullname, permissions, Convert.ToInt32(wid));
            if (user == "salesman")
                this.salesman.setcls_user(code, fullname, permissions, Convert.ToInt32(wid));

            this.Close();
        }

        private void frmLogIn_KeyDown(object sender, KeyEventArgs e)
        {
            Control nextControl;
            if (e.KeyCode == Keys.Enter)
            {
                nextControl = GetNextControl(ActiveControl, !e.Shift);
                if (nextControl == null)
                {
                    nextControl = GetNextControl(null, true);
                }
                nextControl.Focus();
            }
            else if (e.KeyCode == Keys.F1)
            {
                login();
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else
                return;
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
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

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            txtUsername.SelectAll();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
