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
    public partial class frmWholesale : Form
    {
        public cls_product cproduct;

        public frmWholesale()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            cproduct = new cls_product();
        }

        private void frmWholesale_Load(object sender, EventArgs e)
        {
            lblProduct_d.Text = cproduct.getProductName();
            lblUnitPrice_d.Text = cproduct.getRetailWholesalePrice().ToString("N2");
            txtQty.Text = "1";
            txtPrice.Text = cproduct.getRetailWholesalePrice().ToString("N2");

            txtQty.Focus();
            txtQty.SelectAll();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        private void frmWholesale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                btnOK_Click(btnOK, new EventArgs());
            else if (e.KeyCode == Keys.Escape)
                btnESC_Click(btnESC, new EventArgs());
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                txtPrice.Focus();
                txtPrice.SelectAll();
            }
        }
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                done_process();
            }
        }

        public void done_process()
        {
            decimal new_qty = fncFilter.getDecimalValue(this.txtQty.Text);
            if (new_qty == 0)
            {
                fncFilter.alert(cls_globalvariables.warning_quantity_invalid);
                return;
            }

            decimal new_price = fncFilter.getDecimalValue(this.txtPrice.Text);
            if (new_price <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_price_invalid);
                return;
            }

            this.cproduct.setQty(new_qty);
            this.cproduct.setRetailWholesalePrice(new_price);            

            this.Close();
            return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            cproduct.setQty(0);
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
