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
    public partial class frmCashDenomination : Form
    {

        public cls_bills cash_bills;

        public frmCashDenomination()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);

            txt1000.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt500.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt200.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt100.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt50.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt20.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt10.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt5.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt1.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt25c.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt10c.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);
            txt5c.KeyPress += new KeyPressEventHandler(Numeric_KeyPress);

            refresh_total_amount();
            cls_globalfunc.formaddkbkpevent(this);
        }

        private void refresh_total_amount()
        {
            int bill_1000 = fncFilter.getIntegerValue(this.txt1000.Text);
            int bill_500 = fncFilter.getIntegerValue(this.txt500.Text);
            int bill_200 = fncFilter.getIntegerValue(this.txt200.Text);
            int bill_100 = fncFilter.getIntegerValue(this.txt100.Text);
            int bill_50 = fncFilter.getIntegerValue(this.txt50.Text);
            int bill_20 = fncFilter.getIntegerValue(this.txt20.Text);
            int bill_10 = fncFilter.getIntegerValue(this.txt10.Text);
            int bill_5 = fncFilter.getIntegerValue(this.txt5.Text);
            int bill_1 = fncFilter.getIntegerValue(this.txt1.Text);
            int bill_25c = fncFilter.getIntegerValue(this.txt25c.Text); 
            int bill_10c = fncFilter.getIntegerValue(this.txt10c.Text);
            int bill_5c = fncFilter.getIntegerValue(this.txt5c.Text);

            decimal total = 0;

            total = (bill_1000 * 1000) +
                    (bill_500 * 500) +
                    (bill_200 * 200) +
                    (bill_100 * 100) +
                    (bill_50 * 50) +
                    (bill_20 * 20) +
                    (bill_10 * 10) +
                    (bill_5 * 5) +
                    (bill_1 * 1) +
                    (decimal)(bill_25c * 0.25) +
                    (decimal)(bill_10c * 0.10) +
                    (decimal)(bill_5c * 0.05);

            this.txt1000.Text = bill_1000.ToString();
            this.txt500.Text = bill_500.ToString();
            this.txt200.Text = bill_200.ToString();
            this.txt100.Text = bill_100.ToString();
            this.txt50.Text = bill_50.ToString();
            this.txt20.Text = bill_20.ToString();
            this.txt10.Text = bill_10.ToString();
            this.txt5.Text = bill_5.ToString();
            this.txt1.Text = bill_1.ToString();
            this.txt25c.Text = bill_25c.ToString();
            this.txt10c.Text = bill_10c.ToString();
            this.txt5c.Text = bill_5c.ToString();
            this.lblTotal.Text = total.ToString("N");           
        }

        private void txt1000_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt500_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt200_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt100_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt50_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt20_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt10_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt5_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt1_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt25c_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt10c_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txt5c_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }

        private void frmCashDenomination_KeyDown(object sender, KeyEventArgs e)
        {
            Control nextControl;
            //Checks if the Enter Key was Pressed
            if (e.KeyCode == Keys.Enter && txt5c.Focused == false)
            {
                //If so, it gets the next control and applies the focus to it
                nextControl = GetNextControl(ActiveControl, !e.Shift);
                if (nextControl == null)
                {
                    nextControl = GetNextControl(null, true);
                }
                nextControl.Focus();
                //Finally - it suppresses the Enter Key
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                saving();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
            else
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saving();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saving()
        {
            int bill_1000 = fncFilter.getIntegerValue(this.txt1000.Text);
            int bill_500 = fncFilter.getIntegerValue(this.txt500.Text);
            int bill_200 = fncFilter.getIntegerValue(this.txt200.Text);
            int bill_100 = fncFilter.getIntegerValue(this.txt100.Text);
            int bill_50 = fncFilter.getIntegerValue(this.txt50.Text);
            int bill_20 = fncFilter.getIntegerValue(this.txt20.Text);
            int bill_10 = fncFilter.getIntegerValue(this.txt10.Text);
            int bill_5 = fncFilter.getIntegerValue(this.txt5.Text);
            int bill_1 = fncFilter.getIntegerValue(this.txt1.Text);
            int bill_25c = fncFilter.getIntegerValue(this.txt25c.Text);
            int bill_10c = fncFilter.getIntegerValue(this.txt10c.Text);
            int bill_5c = fncFilter.getIntegerValue(this.txt5c.Text);

            this.cash_bills.setBills(bill_1000, bill_500, bill_200, bill_100, bill_50,
                                     bill_20, bill_10, bill_5, bill_1, bill_25c, bill_10c,
                                     bill_5c);
            this.Close();
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                return;
            else
                e.Handled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmCashDenomination_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
    }
}
