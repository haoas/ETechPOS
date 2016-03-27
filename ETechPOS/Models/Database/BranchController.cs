using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ETech.Models.Global;
using ETech.fnc;

namespace ETech.Models.Database
{
    public static class BranchController
    {
        public static Branch GetData(object id)
        {
            string selectSql =
                @"SELECT
                    `Id` AS 'Id',
                    `code` AS 'Code',
                    `name` AS 'Name'
                FROM
                    `branch`
                WHERE
                    `Id` = @id";
            SqlParameters sqlParameters = new SqlParameters();
            sqlParameters.Add(new SqlParameter("id", id));
            DataTable resultDt = MySqlFunction.GetDataTable(new SqlDetail(selectSql, sqlParameters));
            if (resultDt == null
                || resultDt.Rows.Count <= 0)
                return null;
            Branch branch = new Branch();
            DataRow dataRow = resultDt.Rows[0];
            branch.Id = Convert.ToInt64(dataRow["Id"]);
            branch.Code = dataRow["Code"].ToString();
            branch.Name = dataRow["Name"].ToString();
            return branch;
        }
        public static Branch GetDataFromConfigurationTable()
        {
            string selectSql =
                @"SELECT
                    `value` AS Value
                FROM
                    `config`
                WHERE
                    `particular` = 'branchid'";
            DataTable branchConfigDt = MySqlFunction.GetDataTable(selectSql);
            if (branchConfigDt == null)
                return null;
            return GetData(Convert.ToInt64(branchConfigDt.Rows[0]["Value"]));
        }
    }

    public static class BranchListController
    {
        public static BranchList GetData()
        {
            return GetData(new Dictionary<string, object>());
        }
        public static BranchList GetData(Dictionary<string, object> dictionary)
        {
            SqlParameters sqlParameters = new SqlParameters();
            string whereSql = "";
            foreach (string key in dictionary.Keys)
            {
                whereSql += " AND `" + key + "` = @" + key;
                sqlParameters.Add(new SqlParameter(key, dictionary[key]));
            }
            if (whereSql.Length > 0)
            {
                whereSql = whereSql.Remove(0, 5);
                whereSql = @"WHERE
                    " + whereSql;
            }
            string selectSql =
                @"SELECT
                    `Id` AS 'Id',
                    `code` AS 'Code',
                    `name` AS 'Name'
                FROM
                    `branch`
                " + whereSql + @"";
            DataTable resultDt = MySqlFunction.GetDataTable(new SqlDetail(selectSql, sqlParameters));
            if (resultDt == null)
                return null;
            else if (resultDt.Rows.Count <= 0)
                return new BranchList();
            BranchList branchList = new BranchList();
            foreach (DataRow dataRow in resultDt.Rows)
            {
                Branch branch = new Branch();
                branch.Id = Convert.ToInt64(dataRow["Id"]);
                branch.Code = dataRow["Code"].ToString();
                branch.Name = dataRow["Name"].ToString();
                branchList.Add(branch);
            }
            return branchList;
        }
    }
}
