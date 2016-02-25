﻿using System;
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
    public partial class frmProductQuantity : Form
    {
        //public cls_product prod;
        public decimal new_qty;
        public string productname;
        public int productid;
        public bool delete_permission;
        public bool return_permission;
        public bool forcereturn_permission;
        public string salesdetailmemo;
        public int salesmode = 0;
        // 0 = online 1 = offline

        public frmProductQuantity()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            delete_permission = false;
            return_permission = false;
        }

        private void frmProductQuantity_Load(object sender, EventArgs e)
        {
            lblProductName.Text = this.productname;
            lblOldQty_d.Text = this.new_qty.ToString("G29");
            txtNewQty_d.Text = this.new_qty.ToString("G29");
            txtNewQty_d.SelectAll();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        private void frmProductQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else
                return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void done_process()
        {
            decimal new_qty;
            bool isNum = decimal.TryParse(txtNewQty_d.Text, out new_qty);

            if (isNum)
            {

                decimal old_qty = fncFilter.getDecimalValue(this.lblOldQty_d.Text);
                if (new_qty >= old_qty)
                {
                    this.new_qty = new_qty;
                    this.Close();
                    return;
                }

                bool permcheck = false;
                if (new_qty == 0 && this.delete_permission == true)
                {
                    permcheck = true;
                }
                else if (new_qty < 0 && this.return_permission == true)
                {
                    permcheck = true;
                }
                else if (new_qty < old_qty && this.return_permission == true)
                {
                    permcheck = true;
                }
                else if (new_qty < old_qty && this.forcereturn_permission == true)
                {
                    permcheck = true;
                }
                else if (new_qty == 0)
                {
                    frmPermissionCode frmpermcode = new frmPermissionCode();
                    frmpermcode.permission_needed = fncFilter.get_permission_delete();
                    frmpermcode.ShowDialog();
                    permcheck = frmpermcode.permcode;
                }
                else
                {
                    frmPermissionCode frmpermcode = new frmPermissionCode();
                    frmpermcode.permission_needed = fncFilter.get_permission_return();
                    frmpermcode.ShowDialog();
                    permcheck = frmpermcode.permcode;
                }

                if (permcheck)
                {
                    if (new_qty < 0 && cls_globalvariables.RefundMemo_v == "2")
                    {
                        frmRefundInfo refundinfo = new frmRefundInfo();
                        refundinfo.productid = productid;
                        refundinfo.negative_qty = new_qty;
                        refundinfo.productname = productname;
                        refundinfo.forcereturn_permission = forcereturn_permission;
                        refundinfo.ShowDialog();
                        if (refundinfo.issuccess)
                        {
                            this.new_qty = new_qty;
                            this.salesdetailmemo = refundinfo.salesdetailmemo;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.new_qty = new_qty;
                        this.salesdetailmemo = "";
                        this.Close();
                        return;
                    }
                }
            }
            else
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                txtNewQty_d.Focus();
                txtNewQty_d.SelectAll();
                return;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtNewQty_d_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (salesmode == 1) //offline
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '-' && (sender as TextBox).Text.Contains('-'))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
