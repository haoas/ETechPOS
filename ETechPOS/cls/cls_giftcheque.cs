using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_giftcheque
    {
        private string referenceno;
        private DateTime expdate;
        private decimal amount;
        private string memo;
        private int paymentmethod; //wid

        //constructor
        public cls_giftcheque()
        {
            this.referenceno = "";
            this.expdate = new DateTime();
            this.amount = 0;
            this.memo = "";
            this.paymentmethod = 0;
        }

        public cls_giftcheque(int collectiondetailwid_d, int paymentmethod)
        {
            this.referenceno = "";
            this.expdate = new DateTime();
            this.amount = 0;
            this.memo = "";

            string otherpaymenttable = "posgiftchequepayment";
            string reference = "giftchequeno";

            if (paymentmethod == 13)
            {
                otherpaymenttable = "posgiftchequepayment";
                reference = "giftchequeno";
            }

            string SQL = @"SELECT * FROM `" + otherpaymenttable + @"` 
                            WHERE `collectiondetailid` = " + collectiondetailwid_d + @" LIMIT 1";

            DataTable DT = mySQLFunc.getdb(SQL);
            if ((DT.Rows.Count <= 0) || DT == null)
                return;

            DataRow DR = DT.Rows[0];
            this.referenceno = DR[reference].ToString();
            this.expdate = Convert.ToDateTime(DR["expdate"]);
            this.amount = Convert.ToDecimal(DR["amount"]);
            this.memo = DR["memo"].ToString();
        }

        public void setotherpaymentinfo(string referenceno_d, DateTime expdate_d, 
            decimal amount_d, string memo_d, int paymentmethod_d)
        {
            this.referenceno = referenceno_d;
            this.expdate = expdate_d;
            this.amount = amount_d;
            this.memo = memo_d;
            this.paymentmethod = paymentmethod_d;
        }

        public cls_giftcheque ShallowCopy()
        {
            return (cls_giftcheque)this.MemberwiseClone();
        }

        public string get_referenceno()
        {
            return this.referenceno;
        }

        public DateTime getexpdate()
        {
            return this.expdate;
        }

        public decimal getamount()
        {
            return this.amount;
        }

        public string get_memo()
        {
            return this.memo;
        }


    }



}
