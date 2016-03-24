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
    public partial class frmTransactionDiscountList : Form
    {
        private cls_discountlist disclist;
        private decimal totalAmt;
        public void setTotalAmt(decimal val) { this.totalAmt = val; }
        public void setDiscList(cls_discountlist val) { this.disclist = val; }
 
        public frmTransactionDiscountList()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);

            this.disclist = new cls_discountlist(0);
            this.totalAmt = 0;
        }

        private void frmTransactionDiscountList_Load(object sender, EventArgs e)
        {
            this.lblAmtFrom.Text = this.totalAmt.ToString("N2");           

            foreach(cls_discount disc in this.disclist.get_discount_list()){
                if(!disc.get_status())
                    continue;
                DataGridViewRow row = (DataGridViewRow)this.dgDiscounts.Rows[0].Clone();
                row.Cells[0].Value = disc.get_name();
                row.Cells[1].Value = disc.get_basis();
                row.Cells[2].Value = (disc.get_ismultiple()) ? ((1 - disc.get_value()) * 100).ToString()+"%" : disc.get_value().ToString("N2");
                row.Cells[3].Value = disc.get_SyncId();
                row.Cells[4].Value = disc.get_status();
                row.Cells[5].Value = disc.get_ismultiple();
                row.Cells[6].Value = disc.get_value();
                this.dgDiscounts.Rows.Add(row);
            }

            this.dgDiscounts.AllowUserToAddRows = false;

            if (this.dgDiscounts.Rows.Count == 0)
            {
                this.lblAmtTo.Text = this.totalAmt.ToString("N2");
                return;
            }
            this.dgDiscounts.CurrentCell = this.dgDiscounts.Rows[this.dgDiscounts.Rows.Count-1].Cells[1];
            this.dgDiscounts.Rows[this.dgDiscounts.Rows.Count - 1].Selected = true;
            refresh_amounts();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        private void frmTransactionDiscountList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.Close();
            else if(e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                select_next();
                refresh_amounts();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                select_previous();
                refresh_amounts();
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                e.Handled = true;
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh_amounts()
        {
            if (this.dgDiscounts.Rows.Count == 0)
                return;

            int discwid = Convert.ToInt32(this.dgDiscounts.CurrentRow.Cells[3].Value);
            decimal basis_before_disc = this.disclist.get_basis_before_discount(discwid, this.totalAmt);
            decimal amt_before_disc = this.disclist.get_last_amt_before_discount(discwid, this.totalAmt);

            bool ismultiple = Convert.ToBoolean(this.dgDiscounts.CurrentRow.Cells[5].Value);
            decimal value = Convert.ToDecimal(this.dgDiscounts.CurrentRow.Cells[6].Value);
            this.lblAmtFrom.Text = amt_before_disc.ToString("N2");
            this.lblAmtTo.Text = ((ismultiple) ? amt_before_disc - (basis_before_disc * (1 - value)) : amt_before_disc + value).ToString("N2");
        }
        private void select_next()
        {
            try
            {
                int row_index = this.dgDiscounts.CurrentRow.Index + 1;

                if (row_index > this.dgDiscounts.Rows.Count - 1)
                    return;

                this.dgDiscounts.CurrentCell = this.dgDiscounts.Rows[row_index].Cells[1];
                this.dgDiscounts.Rows[row_index].Selected = true;
            }
            catch(Exception){
                Console.WriteLine("No Discount in list");
            }
        }
        private void select_previous()
        {
            try
            {
                int row_index = this.dgDiscounts.CurrentRow.Index - 1;

                if (row_index < 0)
                    return;

                this.dgDiscounts.CurrentCell = this.dgDiscounts.Rows[row_index].Cells[1];
                this.dgDiscounts.Rows[row_index].Selected = true;
            }
            catch (Exception)
            {
                Console.WriteLine("No Discount in list");
            }
        }
    }
}
