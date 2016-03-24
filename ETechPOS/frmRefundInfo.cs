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
    public partial class frmRefundInfo : Form
    {
        public string productname;
        public long productid;
        public decimal negative_qty;
        public string remarks;
        public string salesdetailmemo;
        public bool issuccess;
        public bool forcereturn_permission;

        public frmRefundInfo()
        {
            InitializeComponent();
            fncFilter.set_theme_color(this);

            productname = "";
            productid = 0;
            negative_qty = 0;
            remarks = "";
            salesdetailmemo = "";
            issuccess = false;
        }

        private void frmRefundInfo_Load(object sender, EventArgs e)
        {
            lblProductName.Text = productname;
            cboxRefundReason.SelectedIndex = 0;
            txtORno.Text = "";
            txtRemark.Text = "";
            txtORno.AsInteger();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.issuccess = false;
            string ornumber = txtORno.Text.Trim();

            if (ornumber == "")
            {
                if (forcereturn_permission)
                    successReturn();
                else
                {
                    frmPermissionCode frmpermcode = new frmPermissionCode();
                    frmpermcode.permission_needed = fncFilter.get_permission_forcereturn();
                    frmpermcode.ShowDialog();
                    if (frmpermcode.permcode)
                        successReturn();
                }
            }
            else
            {
                decimal prodcount = get_productCountFromOR(this.productid, ornumber);
                decimal ABSnegative_qty = Math.Abs(negative_qty);
                if (prodcount >= ABSnegative_qty)
                    successReturn();
                else
                {
                    if (prodcount == 0)
                        fncFilter.alert("OR has no such products");
                    else
                        fncFilter.alert("Products intended for refund are more than sales in Previous OR");
                }
            }
        }
        private void successReturn()
        {
            string refundreason = cboxRefundReason.SelectedItem.ToString();
            string ornumber = txtORno.Text.Trim();
            string remarks = txtRemark.Text.Trim();
            this.salesdetailmemo = "OR#: " + ornumber + @" | Return: " + refundreason + @" | Remark: " + remarks;
            this.issuccess = true;
            this.Close();
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            this.issuccess = false;
            this.Close();
        }

        public decimal get_productCountFromOR(long productid, string ornumber)
        {
            string branchid = cls_globalvariables.BranchCode;

            string sql = @"SELECT COALESCE(SUM(D.`quantity`),0) as `remainqty` 
            FROM Saleshead as H, Salesdetail as D
            WHERE H.`status` = 1
                AND H.`branchid`=" + branchid + @"
                AND H.`SyncId`=D.`headid`
                AND D.`productid`=" + productid + @" AND 
            (H.`ornumber`='" + ornumber + @"' OR D.`description` LIKE 'OR#: " + ornumber + @"%' );";

            DataTable DT = mySQLFunc.getdb(sql);
            if (DT == null)
                return 0;
            else if (DT.Rows.Count <= 0)
                return 0;
            else
                return Convert.ToDecimal(DT.Rows[0]["remainqty"]);
        }

        private void cboxRefundReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtORno.Focus();
                this.txtORno.SelectAll();
                e.Handled = true;
            }
        }

        private void txtORno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtRemark.Focus();
                this.txtRemark.SelectAll();
                e.Handled = true;
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        private void frmRefundInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F1))
            {
                btnOK_Click(null, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void cboxRefundReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
                e.Handled = true;
        }
    }
}
