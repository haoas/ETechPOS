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
    public partial class frmCustomPayments : Form
    {
        public List<cls_CustomPayments> list_customPayment = new List<cls_CustomPayments>();
        public List<cls_CustomPaymentsInfo> list_Custompayments;
        public bool changeupdated;

        public frmCustomPayments()
        {
            InitializeComponent();

            this.changeupdated = false;
        }

        private void frmCustomPayments_Load(object sender, EventArgs e)
        {
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgvTendCustPayment);

            set_customPayments_from_paymentmethod();
            Refresh_listbox_CustomPayments();

            foreach (cls_CustomPaymentsInfo custompaymentsinfo in list_Custompayments)
            {
                int paymentwid = custompaymentsinfo.get_paymentwid();
                string paymentname = custompaymentsinfo.get_paymentname();
                decimal amount = custompaymentsinfo.get_amount();
                string field1 = custompaymentsinfo.get_field1info();
                string field2 = custompaymentsinfo.get_field2info();
                string field3 = custompaymentsinfo.get_field3info();
                string field4 = custompaymentsinfo.get_field4info();
                string field5 = custompaymentsinfo.get_field5info();
                string field6 = custompaymentsinfo.get_field6info();

                this.AddPaymentInfo(paymentwid, paymentname, amount, field1, field2, field3, field4, field5, field6);

                decimal totalamt = this.gettotalamount();
                lblTotalAmount.Text = totalamt.ToString("N2");
            }

            txtAmount.Focus();
            txtAmount.SelectAll();
        }

        public decimal gettotalamount()
        {
            decimal totalamt = 0;
            for (int row_cnt = 0; row_cnt < dgvTendCustPayment.RowCount; row_cnt++)
            {
                totalamt += Convert.ToDecimal(dgvTendCustPayment.Rows[row_cnt].Cells["colAmount"].Value);
            }
            return totalamt;
        }

        public void set_customPayments_from_paymentmethod()
        {
            cls_CustomPayments custompayment = new cls_CustomPayments();

            string sql =
            @"SELECT
	            `SyncId` AS `Payment Id`,
                `name` AS `Payment Name`,
                `field1` AS 'Field 1',
                `field2` AS 'Field 2',
                `field3` AS 'Field 3',
                `field4` AS 'Field 4',
                `field5` AS 'Field 5',
                `field6` AS 'Field 6'
            FROM
	            `paymentmethod`
            WHERE
	            `SyncId` >= 100
            ORDER BY
	            `SyncId`";

            DataTable DT = mySQLFunc.getdb(sql);
            if (DT == null)
                return;
            if (DT.Rows.Count <= 0)
                return;
            else
            {
                foreach (DataRow DR in DT.Rows)
                {
                    custompayment = new cls_CustomPayments();
                    custompayment.set_custompayments_by_DataRow(DR);
                    list_customPayment.Add(custompayment);
                }
            }
        }

        public void Refresh_listbox_CustomPayments()
        {
            foreach (cls_CustomPayments custompayment in list_customPayment)
            {
                dgvCustomPayments.Rows.Add();
                dgvCustomPayments.Rows[dgvCustomPayments.RowCount - 1].Cells["colPaymentName"].Value = custompayment.get_name();
            }
            if (dgvCustomPayments.Rows.Count <= 0)
                return;
            dgvCustomPayments.Rows[0].Selected = true;
        }

        public void AddPaymentInfo(int paymentwid_d, string paymentname_d, decimal amt_d,
                                   string field1_d, string field2_d, string field3_d,
                                   string field4_d, string field5_d, string field6_d)
        {
            dgvTendCustPayment.Rows.Add();
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colPaymentwid"].Value = paymentwid_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colName"].Value = paymentname_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colAmount"].Value = amt_d.ToString("N2");
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colField1"].Value = field1_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colField2"].Value = field2_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colField3"].Value = field3_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colField4"].Value = field4_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colField5"].Value = field5_d;
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colField6"].Value = field6_d;
            dgvTendCustPayment.CurrentCell = dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Cells["colAmount"];
            dgvTendCustPayment.Rows[dgvTendCustPayment.RowCount - 1].Selected = true;

            txtAmount.Clear();
            txtField1.Clear();
            txtField2.Clear();
            txtField3.Clear();
            txtField4.Clear();
            txtField5.Clear();
            txtField6.Clear();

            txtAmount.Focus();
            txtAmount.SelectAll();
        }
        public void AddtoDGV()
        {
            decimal amt = fncFilter.getDecimalValue(txtAmount.Text);
            if (amt == 0)
            {
                fncFilter.alert(cls_globalvariables.warning_amount_invalid);
                txtAmount.Focus();
                txtAmount.SelectAll();
                return;
            }

            int paymentwid = Convert.ToInt32(lblPaymentId.Text);
            string paymentname = lblPaymentMethodName.Text;
            string field1 = txtField1.Text;
            string field2 = txtField2.Text;
            string field3 = txtField3.Text;
            string field4 = txtField4.Text;
            string field5 = txtField5.Text;
            string field6 = txtField6.Text;

            this.AddPaymentInfo(paymentwid, paymentname, amt, field1, field2, field3, field4, field5, field6);

            decimal totalamt = this.gettotalamount();
            lblTotalAmount.Text = totalamt.ToString("N2");
        }

        public void DeleteFromDGV()
        {
            if (dgvTendCustPayment.RowCount > 0)
            {
                dgvTendCustPayment.Rows.RemoveAt(dgvTendCustPayment.CurrentCell.RowIndex);
                if (dgvTendCustPayment.RowCount > 0)
                    dgvTendCustPayment.CurrentRow.Selected = true;
                txtAmount.Focus();
            }
            else
            {
                txtAmount.Focus();
                return;
            }

            decimal totalamt = this.gettotalamount();
            lblTotalAmount.Text = totalamt.ToString("N2");
        }

        private void done_process()
        {
            this.list_Custompayments.Clear();
            for (int row_cnt = 0; row_cnt < dgvTendCustPayment.RowCount; row_cnt++)
            {
                int paymentwid = Convert.ToInt32(dgvTendCustPayment.Rows[row_cnt].Cells["colPaymentwid"].Value);
                string paymentname = dgvTendCustPayment.Rows[row_cnt].Cells["colName"].Value.ToString();
                decimal Amount = Convert.ToDecimal(dgvTendCustPayment.Rows[row_cnt].Cells["colAmount"].Value);
                string field1 = dgvTendCustPayment.Rows[row_cnt].Cells["colField1"].Value.ToString();
                string field2 = dgvTendCustPayment.Rows[row_cnt].Cells["colField2"].Value.ToString();
                string field3 = dgvTendCustPayment.Rows[row_cnt].Cells["colField3"].Value.ToString();
                string field4 = dgvTendCustPayment.Rows[row_cnt].Cells["colField4"].Value.ToString();
                string field5 = dgvTendCustPayment.Rows[row_cnt].Cells["colField5"].Value.ToString();
                string field6 = dgvTendCustPayment.Rows[row_cnt].Cells["colField6"].Value.ToString();

                cls_CustomPaymentsInfo custompaymentinfo = new cls_CustomPaymentsInfo();
                custompaymentinfo.set_CustomPaymentsInfo(paymentwid, paymentname, Amount, field1, field2, field3, field4, field5, field6);

                list_Custompayments.Add(custompaymentinfo);
            }
            this.Close();
        }

        private void SelectCustomPayment()
        {
            if (dgvCustomPayments.SelectedRows.Count <= 0)
                return;

            cls_CustomPayments custompayments = list_customPayment[dgvCustomPayments.SelectedRows[0].Index];
            lblPaymentId.Text = custompayments.get_paymentwid();
            lblPaymentMethodName.Text = custompayments.get_name();

            ClearCustomPaymentInformation();

            if (custompayments.get_field1() != "")
            {
                lblField1.Text = custompayments.get_field1();
                lblField1.Visible = true;
                txtField1.Visible = true;
            }
            else
            {
                lblField1.Visible = false;
                txtField1.Visible = false;
            }

            if (custompayments.get_field2() != "")
            {
                lblField2.Text = custompayments.get_field2();
                lblField2.Visible = true;
                txtField2.Visible = true;
            }
            else
            {
                lblField2.Visible = false;
                txtField2.Visible = false;
            }

            if (custompayments.get_field3() != "")
            {
                lblField3.Text = custompayments.get_field3();
                lblField3.Visible = true;
                txtField3.Visible = true;
            }
            else
            {
                lblField3.Visible = false;
                txtField3.Visible = false;
            }

            if (custompayments.get_field4() != "")
            {
                lblField4.Text = custompayments.get_field4();
                lblField4.Visible = true;
                txtField4.Visible = true;
            }
            else
            {
                lblField4.Visible = false;
                txtField4.Visible = false;
            }

            if (custompayments.get_field5() != "")
            {
                lblField5.Text = custompayments.get_field5();
                lblField5.Visible = true;
                txtField5.Visible = true;
            }
            else
            {
                lblField5.Visible = false;
                txtField5.Visible = false;
            }

            if (custompayments.get_field6() != "")
            {
                lblField6.Text = custompayments.get_field6();
                lblField6.Visible = true;
                txtField6.Visible = true;
            }
            else
            {
                lblField6.Visible = false;
                txtField6.Visible = false;
            }
        }
        private void ClearCustomPaymentInformation()
        {
            txtAmount.Text = "";
            txtField1.Text = "";
            txtField2.Text = "";
            txtField3.Text = "";
            txtField4.Text = "";
            txtField5.Text = "";
            txtField6.Text = "";
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtField1.SelectAll();
                txtField1.Focus();
                e.Handled = true;
            }
        }

        private void txt_field1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtField2.Visible)
                {
                    txtField2.SelectAll();
                    txtField2.Focus();
                    e.Handled = true;
                }
                else
                {
                    txt_field2_KeyDown(sender, e);
                }
            }
        }

        private void txt_field2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtField3.Visible)
                {
                    txtField3.SelectAll();
                    txtField3.Focus();
                    e.Handled = true;
                }
                else
                {
                    txt_field3_KeyDown(sender, e);
                }
            }
        }

        private void txt_field3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtField4.Visible)
                {
                    txtField4.SelectAll();
                    txtField4.Focus();
                    e.Handled = true;
                }
                else
                {
                    txt_field4_KeyDown(sender, e);
                }
            }
        }

        private void txt_field4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtField5.Visible)
                {
                    txtField5.SelectAll();
                    txtField5.Focus();
                    e.Handled = true;
                }
                else
                {
                    txt_field5_KeyDown(sender, e);
                }
            }
        }

        private void txt_field5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtField6.Visible)
                {
                    txtField6.SelectAll();
                    txtField6.Focus();
                    e.Handled = true;
                }
                else
                {
                    txt_field6_KeyDown(sender, e);
                }
            }
        }

        private void txt_field6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddtoDGV();
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddtoDGV();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteFromDGV();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            done_process();
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void frmCustomPaymentsInfo_KeyDown(object sender, KeyEventArgs e)
        {
            this.processShortCutKey(e);
        }

        private void dgvCustomPayments_SelectionChanged(object sender, EventArgs e)
        {
            SelectCustomPayment();
        }

        public bool processShortCutKey(KeyEventArgs e)
        {
            bool isdetected = false;
            switch (e.KeyCode)
            {
                case Keys.F1:
                    done_process();
                    break;
                case Keys.F4:
                    AddtoDGV();
                    break;
                case Keys.F7:
                    DeleteFromDGV();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Up:
                    dgvTendCustPayment.Select();
                    break;
                case Keys.Down:
                    dgvTendCustPayment.Select();
                    break;
            }
            isdetected = true;
            return isdetected;
        }
    }
}
