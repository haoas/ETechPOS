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
    public partial class frmProductAdjust : Form
    {
        public bool iscomplete = false;
        public decimal new_price;
        public decimal new_adjust;
        public decimal new_discount;
        public int customDiscWID;
        public string productname;

        public decimal orig_price;
        public decimal orig_pricea;
        public decimal orig_priceb;
        public decimal orig_pricec;
        public decimal orig_priced;
        public decimal orig_pricee;
        public cls_discountlist disclist;
        public cls_discount disc;

        public frmProductAdjust()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.customDiscWID = 0;

            this.orig_price = 0;
            this.disclist = new cls_discountlist(0);
            this.disc = new cls_discount();
        }

        private void frmProductAdjust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) done_process();
            else if (e.KeyCode == Keys.Escape) this.Close();
            else if (e.KeyCode == Keys.F6) CustomerPrices("A");
            else if (e.KeyCode == Keys.F7) CustomerPrices("B");
            else if (e.KeyCode == Keys.F8) CustomerPrices("C");
            else if (e.KeyCode == Keys.F9) CustomerPrices("D");
            else if (e.KeyCode == Keys.F10) CustomerPrices("E");
            else if (e.KeyCode == Keys.F11) CustomButtonClicked();
            else if (e.KeyCode == Keys.F12) RemoveButtonClicked();
            else
                return;
        }

        private void txtAdjustTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (disc.get_wid() != 0)
            {
                MessageBox.Show("Please remove custom discount first.");
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                if (txtDiscount.Enabled == false)
                    done_process();
                else
                    txtDiscount.Focus();
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
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
                //this.new_discount = 1 - (prodadjust / this.new_price);
                //this.new_adjust = 0;
            }
            else if (discount != 0)
            {
                this.new_adjust = 0;
                this.new_discount = Math.Round(discount / 100, 6);
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

        private void frmProductAdjust_Load(object sender, EventArgs e)
        {
            lblProductName.Text = this.productname;
            lblOrigPrice_d.Text = this.new_price.ToString("N2");
            if (new_adjust != 0)
            {
                txtAdjustTo.Enabled = true;
                txtDiscount.Enabled = false;
                txtAdjustTo.Text = (this.new_price + this.new_adjust).ToString("N2");
                txtAdjustTo.SelectAll();
            }
            else if (new_discount != 0)
            {
                txtAdjustTo.Enabled = false;
                txtDiscount.Enabled = true;
                txtDiscount.Focus();
                txtDiscount.Text = (this.new_discount * 100).ToString();
                txtDiscount.SelectAll();
            }
            else
            {
                txtAdjustTo.Enabled = true;
                this.txtAdjustTo.Focus();
            }

            refresh_new_price();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void CustomButtonClicked()
        {
            if (this.txtDiscount.Enabled)
            {
                frmCustomDiscount customdiscounts = new frmCustomDiscount(this.orig_price, 1);
                customdiscounts.passDiscountlist(this.disclist);
                customdiscounts.ShowDialog();
                this.new_adjust = 0;
                this.new_discount = 0;
                if (customdiscounts.getDiscount().get_wid() != 0)
                {
                    this.disc = customdiscounts.getDiscount();
                    this.txtDiscount.Text = ((1 - this.disc.get_value()) * 100).ToString();
                    this.new_discount = (1 - this.disc.get_value()) * 100;
                    this.lblCustomDiscount.Text = this.disc.get_name();
                }
            }
        }

        private void RemoveButtonClicked()
        {
            if (MessageBox.Show("Are you sure you want to clear product discount?", "Confirm Box",
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.lblCustomDiscount.Text = "";
                this.new_discount = 0;
                this.new_adjust = 0;
                this.disc = new cls_discount();
                this.disclist.disable_all_discounts();
                this.lblCustomDiscount.Text = "Regular Discount";
                this.iscomplete = true;
                this.Close();
            }
        }

        private void CustomerPrices(string pricingtype)
        {
            if (this.txtAdjustTo.Enabled)
            {
                decimal price = 0;
                if (pricingtype == "A") price = this.orig_pricea;
                else if (pricingtype == "B") price = this.orig_priceb;
                else if (pricingtype == "C") price = this.orig_pricec;
                else if (pricingtype == "D") price = this.orig_priced;
                else if (pricingtype == "E") price = this.orig_pricee;
                else price = 0;

                if (price == 0) price = this.orig_price;

                txtAdjustTo.Text = price.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCustom_Click(object sender, EventArgs e)
        {
            CustomButtonClicked();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveButtonClicked();
        }
        private void btnPriceA_Click(object sender, EventArgs e)
        {
            CustomerPrices("A");
        }
        private void btnPriceB_Click(object sender, EventArgs e)
        {
            CustomerPrices("B");
        }
        private void btnPriceC_Click(object sender, EventArgs e)
        {
            CustomerPrices("C");
        }
        private void btnPriceD_Click(object sender, EventArgs e)
        {
            CustomerPrices("D");
        }
        private void btnPriceE_Click(object sender, EventArgs e)
        {
            CustomerPrices("E");
        }
    }
}
