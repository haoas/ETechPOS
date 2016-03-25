using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ETech.cls;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;
using ETECHPOS.Helpers;


namespace ETech
{
    class mySQLClass
    {
        public mySQLClass()
        {
            
        }

        private MySqlConnection connecttoSQL()
        {
            string server = cls_globalvariables.ConnectionSettings.Server;
            string userid = cls_globalvariables.ConnectionSettings.UserId;
            string password = cls_globalvariables.ConnectionSettings.Password;
            string database = cls_globalvariables.ConnectionSettings.Database;

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
                    string server = cls_globalvariables.ConnectionSettings.Server;
                    string userid = cls_globalvariables.ConnectionSettings.UserId;
                    string password = cls_globalvariables.ConnectionSettings.Password;
                    string database = cls_globalvariables.ConnectionSettings.Database;

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

            string server = cls_globalvariables.ConnectionSettings.Server;
            string userid = cls_globalvariables.ConnectionSettings.UserId;
            string password = cls_globalvariables.ConnectionSettings.Password;
            string database = cls_globalvariables.ConnectionSettings.Database;

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

            string server = cls_globalvariables.ConnectionSettings.Server;
            string userid = cls_globalvariables.ConnectionSettings.UserId;
            string password = cls_globalvariables.ConnectionSettings.Password;
            string database = cls_globalvariables.ConnectionSettings.Database;
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
                LogsHelper.Print("[getdb_branch] " + ex.Message.ToString());
                LogsHelper.Print("[getdb_branch(sql)] " + SQL);

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
                LogsHelper.Print("[setdb_branch] " + ex.Message.ToString());
                LogsHelper.Print("[setdb_branch(sql)] " + SQL);

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
                           WHERE `SyncId` = 10 LIMIT 1";
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
                           WHERE `SyncId` = 10 LIMIT 1";
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
		                            WHERE H.`SyncId` = D.`headid` AND H.`SyncId` = S.`headid` 
			                            AND H.`show` = 1 AND H.`status` = 1
			                            AND D.`method` = 1
			                            AND CAST(H.`collectiondate` AS DATE) = CAST(NOW() AS DATE)
		                            GROUP BY S.`saleswid`
	                            ) AS C
	                            WHERE C.`saleswid` = H.`SyncId` AND 
		                            H.`branchid` = " + branchid + @" AND
		                            `terminalno` = " + terminalno + @" AND 
		                            CAST(`date` AS DATE) = CAST(NOW() AS DATE)
		                            AND `status` = 1
                            ) A";
            DataTable dt = getdb(sSQL);
            return Convert.ToDecimal(dt.Rows[0]["amount"]);
        }

        public long get_nextornumber()
        {
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            string sSQLtransno = @"SELECT COALESCE(MAX(`ornumber`),0) AS 'ornumber' FROM saleshead
                                WHERE `terminalno` = " + terminalno + @"
                                    AND `branchid` = " + branchid;
            DataTable dt = getdb(sSQLtransno);

            return 1 + Convert.ToInt64(dt.Rows[0]["ornumber"]);
        }

        public int create_invoice(cls_POSTransaction trans)
        {
            string datetime_d = trans.getdatetime().ToString("yyyy-MM-dd HH:mm:ss");
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            long userwid = trans.getclerk().getsyncid();

            long next_ornumber = get_nextornumber();

            long next_SyncId = GetAndInsertNextSyncId("saleshead");
            string sSQL = @"UPDATE `saleshead` SET
                                `branchid` = '" + branchid + @"', 
                                `date` = '" + datetime_d + @"', 
                                `userid` = '" + userwid + @"', 
                                `lastmodifiedby` = '" + userwid + @"', 
                                `lastmodifieddate` = NOW(), 
                                `datecreated` = NOW(),  
                                `ornumber` = '" + next_ornumber + @"',  
                                `terminalno` = '" + terminalno + @"'
                            WHERE `SyncId` = '" + next_SyncId + @"'";
            setdb(sSQL);

            trans.setORnumber(next_ornumber);
            trans.setSyncId(next_SyncId);
            return 0;
        }

        public void update_synctable(string table_name, long SyncId)
        {
            if (cls_globalvariables.BranchCode == "10")
                return;

            //delete old data if exists
            string sSQL = "DELETE FROM `sync` WHERE `tablename` = '" + table_name + "' AND `SyncId` = " + SyncId;
            setdb(sSQL);

            string sSQLInsert = @"INSERT INTO `sync` 
						    (`tablename`, `SyncId`, `branchid`) 
						    VALUES ('" + table_name + "', " + SyncId + ", 10)";
            setdb(sSQLInsert);
        }

        public List<string> update_synctable_liststring(string table_name, string syncid)
        {
            List<string> tempStringList = new List<string>();
            if (cls_globalvariables.BranchCode == "10")
                return tempStringList;
            //delete old data if exists
            string sSQL = "DELETE FROM `sync` WHERE `tablename` = '" + table_name + "' AND `SyncId` = " + syncid;
            tempStringList.Add(sSQL);

            string sSQLInsert = @"INSERT INTO `sync` 
						    (`tablename`, `SyncId`, `branchid`) 
						    VALUES ('" + table_name + "', " + syncid + ", 10)";
            tempStringList.Add(sSQLInsert);
            return tempStringList;
        }

        public int save_transaction(cls_POSTransaction trans)
        {
            string datetime_d = trans.getdatetime().ToString("yyyy-MM-dd HH:mm:ss");
            string branchid = cls_globalvariables.BranchCode;
            long salesheadwid = trans.getSyncId();
            long customerid = trans.getcustomer().getwid();
            string customername = trans.getcustomer().getfullname();
            decimal adjust = trans.getadjust();
            string seniorno = trans.getsenior().get_idnumber();
            string seniorname = trans.getsenior().get_fullname();
            long userid = trans.getclerk().getsyncid();
            long memberid = trans.getmember().getSyncId();
            long checkerid = trans.getchecker().getsyncid();
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
            //              DO NOT DELETE
            //            //MEMBER (Priority since it will be run on main branch)
            //            if (trans.getmember().getwid() != 0)
            //            {
            //                List<string> memberTransactionListString = new List<string>();
            //                if (mem_points != 0)
            //                {
            //                    tempStringList = get_next_wid_withlock_main_liststring("memberpointtrans");
            //                    foreach (string str in tempStringList)
            //                        memberTransactionListString.Add(str);
            //                    string sSQLmemberpoint_d = @"UPDATE `memberpointtrans` SET
            //                                            `memberid` = " + trans.getmember().getwid() + @",
            //                                            `branchid` = " + branchid + @",
            //                                            `type` = 3,
            //                                            `referencewid` = " + salesheadwid.ToString() + @",
            //                                            `amount` = " + mem_points + @",
            //                                            `status` = 1,
            //                                            `date` = NOW(),
            //                                            `status` = 1,
            //                                            `datecreated` = NOW(),
            //                                            `lastmodifieddate` = NOW(),
            //                                            `userid` = " + userid + @",
            //                                            `lastmodifiedby` = " + userid + @"
            //                                           WHERE `SyncId` = @syncid_d;";
            //                    memberTransactionListString.Add(sSQLmemberpoint_d);
            //                }
            //                if (trans.getmember().getwid() != 0)
            //                {
            //                    decimal point_earn = trans.get_memberpoint_earn();
            //                    List<string> temp = get_next_wid_withlock_main_liststring("memberpointtrans");
            //                    foreach (string str in temp)
            //                        memberTransactionListString.Add(str);
            //                    string sSQLmemberpoint = @"UPDATE `memberpointtrans` SET
            //                                            `memberid` = " + trans.getmember().getwid() + @",
            //                                            `branchid` = " + branchid + @",
            //                                            `type` = 1,
            //                                            `referencewid` = " + salesheadwid.ToString() + @",
            //                                            `amount` = " + point_earn.ToString("N2") + @",
            //                                            `status` = 1,
            //                                            `date` = NOW(),
            //                                            `datecreated` = NOW(),
            //                                            `lastmodifieddate` = NOW(),
            //                                            `userid` = " + userid + @",
            //                                            `lastmodifiedby` = " + userid + @"
            //                                           WHERE `SyncId` = @syncid_d;";
            //                    memberTransactionListString.Add(sSQLmemberpoint);
            //                }
            //                //'select' is purposely lower cased for exec_trans method
            //                memberTransactionListString.Add(@"select 'SUCCESS';");
            //                if (!mySQLFunc.check_connection_main() || exec_trans_main(memberTransactionListString, 3) != "SUCCESS")
            //                    return 1;
            //            }

            string discquery = @"";
            List<string> transactionQueryList = new List<string>();
            string sSQL = "";

            sSQL = @"UPDATE `saleshead` SET
                        `salesman` = " + trans.getsalesman().getsyncid().ToString() + @",
                        `status` = 1, 
                        `customerid` = " + customerid + @", 
                        `customername` = '" + escapeString(customername) + @"',
                        `date` = NOW(),
                        `adjust` = " + adjust + @", 
                        `discount1` = " + discount + @",
                        `seniorno` = '" + seniorno + @"', 
                        `seniorname` = '" + escapeString(seniorname) + @"',
                        `ornumber` = '" + trans.getORnumber() + @"',
                        `userid` = '" + userid + @"',
                        `branchid` = '" + branchid + @"',                     
                        `lastmodifiedby` = " + userid + @", 
                        `lastmodifieddate` = NOW(), 
                        `memberid` = " + memberid + @", 
                        `checkerid` = " + checkerid + @",
                        `iswholesale` = " + iswholesale + @",
                        `isnonvat` = " + isnonvat.ToString() + @"
                        WHERE `SyncId` = " + salesheadwid;

            //Console.WriteLine(sSQL);
            //setdb(sSQL);
            transactionQueryList.Add(sSQL);

            foreach (cls_product prod in trans.get_productlist().get_productlist())
            {
                string qty = prod.getQty().ToString("G29");
                string price = (prod.getPrice()).ToString();
                string vat = prod.getVat().ToString();

                cls_user soldby = (cls_user)prod.getSoldBy();
                long soldbywid = 0;
                try { soldbywid = soldby.getsyncid(); }
                catch { soldbywid = userid; }

                List<string> temp = GetListStringAndInsertNextSyncId("salesdetail");
                foreach (string str in temp)
                    transactionQueryList.Add(str);
                transactionQueryList.Add("SET @salesdetailwid := @SyncId");
                int issenior = (trans.getsenior().get_idnumber().Length >= 1 && prod.getIsSenior() != 0) ? prod.getIsSenior() : 0;
                string sSQLdetail = @"UPDATE `salesdetail` SET
                                `headid` = '" + salesheadwid + @"', 
                                `productid` = '" + prod.getSyncId().ToString() + @"',  
                                `quantity` = '" + qty + @"',   
                                `oprice` = '" + prod.getOrigPrice().ToString() + @"',  
                                `regularDC` = '" + prod.getDiscount().ToString() + @"', 
                                `price` = '" + price + @"',  
                                `pprice` = '" + prod.getPurchasePrice().ToString() + @"', 
                                `vat` = '" + vat + @"',
                                `soldby` = '" + soldbywid + @"',  
                                `memo` = '" + prod.getMemo() + @"'
                            WHERE `SyncId` = @salesdetailwid";
                //Console.WriteLine(sSQLdetail);
                //setdb(sSQLdetail);
                transactionQueryList.Add(sSQLdetail);
            }

            tempStringList = GetListStringAndInsertNextSyncId("collectionhead");
            foreach (string str in tempStringList)
                transactionQueryList.Add(str);
            transactionQueryList.Add(@"SET @collectionheadwid := @syncid_d");
            string sSQLch = @"UPDATE `collectionhead` SET
                                `customerid` = " + customerid + @", 
                                `collectiondate` = NOW(), 
                                `userid` = " + userid + @",  
                                `status` = 1,
                                `branchid` = " + branchid + @", 
                                `lastmodifieddate` = NOW(), 
                                `lastmodifiedby` = " + userid + @", 
                                `datecreated` = NOW(),
                                `memo` = '" + trans.getpayments().get_memo() + @"',
                                `show` = 1
                            WHERE `SyncId` = @collectionheadwid";

            //Console.WriteLine(sSQLch);
            //setdb(sSQLch);
            transactionQueryList.Add(sSQLch);

            string sSQLcs = @"INSERT INTO `collectionsales`
                            (`headid`, `saleswid`, `amount`)
                            VALUES
                            ( @collectionheadwid, " + salesheadwid + ", " + (totalpaidamt - trans.get_changeamount()) + ")";
            //Console.WriteLine(sSQLcs);
            //setdb(sSQLcs);
            transactionQueryList.Add(sSQLcs);

            if (cash != 0)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = 1, 
                                `amount` = " + cash + @"
                           WHERE `SyncId` = @syncid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            if (change > 0)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = 1,
                                `amount` = -" + change + @"
                           WHERE `SyncId` = @syncid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_cardinfo creditcard in creditcards)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = 5, 
                                `amount` = " + creditcard.getamount() + @"
                           WHERE `SyncId` = @syncid_d";

                //Console.WriteLine(sSQLcd);
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
                transactionQueryList.Add("SET @collectiondetailwid = @syncid_d");

                tempStringList = GetListStringAndInsertNextSyncId("poscardpayment");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `poscardpayment` SET
                            `collectiondetailid` = @collectiondetailwid, 
                            `cardsettingwid` = '" + cls_globalfunc.getCreditDebiCardInfo(creditcard.getcardno()) + @"', 
                            `cardno` = '" + creditcard.getcardno() + @"', 
                            `fullname` = '" + escapeString(creditcard.getname()) + @"',
                            `expdate` = '" + creditcard.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `type` = '0', 
                            `approvalcode` = '" + escapeString(creditcard.getapprovalcode()) + @"',
                            `amount` = '" + creditcard.getamount() + @"'
                           WHERE `SyncId` = @syncid_d";
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_cardinfo debitcard in debitcards)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = 6, 
                                `amount` = " + debitcard.getamount() + @"
                           WHERE `SyncId` = @syncid_d";

                //Console.WriteLine(sSQLcd);
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
                transactionQueryList.Add("SET @collectiondetailwid = @syncid_d");

                tempStringList = GetListStringAndInsertNextSyncId("poscardpayment");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `poscardpayment` SET
                            `collectiondetailid` = @collectiondetailwid, 
                            `cardno` = '" + debitcard.getcardno() + @"', 
                            `fullname` = '" + escapeString(debitcard.getname()) + @"',
                            `expdate` = '" + debitcard.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `type` = '1', 
                            `approvalcode` = '" + escapeString(debitcard.getapprovalcode()) + @"',
                            `amount` = '" + debitcard.getamount() + @"'
                           WHERE `SyncId` = @syncid_d";
                //Console.WriteLine(sSQLcd);
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_giftcheque giftchequenew in giftchequesnew)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = 13,
                                `amount` = " + giftchequenew.getamount() + @"
                           WHERE `SyncId` = @syncid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
                transactionQueryList.Add("SET @collectiondetailwid = @syncid_d");

                tempStringList = GetListStringAndInsertNextSyncId("posgiftchequepayment");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `posgiftchequepayment` SET
                            `collectiondetailid` = @collectiondetailwid, 
                            `giftchequeno` = '" + giftchequenew.get_referenceno() + @"', 
                            `expdate` = '" + giftchequenew.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `memo` = '" + escapeString(giftchequenew.get_memo()) + @"', 
                            `amount` = '" + giftchequenew.getamount() + @"'
                           WHERE `SyncId` = @syncid_d";
                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }

            foreach (cls_CustomPaymentsInfo custompayment in custompaymentsinfo)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = " + custompayment.get_paymentwid() + @", 
                                `amount` = " + custompayment.get_amount() + @"
                           WHERE `SyncId` = @syncid_d";
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
                            ( @syncid_d, '" + escapeString(field1info) + @"',
                                '" + escapeString(field2info) + @"','" + escapeString(field3info) + @"',
                                '" + escapeString(field4info) + @"','" + escapeString(field5info) + @"',
                                '" + escapeString(field6info) + @"')";
                    //setdb(sSQLcs);
                    transactionQueryList.Add(sSQLcs);
                }
            }

            if (mem_points != 0)
            {
                tempStringList = GetListStringAndInsertNextSyncId("collectiondetail");
                foreach (string str in tempStringList)
                    transactionQueryList.Add(str);
                sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = @collectionheadwid,
                                `method` = 8, 
                                `amount` = " + mem_points + @"
                           WHERE `SyncId` = @syncid_d";

                //setdb(sSQLcd);
                transactionQueryList.Add(sSQLcd);
            }
            tempStringList = update_synctable_liststring("saleshead", salesheadwid.ToString());
            foreach (string str in tempStringList)
                transactionQueryList.Add(str);

            tempStringList = update_synctable_liststring("collectionhead", "@collectionheadwid");
            foreach (string str in tempStringList)
                transactionQueryList.Add(str);

            transactionQueryList.Add("select 'SUCCESS'");

            string returnVal = exec_trans(transactionQueryList, 5);
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
                FileStream fs = new FileStream(Application.StartupPath + "\\Errors\\ExcemptionErrors.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter s = new StreamWriter(fs);
                s.Close();
                fs.Close();
            }
            catch (Exception) { }

            try
            {
                FileStream fs1 = new FileStream(Application.StartupPath + "\\Errors\\ExcemptionErrors.txt", FileMode.Append, FileAccess.Write);
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
            return exec_trans_branch(SQL, cls_globalvariables.ConnectionSettings.Server, cls_globalvariables.ConnectionSettings.UserId, cls_globalvariables.ConnectionSettings.Password, cls_globalvariables.ConnectionSettings.Database, retryMax);
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
                           WHERE `Id` = 10 LIMIT 1";
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

        public long GetAndInsertNextSyncId(string Table)
        {
            //Format 1001 01 
            //1001 = BranchCode
            //  01 = Terminal
            //MAX = 9999-01-99999999999
            //MIN = 1001-01-00000000000
            string branchcode = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;
            string minimum = branchcode + terminalno + "000000000000";
            string first = branchcode + terminalno + "000000000001";
            string maximum = branchcode + terminalno + "999999999999";

            List<string> SQLS = new List<string>();
            SQLS.Add("SET @SyncId = 0;");
            SQLS.Add(@"SELECT COALESCE(MAX(`SyncId`)," + minimum + ") INTO @SyncId FROM `" + Table + @"` 
                       WHERE `SyncId` BETWEEN " + minimum + " AND " + maximum + " FOR UPDATE;");
            SQLS.Add(@"INSERT INTO `" + Table + @"` (`SyncId`) VALUES ( IF(@SyncId = 0," + first + @",@SyncId+1) );");
            SQLS.Add(@"select IF(@SyncId = 0," + first + @",@SyncId+1) AS 'SyncId';");
            return Convert.ToInt64(exec_trans(SQLS));
        }

        //Used For Member Needs to refactor this
        //        public List<string> get_next_wid_withlock_main_liststring(string tablename)
        //        {
        //            List<string> ls = new List<string>();
        //            ls.Add("SET @syncid_d = 0;");
        //            ls.Add(@"SELECT coalesce(MAX(`SyncId`)," + 10 + "0000000) INTO @syncid_d FROM `" + tablename + @"` 
        //                                        WHERE `SyncId` >= " + 10 + @"0000000 
        //                                            AND `SyncId` <= " + 10 + @"9999999 FOR UPDATE;");
        //            ls.Add(@"INSERT into `" + tablename + @"` (`SyncId`)
        //                                     VALUES ( IF(@syncid_d = 0,'" + 10 + @"0000001',@syncid_d+1) );");
        //            ls.Add(@"SET @syncid_d = IF(@syncid_d = 0,'" + 10 + @"0000001', @syncid_d + 1);");
        //            //ls.Add(@"select IF(@syncid_d = 0,'" + 10 + @"0000001',@syncid_d+1) AS 'syncid';");
        //            return ls;
        //        }

        public List<string> GetListStringAndInsertNextSyncId(string Table)
        {
            //Format 1001 01 
            //1001 = BranchCode
            //  01 = Terminal
            //MAX = 9999-01-99999999999
            //MIN = 1001-01-00000000000
            string branchcode = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;
            string minimum = branchcode + terminalno + "000000000000";
            string first = branchcode + terminalno + "000000000001";
            string maximum = branchcode + terminalno + "999999999999";

            List<string> SQLS = new List<string>();
            SQLS.Add("SET @SyncId = 0;");
            SQLS.Add(@"SELECT COALESCE(MAX(`SyncId`)," + minimum + ") INTO @SyncId FROM `" + Table + @"` 
                       WHERE `SyncId` BETWEEN " + minimum + " AND " + maximum + " FOR UPDATE;");
            SQLS.Add(@"INSERT INTO `" + Table + @"` (`SyncId`) VALUES ( IF(@SyncId = 0," + first + @",@SyncId + 1) );");
            SQLS.Add(@"SET @SyncId = IF(@SyncId = 0," + first + @", @SyncId + 1); ");

            return SQLS;
        }
    }
}