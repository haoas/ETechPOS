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
                    `SyncId` AS 'Id',
                    `Title` AS 'Title',
                    `Subtitle` AS 'Subtitle',
                    `Value1` AS 'Value 1',
                    `Value2` AS 'Value 2',
                    `Value3` AS 'Value 3'
                FROM
                    `settings`
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
            setting.Id = (long)dataRow["Id"];
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
                    `SyncId` AS 'Id',
                    `Title` AS 'Title',
                    `Subtitle` AS 'Subtitle',
                    `Value1` AS 'Value 1',
                    `Value2` AS 'Value 2',
                    `Value3` AS 'Value 3'
                FROM
                    `settings`
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
                setting.Id = (long)dataRow["Id"];
                setting.Title = (string)dataRow["Title"];
                setting.SubTitle = dataRow["Subtitle"].ToString();
                setting.Value1 = (object)dataRow["Value 1"];
                setting.Value2 = (object)dataRow["Value 2"];
                setting.Value3 = (object)dataRow["Value 3"];
                settings.Add(setting);
            }
            return settings;
        }

        public static Settings GetDefaultData()
        {
            Settings settings = new Settings();
            settings.Add(new Setting(1, "Com Port Number", "", "3", "", ""));
            settings.Add(new Setting(2, "Customer Display", "Display 1", "WELCOME TO", "", ""));
            settings.Add(new Setting(3, "Customer Display", "Display 2", "ETECH", "", ""));
            settings.Add(new Setting(4, "Color Theme", "", "", "", ""));
            settings.Add(new Setting(5, "Business Information", "Business Name", "ETECH", "", ""));
            settings.Add(new Setting(6, "Business Information", "Owner", "ETECH", "", ""));
            settings.Add(new Setting(7, "Business Information", "TIN", "TIN: 000-000-000-000", "", ""));
            settings.Add(new Setting(8, "Business Information", "Address", "MANILA CITY", "", ""));
            settings.Add(new Setting(9, "Business Information", "Permit Number", "Permit No.: 12345678901234567890", "", ""));
            settings.Add(new Setting(10, "Business Information", "ACC", "ACC: 03840000317000054045982", "", ""));
            settings.Add(new Setting(11, "Business Information", "Serial Number", "SN: NTS00000A0000000", "", ""));
            settings.Add(new Setting(12, "Business Information", "MIN", "MIN: 12345678901234567890", "", ""));
            settings.Add(new Setting(13, "Receipt Display", "Footer 1", "THIS SERVES AS AN OFFICIAL RECEIPT. BRING THIS RECEIPT IN CASE OF EXCHANGE OF MERCHANDISE WITHIN 2 DAYS", "", ""));
            settings.Add(new Setting(14, "Receipt Display", "Footer 2", "THANK YOU AND COME AGAIN!", "", ""));
            settings.Add(new Setting(15, "Receipt Display", "Footer 3", "", "", ""));
            settings.Add(new Setting(16, "Receipt Display", "Footer 4", "", "", ""));
            settings.Add(new Setting(17, "Avoid Invalid Purchase Price", "", "0", "", ""));
            settings.Add(new Setting(18, "Print Receipt Format", "", "", "", ""));
            settings.Add(new Setting(19, "Allow Zero Price", "", "1", "", ""));
            settings.Add(new Setting(20, "Gross Method", "", "1", "", ""));
            settings.Add(new Setting(21, "Show Detail Creditcard in ZRead", "", "1", "", ""));
            settings.Add(new Setting(22, "OR Print Count", "", "1", "", ""));
            settings.Add(new Setting(23, "POS Name", "", "ETECH POS SYSTEM", "", ""));
            settings.Add(new Setting(25, "Show Complete OR", "", "1", "", ""));
            settings.Add(new Setting(26, "Local Tax", "", "0", "", ""));
            settings.Add(new Setting(27, "Service Charge", "", "0", "", ""));
            settings.Add(new Setting(28, "Refund Memo", "", "2", "", ""));
            settings.Add(new Setting(29, "Default Printer", "", "", "", ""));
            settings.Add(new Setting(30, "Preview OR", "", "0", "", ""));
            settings.Add(new Setting(31, "Product Search Style", "", "", "", ""));
            settings.Add(new Setting(32, "Advertising URL", "", "", "", ""));
            settings.Add(new Setting(33, "Maximum Cash Collection", "", "0", "", ""));
            settings.Add(new Setting(34, "Read Date Range", "", "1", "", ""));
            settings.Add(new Setting(35, "Automatic Show Keyboard", "", "0", "", ""));
            settings.Add(new Setting(36, "POS Mac Address", "", "", "", ""));
            settings.Add(new Setting(37, "Discount Details", "", "0", "", ""));
            settings.Add(new Setting(38, "Customer Display Length", "", "0", "", ""));
            settings.Add(new Setting(39, "POS Provider Name", "", "ETECH TECHNOLOGY SERVICE", "", ""));
            settings.Add(new Setting(40, "POS Provider Address", "", "", "", ""));
            settings.Add(new Setting(41, "POS Provider TIN", "", "", "", ""));
            settings.Add(new Setting(42, "ACC Date", "", "", "", ""));
            settings.Add(new Setting(43, "Print Receipt Actual", "", "0", "", ""));
            settings.Add(new Setting(44, "Print Receipt Limit", "", "0", "", ""));
            settings.Add(new Setting(45, "Print Receipt Buffer", "", "0", "", ""));
            return settings;
        }
    }
}
