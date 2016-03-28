﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.fnc;
using ETech.cls;
using ETech.Standard_Views;
using ETech.Helpers;
using ETech.Variables;
using ETech.Models.Global;

namespace ETech.Views.Generic_Forms
{
    public partial class UserAuthenticationForm : StandardDialogForm
    {
        public string UserAuthorization;
        public bool HasAuthorization;
        public cls_user User;

        public UserAuthenticationForm()
        {
            InitializeComponent();
            Initialization();
        }

        private void UserAuthenticationForm_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void tbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                tbPassword.Select();
        }
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                LogIn();
        }
        private void Initialization()
        {
            InitializeVariables();
            InitializeEvents();
        }
        private void InitializeVariables()
        {
            UserAuthorization = "";
            HasAuthorization = false;
            User = null;
        }
        private void InitializeEvents()
        {
            //tbUserName.Enter += new EventHandler(ControlEventHelper.Control_HighLight_Enter);
            //tbPassword.Enter += new EventHandler(ControlEventHelper.Control_HighLight_Enter);

            tbUserName.Leave += new EventHandler(ControlEventHelper.Control_Trim_Leave);
            tbPassword.Leave += new EventHandler(ControlEventHelper.Control_Trim_Leave);
        }

        private void LogIn()
        {
            if (!IsValidInputs())
                return;
            if (!IsValidData())
                return;
            if (!CheckAuthorization())
                return;
            HasAuthorization = true;
            Close();
        }

        private bool IsValidInputs()
        {
            if (!DataValidator.HasInputData(tbUserName, MessagesVariable.RequiredUserName))
                return false;
            if (!DataValidator.HasInputData(tbPassword, MessagesVariable.RequiredPassword))
                return false;
            return true;
        }
        private bool IsValidData()
        {
            string userName = tbUserName.Text;
            string password = MD5EncryptionFunction.Encrypt(tbPassword.Text);
            string selectSql = @"SELECT * FROM `user` WHERE `status`=1 AND username = @username AND password = @password";
            SqlParameters sqlParameters = new SqlParameters();
            sqlParameters.Add(new SqlParameter("username", userName));
            sqlParameters.Add(new SqlParameter("password", password));
            DataTable resultDt = MySqlFunction.GetDataTable(new SqlDetail(selectSql, sqlParameters));
            if (resultDt.Rows.Count <= 0)
            {
                fncFilter.alert(MessagesVariable.InvalidUserNameOrPasswod);
                ClearData();
                tbUserName.Select();
                return false;
            }

            string code = resultDt.Rows[0]["usercode"].ToString();
            string fullName = resultDt.Rows[0]["fullname"].ToString();
            string syncId = resultDt.Rows[0]["SyncId"].ToString();
            selectSql = @"SELECT * FROM `userauth` WHERE `userid` = @userwid";
            sqlParameters.Clear();
            sqlParameters.Add(new SqlParameter("userwid", syncId));
            resultDt = MySqlFunction.GetDataTable(new SqlDetail(selectSql, sqlParameters));
            List<string> Authorizations = new List<string>();
            foreach (DataRow dr in resultDt.Rows)
                Authorizations.Add(dr["authorization"].ToString());
            User = new cls_user();
            User.setcls_user(code, fullName, Authorizations, Convert.ToInt32(syncId));
            return true;
        }
        private bool CheckAuthorization()
        {
            if (!User.CheckAuth(UserAuthorization))
            {
                DialogHelper.ShowDialog(MessagesVariable.MessageYourAccountHasNoAuthorization);
                User = null;
                return false;
            }
            return true;
        }

        private void ClearData()
        {
            tbUserName.Text = "";
            tbPassword.Text = "";
        }
    }
}