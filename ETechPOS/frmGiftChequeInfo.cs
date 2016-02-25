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
using ETech.fnc;

namespace ETech
{
    public partial class frmGiftChequeInfo : Form
    {
        public List<cls_giftchequeinfo> giftchequeinfos;

        public frmGiftChequeInfo()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            cls_globalfunc.formaddkbkpevent(this);
        }

        public decimal gettotalamount()
        {
            decimal totalamt = 0;
            for (int row_cnt = 0; row_cnt < dgvGiftChequeInfo.RowCount; row_cnt++)
                totalamt += Convert.ToDecimal(dgvGiftChequeInfo.Rows[row_cnt].Cells["colAmount"].Value);
            
            return totalamt;
        }

        private void DeleteGCInfo()
        {
            if (dgvGiftChequeInfo.RowCount > 0)
            {
                int cur_row = dgvGiftChequeInfo.CurrentCell.RowIndex;
                //mySQLFunc.setdb(@"UPDATE `giftcheque` SET `status` = 1 WHERE wid = "
                //                    + dgvGiftChequeInfo.Rows[cur_row].Cells["colwid"].Value.ToString());
                dgvGiftChequeInfo.Rows.RemoveAt(cur_row);
                if (dgvGiftChequeInfo.RowCount > 0)
                    dgvGiftChequeInfo.CurrentRow.Selected = true;
                txtScan.Focus();
            }
            else
            {
                txtScan.Focus();
                return;
            }

            decimal totalamt = gettotalamount();
            lblTotalAmount_d.Text = totalamt.ToString("N2");
        }
        private void Cancel_All_GC_Scanned()
        {
            /*
            foreach (DataGridViewRow dgvr in dgvGiftChequeInfo.Rows)
            {
                mySQLFunc.setdb(@"UPDATE `giftcheque` SET `status` = 1 WHERE wid = "
                                    + dgvr.Cells["colwid"].Value.ToString());
            }

            foreach (cls_giftchequeinfo gcinfo in this.giftchequeinfos)
            {
                mySQLFunc.setdb(@"UPDATE `giftcheque` SET `status` = 3 WHERE wid = "
                                    + gcinfo.getwid());
            }
            */
        }

        private void frmGiftChequeInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                done_process();
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
                Cancel_All_GC_Scanned();
                this.Close();
                return;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dgvGiftChequeInfo.RowCount != 0)
                {
                    int current_row = dgvGiftChequeInfo.CurrentCell.RowIndex;
                    int next_row = current_row + 1;
                    dgvGiftChequeInfo.MultiSelect = false;

                    if (next_row < dgvGiftChequeInfo.RowCount)
                    {
                        dgvGiftChequeInfo.CurrentCell = dgvGiftChequeInfo[0, next_row];
                        dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.CurrentCell.RowIndex].Selected = true;
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
                if (dgvGiftChequeInfo.RowCount != 0)
                {
                    int current_row = dgvGiftChequeInfo.CurrentCell.RowIndex;
                    int next_row = current_row - 1;
                    dgvGiftChequeInfo.MultiSelect = false;

                    if (next_row > -1)
                    {
                        dgvGiftChequeInfo.CurrentCell = dgvGiftChequeInfo[0, next_row];
                        dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.CurrentCell.RowIndex].Selected = true;
                    }
                    else
                        return;

                    e.SuppressKeyPress = true;
                }
                else
                    return;
            }
        }

        public void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;

                string chequeno = txtScan.Text.Trim();
                if (chequeno == "")
                {
                    txtScan.Focus();
                    return;
                }

                //check if giftcheque has been used
                foreach (DataGridViewRow dgvr in dgvGiftChequeInfo.Rows)
                {
                    if( chequeno.Equals(dgvr.Cells["colGiftChequeNo"].Value.ToString()) )
                    {
                        fncFilter.alert(cls_globalvariables.warning_giftcheque_beenused);
                        txtScan.Focus();
                        txtScan.SelectAll();
                        return;
                    }
                }
                
                DataTable dt = mySQLFunc.getdb(@"SELECT * FROM `giftcheque`
                                                 WHERE (`status` = 2 OR `status` = 3) AND (`chequebarcode` = @chequebc OR `chequeno` = @chequebc)", 
                                                 new List<string>(new string[] { "chequebc" }),
                                                 new List<string>(new string[] { chequeno }));
                if (dt.Rows.Count > 0)
                {
                    fncFilter.alert(cls_globalvariables.warning_giftcheque_beenused);
                    txtScan.Focus();
                    txtScan.SelectAll();
                    return;
                }

                dt = mySQLFunc.getdb(@"SELECT * FROM `giftcheque` 
                                       WHERE `status` = 1 AND (`chequebarcode` = @chequebc OR `chequeno` = @chequebc)",
                                       new List<string>(new string[] { "chequebc" }),
                                       new List<string>(new string[] { chequeno }));

                if (dt.Rows.Count <= 0)
                {
                    fncFilter.alert(cls_globalvariables.warning_giftcheque_invalid);
                    txtScan.Focus();
                    txtScan.SelectAll();
                    return;
                }

                addgctodgv(dt.Rows[0]["wid"].ToString(), dt.Rows[0]["chequeno"].ToString(), Convert.ToDecimal(dt.Rows[0]["amount"]));

                //mySQLFunc.setdb(@"UPDATE `giftcheque` SET `status` = 3 WHERE wid = "+dt.Rows[0]["wid"].ToString());

                decimal totalamt = gettotalamount();
                lblTotalAmount_d.Text = totalamt.ToString("N2");

            }

            
        }

        public void addgctodgv(string wid, string gcno, decimal amt)
        {
            dgvGiftChequeInfo.Rows.Add();
            dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.RowCount - 1].Cells["colwid"].Value = wid;
            dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.RowCount - 1].Cells["colGiftChequeNo"].Value = gcno;
            dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.RowCount - 1].Cells["colAmount"].Value = amt.ToString("N2");
            dgvGiftChequeInfo.CurrentCell = dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.RowCount - 1].Cells["colGiftChequeNo"];
            dgvGiftChequeInfo.Rows[dgvGiftChequeInfo.RowCount - 1].Selected = true;
            txtScan.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteGCInfo();
        }
        public void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            Cancel_All_GC_Scanned();
            this.Close();
        }

        private void done_process()
        {
            foreach (cls_giftchequeinfo gcinfo in this.giftchequeinfos)
            {
                mySQLFunc.setdb(@"UPDATE `giftcheque` SET `status` = 1 WHERE wid = "
                                    + gcinfo.getwid());
            }
            this.giftchequeinfos.Clear();
            for (int row_cnt = 0; row_cnt < dgvGiftChequeInfo.RowCount; row_cnt++)
            {
                string wid = dgvGiftChequeInfo.Rows[row_cnt].Cells["colwid"].Value.ToString();
                string gcno = dgvGiftChequeInfo.Rows[row_cnt].Cells["colGiftChequeNo"].Value.ToString();
                decimal amount = Convert.ToDecimal(dgvGiftChequeInfo.Rows[row_cnt].Cells["colAmount"].Value);


                DataTable dt = mySQLFunc.getdb(@"SELECT * FROM `giftcheque` 
                                       WHERE `status` = 1 AND `wid` = " + wid);
                if (dt.Rows.Count <= 0)
                {
                    fncFilter.alert(cls_globalvariables.warning_giftcheque_beenused);
                    txtScan.Focus();
                    txtScan.SelectAll();
                    return;
                }
                mySQLFunc.setdb(@"UPDATE `giftcheque` SET `status` = 3 WHERE wid = "
                                    + wid);
                
                cls_giftchequeinfo gc = new cls_giftchequeinfo();
                gc.setgiftchequeinfo(Convert.ToInt32(wid), gcno, amount);
                this.giftchequeinfos.Add(gc);
            }

            this.Close();
        }

        private void frmGiftChequeInfo_Load(object sender, EventArgs e)
        {
            foreach (cls_giftchequeinfo gc in this.giftchequeinfos)
            {
                string wid = gc.getwid().ToString();
                string gcno = gc.getgiftchequeno();
                decimal amt = gc.getamount();

                this.addgctodgv(wid, gcno, amt);

                decimal totalamt = this.gettotalamount();
                lblTotalAmount_d.Text = totalamt.ToString("N2");
            }

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgvGiftChequeInfo);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
