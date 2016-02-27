using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETech.cls;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using ETech;
using ETech.fnc;

public static class zreadFunc
{
    public static DateTime GetMinSalesDate(bool isposd)
    {
        string saleshead = "`saleshead`";
        string posxyzread = "`posxyzread`";
        if (isposd)
        {
            saleshead = "`saleshead_posd`";
            posxyzread = "`posxyzread_posd`";
        }

        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        string GetMinDateInSalesHead =
        @"SELECT COALESCE(min(`date`),NOW()) as mindate 
            FROM " + saleshead + @" WHERE `branchid` = " + branchid +
        " AND `terminalno` = " + terminalno + " AND `type` = 3";
        DateTime DTMinDateInSaleshead = Convert.ToDateTime(mySQLFunc.getdb(GetMinDateInSalesHead).Rows[0]["mindate"]);
        DTMinDateInSaleshead = zreadFunc.getZreadDate(DTMinDateInSaleshead);

        string GetMinDateInPosxyzread =
        @"SELECT COALESCE(MIN(`date`),NOW()) as mindate FROM " + posxyzread + @"
            WHERE terminalno=" + terminalno + " AND branchid=" + branchid;
        DateTime DTMinDateInPosxyzread = Convert.ToDateTime(mySQLFunc.getdb(GetMinDateInPosxyzread).Rows[0]["mindate"]);

        int earlier = DateTime.Compare(DTMinDateInSaleshead, DTMinDateInPosxyzread);
        DateTime DTearlier = (earlier < 0) ? DTMinDateInSaleshead : DTMinDateInPosxyzread;
        return DTearlier;
    }

    public static string GetSQLDateRange(string SQLdate, DateTime DateFrom, DateTime DateTo)
    {
        double starttime = Convert.ToDouble(cls_globalvariables.starttime_v);
        double endtime = (starttime + (24 * 3600) - 1);
        DateFrom = DateFrom.Date.AddSeconds(starttime);
        DateTo = DateTo.Date.AddSeconds(endtime);
        string SQLDateFrom = DateFrom.ToString("yyyy-MM-dd HH:mm:ss");
        string SQLDateTo = DateTo.ToString("yyyy-MM-dd HH:mm:ss");
        string SQLDateRangeQuery = " AND " + SQLdate + @" BETWEEN '" + SQLDateFrom + @"' AND '" + SQLDateTo + @"' ";
        //MessageBox.Show(SQLDateRangeQuery);
        return SQLDateRangeQuery;
    }

    public static int get_readcount(int readtype, DateTime datetime)
    {
        try
        {
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            DataTable dt = mySQLFunc.getdb(@"SELECT * FROM `posxyzread`
                                                      WHERE `readcount`=1
                                                      AND `readtype` = " + readtype + @"  
                                                      AND `branchid` = " + branchid + @"
                                                      AND `terminalno` = " + terminalno);
            if (dt.Rows.Count == 0) return 1;

            DateTime dateOfFirstDay = DateTime.Parse(dt.Rows[0]["date"].ToString()).Date;
            TimeSpan spanDateDiff = (datetime.Date - dateOfFirstDay);
            int datediff = Convert.ToInt32(Math.Floor(spanDateDiff.TotalDays));
            return datediff + 1;
        }
        catch (Exception e)
        {
            MessageBox.Show("ERROR in get_readcount: " + e.ToString());
        }
        return 0;
    }

    public static DateTime getZreadDate(DateTime datetime)
    {
        double StartTimeSeconds = Convert.ToDouble(cls_globalvariables.starttime_v);
        return (datetime >= datetime.Date.AddSeconds(StartTimeSeconds)) ? datetime : datetime.AddDays(-1);
    }

    public static DateTime GetMaxDateTimeFromPosXYZRead()
    {
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;
        string SQL =
        @"SELECT COALESCE(DATE(max(`date`)), DATE_ADD(DATE(NOW()),INTERVAL -1 DAY)) as 'maxdate'  
            FROM (  SELECT max(`date`) as 'date'
	                FROM `posxyzread` as P
	                WHERE P.`branchid`= " + branchid + @" AND P.`terminalno`=" + terminalno + @" AND P.`readtype`=3 
	                union all
	                SELECT max(`date`)
	                FROM `posxyzread_posd` as P
	                WHERE P.`branchid`= " + branchid + @" AND P.`terminalno`=" + terminalno + @" AND P.`readtype`=3) A";
        Console.WriteLine(SQL);
        return Convert.ToDateTime(mySQLFunc.getdb(SQL).Rows[0]["maxdate"].ToString());
    }

    public static string Zread_getBatch(DateTime date, int leftpadding)
    {
        string terminalno = cls_globalvariables.terminalno_v;
        string branchid = cls_globalvariables.BranchCode;

        string sqlDate = date.ToString("yyyy-MM-dd");
        string SQLgetBatch = @"SELECT `batch` FROM `posxyzread` 
            WHERE `readtype`=3 AND `terminalno`='" + terminalno +
       "' AND `branchid`='" + branchid + "' AND DATE(`date`)='" + sqlDate + "'";
        DataTable DTgetBatch = mySQLFunc.getdb(SQLgetBatch);

        try
        {
            return DTgetBatch.Rows[0]["batch"].ToString().PadLeft(leftpadding, '0');
        }
        catch (Exception)
        {
            return "01";
        }
    }

    public static bool Zread_exist(DateTime date, int type, bool isposd)
    {
        string tbl_posxyzread = "`posxyzread`";
        if (isposd) tbl_posxyzread = "`posxyzread_posd`";

        string terminalno = cls_globalvariables.terminalno_v;
        string branchid = cls_globalvariables.BranchCode;

        string sqlDate = date.ToString("yyyy-MM-dd");
        string SQLexist =
        @"SELECT * FROM " + tbl_posxyzread + @" 
            WHERE `readtype`=" + type + @" AND `terminalno`='" + terminalno +
       "' AND `branchid`='" + branchid + "' AND DATE(`date`)='" + sqlDate + "'";
        return (mySQLFunc.getdb(SQLexist).Rows.Count > 0) ? true : false;
    }

    public static bool Zcount()
    {
        string SQL =
            @"SELECT
	            COUNT(*) AS 'Count'
            FROM
	            `posxyzread`
            WHERE
	            `branchid` = " + cls_globalvariables.BranchCode + @"
                AND `terminalno`= " + cls_globalvariables.terminalno_v + @"
                AND `readtype` = '3'
                AND `lock` = 1
                AND DATE(NOW()) = DATE(`date`)";
        int zcnt = Convert.ToInt32(mySQLFunc.getdb(SQL).Rows[0]["Count"].ToString());
        if (zcnt > 0) return true;
        return false;
    }

    public static bool generate_posxyzread(DateTime datetime_d, int Readtype, bool isposd)
    {
        //When Generating Z reading, Automatically Generate X reading
        if (Readtype == 3)
            generate_posxyzread(datetime_d, 1, isposd);
        //-----------------------------------------------------------

        DateTime now = mySQLFunc.DateTimeNow();

        string suffix = (isposd) ? "_posd" : "";

        DateTime MinSalesDate = GetMinSalesDate(isposd).Date;
        if ((MinSalesDate == now.Date) || (MinSalesDate == datetime_d.Date))
        {
            generate_posxyzread_firstday(Readtype, isposd);
            return true;
        }

        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        DateTime yesterdaydatetime = datetime_d.AddDays(-1);

        //previousday
        string readcount = zreadFunc.get_values_from_posxyzread(yesterdaydatetime, Readtype, isposd)["readcount"].ToString();
        decimal newgrandtotal = zreadFunc.get_newgrandtotal_from_posxyzread(yesterdaydatetime, Readtype, isposd);

        if (readcount == "0")
        {
            generate_ungenerated_readings(yesterdaydatetime, Readtype, isposd);
            return false;
        }

        readcount = (Convert.ToInt16(readcount) + 1).ToString();
        decimal oldgrandtotal = newgrandtotal;
        decimal cur_total = Math.Round(Convert.ToDecimal(get_currenttotal_data(datetime_d, isposd)), 2, MidpointRounding.AwayFromZero);
        newgrandtotal = Math.Round(oldgrandtotal + cur_total, 2, MidpointRounding.AwayFromZero);

        DataRow DRminmax = get_max_min_OR(3, datetime_d, datetime_d);
        string ornumber_begin = zreadFunc.get_min_OR(3, datetime_d, datetime_d); //DRminmax["minornumber"].ToString();
        string ornumber_end = DRminmax["maxornumber"].ToString();
        string sdatetime = DRminmax["maxdatetime"].ToString();

        if (isposd)
        {
            string deletesql = @"DELETE FROM posxyzread_posd WHERE readcount='" + readcount + @"' AND readtype='" + Readtype + @"'
                                  AND terminalno='" + terminalno + @"' AND branchid='" + branchid + @"'";
            mySQLFunc.setdb(deletesql);

            string sqlinsert = @"INSERT INTO `posxyzread_posd` 
                (`readcount`, `readtype`, `startor`, `endor`, `date`, `terminalno`, `branchid`, `oldgrandtotal`, `newgrandtotal`)
                VALUES 
                (" + readcount + @", " + Readtype + " ,'" + ornumber_begin + @"','" + ornumber_end + @"', '" + sdatetime + @"',
                 " + terminalno + @"," + branchid + @", " + oldgrandtotal + @", " + newgrandtotal + @" )";
            Console.WriteLine(sqlinsert);
            mySQLFunc.setdb(sqlinsert);
        }
        else
        {
            string SQLexist =
            @"SELECT * FROM `posxyzread` WHERE `readcount`='" + readcount + @"' AND `readtype`='" + Readtype +
            @"' AND `terminalno`='" + terminalno + "' AND `branchid`='" + branchid + @"'";

            if ((mySQLFunc.getdb(SQLexist).Rows.Count <= 0))
            {
                string sqlinsert = @"INSERT INTO `posxyzread` 
                (`readcount`, `readtype`, `startor`, `endor`, `date`, `terminalno`, `branchid`, `oldgrandtotal`, `newgrandtotal`,`lock`)
                VALUES 
                (" + readcount + @", " + Readtype + " ,'" + ornumber_begin + @"','" + ornumber_end + @"', '" + sdatetime + @"',
                 " + terminalno + @"," + branchid + @", " + oldgrandtotal + @", " + newgrandtotal + @", " + 1 + @" )";
                Console.WriteLine(sqlinsert);
                mySQLFunc.setdb(sqlinsert);
            }
            else
            {
                // do not update begin and end ors
                string sqlupdate =
                @"UPDATE `posxyzread` SET `date`='" + sdatetime + @"',`startor`='" + ornumber_begin + @"', `endor`='" + ornumber_end +
                @"',`oldgrandtotal`='" + oldgrandtotal + @"', `newgrandtotal`='" + newgrandtotal + @"' , `lock`='1' 
                WHERE `readcount`='" + readcount + @"' AND `readtype`='" + Readtype +
                @"' AND `terminalno`='" + terminalno + "' AND `branchid`='" + branchid + @"' LIMIT 1";
                Console.WriteLine(sqlupdate);
                mySQLFunc.setdb(sqlupdate);
            }
        }

        DataRow dr_summary = fncHardware.get_summary_data(datetime_d, isposd);
        decimal vat_sales_amt = Convert.ToDecimal(dr_summary["vatable_sale"]);
        decimal vat_returns_amt = Convert.ToDecimal(dr_summary["vatable_return"]);
        decimal nonvat_sales_amt = Convert.ToDecimal(dr_summary["nonvat_sale"]);
        decimal nonvat_returns_amt = Convert.ToDecimal(dr_summary["nonvat_return"]);
        decimal senior_returns_amt = Convert.ToDecimal(dr_summary["senior_return"]);
        decimal senior_sale_amt = Convert.ToDecimal(dr_summary["senior_sale"]);
        decimal t_senior_sale = senior_sale_amt + senior_returns_amt;
        decimal total_discount_amt = Convert.ToDecimal(dr_summary["total_discount"]);

        decimal cash_sales_amt = Convert.ToDecimal(dr_summary["cash"]);
        decimal credit_sales_amt = Convert.ToDecimal(dr_summary["creditcard"]);
        decimal debit_sales_amt = Convert.ToDecimal(dr_summary["debitcard"]);
        decimal bank_sales_amt = Convert.ToDecimal(dr_summary["bankcheque"]);
        decimal gift_sales_amt = Convert.ToDecimal(dr_summary["giftcheque"]);
        decimal member_points_amt = Convert.ToDecimal(dr_summary["memberpoints"]);
        decimal senior_discount_amt = (t_senior_sale / (1 - cls_globalvariables.senior)) * cls_globalvariables.senior;
        decimal void_trans_amt = Convert.ToDecimal(dr_summary["total_void_amount"]);
        decimal sucess_trans_cnt = Convert.ToDecimal(dr_summary["success_trans"]);
        decimal all_trans_cnt = Convert.ToDecimal(dr_summary["all_trans"]);
        decimal return_qty_cnt = -1 * Convert.ToDecimal(dr_summary["total_return_qty"]);
        int total_qty_sold = Convert.ToInt32(dr_summary["total_qty"]);
        int total_qty_void = 0;

        string sqlupdate2 =
        @"UPDATE `posxyzread" + suffix + @"` SET 
        `total_discount_amt`='" + total_discount_amt + @"',
        `vat_returns_amt`='" + vat_returns_amt + @"',
        `nonvat_returns_amt`='" + nonvat_returns_amt + @"',
        `senior_returns_amt`='" + senior_returns_amt + @"',
        `cash_sales_amt`='" + cash_sales_amt + @"',
        `credit_sales_amt`='" + credit_sales_amt + @"',
        `debit_sales_amt`='" + debit_sales_amt + @"', 
        `bank_sales_amt`='" + bank_sales_amt + @"',
        `gift_sales_amt`='" + gift_sales_amt + @"',
        `member_points_amt`='" + member_points_amt + @"',
        `senior_sales_amt`='" + senior_sale_amt + @"',
        `vatable_sales_amt`='" + vat_sales_amt + @"',
        `nonvat_sales_amt`='" + nonvat_sales_amt + @"',
        `total_qty_sold`='" + total_qty_sold + @"',
        `total_qty_void`='" + total_qty_void + @"',
        `void_trans_amt`='" + void_trans_amt + @"',
        `sucess_trans_cnt`='" + sucess_trans_cnt + @"',
        `return_qty_cnt`='" + return_qty_cnt + @"'
         WHERE `readcount`='" + readcount + @"' AND `readtype`='" + Readtype +
             @"' AND `terminalno`='" + terminalno + "' AND `branchid`='" + branchid + @"' LIMIT 1";
        mySQLFunc.setdb(sqlupdate2);
        return true;
    }

    public static decimal get_currenttotal_data(DateTime datetime_d, bool isposd)
    {
        string suffix = (isposd) ? "_posd" : "";
        string sBranchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;
        string sdate = datetime_d.ToString("yyyy-MM-dd");

        string sql = @"SELECT SUM(ROUND(CAST(D.`price` * D.`quantity` AS DECIMAL(10,3)),2)) AS 'total_amount'
                        FROM `saleshead" + suffix + @"` AS H, `salesdetail" + suffix + @"` AS D
                        WHERE H.`wid` = D.`headid` AND H.`show` = 1 AND H.`status` = 1 AND H.`type` = 3
                            AND H.`terminalno` = " + terminalno + @" AND H.`branchid` = " + sBranchid +
                        zreadFunc.GetSQLDateRange("H.`date`", datetime_d, datetime_d);
        decimal cur_total = 0;
        decimal.TryParse(mySQLFunc.getdb(sql).Rows[0]["total_amount"].ToString(), out cur_total);
        return cur_total;
    }

    public static string get_min_OR(int type, DateTime datetimeFrom, DateTime datetimeTo)
    {
        string sql =
                @"SELECT 
                    COALESCE(MAX(`ornumber`), 0) AS `maxornumber`,
                    COALESCE(MIN(`ornumber`), 0) AS `minornumber`,
                    DATE(`date`) AS `date`
                FROM `saleshead`
                WHERE `type` = 3
                    AND `ornumber` <> 0
                    AND `branchid` = " + cls_globalvariables.BranchCode + @"
                    AND `terminalno` = " + cls_globalvariables.terminalno_v + @"
                    AND `date`
                        BETWEEN '" + datetimeFrom.ToString("yyyy-MM-dd") + @" 00:00:00'
                        AND '" + datetimeTo.ToString("yyyy-MM-dd") + @" 23:59:59'";
        DataTable DTMinOrNumber = mySQLFunc.getdb(sql);
        if (DTMinOrNumber == null)
            return "000000000";
        else if (DTMinOrNumber.Rows.Count <= 0)
            return "000000000";
        else
            return DTMinOrNumber.Rows[0]["minornumber"].ToString();
    }

    public static DataRow get_max_min_OR(int type, DateTime datetimeFrom, DateTime datetimeTo)
    {
        string datetimeNosales = datetimeFrom.Date.Add(DateTime.Now.TimeOfDay).ToString("yyyy-MM-dd HH:mm:ss");
        string GetMinAndMaxOR =
                @"SELECT 
                    IF(DATE(NOW())='" + datetimeFrom.ToString("yyyy-MM-dd") + @"',NOW(),COALESCE(MAX(`date`),'" + datetimeNosales + @"')) AS `maxdatetime`,
                    COALESCE(MAX(`ornumber`), 0) AS `maxornumber`,
                    COALESCE(MIN(`ornumber`), 0) AS `minornumber`,
                    DATE(`date`) AS `date`
                FROM `saleshead`
                WHERE `type` = 3
                    AND `ornumber` <> 0
                    AND `branchid` = " + cls_globalvariables.BranchCode + @"
                    AND `terminalno` = " + cls_globalvariables.terminalno_v + @"
                    AND `date`
                        BETWEEN '" + datetimeFrom.ToString("yyyy-MM-dd") + @" 00:00:00'
                        AND '" + datetimeTo.ToString("yyyy-MM-dd") + @" 23:59:59'";
        Console.WriteLine(GetMinAndMaxOR);
        DataTable dtornumber = mySQLFunc.getdb(GetMinAndMaxOR);
        return dtornumber.Rows[0];
    }

    public static decimal get_newgrandtotal_from_posxyzread(DateTime datetime_d, int readtype, bool isposd)
    {
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        string sql = @"SELECT COALESCE(`newgrandtotal`,0) as 'newgrandtotal'
                FROM `posxyzread`
                WHERE `branchid` = " + branchid + @" 
                    AND `terminalno` = " + terminalno + @"
                    AND `readtype` = " + readtype +
                zreadFunc.GetSQLDateRange("`date`", datetime_d, datetime_d) + @" LIMIT 1";
        Console.WriteLine(sql);

        DataTable DT = mySQLFunc.getdb(sql);
        if (DT.Rows.Count <= 0)
            return 0;
        else
            return Convert.ToDecimal(DT.Rows[0]["newgrandtotal"]);
    }

    public static DataRow get_values_from_posxyzread(DateTime datetime_d, DateTime datetimeTO_d, int readtype, bool isposd)
    {
        string suffix = (isposd) ? "_posd" : "";
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        string sql = @"SELECT 
            COALESCE(CONCAT(MIN(`readcount`),' to ',MAX(`readcount`)),0) AS 'readcount',
            COALESCE(MIN(`date`),0) AS 'mindate',
            COALESCE(MAX(`date`),0) AS 'maxdate',            
            COALESCE(MIN(`startor`),0) AS 'startor',
            COALESCE(MAX(`endor`),0) AS 'endor',
            COALESCE(MIN(`oldgrandtotal`),0) AS 'oldgrandtotal',
            COALESCE(MAX(`newgrandtotal`),0) AS 'newgrandtotal',
            COALESCE(SUM(`total_discount_amt`),0) AS 'total_discount',
            COALESCE(SUM(`vat_returns_amt`),0) AS 'vatable_return',
            COALESCE(SUM(`nonvat_returns_amt`),0) AS 'nonvat_return',
            COALESCE(SUM(`senior_returns_amt`),0) AS 'senior_return',
            COALESCE(SUM(`cash_sales_amt`),0) AS 'cash',
            COALESCE(SUM(`credit_sales_amt`),0) AS 'creditcard',
            COALESCE(SUM(`debit_sales_amt`),0) AS 'debitcard',
            COALESCE(SUM(`bank_sales_amt`),0) AS 'bankcheque',
            COALESCE(SUM(`gift_sales_amt`),0) AS 'giftcheque',
            COALESCE(SUM(`senior_sales_amt`),0) AS 'senior_sale',
            COALESCE(SUM(`vatable_sales_amt`),0) AS 'vatable_sale',
            COALESCE(SUM(`nonvat_sales_amt`),0) AS 'nonvat_sale',
            COALESCE(SUM(`total_qty_sold`),0) AS 'total_qty',
            0 as 'total_void_qty',
            COALESCE(SUM(`void_trans_amt`),0) AS 'total_void_amount',
            COALESCE(SUM(`sucess_trans_cnt`),0) AS 'success_trans',
            COALESCE(SUM(`return_qty_cnt`),0) AS 'total_return_qty'
                FROM `posxyzread" + suffix + @"`
                WHERE `branchid` = " + branchid + @" 
                    AND `terminalno` = " + terminalno + @"
                    AND `readtype` = " + readtype +
                zreadFunc.GetSQLDateRange("`date`", datetime_d, datetimeTO_d) + @" LIMIT 1";
        Console.WriteLine(sql);

        DataRow dr_summary = mySQLFunc.getdb(sql).Rows[0];
        return dr_summary;
    }
    public static DataRow get_values_from_posxyzread(DateTime datetime_d, int readtype, bool isposd)
    {
        string suffix = (isposd) ? "_posd" : "";
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        string sql = @"SELECT 
            COUNT(0) as cntt,
            COALESCE(MIN(`date`),0) AS 'mindate',
                COALESCE(MAX(`date`),0) AS 'maxdate', 
            COALESCE(`readcount`,0) AS 'readcount',
            COALESCE(`startor`,0) AS 'startor',
            COALESCE(`endor`,0) AS 'endor',
            COALESCE(`oldgrandtotal`,0) AS 'oldgrandtotal',
            COALESCE(`newgrandtotal`,0) AS 'newgrandtotal',
            COALESCE(`total_discount_amt`,0) AS 'total_discount',
            COALESCE(`vat_returns_amt`,0) AS 'vatable_return',
            COALESCE(`nonvat_returns_amt`,0) AS 'nonvat_return',
            COALESCE(`senior_returns_amt`,0) AS 'senior_return',
            COALESCE(`cash_sales_amt`,0) AS 'cash',
            COALESCE(`credit_sales_amt`,0) AS 'creditcard',
            COALESCE(`debit_sales_amt`,0) AS 'debitcard',
            COALESCE(`bank_sales_amt`,0) AS 'bankcheque',
            COALESCE(`gift_sales_amt`,0) AS 'giftcheque',
            COALESCE(`member_points_amt`,0) AS 'memberpoints',
            COALESCE(`senior_sales_amt`,0) AS 'senior_sale',
            COALESCE(`vatable_sales_amt`,0) AS 'vatable_sale',
            COALESCE(`nonvat_sales_amt`,0) AS 'nonvat_sale',
            COALESCE(`total_qty_sold`,0) AS 'total_qty',
            0 as 'total_void_qty',
            COALESCE(`void_trans_amt`,0) AS 'total_void_amount',
            COALESCE(`sucess_trans_cnt`,0) AS 'success_trans',
            COALESCE(`return_qty_cnt`,0) AS 'total_return_qty'
                FROM `posxyzread" + suffix + @"`
                WHERE `branchid` = " + branchid + @" 
                    AND `terminalno` = " + terminalno + @"
                    AND `readtype` = " + readtype +
                zreadFunc.GetSQLDateRange("`date`", datetime_d, datetime_d) + @" LIMIT 1";
        Console.WriteLine(sql);

        DataRow dr_summary = mySQLFunc.getdb(sql).Rows[0];
        return dr_summary;
    }

    public static void generate_posxyzread_firstday(int ReadType, bool isposd)
    {
        string suffix = (isposd) ? "_posd" : "";
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;
        DateTime firstday = GetMinSalesDate(isposd);

        DataRow DRminmax = get_max_min_OR(3, firstday, firstday);
        string ornumber_begin = zreadFunc.get_min_OR(3, firstday, firstday); //DRminmax["minornumber"].ToString();
        string ornumber_end = DRminmax["maxornumber"].ToString();
        string sdatetime = DRminmax["maxdatetime"].ToString();

        decimal newgrandtotal = get_currenttotal_data(firstday, isposd);

        string deletesql = @"DELETE FROM `posxyzread" + suffix + @"` WHERE
                 `readcount`=1 AND `readtype`=" + ReadType + @" AND `branchid`= " + branchid + @" AND
                 `terminalno`= " + terminalno;

        mySQLFunc.setdb(deletesql);
        string sql = "";
        if (isposd)
        {
            sql = @"INSERT INTO `posxyzread_posd` 
            (`readcount`, `readtype`, `startor`, `endor`, `date`, `terminalno`, `branchid`, `oldgrandtotal`, `newgrandtotal`)
            VALUES 
            (" + 1 + @", " + ReadType + " ,'" + ornumber_begin + @"','" + ornumber_end + @"', '" + sdatetime + @"',
             " + terminalno + @"," + branchid + @", " + 0 + @", " + newgrandtotal + @" )";
        }
        else
        {
            sql = @"INSERT INTO `posxyzread` 
            (`readcount`, `readtype`, `startor`, `endor`, `date`, `terminalno`, `branchid`, `oldgrandtotal`, `newgrandtotal`, `lock`)
            VALUES 
            (" + 1 + @", " + ReadType + " ,'" + ornumber_begin + @"','" + ornumber_end + @"', '" + sdatetime + @"',
             " + terminalno + @"," + branchid + @", " + 0 + @", " + newgrandtotal + @", " + 1 + @" )";
        }

        mySQLFunc.setdb(sql);

        DataRow dr_summary = fncHardware.get_summary_data(firstday, isposd);

        decimal vat_sales_amt = Convert.ToDecimal(dr_summary["vatable_sale"]);
        decimal vat_returns_amt = Convert.ToDecimal(dr_summary["vatable_return"]);
        decimal nonvat_sales_amt = Convert.ToDecimal(dr_summary["nonvat_sale"]);
        decimal nonvat_returns_amt = Convert.ToDecimal(dr_summary["nonvat_return"]);
        decimal senior_returns_amt = Convert.ToDecimal(dr_summary["senior_return"]);
        decimal senior_sale_amt = Convert.ToDecimal(dr_summary["senior_sale"]);
        decimal t_senior_sale = senior_sale_amt + senior_returns_amt;
        decimal total_discount_amt = Convert.ToDecimal(dr_summary["total_discount"]);

        decimal cash_sales_amt = Convert.ToDecimal(dr_summary["cash"]);
        decimal credit_sales_amt = Convert.ToDecimal(dr_summary["creditcard"]);
        decimal debit_sales_amt = Convert.ToDecimal(dr_summary["debitcard"]);
        decimal bank_sales_amt = Convert.ToDecimal(dr_summary["bankcheque"]);
        decimal gift_sales_amt = Convert.ToDecimal(dr_summary["giftcheque"]);
        decimal member_points_amt = Convert.ToDecimal(dr_summary["memberpoints"]);
        decimal senior_discount_amt = (t_senior_sale / (1 - cls_globalvariables.senior)) * cls_globalvariables.senior;
        decimal void_trans_amt = Convert.ToDecimal(dr_summary["total_void_amount"]);
        decimal sucess_trans_cnt = Convert.ToDecimal(dr_summary["success_trans"]);
        decimal all_trans_cnt = Convert.ToDecimal(dr_summary["all_trans"]);
        decimal return_qty_cnt = -1 * Convert.ToDecimal(dr_summary["total_return_qty"]);
        int total_qty_sold = Convert.ToInt32(dr_summary["total_qty"]);
        int total_qty_void = 0;

        string sqlupdate2 =
        @"UPDATE `posxyzread" + suffix + @"` SET 
        `total_discount_amt`='" + total_discount_amt + @"',
        `vat_returns_amt`='" + vat_returns_amt + @"',
        `nonvat_returns_amt`='" + nonvat_returns_amt + @"',
        `senior_returns_amt`='" + senior_returns_amt + @"',
        `cash_sales_amt`='" + cash_sales_amt + @"',
        `credit_sales_amt`='" + credit_sales_amt + @"',
        `debit_sales_amt`='" + debit_sales_amt + @"', 
        `bank_sales_amt`='" + bank_sales_amt + @"',
        `gift_sales_amt`='" + gift_sales_amt + @"',
        `member_points_amt`='" + member_points_amt + @"',
        `senior_sales_amt`='" + senior_sale_amt + @"',
        `vatable_sales_amt`='" + vat_sales_amt + @"',
        `nonvat_sales_amt`='" + nonvat_sales_amt + @"',
        `total_qty_sold`='" + total_qty_sold + @"',
        `total_qty_void`='" + total_qty_void + @"',
        `void_trans_amt`='" + void_trans_amt + @"',
        `sucess_trans_cnt`='" + sucess_trans_cnt + @"',
        `return_qty_cnt`='" + return_qty_cnt + @"'
         WHERE `readcount`='" + 1 + @"' AND `readtype`='" + ReadType +
             @"' AND `terminalno`='" + terminalno + "' AND `branchid`='" + branchid + @"' LIMIT 1";
        mySQLFunc.setdb(sqlupdate2);
        zreadFunc.generate_ungenerated_readings(ReadType, isposd);
    }

    public static void generate_ungenerated_readings(DateTime generateUntil, int type, bool isposd)
    {
        DateTime generateFrom = generateUntil;
        string suffix = (isposd) ? "_posd" : "";
        generateUntil = generateUntil.Date;
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        string maxdatesql =
            @"SELECT * FROM (
            SELECT COALESCE(MAX(`date`),'') as 'maxdate'
            FROM `posxyzread" + suffix + @"`
            WHERE   DATE(`date`) <= '" + generateUntil.ToString("yyyy-MM-dd") + @"' 
                    AND `branchid` = " + branchid + @" 
                    AND `terminalno` = " + terminalno + @"
                    AND `readtype` = " + type + ")A WHERE  maxdate<>'';";

        DataTable dt = mySQLFunc.getdb(maxdatesql);

        if (dt.Rows.Count <= 0)
            generate_posxyzread_firstday(type, isposd);
        else
            generateFrom = Convert.ToDateTime(dt.Rows[0]["maxdate"]);

        while (generateFrom.AddDays(-1).Date < generateUntil.Date)
        {
            generateFrom = generateFrom.AddDays(1);
            generate_posxyzread(generateFrom, type, isposd);
        }
    }

    public static void generate_ungenerated_readings(int type, bool isposd)
    {
        if (zreadFunc.GetMinSalesDate(isposd).Date == DateTime.Now.Date)
            return;

        string suffix = (isposd) ? "_posd" : "";
        DateTime temp_datetime = DateTime.Now.Date;
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;

        string maxdatesql =
            @"SELECT * FROM (
            SELECT COALESCE(MAX(`date`),'') as 'maxdate'
            FROM `posxyzread" + suffix + @"`
            WHERE `branchid` = " + branchid + @" 
                AND `terminalno` = " + terminalno + @"
                AND `readtype` = " + type + ")A WHERE  maxdate<>'';";

        DataTable dt = mySQLFunc.getdb(maxdatesql);

        if (dt.Rows.Count <= 0)
            generate_posxyzread_firstday(type, isposd);
        else
            temp_datetime = Convert.ToDateTime(dt.Rows[0]["maxdate"]);

        //Generates All Ungenerated Zreadings until yesterday
        while (temp_datetime.Date < DateTime.Now.AddDays(-1).Date)
        {
            temp_datetime = temp_datetime.AddDays(1);
            generate_posxyzread(temp_datetime, type, isposd);
        }
    }

    public static Int64 get_max_OR_in_posxyzread()
    {
        string sql = @"SELECT max(`endor`) as `maxORWithZread` FROM `posxyzread` WHERE `readtype`=3 AND branchid=" + cls_globalvariables.BranchCode + " AND terminalno=" + cls_globalvariables.terminalno_v;
        DataTable DT = mySQLFunc.getdb(sql);
        if (DT.Rows.Count > 0)
            return Convert.ToInt64(DT.Rows[0]["maxORWithZread"].ToString());
        else
            return 0;
    }

    public static decimal get_all_in_oldgrand(DateTime datetime)
    {
        string branchid = cls_globalvariables.BranchCode;
        string terminalno = cls_globalvariables.terminalno_v;
        string datetimestr = datetime.ToString("yyyy-MM-dd 00:00:00");
        string sql = @"SELECT 
        SUM(`newgrandtotal`-`oldgrandtotal`+`total_discount_amt`+
        ABS(`vat_returns_amt`)+ABS(`nonvat_returns_amt`)) as 'all_in_oldgrand'
        FROM `posxyzread` 
        WHERE `readtype`=3 AND `terminalno`=" + terminalno + @" AND `branchid`=" + branchid + @"
        AND `date` <= '" + datetimestr + @"' 
        GROUP BY `readtype`;";
        DataTable DT = mySQLFunc.getdb(sql);
        if (DT.Rows.Count <= 0)
            return 0;
        else
        {
            decimal allinoldgrand = Convert.ToDecimal(DT.Rows[0]["all_in_oldgrand"].ToString());
            return allinoldgrand;
        }
    }
}
