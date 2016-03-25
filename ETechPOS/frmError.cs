using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.fnc;
using ETECHPOS.Helpers;

namespace ETech
{
    public partial class frmError : Form
    {
        public string errormessage;

        public frmError()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);

            errormessage = "";
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            this.lblError.Text = this.errormessage;

            LogsHelper.Print("ERROR MESSAGE: " + this.errormessage);

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        private void frmError_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                this.Close();
                return;
            }
            else
                return;
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
