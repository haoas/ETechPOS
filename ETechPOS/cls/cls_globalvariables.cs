using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using ETech.Models.Global;

namespace ETech.cls
{
    public static class cls_globalvariables
    {
        public static string ApplicationName = Assembly.GetExecutingAssembly().GetName().Name;
        public static string ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
        public static string CompanyName
        {
            get
            {
                string companyName = "";
                foreach (Attribute attr in Attribute.GetCustomAttributes(Assembly.GetExecutingAssembly()))
                    if (attr.GetType() == typeof(AssemblyCompanyAttribute))
                        companyName = ((AssemblyCompanyAttribute)attr).Company;
                return companyName;
            }
        }

        public static string ApplicationFolderPath = Application.StartupPath;
        public static string ApplicationDataLocalCompanyNameFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + CompanyName;
        public static string ApplicationDataLocalApplicationFolderPath = ApplicationDataLocalCompanyNameFolderPath + "/" + ApplicationName;

        public static string PosTLogsFilePath = ApplicationDataLocalApplicationFolderPath + "/" + DateTime.Now.ToString("yyMMdd") + "POS" + cls_globalvariables.terminalno_v + ".T";
        public static string PosCsvLogsFilePath = ApplicationDataLocalApplicationFolderPath + "/" + DateTime.Now.ToString("yyMMdd") + "POS" + cls_globalvariables.terminalno_v + ".C";
        public static string ConnectionSettingsXmlPath = ApplicationFolderPath + "/Connection Settings.xml";

        public static ConnectionSettings ConnectionSettings = null;

        public static string MainBranchCode = "1000";

        public static decimal vat = 0.12M;
        public static decimal senior = 0.20M;
        public static decimal senior5 = 0.05M;

        public static int dchead_customdiscounttype = -1;
        public static int dchead_adjusttype = 0;
        public static int dchead_discounttype = 1;
        public static int dchead_membertype = 2;
        public static int dchead_pospromotype = 3;

        public static int dcdetail_customdiscounttype = -1;
        public static int dcdetail_adjusttype = 0;
        public static int dcdetail_discounttype = 1;
        public static int dcdetail_senior = 2;
        public static int dcdetail_nonvat = 3;
        public static int dcdetail_promoqty = 4;
        public static int dcdetail_senior5 = 5;

        public static int previewmul = 1;

        public static int dchead_defaultcustom = 10;
        public static DateTime companystartdate = new DateTime(2016, 1, 1, 0, 0, 0);
        public static DateTime companymaxdate = new DateTime(3000, 12, 31, 0, 0, 0);

        public static string settingspath = Application.StartupPath + "/settings.txt";

        public static string mydocumentpath = createDirIfNotExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ETECH POS\");

        public static string createDirIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }

        public static string systemlogpath = mydocumentpath + "ETechPOS_SystemLogs.txt";

        private static string posname = "ETECH POS SYSTEM";
        public static string posname_v
        {
            get { return posname; }
            set { posname = value; }
        }

        private static string version = "1.0.0";
        public static string version_v
        {
            get { return version; }
            set { version = value; }
        }

        private static string terminalno = "01";
        public static string terminalno_v
        {
            get { return terminalno; }
            set
            {
                if (value.Length != 2)
                    throw new Exception("Invalid Terminal no");
                else
                    terminalno = value;
            }
        }

        private static string com = "";
        public static string com_v
        {
            get { return com; }
            set { com = value; }
        }

        private static string disp1 = "";
        public static string disp1_v
        {
            get { return disp1; }
            set { disp1 = value; }
        }

        private static string disp2 = "";
        public static string disp2_v
        {
            get { return disp2; }
            set { disp2 = value; }
        }

        private static string branchcode = "";
        public static string BranchCode
        {
            get { return branchcode; }
            set
            {
                if (value.Length != 4)
                    throw new Exception("Invalid BranchCode");
                else
                {
                    branchcode = value;
                    string selectSql = @"SELECT `name` FROM `branch` WHERE Id = '" + value + "'";
                    DataTable resultDt = mySQLFunc.getdb(selectSql);
                    if (resultDt == null || resultDt.Rows.Count <= 0)
                        return;
                    BranchName = resultDt.Rows[0]["name"].ToString();
                }
            }
        }

        private static string branchname = "";
        public static string BranchName
        {
            get { return branchname; }
            set { branchname = value; }
        }

        private static string BusinessName = "";
        public static string BusinessName_v
        {
            get { return BusinessName; }
            set { BusinessName = value; }
        }

        private static string Owner = "";
        public static string Owner_v
        {
            get { return Owner; }
            set { Owner = value; }
        }

        private static string TIN = "";
        public static string TIN_v
        {
            get { return TIN; }
            set { TIN = value; }
        }

        private static string Address = "";
        public static string Address_v
        {
            get { return Address; }
            set { Address = value; }
        }

        private static string PermitNo = "";
        public static string PermitNo_v
        {
            get { return PermitNo; }
            set { PermitNo = value; }
        }

        private static string ACC = "";
        public static string ACC_v
        {
            get { return ACC; }
            set { ACC = value; }
        }

        private static string Serial = "";
        public static string Serial_v
        {
            get { return Serial; }
            set { Serial = value; }
        }

        private static string MIN = "";
        public static string MIN_v
        {
            get { return MIN; }
            set { MIN = value; }
        }

        public static int qty_places = 2;
        public static int print_receipt_buffer = 0;
        public static int print_receipt_actual = 0;
        public static int print_receipt_limit = 0;
        public static int print_receipt_linespacing = 0;

        public static string PosProviderName_v;
        public static string PosProviderAddress_v;
        public static string PosProviderTIN_v;
        public static string ACC_date_v;

        private static string orfooter1 = "";
        public static string orfooter1_v
        {
            get { return orfooter1; }
            set { orfooter1 = value; }
        }

        private static string orfooter2 = "";
        public static string orfooter2_v
        {
            get { return orfooter2; }
            set { orfooter2 = value; }
        }

        private static string orfooter3 = "";
        public static string orfooter3_v
        {
            get { return orfooter3; }
            set { orfooter3 = value; }
        }

        private static string orfooter4 = "";
        public static string orfooter4_v
        {
            get { return orfooter4; }
            set { orfooter4 = value; }
        }

        private static string colortheme = "";
        public static string colortheme_v
        {
            get { return colortheme; }
            set { colortheme = value; }
        }

        private static string avoidinvalidpprice = "";
        public static string avoidinvalidpprice_v
        {
            get { return avoidinvalidpprice; }
            set { avoidinvalidpprice = value; }
        }

        private static string print_receipt_format = "";
        public static string print_receipt_format_v
        {
            get { return print_receipt_format; }
            set { print_receipt_format = value; }
        }

        private static string allowZeroPrice = "";
        public static string allowZeroPrice_v
        {
            get { return allowZeroPrice; }
            set { allowZeroPrice = value; }
        }

        private static int readDateRange = 1;
        public static int readDateRange_v
        {
            get { return readDateRange; }
            set { readDateRange = value; }
        }

        private static string grossmethod = "";
        public static string grossmethod_v
        {
            get { return grossmethod; }
            set { grossmethod = value; }
        }

        private static int ORPrintCount = 1;
        public static int ORPrintCount_v
        {
            get { return ORPrintCount; }
            set { ORPrintCount = value; }
        }

        private static decimal ServiceCharge = 0;
        public static decimal ServiceCharge_v
        {
            get { return ServiceCharge; }
            set { ServiceCharge = value; }
        }

        private static decimal LocalTax = 0;
        public static decimal LocalTax_v
        {
            get { return LocalTax; }
            set { LocalTax = value; }
        }

        private static string showdetailCCinZRead = "";
        public static string showdetailCCinZRead_v
        {
            get { return showdetailCCinZRead; }
            set { showdetailCCinZRead = value; }
        }

        private static string RefundMemo = "0";
        public static string RefundMemo_v
        {
            get { return RefundMemo; }
            set { RefundMemo = value; }
        }

        private static string prodsearchstyle = "0";
        public static string prodsearchstyle_v
        {
            get { return prodsearchstyle; }
            set { prodsearchstyle = value; }
        }

        private static int CustomerDisplayLength = 20;
        public static int CustomerDisplayLength_v
        {
            get { return CustomerDisplayLength; }
            set { value = CustomerDisplayLength; }
        }

        //RLC---
        private static Int64 starttime = 0;
        private static Int64 endtime = 0;

        public static Int64 starttime_v
        { get { return starttime; } set { starttime = value; } }
        public static Int64 endtime_v
        { get { return endtime; } set { endtime = value; } }

        private static int origwidth = 1292;
        public static int origwidth_v
        { get { return origwidth; } set { origwidth = value; } }
        private static int origheight = 768;
        public static int origheight_v
        { get { return origheight; } set { origheight = value; } }
        private static bool is4By3ratio;
        public static bool is4By3ratio_v
        { get { return is4By3ratio; } set { is4By3ratio = value; } }
        //------

        private static string DefaultPrinter = "";
        public static string DefaultPrinter_v
        { get { return DefaultPrinter; } set { DefaultPrinter = value; } }

        private static string PrinterODByte = "";
        public static string PrinterODByte_v
        { get { return PrinterODByte; } set { PrinterODByte = value; } }

        private static bool PreviewOR = false;
        public static bool PreviewOR_v
        { get { return PreviewOR; } set { PreviewOR = value; } }

        private static string ads_url = "";
        public static string ads_url_v
        { get { return ads_url; } set { ads_url = value; } }

        private static double maximum_cash_collection = 0;
        public static double maximum_cash_collection_v
        { get { return maximum_cash_collection; } set { maximum_cash_collection = value; } }

        private static bool AutoShowKeyboard = false;
        public static bool AutoShowKeyboard_v
        { get { return AutoShowKeyboard; } set { AutoShowKeyboard = value; } }

        private static string POSMacAddress = "";
        public static string POSMacAddress_v
        { get { return POSMacAddress; } set { POSMacAddress = value; } }

        private static int DiscountDetails = 1;
        public static int DiscountDetails_v
        { get { return DiscountDetails; } set { DiscountDetails = value; } }

        public static string warning_lack_of_payment = "There's still remaining amount due.";
        public static string warning_refunded_transaction_cannot_be_voided = "This transaction cannot be void because it has refunded item/s. Please void the refund item/s first before void this transaction.";

        public static string warning_customer_needed = "Select a registered customer first.";
        public static string warning_authcode_needed = "Please fill up auth code!";
        public static string warning_cardholder_needed = "Please indicated Card Holder.";
        public static string warning_invalid_amount = "Invalid Amount.";
        public static string warning_approvalcode_needed = "Please enter Approval Code.";
        public static string warning_input_invalid = "Invalid Input!";
        public static string warning_acctno_invalid = "Invalid Account Number!";
        public static string warning_chequedate_invalid = "Invalid Cheque Date!";
        public static string warning_amount_invalid = "Invalid Amount!";
        public static string warning_giftcheque_invalid = "Invalid Gift Cheque";
        public static string warning_discount_invalid = "Invalid discount!";
        public static string warning_cardno_invalid = "Invalid Card Number!";
        public static string warning_monthofexp_invalid = "Invalid Month of Expiry!";
        public static string warning_yearofexp_invalid = "Invalid Year of Expiry!";
        public static string warning_userpass_invalid = "Invalid Username or Password.";
        public static string warning_authcode_invalid = "Invalid Permission Code!";
        public static string warning_quantity_invalid = "Invalid Quantity!";
        public static string warning_transaction_invalid = "Invalid Transaction!";
        public static string warning_price_invalid = "Invalid Price!";
        public static string warning_ornumber_invalid = "Invalid OR Number!";
        public static string warning_cashtender_invalid = "Invalid Cash Amount!";
        public static string warning_mempointtender_invalid = "Invalid Member Point Amount!";

        public static string warning_card_expired = "Card already expired!";
        public static string warning_giftcheque_beenused = "This Gift Cheque was been used";
        public static string warning_discount_outofrange = "Discount percentage out of range.";
        public static string warning_samecashierchecker = "Cashier and Checker must be two different person.";

        public static string warning_userauthorization_denied = "User is not authorized to use this function.";
        public static string warning_member_notregistered = "Member not yet registered.";
        public static string warning_product_notfound = "Product not found.";
        public static string warning_notransactionyet = "No Transaction Found.";
        public static string warning_no_custom_payment = "There is no custom payment available.";

        public static string warning_promo_notavailable = "This promo is not available.";
        public static string warning_promo_amtnotvalid = "Current total amount does not reach the required amount to avail this promo.";

        public static string confirm_logout = "";
        public static string confirm_logout_voidtran = "Logout?";
        public static string confirm_logout_deletetran = "Do you want to void current transaction?";
        public static string confirm_logout_deleteitem = "Do you want to delete selected item?";
        public static string confirm_customer_debt = "There's still remaining amount due. Add to Customer's debt?";
        public static string confirm_unlock_zreading = "Do you want to continue transacting?\nnote: You need to generate Zreading again \n\n 今天结账单已被打印过。\n如果再开单，要从新打印结账单。是否继续？";
        public static string CashierAcct_wid = "";

    }
}
