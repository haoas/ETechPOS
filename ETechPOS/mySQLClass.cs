using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETech.cls;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;


namespace ETech
{
    class mySQLClass
    {
        public mySQLClass()
        {
        }

        private MySqlConnection connecttoSQL()
        {
            string server = cls_globalvariables.server_v;
            string userid = cls_globalvariables.userid_v;
            string password = cls_globalvariables.password_v;
            string database = cls_globalvariables.database_v;

            string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database;

            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
            }
            catch (Exception)
            {
                //MessageBox.Show("Error detected while connecting to database");
            }
            return conn;
        }

        public Boolean setdb(string SQL)
        {
            for (int retryno = 0; retryno < 20; retryno++)
            {
                try
                {
                    string server = cls_globalvariables.server_v;
                    string userid = cls_globalvariables.userid_v;
                    string password = cls_globalvariables.password_v;
                    string database = cls_globalvariables.database_v;

                    string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database + ";Allow Zero Datetime=true;Connect Timeout=300;";

                    MySqlConnection myconn = new MySqlConnection(cs);
                    myconn.Open();

                    MySqlCommand cmd = new MySqlCommand(SQL, myconn);
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();
                    myconn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    string nw = DateTime.Now.ToString();
                    string terminalno = cls_globalvariables.terminalno_v.ToString();
                    string errcode = ex.ToString();
                    WriteToErrorLog(" \n Date: " + nw + " - Terminalno: " + terminalno + " - Retry #: " + retryno + " \n Exception: \n " + errcode + " \n Query: \n " + SQL + " \n ");
                    System.Threading.Thread.Sleep(500);
                }
            }
            WriteToErrorLog(" \n Date: " + DateTime.Now.ToString() + " - Terminalno: " + cls_globalvariables.terminalno_v.ToString() + " \n Exception: \n Unable to execute query after many tries. \n Query: \n " + SQL + " \n ");
            return false;
        }

        public DataTable getdb(string SQL, List<string> parameters, List<string> values)
        {
            DataTable dataTable = new DataTable();

            string server = cls_globalvariables.server_v;
            string userid = cls_globalvariables.userid_v;
            string password = cls_globalvariables.password_v;
            string database = cls_globalvariables.database_v;

            string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database;

            MySqlConnection myconn = null;

            try
            {
                myconn = new MySqlConnection(cs);
                myconn.Open();
            }
            catch (MySqlException)
            {
                //MessageBox.Show("Error detected while connecting to database");
                return null;
            }

            MySqlCommand cmd = new MySqlCommand(SQL, myconn);
            cmd.CommandTimeout = 300;
            for (int i = 0; i < parameters.Count; i++)
            {
                cmd.Parameters.AddWithValue(parameters[i], values[i]);
            }

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);
            myconn.Close();

            return dataTable;
        }

        public DataTable getdb(string SQL)
        {
            DataTable dataTable = new DataTable();

            string server = cls_globalvariables.server_v;
            string userid = cls_globalvariables.userid_v;
            string password = cls_globalvariables.password_v;
            string database = cls_globalvariables.database_v;
            string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database;

            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(cs);
                myconn.Open();
            }
            catch (MySqlException)
            {
                //MessageBox.Show("Error detected while connecting to database");
                return null;
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand(SQL, myconn);
                cmd.CommandTimeout = 300;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
            }
            catch (MySqlException ex)
            {
                WriteToErrorLog(" \n Date: " + DateTime.Now + " \n Exception: \n " + ex.ToString() +
                   " \n getdb Query: \n " + SQL + " \n ");

                dataTable = new DataTable();
            }

            myconn.Close();
            return dataTable;
        }

        public DataTable getdb_branch(string SQL, string server, string userid, string password, string database)
        {
            string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database + ";Allow Zero Datetime=true;Charset = utf8;Connect Timeout=300;";

            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(cs);
                myconn.Open();
                DataTable dataTable = new DataTable();
                MySqlCommand cmd = new MySqlCommand(SQL, myconn);
                cmd.CommandTimeout = 300;
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                }
                myconn.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                LOGS.LOG_PRINT("[getdb_branch] " + ex.Message.ToString());
                LOGS.LOG_PRINT("[getdb_branch(sql)] " + SQL);

                Console.WriteLine("getdb_branch error: " + SQL);
                Console.WriteLine(ex.Message.ToString());
                return new DataTable();
            }


        }

        public Boolean setdb_branch(string SQL, string server, string userid, string password, string database)
        {
            string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database + ";Allow Zero Datetime=true;Charset = utf8;Connect Timeout=300;";

            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(cs);
                myconn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("setdb_branch connect error: " + server);
                Console.WriteLine(ex.Message.ToString());
                return false;
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand(SQL, myconn);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
                myconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                LOGS.LOG_PRINT("[setdb_branch] " + ex.Message.ToString());
                LOGS.LOG_PRINT("[setdb_branch(sql)] " + SQL);

                Console.WriteLine("setdb_branch error: " + SQL);
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public DataTable getdb_main(string SQL)
        {
            if (cls_globalvariables.BranchCode != cls_globalvariables.MainBranchCode)
            {
                string server = "";
                string userid = "";
                string password = "";
                string database = "";
                string sql = @"SELECT
                            `sqlserver` AS `server`,
                            `sqldatabase` AS `database`,
                            `sqlusername` AS `username`,
                            `sqlpassword` AS `password`
                           FROM `branch`
                           WHERE `wid` = 10 LIMIT 1";
                DataTable connectionTable = getdb(sql);
                if (connectionTable != null && connectionTable.Rows.Count != 0)
                {
                    server = connectionTable.Rows[0]["server"].ToString();
                    userid = connectionTable.Rows[0]["username"].ToString();
                    password = connectionTable.Rows[0]["password"].ToString();
                    database = connectionTable.Rows[0]["database"].ToString();
                }

                return getdb_branch(SQL, server, userid, password, database);
            }
            else
                return getdb(SQL);
        }

        public Boolean setdb_main(string SQL)
        {
            if (cls_globalvariables.BranchCode != cls_globalvariables.MainBranchCode)
            {
                string server = "";
                string userid = "";
                string password = "";
                string database = "";
                string sql = @"SELECT
                            `sqlserver` AS `server`,
                            `sqldatabase` AS `database`,
                            `sqlusername` AS `username`,
                            `sqlpassword` AS `password`
                           FROM `branch`
                           WHERE `wid` = 10 LIMIT 1";
                DataTable connectionTable = getdb(sql);
                if (connectionTable != null && connectionTable.Rows.Count != 0)
                {
                    server = connectionTable.Rows[0]["server"].ToString();
                    userid = connectionTable.Rows[0]["username"].ToString();
                    password = connectionTable.Rows[0]["password"].ToString();
                    database = connectionTable.Rows[0]["database"].ToString();
                }

                return setdb_branch(SQL, server, userid, password, database);
            }
            else
                return setdb(SQL);
        }

        public decimal get_totalcashcollect()
        {
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            string sSQL = @"SELECT COALESCE(SUM(`amount`),0) AS 'totalcash'
                            FROM
                            (
	                            SELECT C.* FROM `saleshead` AS H,
	                            (
		                            SELECT S.`saleswid`, SUM(D.`amount`) AS 'amount' 
		                            FROM `collectionhead` AS H, `collectiondetail` AS D, `collectionsales` AS S
		                            WHERE H.`wid` = D.`headid` AND H.`wid` = S.`headid` 
			                            AND H.`show` = 1 AND H.`status` = 1
			                            AND D.`method` = 1
			                            AND CAST(H.`collectiondate` AS DATE) = CAST(NOW() AS DATE)
		                            GROUP BY S.`saleswid`
	                            ) AS C
	                            WHERE C.`saleswid` = H.`wid` AND 
		                            H.`branchid` = " + branchid + @" AND
		                            `terminalno` = " + terminalno + @" AND 
		                            CAST(`date` AS DATE) = CAST(NOW() AS DATE)
		                            AND `type` = 3 AND `status` = 1 AND `show` = 1
                            ) A";
            DataTable dt = getdb(sSQL);
            return Convert.ToDecimal(dt.Rows[0]["amount"]);
        }

        public string get_nexttransactionno()
        {
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = string.Format("{0:00}", cls_globalvariables.terminalno_v);

            string sSQLtransno = @"SELECT COALESCE(MAX(`transactionno`),0) AS 'transactionno' FROM saleshead
                                WHERE `terminalno` = " + terminalno + @"
	                                AND `branchid` = " + branchid;

            DataTable dt = getdb(sSQLtransno);
            string next_transactionno = terminalno.ToString() + "0000001";

            Int64 maxtransno = Convert.ToInt64(dt.Rows[0]["transactionno"]);

            if ((dt.Rows.Count > 0) && (maxtransno > 0))
            {
                next_transactionno = (Convert.ToInt64(dt.Rows[0]["transactionno"]) + 1).ToString();
            }

            return next_transactionno;
        }

        public string get_nextornumber()
        {
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            string sSQLtransno = @"SELECT COALESCE(MAX(`ornumber`),0) AS 'ornumber' FROM saleshead
                                WHERE `terminalno` = " + terminalno + @"
                                    AND `branchid` = " + branchid;

            //Console.WriteLine(sSQLtransno);
            DataTable dt = getdb(sSQLtransno);
            string next_ornumber = branchid.ToString() + terminalno.ToString() + "0000001";

            Int64 maxor = Convert.ToInt64(dt.Rows[0]["ornumber"]);
            if ((dt.Rows.Count > 0) && (maxor > 0))
            {
                next_ornumber = (maxor + 1).ToString();
            }

            return next_ornumber;
        }

        public int create_invoice(cls_POSTransaction trans)
        {
            string datetime_d = trans.getdatetime().ToString("yyyy-MM-dd HH:mm:ss");
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            int userwid = trans.getclerk().getwid();

            string next_transactionno = get_nexttransactionno();
            string next_ornumber = get_nextornumber();

            int next_wid = get_next_wid_withlock("saleshead");
            string sSQL = @"UPDATE `saleshead` SET
                                `branchid` = '" + branchid + @"', 
                                `type` = '3', 
                                `date` = '" + datetime_d + @"', 
                                `userid` = '" + userwid + @"', 
                                `lastmodifiedby` = '" + userwid + @"', 
                                `lastmodifieddate` = NOW(), 
                                `datecreated` = NOW(),  
                                `transactionno` = '" + next_transactionno + @"', 
                                `ornumber` = '" + next_ornumber + @"',  
                                `terminalno` = '" + terminalno + @"'
                            WHERE `wid` = '" + next_wid + @"'";
            setdb(sSQL);

            trans.setORnumber(next_ornumber);
            trans.settransactionno(next_transactionno);
            trans.setWid(next_wid);
            return 0;
        }

        public void update_synctable(string table_name, int wid)
        {
            if (cls_globalvariables.BranchCode == "10")
                return;

            //delete old data if exists
            string sSQL = "DELETE FROM `sync` WHERE `tablename` = '" + table_name + "' AND `wid` = " + wid;
            setdb(sSQL);

            string sSQLInsert = @"INSERT INTO `sync` 
						    (`tablename`, `wid`, `branchid`) 
						    VALUES ('" + table_name + "', " + wid + ", 10)";
            setdb(sSQLInsert);
        }

        public List<string> update_synctable_liststring(string table_name, string wid)
        {
            List<string> tempStringList = new List<string>();
            if (cls_globalvariables.BranchCode == "10")
                return tempStringList;
            //delete old data if exists
            string sSQL = "DELETE FROM `sync` WHERE `tablename` = '" + table_name + "' AND `wid` = " + wid;
            tempStringList.Add(sSQL);

            string sSQLInsert = @"INSERT INTO `sync` 
						    (`tablename`, `wid`, `branchid`) 
						    VALUES ('" + table_name + "', " + wid + ", 10)";
            tempStringList.Add(sSQLInsert);
            return tempStringList;
        }

        public int save_transaction(cls_POSTransaction trans)
        {
            string tbl_saleshead = "saleshead";
            string tbl_salesdetail = "salesdetail";
            string tbl_collectionhead = "collectionhead";
            string tbl_collectiondetail = "collectiondetail";
            string tbl_collectionsales = "collectionsales";
            string tbl_poscardpayment = "poscardpayment";

            string datetime_d = trans.getdatetime().ToString("yyyy-MM-dd HH:mm:ss");
            string branchid = cls_globalvariables.BranchCode;
            int salesheadwid = trans.getWid();
            int customerid = trans.getcustomer().getwid();
            string customername = trans.getcustomer().getfullname();
            decimal adjust = trans.getadjust();
            string seniorno = trans.getsenior().get_idnumber();
            string seniorname = trans.getsenior().get_fullname();
            int userid = trans.getclerk().getwid();
            int memberid = trans.getmember().getwid();
            int checkerid = trans.getchecker().getwid();
            decimal totalamt = trans.get_productlist().get_totalamount();
            bool iswholesale = trans.get_productlist().get_iswholesale();
            bool isnonvat = trans.get_productlist().get_isnonvat();
            decimal discount = trans.getdiscount();
            decimal totalpaidamt = trans.getpayments().get_totalamount();

            decimal t = trans.get_productlist().get_totalamount();
            decimal t2 = trans.get_productlist().get_totalamount_no_head_discount();
            decimal headDiscountPercentage = (t2 == 0) ? 0 : (t / t2);

            decimal cash = trans.getpayments().get_cash();
            decimal mem_points = trans.getpayments().get_points();
            decimal change = trans.get_changeamount();
            List<cls_cardinfo> creditcards = trans.getpayments().get_creditcard();
            List<cls_cardinfo> debitcards = trans.getpayments().get_debitcard();
            List<cls_giftcheque> giftchequesnew = trans.getpayments().get_giftchequenew();
            List<cls_CustomPaymentsInfo> custompaymentsinfo = trans.getpayments().get_custompayments();
            string sSQLcd = "";
            List<string> tempStringList = new List<string>();

            //MEMBER (Priority since it will be run on main branch)
            if (trans.getmember().getwid() != 0)
            {
                List<string> memberTransactionListString = new List<string>();
                if (mem_points != 0)
                {
                    tempStringList = get_next_wid_withlock_main_liststring("memberpointtrans");
                    foreach (string str in tempStringList)
                        memberTransactionListString.Add(str);
                    string sSQLmemberpoint_d = @"UPDATE `memberpointtrans` SET
                                            `memberid` = " + trans.getmember().getwid() + @",
                                            `branchid` = " + branchid + @",
                                            `type` = 3,
                                            `referencewid` = " + salesheadwid.ToString() + @",
                                            `amount` = " + mem_points + @",
                                            `status` = 1,
                                            `date` = NOW(),
                                            `show` = 1,
                                            `datecreated` = NOW(),
                                            `lastmodifieddate` = NOW(),
                                            `userid` = " + userid + @",
                                            `lastmodifiedby` = " + userid + @"
                                           WHERE `wid` = @wid_d;";
                    memberTransactionListString.Add(sSQLmemberpoint_d);
                }
                if (trans.getmember().getwid() != 0)
                {
                    decimal point_earn = trans.get_memberpoint_earn();
                    List<string> temp = get_next_wid_withlock_main_liststring("memberpointtrans");
                    foreach (string str in temp)
                        memberTransactionListString.Add(str);
                    string sSQLmemberpoint = @"UPDATE `memberpointtrans` SET
                                            `memberid` = " + trans.getmember().getwid() + @",
                                            `branchid` = " + branchid + @",
                                            `type` = 1,
                                            `referencewid` = " + salesheadwid.ToString() + @",
                                            `amount` = " + point_earn.ToString("N2") + @",
                                            `status` = 1,
                                            `date` = NOW(),
                                            `show` = 1,
                                            `datecreated` = NOW(),
                                            `lastmodifieddate` = NOW(),
                                            `userid` = " + userid + @",
                                            `lastmodifiedby` = " + userid + @"
                                           WHERE `wid` = @wid_d;";
                    memberTransactionListString.Add(sSQLmemberpoint);
                }
                //'select' is purposely lower cased for exec_trans method
                memberTransactionListString.Add(@"select 'SUCCESS';");
                if (!mySQLFunc.check_connection_main() || exec_trans_main(memberTransactionListString, 3) != "SUCCESS")
                    return 1;
            }

            string discquery = @"";
            List<string> transactionQueryList = new List<string>();
            string sSQL = "";

            sSQL = @"UPDATE `" + tbl_saleshead + @"` SET
                        `salesman` = " + trans.getsalesman().getwid().ToString() + @",
                        `status` = 1, 
                        `customerid` = " + customerid + @", 
                        `customername` = '" + escapeString(customername) + @"',
                        `date` = NOW(),
                        `adjust` = " + adjust + @", 
                        `discount1` = " + discount + @",
                        `seniorno` = '" + seniorno + @"', 
                        `seniorname` = '" + escapeString(seniorname) + @"',
                        `ornumber` = '" + trans.getORnumber() + @"',
                        `transactionno` = '" + trans.gettransactionno() + @"',
                        `userid` = '" + userid + @"',
                        `branchid` = '" + branchid + @"',
                        `type` = 3,                        
                        `lastmodifiedby` = " + userid + @", 
                        `lastmodifieddate` = NOW(), 
                        `istransfer` = 0, 
                        `show` = 1, 
                        `memberid` = " + memberid + @", 
                        `checkerid` = " + checkerid + @",
                        `iswholesale` = " + iswholesale + @",
                        `isnonvat` = " +  isnonvat.ToString() + @"
                        WHERE `wid` = " + salesheadwid;

            //Console.WriteLine(sSQL);
            //setdb(sSQL);
            transactionQueryList.Add(sSQL);

            foreach (cls_product prod in trans.get_productlist().get_productlist())
            {
                string qty = prod.getQty().ToString("G29");
                string price = (prod.getPrice()).ToString();
                string vat = prod.getVat().ToString();

                cls_user soldby = (cls_user)prod.getSoldBy();
                int soldbywid = 0;
                try { soldbywid = soldby.getwid(); }
                catch { soldbywid = userid; }

                decimal addbackqty = (prod.getQty() < 0) ? -1 * prod.getQty() : 0;
                decimal addbackbigqty = (prod.getQty() < 0) ? prod.getBigQty() : 1;

                List<string> temp = get_next_wid_withlock_liststring(tbl_salesdetail);
                foreach (string str in temp)
                    transactionQueryList.Add(str);
                transactionQueryList.Add("SET @salesdetailwid := @wid_d");
                int issenior = (trans.getsenior().get_idnumber().Length >= 1 && prod.getIsSenior() != 0) ? prod.getIsSenior() : 0;
                string sSQLdetail = @"UPDATE `" + tbl_salesdetail + @"` SET
                                `headid` = '" + salesheadwid + @"', 
                                `productid` = '" + prod.getWid().ToString() + @"',  
                                `quantity` = '" + qty + @"',  
                                `bigquantity` = '" + prod.getBigQty() + @"',  
                                `oprice` = '" + prod.getOrigPrice().ToString() + @"',  
                                `discount1` = '" + prod.getDiscount().ToString() + @"', 
                                `price` = '" + price + @"',  
                                `pprice` = '" + prod.getPurchasePrice().ToString() + @"', 
                                `vat` = '" + vat + @"', 
                                `senior` = " + issenior + @", 
                                `soldby` = '" + soldbywid + @"',  
                                `addbackqty` = '" + addbackqty.ToString() + @"',  
                                `addbackbigqty` = '" + addbackbigqty.ToString() + @"',
                                `description` = '" + prod.getMemo() + @"'
                            WHERE `wid` = @salesdetailwid";
                //Console.WriteLine(sSQLdetail);
                //setdb(sSQLdetail);
                transactionQueryList.Add(sSQLdetail);
                discquery = "";
                List<cls_discount> detailDiscounts = prod.getProductDiscountList().get_discount_list();
                foreach (cls_discount disc in detailDiscounts)
                {
                    if (disc.get_status())
                    {
                        discquery += @" ,(@salesdetailwid," + disc.get_type() + "," + disc.get_basis() + "," +
                                            disc.get_value() + "," + disc.get_ismultiple() + "," + disc.get_discounted_amount() + "," + disc.get_wid() + ") ";
                    }
                }

                //insert detail discounts
                if (discquery.Length > 0)
                {
                    transactionQueryList.Add(@"INSERT INTO `salesdetaildiscounts`
                            (`salesdetailwid`,`type`,`basis`,`value`,`ismultiple`,`amount`, `discountwid`)
                            VALUES " + discquery.Substring(2));
                }
            }

            //head discounts
            List<cls_discount> headDiscounts = trans.get_productlist().getTransDisc().get_discount_list();
            discquery = @"";
            foreach (cls_discount disc in headDiscounts)
            {
                if (disc.get_status())
                {
                    discquery += @" ,(" + salesheadwid + "," + disc.get_type() + "," + disc.get_basis() + "," +
                                        disc.get_value() + "," + disc.get_ismultiple() + "," + disc.get_discounted_amount() + "," + disc.get_wid() + ") ";
                }
            }
            if (discquery.Length > 0)
            {
                transactionQueryList.Add(@"INSERT INTO `salesheaddiscounts`
                            (`salesheadwid`,`type`,`basis`,`value`,`ismultiple`,`amount`, `discountwid`)
                            VALUES " + discquery.Substring(2));
            }

            tempStringList = get_next_wid_withlock_liststring(tbl_collectionhead);
            foreach (string str in tempStringList)
                transactionQueryList.Add(str);
            transactionQueryList.Add(@"SET @collectionheadwid := @wid_d");
            string sSQLch = @"UPDATE `" + tbl_collectionhead + @"` SET
                                `customerid` = " + customerid + @", 
                                `collectiondate` = NOW(), 
                                `userid` = " + userid + @",  
                                `status` = 1,
                                `branchid` = " + branchid + @", 
                                `lastmodifieddate` = NOW(), 
                                `lastmodifiedby` = " + userid + @", 
                                `istransfer` = 0, 
                                `datecreated` = NOW(),
                                `memo` = '" + trans.getpayments().get_memo() + @"',
                                `show` = 1
                            WHERE `wid` = @collectionheadwid";

            //Console.WriteLine(sSQLch);
            //setdb(sSQLch);
            transactionQueryList.Add(sSQLch);

            string sSQLcs = @"INSERT INTO `" + tbl_collectionsales + @"`
                            (`headid`, `saleswid`, `amount`)
                            VALUES
                            ( @collectionheadwid, " + salesheadwid + ", " + (totalpaidamt - trans.get_changeamount()) + ")";
            //Console.WriteLine(sSQLcs);
            //setdb(sSQLcs);
            transactionQueryList.Add(sSQLcs);

            if (cash != 0)
            {
                tempStringList = get_next_wid_withlock_liststring(tbl_collectiondetail);
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = 1, 
                                `amount` = " + cash + @"
                           WHERE `wid` = @wid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            if (change > 0)
            {
                tempStringList = get_next_wid_withlock_liststring("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = 1, 
                                `amount` = -" + change + @"
                           WHERE `wid` = @wid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_cardinfo creditcard in creditcards)
            {
                tempStringList = get_next_wid_withlock_liststring("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = 5, 
                                `amount` = " + creditcard.getamount() + @"
                           WHERE `wid` = @wid_d";

                //Console.WriteLine(sSQLcd);
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
                transactionQueryList.Add("SET @collectiondetailwid = @wid_d");

                tempStringList = get_next_wid_withlock_liststring(tbl_poscardpayment);
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_poscardpayment + @"` SET
                            `collectiondetailid` = @collectiondetailwid, 
                            `cardsettingwid` = '" + cls_globalfunc.getCreditDebiCardInfo(creditcard.getcardno()) + @"', 
                            `cardno` = '" + creditcard.getcardno() + @"', 
                            `fullname` = '" + escapeString(creditcard.getname()) + @"',
                            `expdate` = '" + creditcard.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `type` = '0', 
                            `approvalcode` = '" + escapeString(creditcard.getapprovalcode()) + @"',
                            `amount` = '" + creditcard.getamount() + @"'
                           WHERE `wid` = @wid_d";
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_cardinfo debitcard in debitcards)
            {
                tempStringList = get_next_wid_withlock_liststring("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = 6, 
                                `amount` = " + debitcard.getamount() + @"
                           WHERE `wid` = @wid_d";

                //Console.WriteLine(sSQLcd);
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
                transactionQueryList.Add("SET @collectiondetailwid = @wid_d");

                tempStringList = get_next_wid_withlock_liststring(tbl_poscardpayment);
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_poscardpayment + @"` SET
                            `collectiondetailid` = @collectiondetailwid, 
                            `cardno` = '" + debitcard.getcardno() + @"', 
                            `fullname` = '" + escapeString(debitcard.getname()) + @"',
                            `expdate` = '" + debitcard.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `type` = '1', 
                            `approvalcode` = '" + escapeString(debitcard.getapprovalcode()) + @"',
                            `amount` = '" + debitcard.getamount() + @"'
                           WHERE `wid` = @wid_d";
                //Console.WriteLine(sSQLcd);
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_giftcheque giftchequenew in giftchequesnew)
            {
                tempStringList = get_next_wid_withlock_liststring("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = 13,
                                `amount` = " + giftchequenew.getamount() + @"
                           WHERE `wid` = @wid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
                transactionQueryList.Add("SET @collectiondetailwid = @wid_d");

                tempStringList = get_next_wid_withlock_liststring("posgiftchequepayment");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `posgiftchequepayment` SET
                            `collectiondetailid` = @collectiondetailwid, 
                            `giftchequeno` = '" + giftchequenew.get_referenceno() + @"', 
                            `expdate` = '" + giftchequenew.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `memo` = '" + escapeString(giftchequenew.get_memo()) + @"', 
                            `amount` = '" + giftchequenew.getamount() + @"'
                           WHERE `wid` = @wid_d";
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_CustomPaymentsInfo custompayment in custompaymentsinfo)
            {
                tempStringList = get_next_wid_withlock_liststring("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = " + custompayment.get_paymentwid() + @", 
                                `amount` = " + custompayment.get_amount() + @"
                           WHERE `wid` = @wid_d";
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);

                string field1info = custompayment.get_field1info();
                string field2info = custompayment.get_field2info();
                string field3info = custompayment.get_field3info();
                string field4info = custompayment.get_field4info();
                string field5info = custompayment.get_field5info();
                string field6info = custompayment.get_field6info();

                if (field1info.Length > 0 || field2info.Length > 0 || field3info.Length > 0 ||
                    field4info.Length > 0 || field5info.Length > 0 || field6info.Length > 0)
                {
                    sSQLcs = @"INSERT INTO `poscustompayments`
                            (`detailid`,`field1`,`field2`,`field3`,`field4`,`field5`,`field6`)
                            VALUES
                            ( @wid_d, '" + escapeString(field1info) + @"',
                                '" + escapeString(field2info) + @"','" + escapeString(field3info) + @"',
                                '" + escapeString(field4info) + @"','" + escapeString(field5info) + @"',
                                '" + escapeString(field6info) + @"')";
                    //setdb(sSQLcs);
                    transactionQueryList.Add(sSQLcs);
                }
            }

            if (mem_points != 0)
            {
                tempStringList = get_next_wid_withlock_liststring("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = @collectionheadwid,
                                `method` = 8, 
                                `amount` = " + mem_points + @"
                           WHERE `wid` = @wid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }
            tempStringList = update_synctable_liststring(tbl_saleshead, salesheadwid.ToString());
            foreach (string str in tempStringList)
                transactionQueryList.Add(str);

            tempStringList = update_synctable_liststring(tbl_collectionhead, "@collectionheadwid");
            foreach (string str in tempStringList)
                transactionQueryList.Add(str);

            transactionQueryList.Add("select 'SUCCESS'");

            string returnVal = exec_trans(transactionQueryList, 5);
            LOGS.LOG_PRINT_CSV(trans, returnVal);
            if (returnVal != "SUCCESS")
                return -1;
            return 0;
        }

        public void WriteToErrorLog(string msg)
        {
            if (!(System.IO.Directory.Exists(Application.StartupPath + "\\Errors\\")))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Errors\\");
            }

            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\Errors\\errlog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter s = new StreamWriter(fs);
                s.Close();
                fs.Close();
            }
            catch (Exception) { }

            try
            {
                FileStream fs1 = new FileStream(Application.StartupPath + "\\Errors\\errlog.txt", FileMode.Append, FileAccess.Write);
                StreamWriter s1 = new StreamWriter(fs1);
                s1.WriteLine(msg);
                s1.Close();
                fs1.Close();
            }
            catch (Exception) { }
        }

        public string exec_trans(List<string> SQL)
        {
            return exec_trans(SQL, 20);
        }
        public string exec_trans(List<string> SQL, int retryMax)
        {
            return exec_trans_branch(SQL, cls_globalvariables.server_v, cls_globalvariables.userid_v, cls_globalvariables.password_v, cls_globalvariables.database_v, retryMax);
        }
        public string exec_trans_branch(List<string> SQL, string server, string userid, string password, string database)
        {
            return exec_trans_branch(SQL, server, userid, password, database, 20);
        }
        public string exec_trans_branch(List<string> SQL, string server, string userid, string password, string database, int retryMax)
        {
            string val = "";
            string sqlquery = "";
            for (int retryno = 0; retryno < retryMax; retryno++)
            {
                string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database + ";Allow Zero Datetime=true;Charset = utf8; Connect Timeout=300;Allow User Variables=True;";


                MySqlConnection myconn = null;
                MySqlTransaction tr = null;

                try
                {
                    myconn = new MySqlConnection(cs);
                    myconn.Open();
                    tr = myconn.BeginTransaction();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandTimeout = 300;
                    cmd.Connection = myconn;
                    cmd.Transaction = tr;

                    foreach (string query in SQL)
                    {
                        sqlquery = query;
                        cmd.CommandText = query;

                        if (query.Trim().IndexOf("select") == 0)
                        {
                            val = cmd.ExecuteScalar().ToString();
                        }
                        else if (query.Trim().ToLower().IndexOf("insert") == 0)
                        {
                            if (cmd.ExecuteNonQuery() <= 0)
                                throw new Exception();
                        }
                        else
                            cmd.ExecuteNonQuery();
                    }
                    tr.Commit();
                    return val;

                }
                // Notice difference in errorlog
                catch (MySqlException ex)
                {
                    try
                    {
                        tr.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        WriteToErrorLog(" \n " + "Unable to do rollback. error: \n" + ex1.ToString() + " \n ");
                    }

                    WriteToErrorLog(" \n " + "Roll back sucessful" + " \n ");

                    string nw = DateTime.Now.ToString();
                    string terminalno = cls_globalvariables.terminalno_v.ToString();
                    string errcode = ex.ToString();
                    WriteToErrorLog(" \n Date: " + nw + " - Terminalno: " + terminalno + " - Retry #: " + retryno + " \n MySqlException: \n " + errcode + " \n Query: \n " + sqlquery + " \n ");
                    System.Threading.Thread.Sleep(500);

                }
                catch (Exception e)
                {
                    try
                    {
                        tr.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        WriteToErrorLog(" \n " + "Unable to do rollback. error: \n" + ex1.ToString() + " \n ");
                    }

                    WriteToErrorLog(" \n " + "Roll back sucessful" + " \n ");

                    string nw = DateTime.Now.ToString();
                    string terminalno = cls_globalvariables.terminalno_v.ToString();
                    string errcode = e.ToString();
                    WriteToErrorLog(" \n Date: " + nw + " - Terminalno: " + terminalno + " - Retry #: " + retryno + " \n Exception: \n " + errcode + " \n Query: \n " + sqlquery + " \n ");
                    System.Threading.Thread.Sleep(500);

                }
                finally
                {
                    if (myconn != null)
                    {
                        myconn.Close();
                    }
                }

            }
            WriteToErrorLog(" \n Date: " + DateTime.Now.ToString() + " - Terminalno: " + cls_globalvariables.terminalno_v.ToString() + " \n Exception: \n Unable to execute query after many tries. \n Query: \n " + SQL + " \n ");
            return "";
        }
        public string exec_trans_main(List<string> SQL)
        {
            return exec_trans_main(SQL, 20);
        }
        public string exec_trans_main(List<string> SQL, int retryMax)
        {
            if (cls_globalvariables.BranchCode != cls_globalvariables.MainBranchCode)
            {
                string server = "";
                string userid = "";
                string password = "";
                string database = "";
                string sql = @"SELECT
                            `sqlserver` AS `server`,
                            `sqldatabase` AS `database`,
                            `sqlusername` AS `username`,
                            `sqlpassword` AS `password`
                           FROM `branch`
                           WHERE `wid` = 10 LIMIT 1";
                DataTable connectionTable = getdb(sql);
                if (connectionTable != null && connectionTable.Rows.Count != 0)
                {
                    server = connectionTable.Rows[0]["server"].ToString();
                    userid = connectionTable.Rows[0]["username"].ToString();
                    password = connectionTable.Rows[0]["password"].ToString();
                    database = connectionTable.Rows[0]["database"].ToString();
                }

                return exec_trans_branch(SQL, server, userid, password, database, retryMax);
            }
            else
            {
                return exec_trans(SQL);
            }
        }
        public string escapeString(string str)
        {
            return MySql.Data.MySqlClient.MySqlHelper.EscapeString(str);
        }

        public int get_next_wid_withlock(string tablename)
        {
            List<string> ls = new List<string>();
            ls.Add("SET @wid_d = 0;");
            ls.Add(@"SELECT coalesce(MAX(`wid`)," + cls_globalvariables.BranchCode + "0000000) INTO @wid_d FROM `" + tablename + @"` 
                                WHERE `wid` >= " + cls_globalvariables.BranchCode + @"0000000 
                                    AND `wid` <= " + cls_globalvariables.BranchCode + @"9999999 FOR UPDATE;");
            ls.Add(@"INSERT into `" + tablename + @"` (`wid`)
                             VALUES ( IF(@wid_d = 0,'" + cls_globalvariables.BranchCode + @"0000001',@wid_d+1) );");
            ls.Add(@"select IF(@wid_d = 0,'" + cls_globalvariables.BranchCode + @"0000001',@wid_d+1) AS 'wid';");
            return Convert.ToInt32(exec_trans(ls));
        }

        public int get_next_wid_withlock_main(string tablename)
        {
            List<string> ls = new List<string>();
            ls.Add("SET @wid_d = 0;");
            ls.Add(@"SELECT coalesce(MAX(`wid`)," + 10 + "0000000) INTO @wid_d FROM `" + tablename + @"` 
                                WHERE `wid` >= " + 10 + @"0000000 
                                    AND `wid` <= " + 10 + @"9999999 FOR UPDATE;");
            ls.Add(@"INSERT into `" + tablename + @"` (`wid`)
                             VALUES ( IF(@wid_d = 0,'" + 10 + @"0000001',@wid_d+1) );");
            ls.Add(@"select IF(@wid_d = 0,'" + 10 + @"0000001',@wid_d+1) AS 'wid';");
            return Convert.ToInt32(exec_trans_main(ls));
        }

        public List<string> get_next_wid_withlock_main_liststring(string tablename)
        {
            List<string> ls = new List<string>();
            ls.Add("SET @wid_d = 0;");
            ls.Add(@"SELECT coalesce(MAX(`wid`)," + 10 + "0000000) INTO @wid_d FROM `" + tablename + @"` 
                                WHERE `wid` >= " + 10 + @"0000000 
                                    AND `wid` <= " + 10 + @"9999999 FOR UPDATE;");
            ls.Add(@"INSERT into `" + tablename + @"` (`wid`)
                             VALUES ( IF(@wid_d = 0,'" + 10 + @"0000001',@wid_d+1) );");
            ls.Add(@"SET @wid_d = IF(@wid_d = 0,'" + 10 + @"0000001', @wid_d + 1);");
            //ls.Add(@"select IF(@wid_d = 0,'" + 10 + @"0000001',@wid_d+1) AS 'wid';");
            return ls;
        }

        public List<string> get_next_wid_withlock_liststring(string tablename)
        {
            List<string> ls = new List<string>();
            ls.Add("SET @wid_d = 0;");
            ls.Add(@"SELECT coalesce(MAX(`wid`)," + cls_globalvariables.BranchCode + "0000000) INTO @wid_d FROM `" + tablename + @"` 
                                WHERE `wid` >= " + cls_globalvariables.BranchCode + @"0000000 
                                    AND `wid` <= " + cls_globalvariables.BranchCode + @"9999999 FOR UPDATE;");
            ls.Add(@"INSERT into `" + tablename + @"` (`wid`)
                             VALUES ( IF(@wid_d = 0,'" + cls_globalvariables.BranchCode + @"0000001',@wid_d+1) );");
            ls.Add(@"SET @wid_d = IF(@wid_d = 0,'" + cls_globalvariables.BranchCode + @"0000001', @wid_d + 1);");
            //ls.Add(@"select IF(@wid_d = 0,'" + cls_globalvariables.branchid_v + @"0000001',@wid_d+1) AS 'wid';");
            return ls;
        }
    }
}