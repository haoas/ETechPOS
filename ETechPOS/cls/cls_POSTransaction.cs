using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using ETech.ctrl;

namespace ETech.cls
{
    public class cls_POSTransaction
    {
        private cls_senior senior;
        private cls_nonvat nonvat;
        private int show;
        private int status;
        private cls_productlist productlist;

        public DateTime SalesDateTime { get; set; }
        public long SyncId { get; set; }
        public long ORNumber { get; set; }
        public decimal Discount { get; set; }
        public decimal Adjustment { get; set; }
        public string memo { get; set; }
        public cls_user Cashier { get; set; }
        public cls_user Checker { get; set; }
        public cls_user UserAuthorizer { get; set; }
        public string VatStatus { get; set; }
        public cls_payment Payments { get; set; }
        public cls_member Member { get; set; }
        public cls_customer Customer { get; set; }
        public cls_user SalesAgent { get; set; }

        private RoundedDecimal _RegularDiscountPercentage;
        public RoundedDecimal RegularDiscountPercentage
        {
            get { return _RegularDiscountPercentage; }
            set
            {
                _RegularDiscountPercentage = value;
                _RegularFixedDiscount = 0;
                foreach (cls_product prod in productlist.list_product)
                {
                    prod.TransactionRegularDiscountPercentage = value;
                }
            }
        }
        private RoundedDecimal _RegularFixedDiscount;
        public RoundedDecimal RegularFixedDiscount
        {
            get { return _RegularFixedDiscount; }
            set
            {
                _RegularFixedDiscount = value;
                _RegularDiscountPercentage = 0;
                foreach (cls_product prod in productlist.list_product)
                {
                    prod.TransactionRegularFixedDiscount = (value == 0) ? 0 : 
                        ((TotalOriginalPrice - value) / TotalOriginalPrice) * prod.OriginalPrice;
                }
            }
        }

        //Computed Get Fields


        //constructor
        public cls_POSTransaction()
        {
            this.SyncId = 0;
            this.ORNumber = 0;
            this.Adjustment = 0;
            this.Discount = 0;
            this.SalesDateTime = DateTime.Now;
            this.productlist = new cls_productlist();
            this.Cashier = new cls_user();
            this.Checker = new cls_user();
            this.SalesAgent = new cls_user();
            this.Customer = new cls_customer();
            this.Member = new cls_member();
            this.Payments = new cls_payment();
            this.senior = new cls_senior();
            this.nonvat = new cls_nonvat();
            this.memo = "";
            this.UserAuthorizer = new cls_user();
            this.show = 0;
            this.status = 0;
            this.VatStatus = "V";
        }

        public void setsenior(cls_senior senior_d)
        {
            this.senior = senior_d;
            if (this.senior.get_idnumber() != "")
            {
                this.get_productlist().set_issenior(true);
            }
            else
            {
                this.get_productlist().set_issenior(false);
            }
        }
        public cls_senior getsenior() { return this.senior; }

        public void setnonvat(cls_nonvat nonvat_d)
        {
            this.nonvat = nonvat_d;
            if (this.nonvat.get_idnumber() != "")
                this.get_productlist().set_isnonvat(true);
            else
                this.get_productlist().set_isnonvat(false);
        }
        public cls_nonvat getnonvat() { return this.nonvat; }

        public string getmode_str()
        {
            StringBuilder sbmode = new StringBuilder();
            if (this.productlist.get_iswholesale()) { sbmode.Append("WholeSale"); }
            if (this.productlist.get_issenior()) { if (sbmode.Length > 0) sbmode.Append(" | "); sbmode.Append("Senior"); }
            if (this.productlist.get_isnonvat()) { if (sbmode.Length > 0) sbmode.Append(" | "); sbmode.Append("VAT Exempt"); }
            return sbmode.ToString();
        }

        public cls_productlist get_productlist() { return this.productlist; }

        public int getShow() { return this.show; }
        public int getStatus() { return this.status; }

        //get changeamount
        public decimal get_changeamount()
        {
            decimal totalpaymentamt = this.Payments.get_totalamount();
            decimal change = totalpaymentamt - TotalAmount;
            return (change > 0) ? change : 0;
        }

        public void set_transaction_by_wid(long SyncId, bool status)
        {
            this.SyncId = SyncId;

            string sSQL = "SELECT * FROM `saleshead` WHERE `SyncId` = " + SyncId;
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.SyncId = 0;
                return;
            }

            DataRow dr = dt.Rows[0];
            this.ORNumber = long.Parse(dr["ornumber"].ToString());
            this.Adjustment = Convert.ToDecimal(dr["adjust"]);
            this.Discount = Convert.ToDecimal(dr["discount1"]);
            this.SalesDateTime = DateTime.Parse(dr["date"].ToString());
            this.senior = new cls_senior();
            this.senior.set_senior(dr["seniorno"].ToString(), dr["seniorname"].ToString());
            this.memo = dr["memo"].ToString();
            Int32.TryParse(dr["show"].ToString(), out this.show);
            Int32.TryParse(dr["status"].ToString(), out this.status);

            this.Cashier = new cls_user(Convert.ToInt32(dr["userid"]), status);
            this.Checker = new cls_user(Convert.ToInt32(dr["checkerid"]), status);
            this.Customer = new cls_customer(Convert.ToInt32(dr["customerid"]), status);
            this.Member = new cls_member(Convert.ToInt32(dr["memberid"]), status);
            this.Payments = new cls_payment(SyncId);

            this.productlist = new cls_productlist();
            this.productlist.set_productlist_by_wid(SyncId, status);

        }

        public void set_transaction_by_ornumber(long or_num)
        {
            //get SyncId by or number
            string sSQL = @"SELECT `SyncId` FROM `saleshead` WHERE `ornumber` = '" + or_num + @"'";
            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.SyncId = 0;
                return;
            }

            int syncid = Convert.ToInt32(dt.Rows[0]["SyncId"]);
            set_transaction_by_wid(syncid, true);
        }

        public decimal get_memberpoint_earn()
        {
            if (this.Member.getSyncId() == 0)
                return 0;
            decimal totalamt = this.TotalAmount - Payments.get_points();
            bool isNegative = totalamt < 0;
            totalamt = Math.Abs(totalamt);
            int rateid = this.Member.get_memberrate_id();


            decimal ratio = -1;
            DataTable dt = mySQLFunc.getdb(@"SELECT `ratio` FROM `memberratedetail`
                                            WHERE `headid` = " + rateid + @" AND `type` = 1
	                                            AND `amountfrom` <= " + totalamt + @"
	                                            AND `amountto` >= " + totalamt);
            if (dt.Rows.Count > 0)
                ratio = Convert.ToDecimal(dt.Rows[0]["ratio"]);

            if (ratio == -1)
            {
                dt = mySQLFunc.getdb(@"SELECT `ratio` FROM `memberratedetail`
                                            WHERE `headid` = " + rateid + @" AND `type` = 2
	                                            AND `datefrom` <= CAST(NOW() AS DATE)
	                                            AND `dateto` >= CAST(NOW() AS DATE)");
                if (dt.Rows.Count > 0)
                    ratio = Convert.ToDecimal(dt.Rows[0]["ratio"]);
            }

            if (ratio == -1)
            {
                dt = mySQLFunc.getdb(@"SELECT `ratio` FROM `memberratedetail`
                                            WHERE `headid` = " + rateid + @" AND `type` = 3
	                                            AND `amountfrom` <= " + totalamt + @"
	                                            AND `amountto` >= " + totalamt + @"
	                                            AND `datefrom` <= CAST(NOW() AS DATE)
	                                            AND `dateto` >= CAST(NOW() AS DATE)");
                if (dt.Rows.Count > 0)
                    ratio = Convert.ToDecimal(dt.Rows[0]["ratio"]);
            }

            if (ratio == -1)
            {
                dt = mySQLFunc.getdb(@"SELECT `ratio` FROM `memberratedetail`
                                            WHERE `headid` = " + rateid + @" AND `type` = 0");
                if (dt.Rows.Count > 0)
                    ratio = Convert.ToDecimal(dt.Rows[0]["ratio"]);
            }

            if (ratio == -1)
                return 0;

            if (ratio == 0)
                return 0;
            if (isNegative)
                totalamt *= -1;
            return totalamt / ratio;
        }

        public long get_UserAuthorizer_SyncId()
        {
            long permissiongiverwid = this.UserAuthorizer.getsyncid();
            long clerkwid = this.Cashier.getsyncid();
            return (permissiongiverwid != 0) ? permissiongiverwid : clerkwid;
        }

        public string get_permissiongiver_fullname()
        {
            string permissiongiverfullname = this.UserAuthorizer.getfullname();
            string clerkfullname = this.Cashier.getfullname();

            return (permissiongiverfullname != "") ? permissiongiverfullname : clerkfullname;
        }

        public Decimal TotalOriginalPrice { get { return productlist.list_product.Sum(Q => Q.OriginalPrice * Q.Quantity); } }
        public Decimal TotalQuantity { get { return productlist.list_product.Sum(Q => Q.Quantity); } }
        public Decimal TotalAmount { get { return productlist.list_product.Sum(Q => Q.Amount); } }
        public Decimal TotalVat { get { return productlist.list_product.Sum(V => V.Quantity * V.Vat); } }

        public bool HasItemDiscounts { get { return productlist.list_product.Where(P => P.HasItemDiscounts).Any(); } }
    }
}
