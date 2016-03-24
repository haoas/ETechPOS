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

namespace ETech
{
    public partial class frmSearchProduct : Form
    {
        public int productwid = 0;

        private DataTable dt_temp = new DataTable();

        public frmSearchProduct()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            cls_globalfunc.formaddkbkpevent(this);

            this.dgvProduct.AutoGenerateColumns = false;

            this.productwid = 0;
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            keyDownTimer.Stop();
            keyDownTimer.Start();
        }

        private void frmSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.productwid = 0;
                this.Close();
                return;
            }

            if (dgvProduct.RowCount <= 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                if (dgvProduct.RowCount != 0)
                {
                    int current_row = dgvProduct.CurrentCell.RowIndex;
                    int next_row = current_row + 1;
                    dgvProduct.MultiSelect = false;

                    if (next_row < dgvProduct.RowCount)
                    {
                        dgvProduct.Rows[next_row].Selected = true;
                        dgvProduct.CurrentCell = dgvProduct[0, next_row];
                    }
                    else
                        return;
                }
                else
                    return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dgvProduct.RowCount != 0)
                {
                    int current_row = dgvProduct.CurrentCell.RowIndex;
                    int next_row = current_row - 1;
                    dgvProduct.MultiSelect = false;

                    if (next_row > -1)
                    {
                        dgvProduct.Rows[next_row].Selected = true;
                        dgvProduct.CurrentCell = dgvProduct[0, next_row];
                    }
                    else
                        return;
                }
                else
                    return;
            }
            else if (e.KeyCode == Keys.Enter)
                addproduct();
            else
                return;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtProduct_Leave(object sender, EventArgs e)
        {
            this.txtProduct.Focus();
        }

        private void frmSearchProduct_Load(object sender, EventArgs e)
        {
            dgvProduct.FormatColumnsWidth();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgvProduct);
        }

        private void keyDownTimer_Tick(object sender, EventArgs e)
        {
            keyDownTimer.Stop();
            if (this.txtProduct.Text.Trim().Length == 0)
            {
                dt_temp.Clear();
                this.dgvProduct.DataSource = dt_temp;
                dgvProduct.FormatColumnsWidth();
                return;
            }

            string str_input = "";
            string[] words = this.txtProduct.Text.Split(' ');
            foreach (string word in words)
            {
                if (word.Length > 0)
                    str_input += "%" + word;
            }
            str_input += "%";

            if (cls_globalvariables.prodsearchstyle_v == "1")
            {
                str_input = this.txtProduct.Text + "%";
            }

            string allowZeroPrice = "";
            if (cls_globalvariables.allowZeroPrice_v != "1")
            {
                allowZeroPrice = " AND B.`sellingprice` > 0 AND B.`wholesaleprice` > 0";
            }

            string SQL = @"SELECT A.* FROM (
                            SELECT P.`SyncId` as 'productwid', P.`product` AS 'productname', 
                                P.`barcode` AS 'productbarcode',
                                P.`stockno`, B.`sellingprice` AS 'price', 
                                B.`wholesaleprice` AS 'wholesaleprice',
                                P.`description` as 'desc', P.`memo`
                            FROM `product` AS P, `branchprice` AS B 
                            WHERE B.`branchid` = " + cls_globalvariables.BranchCode + @" AND B.`productid` = P.`SyncId` AND 
                                P.`status` = 1 " + allowZeroPrice + @" AND
                                CONCAT(P.`product`, ' ', P.`barcode`, ' ', P.`stockno`,' ',
                                    P.`clientbarcode`,' ',P.`clientbarcode2`) LIKE @str_param
                            LIMIT 50)A ";

            //SQL = sqlsearchstring();

            List<string> parameters = new List<string>();
            List<string> values = new List<string>();
            parameters.Add("@str_param");
            values.Add(str_input);
            Console.WriteLine(SQL);
            dt_temp = mySQLFunc.getdb(SQL, parameters, values);

            this.dgvProduct.DataSource = dt_temp;
            if (dgvProduct.RowCount > 0)
                this.dgvProduct.Rows[0].Selected = true;

            dgvProduct.FormatColumnsWidth();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            addproduct();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.productwid = 0;
            this.Close();
        }

        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            addproduct();
        }

        private void addproduct()
        {
            if (dgvProduct.SelectedRows.Count <= 0)
                return;
            int.TryParse(dgvProduct.SelectedRows[0].Cells["colProductSyncid"].Value.ToString(), out productwid);
            this.Close();
        }
    }
}
