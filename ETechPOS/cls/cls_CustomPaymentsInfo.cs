using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_CustomPaymentsInfo
    {
        private int paymentwid;
        private string paymentname;
        private decimal amount;
        private string field1info;
        private string field2info;
        private string field3info;
        private string field4info;
        private string field5info;
        private string field6info;

        public void init()
        {
            this.paymentwid = 0;
            this.paymentname = "";
            this.amount = 0;
            this.field1info = "";
            this.field2info = "";
            this.field3info = "";
            this.field4info = "";
            this.field5info = "";
            this.field6info = "";
        }

        public cls_CustomPaymentsInfo()
        {
            init();
        }

        public cls_CustomPaymentsInfo(int wid_d)
        {
            init();

            string sSQL = @"SELECT COALESCE(D.`method`,100) as `method`, 
	                       COALESCE(D.`amount`,0) as `amount`,
	                       COALESCE(P.`name`,'') as `paymentname`,
	                       COALESCE(C.`field1`,'') as `field1`,
	                       COALESCE(C.`field2`,'') as `field2`, 
	                       COALESCE(C.`field3`,'') as `field3`,
	                       COALESCE(C.`field4`,'') as `field4`, 
	                       COALESCE(C.`field5`,'') as `field5`,
	                       COALESCE(C.`field6`,'') as `field6`
                    FROM  `paymentmethod` as P, `collectiondetail` as D
                    LEFT JOIN  `poscustompayments` as C  ON C.`detailid` = D.`wid` AND C.`detailid` = " + wid_d + @"
                        WHERE P.`wid`=D.`method` AND D.`wid` = " + wid_d;

            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
                return;
            DataRow dr = dt.Rows[0];

            this.paymentwid = Convert.ToInt32(dr["method"]);
            this.paymentname = dr["paymentname"].ToString();
            this.amount = Convert.ToDecimal(dr["amount"]);
            this.field1info = dr["field1"].ToString();
            this.field2info = dr["field2"].ToString();
            this.field3info = dr["field3"].ToString();
            this.field4info = dr["field4"].ToString();
            this.field5info = dr["field5"].ToString();
            this.field6info = dr["field6"].ToString();
        }

        public void set_CustomPaymentsInfo(int paymentwid_d, string paymentname_d, decimal amount_d,
                                           string field1_d, string field2_d, string field3_d,
                                           string field4_d, string field5_d, string field6_d)
        {
            this.paymentwid = paymentwid_d;
            this.paymentname = paymentname_d;
            this.amount = amount_d;
            this.field1info = field1_d;
            this.field2info = field2_d;
            this.field3info = field3_d;
            this.field4info = field4_d;
            this.field5info = field5_d;
            this.field6info = field6_d;
        }

        public int get_paymentwid()
        {
            return this.paymentwid;
        }
        public string get_paymentname()
        {
            return this.paymentname;
        }
        public decimal get_amount()
        {
            return this.amount;
        }
        public string get_field1info()
        {
            return this.field1info;
        }
        public string get_field2info()
        {
            return this.field2info;
        }
        public string get_field3info()
        {
            return this.field3info;
        }
        public string get_field4info()
        {
            return this.field4info;
        }
        public string get_field5info()
        {
            return this.field5info;
        }
        public string get_field6info()
        {
            return this.field6info;
        }
        public cls_CustomPaymentsInfo ShallowCopy()
        {
            return (cls_CustomPaymentsInfo)this.MemberwiseClone();
        }
    }
}
