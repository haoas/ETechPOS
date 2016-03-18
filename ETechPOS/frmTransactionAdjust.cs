﻿using System;
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
    public partial class frmTransactionAdjust : Form
    {
        public bool iscomplete = false;
        public decimal new_price;
        public decimal new_adjust;
        public decimal new_discount;
        public cls_discount disc;
        public int new_customDiscWID;

        public decimal orig_price;
        public cls_discountlist disclist;

        public frmTransactionAdjust()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.disc = new cls_discount();

            this.orig_price = 0;
            this.disclist = new cls_discountlist(0);
            this.new_customDiscWID = 0;
        }

        private void frmTransactionAdjust_Load(object sender, EventArgs e)
        {
            lblOrigPrice_d.Text = this.new_price.ToString("N2");
            lblNewPrice_d.Text = this.new_price.ToString("N2");

            txtAdjustTo.Focus();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            txtAdjustTo.AsUnsigned2DecimalTextBox();
            txtDiscount.AsUnsigned2DecimalTextBox();
        }

        private void frmTransactionAdjust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)   done_process();
            else if (e.KeyCode == Keys.Escape)  this.Close();
            else if(e.KeyCode == Keys.F12)  RemoveButtonClicked();
            return;
        }

        private void txtAdjustTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.disc.get_wid() != 0)
            {
                MessageBox.Show("Please remove custom discount first.");
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                if (txtDiscount.Enabled == false)
                {
                    done_process();
                }
                else
                {
                    this.txtDiscount.Focus();
                }
                e.Handled = true;
            }
        }
        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtAdjustTo.Enabled == false)
                {
                    if (fncFilter.getDecimalValue(this.txtDiscount.Text) > 100 ||
                        fncFilter.getDecimalValue(this.txtDiscount.Text) < 0)
                    {
                        fncFilter.alert(cls_globalvariables.warning_discount_outofrange);
                        this.txtDiscount.Focus();
                        this.txtDiscount.SelectAll();
                    }
                    else
                    {
                        done_process();
                    }
                }
                else
                {
                    this.txtAdjustTo.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtAdjustTo_TextChanged(object sender, EventArgs e)
        {
            if (txtAdjustTo.Text != "")
            {
                txtDiscount.Enabled = false;
                refresh_new_price();
            }
            else
            {
                lblNewPrice_d.Text = lblOrigPrice_d.Text;
                txtDiscount.Enabled = true;
            }
        }
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text != "")
            {
                txtAdjustTo.Enabled = false;
                refresh_new_price();
            }
            else
            {
                lblNewPrice_d.Text = lblOrigPrice_d.Text;
                txtAdjustTo.Enabled = true;
            }
        }

        private void refresh_new_price()
        {
            decimal adjust = fncFilter.getDecimalValue(txtAdjustTo.Text);
            decimal discount = fncFilter.getDecimalValue(txtDiscount.Text);
            if (adjust != 0 && txtAdjustTo.Enabled == true)
            {
                lblNewPrice_d.Text = adjust.ToString("N2");
            }
            else if (discount != 0 && txtDiscount.Enabled == true)
            {
                lblNewPrice_d.Text = (this.new_price * (1 - (discount / 100))).ToString("N2");
            }
            else
            {
                lblNewPrice_d.Text = this.new_price.ToString("N2");
            }
        }
        public void done_process()
        {
            decimal prodadjust = (this.txtAdjustTo.Enabled) ? fncFilter.getDecimalValue(this.txtAdjustTo.Text) : 0;
            decimal discount = (this.txtDiscount.Enabled) ? fncFilter.getDecimalValue(this.txtDiscount.Text) : 0;

            if (prodadjust != 0)
            {
                if (prodadjust < 0)
                {
                    fncFilter.alert(cls_globalvariables.warning_price_invalid);
                    return;
                }
                this.new_adjust = prodadjust - this.new_price;
                this.new_discount = 0;
                //this.new_discount = Math.Round(1 - (prodadjust / this.new_price), 6);
                //this.new_adjust = 0;
            }
            else if (discount != 0)
            {
                this.new_adjust = 0;
                this.new_discount = Math.Round( discount / 100, 6);
            }
            else
            {
                this.new_adjust = 0;
                this.new_discount = 0;
            }
            iscomplete = true;
            this.Close();
            return;
        }
        private void RemoveButtonClicked()
        {
            if (MessageBox.Show("Are you sure you want to clear the transaction discounts?", "Confirm Box",
                         MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.disc = new cls_discount();
                this.lblCustomDiscount.Text = "Regular Discount";
                this.disclist.disable_all_discounts();
                this.iscomplete = true;
                this.Close();
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveButtonClicked();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
