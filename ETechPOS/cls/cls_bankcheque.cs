using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_bankcheque
    {
        //private string accountno;
        //private string accountname;
        private DateTime chequedate;
        private string chequeno;
        private decimal amount;
        private string payto;
        private string bank;
        private bool isbanktransitonly;

        public cls_bankcheque()
        {
            //this.accountno = "";
            //this.accountname = "";
            this.chequedate = new DateTime();
            this.chequeno = "";
            this.amount = 0;
            this.payto = "";
            this.bank = "";
            this.isbanktransitonly = false;
        }

        public cls_bankcheque(int wid_d, bool is_posd)
        {
            string posdsuffix = is_posd ? "_posd" : "";
            //this.accountno = "";
            //this.accountname = "";
            this.chequedate = new DateTime();
            this.chequeno = "";
            this.amount = 0;
            this.payto = "";
            this.bank = "";
            this.isbanktransitonly = false;

            string sSQL =
                @"SELECT `checkdate`,`checkno`,`checkbank`,`amount` 
                FROM `receivecheck" + posdsuffix + @"` 
                WHERE `reference` = 1 AND `referenceno` = " + wid_d;
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            DateTime tempDateTime = new DateTime();
            this.chequedate = DateTime.TryParse(dr["checkdate"].ToString(), out tempDateTime) ? tempDateTime : new DateTime();
            this.chequeno = dr["checkno"].ToString();
            this.bank = dr["checkbank"].ToString();
            this.amount = Convert.ToDecimal(dr["amount"]);
        }

        public void setbankchqueinfo(DateTime chequedate_d, string chequeno_d,
                                    decimal amount_d, string payto_d, string bank_d, bool isbanktransitonly_d)
        {
            //this.accountno = accountno_d;
            //this.accountname = accountname_d;
            this.chequedate = chequedate_d;
            this.chequeno = chequeno_d;
            this.amount = amount_d;
            this.payto = payto_d;
            this.bank = bank_d;
            this.isbanktransitonly = isbanktransitonly_d;
        }

        //public string getaccountno() { return this.accountno; }
        //public string getaccountname() { return this.accountname; }
        public DateTime getchequedate() { return this.chequedate; }
        public string getchequeno() { return this.chequeno; }
        public decimal getamount() { return this.amount; }
        public string getpayto() { return this.payto; }
        public string getbank() { return this.bank; }
        public bool getisbanktransitonly() { return this.isbanktransitonly; }

        public cls_bankcheque ShallowCopy()
        {
            return (cls_bankcheque)this.MemberwiseClone();
        }

    }
}
