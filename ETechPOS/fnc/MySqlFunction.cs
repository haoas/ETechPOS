using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using ETech.Models.Global;
using ETech.cls;
using ETech.Helpers;
using ETech.Variables;

namespace ETech.fnc
{
    public static class MySqlFunction
    {
        private static ConnectionSettings _ConnectionSettings = cls_globalvariables.ConnectionSettings;

        private static string _ConnectionString = @"Server=" + _ConnectionSettings.Server + @";
                Userid=" + _ConnectionSettings.UserId + @";
                Password=" + _ConnectionSettings.Password + @";
                Database=" + _ConnectionSettings.Database + @";";

        public static bool HasConnection()
        {
            string connectionString = _ConnectionString + @"
                ConnectionTimeOut=30;";
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialogWithPrintLogs(MessagesVariable.FailedConnectDatabaseInMySql, ex.ToString());
                return false;
            }
        }
        public static MySqlConnection ConnectToSql()
        {
            string connectionString = _ConnectionString + @"
                CharSet=utf8;" + @"
                Allow User Variables=True;" + @"
                Convert Zero Datetime=True;" + @"
                ConnectionTimeOut=300;";
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                DialogHelper.ShowDialogWithPrintLogs(MessagesVariable.FailedConnectDatabaseInMySql, ex.ToString());
            }
            return connection;
        }

        public static DataTable GetDataTable(string sql)
        {
            return GetDataTableByTransaction(new List<string>(new string[] { sql }));
        }
        public static DataTable GetDataTable(SqlDetail sqlDetail)
        {
            SqlDetails sqlDetails = new SqlDetails();
            sqlDetails.Add(sqlDetail);
            return GetDataTableByTransaction(sqlDetails);
        }

        public static DataTable GetDataTableByTransaction(List<string> sqlList)
        {
            SqlDetails sqlDetails = new SqlDetails();
            foreach (string sql in sqlList)
                sqlDetails.Add(new SqlDetail(sql));
            return GetDataTableByTransaction(sqlDetails);
        }
        public static DataTable GetDataTableByTransaction(SqlDetails sqlDetails)
        {
            MySqlConnection connection = ConnectToSql();
            DataTable resultDt = null;
            if (connection == null)
                return resultDt;
            MySqlTransaction transaction = connection.BeginTransaction();
            resultDt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 300;
                foreach (SqlDetail sqlDetail in sqlDetails)
                {
                    cmd.CommandText = sqlDetail.Sql;
                    SqlParameters sqlParameters = sqlDetail.SqlParameters;
                    foreach (SqlParameter sqlParameter in sqlParameters)
                        cmd.Parameters.AddWithValue(sqlParameter.Field, sqlParameter.Value);
                    if (sqlDetail.Sql.Trim('\r', '\n', ' ').IndexOf("SELECT", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(resultDt);
                        }
                    }
                    else
                        cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                connection.Close();
                return resultDt;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                connection.Close();
                LogsHelper.Print(MessagesVariable.FailedFetchDataInMySql + "\n" + ex.ToString());
                return resultDt;
            }
        }

        public static DataTable GetDataTableByStoreProcedure(string storeProcedureName)
        {
            return GetDataTableByStoreProcedure(storeProcedureName, new SqlParameters());
        }
        public static DataTable GetDataTableByStoreProcedure(string storeProcedureName, SqlParameters sqlParameters)
        {
            DataTable resultDt = new DataTable();
            MySqlConnection connection = ConnectToSql();
            if (connection == null)
                return null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(storeProcedureName, connection);
                foreach (SqlParameter sqlParameter in sqlParameters)
                    cmd.Parameters.AddWithValue(sqlParameter.Field, sqlParameter.Value);
                cmd.CommandType = CommandType.StoredProcedure;
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(resultDt);
                }
                connection.Close();
                return resultDt;
            }
            catch (Exception ex)
            {
                LogsHelper.Print(MessagesVariable.FailedFetchDataInMySql + "\n" + ex.ToString());
                return null;
            }
        }

        public static bool ExecuteQuery(string sql)
        {
            return ExecuteTransaction(new List<string>(new string[] { sql }));
        }
        public static bool ExecuteQuery(SqlDetail sqlDetail)
        {
            SqlDetails sqlDetails = new SqlDetails();
            sqlDetails.Add(sqlDetail);
            return ExecuteTransaction(sqlDetails);
        }

        public static bool ExecuteTransaction(List<string> sqlList)
        {
            SqlDetails sqlDetails = new SqlDetails();
            foreach (string sql in sqlList)
                sqlDetails.Add(new SqlDetail(sql));
            return ExecuteTransaction(sqlDetails);
        }
        public static bool ExecuteTransaction(SqlDetails sqlDetails)
        {
            MySqlConnection connection = ConnectToSql();
            if (connection == null)
                return false;
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 300;
                foreach (SqlDetail sqlDetail in sqlDetails)
                {
                    cmd.CommandText = sqlDetail.Sql;
                    foreach (SqlParameter sqlParameter in sqlDetail.SqlParameters)
                        cmd.Parameters.AddWithValue(sqlParameter.Field, sqlParameter.Value);
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                connection.Close();
                LogsHelper.Print(MessagesVariable.FailedExecuteQueryInMySql + "\n" + ex.ToString());
                return false;
            }
        }

        public static bool ExecuteQueryByStoreProcedure(string storeProcedureName)
        {
            return ExecuteQueryByStoreProcedure(storeProcedureName, new SqlParameters());
        }
        public static bool ExecuteQueryByStoreProcedure(string storeProcedureName, SqlParameters sqlParameters)
        {
            try
            {
                MySqlConnection connection = ConnectToSql();
                MySqlCommand cmd = new MySqlCommand(storeProcedureName, connection);
                foreach (SqlParameter sqlParameter in sqlParameters)
                    cmd.Parameters.AddWithValue(sqlParameter.Field, sqlParameter.Value);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogsHelper.Print(MessagesVariable.FailedExecuteQueryInMySql + "\n" + ex.ToString());
                return false;
            }
        }
    }
}
