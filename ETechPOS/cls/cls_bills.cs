using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_bills
    {
        private int cash_1000;
        private int cash_500;
        private int cash_200;
        private int cash_100;
        private int cash_50;
        private int cash_20;
        private int cash_10;
        private int cash_5;
        private int cash_1;
        private int cash_25c;
        private int cash_10c;
        private int cash_5c;
        private int type;

        //constructor
        public cls_bills()
        {
            this.cash_1000 = 0;
            this.cash_500 = 0;
            this.cash_200 = 0;
            this.cash_100 = 0;
            this.cash_50 = 0;
            this.cash_20 = 0;
            this.cash_10 = 0;
            this.cash_5 = 0;
            this.cash_1 = 0;
            this.cash_25c = 0;
            this.cash_10c = 0;
            this.cash_5c = 0;
            this.type = -1;
        }

        //set bills
        public void setBills(int cash_1000_d, int cash_500_d, int cash_200_d, int cash_100_d,
                             int cash_50_d, int cash_20_d, int cash_10_d, int cash_5_d,
                             int cash_1_d, int cash_25c_d, int cash_10c_d, int cash_5c_d)
        {
            this.cash_1000 = cash_1000_d;
            this.cash_500 = cash_500_d;
            this.cash_200 = cash_200_d;
            this.cash_100 = cash_100_d;
            this.cash_50 = cash_50_d;
            this.cash_20 = cash_20_d;
            this.cash_10 = cash_10_d;
            this.cash_5 = cash_5_d;
            this.cash_1 = cash_1_d;
            this.cash_25c = cash_25c_d;
            this.cash_10c = cash_10c_d;
            this.cash_5c = cash_5c_d;
        }

        public void set_type(int type_d) { this.type = type_d; }
        public int get_type() { return this.type; }

        //get bills
        public int getCash_1000()
        {
            return this.cash_1000;
        }
        public int getCash_500()
        {
            return this.cash_500;
        }
        public int getCash_200()
        {
            return this.cash_200;
        }
        public int getCash_100()
        {
            return this.cash_100;
        }
        public int getCash_50()
        {
            return this.cash_50;
        }
        public int getCash_20()
        {
            return this.cash_20;
        }
        public int getCash_10()
        {
            return this.cash_10;
        }
        public int getCash_5()
        {
            return this.cash_5;
        }
        public int getCash_1()
        {
            return this.cash_1;
        }
        public int getCash_25c()
        {
            return this.cash_25c;
        }
        public int getCash_10c()
        {
            return this.cash_10c;
        }
        public int getCash_5c()
        {
            return this.cash_5c;
        }

        public void save_cashdenomination(cls_user cashier)
        {
            //type:
            //1 - beginning
            //2 - pickup
            //3 - ending

            mySQLClass mysqlclass = new mySQLClass();

            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;
            int userwid = cashier.getwid();
            int wid = mysqlclass.get_next_wid_withlock("poscashdenomination");

            if (this.get_type() == 3)
            {
                mySQLFunc.setdb(@"UPDATE `poscashdenomination` SET `type` = 2 
                                    WHERE `type` = 3 AND CAST(`datecreated` AS DATE) = CAST(NOW() AS DATE)
                                        AND `branchid` = " + branchid + @" AND `terminalno` = " + terminalno);
            }

           string sSQL = @"UPDATE `poscashdenomination` SET 
               `branchid` = " + branchid + ", `terminalno` = " + terminalno + 
               ", `userid` = " + userwid + ", `datecreated` = NOW(), `type` = " + this.get_type() +
               ", `b1000` = " + this.getCash_1000() + ", `b500` = " + this.getCash_500() + 
               ", `b200` = " + this.getCash_200() + ", `b100` = " + this.getCash_100() + 
               ", `b50` = " + this.getCash_50() + ", `b20` = " + this.getCash_20() + 
               ", `c10` = " + this.getCash_10() + ", `c5` = " + this.getCash_5() + 
               ", `c1` = " + this.getCash_1() + ", `c25c` = " + this.getCash_25c() + 
               ", `c10c` = " + this.getCash_10c() + ", `c5c` = " + this.getCash_5c() + @" 
               WHERE `wid` = " + wid + " LIMIT 1";

            //Console.WriteLine(sSQL);
            mySQLFunc.setdb(sSQL);

            mysqlclass.update_synctable("poscashdenomination", wid);
        }
        public void get_cashdenomination(cls_user cashier, int type)
        {
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;
            int userwid = cashier.getwid();

            string sSQL = @"SELECT * FROM `poscashdenomination` WHERE 
                            `branchid` = " + branchid + @" AND
                            `terminalno` = " + terminalno + @" AND 
                            CAST(`datecreated` AS DATE) = CAST(NOW() AS DATE) AND 
                            `type` = " + type + @"
                            ORDER BY `id` DESC
                            LIMIT 1";

            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
                return;

            DataRow dr = dt.Rows[0];

            this.cash_1000 = Convert.ToInt32(dr["b1000"]);
            this.cash_500 = Convert.ToInt32(dr["b500"]);
            this.cash_200 = Convert.ToInt32(dr["b200"]);
            this.cash_100 = Convert.ToInt32(dr["b100"]);
            this.cash_50 = Convert.ToInt32(dr["b50"]);
            this.cash_20 = Convert.ToInt32(dr["b20"]);
            this.cash_10 = Convert.ToInt32(dr["c10"]);
            this.cash_5 = Convert.ToInt32(dr["c5"]);
            this.cash_1 = Convert.ToInt32(dr["c1"]);
            this.cash_25c = Convert.ToInt32(dr["c25c"]);
            this.cash_10c = Convert.ToInt32(dr["c10c"]);
            this.cash_5c = Convert.ToInt32(dr["c5c"]);
            this.type = 0;
        }

        public decimal get_totalamount()
        {
            return (this.cash_1000 * 1000)
                   + (this.cash_500 * 500)
                   + (this.cash_200 * 200)
                   + (this.cash_100 * 100)
                   + (this.cash_50 * 50)
                   + (this.cash_20 * 20)
                   + (this.cash_10 * 10)
                   + (this.cash_5 * 5)
                   + (this.cash_1 * 1)
                   + (this.cash_25c * 0.25M)
                   + (this.cash_10c * 0.1M)
                   + (this.cash_5c * 0.05M);
        }
    }
}
