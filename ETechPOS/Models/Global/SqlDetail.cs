using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ETech.Models.Global
{
    [Serializable]
    public class SqlDetail
    {
        public string Sql
        {
            get;
            set;
        }
        public SqlParameters SqlParameters
        {
            get;
            set;
        }

        public SqlDetail()
            : this("", new SqlParameters())
        {
        }
        public SqlDetail(string sql)
            : this(sql, new SqlParameters())
        {
        }
        public SqlDetail(string sql, SqlParameters sqlParameters)
        {
            Sql = sql;
            SqlParameters = sqlParameters;
        }
    }

    [Serializable]
    public class SqlDetails : List<SqlDetail>
    {
        public void AddRange(List<string> sqlList)
        {
            foreach (string sql in sqlList)
                this.Add(new SqlDetail(sql, new SqlParameters()));
        }
    }
}
