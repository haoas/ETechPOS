using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_cardinfo
    {
        //attributes
        private string name;
        private string cardno;
        private string cardname;
        private DateTime expdate;
        private decimal amount;
        private string approvalcode;

        //constructor
        public cls_cardinfo()
        {
            this.name = "";
            this.cardno = "";
            this.cardname = "";
            this.expdate = new DateTime();
            this.amount = 0;
            this.approvalcode = "";
        }

        public cls_cardinfo(int wid_d, int type_d)
        {
            this.name = "";
            this.cardno = "";
            this.cardname = "";
            this.expdate = new DateTime();
            this.amount = 0;

            string SQLcardno = @"SELECT * FROM `poscardpayment` 
                            WHERE `type` = " + type_d + " AND `collectiondetailid` = " + wid_d;
            string cardno = mySQLFunc.getdb(SQLcardno).Rows[0]["cardno"].ToString();

            string cardname = string.Empty;
            cls_globalfunc.getCreditDebiCardInfo(cardno, out cardname);

            string sSQL = @"SELECT *,
                     '" + cardname + @"' as cardtype
                     FROM `poscardpayment` 
                     WHERE `type` = " + type_d + " AND `collectiondetailid` = " + wid_d;
            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            this.name = dr["fullname"].ToString();
            this.cardno = dr["cardno"].ToString();
            this.cardname = dr["cardtype"].ToString();
            this.expdate = DateTime.Parse(dr["expdate"].ToString());
            this.amount = Convert.ToDecimal(dr["amount"]);
            this.approvalcode = dr["approvalcode"].ToString();
        }

        //set card info
        public void setcardinfo(string name_d, string cardno_d, string cardname_d, DateTime expdate_d, decimal amount_d, string approvalcode_d)
        {
            this.name = name_d;
            this.cardno = cardno_d;
            this.cardname = cardname_d;
            this.expdate = expdate_d;
            this.amount = amount_d;
            this.approvalcode = approvalcode_d;
        }

        //getname - return name
        public string getname()
        {
            return this.name;
        }

        //getcardno - return cardno
        public string getcardno()
        {
            string first2char = this.cardno.Substring(0, 2);
            string last4char = this.cardno.Substring(this.cardno.Length - 4, 4);
            return first2char.PadRight(this.cardno.Length - 4, 'X') + last4char;
        }

        //getexpdate - return expdate
        public DateTime getexpdate()
        {
            return this.expdate;
        }

        public string getcardname()
        {
            return this.cardname;
        }

        //getamount - return amount
        public decimal getamount()
        {
            return this.amount;
        }

        public cls_cardinfo ShallowCopy()
        {
            return (cls_cardinfo)this.MemberwiseClone();
        }

        public string getapprovalcode()
        {
            return this.approvalcode;
        }
    }
}
