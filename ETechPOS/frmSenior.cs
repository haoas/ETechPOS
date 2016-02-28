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

namespace ETech
{
    public partial class frmSenior : Form
    {
        public cls_senior senior;

        public frmSenior()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.senior = new cls_senior();
        }

        private void frmSenior_Load(object sender, EventArgs e)
        {
            if (senior.get_idnumber() != "")
            {
                txtIDNo.Text = senior.get_idnumber();
                txtName.Text = senior.get_fullname();
            }
            else
                btnRemove.Visible = false;

            txtIDNo.SelectAll();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            senior = new cls_senior();
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSenior_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else
                return;
        }

        private void done_process()
        {
            string new_idnumber = this.txtIDNo.Text.Trim();
            string new_fullname = this.txtName.Text.Trim();

            if (new_idnumber.Length <= 0 || new_fullname.Length <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtIDNo.Focus();
                this.txtIDNo.SelectAll();
                return;
            }

            this.senior.set_senior(new_idnumber, new_fullname);
            this.Close();
        }

        private void txtIDNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtName.Focus();
                this.txtName.SelectAll();
                e.Handled = true;
            }
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                done_process();
                e.Handled = true;
            }
        }
    }
}
