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
using MySql.Data.MySqlClient;
using ETech.fnc;

namespace ETech
{
    public partial class frmCustomDiscount : Form
    {
        private decimal product_price;
        private decimal discount_value;
        private int discountWID;
        private cls_discount discount;

        private int headdetail;

        cls_discountlist disclist;

        public frmCustomDiscount(decimal price, int headdetail)
        {
            InitializeComponent();

            this.dg_discounts.Focus();
            this.product_price = price;
            this.discount_value = 0;
            this.discountWID = 0;
            this.headdetail = headdetail;
            this.disclist = new cls_discountlist(0);
            this.discount = new cls_discount();
        }

        public void passDiscountlist(cls_discountlist disc)
        {
            this.disclist = disc;
        }

        private void frmCustomDetailDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.discount_value = 0;
                this.Close();
            }
            if(e.KeyCode == Keys.F1){
                this.selectDiscount();
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.selectDiscount();
                e.Handled = true;
            }
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right){
                e.Handled = true;
            }
        }
        private void frmCustomDetailDiscount_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dg_discounts);

            string query = @" select T.wid, T.`particular`, ROUND(T.value*100,2) as value 
                              from `discounttype` as T, discounthierarchy as H 
                              where H.discountid = T.wid and T.headdetail = "+this.headdetail+@" and T.`status` = 1
                                and (T.`type` < 0 or T.`type` >= " + cls_globalvariables.dchead_defaultcustom .ToString()+ @")
                                and T.`branchid` = " + cls_globalvariables.BranchCode.ToString() + @" 
                              order by H.`position`";

            DataTable dt = mySQLFunc.getdb(query);

            if (dt.Rows.Count <= 0)
            {
                this.lbl_origPrice.Text = "P"+this.product_price.ToString("N2");
                this.lbl_newPrice.Text = "P"+this.product_price.ToString("N2");
                return;
            }

            this.dg_discounts.DataSource = dt;
            this.dg_discounts.CurrentCell = this.dg_discounts.Rows[0].Cells[1];
            this.dg_discounts.Rows[0].Selected = true;
            this.refreshValue();
            fncFilter.set_dgv_controls(dg_discounts);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selectDiscount();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.discount_value = 0;
            this.Close();
        }

        public void selectDiscount()
        {
            if (this.dg_discounts.Rows.Count > 0)
            {
                this.discount_value = fncFilter.getDecimalValue(this.dg_discounts.CurrentRow.Cells[2].Value.ToString()) / 100;
                this.discountWID = fncFilter.getIntegerValue(this.dg_discounts.CurrentRow.Cells[0].Value.ToString());
                this.discount = this.disclist.get_discount_using_wid(this.discountWID);
                this.Close();
            }
        }
        public void refreshValue()
        {
            decimal value = fncFilter.getDecimalValue(this.dg_discounts.CurrentRow.Cells[2].Value.ToString()) / 100;
            int discountWID = fncFilter.getIntegerValue(this.dg_discounts.CurrentRow.Cells[0].Value.ToString());
            
            decimal basis_before_disc = this.disclist.get_basis_before_discount(discountWID, this.product_price);
            decimal amt_before_disc = this.disclist.get_last_amt_before_discount(discountWID, this.product_price);

            this.lbl_origPrice.Text = "P" + amt_before_disc.ToString("N2");
            this.lbl_newPrice.Text = "P" + (amt_before_disc - (basis_before_disc * (value))).ToString("N2");
        }

        private void dg_discounts_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.refreshValue();
        }

        public decimal getDiscountValue()
        {
            return this.discount_value;
        }
        public int getDiscountWID()
        {
            return this.discountWID;
        }
        public cls_discount getDiscount()
        {
            return this.discount;
        }

        
    }
}
