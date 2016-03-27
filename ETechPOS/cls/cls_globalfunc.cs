using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using ETech.cls;
using ETech.fnc;
using ETech.FormatDesigner;
using ETech.Helpers;

namespace ETech.cls
{
    public static class cls_globalfunc
    {
        [DllImport("User32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool IsWindowVisible(IntPtr hWnd);

        //private static int Keyboard_Width = 695;
        //private static int Keyboard_Height = 250;
        //private static int Keypad_Width = 188;
        //private static int Keypad_Height = 254;
        private static double keyboard_ratio = 2.76;
        private static double keypad_ratio = 0.7076923076923077;

        public static int getCreditDebiCardInfo(string cardno)
        {
            string tempstr = string.Empty;
            return getCreditDebiCardInfo(cardno, out tempstr);
        }
        public static int getCreditDebiCardInfo(string cardno, out string CardName)
        {
            if (cardno.StartsWith("5"))
            {
                CardName = "MASTERCARD";
                return 5;
            }
            else if (cardno.StartsWith("4"))
            {
                CardName = "VISA";
                return 4;
            }
            else if (cardno.StartsWith("35"))
            {
                CardName = "JCB";
                return 2;
            }
            else if (cardno.StartsWith("34") || cardno.StartsWith("37"))
            {
                CardName = "AMEX";
                return 3;
            }
            else if (cardno.StartsWith("2") || cardno.StartsWith("30")
                  || cardno.StartsWith("33") || cardno.StartsWith("36") || cardno.StartsWith("39"))
            {
                CardName = "DINERS";
                return 1;
            }
            else
            {
                CardName = "OTHER CARDS";
                return 0;
            }
        }

        public static void DeleteUnusedSalesHead()
        {
            // done to avoid deletion of current pending transaction 
            return;

            string sql = @"
                SELECT
                    `ornumber`
                FROM
                    `saleshead`
                WHERE
                    `branchid`=" + cls_globalvariables.Branch.Id + @"
                    AND `terminalno` = " + cls_globalvariables.TerminalNumber + @"
                    AND `date` >= '" + DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd") + @"'
                    AND `status` = 1
                ORDER BY `ornumber` DESC LIMIT 1;";
            DataTable DT = mySQLFunc.getdb(sql);
            if (DT == null || DT.Rows.Count <= 0)
                return;
            sql = @"
                SELECT
                    GROUP_CONCAT(`SyncId`) AS 'wids', GROUP_CONCAT(`ornumber`) AS 'ornumbers'
                FROM
                    `saleshead`
                WHERE
                    `branchid`=" + cls_globalvariables.Branch.Id + @"
                    AND `terminalno` = " + cls_globalvariables.TerminalNumber + @"
                    AND `date` >= '" + DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd") + @"'
                    AND `ornumber` > " + DT.Rows[0]["ornumber"].ToString() + @"
                    AND `status` = 0";

            DT = mySQLFunc.getdb(sql);
            if (DT == null || DT.Rows[0]["wids"] == null || DT.Rows[0]["wids"].ToString() == "")
                return;
            string wids = DT.Rows[0]["wids"].ToString();
            string ornumbers = DT.Rows[0]["wids"].ToString();

            sql = @"DELETE FROM saleshead WHERE `SyncId` IN (" + wids + ")";
            mySQLFunc.setdb(sql);
            LogsHelper.WriteToTLog("DELETED2 in saleshead ors = " + ornumbers);
        }

        public static void CreateIfMissing(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void formaddkbkpevent(Form form)
        {
            if (!cls_globalvariables.AutoShowKeyboard_v)
                return;
            form.Click += new EventHandler(frmclosekbkp_Click);
            foreach (Control ctrl in form.Controls)
                GetAllControls(ctrl);
        }
        public static void GetAllControls(Control container)
        {
            if (container is TextBox || container is NumericUpDown)
            {
                tbshowkbkp(container);
                tbclosekbkp(container);
            }
            foreach (Control ctrl in container.Controls)
                GetAllControls(ctrl);
        }
        public static void tbshowkbkp(Control control)
        {
            if (control.Tag == null || control.Tag.ToString() == "")
                control.Click += new EventHandler(tbshowkeyboard_Click);
            else if (control.Tag.ToString() == "num")
                control.Click += new EventHandler(tbshowkeypad_Click);
            else
                control.Click += new EventHandler(tbshowkeyboard_Click);
        }
        public static void tbclosekbkp(Control control)
        {
            control.LostFocus += new EventHandler(tbclosekbkp_LostFocus);
        }
        public static void tbshowkeyboard_Click(Object sender, EventArgs e)
        {
            try
            {
                List<string> processnames = new List<string>();
                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName == "Keypad")
                        theprocess.Kill();
                    processnames.Add(theprocess.ProcessName);
                }
                int height = Convert.ToInt32((decimal)Screen.PrimaryScreen.Bounds.Height * (decimal)0.352864);
                int width = (int)((double)height * keyboard_ratio);
                if (height < 291)
                {
                    width = 690;
                    height = 291;
                }
                int adjust = (height > 291) ? height - 291 : 0;
                int x = (Screen.PrimaryScreen.Bounds.Width - width) / 2;
                int y = Screen.PrimaryScreen.Bounds.Height - height - adjust;
                if (!processnames.Contains("Keyboard"))
                    Start(Application.StartupPath + "/tools/Keyboard.exe", x, y, width, 1);
            }
            catch
            {
            }
        }
        public static void tbshowkeypad_Click(Object sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox)
                {
                    TextBox tb = (TextBox)sender;
                    tb.SelectAll();
                }

                List<string> processnames = new List<string>();
                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName == "Keyboard")
                        theprocess.Kill();
                    processnames.Add(theprocess.ProcessName);
                }
                int height = Convert.ToInt32((decimal)Screen.PrimaryScreen.Bounds.Height * (decimal)0.459635);
                int width = (int)((double)height * keypad_ratio);
                if (height < 260)
                {
                    width = 184;
                    height = 260;
                }
                int adjust = (height > 260) ? height - 260 : 0;
                int x = (Screen.PrimaryScreen.Bounds.Width - width);
                int y = Screen.PrimaryScreen.Bounds.Height - height;
                if (!processnames.Contains("Keypad"))
                    Start(Application.StartupPath + "/tools/Keypad.exe", x, y, width, 1);
            }
            catch
            {
            }
        }
        public static void tbclosekbkp_LostFocus(Object sender, EventArgs e)
        {
            closekbkp();
        }
        public static void frmclosekbkp_FormClosed(Object sender, FormClosedEventArgs e)
        {
            closekbkp();
        }
        public static void frmclosekbkp_Click(Object sender, EventArgs e)
        {
            closekbkp();
        }
        private static void closekbkp()
        {
            try
            {
                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName == "Keyboard" || theprocess.ProcessName == "Keypad")
                        theprocess.Kill();
                }
            }
            catch
            {
            }
        }
        private static void Start(string fName)
        {
            const int MAX_WAIT = 200;
            int counter = 0;
            using (Process p = Process.Start(fName))
            {
                while ((p.MainWindowHandle == IntPtr.Zero) || !IsWindowVisible(p.MainWindowHandle))
                {
                    System.Threading.Thread.Sleep(10);
                    p.Refresh();
                    counter++;
                    if (counter > MAX_WAIT)
                        return;
                }
                p.WaitForInputIdle(10000);
                MoveWindow(p.MainWindowHandle, 0, 0, 1, 1, true);
            }
        }
        private static void Start(string fName, int x, int y, int Keyboard_Width, int Keyboard_Height)
        {
            const int MAX_WAIT = 200;
            int counter = 0;
            using (Process p = Process.Start(fName))
            {
                while ((p.MainWindowHandle == IntPtr.Zero) || !IsWindowVisible(p.MainWindowHandle))
                {
                    System.Threading.Thread.Sleep(10);
                    p.Refresh();
                    counter++;
                    if (counter > MAX_WAIT)
                        return;
                }
                p.WaitForInputIdle(10000);
                MoveWindow(p.MainWindowHandle, x, y, Keyboard_Width, Keyboard_Height, true);
            }
        }
        private static void StartCallback(IAsyncResult ar)
        {
            System.Runtime.Remoting.Messaging.AsyncResult result = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
            Action<string> del = (Action<string>)result.AsyncDelegate;
            try
            {
                del.EndInvoke(ar);
            }
            catch { }
        }

        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }
        public static string GetMacAddress(string connectionName)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                if (adapter.Name == connectionName)
                    return adapter.GetPhysicalAddress().ToString();
            return "";
        }
        public static bool CheckMacAddress()
        {
            string setMacAddress = cls_globalvariables.POSMacAddress_v.Replace("-", "");

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                string physicaladdress = adapter.GetPhysicalAddress().ToString();
                if (physicaladdress == "")
                    continue;
                if (setMacAddress == physicaladdress)
                {
                    Console.WriteLine("MAC address:" + adapter.GetPhysicalAddress().ToString());
                    return true;
                }
            }
            return false;
        }
        public static bool CheckDatabaseVersion()
        {
            return true;

            string sql = @"SELECT * FROM config WHERE `particular`='version'";
            DataTable DT = mySQLFunc.getdb(sql);
            if (DT == null)
                return false;
            if (DT.Rows.Count <= 0)
                return false;

            decimal dbversion = Convert.ToDecimal(DT.Rows[0]["value"]);
            if (2.8M > dbversion)
                return false;

            return true;
        }
        public static bool isReceiptInTransList(List<cls_POSTransaction> TransactionLists, long ornumber)
        {
            foreach (cls_POSTransaction temptran in TransactionLists)
            {
                if (temptran.getORnumber() == ornumber)
                {
                    fncFilter.alert("OR is still in Transaction List!");
                    return true;
                }
            }
            return false;
        }
        
        public static string CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }
    }
}
