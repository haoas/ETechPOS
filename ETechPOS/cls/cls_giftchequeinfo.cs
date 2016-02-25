using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_giftchequeinfo
    {
        //attributes
        private int wid;
        private string giftchequeno;
        private decimal amount;

        //constructor
        public cls_giftchequeinfo()
        {
            this.wid = 0;
            this.giftchequeno = "";
            this.amount = 0;
        }

        public cls_giftchequeinfo(int wid_d)
        {
            this.wid = 0;
            this.giftchequeno = "";
            this.amount = 0;

            string sSQL = @"SELECT G.* FROM `posgiftcheque` AS P, `giftcheque` AS G
                    WHERE G.`wid` = P.`chequeid` AND G.`status` = 1 AND
                        P.`collectiondetailid` = " + wid_d;
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
                return;

            DataRow dr = dt.Rows[0];
            this.wid = Convert.ToInt32(dr["wid"]);
            this.giftchequeno = dr["chequeno"].ToString();
            this.amount = Convert.ToDecimal(dr["amount"]);
        }

        //set
        public void setgiftchequeinfo(int wid_d, string giftchequeno_d, decimal amount_d)
        {
            this.wid = wid_d;
            this.giftchequeno = giftchequeno_d;
            this.amount = amount_d;
        }

        //get
        public int getwid()
        {
            return this.wid;
        }

        public string getgiftchequeno()
        {
            return this.giftchequeno;
        }

        public decimal getamount()
        {
            return this.amount;
        }

        public cls_giftchequeinfo ShallowCopy()
        {
            return (cls_giftchequeinfo)this.MemberwiseClone();
        }
    }
}
