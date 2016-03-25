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
        private List<cls_giftcheque> giftchequenew;
        private List<cls_CustomPaymentsInfo> list_custompayment;
        private decimal cash;
        private decimal points;
        private string memo;

        public cls_payment()
        {
            this.creditcard = new List<cls_cardinfo>();
            this.debitcard = new List<cls_cardinfo>();
            this.giftchequenew = new List<cls_giftcheque>();
            this.list_custompayment = new List<cls_CustomPaymentsInfo>();
            this.cash = 0;
            this.points = 0;
            this.memo = "";
        }

        public cls_payment(long salesheadwid)
        {
            this.creditcard = new List<cls_cardinfo>();
            this.debitcard = new List<cls_cardinfo>();
            this.giftchequenew = new List<cls_giftcheque>();
            this.list_custompayment = new List<cls_CustomPaymentsInfo>();
            this.cash = 0;
            this.points = 0;
            this.memo = "";

            string sSQL = @"SELECT D.*,H.`memo` 
                    FROM `collectionhead` AS H, `collectionsales` AS S, 
                        `collectiondetail` AS D 
                    WHERE H.`SyncId` = S.`headid` AND H.`SyncId` = D.`headid` 
	                   AND S.`saleswid` = " + salesheadwid;
            DataTable dt = mySQLFunc.getdb(sSQL);
            foreach (DataRow dr_d in dt.Rows)
            {
                int dwid = Convert.ToInt32(dr_d["SyncId"]);
                int method = Convert.ToInt32(dr_d["method"]);
                decimal amt = Convert.ToDecimal(dr_d["amount"]);
                this.memo = dt.Rows[0]["memo"].ToString();

                switch (method)
                {
                    case 1:
                        if (amt > 0)
                            this.cash += amt;
                        break;
                    case 5:
                        this.creditcard.Add(new cls_cardinfo(dwid, 0));
                        break;
                    case 6:
                        this.debitcard.Add(new cls_cardinfo(dwid, 1));
                        break;
                    case 8:
                        if (amt > 0)
                            this.points += amt;
                        break;
                    case 13:
                        this.giftchequenew.Add(new cls_giftcheque(dwid, 13));
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

        public void set_giftchequenew(List<cls_giftcheque> giftchequenew_d)
        {
            this.giftchequenew = giftchequenew_d;
        }

        public void set_cash(decimal cash_d)
        {
            this.cash = cash_d;
        }

        public void set_points(decimal points_d)
        {
            this.points = points_d;
        }

        public void set_custompayments(List<cls_CustomPaymentsInfo> custompayments_d)
        {
            this.list_custompayment = custompayments_d;
        }

        public string get_memo() { return this.memo; }
        public List<cls_cardinfo> get_creditcard() { return this.creditcard; }
        public List<cls_cardinfo> get_debitcard() { return this.debitcard; }
        public List<cls_giftcheque> get_giftchequenew() { return this.giftchequenew; }
        public List<cls_CustomPaymentsInfo> get_custompayments() { return this.list_custompayment; }
        public decimal get_cash() { return this.cash; }
        public decimal get_points() { return this.points; }

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
            foreach (cls_giftcheque gc in this.giftchequenew)
            {
                amt += gc.getamount();
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
                    this.get_custompaymentamount() +
                    this.get_cash() +
                    this.get_points();
        }

        public cls_payment DeepCopy()
        {
            cls_payment payment_clone = (cls_payment)this.MemberwiseClone();
            payment_clone.set_creditcard(this.creditcard.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_debitcard(this.debitcard.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_giftchequenew(this.giftchequenew.Select(i => i.ShallowCopy()).ToList());
            payment_clone.set_custompayments(this.list_custompayment.Select(i => i.ShallowCopy()).ToList());
            return payment_clone;
        }

    }
}
