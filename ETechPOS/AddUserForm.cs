using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.FormatDesigner;
using ETech.fnc;
using ETech.cls;

namespace ETech
{
    public partial class AddUserForm : Form
    {
        private cls_user user = null;
        private DataTable dt_temp = new DataTable();

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            DGVUsers.Standardize();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(DGVUsers);
        }

        private void RefrshDGVUsers()
        {
            string statuscondition = (cbxActive.Checked) ? "1" : "0";

            if (this.txtUser.Text.Trim().Length == 0)
            {
                dt_temp.Clear();
                DGVUsers.DataSource = dt_temp;
                fncFilter.set_dgv_display(DGVUsers);
                return;
            }
            string str_input = "%" + this.txtUser.Text.Trim() + "%";

            string SQL = @"SELECT `SyncId`, `usercode` AS 'code', `fullname`, `username`
                            FROM `user`
                            WHERE SyncId > 1000000 AND `status` = " + statuscondition + @" AND 
	                            CONCAT(`fullname`, `username`) LIKE @str_param";

            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            parameters.Add("@str_param");
            values.Add(str_input);

            dt_temp = mySQLFunc.getdb(SQL, parameters, values);

            this.DGVUsers.DataSource = dt_temp;

            if (DGVUsers.RowCount > 0)
                this.DGVUsers.Rows[0].Selected = true;
        }

        private void AddUserForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                DGVUsers.SelectNextRow();
            }
            else if (e.KeyCode == Keys.Up)
            {
                DGVUsers.SelectPreviousRow();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                EditUser();
            }
            else if (e.KeyCode == Keys.Escape)
                this.Close();

            e.Handled = true;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            RefrshDGVUsers();
        }

        private void cbxActive_CheckedChanged_1(object sender, EventArgs e)
        {
            RefrshDGVUsers();
        }

        private void EditUser()
        {
            ViewUsersPanel.Enabled = false;

            if (DGVUsers.SelectedRows.Count <= 0)
                return;

            long SyncId = Convert.ToInt64(DGVUsers.SelectedRows[0].Cells["id"].Value);
            user = new cls_user(SyncId);

            tbx_Usercode.Text = user.getusercode().Trim();
            tbx_Fullname.Text = user.getfullname().Trim();
            tbx_Username.Text = user.username.Trim();
            tbx_Password.Text = user.password.Trim();
            cbx_All.Checked = user.authorization.Contains("ALL");
            cbx_ChangeQuantity.Checked= user.authorization.Contains("CHANGEQTY");
            cbx_discount.Checked= user.authorization.Contains("DISCOUNT");
            cbx_membertrans.Checked= user.authorization.Contains("MEMBERTRANS");
            cbx_nonvattrans.Checked= user.authorization.Contains("NONVATTRANS");
            cbx_opendrawer.Checked= user.authorization.Contains("OPENDRAWER");
            cbx_Openitem.Checked= user.authorization.Contains("OPENITEM");
            cbx_pickupcash.Checked= user.authorization.Contains("PICKUPCASH");
            cbx_Refunditem.Checked= user.authorization.Contains("REFUNDITEM");
            cbx_Removeitem.Checked= user.authorization.Contains("REMOVEITEM");
            cbx_reprintor.Checked= user.authorization.Contains("REPRINTOR");
            cbx_seniortrans.Checked= user.authorization.Contains("SENIORTRANS");
            cbx_voidtrans.Checked= user.authorization.Contains("VOIDTRANS");
            cbx_wholesale.Checked= user.authorization.Contains("WSALETRANS");
            cbx_xread.Checked= user.authorization.Contains("XREAD");
            cbx_zread.Checked= user.authorization.Contains("ZREAD");


            AddOrEditUsersPanel.Enabled = true;
            tbx_Usercode.Focus();
        }

        private void btn_UpdateUser_Click(object sender, EventArgs e)
        {
            string usercode = tbx_Usercode.Text;
            string fullname = tbx_Fullname.Text;
            string username = tbx_Username.Text;
            string password = tbx_Password.Text;

            if (usercode.Length == 0 && usercode.Length == 0 && usercode.Length == 0 && usercode.Length == 0)
            {
                MessageBox.Show("Fields cannot be empty");
            }

            string SQL =
            @"UPDATE `user` SET `usercode`= '" + usercode + @"', `fullname`='" + fullname + @"', 
                     `username`='" + username + @"', `password`='" + password + @"' 
                WHERE `Syncid`=" + user.getsyncid() + @" LIMIT 1";
            mySQLFunc.setdb(SQL);

            List<string> AuthorizationCodes = new List<string>();
            if (cbx_All.Checked) AuthorizationCodes.Add("ALL");
            if (cbx_ChangeQuantity.Checked) AuthorizationCodes.Add("CHANGEQTY");
            if (cbx_discount.Checked) AuthorizationCodes.Add("DISCOUNT");
            if (cbx_membertrans.Checked) AuthorizationCodes.Add("MEMBERTRANS");
            if (cbx_nonvattrans.Checked) AuthorizationCodes.Add("NONVATTRANS");
            if (cbx_opendrawer.Checked) AuthorizationCodes.Add("OPENDRAWER");
            if (cbx_Openitem.Checked) AuthorizationCodes.Add("OPENITEM");
            if (cbx_pickupcash.Checked) AuthorizationCodes.Add("PICKUPCASH");
            if (cbx_Refunditem.Checked) AuthorizationCodes.Add("REFUNDITEM");
            if (cbx_Removeitem.Checked) AuthorizationCodes.Add("REMOVEITEM");
            if (cbx_reprintor.Checked) AuthorizationCodes.Add("REPRINTOR");
            if (cbx_seniortrans.Checked) AuthorizationCodes.Add("SENIORTRANS");
            if (cbx_voidtrans.Checked) AuthorizationCodes.Add("VOIDTRANS");
            if (cbx_wholesale.Checked) AuthorizationCodes.Add("WSALETRANS");
            if (cbx_xread.Checked) AuthorizationCodes.Add("XREAD");
            if (cbx_zread.Checked) AuthorizationCodes.Add("ZREAD");
            AuthorizationCodes = AuthorizationCodes.Distinct().ToList();

            SQL = @"DELETE FROM `userauth` WHERE `userid`=" + user.getsyncid();
            mySQLFunc.setdb(SQL);
            foreach (string Auth in AuthorizationCodes)
            {
                SQL = @"INSERT INTO `userauth` (`userid`,`authorization`) VALUES ( " + user.getsyncid() + @", '" + Auth + @"' )";
                mySQLFunc.setdb(SQL);
            }

            ClearAddOrEditUsersPanel();
            AddOrEditUsersPanel.Enabled = false;
            ViewUsersPanel.Enabled = true;
            txtUser.Focus();
            RefrshDGVUsers();
        }

        private void ClearAddOrEditUsersPanel()
        {
            tbx_Usercode.Text = string.Empty;
            tbx_Fullname.Text = string.Empty;
            tbx_Username.Text = string.Empty;
            tbx_Password.Text = string.Empty;

            foreach (Control ctr in GB_Authorization.Controls)
            {
                if (ctr is CheckBox)
                    ((CheckBox)ctr).Checked = false;
            }
        }
    }
}
