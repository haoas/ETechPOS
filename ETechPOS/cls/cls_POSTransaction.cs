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
        private long syncid;
        private long OfficialReceiptNo;
        private DateTime salesdatetime;
        private cls_user clerk;
        private cls_user checker;
        private cls_user salesman;
        private cls_customer customer;
        private cls_member member;
        private cls_payment payments;
        private cls_senior senior;
        private cls_nonvat nonvat;
        private string memo;
        private int show;
        private int status;
        private cls_user permissiongiver;

        private cls_productlist productlist;

        private decimal adjust;
        private decimal discount;

        //constructor
        public cls_POSTransaction()
        {
            this.syncid = 0;
            this.OfficialReceiptNo = 0;
            this.adjust = 0;
            this.discount = 0;
            this.salesdatetime = DateTime.Now;
            this.productlist = new cls_productlist();
            this.clerk = new cls_user();
            this.checker = new cls_user();
            this.salesman = new cls_user();
            this.customer = new cls_customer();
            this.member = new cls_member();
            this.payments = new cls_payment();
            this.senior = new cls_senior();
            this.nonvat = new cls_nonvat();
            this.memo = "";
            this.permissiongiver = new cls_user();
            this.show = 0;
            this.status = 0;
        }

        public void setSyncId(long syncid_d) { this.syncid = syncid_d; }
        public long getSyncId() { return this.syncid; }

        public void setORnumber(long ORnumber_d) { this.OfficialReceiptNo = ORnumber_d; }
        public long getORnumber() { return this.OfficialReceiptNo; }

        public void setmemo(string memo_d) { this.memo = memo_d; }
        public string getmemo() { return this.memo; }

        public void setadjust(decimal adjust_d) { this.adjust = adjust_d; }
        public decimal getadjust() { return this.adjust; }

        public void setdatetime(DateTime dt) { this.salesdatetime = dt; }
        public DateTime getdatetime() { return this.salesdatetime; }

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

        public void setdiscount(decimal discount_d) { this.discount = discount_d; }
        public decimal getdiscount() { return this.discount; }

        public void setclerk(cls_user clerk_d) { this.clerk = clerk_d; }
        public cls_user getclerk() { return this.clerk; }

        public void setchecker(cls_user checker_d) { this.checker = checker_d; }
        public cls_user getchecker() { return this.checker; }

        public void setsalesman(cls_user salesman_d) { this.salesman = salesman_d; }
        public cls_user getsalesman() { return this.salesman; }

        public void setcustomer(cls_customer customer_d) { this.customer = customer_d; this.productlist.set_customer(customer_d); }
        public cls_customer getcustomer() { return this.customer; }

        public void setmember(cls_member member_d) { this.member = member_d; }
        public cls_member getmember() { return this.member; }

        public void setpayments(cls_payment payments_d) { this.payments = payments_d; }
        public cls_payment getpayments() { return this.payments; }

        public cls_productlist get_productlist() { return this.productlist; }

        public int getShow() { return this.show; }
        public int getStatus() { return this.status; }

        //get changeamount
        public decimal get_changeamount()
        {
            decimal totalamt = Math.Round(this.productlist.get_totalamount(), 2, MidpointRounding.AwayFromZero);
            decimal totalpaymentamt = this.payments.get_totalamount();
            decimal change = totalpaymentamt - totalamt;
            return (change > 0) ? change : 0;
        }

        public void set_transaction_by_wid(long SyncId, bool status)
        {
            this.syncid = SyncId;

            string sSQL = "SELECT * FROM `saleshead` WHERE `SyncId` = " + SyncId;
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.syncid = 0;
                return;
            }

            DataRow dr = dt.Rows[0];
            this.OfficialReceiptNo = long.Parse(dr["ornumber"].ToString());
            this.adjust = Convert.ToDecimal(dr["adjust"]);
            this.discount = Convert.ToDecimal(dr["discount1"]);
            this.salesdatetime = DateTime.Parse(dr["date"].ToString());
            this.senior = new cls_senior();
            this.senior.set_senior(dr["seniorno"].ToString(), dr["seniorname"].ToString());
            this.memo = dr["memo"].ToString();
            Int32.TryParse(dr["show"].ToString(), out this.show);
            Int32.TryParse(dr["status"].ToString(), out this.status);

            this.clerk = new cls_user(Convert.ToInt32(dr["userid"]), status);
            this.checker = new cls_user(Convert.ToInt32(dr["checkerid"]), status);
            this.customer = new cls_customer(Convert.ToInt32(dr["customerid"]), status);
            this.member = new cls_member(Convert.ToInt32(dr["memberid"]), status);
            this.payments = new cls_payment(SyncId);

            this.productlist = new cls_productlist();
            this.productlist.set_productlist_by_wid(SyncId, status);

            if (Convert.ToInt32(dr["customerid"]) > 0 && this.productlist.get_totalamount() > this.payments.get_totalamount())
            {
                this.payments.set_dept(this.productlist.get_totalamount() - this.payments.get_totalamount());
            }

        }

        public void set_transaction_by_ornumber(long or_num)
        {
            //get SyncId by or number
            string sSQL = @"SELECT `SyncId` FROM `saleshead` WHERE `ornumber` = '" + or_num + @"'";
            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.syncid = 0;
                return;
            }

            int wid_d = Convert.ToInt32(dt.Rows[0]["SyncId"]);
            set_transaction_by_wid(wid_d, true);
        }

        public decimal get_memberpoint_earn()
        {
            if (this.member.getSyncId() == 0)
                return 0;
            decimal totalamt = this.get_productlist().get_totalamount() - payments.get_points();
            bool isNegative = totalamt < 0;
            totalamt = Math.Abs(totalamt);
            int rateid = this.member.get_memberrate_id();


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

        public cls_user get_permissiongiver()
        {
            return this.permissiongiver;
        }
        public void set_permissiongiver(cls_user permissiongiver_d)
        {
            this.permissiongiver = permissiongiver_d;
        }

        public long get_permissiongiver_wid()
        {
            long permissiongiverwid = this.get_permissiongiver().getsyncid();
            long clerkwid = this.getclerk().getsyncid();
            return (permissiongiverwid != 0) ? permissiongiverwid : clerkwid;
        }

        public string get_permissiongiver_fullname()
        {
            string permissiongiverfullname = this.get_permissiongiver().getfullname();
            string clerkfullname = this.getclerk().getfullname();

            return (permissiongiverfullname != "") ? permissiongiverfullname : clerkfullname;
        }

    }
}
