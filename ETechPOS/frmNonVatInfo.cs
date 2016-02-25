using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.cls;
using ETech.fnc;

namespace ETech
{
    public partial class frmNonVatInfo : Form
    {
        public cls_nonvat nonvat;

        public frmNonVatInfo()
        {
            InitializeComponent();

            this.nonvat = new cls_nonvat();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }

        private void frmNonVatInfo_Load(object sender, EventArgs e)
        {
            if (this.nonvat.get_idnumber() != "")
            {
                this.txtIDNo.Text = this.nonvat.get_idnumber();
                this.txtName.Text = this.nonvat.get_fullname();
                this.txtAddress.Text = this.nonvat.get_address();
                this.txtTelNo.Text = this.nonvat.get_telno();
            }
            else
                btnRemove.Visible = false;

            this.txtIDNo.SelectAll();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            nonvat = new cls_nonvat();
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

        private void frmNonVatInfo_KeyDown(object sender, KeyEventArgs e)
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
            string new_address = this.txtAddress.Text.Trim();
            string new_telno = this.txtTelNo.Text.Trim();

            if ((new_idnumber == "") || (new_fullname == ""))
            {
                fncFilter.alert("Please Input Required Fields\n *id and *name");
                return;
            }

            this.nonvat.set_nonvat(new_idnumber, new_fullname, new_address, new_telno);
            this.Close();
        }

        private void txtIDNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || (e.KeyCode == Keys.Tab))
            {
                this.txtName.Focus();
                this.txtName.SelectAll();
                e.Handled = true;
            }
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                this.txtAddress.Focus();
                this.txtAddress.SelectAll();
                e.Handled = true;
            }
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                this.txtTelNo.Focus();
                this.txtTelNo.SelectAll();
                e.Handled = true;
            }
        }
        private void txtTelNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                done_process();
                e.Handled = true;
            }
        }
    }
}
