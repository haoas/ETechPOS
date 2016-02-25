using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETech.cls;
using System.Data;

namespace ETech.cls
{
    public class cls_payment
    {
        private List<cls_cardinfo> creditcard;
        private List<cls_cardinfo> debitcard;
        private List<cls_otherpaymentinfo> giftchequenew;
        private List<cls_giftchequeinfo> giftcheque;
        private List<cls_bankcheque> bankcheque;
        private List<cls_CustomPaymentsInfo> list_custompayment;
        private decimal cash;
        private decimal dept;
        private decimal points;
        private decimal eplus;
        private decimal smac;
        private decimal onlinedeal;
        private decimal coupons;
        private string memo;

        public cls_payment()
        {
            this.creditcard = new List<cls_cardinfo>();
            this.debitcard = new List<cls_cardinfo>();
            this.giftchequenew = new List<cls_otherpaymentinfo>();
            this.giftcheque = new List<cls_giftchequeinfo>();
            this.bankcheque = new List<cls_bankcheque>();
            this.list_custompayment = new List<cls_CustomPaymentsInfo>();
            this.cash = 0;
            this.dept = 0;
            this.points = 0;
            this.eplus = 0;
            this.smac = 0;
            this.onlinedeal = 0;
            this.coupons = 0;
            this.memo = "";
        }

        public cls_payment(int salesheadwid, bool is_posd)
        {
            string posdsuffix = is_posd ? "_posd" : "";

            this.creditcard = new List<cls_cardinfo>();
            this.debitcard = new List<cls_cardinfo>();
            this.giftchequenew = new List<cls_otherpaymentinfo>();
            this.giftcheque = new List<cls_giftchequeinfo>();
            this.bankcheque = new List<cls_bankcheque>();
            this.list_custompayment = new List<cls_CustomPaymentsInfo>();
            this.cash = 0;
            this.dept = 0;
            this.points = 0;
            this.eplus = 0;
            this.smac = 0;
            this.onlinedeal = 0;
            this.coupons = 0;
            this.memo = "";

            string sSQL = @"SELECT D.*,H.`memo` 
                    FROM `collectionhead" + posdsuffix + @"` AS H, `collectionsales" + posdsuffix + @"` AS S, 
                        `collectiondetail" + posdsuffix + @"` AS D 
                    WHERE H.`wid` = S.`headid` AND H.`wid` = D.`headid` 
	                   AND S.`saleswid` = " + salesheadwid;
            DataTable dt = mySQLFunc.getdb(sSQL);
            foreach (DataRow dr_d in dt.Rows)
            {
                int dwid = Convert.ToInt32(dr_d["wid"]);
                int method = Convert.ToInt32(dr_d["method"]);
                decimal amt = Convert.ToDecimal(dr_d["amount"]);
                this.memo = dt.Rows[0]["memo"].ToString();

                switch (method)
                {
                    case 1:
                        if (amt > 0)
                            this.cash += amt;
                        break;
                    case 2:
                        this.bankcheque.Add(new cls_bankcheque(dwid, is_posd));
                        break;
                    case 5:
                        this.creditcard.Add(new cls_cardinfo(dwid, 0, is_posd));
                        break;
                    case 6:
                        this.debitcard.Add(new cls_cardinfo(dwid, 1, is_posd));
                        break;
                    case 7:
                        this.giftcheque.Add(new cls_giftchequeinfo(dwid));
                        break;
                    case 8:
                        if (amt > 0)
                            this.points += amt;
                        break;
                    case 9: this.eplus += amt; break;
                    case 10: this.smac += amt; break;
                    case 11: this.onlinedeal += amt; break;
                    case 12: this.coupons += amt; break;
                    case 13:
                        this.giftchequenew.Add(new cls_otherpaymentinfo(dwid, 13));
                        break;
                }

                if (method >= 100)
                {
                    this.list_custompayment.Add(new cls_CustomPaymentsInfo(dwid));
                }
            }
        }

        public void set_memo(string memo_d)
        {
            this.memo = memo_d;
        }

        public void set_creditcard(List<cls_cardinfo> creditcard_d)
        {
            this.creditcard = creditcard_d;
        }

        public void set_debitcard(List<cls_cardinfo> debitcard_d)
        {
            this.debitcard = debitcard_d;
        }

        public void set_giftchequenew(List<cls_otherpaymentinfo> giftchequenew_d)
        {
            this.giftchequenew = giftchequenew_d;
        }

        public void set_giftcheque(List<cls_giftchequeinfo> giftcheque_d)
        {
            this.giftcheque = giftcheque_d;
        }

        public void set_bankcheque(List<cls_bankcheque> bankcheque_d)
        {
            this.bankcheque = bankcheque_d;
        }

        public void set_cash(decimal cash_d)
        {
            this.cash = cash_d;
        }

        public void set_points(decimal points_d)
        {
            this.points = points_d;
        }

        public void set_dept(decimal dept_d)
        {
            this.dept = dept_d;
        }

        public void set_eplus(decimal eplus_d)
        {
            this.eplus = eplus_d;
        }

        public void set_smac(decimal smac_d)
        {
            this.smac = smac_d;
        }

        public void set_onlinedeal(decimal onlinedeal_d)
        {
            this.onlinedeal = onlinedeal_d;
        }

        public void set_coupon(decimal coupon_d)
        {
            this.coupons = coupon_d;
        }

        public void set_custompayments(List<cls_CustomPaymentsInfo> custompayments_d)
        {
            this.list_custompayment = custompayments_d;
        }

        public string get_memo() { return this.memo; }
        public List<cls_cardinfo> get_creditcard() { return this.creditcard; }
        public List<cls_cardinfo> get_debitcard() { return this.debitcard; }
        public List<cls_otherpaymentinfo> get_giftchequenew() { return this.giftchequenew; }
        public List<cls_giftchequeinfo> get_giftcheque() { return this.giftcheque; }
        public List<cls_bankcheque> get_bankcheque() { return this.bankcheque; }
        public List<cls_CustomPaymentsInfo> get_custompayments() { return this.list_custompayment; }
        public decimal get_cash() { return this.cash; }
        public decimal get_dept() { return this.dept; }
        public decimal get_points() { return this.points; }
        public decimal get_eplus() { return this.eplus; }
        public decimal get_smac() { return this.smac; }
        public decimal get_onlinedeal() { return this.onlinedeal; }
        public decimal get_coupons() { return this.coupons; }

        public decimal get_creditamount()
        {
            decimal amt = 0;
            foreach (cls_cardinfo ccard in this.creditcard)
            {
                amt += ccard.getamount();
            }
            return amt;
        }

        public decimal get_debitamount()
        {
            decimal amt = 0;
            foreach (cls_cardinfo dcard in this.debitcard)
            {
                amt += dcard.getamount();
            }
            return amt;
        }

        public decimal get_giftchequenewamount()
        {
            decimal amt = 0;
            foreach (cls_otherpaymentinfo gc in this.giftchequenew)
            {
                amt += gc.getamount();
            }
            return amt;
        }

        public decimal get_giftchequeamount()
        {
            decimal amt = 0;
            foreach (cls_giftchequeinfo gcheck in this.giftcheque)
            {
                amt += gcheck.getamount();
            }
            return amt;
        }

        public decimal get_bankchequeamount()
        {
            decimal amt = 0;
            foreach (cls_bankcheque bcheck in this.bankcheque)
            {
                amt += bcheck.getamount();
            }
            return amt;
        }

        public decimal get_custompaymentamount()
        {
            decimal amt = 0;
            foreach (cls_CustomPaymentsInfo custompaymentsinfo in this.list_custompayment)
            {
                amt += custompaymentsinfo.get_amount();
            }
            return amt;
        }

        public decimal get_totalamount()
        {
            return this.get_creditamount() +
                    this.get_debitamount() +
                    this.get_giftchequenewamount() +
                    this.get_giftchequeamount() +
                    this.get_bankchequeamount() +
                    this.get_custompaymentamount() +
                    this.get_eplus() +
                    this.get_smac() +
                    this.get_onlinedeal() +
                    this.get_coupons() +
                    this.get_cash() +
                    this.get_points();
        }

        public cls_payment DeepCopy()
        {
            cls_payment payment_clone = (cls_payment)this.MemberwiseClone();
            payment_clone.set_creditcard(this.creditcard.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_debitcard(this.debitcard.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_giftchequenew(this.giftchequenew.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_giftcheque(this.giftcheque.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_bankcheque(this.bankcheque.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_custompayments(this.list_custompayment.Select(i => i.ShallowCopy()).ToList());
            return payment_clone;
        }

    }
}
