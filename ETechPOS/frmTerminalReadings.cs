using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.fnc;
using System.IO;
using ETech.Helpers;

namespace ETech
{
    public partial class frmTerminalReadings : Form
    {
        public int origwidth = 296;
        public int origheight = 3000;
        public int zoompercent = 200;

        public frmTerminalReadings()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            pbPreview.Width = origwidth;
            pbPreview.Height = origheight;

            if (!Directory.Exists(cls_globalvariables.mydocumentpath))
                Directory.CreateDirectory(cls_globalvariables.mydocumentpath);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem.ToString() == "CASHIER ACCOUNTABILITY")
                    refresh_DGVcashier();
            }
            catch (Exception) { }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem.ToString() == "CASHIER ACCOUNTABILITY")
                    refresh_DGVcashier();
            }
            catch (Exception) { }
        }

        public void refresh_DGVcashier()
        {
            DateTime Ddatefrom = dateTimePicker1.Value.Date;
            DateTime Ddateto = dateTimePicker2.Value.Date;
            string datefrom = Ddatefrom.ToString("yyyy-MM-dd 00:00:00");
            string dateto = Ddateto.ToString("yyyy-MM-dd 23:59:59");
            string terminalno = cls_globalvariables.terminalno_v;
            string branchid = cls_globalvariables.BranchCode;

            string sql = @"
            SELECT '' as `SyncId`,'ALL' as `fullname`
            UNION
            SELECT DISTINCT U.`SyncId`, U.`fullname` 
            FROM `Saleshead` AS H
            LEFT JOIN user AS U ON U.syncid = H.`userid`
            WHERE H.`branchid` = " + branchid + @" 
            AND (H.`date` BETWEEN '" + datefrom + @"' AND '" + dateto + @"') 
            AND H.`terminalno` = " + terminalno + @" 
            AND H.`status` = 1 ORDER BY `SyncId`";

            DataTable DT = mySQLFunc.getdb(sql);

            DGVcashiers.DataSource = DT;
            foreach (DataGridViewColumn col in DGVcashiers.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        public void Z_term_cashier_reading(int printtype, bool print)
        {
            DateTime Ddatefrom = dateTimePicker1.Value;
            DateTime Ddateto = dateTimePicker2.Value;
            if (printtype == 1)
            {
                if (Ddatefrom == Ddateto)
                {
                    DialogHelper.ShowDialog("Date To/From cannot be the same");
                    return;
                }
                if (!zreadFunc.Zread_exist(Ddateto))
                {
                    DialogHelper.ShowDialog("Z-reading for " + Ddateto.ToString("yyyy-MM-dd") + @" is not yet generated!.");
                    return;
                }
            }
            if (print)
                fncHardware.print_zread(printtype, Ddatefrom, Ddateto, 0);
            else
            {
                SaveBitmapCashierAccountability(printtype, Ddatefrom, Ddateto, 0);
            }
        }

        public void GenerateReport(bool print, int printcnt)
        {
            DateTime Ddatefrom = dateTimePicker1.Value.Date;
            DateTime Ddateto = dateTimePicker2.Value.Date;

            //Does not proceed if dateto > datefrom
            if (DateTime.Compare(Ddatefrom, Ddateto) > 0)
                return;

            string datefrom = Ddatefrom.ToString("yyyy-MM-dd");
            string dateto = Ddateto.ToString("yyyy-MM-dd");

            string GroupByCondition = "";
            string reportname = listBox1.SelectedItem.ToString();
            string SQL = "";
            int reporttype = 0;

            if (reportname == "Z-READING")
            {
                Z_term_cashier_reading(0, print);
                return;
            }
            else if (reportname == "CASHIER ACCOUNTABILITY")
            {
                Z_term_cashier_reading(2, print);
                return;
            }
            else if (reportname == "TERMINAL ACCOUNTABILITY")
            {
                Z_term_cashier_reading(1, print);
                return;
            }
            else if (reportname == "HOURLY SALES")
            {
                int hourfrom = Convert.ToInt32(numericUpDown1.Value);
                int hourto = Convert.ToInt32(numericUpDown2.Value);
                string TIMESQL = @"";
                for (int x = hourfrom; x <= hourto; x++)
                {
                    TIMESQL += "UNION ALL SELECT '" + x.ToString().PadLeft(2, '0') + @":00' as 'time' ";
                }
                TIMESQL = TIMESQL.Substring(10, TIMESQL.Length - 10);

                SQL = @"SELECT A.`time`, COALESCE(B.`Count`,0) as 'Count',
                COALESCE(B.`AC`,0.00) as 'AC', COALESCE(B.`Amount`,0.00) as 'Amount'
                FROM (  " + TIMESQL + @" ) A
                LEFT JOIN ( SELECT CONCAT(LPAD(HOUR(SH.`date`),2,'0'),':00') as 'Time',
                FORMAT(SUM(1),0) as 'Count',
                FORMAT(ROUND(SUM(SD.`quantity`*SD.`price`)/SUM(1),2),2) as 'AC',
                FORMAT(ROUND(SUM(SD.`quantity`*SD.`price`),2),2) as 'Amount'
                FROM saleshead as SH, salesdetail as SD
                WHERE SH.`status` = 1
                AND SD.price <> 0
                AND SH.branchid = " + cls_globalvariables.BranchCode + @"
                AND SH.terminalno = " + cls_globalvariables.terminalno_v + @"
                AND SD.`headid` = SH.`SyncId`
                AND SH.`date` >= '" + datefrom + @" 00:00:00'
                AND SH.`date` <= '" + dateto + @" 23:59:59'
                GROUP BY hour(SH.`date`) ) B on B.`Time` = A.`Time`";
            }
            else if (reportname == "DAILY SALES")
            {
                SQL = @"SELECT CAST(DATE(SH.`date`) as char) as 'Time',
                FORMAT(SUM(1),0) as 'Count',
                FORMAT(ROUND(SUM(SD.`quantity`*SD.`price`)/SUM(1),2),2) as 'AC',
                FORMAT(ROUND(SUM(SD.`quantity`*SD.`price`),2),2) as 'Amount'
                FROM saleshead as SH, salesdetail as SD
                WHERE SH.`status` = 1
                AND SD.price <> 0
                AND SH.branchid = " + cls_globalvariables.BranchCode + @"
                AND SH.terminalno = " + cls_globalvariables.terminalno_v + @"
                AND SD.`headid` = SH.`SyncId`
                AND SH.`date` >= '" + datefrom + @" 00:00:00'
                AND SH.`date` <= '" + dateto + @" 23:59:59'
                GROUP BY DAY(SH.`date`) ORDER BY `Time`";
            }
            else
            {
                reporttype = 1;
                if (reportname == "DEPARTMENT SALES")
                {
                    SQL =
                    @"SELECT D.`name` as 'name',
                    FORMAT(SUM(SD.`quantity`),0) as 'Count',
                    FORMAT(SUM(SD.`quantity`*SD.`price`),2) as 'Amount',
                    '' as '%amt'
                    FROM saleshead as SH, salesdetail as SD
                    LEFT JOIN product as P on P.`SyncId` = SD.`productid`
                    LEFT JOIN department as D on P.`departmentid` = D.`SyncId`
                    WHERE SH.`status` = 1  
                    AND SH.branchid = " + cls_globalvariables.BranchCode + @"
                    AND SH.terminalno = " + cls_globalvariables.terminalno_v + @"
                    AND SD.`headid` = SH.`SyncId`
                    AND SH.`date` >= '" + datefrom + @" 00:00:00'
                    AND SH.`date` <= '" + dateto + @" 23:59:59'
                    GROUP BY D.`SyncId`";
                }
                else
                {
                    GroupByCondition = @"";
                    if (reportname == "VAT SALES")
                        GroupByCondition = @" AND SD.vat > 0 ";
                    else if (reportname == "NONVAT SALES")
                        GroupByCondition = @" AND SD.vat <= 0 ";
                    else if (reportname == "SENIOR SALES")
                        GroupByCondition = @" AND LENGTH(SH.seniorname) > 1 AND SD.`senior`=1 ";

                    SQL = @" SELECT COALESCE(P.`product`,'N/A') as 'name',
	                        FORMAT(SUM(SD.`quantity`),0) as 'Count',
	                        FORMAT(SUM(SD.`quantity`*SD.`price`),2) as 'Amount',
	                        '' as '%amt'
	                        FROM saleshead as SH, salesdetail as SD, product as P
	                        WHERE SH.`status` = 1  
                            AND SD.price <> 0
                            AND SH.branchid = " + cls_globalvariables.BranchCode + @"
	                        AND SH.terminalno = " + cls_globalvariables.terminalno_v + @"
	                        AND SD.`headid` = SH.`SyncId`
	                        AND SH.`date` BETWEEN '" + datefrom + @" 00:00:00' AND '" + dateto + @" 23:59:59'
	                        AND P.`SyncId` = SD.`productid`
	                        " + GroupByCondition + @"
	                        GROUP BY P.`SyncId` HAVING `Count` <> 0";
                }
            }
            DataTable DT_report = mySQLFunc.getdb(SQL);
            if (DT_report.Rows.Count <= 0)
            {
                ClearGraphics(pbPreview);
                DialogHelper.ShowDialog("No Transactions");
                return;
            }

            //Add days with 0 sales
            if (reportname == "DAILY SALES")
            {
                List<string> datelist = new List<string>();
                foreach (DataRow DR in DT_report.Rows)
                    datelist.Add(DR["Time"].ToString());

                DateTime temp_Ddatefrom = Ddatefrom;
                while (temp_Ddatefrom <= Ddateto)
                {
                    if (!datelist.Contains(temp_Ddatefrom.ToString("yyyy-MM-dd")))
                        DT_report.Rows.Add(temp_Ddatefrom.ToString("yyyy-MM-dd"), 0, "0.00", "0.00");
                    temp_Ddatefrom = temp_Ddatefrom.AddDays(1);
                }
                DT_report.DefaultView.Sort = "Time asc";
                DT_report = DT_report.DefaultView.ToTable();
            }

            if (reporttype == 1)
            {
                double sum = 0;
                int cnt = 0;
                foreach (DataRow DR in DT_report.Rows)
                    sum += Convert.ToDouble(DR["Amount"]);
                foreach (DataRow DR in DT_report.Rows)
                {
                    double amt = Convert.ToDouble(DT_report.Rows[cnt]["Amount"]);
                    DT_report.Rows[cnt]["%amt"] = ((amt / sum) * 100).ToString("N2") + "%";
                    cnt++;
                }
            }
            if (print)
                fncHardware.printpage_TerminalReadings(reporttype, reportname + " REPORT", DT_report, Ddatefrom, Ddateto, printcnt);
            else
                SaveBitmap(reporttype, reportname + " REPORT", DT_report, Ddatefrom, Ddateto);
        }
        public void ESCAPE()
        {
            this.Close();
        }

        private void frmTerminalReadings_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    int printcnt = Convert.ToInt16(num_printcnt.Value);
                    GenerateReport(true, printcnt);
                    break;
                case Keys.F2:
                    GenerateReport(false, 0);
                    break;
                case Keys.Escape:
                    ESCAPE();
                    break;
                default: return;
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void frmTerminalReadings_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = zreadFunc.GetMinSalesDate();
            dateTimePicker2.MinDate = zreadFunc.GetMinSalesDate();

            dateTimePicker1.MaxDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            dateTimePicker2.MaxDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            listBox1.SelectedIndex = 0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int printcnt = Convert.ToInt16(num_printcnt.Value);
            try
            {
                cls_globalvariables.CashierAcct_wid = DGVcashiers.CurrentRow.Cells[0].Value.ToString();
            }
            catch (Exception)
            { }
            GenerateReport(true, printcnt);
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                cls_globalvariables.CashierAcct_wid = DGVcashiers.CurrentRow.Cells[0].Value.ToString();
            }
            catch (Exception)
            { }
            if (!bgwLoadReceipt.IsBusy)
                bgwLoadReceipt.RunWorkerAsync();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            ESCAPE();
        }

        private void bgwLoadReceipt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GenerateReport(false, 0);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "HOURLY SALES")
                gbHourlySales.Visible = true;
            else
                gbHourlySales.Visible = false;

            if (listBox1.SelectedItem.ToString() == "CASHIER ACCOUNTABILITY")
            {
                DGVcashiers.Visible = true;
                refresh_DGVcashier();
            }
            else
                DGVcashiers.Visible = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value)
                numericUpDown2.Value = numericUpDown1.Value;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value)
                numericUpDown2.Value = numericUpDown1.Value;
        }

        private void SaveBitmap(int reporttype, string reportname, DataTable DT_report, DateTime Ddatefrom, DateTime Ddateto)
        {
            int width = Convert.ToInt32(origwidth * (zoompercent / (decimal)100));
            int height = Convert.ToInt32(origheight * (zoompercent / (decimal)100));
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(zoompercent, zoompercent);
            fncHardware.printpage_TerminalReadings(null, null, bmp, reporttype, reportname, DT_report, Ddatefrom, Ddateto);
            bmp.Save(cls_globalvariables.mydocumentpath + "Receipt.jpg");
            bmp.Dispose();
            Bitmap bitmap;
            using (var bmpTemp = new Bitmap(cls_globalvariables.mydocumentpath + "Receipt.jpg"))
                bitmap = new Bitmap(bmpTemp);
            pbPreview.Image = bitmap;
        }
        private void SaveBitmapTerminal(int printtype, DateTime datetime_d, DateTime datetimeTO_d, int userwid)
        {
            int width = Convert.ToInt32(origwidth * (zoompercent / (decimal)100));
            int height = Convert.ToInt32(origheight * (zoompercent / (decimal)100));
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(zoompercent, zoompercent);
            fncHardware.printpage_zread(null, null, bmp, printtype, datetime_d, datetimeTO_d);
            bmp.Save(cls_globalvariables.mydocumentpath + "Receipt.jpg");
            bmp.Dispose();
            Bitmap bitmap;
            using (var bmpTemp = new Bitmap(cls_globalvariables.mydocumentpath + "Receipt.jpg"))
                bitmap = new Bitmap(bmpTemp);
            pbPreview.Image = bitmap;
        }
        private void SaveBitmapCashier(int reporttype, string reportname, DataTable DT, DateTime datefrom, DateTime dateto, int printcnt)
        {
            int width = Convert.ToInt32(origwidth * (zoompercent / (decimal)100));
            int height = Convert.ToInt32(origheight * (zoompercent / (decimal)100));
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(zoompercent, zoompercent);
            fncHardware.printpage_TerminalReadings(null, null, bmp, reporttype, reportname, DT, datefrom, dateto);
            bmp.Save(cls_globalvariables.mydocumentpath + "Receipt.jpg");
            bmp.Dispose();
            Bitmap bitmap;
            using (var bmpTemp = new Bitmap(cls_globalvariables.mydocumentpath + "Receipt.jpg"))
                bitmap = new Bitmap(bmpTemp);
            pbPreview.Image = bitmap;
        }
        private void SaveBitmapCashierAccountability(int printtype, DateTime datetime_d, DateTime datetimeTO_d, int userwid)
        {
            int width = Convert.ToInt32(origwidth * (zoompercent / (decimal)100));
            int height = Convert.ToInt32(origheight * (zoompercent / (decimal)100));
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(zoompercent, zoompercent);

            fncHardware.printpage_zread(null, null, bmp, printtype, datetime_d, datetimeTO_d);

            bmp.Save(cls_globalvariables.mydocumentpath + "Receipt.jpg");
            bmp.Dispose();
            Bitmap bitmap;
            using (var bmpTemp = new Bitmap(cls_globalvariables.mydocumentpath + "Receipt.jpg"))
                bitmap = new Bitmap(bmpTemp);
            pbPreview.Image = bitmap;
        }
        private void ClearGraphics(Control control)
        {
            Graphics g = control.CreateGraphics();
            g.Clear(Color.White);
        }
    }
}
