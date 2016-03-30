using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using ETech.cls;
using System.Net.NetworkInformation;
using ETech.Models.Global;
using ETech.Helpers;
using ETech.Variables;

namespace ETech
{
    public static class mySQLFunc
    {
        public static bool initialize_global_variables()
        {
            bool result = true;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BranchId", cls_globalvariables.Branch.Id);
            dictionary.Add("TerminalNumber", cls_globalvariables.ConnectionSettings.TerminalNumber);
            Settings newSettings = SettingsController.GetData(dictionary);
            if (newSettings == null ||
                newSettings.Count <= 0)
            {
                DialogHelper.ShowDialogWithPrintLogs(MessagesVariable.FailedLoadSettingsFromDatabase, "");
                result = false;
            }
            cls_globalvariables.Settings = newSettings;
            return result;
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
                    LogsHelper.WriteToExceptionLog(ex.ToString(), retryno, SQL);
                    System.Threading.Thread.Sleep(500);
                }
            }
            LogsHelper.WriteToExceptionLog("Unable to execute query after many tries.", SQL);
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
                LogsHelper.WriteToExceptionLog(ex.ToString(), SQL);
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
                LogsHelper.WriteToExceptionLog(ex.ToString(), SQL);
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
                LogsHelper.WriteToExceptionLog(ex.ToString(), SQL);
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
                LogsHelper.WriteToTLog("[getdb_branch] " + ex.Message.ToString());
                LogsHelper.WriteToTLog("[getdb_branch(sql)] " + SQL);

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
                LogsHelper.WriteToTLog("[setdb_branch] " + ex.Message.ToString());
                LogsHelper.WriteToTLog("[setdb_branch(sql)] " + SQL);

                Console.WriteLine("setdb_branch error: " + SQL);
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public static DataTable getdb_main(string SQL)
        {
            if (cls_globalvariables.Branch.Id != cls_globalvariables.MainBranchCode)
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
            if (cls_globalvariables.Branch.Id != cls_globalvariables.MainBranchCode)
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
            if (cls_globalvariables.Branch.Id != cls_globalvariables.MainBranchCode)
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
