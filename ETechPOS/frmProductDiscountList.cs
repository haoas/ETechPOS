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
    public partial class frmProductDiscountList : Form
    {
        cls_productlist prodList;

        public frmProductDiscountList()
        {
            InitializeComponent();
        }

        public void setProductList(cls_productlist val)
        {
            this.prodList = val;
        } 

        private void productDiscountList_Load(object sender, EventArgs e)
        {
            foreach (cls_product prod in this.prodList.get_productlist())
            {
                this.dgproducts.Rows.Add(prod.getProductName(),
                                         prod.getOrigPrice().ToString("N2"),
                                         prod.getWid());
            }
            select_cell(this.dgproducts, 0, 1);
            this.dgDiscounts.ClearSelection();
            this.dgDiscounts.AllowUserToAddRows = false;
            this.dgproducts.AllowUserToAddRows = false;

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgDiscounts);
            fncFilter.set_dgv_controls(dgproducts);
        }
        private void frmproductDiscountList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                this.dgproducts.Enabled = false;
                this.dgDiscounts.Enabled = true;
                this.dgDiscounts.Focus();
                this.dgproducts.ClearSelection();
                select_cell(this.dgDiscounts, 0, 1);
            }
            else if (e.KeyCode == Keys.Left)
            {
                e.Handled = true;
                this.dgproducts.Enabled = true;
                this.dgDiscounts.Enabled = false;
                this.dgproducts.Focus();
                this.dgDiscounts.ClearSelection();
                select_cell(this.dgproducts, 0, 1);
            }
            else if(e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void select_cell(DataGridView dg, int row, int col)
        {
            if (dg.Rows.Count == 0)
                return;
            dg.CurrentCell = dg.Rows[row].Cells[col];
            dg.Rows[row].Selected = true;
        }

        private void refresh_discount_list()
        {
            if (this.dgproducts.Rows.Count == 0)
                return;

            try
            {
                this.dgDiscounts.Rows.Clear();
                int rowindex = this.dgproducts.CurrentCell.RowIndex;
                if (rowindex > this.prodList.get_productlist().Count - 1)
                    return;

                cls_product prod = this.prodList.get_product(rowindex);

                foreach (cls_discount disc in prod.getProductDiscountList().get_discount_list())
                {
                    if (!disc.get_status())
                        continue;
                    this.dgDiscounts.Rows.Add(disc.get_name(),
                                              disc.get_basis(),
                                              (disc.get_ismultiple()) ? ((1 - disc.get_value()) * 100).ToString("N2") + "%" : disc.get_value().ToString("N2"),
                                              disc.get_wid(),
                                              disc.get_status(),
                                              disc.get_ismultiple(),
                                              disc.get_value());
                }

                decimal prod_perc_disc = prod.getProductDiscountList().get_discounts_percentage(prod.getOrigPrice());
                decimal headdisc = (prod.getDiscount() - 1) / (1 - prod_perc_disc) * -1;
                if (headdisc > 0)
                {
                    this.dgDiscounts.Rows.Add("Transaction Discount",
                                                  "-",
                                                  ((1-headdisc)*100).ToString("N2") + "%",
                                                  -99, //any value as long as not equal to any wid or type.
                                                  true,
                                                  1,
                                                  headdisc);
                }
                
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No product");
            }
            this.lblAmtFrom.Text = "";
            this.lblAmtTo.Text = "";
            this.dgDiscounts.ClearSelection();
        }
        public void refresh_discount_info()
        {
            if (this.dgDiscounts.Rows.Count == 0)
                return;
            
            int dc_rowindex = this.dgDiscounts.CurrentCell.RowIndex;
            int prod_rowindex = this.dgproducts.CurrentCell.RowIndex;

            int dc_wid = Convert.ToInt32(this.dgDiscounts.Rows[dc_rowindex].Cells[3].Value);
            bool ismultiple = Convert.ToBoolean(this.dgDiscounts.CurrentRow.Cells[5].Value);
            decimal value = Convert.ToDecimal(this.dgDiscounts.CurrentRow.Cells[6].Value);

            cls_product prod = this.prodList.get_product(prod_rowindex);
            
            decimal amt_before_disc = prod.get_last_amt_before_discount(dc_wid);
            decimal basis_before_disc = (dc_wid == -99) ? amt_before_disc : prod.get_basis_before_discount(dc_wid);
            
            this.lblAmtFrom.Text = amt_before_disc.ToString("N2");
            this.lblAmtTo.Text = ((ismultiple) ? amt_before_disc - (basis_before_disc * (1 - value)) : amt_before_disc + value).ToString("N2");
        }

        private void dgproducts_SelectionChanged(object sender, EventArgs e)
        {
            refresh_discount_list();
        }
        private void dgDiscounts_SelectionChanged(object sender, EventArgs e)
        {
            refresh_discount_info();
        }
    }
}
