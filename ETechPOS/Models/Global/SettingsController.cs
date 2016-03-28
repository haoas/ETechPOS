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

        public static Settings GetDefaultData()
        {
            Settings settings = new Settings();
            settings.Add(new Setting("Com Port Number", "", "3", "", ""));
            settings.Add(new Setting("Customer Display", "Display 1", "WELCOME TO", "", ""));
            settings.Add(new Setting("Customer Display", "Display 2", "ETECH", "", ""));
            settings.Add(new Setting("Color Theme", "", "", "", ""));
            settings.Add(new Setting("Business Information", "Business Name", "ETECH", "", ""));
            settings.Add(new Setting("Business Information", "Owner", "ETECH", "", ""));
            settings.Add(new Setting("Business Information", "TIN", "TIN: 000-000-000-000", "", ""));
            settings.Add(new Setting("Business Information", "Address", "MANILA CITY", "", ""));
            settings.Add(new Setting("Business Information", "Permit Number", "Permit No.: 12345678901234567890", "", ""));
            settings.Add(new Setting("Business Information", "ACC", "ACC: 03840000317000054045982", "", ""));
            settings.Add(new Setting("Business Information", "Serial Number", "SN: NTS00000A0000000", "", ""));
            settings.Add(new Setting("Business Information", "MIN", "MIN: 12345678901234567890", "", ""));
            settings.Add(new Setting("Receipt Display", "Footer 1", "THIS SERVES AS AN OFFICIAL RECEIPT. BRING THIS RECEIPT IN CASE OF EXCHANGE OF MERCHANDISE WITHIN 2 DAYS", "", ""));
            settings.Add(new Setting("Receipt Display", "Footer 2", "THANK YOU AND COME AGAIN!", "", ""));
            settings.Add(new Setting("Receipt Display", "Footer 3", "", "", ""));
            settings.Add(new Setting("Receipt Display", "Footer 4", "", "", ""));
            settings.Add(new Setting("Avoid Invalid Purchase Price", "", "0", "", ""));
            settings.Add(new Setting("Print Receipt Format", "", "", "", ""));
            settings.Add(new Setting("Allow Zero Price", "", "1", "", ""));
            settings.Add(new Setting("Gross Method", "", "1", "", ""));
            settings.Add(new Setting("Show Detail Creditcard in ZRead", "", "1", "", ""));
            settings.Add(new Setting("OR Print Count", "", "1", "", ""));
            settings.Add(new Setting("POS Name", "", "ETECH POS SYSTEM", "", ""));
            settings.Add(new Setting("Show Complete OR", "", "1", "", ""));
            settings.Add(new Setting("Local Tax", "", "0", "", ""));
            settings.Add(new Setting("Service Charge", "", "0", "", ""));
            settings.Add(new Setting("Refund Memo", "", "2", "", ""));
            settings.Add(new Setting("Default Printer", "", "", "", ""));
            settings.Add(new Setting("Preview OR", "", "0", "", ""));
            settings.Add(new Setting("Product Search Style", "", "", "", ""));
            settings.Add(new Setting("Advertising URL", "", "", "", ""));
            settings.Add(new Setting("Maximum Cash Collection", "", "0", "", ""));
            settings.Add(new Setting("Read Date Range", "", "1", "", ""));
            settings.Add(new Setting("Automatic Show Keyboard", "", "0", "", ""));
            settings.Add(new Setting("POS Mac Address", "", "", "", ""));
            settings.Add(new Setting("Discount Details", "", "0", "", ""));
            settings.Add(new Setting("Customer Display Length", "", "0", "", ""));
            settings.Add(new Setting("POS Provider Name", "", "ETECH TECHNOLOGY SERVICE", "", ""));
            settings.Add(new Setting("POS Provider Address", "", "", "", ""));
            settings.Add(new Setting("POS Provider TIN", "", "", "", ""));
            settings.Add(new Setting("ACC Date", "", "", "", ""));
            settings.Add(new Setting("Print Receipt Actual", "", "0", "", ""));
            settings.Add(new Setting("Print Receipt Limit", "", "0", "", ""));
            settings.Add(new Setting("Print Receipt Buffer", "", "0", "", ""));
            return settings;
        }
    }
}
