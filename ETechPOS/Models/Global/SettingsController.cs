using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ETech.cls;
using ETech.Variables;
using ETech.Helpers;
using ETech.fnc;
using System.Data;

namespace ETech.Models.Global
{
    public static class SettingController
    {
        public static Setting GetData(object id)
        {
            string selectSql =
                @"SELECT
                    `Title` AS 'Title',
                    `Subtitle` AS 'Subtitle',
                    `Value1` AS 'Value 1',
                    `Value2` AS 'Value 2',
                    `Value3` AS 'Value 3'
                FROM
                    `possettings`
                WHERE
                    `SyncId` = @id";
            SqlParameters sqlParameters = new SqlParameters();
            sqlParameters.Add(new SqlParameter("id", id));
            DataTable resultDt = MySqlFunction.GetDataTable(new SqlDetail(selectSql, sqlParameters));
            if (resultDt == null
                || resultDt.Rows.Count <= 0)
                return null;
            Setting setting = new Setting();
            DataRow dataRow = resultDt.Rows[0];
            setting.Title = (string)dataRow["Title"];
            setting.SubTitle = dataRow["Subtitle"].ToString();
            setting.Value1 = (object)dataRow["Value 1"];
            setting.Value2 = (object)dataRow["Value 2"];
            setting.Value3 = (object)dataRow["Value 3"];
            return setting;
        }
    }

    public static class SettingsController
    {
        public static Settings GetData()
        {
            return GetData(new Dictionary<string, object>());
        }
        public static Settings GetData(Dictionary<string, object> dictionary)
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
                    `Title` AS 'Title',
                    `Subtitle` AS 'Subtitle',
                    `Value1` AS 'Value 1',
                    `Value2` AS 'Value 2',
                    `Value3` AS 'Value 3'
                FROM
                    `possettings`
                " + whereSql + @"";
            DataTable resultDt = MySqlFunction.GetDataTable(new SqlDetail(selectSql, sqlParameters));
            if (resultDt == null)
                return null;
            else if (resultDt.Rows.Count <= 0)
                return new Settings();
            Settings settings = new Settings();
            foreach (DataRow dataRow in resultDt.Rows)
            {
                Setting setting = new Setting();
                setting.Title = (string)dataRow["Title"];
                setting.SubTitle = dataRow["Subtitle"].ToString();
                setting.Value1 = (object)dataRow["Value 1"];
                setting.Value2 = (object)dataRow["Value 2"];
                setting.Value3 = (object)dataRow["Value 3"];
                settings.Add(setting);
            }
            return settings;
        }
    }
}
