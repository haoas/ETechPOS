﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using ETech.cls;
using System.Net.NetworkInformation;
using ETECHPOS.Models.Global;
using ETECHPOS.Helpers;

namespace ETech
{
    class mySQLFunc
    {
        public static bool initialize_global_variables()
        {
            try
            {
                string[] setting_lines = File.ReadAllLines(cls_globalvariables.settingspath);
                IEnumerable<string[]> setting_enu = setting_lines.Select(l => l.Split(new[] { '=' }, 2));
                var dic = setting_enu.ToDictionary(s => s[0].Trim(), s => s[1].Trim());
                if (!ConnectionSettingsController.TryGetData(out cls_globalvariables.ConnectionSettings))
                    return false;
                cls_globalvariables.terminalno_v = dic["terminal"];
                cls_globalvariables.com_v = dic["com"];
                cls_globalvariables.disp1_v = dic["disp1"];
                cls_globalvariables.disp2_v = dic["disp2"];
                cls_globalvariables.colortheme_v = dic["colortheme"];
                cls_globalvariables.BranchCode = dic["branchcode"];
                cls_globalvariables.BusinessName_v = dic["BusinessName"];
                cls_globalvariables.Owner_v = dic["Owner"];
                cls_globalvariables.TIN_v = dic["TIN"];
                cls_globalvariables.Address_v = dic["Address"];
                cls_globalvariables.PermitNo_v = dic["PermitNo"];
                cls_globalvariables.ACC_v = dic["ACC"];
                cls_globalvariables.Serial_v = dic["Serial"];
                cls_globalvariables.MIN_v = dic["MIN"];
                cls_globalvariables.PosProviderName_v = dic["PosProviderName"];
                cls_globalvariables.PosProviderAddress_v = dic["PosProviderAddress"];
                cls_globalvariables.PosProviderTIN_v = dic["PosProviderTIN"];
                try { cls_globalvariables.qty_places = int.Parse(dic["qty_places"]); }
                catch { cls_globalvariables.qty_places = 2; }
                try { cls_globalvariables.print_receipt_buffer = int.Parse(dic["print_receipt_buffer"]); }
                catch { cls_globalvariables.print_receipt_buffer = 0; }
                try { cls_globalvariables.print_receipt_actual = int.Parse(dic["print_receipt_actual"]); }
                catch { cls_globalvariables.print_receipt_actual = 0; }
                try { cls_globalvariables.print_receipt_limit = int.Parse(dic["print_receipt_limit"]); }
                catch { cls_globalvariables.print_receipt_limit = 0; }
                try { cls_globalvariables.print_receipt_linespacing = int.Parse(dic["print_receipt_linespacing"]); }
                catch { cls_globalvariables.print_receipt_linespacing = 0; }

                cls_globalvariables.ACC_date_v = dic["ACC_date"];

                cls_globalvariables.orfooter1_v = dic["orfooter1"];
                cls_globalvariables.orfooter2_v = dic["orfooter2"];
                cls_globalvariables.orfooter3_v = dic["orfooter3"];
                cls_globalvariables.orfooter4_v = dic["orfooter4"];
                try { cls_globalvariables.posname_v = dic["posname"]; }
                catch { cls_globalvariables.posname_v = "ETECH POS SYSTEM"; }
                try { cls_globalvariables.avoidinvalidpprice_v = dic["avoidinvalidpprice"]; }
                catch { cls_globalvariables.avoidinvalidpprice_v = "0"; }
                try { cls_globalvariables.print_receipt_format_v = dic["print_receipt_format"]; }
                catch { cls_globalvariables.print_receipt_format_v = ""; }
                try { cls_globalvariables.allowZeroPrice_v = dic["allowzeroprice"]; }
                catch { cls_globalvariables.allowZeroPrice_v = "0"; }
                try { cls_globalvariables.grossmethod_v = dic["grossmethod"]; }
                catch { cls_globalvariables.grossmethod_v = "0"; }
                try { cls_globalvariables.showdetailCCinZRead_v = dic["showdetailCCinZRead"]; }
                catch { cls_globalvariables.showdetailCCinZRead_v = "0"; }
                try { cls_globalvariables.readDateRange_v = Convert.ToInt16(dic["readDateRange"]); }
                catch { cls_globalvariables.readDateRange_v = 1; }
                try { cls_globalvariables.prodsearchstyle_v = dic["prodsearchstyle"]; }
                catch { cls_globalvariables.prodsearchstyle_v = "0"; }
                try { cls_globalvariables.CustomerDisplayLength_v = int.Parse(dic["CustomerDisplayLength"]); }
                catch { cls_globalvariables.CustomerDisplayLength_v = 20; }

                try { cls_globalvariables.ORPrintCount_v = Convert.ToInt16(dic["ORPrintCount"]); }
                catch { cls_globalvariables.ORPrintCount_v = 1; }
                if (cls_globalvariables.ORPrintCount_v <= 1) cls_globalvariables.ORPrintCount_v = 1;

                try { cls_globalvariables.ServiceCharge_v = Convert.ToDecimal(dic["ServiceCharge"]); }
                catch { }
                try { cls_globalvariables.LocalTax_v = Convert.ToDecimal(dic["LocalTax"]); }
                catch { }

                try { cls_globalvariables.RefundMemo_v = dic["RefundMemo"]; }
                catch { cls_globalvariables.RefundMemo_v = "0"; }

                try { cls_globalvariables.DefaultPrinter_v = dic["DefaultPrinter"]; }
                catch { cls_globalvariables.DefaultPrinter_v = ""; }

                try { cls_globalvariables.PrinterODByte_v = dic["PrinterODByte"]; }
                catch { cls_globalvariables.PrinterODByte_v = ""; }
                if (cls_globalvariables.PrinterODByte_v == "")
                    cls_globalvariables.PrinterODByte_v = "27,112,0,25,250";

                try { cls_globalvariables.PreviewOR_v = (dic["PreviewOR"] == "1"); }
                catch { cls_globalvariables.PreviewOR_v = false; }

                try { cls_globalvariables.ads_url_v = dic["Ads_Url"]; }
                catch { cls_globalvariables.ads_url_v = ""; }

                try { cls_globalvariables.AutoShowKeyboard_v = (dic["AutoShowKeyboard"] == "1"); }
                catch { cls_globalvariables.AutoShowKeyboard_v = false; }

                try { cls_globalvariables.maximum_cash_collection_v = Convert.ToDouble(dic["MaximumCashCollection"]); }
                catch { cls_globalvariables.maximum_cash_collection_v = 0; }

                try { cls_globalvariables.POSMacAddress_v = dic["POSMacAddress"]; }
                catch { cls_globalvariables.POSMacAddress_v = ""; }

                try { cls_globalvariables.DiscountDetails_v = Convert.ToInt16(dic["DiscountDetails"]); }
                catch { cls_globalvariables.DiscountDetails_v = 1; }

                cls_globalvariables.starttime_v = 0;
                cls_globalvariables.endtime_v = 0;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialog("Error in settings \n" + ex.ToString());
                return false;
            }

            return true;
        }

        public static Boolean setdb(string SQL)
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

        public static DataTable getdb(string SQL, List<string> parameters, List<string> values)
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
            catch (MySqlException ex)
            {
                WriteToErrorLog(" \n Date: " + DateTime.Now + " \n Exception: \n " + ex.ToString() +
                  " \n getdb Query: \n " + SQL + " \n ");

                //MessageBox.Show("Error detected while connecting to database");
                return new DataTable();
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

        public static DataTable getdb(string SQL)
        {
            DataTable dataTable = new DataTable();

            string server = cls_globalvariables.ConnectionSettings.Server;
            string userid = cls_globalvariables.ConnectionSettings.UserId;
            string password = cls_globalvariables.ConnectionSettings.Password;
            string database = cls_globalvariables.ConnectionSettings.Database;
            string cs = @"server=" + server + ";userid=" + userid + ";password=" + password + ";database=" + database + ";Allow Zero Datetime=true";
            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(cs);
                myconn.Open();
            }
            catch (MySqlException ex)
            {
                WriteToErrorLog(" \n Date: " + DateTime.Now + " \n Exception: \n " + ex.ToString() +
                  " \n getdb Query: \n " + SQL + " \n ");

                //MessageBox.Show("Error detected while connecting to database");
                //MessageBox.Show(ex.ToString());
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

                //MessageBox.Show("Error detected while connecting to database");
                //MessageBox.Show(ex.ToString());
                dataTable = new DataTable();
            }

            myconn.Close();
            return dataTable;
        }

        public static DateTime DateTimeNow()
        {
            string SQLDateTime = "SELECT NOW() as 'datetime'";
            return Convert.ToDateTime(getdb(SQLDateTime).Rows[0]["datetime"]);
        }

        public static DataTable getdb_branch(string SQL, string server, string userid, string password, string database)
        {
            if (!check_connection_branch(server, userid, password, database))
                return new DataTable();

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

        public static Boolean setdb_branch(string SQL, string server, string userid, string password, string database)
        {
            if (!check_connection_branch(server, userid, password, database))
                return false;

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

        public static DataTable getdb_main(string SQL)
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

                return getdb_branch(SQL, server, userid, password, database);
            }
            else
                return getdb(SQL);
        }

        public static Boolean setdb_main(string SQL)
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

                return setdb_branch(SQL, server, userid, password, database);
            }
            else
                return setdb(SQL);
        }

        public static void WriteToErrorLog(string msg)
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

        public static bool check_connection()
        {
            try
            {
                string server = cls_globalvariables.ConnectionSettings.Server;
                string userid = cls_globalvariables.ConnectionSettings.UserId;
                string password = cls_globalvariables.ConnectionSettings.Password;
                string database = cls_globalvariables.ConnectionSettings.Database;
                string cs = @"server=" + server
                            + ";userid=" + userid
                            + ";password=" + password
                            + ";database=" + database
                            + ";Connect Timeout=300";
                MySqlConnection conn = new MySqlConnection(cs);
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool check_connection_branch(string server, string userid, string password, string database)
        {
            try
            {
                string cs = @"server=" + server
                            + ";userid=" + userid
                            + ";password=" + password
                            + ";database=" + database
                            + ";Connect Timeout=300";
                MySqlConnection conn = new MySqlConnection(cs);
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool check_connection_main()
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

                return check_connection_branch(server, userid, password, database);
            }
            else
                return check_connection();
        }

        public static string getServerDateTime()
        {
            string sql = @"Select NOW() as `now`";
            DataTable DT = mySQLFunc.getdb(sql);
            DateTime datetime = Convert.ToDateTime(DT.Rows[0]["now"].ToString());

            return datetime.ToString("yyyy-MM-dd hh tt");
        }
    }
}
