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
        private int wid;
        private string ORnumber;
        private string transactionno;
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
            this.wid = 0;
            this.ORnumber = "";
            this.transactionno = "";
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

        public void setWid(int wid_d) { this.wid = wid_d; }
        public int getWid() { return this.wid; }

        public void setORnumber(string ORnumber_d) { this.ORnumber = ORnumber_d; }
        public string getORnumber() { return this.ORnumber; }

        public void settransactionno(string transactionno_d) { this.transactionno = transactionno_d; }
        public string gettransactionno() { return this.transactionno; }

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

        public void set_transaction_by_wid(int wid, bool is_history)
        {
            this.wid = wid;

            string sSQL = "SELECT * FROM `saleshead` WHERE `wid` = " + wid;
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.wid = 0;
                return;
            }

            DataRow dr = dt.Rows[0];
            this.ORnumber = dr["ornumber"].ToString();
            this.transactionno = dr["transactionno"].ToString();
            this.adjust = Convert.ToDecimal(dr["adjust"]);
            this.discount = Convert.ToDecimal(dr["discount1"]);
            this.salesdatetime = DateTime.Parse(dr["date"].ToString());
            this.senior = new cls_senior();
            this.senior.set_senior(dr["seniorno"].ToString(), dr["seniorname"].ToString());
            this.memo = dr["memo"].ToString();
            Int32.TryParse(dr["show"].ToString(), out this.show);
            Int32.TryParse(dr["status"].ToString(), out this.status);

            this.clerk = new cls_user(Convert.ToInt32(dr["userid"]), is_history);
            this.checker = new cls_user(Convert.ToInt32(dr["checkerid"]), is_history);
            this.customer = new cls_customer(Convert.ToInt32(dr["customerid"]), is_history);
            this.member = new cls_member(Convert.ToInt32(dr["memberid"]), is_history);
            this.payments = new cls_payment(wid);

            this.productlist = new cls_productlist();
            this.productlist.set_productlist_by_wid(wid, is_history);

            //--------------------------------------------------------
            //get discounts

            string query = @"SELECT * FROM `salesheaddiscounts` WHERE salesheadwid = " + wid + " order by id";
            DataTable discs = mySQLFunc.getdb(query);
            if (discs.Rows.Count > 0)
            {
                int customdiscounttype = cls_globalvariables.dchead_customdiscounttype;
                int adjusttype = cls_globalvariables.dchead_adjusttype;
                int discounttype = cls_globalvariables.dchead_discounttype;
                int membertype = cls_globalvariables.dchead_membertype;
                int pospromotype = cls_globalvariables.dchead_pospromotype;

                foreach (DataRow d in discs.Rows)
                {
                    int discwid = Convert.ToInt32(d["discountwid"]);
                    int type = Convert.ToInt32(d["type"]);
                    int basis = Convert.ToInt32(d["basis"]);
                    decimal value = Convert.ToDecimal(d["value"]);
                    decimal amount = Convert.ToDecimal(d["amount"]);
                    bool ismultiple = Convert.ToBoolean(d["ismultiple"]);

                    if (type == adjusttype || type == discounttype)
                    {
                        this.productlist.getTransDisc().appendDiscount(type, value, amount, ismultiple, true);
                    }
                    else if (type == membertype)
                    {
                        this.productlist.getTransDisc().setMember(this.member, value, amount, true);
                    }
                    else if (type == pospromotype)
                    {
                        this.productlist.getTransDisc().setPosPromo(value, ismultiple, amount, true);
                    }
                    else if (type == customdiscounttype)
                    {
                        this.productlist.getTransDisc().activateDiscount(customdiscounttype, value, amount, true, discwid);
                    }
                    else
                    {
                        this.productlist.getTransDisc().activateDiscount(type, value, amount, ismultiple, true);
                    }
                }
            }

            //--------------------------------------------------------

            if (Convert.ToInt32(dr["customerid"]) > 0 && this.productlist.get_totalamount() > this.payments.get_totalamount())
            {
                this.payments.set_dept(this.productlist.get_totalamount() - this.payments.get_totalamount());
            }

        }

        public void set_transaction_by_ornumber(string or_num)
        {
            //get wid by or number
            string sSQL = @"SELECT `wid` FROM `saleshead`
                            WHERE `ornumber` = '" + or_num.Replace("'", "''") + @"'
                            ORDER BY `id` DESC LIMIT 1";
            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.wid = 0;
                return;
            }

            int wid_d = Convert.ToInt32(dt.Rows[0]["wid"]);
            set_transaction_by_wid(wid_d, true);
        }

        public decimal get_memberpoint_earn()
        {
            if (this.member.getwid() == 0)
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

        public int get_permissiongiver_wid()
        {
            int permissiongiverwid = this.get_permissiongiver().getwid();
            int clerkwid = this.getclerk().getwid();
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
