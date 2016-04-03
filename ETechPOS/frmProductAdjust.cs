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
using ETech.Helpers;

namespace ETech
{
    public partial class frmProductAdjust : Form
    {
        public cls_product prod;

        public frmProductAdjust()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();
            fncFilter.set_theme_color(this);
        }

        private void frmProductAdjust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            else if (e.KeyCode == Keys.F1) BtnF1.PerformClick();
            else if (e.KeyCode == Keys.F2) BtnF2.PerformClick();
            else if (e.KeyCode == Keys.F3) BtnF3.PerformClick();
            else if (e.KeyCode == Keys.F4) BtnF4.PerformClick();
            else if (e.KeyCode == Keys.F11) RemoveButtonClicked();
            else if (e.KeyCode == Keys.Enter) done_process();
            else if (e.KeyCode == Keys.F12) done_process();
            else return;
        }

        private void txtAdjustTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtDiscount.Enabled == false)
                    done_process();
                else
                    txtDiscount.Focus();
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
                lblNewPrice_d.Text = (prod.OriginalPrice * (1 - discount / 100)).ToString("N2");
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
                prod.RegularFixedDiscount = prod.OriginalPrice - prodadjust;
            }
            else if (discount != 0)
            {
                prod.RegularDiscountPercentage = discount;
            }
            else
            {
                prod.RegularFixedDiscount = 0;
                prod.RegularDiscountPercentage = 0;
            }
            this.Close();
            return;
        }

        private void frmProductAdjust_Load(object sender, EventArgs e)
        {
            lblProductName.Text = prod.Name;
            lblOrigPrice_d.Text = prod.OriginalPrice.ToString();

            if (prod.RegularFixedDiscount != 0)
            {
                txtAdjustTo.Enabled = true;
                txtDiscount.Enabled = false;
                txtAdjustTo.Text = (prod.OriginalPrice - prod.RegularFixedDiscount).ToString("N2");
                txtAdjustTo.Focus();
                txtAdjustTo.SelectAll();
            }
            else if (prod.RegularDiscountPercentage != 0)
            {
                txtAdjustTo.Enabled = false;
                txtDiscount.Enabled = true;
                txtDiscount.Text = prod.RegularDiscountPercentage.ToString();
                txtDiscount.Focus();
                txtDiscount.SelectAll();
            }
            else
            {
                txtDiscount.Text = string.Empty;
                txtDiscount.Focus();
            }

            refresh_new_price();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            txtAdjustTo.AsUnsigned2DecimalTextBox();
            txtDiscount.AsUnsigned2DecimalTextBox();
        }

        private void RemoveButtonClicked()
        {
            if (DialogHelper.ShowDialog("Are you sure you want to clear product discount?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                prod.RegularDiscountPercentage = 0;
                prod.RegularFixedDiscount = 0;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnProceed_Click(object sender, EventArgs e)
        {
            done_process();
        }

        private void Btn11_Click(object sender, EventArgs e)
        {
            RemoveButtonClicked();
        }

        private void BtnF1_Click(object sender, EventArgs e)
        {
            txtAdjustTo.Clear();
            txtDiscount.Text = "5";
            done_process();
        }

        private void BtnF2_Click(object sender, EventArgs e)
        {
            txtAdjustTo.Clear();
            txtDiscount.Text = "10";
            done_process();
        }

        private void BtnF3_Click(object sender, EventArgs e)
        {
            txtAdjustTo.Clear();
            txtDiscount.Text = "15";
            done_process();
        }

        private void BtnF4_Click(object sender, EventArgs e)
        {
            txtAdjustTo.Clear();
            txtDiscount.Text = "20";
            done_process();
        }
    }
}
