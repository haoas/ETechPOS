using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ETech.cls;
using ETech.fnc;


namespace ETech
{
    public partial class frmCardInfo : Form
    {
        public List<cls_cardinfo> cardinfos;
        public bool changeupdated;
        private string _cardSwipedString;
        private Timer _timer;
        private string _amountBuf;
        private bool _timerBuf1;
        private bool _timerBuf2;

        public frmCardInfo()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            _cardSwipedString = "";
            this.dgvCardInfo.AutoGenerateColumns = false;
            changeupdated = false;
            cls_globalfunc.formaddkbkpevent(this);
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Enabled = true;
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timerBuf1 = _timerBuf2;
            _timerBuf2 = true;
        }

        private void frmCardInfo_Load(object sender, EventArgs e)
        {
            foreach (cls_cardinfo card in this.cardinfos)
            {
                string name = card.getname();
                string cardno = card.getcardno();
                DateTime expdate = card.getexpdate();
                decimal amt = card.getamount();
                string approvalcode = card.getapprovalcode();

                this.addcardtodgv(cardno, amt, name, expdate.Month.ToString("d2"), expdate.Year.ToString(), approvalcode);

                decimal totalamt = this.gettotalamount();
                lblTotalAmount_d.Text = totalamt.ToString("N2");
            }

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_controls(dgvCardInfo);
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            AddCardInfo();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCardInfo();
        }
        public void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            changeupdated = false;
            this.Close();
        }

        private void frmCardInfo_KeyDown(object sender, KeyEventArgs e)
        {
            Control nextControl;
            //Checks if the Enter Key was Pressed
            if (e.KeyCode == Keys.Enter)
            {
                //If so, it gets the next control and applies the focus to it
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
                AddCardInfo();
                return;
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnDelete.Focus();
                DeleteCardInfo();
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
                if (dgvCardInfo.RowCount != 0)
                {
                    int current_row = dgvCardInfo.CurrentCell.RowIndex;
                    int next_row = current_row + 1;
                    dgvCardInfo.MultiSelect = false;

                    if (next_row < dgvCardInfo.RowCount)
                    {
                        dgvCardInfo.CurrentCell = dgvCardInfo[0, next_row];
                        dgvCardInfo.Rows[dgvCardInfo.CurrentCell.RowIndex].Selected = true;
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
                if (dgvCardInfo.RowCount != 0)
                {
                    int current_row = dgvCardInfo.CurrentCell.RowIndex;
                    int next_row = current_row - 1;
                    dgvCardInfo.MultiSelect = false;

                    if (next_row > -1)
                    {
                        dgvCardInfo.CurrentCell = dgvCardInfo[0, next_row];
                        dgvCardInfo.Rows[dgvCardInfo.CurrentCell.RowIndex].Selected = true;
                    }
                    else
                        return;

                    e.SuppressKeyPress = true;
                }
                else
                    return;
            }
        }

        //private void txtAmount_d_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //if (e.KeyChar == 37)
        //    //{
        //    //    txtCardHolder_d.Text = "";
        //    //    txtCardNo_d.Text = "";
        //    //    txtValidThru_m.Text = "";
        //    //    txtValidThru_y.Text = "";
        //    //}
        //    //else if (e.KeyChar == 13)
        //    //{
        //    //    bool CaretPresent = false;
        //    //    bool EqualPresent = false;

        //    //    CaretPresent = txtAmount_d.Text.Contains("^");
        //    //    EqualPresent = txtAmount_d.Text.Contains("=");

        //    //    if (CaretPresent)
        //    //    {
        //    //        string[] CardData = txtAmount_d.Text.Split('^');
        //    //        //style: B1234123412341234^CardUser/John^030510100000019301000000877000000?

        //    //        txtCardHolder_d.Text = FormatName(CardData[1]);
        //    //        txtCardNo_d.Text = FormatCardNumber(CardData[0]);
        //    //        txtValidThru_m.Text = CardData[2].Substring(2, 2);
        //    //        txtValidThru_y.Text = DateTime.Now.Year.ToString().Substring(0, 2) + CardData[2].Substring(0, 2);
        //    //        txtAmount_d.Text = "";
        //    //        txtAmount_d.Focus();
        //    //    }
        //    //    else if (EqualPresent)
        //    //    {
        //    //        string[] CardData = txtAmount_d.Text.Split('=');
        //    //        //style: 1234123412341234=0305101193010877?

        //    //        txtCardNo_d.Text = FormatCardNumber(CardData[0]);
        //    //        txtValidThru_m.Text = CardData[1].Substring(2, 2);
        //    //        txtValidThru_y.Text = DateTime.Now.Year.ToString().Substring(0, 2) + CardData[1].Substring(0, 2);
        //    //        txtAmount_d.Text = "";
        //    //        txtAmount_d.Focus();
        //    //    }
        //    //    e.Handled = true;
        //    //}
        //    //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
        //    //{
        //    //    e.Handled = true;
        //    //}
        //    //if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
        //    //{
        //    //    e.Handled = true;
        //    //}
        //}
        //private void txtCardHolder_d_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //CreditcardKeyPress(0, e);
        //}
        //private void txtCardNo_d_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //CreditcardKeyPress(1, e);
        //}
        //private void txtValidThru_m_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //CreditcardKeyPress(2, e);
        //}
        //private void txtValidThru_y_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    //CreditcardKeyPress(3, e);
        //}

        private void done_process()
        {
            this.cardinfos.Clear();
            for (int row_cnt = 0; row_cnt < dgvCardInfo.RowCount; row_cnt++)
            {
                string name = dgvCardInfo.Rows[row_cnt].Cells["colCardHolder"].Value.ToString();
                string cardno = dgvCardInfo.Rows[row_cnt].Cells["colCardNo"].Value.ToString();
                string cardtype = dgvCardInfo.Rows[row_cnt].Cells["colCardtype"].Value.ToString();
                string expdate_d = dgvCardInfo.Rows[row_cnt].Cells["colValidThru_m"].Value.ToString().PadLeft(2, '0') + "-" +
                                   dgvCardInfo.Rows[row_cnt].Cells["colValidThru_y"].Value.ToString();
                DateTime expdate = new DateTime();
                expdate = DateTime.ParseExact(expdate_d, "MM-yyyy", null);
                decimal amount = Convert.ToDecimal(dgvCardInfo.Rows[row_cnt].Cells["colAmount"].Value);
                string approvalcode = dgvCardInfo.Rows[row_cnt].Cells["colApprovalCode"].Value.ToString();

                cls_cardinfo card = new cls_cardinfo();
                card.setcardinfo(name, cardno, cardtype, expdate, amount, approvalcode);
                this.cardinfos.Add(card);
            }

            changeupdated = true;
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public decimal gettotalamount()
        {
            decimal totalamt = 0;
            for (int row_cnt = 0; row_cnt < dgvCardInfo.RowCount; row_cnt++)
            {
                totalamt += Convert.ToDecimal(dgvCardInfo.Rows[row_cnt].Cells["colAmount"].Value);
            }
            return totalamt;
        }
        private void AddCardInfo()
        {
            decimal amt = fncFilter.getDecimalValue(txtAmount_d.Text);
            if (amt == 0)
            {
                fncFilter.alert(cls_globalvariables.warning_invalid_amount);
                txtAmount_d.Focus();
                txtAmount_d.SelectAll();
                return;
            }

            txtCardHolder_d.Text.Trim();
            if (txtCardHolder_d.Text == "")
            {
                fncFilter.alert(cls_globalvariables.warning_cardholder_needed);
                txtCardHolder_d.Focus();
                txtCardHolder_d.SelectAll();
                return;
            }

            txtCardNo_d.Text.Trim();
            if ((txtCardNo_d.Text.Length < 11) || (txtCardNo_d.Text.Length > 19))
            {
                fncFilter.alert(cls_globalvariables.warning_cardno_invalid);
                txtCardNo_d.Focus();
                txtCardNo_d.SelectAll();
                return;
            }

            int month = fncFilter.getIntegerValue(txtValidThru_m.Text.Trim());
            if (txtValidThru_m.Text == "" || month > 12 || month < 1)
            {
                fncFilter.alert(cls_globalvariables.warning_monthofexp_invalid);
                txtValidThru_m.Focus();
                txtValidThru_m.SelectAll();
                return;
            }

            int year = fncFilter.getIntegerValue(txtValidThru_y.Text.Trim());
            if (txtValidThru_y.Text == "" || year.ToString().Length != 4)
            {
                fncFilter.alert(cls_globalvariables.warning_yearofexp_invalid);
                txtValidThru_y.Focus();
                txtValidThru_y.SelectAll();
                return;
            }

            txtApprovalCode_d.Text.Trim();
            if (txtApprovalCode_d.Text.Trim() == "")
            {
                fncFilter.alert(cls_globalvariables.warning_approvalcode_needed);
                txtApprovalCode_d.Focus();
                txtApprovalCode_d.SelectAll();
                return;
            }

            string date = month + "/" + DateTime.DaysInMonth(year, month) + "/" + year;
            DateTime validdate = Convert.ToDateTime(date);
            if (DateTime.Compare(validdate.Date, DateTime.Now.Date) < 0)
            {
                fncFilter.alert(cls_globalvariables.warning_card_expired);
                txtValidThru_y.Focus();
                txtValidThru_y.SelectAll();
                return;
            }

            addcardtodgv(txtCardNo_d.Text, amt, txtCardHolder_d.Text, txtValidThru_m.Text, txtValidThru_y.Text, txtApprovalCode_d.Text);
            txtAmount_d.Focus();

            decimal totalamt = this.gettotalamount();
            lblTotalAmount_d.Text = totalamt.ToString("N2");

        }
        private void addcardtodgv(string cardno, decimal amt, string cardholder, string validthru_m, string validthru_y, string cc_approvalcode)
        {
            string cardname = string.Empty;
            cls_globalfunc.getCreditDebiCardInfo(cardno, out cardname);
            dgvCardInfo.Rows.Add();
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colCardNo"].Value = cardno;
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colCardType"].Value = cardname;
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colAmount"].Value = amt.ToString("N2");
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colCardHolder"].Value = cardholder;
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colValidThru_m"].Value = validthru_m;
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colValidThru_y"].Value = validthru_y;
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colApprovalCode"].Value = cc_approvalcode;
            dgvCardInfo.CurrentCell = dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Cells["colCardNo"];
            dgvCardInfo.Rows[dgvCardInfo.RowCount - 1].Selected = true;

            txtAmount_d.Clear();
            txtCardHolder_d.Clear();
            txtCardNo_d.Clear();
            txtValidThru_m.Clear();
            txtValidThru_y.Clear();
            txtApprovalCode_d.Clear();

            txtAmount_d.Focus();
            txtAmount_d.SelectAll();

            fncFilter.set_dgv_display(dgvCardInfo);
        }
        private void DeleteCardInfo()
        {
            if (dgvCardInfo.RowCount > 0)
            {
                dgvCardInfo.Rows.RemoveAt(dgvCardInfo.CurrentCell.RowIndex);
                if (dgvCardInfo.RowCount > 0)
                    dgvCardInfo.CurrentRow.Selected = true;
                txtAmount_d.Focus();
            }
            else
            {
                txtAmount_d.Focus();
                return;
            }

            decimal totalamt = this.gettotalamount();
            lblTotalAmount_d.Text = totalamt.ToString("N2");

        }
        private string FormatCardNumber(string o)
        {
            string result = string.Empty;

            result = Regex.Replace(o, "[^0-9]", string.Empty);

            return result;
        }
        private string FormatName(string o)
        {
            string result = string.Empty;

            if (o.Contains("/"))
            {
                string[] NameSplit = o.Split('/');

                result = NameSplit[1] + " " + NameSplit[0];
            }
            else
            {
                result = o;
            }

            return result;
        }

        private void frmCardInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = (char)e.KeyChar;
            if ((_timerBuf1 && _timerBuf2) || key == '%' || (key == ';' && _cardSwipedString.Split(';').Length == 3))
            {
                _cardSwipedString = "";
                _amountBuf = txtAmount_d.Text;
            }
            _cardSwipedString += key;

            try
            {
                cls_creditCardParser parser = new cls_creditCardParser(_cardSwipedString);
                txtCardHolder_d.Text = parser.Name;
                txtCardNo_d.Text = parser.Number;
                if (parser.ExpirationDate != new DateTime())
                {
                    txtValidThru_m.Text = parser.ExpirationDate.Month.ToString();
                    txtValidThru_y.Text = parser.ExpirationDate.Year.ToString();
                }
                else
                {
                    txtValidThru_m.Text = "";
                    txtValidThru_y.Text = "";
                }
                txtApprovalCode_d.Text = "";
                txtAmount_d.Focus();
                txtAmount_d.Text = _amountBuf;
            }
            catch
            {
            }
            _timerBuf1 = false;
            _timerBuf2 = false;
        }
    }
}