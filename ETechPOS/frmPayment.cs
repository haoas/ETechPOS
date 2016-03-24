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
    public partial class frmPayment : Form
    {
        public cls_payment paymentdata;
        public bool changeupdated;
        public bool transactiondone;
        public decimal totalamtdue;
        public decimal totalpoints;
        public decimal totaleplusdue;
        public decimal totalsmacdue;
        public decimal totalonlinedealsdue;
        public decimal totalcouponsdue;
        public bool hasMember;

        public frmPayment()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            txtCashRcv_d.SelectAll();

            paymentdata = new cls_payment();
            changeupdated = false;
            transactiondone = false;
            totalamtdue = 0;
            totalpoints = 0;
            totaleplusdue = 0;
            totalsmacdue = 0;
            totalonlinedealsdue = 0;
            totalcouponsdue = 0;
        }

        private void refresh_total_amount()
        {
            decimal cash_d = fncFilter.getDecimalValue(this.txtCashRcv_d.Text);
            decimal points_d = fncFilter.getDecimalValue(this.txtPointUsed_d.Text);
            this.paymentdata.set_cash(cash_d);
            this.paymentdata.set_points(points_d);

            decimal totalamt = this.paymentdata.get_totalamount();
            if(points_d >= 0)
                lblremainingpts_d.Text = (this.totalpoints - points_d).ToString("N2");
            lblTotal.Text = totalamt.ToString("N2");
            decimal remaining = this.totalamtdue - totalamt;
            lblRemaining_d.Text = ((remaining < 0) ? 0 : remaining).ToString("N2");
        }

        private void F8()
        {
            frmCardInfo cardinfo = new frmCardInfo();
            cardinfo.cardinfos = this.paymentdata.get_creditcard();
            cardinfo.ShowDialog();
            txtCashRcv_d.Focus();
            txtCashRcv_d.SelectAll();

            lblCreditCard_d.Text = this.paymentdata.get_creditamount().ToString("N2");
            refresh_total_amount();
        }
        private void F9()
        {
            frmCardInfo cardinfo = new frmCardInfo();
            cardinfo.cardinfos = this.paymentdata.get_debitcard();
            cardinfo.ShowDialog();
            txtCashRcv_d.Focus();
            txtCashRcv_d.SelectAll();

            lblDebitCard_d.Text = this.paymentdata.get_debitamount().ToString("N2");
            refresh_total_amount();
        }
        private void F10()
        {
            frmOtherPayment gcinfo = new frmOtherPayment();
            gcinfo.gcinfos = this.paymentdata.get_giftchequenew();
            gcinfo.ShowDialog();
            txtCashRcv_d.Focus();
            txtCashRcv_d.SelectAll();

            lblGiftCheque_d.Text = this.paymentdata.get_giftchequenewamount().ToString("N2");
            refresh_total_amount();
        }
        private void F12()
        {
            if (btnCustomPayment.Visible == false)
                return;

            if (!HasCustomPayment())
            {
                fncFilter.alert(cls_globalvariables.warning_no_custom_payment);
                return;
            }

            frmCustomPayments paymentsinfo = new frmCustomPayments();
            paymentsinfo.list_Custompayments = this.paymentdata.get_custompayments();
            paymentsinfo.ShowDialog();

            txtCashRcv_d.Focus();
            txtCashRcv_d.SelectAll();

            lblCustomAmt_d.Text = this.paymentdata.get_custompaymentamount().ToString("N2");
            refresh_total_amount();
        }
        private void frmPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
            {
                transactiondone = true;
                done_process();
                return;
            }
            //else if (e.KeyCode == Keys.F5)
            //{
            //    transactiondone = false;
            //    done_process();
            //    return;
            //}
            else if (e.KeyCode == Keys.F8)
                F8();
            else if (e.KeyCode == Keys.F9)
                F9();
            else if (e.KeyCode == Keys.F10)
                F10();
            else if (e.KeyCode == Keys.F12)
                F12();
            else if (e.KeyCode == Keys.Escape)
            {
                changeupdated = false;
                transactiondone = false;
                this.Close();
                return;
            }
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            transactiondone = true;
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            changeupdated = false;
            transactiondone = false;
            this.Close();
        }
        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            F8();
        }
        private void btnDebitCard_Click(object sender, EventArgs e)
        {
            F9();
        }
        private void btnGiftCheque_Click(object sender, EventArgs e)
        {
            F10();
        }
        private void btnCustomPayment_Click(object sender, EventArgs e)
        {
            F12();
        }
        private void done_process()
        {
            decimal cash_d = fncFilter.getDecimalValue(this.txtCashRcv_d.Text);
            if (cash_d > 9999999) return;
            if (cash_d < 0) cash_d = 0;
            decimal points_d = fncFilter.getDecimalValue(this.txtPointUsed_d.Text);
            decimal debitcard_d = fncFilter.getDecimalValue(this.lblDebitCard_d.Text);
            decimal creditcard_d = fncFilter.getDecimalValue(this.lblCreditCard_d.Text);
            decimal GiftCheque_d = fncFilter.getDecimalValue(this.lblGiftCheque_d.Text);
            decimal customPayments_d = fncFilter.getDecimalValue(this.lblCustomAmt_d.Text);
            decimal allTenders_d = Math.Round(cash_d + points_d + debitcard_d + creditcard_d + GiftCheque_d + customPayments_d, 2, MidpointRounding.AwayFromZero);

            decimal Non_GC_tender_d = allTenders_d - GiftCheque_d;
            decimal Non_Cash_tender_d = allTenders_d - cash_d;
            decimal Total_AmtDue = Math.Round(this.totalamtdue, 2, MidpointRounding.AwayFromZero);

            if ((Total_AmtDue > 0) && (Total_AmtDue < Non_Cash_tender_d))
            {
                return;
            }

            if (points_d > this.totalpoints && points_d != 0 && this.totalpoints >= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_mempointtender_invalid);
                this.txtPointUsed_d.Focus();
                this.txtPointUsed_d.SelectAll();
                return;
            }

            this.paymentdata.set_cash(cash_d);
            this.paymentdata.set_points(points_d);
            changeupdated = true;

            this.Close();
        }

        private void txtCashRcv_d_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txtEplusRcv_d_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txtSMACPtsRcv_d_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txtOnlineDealRcv_d_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txtCouponsRcv_d_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void txtPointUsed_d_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        private void lblCustomAmt_TextChanged(object sender, EventArgs e)
        {
            refresh_total_amount();
        }
        public void frmPayment_Load(object sender, EventArgs e)
        {
            decimal cash_d = this.paymentdata.get_cash();
            decimal points_d = this.paymentdata.get_points();

            this.txtCashRcv_d.Text = cash_d.ToString("N2");
            this.lblCreditCard_d.Text = this.paymentdata.get_creditamount().ToString("N2");
            this.lblDebitCard_d.Text = this.paymentdata.get_debitamount().ToString("N2");
            this.lblGiftCheque_d.Text = (Convert.ToDecimal(this.paymentdata.get_giftchequenewamount())).ToString("N2");
            this.txtPointUsed_d.Text = points_d.ToString("N2");
            this.lblCustomAmt_d.Text = this.paymentdata.get_custompaymentamount().ToString("N2");

            if (!this.hasMember || (this.totalpoints <= 0 && this.totalamtdue > 0))
            {
                this.lblPointUsed.Visible = false;
                this.txtPointUsed_d.Visible = false;
                this.lblremainingpts.Visible = false;
                this.lblremainingpts_d.Visible = false;
            }

            refresh_total_amount();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }

        private bool HasCustomPayment()
        {
            string selectSql =
                @"SELECT
	                `SyncId` AS `Payment Id`
                FROM
	                `paymentmethod`
                WHERE
	                `SyncId` >= 100
                ORDER BY
	                `SyncId`";
            DataTable resultDt = mySQLFunc.getdb(selectSql);
            if (resultDt == null || resultDt.Rows.Count <= 0)
                return false;
            else
                return true;
        }

        /*
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        */
    }
}
