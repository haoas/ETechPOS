﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.cls;
using ETech.fnc;

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
            foreach (DataGridViewColumn col in dgvProduct.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            //fncFullScreen fncfullscreen = new fncFullScreen(this);
            //fncfullscreen.ResizeFormsControls();
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
                fncFilter.set_dgv_display(this.dgvProduct);
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
            string allowZeroPrice_package = "";
            if (cls_globalvariables.allowZeroPrice_v != "1")
            {
                allowZeroPrice = " AND B.`sellingprice` > 0 AND B.`wholesaleprice` > 0";
                allowZeroPrice_package = " AND B.`packagesellingprice` > 0 AND B.`packagewholesaleprice` > 0";
            }

            string SQL = @"
                            SELECT A.*, format(I.inv,0) as inv FROM (
                                SELECT P.`wid` as 'productwid', P.`product` AS 'productname', 
	                                P.`barcode` AS 'productbarcode',
	                                P.`stockno`, B.`sellingprice` AS 'price', 
	                                B.`wholesaleprice` AS 'wholesaleprice',
                                    P.`description` as 'desc', P.`memo`
                                FROM `product` AS P, `branchprice` AS B 
                                WHERE B.`branchid` = " + cls_globalvariables.branchid_v + @" AND B.`productid` = P.`wid` AND 
	                                P.`show` = 1 and P.`status` = 1 " + allowZeroPrice + @" AND
	                                CONCAT(P.`product`, ' ', P.`barcode`, ' ', P.`stockno`,' ',
                                        P.`clientbarcode`,' ',P.`clientbarcode2`) LIKE @str_param
                                LIMIT 100
                                UNION ALL
                                SELECT P.`wid` as 'productwid', P.`product` AS 'productname', 
	                                P.`packbarcode` AS 'productbarcode',
	                                P.`stockno`, B.`packagesellingprice` AS 'price', 
	                                B.`packagewholesaleprice` AS 'wholesaleprice',
                                    P.`description` as 'desc', P.`memo`
                                FROM `product` AS P, `branchprice` AS B 
                                WHERE B.`branchid` = " + cls_globalvariables.branchid_v + @" AND B.`productid` = P.`wid` AND 
	                                P.`show` = 1 and P.`status` = 1 " + allowZeroPrice_package + @" AND B.`ispackage` = 1 AND
	                                CONCAT(P.`product`, ' ', P.`packbarcode`, ' ', P.`stockno`,' ',P.`packbarcode2`) LIKE @str_param
                                LIMIT 100
                            ) A LEFT JOIN productbranchinventory as I on I.productid = A.productwid and I.branchid = " + cls_globalvariables.branchid_v;

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
            else
                fncFilter.set_dgv_display(this.dgvProduct);
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
            int.TryParse(dgvProduct.SelectedRows[0].Cells["colProductWid"].Value.ToString(), out productwid);
            this.Close();
        }
    }
}
