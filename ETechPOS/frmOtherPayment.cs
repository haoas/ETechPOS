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
    public partial class frmOtherPayment : Form
    {
        public List<cls_otherpaymentinfo> gcinfos;
        public bool changeupdated;

        public frmOtherPayment()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            cls_globalfunc.formaddkbkpevent(this);

            this.dgvGCInfo.AutoGenerateColumns = false;
            changeupdated = false;
        }

        private void OtherPayment_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now;
            foreach (cls_otherpaymentinfo gc in this.gcinfos)
            {
                string referenceno = gc.get_referenceno();
                DateTime expdate = gc.getexpdate();
                decimal amt = gc.getamount();
                string memo = gc.get_memo();

                this.addgctodgv(referenceno, expdate, amt, memo);
                lblTotalAmount_d.Text = this.gettotalamount().ToString("N2");
            }

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgvGCInfo);
        }

        public decimal gettotalamount()
        {
            decimal totalamt = 0;
            for (int row_cnt = 0; row_cnt < dgvGCInfo.RowCount; row_cnt++)
            {
                totalamt += Convert.ToDecimal(dgvGCInfo.Rows[row_cnt].Cells["colAmount"].Value);
            }
            return totalamt;
        }

        private void addgctodgv(string referenceno, DateTime expdate, decimal amt, string memo)
        {
            dgvGCInfo.Rows.Add();
            dgvGCInfo.Rows[dgvGCInfo.RowCount - 1].Cells["colRefNo"].Value = referenceno;
            dgvGCInfo.Rows[dgvGCInfo.RowCount - 1].Cells["colAmount"].Value = amt.ToString("N2");
            dgvGCInfo.Rows[dgvGCInfo.RowCount - 1].Cells["colexpdate"].Value = expdate.Date.ToString("MM-dd-yyyy");
            dgvGCInfo.Rows[dgvGCInfo.RowCount - 1].Cells["colMemo"].Value = memo;
            dgvGCInfo.Rows[dgvGCInfo.RowCount - 1].Selected = true;

            txtRefNo_d.Clear();
            txtAmount_d.Clear();
            txtMemo_d.Clear();
            dateTimePicker1.Value = DateTime.Now;

            txtRefNo_d.Focus();
            txtRefNo_d.SelectAll();

            fncFilter.set_dgv_display(dgvGCInfo);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddGCInfo();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteGCInfo();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            changeupdated = false;
            this.Close();
        }

        private void AddGCInfo()
        {
            decimal amt = fncFilter.getDecimalValue(txtAmount_d.Text);
            if (amt == 0)
            {
                fncFilter.alert(cls_globalvariables.warning_amount_invalid);
                txtAmount_d.Focus();
                txtAmount_d.SelectAll();
                return;
            }

            if (dateTimePicker1.Value < DateTime.Now.Date)
            {
                fncFilter.alert(cls_globalvariables.warning_yearofexp_invalid);
                dateTimePicker1.Focus();
                return;
            }

            string referenceno = txtRefNo_d.Text.Trim();
            if (referenceno.Length == 0)
            {
                fncFilter.alert("GiftCheque No Cannot be empty");
                txtRefNo_d.Focus();
                txtRefNo_d.SelectAll();
                return;
            }

            DateTime expdate = dateTimePicker1.Value;
            string memo = txtMemo_d.Text.Trim();

            addgctodgv(referenceno, expdate, amt, memo);

            txtRefNo_d.Focus();
            txtRefNo_d.SelectAll();
            lblTotalAmount_d.Text = this.gettotalamount().ToString("N2");

        }
        private void DeleteGCInfo()
        {
            if (dgvGCInfo.RowCount > 0)
            {
                dgvGCInfo.Rows.RemoveAt(dgvGCInfo.CurrentCell.RowIndex);
                if (dgvGCInfo.RowCount > 0)
                    dgvGCInfo.CurrentRow.Selected = true;
                txtAmount_d.Focus();
            }
            else
            {
                txtAmount_d.Focus();
                return;
            }
            lblTotalAmount_d.Text = this.gettotalamount().ToString("N2");
        }
        private void done_process()
        {
            this.gcinfos.Clear();
            for (int row_cnt = 0; row_cnt < dgvGCInfo.RowCount; row_cnt++)
            {
                string refno = dgvGCInfo.Rows[row_cnt].Cells["colRefNo"].Value.ToString();
                decimal amt = Convert.ToDecimal(dgvGCInfo.Rows[row_cnt].Cells["colAmount"].Value);
                DateTime expdate = Convert.ToDateTime(dgvGCInfo.Rows[row_cnt].Cells["colexpdate"].Value);
                string memo = dgvGCInfo.Rows[row_cnt].Cells["colMemo"].Value.ToString();

                cls_otherpaymentinfo gc = new cls_otherpaymentinfo();
                gc.setotherpaymentinfo(refno, expdate, amt, memo, 13);
                this.gcinfos.Add(gc);
            }

            changeupdated = true;
            this.Close();
        }

        private void frmOtherPayment_KeyDown(object sender, KeyEventArgs e)
        {
            Control nextControl;
            if (e.KeyCode == Keys.Enter)
            {
                nextControl = GetNextControl(ActiveControl, !e.Shift);
                if (nextControl == null)
                {
                    nextControl = GetNextControl(null, true);
                }
                nextControl.Focus();
            }
            else if (e.KeyCode == Keys.F1)
            {
                done_process();
                return;
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnAdd.Focus();
                AddGCInfo();
                return;
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnDelete.Focus();
                DeleteGCInfo();
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                changeupdated = false;
                this.Close();
                return;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dgvGCInfo.RowCount != 0)
                {
                    int current_row = dgvGCInfo.CurrentCell.RowIndex;
                    int next_row = current_row + 1;
                    dgvGCInfo.MultiSelect = false;

                    if (next_row < dgvGCInfo.RowCount)
                    {
                        dgvGCInfo.CurrentCell = dgvGCInfo[0, next_row];
                        dgvGCInfo.Rows[dgvGCInfo.CurrentCell.RowIndex].Selected = true;
                    }
                    else
                        return;

                    e.SuppressKeyPress = true;
                }
                else
                    return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dgvGCInfo.RowCount != 0)
                {
                    int current_row = dgvGCInfo.CurrentCell.RowIndex;
                    int next_row = current_row - 1;
                    dgvGCInfo.MultiSelect = false;

                    if (next_row > -1)
                    {
                        dgvGCInfo.CurrentCell = dgvGCInfo[0, next_row];
                        dgvGCInfo.Rows[dgvGCInfo.CurrentCell.RowIndex].Selected = true;
                    }
                    else
                        return;

                    e.SuppressKeyPress = true;
                }
                else
                    return;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtAmount_d_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
    }
}
