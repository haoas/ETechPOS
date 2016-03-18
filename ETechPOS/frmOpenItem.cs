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
using ETech.FormatDesigner;

namespace ETech
{
    public partial class frmOpenItem : Form
    {
        public decimal prodprice;
        public decimal quantity;

        public frmOpenItem()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }

        public void done_process()
        {
            decimal price = fncFilter.getDecimalValue(txtPrice.Text);
            decimal qty = fncFilter.getDecimalValue(txtQty.Text);
            if (qty == 0)
                qty = 1;


            if (price <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtPrice.Focus();
                this.txtPrice.SelectAll();
                return;
            }

            if (qty < 0)
            {
                fncFilter.alert("Refund is only allowed through 'Change Qty'.");
                return;
            }

            this.prodprice = price;
            this.quantity = qty;
            this.Close();
            return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.prodprice = 0;
            this.quantity = 0;
            this.Close();
        }

        private void frmOpenItem_KeyDown(object sender, KeyEventArgs e)
        {
            Control nextControl;
            if (e.KeyCode == Keys.Enter)
            {
                nextControl = GetNextControl(ActiveControl, !e.Shift);
                if (nextControl == null)
                    nextControl = GetNextControl(null, true);
                nextControl.Focus();
            }
            else if (e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                this.prodprice = 0;
                this.quantity = 0;
                this.Close();
                return;
            }
            else
                return;
        }
        private void frmOpenItem_Load(object sender, EventArgs e)
        {
            txtPrice.Focus();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            txtQty.AsSigned2DecimalTextBox();
            txtPrice.AsUnsigned2DecimalTextBox();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                done_process();
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
