using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using ETech.Models.Global;
using ETech.Models.Database;

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
        public static string ApplicationErrorFolderPath = cls_globalfunc.CreateDirectoryIfNotExists(Application.StartupPath + "/Errors");
        public static string MyDocumentApplicationFolderPath = cls_globalfunc.CreateDirectoryIfNotExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + ApplicationName);
        public static string MyDocumentBackupFolderPath = cls_globalfunc.CreateDirectoryIfNotExists(MyDocumentApplicationFolderPath + "/Database Backup");
        public static string ApplicationDataLocalCompanyNameFolderPath = cls_globalfunc.CreateDirectoryIfNotExists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + CompanyName);
        public static string ApplicationDataLocalApplicationFolderPath = cls_globalfunc.CreateDirectoryIfNotExists(ApplicationDataLocalCompanyNameFolderPath + "/" + ApplicationName);
        public static string ApplicationToolsFolderPath = ApplicationFolderPath + "/Tools";

        public static string ErrorExceptionLogsFilePath
        {
            get { return ApplicationFolderPath + "/Errors/ExcemptionErrors.txt"; }
        }
        public static string PosTLogsFilePath
        {
            get { return MyDocumentApplicationFolderPath + "/" + DateTime.Now.ToString("yyMMdd") + "POS" + cls_globalvariables.TerminalNumber + ".T"; }
        }
        public static string PosCsvLogsFilePath
        {
            get { return ApplicationErrorFolderPath + "/" + DateTime.Now.ToString("yyMMdd") + "POS" + cls_globalvariables.TerminalNumber + ".C"; }
        }
        public static string ConnectionSettingsXmlPath
        {
            get { return ApplicationFolderPath + "/Connection Settings.xml"; }
        }
        public static string MySqlDumpApplicationPath
        {
            get { return ApplicationToolsFolderPath + "/mysqldump"; }
        }

        public static Branch Branch = null;
        public static ConnectionSettings ConnectionSettings = null;
        public static Settings Settings = null;

        public static long MainBranchCode = 1000;

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
        public static int dcdetail_senior5 = 5;

        public static int previewmul = 1;

        public static int dchead_defaultcustom = 10;
        public static DateTime companystartdate = new DateTime(2016, 1, 1, 0, 0, 0);
        public static DateTime companymaxdate = new DateTime(3000, 12, 31, 0, 0, 0);

        private static string colortheme = "";
        public static string colortheme_v
        {
            get { return colortheme; }
            set { colortheme = value; }
        }

        public static string settingspath = Application.StartupPath + "/settings.txt";

        public static int TerminalNumber
        {
            get { return ConnectionSettings.TerminalNumber; }
        }
        public static string com_v
        {
            get { return Settings.GetValue1("Com Port Number").ToString(); }
        }
        public static string disp1_v
        {
            get { return Settings.GetValue1("Customer Display", "Display 1").ToString(); }
        }
        public static string disp2_v
        {
            get { return Settings.GetValue1("Customer Display", "Display 2").ToString(); }
        }
        public static string BusinessName_v
        {
            get { return Settings.GetValue1("Business Information", "Business Name").ToString(); }
        }
        public static string Owner_v
        {
            get { return Settings.GetValue1("Business Information", "Owner").ToString(); }
        }
        public static string TIN_v
        {
            get { return Settings.GetValue1("Business Information", "TIN").ToString(); }
        }
        public static string Address_v
        {
            get { return Settings.GetValue1("Business Information", "Address").ToString(); }
        }
        public static string PermitNo_v
        {
            get { return Settings.GetValue1("Business Information", "Permit Number").ToString(); }
        }
        public static string ACC_v
        {
            get { return Settings.GetValue1("Business Information", "ACC").ToString(); }
        }
        public static string Serial_v
        {
            get { return Settings.GetValue1("Business Information", "Serial Number").ToString(); }
        }
        public static string MIN_v
        {
            get { return Settings.GetValue1("Business Information", "MIN").ToString(); }
        }
        public static string orfooter1_v
        {
            get { return Settings.GetValue1("Receipt Display", "Footer 1").ToString(); }
        }
        public static string orfooter2_v
        {
            get { return Settings.GetValue1("Receipt Display", "Footer 2").ToString(); }
        }
        public static string orfooter3_v
        {
            get { return Settings.GetValue1("Receipt Display", "Footer 3").ToString(); }
        }
        public static string orfooter4_v
        {
            get { return Settings.GetValue1("Receipt Display", "Footer 4").ToString(); }
        }
        public static bool avoidinvalidpprice_v
        {
            get { return Settings.GetValue1("Avoid Invalid Purchase Price").ToString() == "1"; }
        }
        public static string print_receipt_format_v
        {
            get { return Settings.GetValue1("Print Receipt Format").ToString(); }
        }
        public static bool allowZeroPrice_v
        {
            get { return Settings.GetValue1("Allow Zero Price").ToString() == "1"; }
        }
        public static string grossmethod_v
        {
            get { return Settings.GetValue1("Gross Method").ToString(); }
        }
        public static string showdetailCCinZRead_v
        {
            get { return Settings.GetValue1("Show Detail Creditcard in ZRead").ToString(); }
        }
        public static int ORPrintCount_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("OR Print Count")); }
        }
        public static string PosName
        {
            get { return Settings.GetValue1("POS Name").ToString(); }
        }
        public static int LocalTax_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Local Tax")); }
        }
        public static int ServiceCharge_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Service Charge")); }
        }
        public static int RefundMemo_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Refund Memo")); }
        }
        public static string DefaultPrinter_v
        {
            get { return Settings.GetValue1("Default Printer").ToString(); }
        }
        public static bool PreviewOR_v
        {
            get { return Settings.GetValue1("Preview OR").ToString() == "1"; }
        }
        public static string prodsearchstyle_v
        {
            get { return Settings.GetValue1("Product Search Style").ToString(); }
        }
        public static string ads_url_v
        {
            get { return Settings.GetValue1("Advertising URL").ToString(); }
        }
        public static int maximum_cash_collection_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Maximum Cash Collection")); }
        }
        public static int readDateRange_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Read Date Range")); }
        }
        public static string POSMacAddress_v
        {
            get { return Settings.GetValue1("POS Mac Address").ToString(); }
        }
        public static int DiscountDetails_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Discount Details")); }
        }
        public static int CustomerDisplayLength_v
        {
            get { return Convert.ToInt32(Settings.GetValue1("Customer Display Length")); }
        }
        public static string PosProviderName_v
        {
            get { return Settings.GetValue1("POS Provider Name").ToString(); }
        }
        public static string PosProviderAddress_v
        {
            get { return Settings.GetValue1("POS Provider Address").ToString(); }
        }
        public static string PosProviderTIN_v
        {
            get { return Settings.GetValue1("POS Provider TIN").ToString(); }
        }
        public static string ACC_date_v
        {
            get { return Settings.GetValue1("ACC Date").ToString(); }
        }
        public static int print_receipt_actual
        {
            get { return Convert.ToInt32(Settings.GetValue1("Print Receipt Actual")); }
        }
        public static int print_receipt_limit
        {
            get { return Convert.ToInt32(Settings.GetValue1("Print Receipt Limit")); }
        }
        public static int print_receipt_buffer
        {
            get { return Convert.ToInt32(Settings.GetValue1("Print Receipt Buffer")); }
        }
        public static List<BackupDatabaseDetail> BackupDatabaseDetailList
        {
            get
            {
                List<Setting> filteredSettings = Settings.Where(x => x.Title == "Backup Database Detail").ToList();
                List<BackupDatabaseDetail> backupDatabaseDetailList = new List<BackupDatabaseDetail>();
                foreach (Setting setting in filteredSettings)
                    backupDatabaseDetailList.Add(new BackupDatabaseDetail(setting.Title, setting.Value1.ToString(), setting.Value2.ToString()));
                return backupDatabaseDetailList;
            }
        }
        public static int BackupDatabaseDaysDuration
        {
            get { return Convert.ToInt32(Settings.GetValue1("Backup Database Detail", "Days Duration")); }
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

        public static byte[] OpenDrawerBytes
        {
            get { return new byte[] { 27, 112, 0, 25, 250 }; }
        }

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
