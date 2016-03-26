using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Printing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using ETech.cls;
using System.IO;
using System.Data;
using System.Windows.Forms;
using ETech.Helpers;

namespace ETech.fnc
{
    class fncHardware
    {
        public static Font font_Title = new Font("Consolas", 14.25F, FontStyle.Bold);
        public static Font font_Price = new Font("Consolas", 9.25F, FontStyle.Bold);
        public static Font font_Content = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
        public static Font font_Change = new Font("Consolas", 12, FontStyle.Bold);
        public static SolidBrush brush_Black = new SolidBrush(Color.Black);

        public static StringFormat format_center()
        {
            StringFormat format_center = new StringFormat();
            format_center.LineAlignment = StringAlignment.Center;
            format_center.Alignment = StringAlignment.Center;
            return format_center;
        }
        public static StringFormat format_left()
        {
            StringFormat format_left = new StringFormat();
            format_left.LineAlignment = StringAlignment.Near;
            format_left.Alignment = StringAlignment.Near;
            return format_left;
        }
        private static StringFormat format_right()
        {
            StringFormat format_right = new StringFormat();
            format_right.LineAlignment = StringAlignment.Far;
            format_right.Alignment = StringAlignment.Far;
            return format_right;
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public static List<Rectangle> create_rect_list(int nX, int nY, int[] widths)
        {
            List<Rectangle> rect = new List<Rectangle>();
            int cur_x = 0;
            foreach (int width in widths)
            {
                rect.Add(new Rectangle(nX + cur_x, nY, width * cls_globalvariables.previewmul, 15));
                cur_x += width * cls_globalvariables.previewmul;
            }
            return rect;
        }
        public static List<StringFormat> create_stringformat_list(int[] formats)
        {
            List<StringFormat> sf = new List<StringFormat>();
            foreach (int format in formats)
            {
                switch (format)
                {
                    case 1: sf.Add(fncHardware.format_left()); break;
                    case 2: sf.Add(fncHardware.format_center()); break;
                    case 3: sf.Add(fncHardware.format_right()); break;
                }
            }
            return sf;
        }
        public static List<Font> create_font_list(int[] fonts)
        {
            List<Font> font_list = new List<Font>();
            foreach (int font in fonts)
            {
                switch (font)
                {
                    case 1: font_list.Add(fncHardware.font_Title); break;
                    case 2: font_list.Add(fncHardware.font_Price); break;
                    case 3: font_list.Add(fncHardware.font_Content); break;
                    case 4: font_list.Add(fncHardware.font_Change); break;
                }
            }
            return font_list;
        }

        private static void printpage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Times New Roman", 12.0f);
            SolidBrush BrushBlack = new SolidBrush(Color.Black);
            g.DrawString("", font, BrushBlack, 0, 0);

            g.Dispose();
        }

        public static void start_print(PrintDocument pd)
        {
            pd.Print();
            //CheckPrinterStatus();
        }

        public static void print_receipt(cls_POSTransaction tran, bool isReprint, bool isVoid)
        {
            if (isReprint)
            {
                string terminalno = cls_globalvariables.terminalno_v;
                string branchid = cls_globalvariables.BranchCode;
                string checkIfVoidSql = @"SELECT Count(*) as cnt FROM Saleshead WHERE (`status`=0) AND `SyncId` = '" + tran.getSyncId() + @"'";
                isVoid = Convert.ToBoolean(mySQLFunc.getdb(checkIfVoidSql).Rows[0]["cnt"]);
            }
            RawPrinterHelper.OpenCashDrawer(true);
            //printpage_receipt_new(null, null, null, tran, isVoid, isReprint);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) => { printpage_receipt(sender, e, null, tran, isVoid, isReprint); };
            start_print(pd);
        }

        public static void void_transaction(cls_POSTransaction tran)
        {
            long lastmodifiedby = tran.get_UserAuthorizer_SyncId();

            mySQLFunc.setdb(@"UPDATE collectionhead SET `lastmodifieddate`=NOW(), `lastmodifiedby`=" + lastmodifiedby + ", `status`=2 WHERE SyncId = ( SELECT `headid` FROM collectionsales where `saleswid` = '" + tran.getSyncId() + "') LIMIT 1");
            mySQLFunc.setdb(@"UPDATE saleshead SET `VoidedOn`=NOW(),`VoidedBy`='" + lastmodifiedby + @"' `lastmodifieddate`=NOW(), `lastmodifiedby`=" + lastmodifiedby + ", `status` = 2 WHERE `SyncId` = '" + tran.getSyncId() + "' LIMIT 1");
            mySQLFunc.setdb_main(@"UPDATE memberpointtrans SET `lastmodifieddate`=NOW(), `lastmodifiedby`=" + lastmodifiedby + ", `show` = 0 WHERE `referencewid` = '" + tran.getSyncId() + "'");


            //void gift check
            string collectionsales_headid = @"SELECT `headid` FROM collectionsales WHERE `saleswid` = " + tran.getSyncId();
            string collectiondetail_wid = @"SELECT `SyncId` FROM collectiondetail WHERE `method` = 7 AND `headid` = (" + collectionsales_headid + ")";
            List<string> collectiondetail_wids = new List<string>();

            DataTable dt = mySQLFunc.getdb(collectiondetail_wid);

            foreach (DataRow row in dt.Rows)
                collectiondetail_wids.Add(row[0].ToString());

            mySQLClass mysqlclass = new mySQLClass();
            mysqlclass.update_synctable("saleshead", tran.getSyncId());
        }

        public static void print_zread(DateTime datetime_d)
        {
            fncHardware.print_zread(datetime_d, 0);
        }
        public static void print_zread(DateTime datetime_d, long userwid)
        {
            if (zreadFunc.getZreadDate(datetime_d).Date <
                zreadFunc.getZreadDate(mySQLFunc.DateTimeNow()).Date)
                print_zread(0, datetime_d, datetime_d, userwid);
            else
            {
                if (DialogHelper.ShowDialog("Are you sure you want to Generate Z reading?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (DialogHelper.ShowDialog("Final Confirmation to Generate Z reading?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        print_zread(0, datetime_d, datetime_d, userwid);
                    }
                }
            }
        }

        public static void print_zread(int printtype, DateTime datetime_d, DateTime datetimeTO_d, long userwid)
        {
            //printtype
            //0-zreading
            //1-terminal accountability
            //2-cashier accountability
            PrintDocument pd = new PrintDocument();

            pd.PrintPage += (sender, e) => { printpage_zread(sender, e, null, printtype, datetime_d, datetimeTO_d); };

            start_print(pd);
            RawPrinterHelper.OpenCashDrawer(true);
        }

        public static List<DataTable> get_summary_data(int type, DateTime datetime_d)
        {
            return get_summary_data(false, type, datetime_d, datetime_d);
        }
        public static List<DataTable> get_summary_data(bool isTerminalaccountability, int type, DateTime datetime_d, DateTime datetimeTO_d)
        {
            List<DataTable> dt_list = new List<DataTable>();
            string sBranchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            string sdateFROM = datetime_d.ToString("yyyy-MM-dd");
            string stimeFROM = datetime_d.ToString("HH:mm:ss");

            string sdateTO = datetimeTO_d.ToString("yyyy-MM-dd");
            string stimeTO = datetimeTO_d.ToString("HH:mm:ss");

            DataRow DRminmax = zreadFunc.get_max_min_OR(3, datetime_d, datetimeTO_d);
            long ornumber_begin = zreadFunc.get_min_OR(3, datetime_d, datetimeTO_d); //DRminmax["minornumber"].ToString();
            string ornumber_end = DRminmax["maxornumber"].ToString();

            string SQL_dr_summary =
                @"SELECT 
                        COALESCE(SUM(`vatable_sale`),0) AS 'vatable_sale',
                        COALESCE(SUM(`vatable_return`),0) AS 'vatable_return',
                        COALESCE(SUM(`nonvat_sale`),0) AS 'nonvat_sale',
                        COALESCE(SUM(`nonvat_return`),0) AS 'nonvat_return',
                        COALESCE(SUM(`senior_sale`),0) AS 'senior_sale',
                        COALESCE(SUM(`senior_return`),0) AS 'senior_return',
                        COALESCE(SUM(`total_discount`),0) AS 'total_discount',
                        COALESCE(SUM(`total_qty`),0) AS 'total_qty',
                        COALESCE(SUM(`total_return_qty`),0) AS 'total_return_qty',
                        COALESCE(SUM(`total_void_qty`),0) AS 'total_void_qty',
                        COALESCE(SUM(`total_void_amount`),0) AS 'total_void_amount',
                        COALESCE(SUM(`totalsalesamount`),0) AS 'totalsalesamount',
                        COALESCE(SUM(`retail_sale`),0) AS 'total_retail_sale',
                        COALESCE(SUM(`wholesale_sale`),0) AS 'total_wholesale_sale',
                        COALESCE(SUM(IF(S.`show` = 1,`cash`,0)),0) AS 'cash',
                        COALESCE(SUM(IF(S.`show` = 1,`bankcheque`,0)),0) AS 'bankcheque',
                        COALESCE(SUM(IF(S.`show` = 1,`creditcard`,0)),0) AS 'creditcard',
                        COALESCE(SUM(IF(S.`show` = 1,`visacreditcard`,0)),0) AS 'visacreditcard',
                        COALESCE(SUM(IF(S.`show` = 1,`mccreditcard`,0)),0) AS 'mccreditcard',
                        COALESCE(SUM(IF(S.`show` = 1,`debitcard`,0)),0) AS 'debitcard',
                        COALESCE(SUM(IF(S.`show` = 1,`giftcheque`,0)),0) AS 'giftcheque',
                        COALESCE(SUM(IF(S.`show` = 1,`mempoints`,0)),0) AS 'mempoints',
                        COALESCE(SUM(IF(S.`show` = 1,`othertender`,0)),0) AS 'othertender',
                        COALESCE(SUM(IF(S.`show` = 1 AND S.`status` = 1,1,0)),0) AS 'success_trans',
                        COALESCE(SUM(IF(S.`show` = 0 OR S.`status` = 0,1,0)),0) AS 'void_trans'
                    FROM
                    (
                        SELECT
                            `shwid`, `userid`, `show`, `status`,
                            SUM(IF(`iswholesale` = 0, `amount`, 0)) AS 'retail_sale',
                            SUM(IF(`iswholesale` = 1, `amount`, 0)) AS 'wholesale_sale',
                            SUM(IF(`isnonvat` = 0 AND `issenior` = 0 AND `amount` > 0,`amount`,0)) AS 'vatable_sale',
                            SUM(IF(`isnonvat` = 0 AND `issenior` = 0 AND `amount` < 0,`amount`,0)) AS 'vatable_return',
                            SUM(IF(`isnonvat` = 1 AND `amount` > 0, `amount`, 0)) AS 'nonvat_sale',
                            SUM(IF(`isnonvat` = 1 AND `amount` < 0, `amount`, 0)) AS 'nonvat_return',
                            SUM(IF(`issenior` = 1 AND `amount` > 0, `amount`, 0)) AS 'senior_sale',
                            SUM(IF(`issenior` = 1 AND `amount` < 0, `amount`, 0)) AS 'senior_return',
                            SUM(IF(`dcamount` > 0 ,`dcamount`,0)) AS 'total_discount',
                            SUM(IF(`qty`> 0,`qty`,0)) AS 'total_qty',
                            SUM(IF(`qty`< 0,`qty`,0)) AS 'total_return_qty',
                            SUM(IF(`voidqty`>0,`voidqty`,0)) AS 'total_void_qty',
                            SUM(IF(`voidamount`>0,`voidamount`,0)) AS 'total_void_amount'
                        FROM
                        (
                            SELECT H.`SyncId` AS 'shwid', H.`userid`, H.`show`, H.`status`,
                                IF( D.`vat` = 0, true, false) AS 'isnonvat', H.`iswholesale`,
                                IF( H.`seniorno` = '0' OR H.`seniorno` = '' OR D.`senior` = 0 OR D.`senior` = 2,false,true) AS 'issenior',
                                IF(`show` = 1 AND `status` = 1, D.`quantity`, 0) AS 'qty',
                                IF(`show` = 0 OR `status` = 0, D.`quantity`, 0) AS 'voidqty',
                                IF(`show` = 1 AND `status` = 1, D.`quantity` * (D.`oprice` - D.`price`),0) AS 'dcamount',
                                IF(`show` = 1 AND `status` = 1, D.`quantity` * D.`price`,0) AS 'amount',
                                IF(`show` = 0 OR  `status` = 0, D.`quantity` * D.`price`,0) AS 'voidamount'
                            FROM `saleshead` AS H LEFT JOIN `salesdetail` AS D ON H.`SyncId` = D.`headid` 
                            WHERE H.`branchid` = " + sBranchid + @" AND H.`terminalno` = " + terminalno +
                            zreadFunc.GetSQLDateRange("H.`date`", datetime_d, datetimeTO_d) + @"
                        ) A
                        GROUP BY A.`shwid`
                    ) AS S
                    LEFT JOIN
                    (
                        SELECT S.`saleswid`, S.`amount` AS 'totalsalesamount', 
                                SUM(IF(D.`method` = 1,D.`amount`,0)) AS 'cash',
                                SUM(IF(D.`method` = 2,D.`amount`,0)) AS 'bankcheque',
                                SUM(IF(D.`method` = 5,D.`amount`,0)) AS 'creditcard',
                                SUM(IF(D.`method` = 6,D.`amount`,0)) AS 'debitcard',
                                SUM(IF(D.`method` = 7 OR D.`method`=13 ,D.`amount`,0)) AS 'giftcheque',
                                SUM(IF(D.`method` = 8,D.`amount`,0)) AS 'mempoints',
                                SUM(IF(D.`method` >= 9,D.`amount`,0)) AS 'othertender'
                        FROM `collectionhead` AS H, `collectiondetail` AS D, `collectionsales` AS S
                        WHERE H.`SyncId` = D.`headid` AND H.`SyncId` = S.`headid` AND H.`show` = 1 " +
                            zreadFunc.GetSQLDateRange("H.`collectiondate`", datetime_d, datetimeTO_d) + @"
                        GROUP BY S.`saleswid`
                    ) AS C ON C.`saleswid` = S.`shwid` 
                    LEFT JOIN
	                    (
		                    SELECT S.`saleswid`,
                            SUM(IF( D.`method` = 5 AND CP.`cardsettingwid` = '4', D.`amount`,0)) AS `visacreditcard`,                    
                            SUM(IF( D.`method` = 5 AND CP.`cardsettingwid` = '5', D.`amount`,0)) AS `mccreditcard`	                    
		                    FROM `collectionhead` AS H, `collectiondetail` AS D, `collectionsales` AS S, `poscardpayment` as CP
		                    WHERE CP.`collectiondetailid`= D.`SyncId` AND H.`SyncId` = D.`headid` AND H.`SyncId` = S.`headid` AND H.`show` = 1 " +
                            zreadFunc.GetSQLDateRange("H.`collectiondate`", datetime_d, datetimeTO_d) + @"
		                    GROUP BY S.`saleswid`
	                    ) AS C2 ON C2.`saleswid` = S.`shwid` 
                    ";
            Console.WriteLine(SQL_dr_summary);

            DataRow dr_summary = mySQLFunc.getdb(SQL_dr_summary).Rows[0];

            decimal all_vatable_sale = Convert.ToDecimal(dr_summary["vatable_sale"]);
            decimal all_vatable_return = Convert.ToDecimal(dr_summary["vatable_return"]);
            decimal all_nonvat_sale = Convert.ToDecimal(dr_summary["nonvat_sale"]);
            decimal all_nonvat_return = Convert.ToDecimal(dr_summary["nonvat_return"]);
            decimal all_senior_sale = Convert.ToDecimal(dr_summary["senior_sale"]);
            decimal all_senior_return = Convert.ToDecimal(dr_summary["senior_return"]);
            decimal all_total_discount = Convert.ToDecimal(dr_summary["total_discount"]);
            decimal all_total_qty = Convert.ToDecimal(dr_summary["total_qty"]);
            decimal all_total_return_qty = Convert.ToDecimal(dr_summary["total_return_qty"]);
            decimal all_total_return_trans = Math.Abs(Convert.ToDecimal(dr_summary["total_return_qty"]));

            decimal all_gross_sales = all_vatable_sale + all_nonvat_sale + all_total_discount;
            decimal all_gross_sales_nodiscount = all_gross_sales;//all_vatable_sale + all_nonvat_sale + all_senior_sale;
            decimal all_net_sale_bf_return = all_gross_sales - all_total_discount;
            decimal all_total_return = (all_vatable_return + all_nonvat_return) * -1;
            decimal all_net_sale_af_return = all_net_sale_bf_return - all_total_return;

            decimal all_total_retail_sale = Convert.ToDecimal(dr_summary["total_retail_sale"]);
            decimal all_total_wholesale_sale = Convert.ToDecimal(dr_summary["total_wholesale_sale"]);

            decimal all_total_sales = Convert.ToDecimal(dr_summary["totalsalesamount"]);
            decimal all_cash_sales = Convert.ToDecimal(dr_summary["cash"]);
            decimal all_bankcheque_sales = Convert.ToDecimal(dr_summary["bankcheque"]);
            decimal all_creditcard_sales = Convert.ToDecimal(dr_summary["creditcard"]);
            decimal all_visacreditcard_sales = Convert.ToDecimal(dr_summary["visacreditcard"]);
            decimal all_mccreditcard_sales = Convert.ToDecimal(dr_summary["mccreditcard"]);
            decimal all_othercreditcard_sales = all_creditcard_sales - all_visacreditcard_sales - all_mccreditcard_sales;
            decimal all_debitcard_sales = Convert.ToDecimal(dr_summary["debitcard"]);
            decimal all_giftcheque_sales = Convert.ToDecimal(dr_summary["giftcheque"]);
            decimal all_mempoints_sales = Convert.ToDecimal(dr_summary["mempoints"]);

            decimal all_othertender_sales = all_total_sales - all_cash_sales - all_bankcheque_sales
                 - all_creditcard_sales - all_debitcard_sales - all_giftcheque_sales - all_mempoints_sales;

            decimal all_dept_sales = all_net_sale_af_return - all_total_sales;
            all_dept_sales = (Math.Abs(all_dept_sales) < 5) ? 0 : all_dept_sales;

            decimal all_void_qty = Convert.ToDecimal(dr_summary["total_void_qty"]);
            decimal all_void_amount = Convert.ToDecimal(dr_summary["total_void_amount"]);
            int all_success_trans = Convert.ToInt32(dr_summary["success_trans"]);
            decimal all_void_trans = 1 + Convert.ToDecimal(ornumber_end) - Convert.ToDecimal(ornumber_begin) - all_success_trans; //Convert.ToInt32(dr_summary["void_trans"]);

            string SQL_reprint_summary =
                @"SELECT COALESCE(SUM(count),0) as `totreprintcount`, COALESCE(SUM(totalamount),0) as `totreprintamount`
                        FROM(
	                        SELECT COUNT(*) as 'count', `salesheadwid`, `totalamount`
	                        FROM (
		                        Select C.`salesheadwid`,
		                        SUM(D.`quantity`*D.`price`) as 'totalamount'
		                        FROM reprintcount as C, saleshead as H, salesdetail as D
		                        WHERE C.`salesheadwid` = H.`SyncId` AND C.`terminalnocreate` = " + terminalno + @" AND H.`SyncId` = D.`headid` " +
                                zreadFunc.GetSQLDateRange("`datecreate`", datetime_d, datetimeTO_d) + @"
		                        GROUP BY C.`id`, C.`salesheadwid`
	                        )A
	                        GROUP BY `salesheadwid`
                        )B";
            Console.WriteLine(SQL_reprint_summary);
            DataRow reprint_summary = mySQLFunc.getdb(SQL_reprint_summary).Rows[0];

            decimal all_reprintamount = Convert.ToDecimal(reprint_summary["totreprintamount"]);
            decimal all_reprintcount = Convert.ToDecimal(reprint_summary["totreprintcount"]);

            decimal t_vat_sale = Math.Round(all_vatable_sale + all_vatable_return, 2, MidpointRounding.AwayFromZero);
            decimal t_gross_vatable_sale = t_vat_sale;
            decimal t_vatable_sale = Math.Round(t_gross_vatable_sale / (1 + cls_globalvariables.vat), 2, MidpointRounding.AwayFromZero);
            decimal t_vat = t_gross_vatable_sale - t_vatable_sale;

            decimal t_nonvat_sale = all_net_sale_af_return - t_gross_vatable_sale; //all_net_sale_af_return - t_gross_vatable_sale - t_vat; //all_nonvat_sale + all_nonvat_return;

            decimal t_senior_sale = all_senior_sale + all_senior_return;
            string sql = @"
                SELECT
	                COALESCE(SUM(sdd.`amount` * sd.`quantity`), 0) AS 'senior_disc'
                FROM 
	                `saleshead` AS sh
                LEFT JOIN
	                `salesdetail` AS sd
	                ON sd.`headid` = sh.`SyncId`
                WHERE
	                sh.`show` = 1 and sh.`status` = 1
                    and `date` between '" + datetime_d.ToString("yyyy-MM-dd") + "' and '" + datetime_d.ToString("yyyy-MM-dd") + " 23:59:59';";

            decimal t_senior_disc = 0;
            if (t_senior_sale != 0)
                decimal.TryParse(mySQLFunc.getdb(sql).Rows[0]["senior_disc"].ToString(), out t_senior_disc);
            //(t_senior_sale / (1 - cls_globalvariables.senior)) * cls_globalvariables.senior;

            decimal endcash = 0;
            decimal pickupcash = 0;
            decimal begincash = 0;
            decimal totalcashreceived = 0;
            decimal cashshortover = 0;
            if (!isTerminalaccountability)
            {
                DataRow dr_cashflow = mySQLFunc.getdb(@"SELECT 
	                                    COALESCE(SUM(IF(`type` = 1,`amount`,0)),0) AS 'begin',
	                                    COALESCE(SUM(IF(`type` = 2,`amount`,0)),0) AS 'pickup',
	                                    COALESCE(SUM(IF(`type` = 3,`amount`,0)),0) AS 'end'
                                    FROM
                                    (
                                    SELECT `type`, SUM(`b1000` * 1000 + `b500` * 500 + `b200` * 200 + `b100` * 100 +
				                                       `b50` * 50 + `b20` * 20 + `c10` * 10 + `c5` * 5 +
				                                       `c1` * 1 + `c25c` * 0.25 + `c10c` * 0.1 + `c5c` * 0.05) AS 'amount'
                                    FROM `poscashdenomination` 
                                    WHERE `branchid` = " + sBranchid + @" AND
	                                    `terminalno` = " + terminalno + @" AND 
	                                    CAST(`datecreated` AS DATE) = '" + sdateFROM + @"'
                                    GROUP BY `type`
                                    ) A").Rows[0];
                endcash = Convert.ToDecimal(dr_cashflow["end"]);
                pickupcash = Convert.ToDecimal(dr_cashflow["pickup"]);
                begincash = Convert.ToDecimal(dr_cashflow["begin"]);
                totalcashreceived = endcash + pickupcash - begincash;
                cashshortover = totalcashreceived - all_cash_sales;
            }
            decimal old_grand = get_old_grandtotal(datetime_d);

            decimal cur_total = Convert.ToDecimal(mySQLFunc.getdb(@"SELECT 
                                        COALESCE(SUM(D.`quantity` * D.`price`),0) AS 'total_amount'
                                    FROM `saleshead` AS H, `salesdetail` AS D
                                    WHERE H.`SyncId` = D.`headid` AND H.`show` = 1 AND H.`status` = 1
	                                    AND H.`terminalno` = " + terminalno + @" AND H.`branchid` = " + sBranchid +
                                        zreadFunc.GetSQLDateRange("H.`date`", datetime_d, datetimeTO_d)).Rows[0]["total_amount"]);

            decimal new_grand = old_grand + cur_total;

            Console.WriteLine(" " + new_grand + " " + cur_total + " " + old_grand);
            DataTable dt_salessummary1 = new DataTable();
            dt_salessummary1.Columns.Add(); dt_salessummary1.Columns.Add();
            dt_salessummary1.Rows.Add("TERMINAL", terminalno);
            dt_salessummary1.Rows.Add("BEGIN OR#", ornumber_begin);
            dt_salessummary1.Rows.Add("END OR#", ornumber_end);
            dt_salessummary1.Rows.Add("  ", "  ");

            if (cls_globalvariables.grossmethod_v == "0")
            {
                dt_salessummary1.Rows.Add("GROSS SALES", all_gross_sales.ToString("N2"));
                dt_salessummary1.Rows.Add("TOTAL DISCOUNT", all_total_discount.ToString("N2"));
                dt_salessummary1.Rows.Add("NET SALE BF RETURN", all_net_sale_bf_return.ToString("N2"));
                dt_salessummary1.Rows.Add("TOTAL RETURN", all_total_return.ToString("N2"));
                dt_salessummary1.Rows.Add("NET SALES AF RETURN", all_net_sale_af_return.ToString("N2"));
            }
            else
            {
                dt_salessummary1.Rows.Add("GROSS SALES w/o DC", all_gross_sales_nodiscount.ToString("N2"));
                dt_salessummary1.Rows.Add("TOTAL RETURN", all_total_return.ToString("N2"));
                dt_salessummary1.Rows.Add("GROSS SALE BF DISCOUNT", (all_gross_sales_nodiscount - all_total_return).ToString("N2"));
                dt_salessummary1.Rows.Add("TOTAL DISCOUNT", all_total_discount.ToString("N2"));
                dt_salessummary1.Rows.Add("GROSS SALES AF DISCOUNT", all_net_sale_af_return.ToString("N2"));
                dt_salessummary1.Rows.Add("TAX", t_vat.ToString("N2"));
                dt_salessummary1.Rows.Add("NET SALES", (all_net_sale_af_return - t_vat).ToString("N2"));
            }

            dt_salessummary1.Rows.Add("  ", "  ");
            dt_salessummary1.Rows.Add("RETAIL SALES", all_total_retail_sale.ToString("N2"));
            dt_salessummary1.Rows.Add("WHOLESALES", all_total_wholesale_sale.ToString("N2"));

            dt_salessummary1.Rows.Add("  ", "  ");
            dt_salessummary1.Rows.Add("CASH SALES", all_cash_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("CREDIT CARD SALES", all_creditcard_sales.ToString("N2"));
            if (cls_globalvariables.showdetailCCinZRead_v == "1")
            {
                dt_salessummary1.Rows.Add("  VISA CARD SALES", all_visacreditcard_sales.ToString("N2"));
                dt_salessummary1.Rows.Add("  MASTERCARD SALES", all_mccreditcard_sales.ToString("N2"));
                dt_salessummary1.Rows.Add("  OTHER CARD SALES", all_othercreditcard_sales.ToString("N2"));
            }
            dt_salessummary1.Rows.Add("DEBIT CARD SALES", all_debitcard_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("BANK CHEQUE SALES", all_bankcheque_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("GIFT CHEQUE SALES", all_giftcheque_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("MEM PTS SALES", all_mempoints_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("OTHER TENDER SALES", all_othertender_sales.ToString("N2"));
            if (all_othertender_sales > 0)
            {
                string SQL = @"SELECT P.`name` as `paymentname`,
	                                   COALESCE(SUM(D.`amount`),0) as `amount`
                                FROM  `collectionhead` as H, 
	                                  `collectiondetail` as D,
	                                  `paymentmethod` as P
                                WHERE H.`SyncId`=D.`headid` AND P.`SyncId`=D.`method` 
	                                AND H.`show`=1 AND H.`status`=1 AND H.`branchid`=" + cls_globalvariables.BranchCode
                                   + zreadFunc.GetSQLDateRange("H.`collectiondate`", datetime_d, datetimeTO_d) + @"
	                                AND D.`method` >= 100
                                GROUP BY D.`method`";
                DataTable DTcustompayments = mySQLFunc.getdb(SQL);
                foreach (DataRow DR in DTcustompayments.Rows)
                {
                    dt_salessummary1.Rows.Add("  " + DR["paymentname"], Convert.ToDecimal(DR["amount"]).ToString("N2"));
                }
            }
            dt_salessummary1.Rows.Add("DEBT SALES", all_dept_sales.ToString("N2"));

            DataTable dt_salessummary2 = new DataTable();
            dt_salessummary2.Columns.Add(); dt_salessummary2.Columns.Add();

            if (!isTerminalaccountability)
            {
                dt_salessummary2.Rows.Add("TRANS END COLLECT", endcash.ToString("N2"));
                dt_salessummary2.Rows.Add("PICKED UP CASH", pickupcash.ToString("N2"));
                dt_salessummary2.Rows.Add("CASH BEGINNING", begincash.ToString("N2"));
                dt_salessummary2.Rows.Add("TOTAL CASH RECEIVE", totalcashreceived.ToString("N2"));
                dt_salessummary2.Rows.Add("POS CASH SALES", all_cash_sales.ToString("N2"));
                dt_salessummary2.Rows.Add("CASH SHORT/OVER", cashshortover.ToString("N2"));
            }
            dt_salessummary2.Rows.Add("  ", "  ");
            dt_salessummary2.Rows.Add("SENIOR DIS", t_senior_disc.ToString("N2"));
            dt_salessummary2.Rows.Add("VATABLE SALES", t_vatable_sale.ToString("N2"));
            dt_salessummary2.Rows.Add("NON-VAT SALES", t_nonvat_sale.ToString("N2"));
            dt_salessummary2.Rows.Add("12% VAT AMT", t_vat.ToString("N2"));
            dt_salessummary2.Rows.Add("  ", "  ");
            dt_salessummary2.Rows.Add("TOTAL QTY SOLD", all_total_qty.ToString());
            dt_salessummary2.Rows.Add("ITEM VOID CNT", all_void_qty.ToString());
            dt_salessummary2.Rows.Add("TRANS VOID AMT", all_void_amount.ToString("N2"));
            dt_salessummary2.Rows.Add("TRANS VOID CNT", Math.Abs(all_void_trans).ToString());
            dt_salessummary2.Rows.Add("SUCCESS TRANS CNT", all_success_trans.ToString());
            dt_salessummary2.Rows.Add("TOTAL TRANS CNT", (all_success_trans + all_void_trans).ToString());
            dt_salessummary2.Rows.Add("RETURN ITEMS CNT", Math.Abs(all_total_return_qty).ToString());
            dt_salessummary2.Rows.Add("  ", "  ");
            dt_salessummary2.Rows.Add("NEW GRAND TOTAL", new_grand.ToString("N2"));
            dt_salessummary2.Rows.Add("OLD GRAND TOTAL", old_grand.ToString("N2"));

            dt_list.Add(dt_salessummary1);
            dt_list.Add(dt_salessummary2);
            return dt_list;
        }

        public static Graphics printpage_zread(object sender, PrintPageEventArgs e, Bitmap bmp,
            int printtype, DateTime datetime_d, DateTime datetimeTO_d)
        {
            //printtype
            //0-zreading
            //1-terminal accountability
            //2-cashier accountability

            string cashieracctcond = "";
            if (printtype == 2 && cls_globalvariables.CashierAcct_wid != "")
                cashieracctcond = " WHERE U.`SyncId` = " + cls_globalvariables.CashierAcct_wid + @" ";

            string sBranchid = cls_globalvariables.BranchCode;
            string sBusinessName = cls_globalvariables.BusinessName_v;
            string sOwner = cls_globalvariables.Owner_v;
            string sAddress = cls_globalvariables.Address_v;
            string sTIN = cls_globalvariables.TIN_v;
            string sACC = cls_globalvariables.ACC_v;
            string sPermitNo = cls_globalvariables.PermitNo_v;
            string sMIN = cls_globalvariables.MIN_v;
            string sSerial = cls_globalvariables.Serial_v;
            string terminalno = cls_globalvariables.terminalno_v;
            string cdate = datetime_d.ToString("yyyy-MM-dd");
            string ctime = datetime_d.ToString("hh:mm:ss");
            string cdateTO = datetimeTO_d.ToString("yyyy-MM-dd");
            string ctimeTO = datetimeTO_d.ToString("hh:mm:ss");

            DataTable dt_header1 = new DataTable();
            dt_header1.Columns.Add();
            dt_header1.Rows.Add(sOwner);
            dt_header1.Rows.Add(sAddress);
            dt_header1.Rows.Add(sTIN);
            dt_header1.Rows.Add(sACC);
            dt_header1.Rows.Add(sPermitNo);
            dt_header1.Rows.Add(sSerial);
            dt_header1.Rows.Add(sMIN);

            if (printtype != 0)
            {
                dt_header1.Rows.Add("");
                dt_header1.Rows.Add("PRINTED: " + DateTime.Now);
                dt_header1.Rows.Add("Terminal No: " + cls_globalvariables.terminalno_v);
                dt_header1.Rows.Add("");
            }

            string sdate = datetime_d.ToString("yyyy-MM-dd");
            string readcount = "0";
            if (printtype == 0)
                readcount = zreadFunc.get_readcount(datetime_d).ToString();

            DataTable dt_header2 = new DataTable();
            dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add();

            if (printtype != 0)
                dt_header2.Rows.Add("Date from:", cdate + " ", " Date to:", cdateTO);
            else
                dt_header2.Rows.Add("Date:", cdate + " ", " Time:", ctime);

            DataTable dt_tblheader = new DataTable();
            dt_tblheader.Columns.Add(); dt_tblheader.Columns.Add();
            dt_tblheader.Rows.Add("Description", "QTY / AMOUNT");

            String SQLusersalessummary =
                @"SELECT 
                    S.`userid`, CONCAT(U.`usercode`,' ',U.`fullname`) AS 'user_code_name',
                    COALESCE(SUM(`vatable_sale`),0) AS 'vatable_sale',
                    COALESCE(SUM(`vatable_return`),0) AS 'vatable_return',
                    COALESCE(SUM(`nonvat_sale`),0) AS 'nonvat_sale',
                    COALESCE(SUM(`nonvat_return`),0) AS 'nonvat_return',
                    COALESCE(SUM(`senior_sale`),0) AS 'senior_sale',
                    COALESCE(SUM(`senior_return`),0) AS 'senior_return',
                    COALESCE(SUM(`total_discount`),0) AS 'total_discount',
                    COALESCE(SUM(`total_qty`),0) AS 'total_qty',
                    COALESCE(SUM(`total_return_qty`),0) AS 'total_return_qty',
                    COALESCE(SUM(`total_void_qty`),0) AS 'total_void_qty',
                    COALESCE(SUM(`total_void_amount`),0) AS 'total_void_amount',
                    COALESCE(SUM(`totalsalesamount`),0) AS 'totalsalesamount',
                    COALESCE(SUM(IF(S.`show` = 1,`cash`,0)),0) AS 'cash',
                    COALESCE(SUM(IF(S.`show` = 1,`bankcheque`,0)),0) AS 'bankcheque',
                    COALESCE(SUM(IF(S.`show` = 1,`visacreditcard`,0)),0) AS 'visacreditcard',
                    COALESCE(SUM(IF(S.`show` = 1,`mccreditcard`,0)),0) AS 'mccreditcard',
                    COALESCE(SUM(IF(S.`show` = 1,`creditcard`,0)),0) AS 'creditcard',
                    COALESCE(SUM(IF(S.`show` = 1,`debitcard`,0)),0) AS 'debitcard',
                    COALESCE(SUM(IF(S.`show` = 1,`giftcheque`,0)),0) AS 'giftcheque',
                    COALESCE(SUM(IF(S.`show` = 1,`mempoints`,0)),0) AS 'mempoints',
                    COALESCE(SUM(IF(S.`show` = 1,`othertender`,0)),0) AS 'othertender',
                    COALESCE(SUM(IF(S.`show` = 1 AND S.`status` = 1,1,0)),0) AS 'success_trans',
                    COALESCE(SUM(IF(S.`show` = 0 OR S.`status` = 0,1,0)),0) AS 'void_trans'
                FROM
                (
                    SELECT
                        `shwid`, `userid`, `show`, `status`,
                        SUM(IF(`isnonvat` = 0 AND `issenior` = 0 AND `amount` > 0,`amount`,0)) AS 'vatable_sale',
                        SUM(IF(`isnonvat` = 0 AND `issenior` = 0 AND `amount` < 0,`amount`,0)) AS 'vatable_return',
                        SUM(IF(`isnonvat` = 1 AND `amount` > 0, `amount`, 0)) AS 'nonvat_sale',
                        SUM(IF(`isnonvat` = 1 AND `amount` < 0, `amount`, 0)) AS 'nonvat_return',
                        SUM(IF(`issenior` = 1 AND `amount` > 0, `amount`, 0)) AS 'senior_sale',
                        SUM(IF(`issenior` = 1 AND `amount` < 0, `amount`, 0)) AS 'senior_return',
                        SUM(IF(`dcamount` > 0 ,`dcamount`,0)) AS 'total_discount',
                        SUM(IF(`qty`> 0,`qty`,0)) AS 'total_qty',
                        SUM(IF(`qty`< 0,`qty`,0)) AS 'total_return_qty',
                        SUM(IF(`voidqty`>0,`voidqty`,0)) AS 'total_void_qty',
                        SUM(IF(`voidamount`>0,`voidamount`,0)) AS 'total_void_amount'
                    FROM
                    (
                        SELECT H.`SyncId` AS 'shwid', H.`userid`, H.`show`, H.`status`,
                            IF( D.`vat` = 0, true, false) AS 'isnonvat', H.`iswholesale`,
                            IF( H.`seniorno` = '0' OR H.`seniorno` = '' OR D.`senior` = 0 OR D.`senior` = 2,false,true) AS 'issenior',
                            IF(`show` = 1 AND `status` = 1, D.`quantity`, 0) AS 'qty',
                            IF(`show` = 0 OR `status` = 0, D.`quantity`, 0) AS 'voidqty',
                            IF(`show` = 1 AND `status` = 1, D.`quantity` * (D.`oprice` - D.`price`),0) AS 'dcamount',
                            IF(`show` = 1 AND `status` = 1, D.`quantity` * D.`price`,0) AS 'amount',
                            IF(`show` = 0 or `status` = 0, D.`quantity` * D.`price`,0) AS 'voidamount'
                        FROM `saleshead` AS H LEFT JOIN `salesdetail` AS D ON H.`SyncId` = D.`headid` 
                        WHERE H.`branchid` = " + sBranchid + @" AND H.`terminalno` = " + terminalno +
                        zreadFunc.GetSQLDateRange("H.`date`", datetime_d, datetimeTO_d) + @"
                    ) A
                    GROUP BY A.`shwid`
                ) AS S
                LEFT JOIN
                (
                    SELECT S.`saleswid`, S.`amount` AS 'totalsalesamount', 
                            SUM(IF(D.`method` = 1,D.`amount`,0)) AS 'cash',
                            SUM(IF(D.`method` = 2,D.`amount`,0)) AS 'bankcheque',
                            SUM(IF(D.`method` = 5,D.`amount`,0)) AS 'creditcard',
                            SUM(IF(D.`method` = 6,D.`amount`,0)) AS 'debitcard',
                            SUM(IF(D.`method` = 7 OR D.`method` = 13,D.`amount`,0)) AS 'giftcheque',
                            SUM(IF(D.`method` = 8,D.`amount`,0)) AS 'mempoints',
                            SUM(IF(D.`method` >= 9,D.`amount`,0)) AS 'othertender'
                    FROM `collectionhead` AS H, `collectiondetail` AS D, `collectionsales` AS S
                    WHERE H.`SyncId` = D.`headid` AND H.`SyncId` = S.`headid` AND H.`show` = 1" +
                    zreadFunc.GetSQLDateRange("H.`collectiondate`", datetime_d, datetimeTO_d) + @"
                    GROUP BY S.`saleswid`
                ) AS C ON C.`saleswid` = S.`shwid`
                LEFT JOIN
                (
                    SELECT S.`saleswid`,
                    SUM(IF( D.`method` = 5 AND CP.`cardsettingwid` = '4', D.`amount`,0)) AS 'visacreditcard',                    
                    SUM(IF( D.`method` = 5 AND CP.`cardsettingwid` = '5', D.`amount`,0)) AS 'mccreditcard'
                    FROM `collectionhead` AS H, `collectiondetail` AS D, `collectionsales` AS S, `poscardpayment` as CP
                    WHERE CP.`collectiondetailid`= D.`SyncId` AND H.`SyncId` = D.`headid` AND H.`SyncId` = S.`headid` AND H.`show` = 1 " +
                    zreadFunc.GetSQLDateRange("H.`collectiondate`", datetime_d, datetimeTO_d) + @"
                    GROUP BY S.`saleswid`
                ) AS C2 ON C2.`saleswid` = S.`shwid` 
                LEFT JOIN `user` AS U ON U.`SyncId` = S.`userid` 
                " + cashieracctcond + @"
                GROUP BY S.`userid`";
            DataTable dt_usersalessummary = mySQLFunc.getdb(SQLusersalessummary);

            List<string> cashier_namelist = new List<string>();
            List<DataTable> dt_cashier_salessummarylist = new List<DataTable>();

            foreach (DataRow dr in dt_usersalessummary.Rows)
            {
                string user_code_name = dr["user_code_name"].ToString();
                cashier_namelist.Add(user_code_name);

                decimal vatable_sale = Convert.ToDecimal(dr["vatable_sale"]);
                decimal vatable_return = Convert.ToDecimal(dr["vatable_return"]);
                decimal nonvat_sale = Convert.ToDecimal(dr["nonvat_sale"]);
                decimal nonvat_return = Convert.ToDecimal(dr["nonvat_return"]);
                decimal senior_sale = Convert.ToDecimal(dr["senior_sale"]);
                decimal senior_return = Convert.ToDecimal(dr["senior_return"]);
                decimal total_discount = Convert.ToDecimal(dr["total_discount"]);
                decimal total_qty = Convert.ToDecimal(dr["total_qty"]);
                decimal total_return_qty = Convert.ToDecimal(dr["total_return_qty"]);

                decimal gross_sales = vatable_sale + nonvat_sale + total_discount;
                decimal net_sale_bf_return = gross_sales - total_discount;
                decimal total_return = (vatable_return + nonvat_return + senior_return) * -1;
                decimal net_sale_af_return = net_sale_bf_return - total_return;

                decimal total_sales_amount = Convert.ToDecimal(dr["totalsalesamount"]);
                decimal cash_sales = Convert.ToDecimal(dr["cash"]);
                decimal bankcheque_sales = Convert.ToDecimal(dr["bankcheque"]);
                decimal creditcard_sales = Convert.ToDecimal(dr["creditcard"]);
                decimal visacreditcard_sales = Convert.ToDecimal(dr["visacreditcard"]);
                decimal mccreditcard_sales = Convert.ToDecimal(dr["mccreditcard"]);
                decimal othercreditcard_sales = creditcard_sales - visacreditcard_sales - mccreditcard_sales;
                decimal debitcard_sales = Convert.ToDecimal(dr["debitcard"]);
                decimal giftcheque_sales = Convert.ToDecimal(dr["giftcheque"]);
                decimal mempoints_sales = Convert.ToDecimal(dr["mempoints"]);
                decimal othertender_sales = total_sales_amount
                                        - cash_sales - bankcheque_sales
                                        - creditcard_sales - debitcard_sales
                                        - giftcheque_sales - mempoints_sales;

                decimal dept_sales = net_sale_af_return - total_sales_amount;
                dept_sales = (Math.Abs(dept_sales) < 5) ? 0 : dept_sales;

                decimal void_qty = Convert.ToDecimal(dr["total_void_qty"]);
                decimal void_amount = Convert.ToDecimal(dr["total_void_amount"]);
                //int total_trans = Convert.ToInt32(dr["total_trans_count"]);
                //int success_trans = Convert.ToInt32(dr["success_trans"]);
                //int void_trans = total_trans - success_trans;//Convert.ToInt32(dr["void_trans"]);

                DataTable dt_cashier_salessummary = new DataTable();
                dt_cashier_salessummary.Columns.Add(); dt_cashier_salessummary.Columns.Add();
                if (cls_globalvariables.grossmethod_v == "0")
                {
                    dt_cashier_salessummary.Rows.Add("GROSS SALES", gross_sales.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("TOTAL DISCOUNT", total_discount.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("NET SALE BF RETURN", net_sale_bf_return.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("TOTAL RETURN", total_return.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("NET SALES AF RETURN", net_sale_af_return.ToString("N2"));
                }
                else
                {
                    dt_cashier_salessummary.Rows.Add("GROSS SALES", gross_sales.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("TOTAL RETURN", total_return.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("NET SALE BF DISCOUNT", (gross_sales - total_return).ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("TOTAL DISCOUNT", total_discount.ToString("N2"));
                    dt_cashier_salessummary.Rows.Add("NET SALES AF DISCOUNT", net_sale_af_return.ToString("N2"));
                }
                dt_cashier_salessummary.Rows.Add("  ", "  ");
                dt_cashier_salessummary.Rows.Add("CASH SALES", cash_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("CREDIT CARD SALES", creditcard_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("DEBIT CARD SALES", debitcard_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("BANK CHEQUE SALES", bankcheque_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("GIFT CHEQUE SALES", giftcheque_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("MEM PTS SALES", mempoints_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("OTHER TENDER SALES", othertender_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("DEBT SALES", dept_sales.ToString("N2"));
                dt_cashier_salessummary.Rows.Add("  ", "  ");
                dt_cashier_salessummary.Rows.Add("TOTAL QTY SOLD", total_qty.ToString());
                dt_cashier_salessummary.Rows.Add("ITEM VOID CNT", void_qty.ToString());
                dt_cashier_salessummary.Rows.Add("TRANS VOID AMT", void_amount.ToString("N2"));
                //dt_cashier_salessummary.Rows.Add("TRANS VOID CNT", Math.Abs(void_trans).ToString());
                //dt_cashier_salessummary.Rows.Add("SUCCESS TRANS CNT", success_trans.ToString());
                //dt_cashier_salessummary.Rows.Add("TOTAL TRANS CNT", (success_trans + void_trans).ToString());
                dt_cashier_salessummary.Rows.Add("RETURN ITEMS CNT", Math.Abs(total_return_qty).ToString());

                dt_cashier_salessummarylist.Add(dt_cashier_salessummary);
            }

            List<DataTable> list_dtsummary = (printtype != 0) ?
                get_summary_data(true, 3, datetime_d, datetimeTO_d) :
                get_summary_data(3, datetime_d);

            Graphics g;
            if (bmp == null)
            {
                g = e.Graphics;
                zreadFunc.generate_posxyzread(datetime_d);
            }
            else
            {
                g = Graphics.FromImage(bmp);
                cls_globalvariables.previewmul = 2;
            }

            int nY = 0;
            int nX = 0;
            int maxwidth = 280;
            int[] column2format = new int[] { 160, 120 };
            int[] column4_rect_header2 = new int[] { 70, 70, 70, 70 };
            if (cls_globalvariables.print_receipt_format_v == "76mm")
            {
                column4_rect_header2 = new int[] { 50, 70, 50, 70 };
                column2format = new int[] { 120, 120 };
                maxwidth = 240;
            }
            else if (cls_globalvariables.print_receipt_format_v == "76mm_journal")
            {
                column4_rect_header2 = new int[] { 56, 70, 57, 70 };
                column2format = new int[] { 133, 120 };
                maxwidth = 253;
            }

            //business Title
            Rectangle rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
            nY += DrawString(g, sBusinessName, fncHardware.font_Title, rect_title, fncHardware.brush_Black, fncHardware.format_center());


            //header 1
            List<Rectangle> rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_header1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);

            rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
            string printtitle = "Terminal Report Z-Read";
            if (printtype == 1) printtitle = "Terminal Accountability Report";
            else if (printtype == 2) printtitle = "Cashier Accountability Report";
            nY += DrawString(g, printtitle, fncHardware.font_Change, rect_title, fncHardware.brush_Black, fncHardware.format_center());
            rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
            if (printtype == 0)
                nY += DrawString(g, "Terminal Reading Counter: " + readcount, fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());

            //header 2
            List<Rectangle> rect_header2 = fncHardware.create_rect_list(nX, nY, column4_rect_header2);
            List<StringFormat> sf_header2 = fncHardware.create_stringformat_list(new int[] { 1, 3, 1, 3 });
            List<Font> font_header2 = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
            nY = DrawStringDataTable(g, dt_header2, font_header2, rect_header2, fncHardware.brush_Black, sf_header2);

            //space
            nY += 10;

            //table header
            List<Rectangle> rect_tblheader = fncHardware.create_rect_list(nX, nY, column2format);
            List<StringFormat> sf_tblheader = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            List<Font> font_tblheader = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, dt_tblheader, font_tblheader, rect_tblheader, fncHardware.brush_Black, sf_tblheader);

            for (int i = 0; i < cashier_namelist.Count; i++)
            {
                //space
                nY += 10;

                //-----------line-------------
                nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

                //Cashier info
                rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
                nY += DrawString(g, "CASHIER: " + cashier_namelist[i], fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());

                //-----------line-------------
                nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

                //space
                nY += 10;

                DataTable dt_cashier_salessummary = dt_cashier_salessummarylist[i];
                List<Rectangle> rect_tbldata = fncHardware.create_rect_list(nX, nY, column2format);
                List<StringFormat> sf_tbldata = fncHardware.create_stringformat_list(new int[] { 1, 3 });
                List<Font> font_tbldata = fncHardware.create_font_list(new int[] { 3, 3 });
                nY = DrawStringDataTable(g, dt_cashier_salessummary, font_tbldata, rect_tbldata, fncHardware.brush_Black, sf_tbldata);

            }

            //space
            nY += 10;

            //-----------line-------------
            nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            //Cashier info
            rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);

            if (printtype == 1)
                nY += DrawString(g, "*** TERMINAL ACCOUNTABILITY ***", fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());
            else if (printtype == 0)
                nY += DrawString(g, "*** Z-READING SUMMARY ***", fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());
            else //(printtype == 2)
            {
                nY += DrawString(g, "---end of report---", fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_center());
                cls_globalvariables.previewmul = 1;
                return g;
            }
            //-----------line-------------
            nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            //space
            nY += 10;

            List<Rectangle> rect_salessummary = fncHardware.create_rect_list(nX, nY, column2format);
            List<StringFormat> sf_salessummary = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            List<Font> font_salessummary = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, list_dtsummary[0], font_salessummary, rect_salessummary, fncHardware.brush_Black, sf_salessummary);


            List<Rectangle> rect_salessummary2 = fncHardware.create_rect_list(nX, nY, column2format);
            nY = DrawStringDataTable(g, list_dtsummary[1], font_salessummary, rect_salessummary2, fncHardware.brush_Black, sf_salessummary);

            cls_globalvariables.previewmul = 1;
            return g;
        }

        public static DataTable get_header1()
        {
            //header 1
            string sOwner = cls_globalvariables.Owner_v;
            string sAddress = cls_globalvariables.Address_v;
            string sTIN = cls_globalvariables.TIN_v;
            string sACC = cls_globalvariables.ACC_v;
            string sPermitNo = cls_globalvariables.PermitNo_v;
            string sMIN = cls_globalvariables.MIN_v;
            string sSerial = cls_globalvariables.Serial_v;

            DataTable dt_header1 = new DataTable();
            dt_header1.Columns.Add();
            dt_header1.Rows.Add(sOwner);
            dt_header1.Rows.Add(sAddress);
            dt_header1.Rows.Add(sTIN);
            dt_header1.Rows.Add(sACC);
            dt_header1.Rows.Add(sPermitNo);
            dt_header1.Rows.Add(sSerial);
            dt_header1.Rows.Add(sMIN);

            return dt_header1;
        }
        public static DataRow get_summary_data(DateTime datetime_d)
        {
            string sBranchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;
            string sdate = datetime_d.ToString("yyyy-MM-dd");
            string stime = datetime_d.ToString("HH:mm:ss");

            string sql = @"SELECT 
                        COALESCE(ROUND(CAST(SUM(`vatable_sale`) AS DECIMAL(10,3)),2),0) AS 'vatable_sale',
                        COALESCE(ROUND(CAST(SUM(`vatable_return`) AS DECIMAL(10,3)),2),0) AS 'vatable_return',
                        COALESCE(ROUND(CAST(SUM(`nonvat_sale`) AS DECIMAL(10,3)),2),0) AS 'nonvat_sale',
                        COALESCE(ROUND(CAST(SUM(`nonvat_return`) AS DECIMAL(10,3)),2),0) AS 'nonvat_return',
                        COALESCE(ROUND(CAST(SUM(`senior_sale`) AS DECIMAL(10,3)),2),0) AS 'senior_sale',
                        COALESCE(ROUND(CAST(SUM(`senior_return`) AS DECIMAL(10,3)),2),0) AS 'senior_return',
                        COALESCE(ROUND(CAST(SUM(`total_discount`) AS DECIMAL(10,3)),2),0) AS 'total_discount',
                        COALESCE(ROUND(CAST(SUM(`total_qty`) AS DECIMAL(10,3)),2),0) AS 'total_qty',
                        COALESCE(ROUND(CAST(SUM(`total_return_qty`) AS DECIMAL(10,3)),2),0) AS 'total_return_qty',
                        COALESCE(ROUND(CAST(SUM(`total_void_qty`) AS DECIMAL(10,3)),2),0) AS 'total_void_qty',
                        COALESCE(ROUND(CAST(SUM(`total_void_amount`) AS DECIMAL(10,3)),2),0) AS 'total_void_amount',
                        COALESCE(ROUND(CAST(SUM(`totalsalesamount`) AS DECIMAL(10,3)),2),0) AS 'totalsalesamount',
                        COALESCE(ROUND(CAST(SUM(`retail_sale`) AS DECIMAL(10,3)),2),0) AS 'total_retail_sale',
                        COALESCE(ROUND(CAST(SUM(`wholesale_sale`) AS DECIMAL(10,3)),2),0) AS 'total_wholesale_sale',
                        COALESCE(ROUND(CAST(SUM(IF(S.`show` = 1,`cash`,0)) AS DECIMAL(10,3)),2),0) AS 'cash',
                        COALESCE(ROUND(CAST(SUM(IF(S.`show` = 1,`bankcheque`,0)) AS DECIMAL(10,3)),2),0) AS 'bankcheque',
                        COALESCE(ROUND(CAST(SUM(IF(S.`show` = 1,`creditcard`,0)) AS DECIMAL(10,3)),2),0) AS 'creditcard',
                        COALESCE(ROUND(CAST(SUM(IF(S.`show` = 1,`debitcard`,0)) AS DECIMAL(10,3)),2),0) AS 'debitcard',
                        COALESCE(ROUND(CAST(SUM(IF(S.`show` = 1,`giftcheque`,0)) AS DECIMAL(10,3)),2),0) AS 'giftcheque',
                        COALESCE(ROUND(CAST(SUM(IF(S.`show` = 1,`memberpoints`,0)) AS DECIMAL(10,3)),2),0) AS 'memberpoints',
                        COALESCE(COUNT(*),0) AS 'all_trans',
                        COALESCE(SUM(IF(S.`show` = 1 AND S.`status` = 1,1,0)),0) AS 'success_trans',
                        COALESCE(SUM(IF(S.`show` = 0 OR S.`status` = 0,1,0)),0) AS 'void_trans'
                    FROM
                    (
                        SELECT
                            `shwid`, `userid`, `show`, `status`,
                            SUM(IF(`iswholesale` = 0, `amount`, 0)) AS 'retail_sale',
                            SUM(IF(`iswholesale` = 1, `amount`, 0)) AS 'wholesale_sale',
                            SUM(IF(`isnonvat` = 0 AND `issenior` = 0 AND `amount` > 0,`amount`,0)) AS 'vatable_sale',
                            SUM(IF(`isnonvat` = 0 AND `issenior` = 0 AND `amount` < 0,`amount`,0)) AS 'vatable_return',
                            SUM(IF(`isnonvat` = 1 AND `amount` > 0, `amount`, 0)) AS 'nonvat_sale',
                            SUM(IF(`isnonvat` = 1 AND `amount` < 0, `amount`, 0)) AS 'nonvat_return',
                            SUM(IF(`issenior` = 1 AND `amount` > 0, `amount`, 0)) AS 'senior_sale',
                            SUM(IF(`issenior` = 1 AND `amount` < 0, `amount`, 0)) AS 'senior_return',
                            SUM(IF(`dcamount` > 0 ,`dcamount`,0)) AS 'total_discount',
                            SUM(IF(`qty`> 0,`qty`,0)) AS 'total_qty',
                            SUM(IF(`qty`< 0,`qty`,0)) AS 'total_return_qty',
                            SUM(IF(`voidqty`>0,`voidqty`,0)) AS 'total_void_qty',
                            SUM(IF(`voidamount`>0,`voidamount`,0)) AS 'total_void_amount'
                        FROM
                        (
                            SELECT H.`SyncId` AS 'shwid', H.`userid`, H.`show`, H.`status`,
                                IF( D.`vat` = 0, true, false) AS 'isnonvat', H.`iswholesale`,
                                IF( H.`seniorno` = '0' OR H.`seniorno` = '' OR D.`senior` = 0 OR D.`senior` = 2,false,true) AS 'issenior',
                                IF(`status` = 1, D.`quantity`, 0) AS 'qty',
                                IF(`status` = 0, D.`quantity`, 0) AS 'voidqty',
                                IF(`status` = 1, D.`quantity` * (D.`oprice` - D.`price`),0) AS 'dcamount',
                                IF(`status` = 1, D.`quantity` * D.`price`,0) AS 'amount',
                                IF(status` = 0, D.`quantity` * D.`price`,0) AS 'voidamount'
                            FROM `saleshead` AS H 
                            LEFT JOIN `salesdetail` AS D ON H.`SyncId` = D.`headid` 
                            WHERE H.`branchid` = " + sBranchid + @" AND H.`terminalno` = " + terminalno + @"
                                AND H.`date` >= '" + sdate + @" 00:00:00'
                                AND H.`date` <= '" + sdate + @" 23:59:59'
                        ) A
                        GROUP BY A.`shwid`
                    ) AS S
                    LEFT JOIN
                    (
                        SELECT S.`saleswid`, S.`amount` AS 'totalsalesamount', 
                                SUM(IF(D.`method` = 1,D.`amount`,0)) AS 'cash',
                                SUM(IF(D.`method` = 2,D.`amount`,0)) AS 'bankcheque',
                                SUM(IF(D.`method` = 5,D.`amount`,0)) AS 'creditcard',
                                SUM(IF(D.`method` = 6,D.`amount`,0)) AS 'debitcard',
                                SUM(IF(D.`method` = 7 OR D.`method` = 13,D.`amount`,0)) AS 'giftcheque',
                                SUM(IF(D.`method` = 8,D.`amount`,0)) AS 'memberpoints'
                        FROM `collectionhead` AS H, 
                            `collectiondetail` AS D, 
                            `collectionsales` AS S
                        WHERE H.`SyncId` = D.`headid` AND H.`SyncId` = S.`headid` AND H.`show` = 1 
                            AND H.`collectiondate` >= '" + sdate + @" 00:00:00'
                            AND H.`collectiondate` <= '" + sdate + @" 23:59:59'
                        GROUP BY S.`saleswid`
                    ) AS C ON C.`saleswid` = S.`shwid` ";
            Console.WriteLine(sql);
            DataRow dr_summary = mySQLFunc.getdb(sql).Rows[0];

            return dr_summary;
        }
        public static DataRow get_cashflow_data(DateTime datetime_d)
        {

            string sBranchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;
            string sdate = datetime_d.ToString("yyyy-MM-dd");

            string sql = @"SELECT COALESCE(SUM(IF(`type` = 1,`amount`,0)),0) AS 'begin',
                            COALESCE(SUM(IF(`type` = 2,`amount`,0)),0) AS 'pickup',
                            COALESCE(SUM(IF(`type` = 3,`amount`,0)),0) AS 'end'
                        FROM
                        (
                        SELECT `type`, SUM(`b1000` * 1000 + `b500` * 500 + `b200` * 200 + `b100` * 100 +
	                                       `b50` * 50 + `b20` * 20 + `c10` * 10 + `c5` * 5 +
	                                       `c1` * 1 + `c25c` * 0.25 + `c10c` * 0.1 + `c5c` * 0.05) AS 'amount'
                        FROM `poscashdenomination` 
                        WHERE `branchid` = " + sBranchid + @" AND
                            `terminalno` = " + terminalno +
                        zreadFunc.GetSQLDateRange("`datecreated`", datetime_d, datetime_d) + @"
                        GROUP BY `type`
                        ) A";
            Console.WriteLine(sql);
            DataTable DT = mySQLFunc.getdb(sql);
            if (DT.Rows.Count == 0) return null;
            else return DT.Rows[0];
        }

        public static decimal get_old_grandtotal(DateTime dDateTime)
        {
            string terminalno = cls_globalvariables.terminalno_v;
            string sBranchid = cls_globalvariables.BranchCode;

            string prev_date = dDateTime.AddDays(-1).ToString("yyyy-MM-dd");
            string this_date = dDateTime.ToString("yyyy-MM-dd");

            DateTime prev_date_d = dDateTime.AddDays(-1);

            string prev_new_grand_sql = @"SELECT COALESCE(`newgrandtotal`, 0) AS 'grandtotal' 
                                    FROM `posxyzread`
                                    WHERE `terminalno` = " + terminalno + @"
                                        AND `branchid` = " + sBranchid +
                        zreadFunc.GetSQLDateRange("`date`", prev_date_d, prev_date_d);
            Console.WriteLine(prev_new_grand_sql);
            DataTable dt = mySQLFunc.getdb(prev_new_grand_sql);
            decimal prev_new_grand = 0;
            if (dt.Rows.Count > 0)
            {
                prev_new_grand = Convert.ToDecimal(dt.Rows[0]["grandtotal"]);
            }

            if (prev_new_grand == 0)
            {
                string old_grand_sql = @"SELECT 
                                        COALESCE(SUM(D.`quantity` * D.`price`),0) AS 'total_amount'
                                    FROM `saleshead` AS H, `salesdetail` AS D
                                    WHERE H.`SyncId` = D.`headid` AND H.`show` = 1 AND H.`status` = 1
	                                    AND H.`terminalno` = " + terminalno + @" AND H.`branchid` = " + sBranchid + @"
	                                    AND H.`date` > '2013-01-15 00:00:00' 
	                                    AND H.`date` < '" + this_date + @" 00:00:00'";
                dt = mySQLFunc.getdb(old_grand_sql);
                prev_new_grand = Convert.ToDecimal(dt.Rows[0]["total_amount"]);
            }

            return prev_new_grand;
        }

        public static Graphics render_reading(PrintPageEventArgs e, Bitmap bmp, DateTime datetime_d, DataRow dr_summary, DataRow dr_cashflow)
        {
            //80mm: maxwidth 280
            //76mm: maxwidth 240
            //76mm_journal: maxwidth 253
            //57mm: maxwidth 180

            DateTime mindate = Convert.ToDateTime(dr_summary["mindate"]);
            DateTime maxdate = Convert.ToDateTime(dr_summary["maxdate"]);
            bool isterminalaccountability = (mindate.Date != maxdate.Date) ? true : false;

            string readcount = dr_summary["readcount"].ToString();
            string ornumber_begin = dr_summary["startor"].ToString();
            string ornumber_end = dr_summary["endor"].ToString();
            decimal new_grand = Convert.ToDecimal(dr_summary["newgrandtotal"]);
            decimal old_grand = Convert.ToDecimal(dr_summary["oldgrandtotal"]);

            int nY = 0;
            int nX = 0;
            int[] column2format = new int[] { 160, 120 };
            int[] column4_rect_header2 = new int[] { 70, 70, 70, 70 };
            int maxwidth = 280;
            if (cls_globalvariables.print_receipt_format_v == "76mm")
            {
                column4_rect_header2 = new int[] { 44, 70, 44, 70 };
                column2format = new int[] { 120, 120 };
                maxwidth = 240;
            }
            else if (cls_globalvariables.print_receipt_format_v == "57mm")
            {
                column2format = new int[] { 95, 85 };
                maxwidth = 180;
            }
            else if (cls_globalvariables.print_receipt_format_v == "76mm_journal")
            {
                column4_rect_header2 = new int[] { 56, 70, 56, 70 };
                column2format = new int[] { 133, 120 };
                maxwidth = 253;
            }

            Graphics g;
            if (bmp == null)
                g = e.Graphics;
            else
            {
                g = Graphics.FromImage(bmp);
                cls_globalvariables.previewmul = 2;
            }

            //business Title
            string sBusinessName = cls_globalvariables.BusinessName_v;
            Rectangle rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
            nY += DrawString(g, sBusinessName, fncHardware.font_Title, rect_title, fncHardware.brush_Black, fncHardware.format_center());

            //header 1
            DataTable dt_header1 = get_header1();
            List<Rectangle> rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_header1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);


            rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);

            if (isterminalaccountability)
                nY += DrawString(g, "Terminal Accountability", font_Change, rect_title, brush_Black, format_center());
            else
            {
                nY += DrawString(g, "Terminal Report Z-Read", font_Change, rect_title, brush_Black, format_center());
                rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
                nY += DrawString(g, "Terminal Reading Counter: " + readcount, fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());
            }

            //header 2
            string cdate = datetime_d.ToString("MM/dd/yyyy");
            string ctime = datetime_d.ToString("HH:mm:ss");
            DataTable dt_header2 = new DataTable();

            if (cls_globalvariables.print_receipt_format_v == "57mm")
            {
                dt_header2.Columns.Add(); dt_header2.Columns.Add();
                dt_header2.Rows.Add("Date:", cdate);
                dt_header2.Rows.Add("Time:", mindate.ToString("HH:mm:ss"));
                List<Rectangle> rect_header2 = fncHardware.create_rect_list(nX, nY, column2format);
                List<StringFormat> sf_header2 = fncHardware.create_stringformat_list(new int[] { 1, 3 });
                List<Font> font_header2 = fncHardware.create_font_list(new int[] { 3, 3 });
                nY = DrawStringDataTable(g, dt_header2, font_header2, rect_header2, fncHardware.brush_Black, sf_header2);
            }
            else
            {
                dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add();
                if (isterminalaccountability)
                    dt_header2.Rows.Add("Date From:", mindate.ToString("MM/dd/yyyy") + " ", "Date To:", maxdate.ToString("MM/dd/yyyy"));
                else
                    dt_header2.Rows.Add("Date:", cdate + " ", " Time:", mindate.ToString("HH:mm:ss"));
                List<Rectangle> rect_header2 = fncHardware.create_rect_list(nX, nY, column4_rect_header2);
                List<StringFormat> sf_header2 = fncHardware.create_stringformat_list(new int[] { 1, 3, 1, 3 });
                List<Font> font_header2 = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
                nY = DrawStringDataTable(g, dt_header2, font_header2, rect_header2, fncHardware.brush_Black, sf_header2);

            }

            //space
            nY += 10;

            //table header
            DataTable dt_tblheader = new DataTable();
            dt_tblheader.Columns.Add(); dt_tblheader.Columns.Add();
            dt_tblheader.Rows.Add("Description", "QTY / AMOUNT");
            List<Rectangle> rect_tblheader = fncHardware.create_rect_list(nX, nY, column2format);
            List<StringFormat> sf_tblheader = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            List<Font> font_tblheader = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, dt_tblheader, font_tblheader, rect_tblheader, fncHardware.brush_Black, sf_tblheader);

            if (!isterminalaccountability)
            {
                //space
                nY += 10;
                //-----------line-------------
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
                nY += DrawString(g, "*** Z-READING SUMMARY ***", fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());
                //-----------line-------------
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                //space
                nY += 10;
            }
            else
            {
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
            }

            //summaries
            DataTable dt_salessummary1 = new DataTable();
            dt_salessummary1.Columns.Add(); dt_salessummary1.Columns.Add();

            string terminalno = cls_globalvariables.terminalno_v;
            dt_salessummary1.Rows.Add("TERMINAL", terminalno);
            dt_salessummary1.Rows.Add("BEGIN OR#", ornumber_begin);
            dt_salessummary1.Rows.Add("END OR#", ornumber_end);
            dt_salessummary1.Rows.Add("  ", "  ");

            decimal all_vatable_sale = Convert.ToDecimal(dr_summary["vatable_sale"]);
            decimal all_vatable_return = Convert.ToDecimal(dr_summary["vatable_return"]);
            decimal all_nonvat_sale = Convert.ToDecimal(dr_summary["nonvat_sale"]);
            decimal all_nonvat_return = Convert.ToDecimal(dr_summary["nonvat_return"]);
            decimal all_senior_sale = Convert.ToDecimal(dr_summary["senior_sale"]);
            decimal all_senior_return = Convert.ToDecimal(dr_summary["senior_return"]);
            decimal all_total_discount = Convert.ToDecimal(dr_summary["total_discount"]);
            decimal all_total_qty = Convert.ToDecimal(dr_summary["total_qty"]);
            decimal all_total_return_qty = Convert.ToDecimal(dr_summary["total_return_qty"]);

            decimal all_total_return = Math.Abs(all_vatable_return) + Math.Abs(all_nonvat_return);
            decimal all_gross_sales = all_vatable_sale + all_nonvat_sale + all_total_discount;
            decimal all_net_sale_bf_return = all_gross_sales - all_total_discount;
            decimal all_net_sale_af_return = all_net_sale_bf_return - all_total_return;

            dt_salessummary1.Rows.Add("GROSS SALES", all_gross_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("TOTAL DISCOUNT", all_total_discount.ToString("N2"));
            dt_salessummary1.Rows.Add("NET SALE BF RETURN", all_net_sale_bf_return.ToString("N2"));
            dt_salessummary1.Rows.Add("TOTAL RETURN", all_total_return.ToString("N2"));
            dt_salessummary1.Rows.Add("NET SALES AF RETURN", all_net_sale_af_return.ToString("N2"));

            //ready decimal all_total_sales = Convert.ToDecimal(dr_summary["totalsalesamount"]);
            decimal all_cash_sales = Convert.ToDecimal(dr_summary["cash"]);
            decimal all_bankcheque_sales = Convert.ToDecimal(dr_summary["bankcheque"]);
            decimal all_creditcard_sales = Convert.ToDecimal(dr_summary["creditcard"]);
            decimal all_debitcard_sales = Convert.ToDecimal(dr_summary["debitcard"]);
            decimal all_giftcheque_sales = Convert.ToDecimal(dr_summary["giftcheque"]);
            decimal all_mempoints_sales = Convert.ToDecimal(dr_summary["memberpoints"]);
            // ready decimal all_mempoints_sales = Convert.ToDecimal(dr_summary["mempoints"]);
            // ready decimal all_othertender_sales = all_total_sales - all_cash_sales - all_bankcheque_sales
            //        -all_creditcard_sales - all_debitcard_sales - all_giftcheque_sales - all_mempoints_sales;
            // ready decimal all_debt_sales = all_net_sale_af_return - all_total_sales;

            dt_salessummary1.Rows.Add("  ", "  ");
            dt_salessummary1.Rows.Add("CASH SALES", all_cash_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("CREDIT CARD SALES", all_creditcard_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("DEBIT CARD SALES", all_debitcard_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("BANK CHEQUE SALES", all_bankcheque_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("GIFT CHEQUE SALES", all_giftcheque_sales.ToString("N2"));
            dt_salessummary1.Rows.Add("MEM PTS SALES", all_mempoints_sales.ToString("N2"));
            //ready  dt_salessummary1.Rows.Add("DEBT SALES", all_debt_sales.ToString("N2"));


            DataTable dt_salessummary2 = new DataTable();
            dt_salessummary2.Columns.Add(); dt_salessummary2.Columns.Add();

            decimal endcash = Convert.ToDecimal(dr_cashflow["end"]);
            decimal pickupcash = Convert.ToDecimal(dr_cashflow["pickup"]);
            decimal begincash = Convert.ToDecimal(dr_cashflow["begin"]);
            decimal totalcashreceived = endcash + pickupcash - begincash;
            decimal cashshortover = totalcashreceived - all_cash_sales;

            //dt_salessummary2.Rows.Add("TRANS END COLLECT", endcash.ToString("N2"));
            //dt_salessummary2.Rows.Add("PICKED UP CASH", pickupcash.ToString("N2"));
            //dt_salessummary2.Rows.Add("CASH BEGINNING", begincash.ToString("N2"));
            //dt_salessummary2.Rows.Add("TOTAL CASH RECEIVE", totalcashreceived.ToString("N2"));
            //dt_salessummary2.Rows.Add("POS CASH SALES", all_cash_sales.ToString("N2"));
            //dt_salessummary2.Rows.Add("CASH SHORT/OVER", cashshortover.ToString("N2"));

            decimal t_vat_sale = Math.Round(all_vatable_sale + all_vatable_return, 2, MidpointRounding.AwayFromZero);
            decimal t_gross_vatable_sale = t_vat_sale;
            decimal t_vatable_sale = Math.Round(t_gross_vatable_sale / (1 + cls_globalvariables.vat), 2, MidpointRounding.AwayFromZero);
            decimal t_vat = t_gross_vatable_sale - t_vatable_sale;

            decimal t_nonvat_sale = all_net_sale_af_return - t_gross_vatable_sale; // all_nonvat_sale + all_nonvat_return;
            decimal t_senior_sale = all_senior_sale + all_senior_return;
            string sql = @"
                SELECT
	                COALESCE(SUM(sdd.`amount` * sd.`quantity`), 0) AS 'senior_disc'
                FROM 
	                `saleshead` AS sh
                LEFT JOIN
	                `salesdetail` AS sd
	                ON sd.`headid` = sh.`SyncId`
                WHERE
	                sh.`show` = 1 and sh.`status` = 1
                    and `date` between '" + datetime_d.ToString("yyyy-MM-dd") + "' and '" + datetime_d.ToString("yyyy-MM-dd") + " 23:59:59';";

            decimal t_senior_disc = 0;
            if (t_senior_sale != 0)
                decimal.TryParse(mySQLFunc.getdb(sql).Rows[0]["senior_disc"].ToString(), out t_senior_disc);
            //(t_senior_sale / (1 - cls_globalvariables.senior)) * cls_globalvariables.senior;

            dt_salessummary2.Rows.Add("  ", "  ");
            dt_salessummary2.Rows.Add("SENIOR DIS", t_senior_disc.ToString("N2"));
            dt_salessummary2.Rows.Add("VATABLE SALES", t_vatable_sale.ToString("N2"));
            dt_salessummary2.Rows.Add("NON-VAT SALES", t_nonvat_sale.ToString("N2"));
            dt_salessummary2.Rows.Add("12% VAT AMT", t_vat.ToString("N2"));

            decimal all_void_qty = Convert.ToDecimal(dr_summary["total_void_qty"]);
            decimal all_void_amount = Convert.ToDecimal(dr_summary["total_void_amount"]);

            Int64 all_trans_count = 0;
            if (Convert.ToInt64(ornumber_begin) > 0)
                all_trans_count = (Convert.ToInt64(ornumber_end) - Convert.ToInt64(ornumber_begin)) + 1;
            Int64 all_success_trans = Convert.ToInt64(dr_summary["success_trans"]);
            Int64 all_void_trans = all_trans_count - all_success_trans;

            dt_salessummary2.Rows.Add("  ", "  ");
            dt_salessummary2.Rows.Add("TOTAL QTY SOLD", all_total_qty.ToString());
            dt_salessummary2.Rows.Add("ITEM VOID CNT", all_void_qty.ToString());
            dt_salessummary2.Rows.Add("TRANS VOID AMT", all_void_amount.ToString("N2"));
            dt_salessummary2.Rows.Add("TRANS VOID CNT", Math.Abs(all_void_trans).ToString());
            dt_salessummary2.Rows.Add("SUCCESS TRANS CNT", all_success_trans.ToString());
            dt_salessummary2.Rows.Add("TOTAL TRANS CNT", all_trans_count.ToString());
            dt_salessummary2.Rows.Add("RETURN ITEMS CNT", Math.Abs(all_total_return_qty).ToString());

            dt_salessummary2.Rows.Add("  ", "  ");
            dt_salessummary2.Rows.Add("NEW GRAND TOTAL", new_grand.ToString("N2"));
            dt_salessummary2.Rows.Add("OLD GRAND TOTAL", old_grand.ToString("N2"));

            List<Rectangle> rect_salessummary = fncHardware.create_rect_list(nX, nY, column2format);
            List<StringFormat> sf_salessummary = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            List<Font> font_salessummary = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, dt_salessummary1, font_salessummary, rect_salessummary, fncHardware.brush_Black, sf_salessummary);

            List<Rectangle> rect_salessummary2 = fncHardware.create_rect_list(nX, nY, column2format);
            nY = DrawStringDataTable(g, dt_salessummary2, font_salessummary, rect_salessummary2, fncHardware.brush_Black, sf_salessummary);

            cls_globalvariables.previewmul = 1;

            return g;
        }

        //Dont delete yet
        public static void printpage_read(object sender, PrintPageEventArgs e, Bitmap bmp, DateTime datetime_d, DateTime datetimeTO_d, int userwid)
        {
            DataRow dr_summary = null;
            DataRow dr_cashflow = null;
            Graphics g = null;

            bool zreadingexist = zreadFunc.Zread_exist(datetime_d);
            DataRow posxyzreadRow = zreadFunc.get_values_from_posxyzread(datetime_d);
            bool hasCashTender = Convert.ToDecimal(posxyzreadRow["cash"]) > 0;
            bool recompute = false;
            if (userwid == 1)
            {
                if (DialogHelper.ShowDialog("superadmin: recompute?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    recompute = true;
                }
            }
            if ((datetime_d.Date.Date <= mySQLFunc.DateTimeNow().Date) && zreadingexist && hasCashTender && !recompute)
            {
                //Get Values from posxyzread
                dr_summary = posxyzreadRow;
            }
            else //recomputes values to be saved in posxyzread
            {
                //Generate Values to posxyzread
                zreadFunc.generate_posxyzread(datetime_d);
                dr_summary = zreadFunc.get_values_from_posxyzread(datetime_d);
            }

            dr_cashflow = fncHardware.get_cashflow_data(datetime_d);
            g = fncHardware.render_reading(e, bmp, datetime_d, dr_summary, dr_cashflow);

            g.Dispose();
        }

        public static Graphics printpage_receipt(object sender, PrintPageEventArgs e, Bitmap bmp, cls_POSTransaction tran, bool isvoid, bool isreprint)
        {
            if (bmp == null)
                return printpage_receipt_new(sender, e, bmp, tran, isvoid, isreprint);
            //-------------------------------data-------------------------------------
            //80mm: maxwidth 280
            //76mm_journal: maxwidth 253
            //76mm: maxwidth 240
            //57mm: maxwidth 180

            int printformat = 0;
            int maxwidth = 280;
            int[] column2format = new int[] { 160, 120 };

            if (cls_globalvariables.print_receipt_format_v == "76mm")
            {
                printformat = 76;
                maxwidth = 240;
                column2format = new int[] { 120, 120 };
            }
            else if (cls_globalvariables.print_receipt_format_v == "57mm")
            {
                printformat = 57;
                maxwidth = 180;
                column2format = new int[] { 90, 90 };
            }
            else if (cls_globalvariables.print_receipt_format_v == "76mm_journal")
            {
                printformat = 762;
                maxwidth = 253;
                column2format = new int[] { 133, 120 };
            }

            string sBusinessName = cls_globalvariables.BusinessName_v;
            string sOwner = cls_globalvariables.Owner_v;
            string sAddress = cls_globalvariables.Address_v;
            string sTIN = cls_globalvariables.TIN_v;
            string sACC = cls_globalvariables.ACC_v;
            string sPermitNo = cls_globalvariables.PermitNo_v;

            long ornumber = tran.getORnumber();
            string sMIN = cls_globalvariables.MIN_v;
            string sSerial = cls_globalvariables.Serial_v;
            string terminalno = cls_globalvariables.terminalno_v;
            DateTime salesdatetime = tran.getdatetime();
            string salesdate = salesdatetime.ToString("MM/dd/yyyy");
            string salestime = salesdatetime.ToString("HH:mm:ss");

            string cashier = tran.getclerk().getfullname();
            string cashierid = tran.getclerk().getsyncid().ToString();
            string cashiercode = tran.getclerk().getusercode().ToString();
            string checker = tran.getchecker().getfullname();
            string checkerid = tran.getchecker().getsyncid().ToString();
            string checkercode = tran.getchecker().getusercode().ToString();
            string customername = tran.getcustomer().getfullname();
            string customercode = tran.getcustomer().getCode().ToString();
            string customerid = tran.getcustomer().getwid().ToString();
            string membername = tran.getmember().MemberButOffline ? "Offline" : tran.getmember().getfullname();
            string memberidno = tran.getmember().MemberButOffline ? "Offline" : tran.getmember().getcardid();
            string seniorname = tran.getsenior().get_fullname();
            string senioridno = tran.getsenior().get_idnumber();
            string isnonvat = tran.get_productlist().get_isnonvat() ? "NON VAT" : "";

            if (cashierid == "2")
                cashier = tran.getpayments().get_memo();

            DataTable dt_header1 = new DataTable();
            dt_header1.Columns.Add();
            dt_header1.Rows.Add(sOwner);
            dt_header1.Rows.Add(sAddress);
            dt_header1.Rows.Add(sTIN);
            dt_header1.Rows.Add(sACC);
            dt_header1.Rows.Add(sPermitNo);
            dt_header1.Rows.Add(sSerial);
            dt_header1.Rows.Add(sMIN);
            if (isvoid || isreprint)
                dt_header1.Rows.Add("PRINTED: " + DateTime.Now);

            DataTable dt_header2 = new DataTable();
            if (printformat == 76 || printformat == 57)
            {
                dt_header2.Columns.Add(); dt_header2.Columns.Add();
                dt_header2.Rows.Add("OR#:", ornumber);
                dt_header2.Rows.Add("Date:", salesdate);
                dt_header2.Rows.Add("Time:", salestime);
                dt_header2.Rows.Add("TRM:", terminalno);
                if (cashier != "")
                {
                    dt_header2.Rows.Add("Cashier:", cashier);
                    dt_header2.Rows.Add("ID No.:", cashiercode);
                }
                if (checker != "")
                {
                    dt_header2.Rows.Add("Checker:", checker);
                    dt_header2.Rows.Add("ID No.:", checkercode);
                }
                if (membername != "")
                {
                    dt_header2.Rows.Add("Member:", membername);
                    dt_header2.Rows.Add("ID No.:", memberidno);
                }
                if (seniorname != "")
                {
                    dt_header2.Rows.Add("Senior:", seniorname);
                    dt_header2.Rows.Add("ID No.:", senioridno);
                }
                if (isnonvat != "")
                    dt_header2.Rows.Add("VAT:", isnonvat);
                if (customername != "")
                    dt_header2.Rows.Add("Customer ID:", customercode);
            }
            else
            {
                dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add();
                dt_header2.Rows.Add("","", "Date:", salesdate);
                dt_header2.Rows.Add("OR#:", ornumber + "", "Time:", salestime);
                dt_header2.Rows.Add("TRM:", terminalno + " ", " ", "");
                if (cashier != "") dt_header2.Rows.Add("Cashier:", cashier + "", "ID#:", cashiercode);
                if (checker != "") dt_header2.Rows.Add("Checker:", checker + "", "ID#:", checkercode);
                if (membername != "") dt_header2.Rows.Add("Member:", membername + "", "ID#:", memberidno);
                if (seniorname != "") dt_header2.Rows.Add("Senior:", seniorname + "", "ID#:", senioridno);
                if (isnonvat != "") dt_header2.Rows.Add("VAT:", isnonvat + "", "", "");
                if (customername != "") dt_header2.Rows.Add("C. ID:", "", "", customercode);
            }
            DataTable dt_items_header = new DataTable();
            dt_items_header.Columns.Add(); dt_items_header.Columns.Add(); dt_items_header.Columns.Add(); dt_items_header.Columns.Add();

            dt_items_header.Rows.Add("QTY", "", "Description", "Amount");

            DataTable dt_items = new DataTable();
            dt_items.Columns.Add(); dt_items.Columns.Add(); dt_items.Columns.Add(); dt_items.Columns.Add();
            foreach (cls_product prod in tran.get_productlist().get_productlist())
            {
                string proddesc = prod.getProductName();
                if ((prod.getSyncId() != 1) && (prod.getSyncId() != 2))
                    proddesc += " @P" + prod.getPrice().ToString("N2") + "ea\n";
                if ((prod.getPrice() != prod.getOrigPrice()) && (prod.getOrigPrice() != 0)
                    && (printformat != 57) && cls_globalvariables.DiscountDetails_v == 1)
                    proddesc += "(P" + prod.getOrigPrice().ToString("N2") + " - " + ((1 - (prod.getPrice() / prod.getOrigPrice())) * 100).ToString("N2") + "%)";
                dt_items.Rows.Add(prod.getQty().ToString("G29"), "", proddesc, prod.getAmount().ToString("N2"));
                if (prod.getQty() < 0)
                {
                    dt_items.Rows.Add("", "", "ITEM REFUND!", "");
                }
            }

            /* Vatable Sale = full amount
             * pre-vat sale = vat sale = less vat sale
             */
            decimal non_vat_sale = tran.get_productlist().get_nonvatsale();
            decimal non_vat_return = tran.get_productlist().get_nonvatreturn();
            decimal senior_discount = tran.get_productlist().get_seniordiscount();
            decimal subtotal_non_vat = tran.get_productlist().get_subtotal_nonvat();
            decimal vatable_sale = Math.Round(tran.get_productlist().get_vatablesale() / (1 + cls_globalvariables.vat), 2, MidpointRounding.AwayFromZero);
            decimal vatable_return = Math.Round(tran.get_productlist().get_vatablereturn() / (1 + cls_globalvariables.vat), MidpointRounding.AwayFromZero);
            decimal subtotal_vatable = vatable_return + vatable_sale;
            decimal vat = tran.get_productlist().get_subtotal_vat() - vatable_sale - vatable_return;

            DataTable dt_subtotal = new DataTable();
            dt_subtotal.Columns.Add(); dt_subtotal.Columns.Add();
            dt_subtotal.Rows.Add("Total QTY:", tran.get_productlist().get_totalqty());

            decimal total_sale = tran.get_productlist().get_totalamount();
            decimal trans_adjust = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_adjusttype);
            decimal trans_memberdiscount = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_membertype);
            decimal trans_pospromo = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_pospromotype);
            decimal trans_customdiscount = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_customdiscounttype);

            decimal total_gross = tran.get_productlist().get_totalamount_gross();
            decimal total_discount = total_gross - total_sale;
            decimal total_detail_discount = total_gross - tran.get_productlist().get_totalamount_no_head_discount();
            decimal total_head_discount = total_discount - total_detail_discount;

            DataTable dt_discounts = new DataTable();
            dt_discounts.Columns.Add(); dt_discounts.Columns.Add(); dt_discounts.Columns.Add();

            if (total_discount > 0.01M)
            {
                dt_discounts.Rows.Add("Total gross value:", "", total_gross.ToString("N2"));
                if (total_detail_discount > 0.01M)
                {
                    dt_discounts.Rows.Add("Prod. Discounts:", "", total_detail_discount.ToString("N2"));
                    DataTable dt_ddiscs = tran.get_productlist().get_discount_amt_summary(1);
                    foreach (DataRow dr_ddiscs in dt_ddiscs.Rows)
                    {
                        string dvalue = (Convert.ToDecimal(dr_ddiscs["value"]) == 0) ? "" : fncFilter.getDecimalValue(dr_ddiscs["value"].ToString()).ToString("N2") + "%";
                        string damount = fncFilter.getDecimalValue(dr_ddiscs["amt"].ToString()).ToString("N2");
                        dt_discounts.Rows.Add("   " + dr_ddiscs["name"], dvalue, damount);
                    }
                }
                if (total_head_discount > 0.01M)
                {
                    dt_discounts.Rows.Add("Tran. Discounts:", "", total_head_discount.ToString("N2"));
                    DataTable dt_ddiscs = tran.get_productlist().get_discount_amt_summary(0);
                    foreach (DataRow dr_ddiscs in dt_ddiscs.Rows)
                    {
                        string dvalue = (Convert.ToDecimal(dr_ddiscs["value"]) == 0) ? "" : fncFilter.getDecimalValue(dr_ddiscs["value"].ToString()).ToString("N2") + "%";
                        string damount = fncFilter.getDecimalValue(dr_ddiscs["amt"].ToString()).ToString("N2");
                        dt_discounts.Rows.Add("   " + dr_ddiscs["name"], dvalue, damount);
                    }
                }
            }

            int cnt_item_nv = 0;
            if (non_vat_sale != 0) { dt_subtotal.Rows.Add("Non-VAT Sale:", non_vat_sale.ToString("N2")); cnt_item_nv++; }
            if (non_vat_return != 0) { dt_subtotal.Rows.Add("Non-VAT Return:", non_vat_return.ToString("N2")); cnt_item_nv++; }
            if (cnt_item_nv >= 2)
                dt_subtotal.Rows.Add("Subtotal Non-VAT:", subtotal_non_vat.ToString("N2"));
            int cnt_item_v = 0;

            if (vatable_sale != 0) { dt_subtotal.Rows.Add("VATable Sale:", vatable_sale.ToString("N2")); cnt_item_v++; }
            if (vatable_return != 0) { dt_subtotal.Rows.Add("VATable Return:", vatable_return.ToString("N2")); cnt_item_v++; }
            if (cnt_item_v >= 2)
                dt_subtotal.Rows.Add("Subtotal VATable:", subtotal_vatable.ToString("N2"));
            dt_subtotal.Rows.Add("VAT 12%:", vat.ToString("N2"));


            DataTable dt_total = new DataTable();
            dt_total.Columns.Add(); dt_total.Columns.Add();
            dt_total.Rows.Add("Amount Due:", total_sale.ToString("N2"));

            decimal tendered_cash = tran.getpayments().get_cash();
            decimal tendered_credit = tran.getpayments().get_creditamount();
            decimal tendered_debit = tran.getpayments().get_debitamount();
            decimal tendered_gift = tran.getpayments().get_giftchequenewamount();
            decimal tendered_mempoints = tran.getpayments().get_points();
            decimal tendered_custompayments = tran.getpayments().get_custompaymentamount();
            decimal tendered_total = tran.getpayments().get_totalamount();
            decimal changeamt = tran.get_changeamount();
            DataTable dt_tendered = new DataTable();
            dt_tendered.Columns.Add(); dt_tendered.Columns.Add();
            int cnt_item_t = 0;
            if (tendered_cash != 0) { dt_tendered.Rows.Add("Cash:", tendered_cash.ToString("N2")); cnt_item_t++; }
            if (tendered_credit != 0 || tran.getpayments().get_creditcard().Count > 0)
            {
                dt_tendered.Rows.Add("Credit Card(s):", tendered_credit.ToString("N2")); cnt_item_t++;
                foreach (cls_cardinfo card in tran.getpayments().get_creditcard())
                {
                    dt_tendered.Rows.Add(card.getcardname().ToString(), card.getamount().ToString("N2"));
                    dt_tendered.Rows.Add("    Card Holder: ", card.getname().ToString());
                    dt_tendered.Rows.Add("    Card No: ", card.getcardno().ToString());
                    dt_tendered.Rows.Add("    Expiry Date: ", card.getexpdate().ToString("MM-yyyy"));
                    dt_tendered.Rows.Add("    Approval Code: ", card.getapprovalcode().ToString());
                }
                cnt_item_t++;
            }
            if (tendered_debit != 0 || tran.getpayments().get_debitcard().Count > 0)
            {
                dt_tendered.Rows.Add("Debit Card(s):", tendered_debit.ToString("N2")); cnt_item_t++;
                foreach (cls_cardinfo card in tran.getpayments().get_debitcard())
                {
                    dt_tendered.Rows.Add(card.getcardname().ToString(), card.getamount().ToString("N2"));
                    dt_tendered.Rows.Add("    Card Holder: ", card.getname().ToString());
                    dt_tendered.Rows.Add("    Card No: ", card.getcardno().ToString());
                    dt_tendered.Rows.Add("    Expiry Date: ", card.getexpdate().ToString("MM-yyyy"));
                    dt_tendered.Rows.Add("    Approval Code: ", card.getapprovalcode().ToString());
                }
                cnt_item_t++;
            }
            if (tendered_gift != 0 || (tran.getpayments().get_giftchequenew().Count > 0))
            {
                dt_tendered.Rows.Add("Gift Cheque:", tendered_gift.ToString("N2")); cnt_item_t++;
                foreach (cls_giftcheque giftcheque in tran.getpayments().get_giftchequenew())
                {
                    dt_tendered.Rows.Add(giftcheque.get_referenceno().ToString(), giftcheque.getamount().ToString("N2"));
                    dt_tendered.Rows.Add("    Expiry Date: ", giftcheque.getexpdate().ToString("MM-yyyy"));
                    dt_tendered.Rows.Add("    Memo: ", giftcheque.get_memo().ToString());
                }
                cnt_item_t++;
            }
            if (tendered_mempoints != 0) { dt_tendered.Rows.Add("Mem. Pts:", tendered_mempoints.ToString("N2")); cnt_item_t++; }

            if (tendered_custompayments != 0 || tran.getpayments().get_custompayments().Count > 0)
            {
                dt_tendered.Rows.Add("Custom Payments:", tendered_custompayments.ToString("N2")); cnt_item_t++;
                foreach (cls_CustomPaymentsInfo custompaymentsinfo in tran.getpayments().get_custompayments())
                {
                    dt_tendered.Rows.Add("    " + custompaymentsinfo.get_paymentname(), custompaymentsinfo.get_amount().ToString("N2"));
                }
                cnt_item_t++;
            }

            if (cnt_item_t >= 2)
                dt_tendered.Rows.Add("Total Tendered:", tendered_total.ToString("N2"));

            DataTable dt_change = new DataTable();
            dt_change.Columns.Add(); dt_change.Columns.Add();
            dt_change.Rows.Add("Change:", changeamt.ToString("N2"));

            DataTable dt_memberpoints = new DataTable();
            dt_memberpoints.Columns.Add(); dt_memberpoints.Columns.Add();
            if (tran.getmember().MemberButOffline)
            {
                dt_memberpoints.Rows.Add("Previous points:", "Offline");
                dt_memberpoints.Rows.Add("Points use:", "Offline");
                dt_memberpoints.Rows.Add("Points earned:", "Offline");
                dt_memberpoints.Rows.Add("Total Points:", "Offline");
            }
            else
            {
                dt_memberpoints.Rows.Add("Previous points:", tran.getmember().getPreviousPoints().ToString("N2"));
                dt_memberpoints.Rows.Add("Points use:", tran.getpayments().get_points().ToString("N2"));
                dt_memberpoints.Rows.Add("Points earned:", tran.get_memberpoint_earn().ToString("N2"));
                dt_memberpoints.Rows.Add("Total Points:", (tran.getmember().getPreviousPoints() - tran.getpayments().get_points() + tran.get_memberpoint_earn()).ToString("N2"));
            }

            DataTable dt_footer = new DataTable();
            dt_footer.Columns.Add();
            dt_footer.Rows.Add(cls_globalvariables.orfooter1_v);
            dt_footer.Rows.Add(cls_globalvariables.orfooter2_v);
            dt_footer.Rows.Add(cls_globalvariables.orfooter3_v);
            dt_footer.Rows.Add(cls_globalvariables.orfooter4_v);

            //------------------------------data end---------------------------------
            int nY = 0;
            int nX = 0;

            Graphics g;
            if (bmp == null)
                g = e.Graphics;
            else
            {
                g = Graphics.FromImage(bmp);
                cls_globalvariables.previewmul = 2;
            }

            //business Title
            Rectangle rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
            nY += DrawString(g, sBusinessName, fncHardware.font_Title, rect_title, fncHardware.brush_Black, fncHardware.format_center());


            //header 1
            List<Rectangle> rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_header1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);

            //-----------space-------------
            nY += 10;

            //header 2
            List<Rectangle> rect_header2 = new List<Rectangle>();
            if (printformat == 76)
                rect_header2 = fncHardware.create_rect_list(nX, nY, new int[] { 140, 100 });
            else if (printformat == 57)
                rect_header2 = fncHardware.create_rect_list(nX, nY, new int[] { 90, 90 });
            else if (printformat == 762)
                rect_header2 = fncHardware.create_rect_list(nX, nY, new int[] { 56, 82, 46, 69 });
            else
                rect_header2 = fncHardware.create_rect_list(nX, nY, new int[] { 60, 90, 60, 70 });

            List<StringFormat> sf_header2 = fncHardware.create_stringformat_list(new int[] { 1, 3, 1, 3 });
            List<Font> font_header2 = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });

            nY = DrawStringDataTable(g, dt_header2, font_header2, rect_header2, fncHardware.brush_Black, sf_header2);

            //header customer
            if (customername != "")
            {
                DataTable dt_customer = new DataTable();
                dt_customer.Columns.Add();
                dt_customer.Rows.Add("SOLD TO: " + customername);

                List<Rectangle> rect_headercustomer = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
                List<StringFormat> sf_headercustomer = fncHardware.create_stringformat_list(new int[] { 1 });
                List<Font> font_headercustomer = fncHardware.create_font_list(new int[] { 4 });
                nY = DrawStringDataTable(g, dt_customer, font_headercustomer, rect_headercustomer, fncHardware.brush_Black, sf_headercustomer);
            }

            //header salelsmemo
            if (tran.getmemo() != "")
            {
                DataTable dt_salesmemo = new DataTable();
                dt_salesmemo.Columns.Add();
                dt_salesmemo.Rows.Add("MEMO: " + tran.getmemo());

                List<Rectangle> rect_headersalesmemo = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
                List<StringFormat> sf_headersalesmemo = fncHardware.create_stringformat_list(new int[] { 1 });
                List<Font> font_headersalesmemo = fncHardware.create_font_list(new int[] { 4 });
                nY = DrawStringDataTable(g, dt_salesmemo, font_headersalesmemo, rect_headersalesmemo, fncHardware.brush_Black, sf_headersalesmemo);
            }

            //-----------line-------------
            nY += 5;
            g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            //items header
            List<Rectangle> rect_item_header = new List<Rectangle>();
            if (printformat == 76)
                rect_item_header = fncHardware.create_rect_list(nX, nY, new int[] { 30, 20, 138, 50 });
            else if (printformat == 57)
                rect_item_header = fncHardware.create_rect_list(nX, nY, new int[] { 30, 20, 80, 50 });
            else if (printformat == 762)
                rect_item_header = fncHardware.create_rect_list(nX, nY, new int[] { 30, 20, 153, 50 });
            else
                rect_item_header = fncHardware.create_rect_list(nX, nY, new int[] { 30, 20, 170, 60 });

            List<StringFormat> sf_item_header = fncHardware.create_stringformat_list(new int[] { 2, 2, 2, 2 });
            List<Font> font_item_header = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
            nY = DrawStringDataTable(g, dt_items_header, font_item_header, rect_item_header, fncHardware.brush_Black, sf_item_header);


            //items
            List<Rectangle> rect_item = new List<Rectangle>();
            List<StringFormat> sf_item = fncHardware.create_stringformat_list(new int[] { 3, 2, 1, 3 });
            List<Font> font_item = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
            if (printformat == 76)
                rect_item = fncHardware.create_rect_list(nX, nY, new int[] { 30, 10, 130, 70 });
            else if (printformat == 57)
                rect_item = fncHardware.create_rect_list(nX, nY, new int[] { 25, 5, 90, 60 });
            else if (printformat == 762)
                rect_item = fncHardware.create_rect_list(nX, nY, new int[] { 30, 10, 133, 80 });
            else //80mm
                rect_item = fncHardware.create_rect_list(nX, nY, new int[] { 30, 20, 150, 80 });
            nY = DrawStringDataTable(g, dt_items, font_item, rect_item, fncHardware.brush_Black, sf_item);
            //-----------line-------------
            nY += 5;
            g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            if (dt_discounts.Rows.Count > 0 && printformat != 57 && cls_globalvariables.DiscountDetails_v == 1)
            {
                List<Rectangle> rect_disc;
                List<StringFormat> sf_disc = fncHardware.create_stringformat_list(new int[] { 1, 3, 3 });
                //discounts
                if (printformat == 76)
                    rect_disc = fncHardware.create_rect_list(nX, nY, new int[] { 120, 50, 70 });
                else if (printformat == 762)
                    rect_disc = fncHardware.create_rect_list(nX, nY, new int[] { 133, 50, 70 });
                else
                    rect_disc = fncHardware.create_rect_list(nX, nY, new int[] { 160, 50, 70 });
                List<Font> font_disc = fncHardware.create_font_list(new int[] { 3, 3, 3 });
                nY = DrawStringDataTable(g, dt_discounts, font_disc, rect_disc, fncHardware.brush_Black, sf_disc);


                //-----------line-------------
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
            }


            //subtotal, total, tendered, change
            List<Rectangle> rect_payment;
            List<StringFormat> sf_payment = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            //subtotal
            rect_payment = fncHardware.create_rect_list(nX, nY, column2format);
            List<Font> font_subtotal = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, dt_subtotal, font_subtotal, rect_payment, fncHardware.brush_Black, sf_payment);

            //total
            rect_payment = fncHardware.create_rect_list(nX, nY, column2format);
            List<Font> font_total = fncHardware.create_font_list(new int[] { 4, 4 });
            nY = DrawStringDataTable(g, dt_total, font_total, rect_payment, fncHardware.brush_Black, sf_payment);

            //tendered
            rect_payment = fncHardware.create_rect_list(nX, nY, column2format);
            List<Font> font_tendered = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, dt_tendered, font_tendered, rect_payment, fncHardware.brush_Black, sf_payment);

            //change
            rect_payment = fncHardware.create_rect_list(nX, nY, column2format);
            List<Font> font_change = fncHardware.create_font_list(new int[] { 2, 2 });
            nY = DrawStringDataTable(g, dt_change, font_change, rect_payment, fncHardware.brush_Black, sf_payment);

            DataTable dt_addedprints = new DataTable();
            dt_addedprints.Columns.Add();

            if (isvoid)
            {
                dt_addedprints.Rows.Add("VOIDED RECEIPT!");
            }
            if (isreprint)
            {
                dt_addedprints.Rows.Add("REPRINTED RECEIPT!");
            }

            List<StringFormat> sf_addedprints = fncHardware.create_stringformat_list(new int[] { 2 });
            rect_payment = fncHardware.create_rect_list(0, nY, new int[] { maxwidth });
            List<Font> font_voidprint = fncHardware.create_font_list(new int[] { 4 });
            nY = DrawStringDataTable(g, dt_addedprints, font_voidprint, rect_payment, fncHardware.brush_Black, sf_addedprints);

            if ((tran.getmember().getSyncId() != 0 || tran.getmember().MemberButOffline) && !isreprint && !isvoid)
            {
                //-----------line-------------
                nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

                //member point
                List<Rectangle> rect_memberpoint = fncHardware.create_rect_list(nX, nY, column2format);
                nY = DrawStringDataTable(g, dt_memberpoints, font_tendered, rect_memberpoint, fncHardware.brush_Black, sf_payment);
            }

            //-----------line-------------
            nY += 5;
            g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            //footer
            List<Rectangle> rect_footer1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_footer1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_footer1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_footer, font_footer1, rect_footer1, fncHardware.brush_Black, sf_footer1);

            cls_globalvariables.previewmul = 1;

            return g;
        }

        public static Graphics printpage_receipt_new(object sender, PrintPageEventArgs e, Bitmap bmp, cls_POSTransaction tran, bool isvoid, bool isreprint)
        {
            // This actually doesnt use Graphics
            DataTable tempDataTable;
            int value = 64;
            if (cls_globalvariables.print_receipt_format_v == "76mm_journal")
                value = 40;
            else if (cls_globalvariables.print_receipt_format_v == "JNF_57mm")
                value = 42;
            ReceiptPrinterHelper printer = new ReceiptPrinterHelper(value);
            if (cls_globalvariables.print_receipt_buffer != 0)
                printer.StringBufferWidth = cls_globalvariables.print_receipt_buffer;
            if (cls_globalvariables.print_receipt_actual != 0)
                printer.StringFullWidth = cls_globalvariables.print_receipt_actual;
            if (cls_globalvariables.print_receipt_limit != 0)
                printer.StringWidth = cls_globalvariables.print_receipt_limit;
            if (cls_globalvariables.print_receipt_linespacing != 0)
                printer.LineSpacing = cls_globalvariables.print_receipt_linespacing;

            printer.NormalFont();
            printer.CPI12();

            // Header
            if (cls_globalvariables.BusinessName_v != "")
                printer.WriteLines(cls_globalvariables.BusinessName_v);
            if (cls_globalvariables.Owner_v != "")
                printer.WriteLines(cls_globalvariables.Owner_v);
            printer.WriteLines("VAT REG. " + cls_globalvariables.TIN_v);
            if (cls_globalvariables.MIN_v != "")
                printer.WriteLines(cls_globalvariables.MIN_v);
            if (cls_globalvariables.Serial_v != "")
                printer.WriteLines(cls_globalvariables.Serial_v);
            if (cls_globalvariables.Address_v != "")
                printer.WriteLines(cls_globalvariables.Address_v);

            // Transaction Info
            tempDataTable = new DataTable();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();
            tempDataTable.Rows.Add("TERMINAL#: " + cls_globalvariables.terminalno_v, "");
            tempDataTable.Rows.Add("SI#: " + tran.getORnumber());
            tempDataTable.Rows.Add("CASHIER: " + tran.getclerk().getfullname(), "ID#: " + tran.getclerk().getusercode());
            tempDataTable.Rows.Add("DATE: " + tran.getdatetime().ToShortDateString(), "TIME: " + tran.getdatetime().ToLongTimeString());

            printer.WriteRepeatingCharacterLine('=');
            printer.WriteTable(
                tempDataTable,
                new StringAlignment[] { StringAlignment.Near, StringAlignment.Near },
                new int[] { printer.StringWidth / 2, printer.StringWidth / 2 }
            );
            if (tran.getmemo() != "")
                printer.WriteRow(
                  new string[] { "MEMO: ", tran.getmemo() },
                  new StringAlignment[] { StringAlignment.Near, StringAlignment.Near },
                  new int[] { 6, printer.StringWidth }
                );

            // Product Info
            tempDataTable = new DataTable();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();
            tempDataTable.Rows.Add("QTY", "", "DESCRIPTION", "AMOUNT");
            foreach (cls_product prod in tran.get_productlist().get_productlist())
            {
                string proddesc = prod.getProductName();
                if ((prod.getSyncId() != 1) && (prod.getSyncId() != 2))
                    proddesc += " @P" + prod.getPrice().ToString("N2") + "ea";
                tempDataTable.Rows.Add(prod.getQty().ToString("G29"), "", proddesc, prod.getAmount().ToString("N2"));
                if ((prod.getPrice() != prod.getOrigPrice()) && (prod.getOrigPrice() != 0)
                    && cls_globalvariables.DiscountDetails_v == 1)
                    tempDataTable.Rows.Add("", "", "(P" + prod.getOrigPrice().ToString("N2") + " - " + ((1 - (prod.getPrice() / prod.getOrigPrice())) * 100).ToString("N2") + "%)", "");
                if (prod.getQty() < 0)
                {
                    tempDataTable.Rows.Add("", "", "ITEM REFUND!", "");
                }
            }
            printer.WriteRepeatingCharacterLine('=');
            printer.WriteTable(
                tempDataTable,
                new StringAlignment[] { StringAlignment.Far, StringAlignment.Far, StringAlignment.Near, StringAlignment.Far },
                new int[] { (int)(printer.StringWidth * 0.15), 1, (int)(printer.StringWidth * 0.6), (int)(printer.StringWidth * 0.25) }
            );

            // Discount Breakdown
            decimal total_sale = tran.get_productlist().get_totalamount();
            decimal trans_adjust = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_adjusttype);
            decimal trans_memberdiscount = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_membertype);
            decimal trans_pospromo = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_pospromotype);
            decimal trans_customdiscount = tran.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_customdiscounttype);

            decimal total_gross = tran.get_productlist().get_totalamount_gross();
            decimal total_discount = total_gross - total_sale;
            decimal total_detail_discount = total_gross - tran.get_productlist().get_totalamount_no_head_discount();
            decimal total_head_discount = total_discount - total_detail_discount;

            tempDataTable = new DataTable();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();

            if (total_discount > 0.01M)
            {
                tempDataTable.Rows.Add("Total gross value:", "", total_gross.ToString("N2"));
                if (total_detail_discount > 0.01M)
                {
                    tempDataTable.Rows.Add("Prod. Discounts:", "", total_detail_discount.ToString("N2"));
                    DataTable dt_ddiscs = tran.get_productlist().get_discount_amt_summary(1);
                    foreach (DataRow dr_ddiscs in dt_ddiscs.Rows)
                    {
                        string dvalue = (Convert.ToDecimal(dr_ddiscs["value"]) == 0) ? "" : fncFilter.getDecimalValue(dr_ddiscs["value"].ToString()).ToString("N2") + "%";
                        string damount = fncFilter.getDecimalValue(dr_ddiscs["amt"].ToString()).ToString("N2");
                        tempDataTable.Rows.Add("   " + dr_ddiscs["name"], dvalue, damount);
                    }
                }
                if (total_head_discount > 0.01M)
                {
                    tempDataTable.Rows.Add("Tran. Discounts:", "", total_head_discount.ToString("N2"));
                    DataTable dt_ddiscs = tran.get_productlist().get_discount_amt_summary(0);
                    foreach (DataRow dr_ddiscs in dt_ddiscs.Rows)
                    {
                        string dvalue = (Convert.ToDecimal(dr_ddiscs["value"]) == 0) ? "" : fncFilter.getDecimalValue(dr_ddiscs["value"].ToString()).ToString("N2") + "%";
                        string damount = fncFilter.getDecimalValue(dr_ddiscs["amt"].ToString()).ToString("N2");
                        tempDataTable.Rows.Add("   " + dr_ddiscs["name"], dvalue, damount);
                    }
                }
                printer.WriteRepeatingCharacterLine('=');
                printer.WriteTable(
                    tempDataTable,
                    new StringAlignment[] { StringAlignment.Near, StringAlignment.Far, StringAlignment.Far },
                    new int[] { printer.StringWidth / 2, printer.StringWidth / 4, printer.StringWidth / 4 }
                );
            }

            // Sub-Total Info
            tempDataTable = new DataTable();
            tempDataTable.Columns.Add();
            tempDataTable.Columns.Add();
            tempDataTable.Rows.Add("Total QTY:", tran.get_productlist().get_totalqty().ToString("G29"));
            // Non-VAT
            if (tran.get_productlist().get_nonvatsale() != 0)
                tempDataTable.Rows.Add("VAT EXEMPT SALE:", tran.get_productlist().get_nonvatsale().ToString("N2"));
            if (tran.get_productlist().get_nonvatreturn() != 0)
                tempDataTable.Rows.Add("VAT EXEMPT RETURN:", tran.get_productlist().get_nonvatreturn().ToString("N2"));
            if (tran.get_productlist().get_nonvatsale() != 0 && tran.get_productlist().get_nonvatreturn() != 0)
                tempDataTable.Rows.Add("VAT EXEMPT SUBTOTAL:", tran.get_productlist().get_subtotal_nonvat().ToString("N2"));

            // VATable
            decimal vatable_sale = Math.Round(tran.get_productlist().get_vatablesale() / (1 + cls_globalvariables.vat), 2, MidpointRounding.AwayFromZero);
            decimal vatable_return = Math.Round(tran.get_productlist().get_vatablereturn() / (1 + cls_globalvariables.vat), 2, MidpointRounding.AwayFromZero);
            decimal vatableSubtotal = vatable_sale + vatable_return;
            if (vatable_sale != 0)
                tempDataTable.Rows.Add("VATABLE SALE:", vatable_sale.ToString("N2"));
            if (vatable_return != 0)
                tempDataTable.Rows.Add("VATABLE RETURN:", vatable_return.ToString("N2"));
            if (vatable_sale != 0 && vatable_return != 0)
                tempDataTable.Rows.Add("VATABLE SUBTOTAL:", vatableSubtotal.ToString("N2"));
            printer.WriteRepeatingCharacterLine('=');
            // VAT
            tempDataTable.Rows.Add("VAT 12%:", (tran.get_productlist().get_subtotal_vat() - vatableSubtotal).ToString("N2"));

            printer.WriteTable(
                tempDataTable,
                new StringAlignment[] { StringAlignment.Near, StringAlignment.Far },
                new int[] { printer.StringWidth / 2, printer.StringWidth / 2 }
            );
            printer.LargeFont();
            printer.CPI12();
            printer.WriteRow(
                new string[] { "AMOUNT DUE:", tran.get_productlist().get_totalsale().ToString("N2") },
                new StringAlignment[] { StringAlignment.Near, StringAlignment.Far },
                new int[] { printer.StringWidth / 2, printer.StringWidth / 2 }
            );
            printer.NormalFont();
            printer.CPI12();

            // Payment
            decimal tendered_cash = tran.getpayments().get_cash();
            decimal tendered_credit = tran.getpayments().get_creditamount();
            decimal tendered_debit = tran.getpayments().get_debitamount();
            decimal tendered_gift = tran.getpayments().get_giftchequenewamount();
            decimal tendered_mempoints = tran.getpayments().get_points();
            decimal tendered_custompayments = tran.getpayments().get_custompaymentamount();
            decimal tendered_total = tran.getpayments().get_totalamount();
            decimal changeamt = tran.get_changeamount();
            tempDataTable = new DataTable();
            tempDataTable.Columns.Add(); tempDataTable.Columns.Add();
            int cnt_item_t = 0;
            if (tendered_cash != 0)
                tempDataTable.Rows.Add("Cash:", tendered_cash.ToString("N2")); cnt_item_t++;
            if (tendered_credit != 0 || tran.getpayments().get_creditcard().Count > 0)
            {
                tempDataTable.Rows.Add("Credit Card(s):", tendered_credit.ToString("N2")); cnt_item_t++;
                foreach (cls_cardinfo card in tran.getpayments().get_creditcard())
                {
                    tempDataTable.Rows.Add(card.getcardname().ToString(), card.getamount().ToString("N2"));
                    tempDataTable.Rows.Add("    Card Holder: ", card.getname().ToString());
                    tempDataTable.Rows.Add("    Card No: ", card.getcardno().ToString());
                    tempDataTable.Rows.Add("    Expiry Date: ", card.getexpdate().ToString("MM-yyyy"));
                    tempDataTable.Rows.Add("    Approval Code: ", card.getapprovalcode().ToString());
                }
                cnt_item_t++;
            }
            if (tendered_debit != 0 || tran.getpayments().get_debitcard().Count > 0)
            {
                tempDataTable.Rows.Add("Debit Card(s):", tendered_debit.ToString("N2")); cnt_item_t++;
                foreach (cls_cardinfo card in tran.getpayments().get_debitcard())
                {
                    tempDataTable.Rows.Add(card.getcardname().ToString(), card.getamount().ToString("N2"));
                    tempDataTable.Rows.Add("    Card Holder: ", card.getname().ToString());
                    tempDataTable.Rows.Add("    Card No: ", card.getcardno().ToString());
                    tempDataTable.Rows.Add("    Expiry Date: ", card.getexpdate().ToString("MM-yyyy"));
                    tempDataTable.Rows.Add("    Approval Code: ", card.getapprovalcode().ToString());
                }
                cnt_item_t++;
            }
            if (tendered_gift != 0 || (tran.getpayments().get_giftchequenew().Count > 0))
            {
                tempDataTable.Rows.Add("Gift Cheque:", tendered_gift.ToString("N2")); cnt_item_t++;
                foreach (cls_giftcheque giftcheque in tran.getpayments().get_giftchequenew())
                {
                    tempDataTable.Rows.Add(giftcheque.get_referenceno().ToString(), giftcheque.getamount().ToString("N2"));
                    tempDataTable.Rows.Add("    Expiry Date: ", giftcheque.getexpdate().ToString("MM-yyyy"));
                    tempDataTable.Rows.Add("    Memo: ", giftcheque.get_memo().ToString());
                }
                cnt_item_t++;
            }
            if (tendered_mempoints != 0)
            {
                tempDataTable.Rows.Add("Mem. Pts:", tendered_mempoints.ToString("N2"));
                cnt_item_t++;
            }
            if (tendered_custompayments != 0 || tran.getpayments().get_custompayments().Count > 0)
            {
                tempDataTable.Rows.Add("Custom Payments:", tendered_custompayments.ToString("N2")); cnt_item_t++;
                foreach (cls_CustomPaymentsInfo custompaymentsinfo in tran.getpayments().get_custompayments())
                    tempDataTable.Rows.Add("    " + custompaymentsinfo.get_paymentname(), custompaymentsinfo.get_amount().ToString("N2"));
                cnt_item_t++;
            }
            if (cnt_item_t >= 2)
                tempDataTable.Rows.Add("Total Tendered:", tendered_total.ToString("N2"));

            tempDataTable.Rows.Add("Change:", changeamt.ToString("N2"));
            printer.WriteTable(
                tempDataTable,
                new StringAlignment[] { StringAlignment.Near, StringAlignment.Far },
                new int[] { printer.StringWidth / 2, printer.StringWidth / 2 }
            );

            // Customer/Member/Senior Info
            printer.WriteRepeatingCharacterLine('=');
            if (tran.getmember().getSyncId() != 0)
                printer.WriteLines("MEMBER INFORMATION");
            else if (tran.getsenior().get_fullname() != "")
                printer.WriteLines("SENIOR INFORMATION");
            else if (tran.get_productlist().get_isnonvat())
                printer.WriteLines("VAT EXEMPT INFORMATION");
            else
                printer.WriteLines("CUSTOMER INFORMATION");

            if (tran.getmember().getSyncId() != 0)
                printer.WriteLines("Name: " + tran.getmember().getfullname(), StringAlignment.Near);
            else if (tran.getsenior().get_fullname() != "")
                printer.WriteLines("Name: " + tran.getsenior().get_fullname(), StringAlignment.Near);
            else if (tran.getcustomer().getwid() != 0)
                printer.WriteLines("Name: " + tran.getcustomer().getfullname(), StringAlignment.Near);
            else
                printer.WriteLines("Name: " + printer.GetRepeatingCharacter('_', printer.StringWidth - 6));

            if (tran.getmember().getSyncId() != 0)
                printer.WriteLines("Card#: " + tran.getmember().getcardid(), StringAlignment.Near);
            else if (tran.getsenior().get_fullname() != "")
                printer.WriteLines("ID#: " + tran.getsenior().get_idnumber(), StringAlignment.Near);
            if (tran.getmember().getSyncId() != 0)
                printer.WriteLines("Address: " + tran.getmember().getaddress(), StringAlignment.Near);
            else
                printer.WriteLines("Address: " + printer.GetRepeatingCharacter('_', printer.StringWidth - 9));
            printer.WriteLines("TIN: " + printer.GetRepeatingCharacter('_', printer.StringWidth - 5));
            //printer.Write("BUS. TYPE: " + printer.GetRepeatingCharacter('_', printer.StringWidth - 11));
            //printer.Write("\n\r\n\r\n\rSIGNATURE: " + printer.GetRepeatingCharacter('_', printer.StringWidth - 11));

            // Member Points (Only appears on original OR)
            if (tran.getmember().getSyncId() != 0 && !tran.getmember().MemberButOffline && !isvoid && !isreprint)
            {
                tempDataTable = new DataTable();
                tempDataTable.Columns.Add();
                tempDataTable.Columns.Add();
                tempDataTable.Rows.Add("Previous points:", tran.getmember().getPreviousPoints().ToString("N2"));
                tempDataTable.Rows.Add("Points use:", tran.getpayments().get_points().ToString("N2"));
                tempDataTable.Rows.Add("Points earned:", tran.get_memberpoint_earn().ToString("N2"));
                tempDataTable.Rows.Add("Total Points:", (tran.getmember().getPreviousPoints() - tran.getpayments().get_points() + tran.get_memberpoint_earn()).ToString("N2"));

                printer.WriteRepeatingCharacterLine('=');
                printer.WriteTable(
                    tempDataTable,
                    new StringAlignment[] { StringAlignment.Near, StringAlignment.Far },
                    new int[] { printer.StringWidth / 2, printer.StringWidth / 2 }
                );
            }

            // POS Provider Info
            // Based on BIR Revenue Regulations No. 10-2015
            printer.WriteRepeatingCharacterLine('=');
            if (cls_globalvariables.PosProviderName_v != "")
                printer.WriteLines(cls_globalvariables.PosProviderName_v);
            if (cls_globalvariables.PosProviderAddress_v != "")
                printer.WriteLines(cls_globalvariables.PosProviderAddress_v);
            if (cls_globalvariables.PosProviderTIN_v != "")
                printer.WriteLines(cls_globalvariables.PosProviderTIN_v);
            if (cls_globalvariables.ACC_v != "")
                printer.WriteLines(cls_globalvariables.ACC_v);
            if (cls_globalvariables.ACC_date_v != "")
                printer.WriteLines(cls_globalvariables.ACC_date_v);
            if (cls_globalvariables.PermitNo_v != "")
                printer.WriteLines(cls_globalvariables.PermitNo_v);
            printer.WriteLines("THIS INVOICE/RECEIPT SHALL BE VALID FOR FIVE(5) YEARS FROM THE DATE OF THE PERMIT TO USE.");

            // Footers
            printer.WriteRepeatingCharacterLine('=');
            if (cls_globalvariables.orfooter1_v != "")
                printer.WriteLines(cls_globalvariables.orfooter1_v);
            if (cls_globalvariables.orfooter2_v != "")
                printer.WriteLines(cls_globalvariables.orfooter2_v);
            if (cls_globalvariables.orfooter3_v != "")
                printer.WriteLines(cls_globalvariables.orfooter3_v);
            if (cls_globalvariables.orfooter4_v != "")
                printer.WriteLines(cls_globalvariables.orfooter4_v);

            // Based on BIR Revenue Regulations No. 10-2015
            printer.LargeFont();
            printer.CPI12();
            if (tran.get_productlist().get_isnonvat())
                printer.WriteLines("THIS DOCUMENT IS NOT VALID FOR CLAIM OF INPUT TAX.");
            printer.NormalFont();
            printer.CPI12();

            // Reprint/Voided Tag
            if (isvoid)
                printer.WriteLines("VOIDED RECEIPT!");
            if (isreprint)
            {
                printer.WriteLines("REPRINTED RECEIPT!");
            }
            DateTime now = DateTime.Now;
            printer.WriteLines("PRINTED: " + now.ToShortDateString() + " " + now.ToLongTimeString());

            printer.Print();
            printer.ActivateCutter();
            return null;
        }

        public static void printpage_TerminalReadings(int reporttype, string reportname, DataTable DT, DateTime datefrom, DateTime dateto, int printcnt)
        {
            //report type
            // 0 - hourly or daily sales
            // 1 - other sales
            // 2 - cashier accountability
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) => { printpage_TerminalReadings(sender, e, null, reporttype, reportname, DT, datefrom, dateto); };

            for (int x = 0; x < printcnt; x++)
            {
                start_print(pd);
            }
        }

        public static Graphics printpage_TerminalReadings(object sender, PrintPageEventArgs e, Bitmap bmp, int reporttype, string reportname, DataTable DT, DateTime datefrom, DateTime dateto)
        {
            Graphics g;
            if (bmp == null)
                g = e.Graphics;
            else
            {
                g = Graphics.FromImage(bmp);
                cls_globalvariables.previewmul = 2;
            }
            int nY = 0;
            int nX = 0;
            int maxwidth = 280;
            int[] rectangle4 = new int[] { 80, 40, 80, 80 };
            int[] rectangle2 = new int[] { 120, 160 };
            if (cls_globalvariables.print_receipt_format_v == "76mm")
            {
                maxwidth = 240;
                rectangle4 = new int[] { 70, 40, 80, 50 };
                rectangle2 = new int[] { 110, 130 };
            }
            else if (cls_globalvariables.print_receipt_format_v == "76mm_journal")
            {
                maxwidth = 253;
                rectangle4 = new int[] { 75, 41, 85, 52 };
                rectangle2 = new int[] { 120, 133 };
            }

            double totalqty = 0;
            double totalamt = 0;
            double totalAC = 0;
            if (reporttype != 2)
            {

                foreach (DataRow DR in DT.Rows)
                    totalqty += Convert.ToDouble(DR["Count"]);

                foreach (DataRow DR in DT.Rows)
                    totalamt += Convert.ToDouble(DR["Amount"]);

                totalAC = Math.Round(totalamt / totalqty, 2);
                totalamt = Math.Round(totalamt, 2);
                totalqty = Math.Round(totalqty, 2);
            }

            string sBusinessName = cls_globalvariables.BusinessName_v;
            string sAddress = cls_globalvariables.Address_v;
            string sOwner = cls_globalvariables.Owner_v;
            string sTIN = cls_globalvariables.TIN_v;
            string sACC = cls_globalvariables.ACC_v;
            string sPermitNo = cls_globalvariables.PermitNo_v;
            string sSerial = cls_globalvariables.Serial_v;
            string sMIN = cls_globalvariables.MIN_v;
            string branchid = cls_globalvariables.BranchCode;
            string terminalno = cls_globalvariables.terminalno_v;

            DataTable dt_header1 = new DataTable();
            dt_header1.Columns.Add();
            dt_header1.Rows.Add(sBusinessName);
            dt_header1.Rows.Add(sAddress);
            dt_header1.Rows.Add(sOwner);
            dt_header1.Rows.Add(sTIN);
            dt_header1.Rows.Add(sACC);
            dt_header1.Rows.Add(sPermitNo);
            dt_header1.Rows.Add(sSerial);
            dt_header1.Rows.Add(sMIN);
            dt_header1.Rows.Add(" ");
            dt_header1.Rows.Add("PRINTED: " + DateTime.Now);
            dt_header1.Rows.Add("Terminal No: " + cls_globalvariables.terminalno_v);
            dt_header1.Rows.Add(" ");
            dt_header1.Rows.Add(reportname);
            string sdatefrom = datefrom.ToString("yyyy-MM-dd");
            string sdateto = dateto.ToString("yyyy-MM-dd");
            dt_header1.Rows.Add(sdatefrom + " - " + sdateto);
            dt_header1.Rows.Add(" ");

            List<Rectangle> rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_header1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);

            DataTable dt_columns = new DataTable();

            List<Rectangle> rect_item = new List<Rectangle>();
            List<StringFormat> sf_item = new List<StringFormat>();
            List<Font> font_item = new List<Font>();

            if (reporttype == 2)
            {

                dt_columns.Columns.Add(); dt_columns.Columns.Add();
                rect_item = fncHardware.create_rect_list(nX, nY, rectangle2);
                sf_item = fncHardware.create_stringformat_list(new int[] { 1, 3 });
                font_item = fncHardware.create_font_list(new int[] { 3, 3 });
                Rectangle rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
                decimal grandtotal = 0;

                foreach (DataRow DR in DT.Rows)
                {

                    nY += 10;
                    nY += 5;
                    g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                    rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
                    nY += DrawString(g, "CASHIER: " + DR["fullname"].ToString(), fncHardware.font_Content, rect_title, fncHardware.brush_Black, fncHardware.format_left());
                    nY += 5;
                    g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                    nY += 10;

                    dt_columns.Rows.Add("CASH ", Math.Round(Convert.ToDecimal(DR["cash"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    dt_columns.Rows.Add("BANK CHEQUE ", Math.Round(Convert.ToDecimal(DR["bankcheque"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    dt_columns.Rows.Add("CREDIT CARD ", Math.Round(Convert.ToDecimal(DR["creditcard"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    dt_columns.Rows.Add("DEBIT CARD ", Math.Round(Convert.ToDecimal(DR["debitcard"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    dt_columns.Rows.Add("GIFT CHEQUE ", Math.Round(Convert.ToDecimal(DR["giftcheque"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    dt_columns.Rows.Add("MEM POINTS ", Math.Round(Convert.ToDecimal(DR["mempoints"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    dt_columns.Rows.Add("OTHER TENDER ", Math.Round(Convert.ToDecimal(DR["othertender"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));

                    rect_item = fncHardware.create_rect_list(nX, nY, rectangle2);
                    sf_item = fncHardware.create_stringformat_list(new int[] { 1, 3 });
                    font_item = fncHardware.create_font_list(new int[] { 3, 3 });
                    grandtotal += Convert.ToDecimal(DR["totalsalesamount"]);
                    dt_columns.Rows.Add("TOTAL: ", Math.Round(Convert.ToDecimal(DR["totalsalesamount"]), 2, MidpointRounding.AwayFromZero).ToString("N2"));
                    nY = DrawStringDataTable(g, dt_columns, font_item, rect_item, fncHardware.brush_Black, sf_item);
                    dt_columns.Rows.Clear();
                }
                nY += 10;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

                rect_item = fncHardware.create_rect_list(nX, nY, rectangle2);
                sf_item = fncHardware.create_stringformat_list(new int[] { 1, 3 });
                font_item = fncHardware.create_font_list(new int[] { 3, 3 });
                dt_columns.Rows.Add("GRAND TOTAL: ", Math.Round(grandtotal, 2, MidpointRounding.AwayFromZero).ToString("N2"));
                nY = DrawStringDataTable(g, dt_columns, font_item, rect_item, fncHardware.brush_Black, sf_item);
                dt_columns.Rows.Clear();

                nY += 10;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
            }
            else if (reporttype == 1)
            {
                dt_columns.Columns.Add(); dt_columns.Columns.Add();
                dt_columns.Columns.Add(); dt_columns.Columns.Add();
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                dt_columns.Rows.Add(reportname.Split(' ')[0], "Qty", "Amount", "%Amount");
                rect_item = fncHardware.create_rect_list(nX, nY, rectangle4);
                sf_item = fncHardware.create_stringformat_list(new int[] { 2, 2, 2, 2 });
                font_item = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
                nY = DrawStringDataTable(g, dt_columns, font_item, rect_item, fncHardware.brush_Black, sf_item);
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

                rect_item = fncHardware.create_rect_list(nX, nY, rectangle4);
                sf_item = fncHardware.create_stringformat_list(new int[] { 1, 2, 3, 3 });
                nY = DrawStringDataTable(g, DT, font_item, rect_item, fncHardware.brush_Black, sf_item);
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                dt_columns.Rows.Clear();
                dt_columns.Rows.Add("GrandTotal", totalqty.ToString("N0"), totalamt.ToString("N2"), "100%");
                rect_item = fncHardware.create_rect_list(nX, nY, rectangle4);
                nY = DrawStringDataTable(g, dt_columns, font_item, rect_item, fncHardware.brush_Black, sf_item);
                nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
            }
            else
            {
                string Column1name = (reportname == "HOURLY SALES REPORT") ? " TIME " : " DATE ";
                dt_columns.Columns.Add(); dt_columns.Columns.Add();
                dt_columns.Columns.Add(); dt_columns.Columns.Add();
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                dt_columns.Rows.Add(Column1name, "Trn Cnt", "AC", "Amount");
                rect_item = fncHardware.create_rect_list(nX, nY, rectangle4);
                sf_item = fncHardware.create_stringformat_list(new int[] { 2, 2, 2, 2 });
                font_item = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
                nY = DrawStringDataTable(g, dt_columns, font_item, rect_item, fncHardware.brush_Black, sf_item);
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

                rect_item = fncHardware.create_rect_list(nX, nY, rectangle4);
                sf_item = fncHardware.create_stringformat_list(new int[] { 2, 3, 3, 3 });
                nY = DrawStringDataTable(g, DT, font_item, rect_item, fncHardware.brush_Black, sf_item);

                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
                dt_columns.Rows.Clear();
                dt_columns.Rows.Add("GrandTotal", totalqty.ToString("N0"), totalAC.ToString("N2"), totalamt.ToString("N2"));
                rect_item = fncHardware.create_rect_list(nX, nY, rectangle4);
                nY = DrawStringDataTable(g, dt_columns, font_item, rect_item, fncHardware.brush_Black, sf_item);
                nY += 5;
                g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;
            }

            dt_header1.Rows.Clear();
            dt_header1.Rows.Add("---END OF REPORT---");
            rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            font_header1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);

            cls_globalvariables.previewmul = 1;

            return g;
        }

        public static int DrawString(Graphics g, string str, Font font, Rectangle rect, SolidBrush sb, StringFormat sf)
        {
            rect.Height = (int)g.MeasureString(str, font, rect.Width).Height;
            g.DrawString(str, font, sb, rect, sf);
            return rect.Height;
        }

        private static int DrawStringRect(Graphics g, List<string> str_list, Font font, Rectangle rect, SolidBrush sb, StringFormat sf)
        {
            foreach (string str in str_list)
            {
                rect.Y += DrawString(g, str, font, rect, sb, sf);
            }

            return rect.Y;
        }
        public static int DrawStringDataTable(Graphics g, DataTable dt, List<Font> font_list, List<Rectangle> rect_list, SolidBrush sb, List<StringFormat> sf_list)
        {
            int cellnum = rect_list.Count;
            int nY = rect_list[0].Y;
            int temp_height = 0;
            int height = 0;
            foreach (DataRow dr in dt.Rows)
            {
                temp_height = 0;
                height = 0;
                for (int i = 0; i < cellnum; i++)
                {
                    Rectangle rect = rect_list[i];
                    rect.Y = nY;
                    temp_height = DrawString(g, dr[i].ToString(), font_list[i], rect, sb, sf_list[i]);

                    if (temp_height > height) height = temp_height;
                }
                nY += height;
            }

            return nY;
        }

        public static bool PulloutCashCollection()
        {
            if (cls_globalvariables.maximum_cash_collection_v == 0)
                return false;
            string sql = @"SELECT SUM(D.amount) FROM collectionhead AS H
                    LEFT JOIN collectiondetail AS D ON H.syncid = D.headid
                    WHERE DATE_FORMAT(H.`datecreated`, '%Y-%m-%d') = DATE(NOW()) AND
                    D.`method` = 1 AND H.`status` = 1 AND H.`show` = 1;";
            DataTable dt = mySQLFunc.getdb(sql);
            if (dt == null)
                return false;
            if (dt.Rows.Count <= 0)
                return false;
            if (Convert.ToDouble(dt.Rows[0][0].ToString()) >= cls_globalvariables.maximum_cash_collection_v)
                return true;
            else
                return false;
        }

        public static void print_pickupcash(DateTime datetime_d, long userwid)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) => { printpage_pickupcash(sender, e, null, datetime_d, userwid); };
            start_print(pd);
        }

        private static Graphics printpage_pickupcash(object sender, PrintPageEventArgs e, Bitmap bmp, DateTime datetime_d, long userwid)
        {
            string sBranchid = cls_globalvariables.BranchCode;
            string sBusinessName = cls_globalvariables.BusinessName_v;
            string sOwner = cls_globalvariables.Owner_v;
            string sAddress = cls_globalvariables.Address_v;
            string sTIN = cls_globalvariables.TIN_v;
            string sACC = cls_globalvariables.ACC_v;
            string sPermitNo = cls_globalvariables.PermitNo_v;
            string sMIN = cls_globalvariables.MIN_v;
            string sSerial = cls_globalvariables.Serial_v;
            string terminalno = cls_globalvariables.terminalno_v;
            string cdate = datetime_d.ToString("MM/dd/yyyy");
            string ctime = datetime_d.ToString("HH:mm:ss");
            cls_user cashier = new cls_user(userwid);

            DataTable dt_header1 = new DataTable();
            dt_header1.Columns.Add();
            dt_header1.Rows.Add(sOwner);
            dt_header1.Rows.Add(sAddress);
            dt_header1.Rows.Add("PICK-UP CASH DETAILS");
            dt_header1.Rows.Add("Cashier: " + cashier.getfullname());

            string sdate = datetime_d.ToString("yyyy-MM-dd");

            DataTable dt_header2 = new DataTable();
            dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add(); dt_header2.Columns.Add();
            dt_header2.Rows.Add("Date:", cdate + " ", "Time:", ctime);

            DataTable dt_tblheader = new DataTable();
            dt_tblheader.Columns.Add(); dt_tblheader.Columns.Add(); dt_tblheader.Columns.Add();
            dt_tblheader.Rows.Add("DESCRIPTION", "QTY", "AMOUNT");

            cls_bills cash_denom = new cls_bills();
            cash_denom.get_cashdenomination(cashier, 2);

            DataTable dt_tbldata2 = new DataTable();
            dt_tbldata2.Columns.Add(); dt_tbldata2.Columns.Add();
            dt_tbldata2.Columns.Add();
            dt_tbldata2.Rows.Add("1000 PESOS", cash_denom.getCash_1000().ToString(), (cash_denom.getCash_1000() * 1000).ToString("N2"));
            dt_tbldata2.Rows.Add("500 PESOS", cash_denom.getCash_500().ToString(), (cash_denom.getCash_500() * 500).ToString("N2"));
            dt_tbldata2.Rows.Add("200 PESOS", cash_denom.getCash_200().ToString(), (cash_denom.getCash_200() * 200).ToString("N2"));
            dt_tbldata2.Rows.Add("100 PESOS", cash_denom.getCash_100().ToString(), (cash_denom.getCash_100() * 100).ToString("N2"));
            dt_tbldata2.Rows.Add("50 PESOS", cash_denom.getCash_50().ToString(), (cash_denom.getCash_50() * 50).ToString("N2"));
            dt_tbldata2.Rows.Add("20 PESOS", cash_denom.getCash_20().ToString(), (cash_denom.getCash_20() * 20).ToString("N2"));
            dt_tbldata2.Rows.Add("10 PESOS", cash_denom.getCash_10().ToString(), (cash_denom.getCash_10() * 10).ToString("N2"));
            dt_tbldata2.Rows.Add("5 PESOS", cash_denom.getCash_5().ToString(), (cash_denom.getCash_5() * 5).ToString("N2"));
            dt_tbldata2.Rows.Add("1 PESOS", cash_denom.getCash_1().ToString(), (cash_denom.getCash_1() * 1).ToString("N2"));
            dt_tbldata2.Rows.Add("25 CENTS", cash_denom.getCash_25c().ToString(), (cash_denom.getCash_25c() * 0.25).ToString("N2"));
            dt_tbldata2.Rows.Add("10 CENTS", cash_denom.getCash_10c().ToString(), (cash_denom.getCash_10c() * 0.1).ToString("N2"));
            dt_tbldata2.Rows.Add("5 CENTS", cash_denom.getCash_5c().ToString(), (cash_denom.getCash_5c() * 0.05).ToString("N2"));

            DataTable dt_tblbottom = new DataTable();
            dt_tblbottom.Columns.Add(); dt_tblbottom.Columns.Add();
            dt_tblbottom.Rows.Add("TOTAL AMOUNT: ", cash_denom.get_totalamount().ToString("N2"));

            Graphics g;
            if (bmp == null)
                g = e.Graphics;
            else
            {
                g = Graphics.FromImage(bmp);
                cls_globalvariables.previewmul = 2;
            }

            int nY = 0;
            int nX = 0;
            int maxwidth = 280;
            if (cls_globalvariables.print_receipt_format_v == "76mm_journal")
            {
                maxwidth = 253;

            }

            //business Title
            Rectangle rect_title = new Rectangle(nX, nY, maxwidth * cls_globalvariables.previewmul, 15);
            nY += DrawString(g, sBusinessName, fncHardware.font_Title, rect_title, fncHardware.brush_Black, fncHardware.format_center());

            //header 1
            List<Rectangle> rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_header1 = fncHardware.create_font_list(new int[] { 3 });
            nY = DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);

            //header 2
            List<Rectangle> rect_header2 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth / 6, maxwidth / 3, maxwidth / 6, maxwidth / 3 });
            List<StringFormat> sf_header2 = fncHardware.create_stringformat_list(new int[] { 1, 3, 1, 3 });
            List<Font> font_header2 = fncHardware.create_font_list(new int[] { 3, 3, 3, 3 });
            nY = DrawStringDataTable(g, dt_header2, font_header2, rect_header2, fncHardware.brush_Black, sf_header2);

            //space
            nY += 10;

            //table header
            List<Rectangle> rect_tblheader = fncHardware.create_rect_list(nX, nY, new int[] { 3 * maxwidth / 7, 1 * maxwidth / 7, 3 * maxwidth / 7 });
            List<StringFormat> sf_tblheader = fncHardware.create_stringformat_list(new int[] { 1, 2, 3 });
            List<Font> font_tblheader = fncHardware.create_font_list(new int[] { 3, 3, 3 });
            nY = DrawStringDataTable(g, dt_tblheader, font_tblheader, rect_tblheader, fncHardware.brush_Black, sf_tblheader);

            //-----------line-------------
            nY += 5;
            g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            //table data2
            List<Rectangle> rect_tbldata2 = fncHardware.create_rect_list(nX, nY, new int[] { 3 * maxwidth / 7, 1 * maxwidth / 7, 3 * maxwidth / 7 });
            List<StringFormat> sf_tbldata2 = fncHardware.create_stringformat_list(new int[] { 1, 2, 3 });
            List<Font> font_tbldata2 = fncHardware.create_font_list(new int[] { 3, 3, 3 });
            nY = DrawStringDataTable(g, dt_tbldata2, font_tbldata2, rect_tbldata2, fncHardware.brush_Black, sf_tbldata2);

            //-----------line-------------
            nY += 5;
            g.DrawLine(new Pen(fncHardware.brush_Black), nX, nY, maxwidth * cls_globalvariables.previewmul, nY); nY += 5;

            //table bottom
            List<Rectangle> rect_tblbottom = fncHardware.create_rect_list(nX, nY, new int[] { 5 * maxwidth / 14, 9 * maxwidth / 14 });
            List<StringFormat> sf_tblbottom = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            List<Font> font_tblbottom = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = DrawStringDataTable(g, dt_tblbottom, font_tblbottom, rect_tblbottom, fncHardware.brush_Black, sf_tblbottom);

            cls_globalvariables.previewmul = 1;

            return g;
        }

        public static void CheckPrinterStatus()
        {
            string statusReport = null;
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues();
            foreach (PrintQueue pq in printQueuesOnLocalServer)
                if (pq.Name == cls_globalvariables.DefaultPrinter_v)
                {
                    statusReport = "";
                    if (pq.HasPaperProblem)
                        statusReport = statusReport + "It has a paper problem. ";
                    if (!(pq.HasToner))
                        statusReport = statusReport + "It is out of toner. ";
                    if (pq.IsDoorOpened)
                        statusReport = statusReport + "It has an open door. ";
                    if (pq.IsInError)
                        statusReport = statusReport + "It is in an error state. ";
                    if (pq.IsNotAvailable)
                        statusReport = statusReport + "It is not available. ";
                    if (pq.IsOffline)
                        statusReport = statusReport + "It is offline. ";
                    if (pq.IsOutOfMemory)
                        statusReport = statusReport + "It is out of memory. ";
                    if (pq.IsOutOfPaper)
                        statusReport = statusReport + "It is out of paper. ";
                    if (pq.IsOutputBinFull)
                        statusReport = statusReport + "It has a full output bin. ";
                    if (pq.IsPaperJammed)
                        statusReport = statusReport + "It has a paper jam. ";
                    if (pq.IsPaused)
                        statusReport = statusReport + "It is paused. ";
                    if (pq.IsTonerLow)
                        statusReport = statusReport + "It is low on toner. ";
                    if (pq.NeedUserIntervention)
                        statusReport = statusReport + "It needs user intervention. ";
                }
            if (statusReport == null)
            {
                DialogHelper.ShowDialog("Printer default name is blank or wrong.");
                return;
            }
            if (statusReport.Length > 0)
            {
                statusReport = "Printer has encounter problem/s:\n\n" + statusReport;
                DialogHelper.ShowDialog(statusReport);
            }
            else
                DialogHelper.ShowDialog("Printer has no problem.");
        }
    }
}
