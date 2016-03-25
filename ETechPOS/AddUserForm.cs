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
using ETECHPOS.Helpers;

namespace ETech
{
    public partial class AddUserForm : Form
    {
        private cls_user user = null;
        private cls_user Cashier = null;
        private DataTable dt_temp = new DataTable();
        private Color enabledcolor = Color.LightPink;
        private Dictionary<string, CheckBox> AuthDictionary = null;

        public AddUserForm(cls_user cashier_d)
        {
            InitializeComponent();
            Cashier = cashier_d;
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            DGVUsers.Standardize();
            RefrshDGVUsers();
            ClearAddOrEditUsersPanel();

            AuthDictionary = new Dictionary<string, CheckBox>()
            {
                { "ALL", cbx_All },
                { "CHANGEQTY", cbx_ChangeQuantity },
                { "DISCOUNT", cbx_discount },
                { "MEMBERTRANS", cbx_membertrans },
                { "NONVATTRANS", cbx_nonvattrans },
                { "OPENDRAWER", cbx_opendrawer },
                { "OPENITEM", cbx_Openitem },
                { "PICKUPCASH", cbx_pickupcash },
                { "REFUNDITEM", cbx_Refunditem },
                { "REMOVEITEM", cbx_Removeitem },
                { "REPRINTOR", cbx_reprintor },
                { "SENIORTRANS", cbx_seniortrans },
                { "VOIDTRANS", cbx_voidtrans },
                { "WSALETRANS", cbx_wholesale },
                { "XREAD", cbx_xread },
                { "ZREAD", cbx_zread },
                { "MODIFYUSER", cbx_modifyuser }
            };

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(DGVUsers);
        }

        private void RefrshDGVUsers()
        {
            string statuscondition = (cbxActive.Checked) ? "1" : "0";

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

        private void cbxActive_CheckedChanged(object sender, EventArgs e)
        {
            RefrshDGVUsers();
        }

        private void EditUser()
        {
            if (DGVUsers.SelectedRows.Count <= 0)
                return;

            long SyncId = Convert.ToInt64(DGVUsers.SelectedRows[0].Cells["id"].Value);
            user = new cls_user();
            user.setcls_user_by_wid(SyncId, true);

            EnableUserModifierPanel();

            tbx_Usercode.Text = user.getusercode().Trim();
            tbx_Fullname.Text = user.getfullname().Trim();
            tbx_Username.Text = user.username.Trim();
            tbx_Password.Text = user.password.Trim();

            foreach (KeyValuePair<string, CheckBox> dicentry in AuthDictionary)
            {
                dicentry.Value.Checked = (user.AuthorizationList.Contains(dicentry.Key)) ? true : false;
            }
        }

        private void btn_UpdateUser_Click(object sender, EventArgs e)
        {
            string usercode = tbx_Usercode.Text;
            string fullname = tbx_Fullname.Text;
            string username = tbx_Username.Text;
            string password = tbx_Password.Text;

            if (usercode.Length == 0 && usercode.Length == 0 && usercode.Length == 0 && usercode.Length == 0)
            {
                DialogHelper.ShowDialog("Fields cannot be empty");
                return;
            }

            string SQL =
            @"UPDATE `user` SET `usercode`= '" + usercode + @"', `fullname`='" + fullname + @"', 
                     `username`='" + username + @"', `password`='" + password + @"', lastmodifieddate=NOW(), `lastmodifiedby` = " + user.syncid + @"
                WHERE `Syncid`=" + user.getsyncid() + @" LIMIT 1";
            mySQLFunc.setdb(SQL);
            //lastmodifiedby
            //make sure no duplicate fields

            List<string> AuthorizationCodes = new List<string>();
            foreach (KeyValuePair<string, CheckBox> dicentry in AuthDictionary)
            {
                if (dicentry.Value.Checked)
                    AuthorizationCodes.Add(dicentry.Key);
            }

            SQL = @"DELETE FROM `userauth` WHERE `userid`=" + user.getsyncid();
            mySQLFunc.setdb(SQL);
            foreach (string Auth in AuthorizationCodes)
            {
                SQL = @"INSERT INTO `userauth` (`userid`,`authorization`) VALUES ( " + user.getsyncid() + @", '" + Auth + @"' )";
                mySQLFunc.setdb(SQL);
            }
            EnableViewUsersPanel();
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
                {
                    ((CheckBox)ctr).Checked = false;
                    ((CheckBox)ctr).BackColor = enabledcolor;
                    ((CheckBox)ctr).Visible = false;
                }
            }
            btn_AddUser.Visible = false;
            btn_UpdateUser.Visible = false;
            btnDeactivate.Visible = false;
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            string SQL = @"UPDATE `user` SET `status`=" + ((user.status == 0) ? "1" : "0") +
                         @"`lastmodifiedby` = " + user.syncid + @"  WHERE `SyncId`=" + user.getsyncid();
            //lastmodifiedby
            //lastdeactivatedby
            //make sure no duplicate fields
            mySQLFunc.setdb(SQL);

            EnableViewUsersPanel();
            fncFilter.alert("user " + user.getfullname() + ((user.status == 0) ? " Reactivated" : " Deactivated"));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            user = new cls_user();
            EnableUserModifierPanel();
        }

        private void EnableViewUsersPanel()
        {
            ViewUsersPanel.Enabled = true;
            AddOrEditUsersPanel.Enabled = false;
            ClearAddOrEditUsersPanel();
            txtUser.Focus();
            RefrshDGVUsers();
        }

        private void EnableUserModifierPanel()
        {
            AddOrEditUsersPanel.Enabled = true;
            ViewUsersPanel.Enabled = false;
            ClearAddOrEditUsersPanel();
            btnCancel.Enabled = true;

            if (user.getsyncid() == 0) //Add User
            {
                btn_AddUser.Visible = true;
            }
            else
            {
                btnDeactivate.Visible = true;
                if (user.status == 0) //Activating Deactivated User
                {
                    btnDeactivate.Text = "Activate";
                    btn_UpdateUser.Visible = false;
                }
                else //Deactivating Activated User
                {
                    btnDeactivate.Text = "Deactivate";
                    btn_UpdateUser.Visible = true;
                }
            }
            tbx_Usercode.Focus();

            foreach (KeyValuePair<string, CheckBox> dicentry in AuthDictionary)
            {
                if (Cashier.AuthorizationList.Contains("ALL") ||
                    Cashier.AuthorizationList.Contains(dicentry.Key))
                {
                    dicentry.Value.BackColor = enabledcolor;
                    dicentry.Value.Visible = true;
                }
            }
        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            string usercode = tbx_Usercode.Text;
            string fullname = tbx_Fullname.Text;
            string username = tbx_Username.Text;
            string password = tbx_Password.Text;

            if (usercode.Length == 0 && usercode.Length == 0 && usercode.Length == 0 && usercode.Length == 0)
            {
                DialogHelper.ShowDialog("Fields cannot be empty");
                return;
            }

            user.syncid = new mySQLClass().GetAndInsertNextSyncId("user");
            string SQL =
            @"UPDATE `user` SET `usercode`= '" + usercode + @"', `fullname`='" + fullname + @"', 
                     `username`='" + username + @"', `password`='" + password + @"',
                     `status`=1,`datecreated`=NOW(),`lastmodifieddate`=NOW(),`userid`= " + user.syncid + @", `lastmodifiedby` = " + user.syncid + @"
                WHERE `Syncid`=" + user.getsyncid() + @" LIMIT 1";
            //lastmodifiedby
            //make sure no duplicate fields
            mySQLFunc.setdb(SQL);
            SQL = @"DELETE FROM `userauth` WHERE `userid`=" + user.getsyncid();
            mySQLFunc.setdb(SQL);

            List<string> AuthorizationCodes = new List<string>();
            foreach (KeyValuePair<string, CheckBox> dicentry in AuthDictionary)
            {
                if (dicentry.Value.Checked)
                    AuthorizationCodes.Add(dicentry.Key);
            }
            foreach (string Auth in AuthorizationCodes)
            {
                SQL = @"INSERT INTO `userauth` (`userid`,`authorization`) VALUES ( " + user.getsyncid() + @", '" + Auth + @"' )";
                mySQLFunc.setdb(SQL);
            }
            EnableViewUsersPanel();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnableViewUsersPanel();
        }
    }
}
