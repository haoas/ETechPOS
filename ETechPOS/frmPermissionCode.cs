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
    public partial class frmPermissionCode : Form
    {
        public int permission_needed;
        public bool permcode;
        public string permissionwid = "0";

        public frmPermissionCode()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            permission_needed = 100;
        }

        public void done_process()
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(this.txtPermissionCode.Text);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            string pass = BitConverter.ToString(encodedBytes).Replace("-", "");

            string SQL = @"SELECT `SyncId` FROM `user` WHERE password = @password AND `status`= 1";
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            parameters.Add("@password");
            values.Add(pass);

            DataTable dt = mySQLFunc.getdb(SQL, parameters, values);
            if (dt.Rows.Count <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_permissioncode_invalid);
                txtPermissionCode.Focus();
                txtPermissionCode.SelectAll();
                return;
            }

            string SyncId = dt.Rows[0]["SyncId"].ToString();
            string SQLpermissions = @"SELECT * FROM `userpermission` WHERE `userid` = @userwid";
            parameters = new List<string>();
            values = new List<string>();
            parameters.Add("@userwid");
            values.Add(SyncId);
            DataTable dtpermission = mySQLFunc.getdb(SQLpermissions, parameters, values);
            List<int> permissions = new List<int>();
            foreach (DataRow dr in dtpermission.Rows)
            {
                permissions.Add(Convert.ToInt32(dr["code"]));
            }

            if (permissions.Contains(100) || permissions.Contains(this.permission_needed))
            {
                permcode = true;
                this.permissionwid = SyncId;
                this.Close();
                return;
            }
            else
            {
                fncFilter.alert(cls_globalvariables.warning_permissioncode_invalid);
                return;
            }
        }

        private void frmPermissionCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                permcode = false;
                this.Close();
                return;
            }
            else
                return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            permcode = false;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmPermissionCode_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
    }
}
