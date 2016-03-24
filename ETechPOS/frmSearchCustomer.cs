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
    public partial class frmSearchCustomer : Form
    {
        public cls_customer customer;
        private DataTable dt_temp = new DataTable();

        public frmSearchCustomer()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            cls_globalfunc.formaddkbkpevent(this);

            this.dgvCustomer.AutoGenerateColumns = false;
            this.customer = new cls_customer();
        }

        private void frmSearchCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                fncFilter.gridview_selectnextrow(this.dgvCustomer);
            }
            else if (e.KeyCode == Keys.Up)
            {
                fncFilter.gridview_selectpreviousrow(this.dgvCustomer);
            }
            else if (e.KeyCode == Keys.Enter)
                btnOk_Click(btnOk, new EventArgs());
            else if (e.KeyCode == Keys.Escape)
                btnESC_Click(btnESC, new EventArgs());
            else
                return;
        }
        private void frmSearchCustomer_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgvCustomer.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgvCustomer);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            addcustomer();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            this.txtCustomer.Focus();
        }
        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCustomer.Text.Trim().Length == 0)
            {
                dt_temp.Clear();
                this.dgvCustomer.DataSource = dt_temp;
                fncFilter.set_dgv_display(this.dgvCustomer);
                return;
            }
            string str_input = "%" + this.txtCustomer.Text.Trim() + "%";

            string SQL = @"SELECT `SyncId`, `customercode` AS 'code', `fullname`, `ownername`
                            FROM `customer`
                            WHERE `show` = 1 AND `status` = 1 AND 
	                            CONCAT(`customercode`, `fullname`, `chinesename`, `ownername`) LIKE @str_param";
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            parameters.Add("@str_param");
            values.Add(str_input);

            dt_temp = mySQLFunc.getdb(SQL, parameters, values);

            this.dgvCustomer.DataSource = dt_temp;

            if (dgvCustomer.RowCount > 0)
                this.dgvCustomer.Rows[0].Selected = true;

            fncFilter.set_dgv_display(dgvCustomer);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            addcustomer();
        }

        private void addcustomer()
        {
            if (dgvCustomer.SelectedRows.Count <= 0)
                return;
            long SyncId = Convert.ToInt32(dgvCustomer.SelectedRows[0].Cells["colWid"].Value);

            frmLoad loadForm = new frmLoad("Loading Customer Data", "Loading Screen");
            loadForm.BackgroundWorker.DoWork += (sender, e1) =>
            {
                this.customer = new cls_customer(SyncId);
            };
            loadForm.ShowDialog();
            this.Close();
        }
    }
}
