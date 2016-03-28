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
        public cls_product openitem = null;

        public frmOpenItem()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
        }

        public void proceed()
        {
            string memo = txtMemo.Text.Trim();
            decimal price = txtPrice.Text.ToRoundedDecimal();
            decimal qty = txtQty.Text.ToRoundedDecimal();

            qty = (qty == 0) ? 1 : qty;
            if (memo.Length <= 0 && price <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtMemo.Focus();
                this.txtMemo.SelectAll();
                return;
            }

            openitem = new cls_product(price, 0, qty);
            openitem.Memo = memo;
            openitem.Name = "[OPENITEM]: " + memo;
            this.Close();
            return;
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
            else if (e.KeyCode == Keys.F12)
                proceed();
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else
                return;
        }
        private void frmOpenItem_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            txtPrice.AsUnsigned2DecimalTextBox();
            txtQty.AsSigned2DecimalTextBox();
            txtMemo.Focus();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQty.Focus();
                e.Handled = true;
            }
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                proceed();
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Proceed_Click(object sender, EventArgs e)
        {
            proceed();
        }
    }
}
