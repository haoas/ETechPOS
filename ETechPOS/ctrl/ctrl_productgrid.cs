using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using ETech.cls;
using ETech.fnc;

namespace ETech.ctrl
{
    public class ctrl_productgrid
    {
        private DataGridView dgvProduct;
        private BindingSource bdsProduct;
        private DataTable dtProduct;

        public ctrl_productgrid(DataGridView dgv_d)
        {
            this.dgvProduct = dgv_d;
            this.dgvProduct.AutoGenerateColumns = false;
            this.dgvProduct.MultiSelect = false;
        }

        public void initial_display()
        {
            this.dgvProduct.Enabled = false;
            this.bdsProduct = new BindingSource();
            this.dtProduct = new DataTable();
            this.bdsProduct.DataSource = dtProduct;
            this.dgvProduct.DataSource = this.bdsProduct;
        }

        public void set_databinding(DataTable dtProduct_d)
        {
            this.dtProduct = dtProduct_d;
            this.bdsProduct = new BindingSource();
            this.bdsProduct.DataSource = dtProduct_d;
            this.dgvProduct.DataSource = this.bdsProduct;
            this.dgvProduct.Enabled = true;
            this.bdsProduct.ListChanged += new ListChangedEventHandler(this.bdsProduct_ListChanged);
        }

        public DataGridView get_productgrid()
        {
            return this.dgvProduct;
        }

        private void select_row(int row_index)
        {
            fncFilter.gridview_selectrow(dgvProduct, row_index);
        }

        public void select_next()
        {
            fncFilter.gridview_selectnextrow(dgvProduct);
        }

        public void select_previous()
        {
            fncFilter.gridview_selectpreviousrow(dgvProduct);
        }

        private void bdsProduct_ListChanged(object sender, ListChangedEventArgs e)
        {
            int rowcount = this.dgvProduct.Rows.Count;
            if (rowcount > 0)
            {
                if (e.NewIndex < 0)
                {
                    this.dgvProduct.Rows[0].Selected = true;
                    this.dgvProduct.CurrentCell = this.dgvProduct[0, 0];
                }
                else if (e.NewIndex >= rowcount)
                {
                    this.dgvProduct.Rows[rowcount - 1].Selected = true;
                    this.dgvProduct.CurrentCell = this.dgvProduct[0, rowcount - 1];
                }
                else
                {
                    this.dgvProduct.Rows[e.NewIndex].Selected = true;
                    this.dgvProduct.CurrentCell = this.dgvProduct[0, e.NewIndex];
                }
            }
        }

        public DataGridViewRow get_currentrow()
        {
            if (this.dgvProduct.Rows.Count <= 0)
                return null;

            return this.dgvProduct.CurrentRow;
        }
    }
}
