using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.fnc;
using ETech.cls;

namespace ETECHPOS.Views.Forms.Generics
{
    public partial class UserAuthenticationForm : Form
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
    }
}
