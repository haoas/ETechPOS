using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace ETech.cls
{
    public class cls_member
    {
        //attributes
        private string fullname;
        private string mobile;
        private DateTime birthdate;
        private int wid;
        private string cardid;
        private decimal points;
        private int rateid;
        private string ratename;
        private string barcode;
        private string address;

        private bool memberButOffline = false;
        public bool MemberButOffline
        {
            get { return memberButOffline; }
        }
        private int memberrateid;
        private List<cls_memberdiscount> memberdisc_list;

        public void init()
        {
            this.fullname = "";
            this.mobile = "";
            this.birthdate = new DateTime();
            this.wid = 0;
            this.cardid = "";
            this.points = 0;
            this.rateid = 0;
            this.ratename = "";
            this.memberrateid = 0;
            this.memberdisc_list = new List<cls_memberdiscount>();
        }

        //constructor
        public cls_member()
        {
            init();
        }

        public cls_member(int wid_d)
        {
            init();
            setcls_member_by_wid(wid_d);
        }

        public cls_member(int wid_d, bool is_history)
        {
            init();
            setcls_member_by_wid(wid_d, is_history);
        }

        public cls_member(string fullname_d, int wid_d, string cardid_d, decimal points_d)
        {
            this.fullname = fullname_d;
            this.wid = wid_d;
            this.cardid = cardid_d;
            this.points = points_d;
        }

        public string getfullname() { return this.fullname; }
        public string getmobile() { return this.mobile; }
        public string getbirhtdate() { return this.birthdate.ToString("yyyy-MM-dd"); }
        public int getwid() { return this.wid; }
        public string getbarcode() { return this.barcode; }
        public string getaddress() { return this.address; }

        public void setcardid(string cardid_d) { this.cardid = cardid_d; }
        public string getcardid() { return this.cardid; }

        public void setpoints(decimal points_d) { this.points = points_d; }
        public decimal getPreviousPoints() { return this.points; }

        public cls_member ShallowCopy()
        {
            return (cls_member)this.MemberwiseClone();
        }

        public void setcls_member_by_cardid(string cardid)
        {
            cardid = MySqlHelper.EscapeString(cardid);
            string sSQL = @"SELECT `wid`, `memberrateid` FROM `member` AS M 
                            WHERE (`cardid` = '" + cardid + @"'
                                OR `barcode` = '" + cardid + @"' ) 
                                AND M.`show` = 1 AND M.`status` = 1";
            DataTable dt = mySQLFunc.getdb_main(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.wid = 0;
                return;
            }

            int wid_d = Convert.ToInt32(dt.Rows[0]["wid"]);
            setcls_member_by_wid(wid_d);

            //int memberrateid_d = Convert.ToInt32(dt.Rows[0]["memberrateid"]);
            //setcls_memberrate_by_wid(memberrateid_d);
        }

        public void setcls_member_by_wid(int wid, bool is_history)
        {
            this.wid = wid;

            string sSQL = @"SELECT COALESCE(M.`wid`, 0) AS 'wid', 
	                            COALESCE(M.`fullname`, '') AS 'fullname', 
                                COALESCE(M.`mobile`, '') AS 'mobile',
                                COALESCE(M.`birthdate`, '') AS 'birthdate',
	                            COALESCE(M.`cardid`, 0) AS 'cardid', 
	                            COALESCE(M.`memberrateid`, 0) AS 'memberrateid', 
                                COALESCE(M.`barcode`, 0) AS 'barcode',
                                COALESCE(M.`address`, 0) AS 'address',
	                            COALESCE(R.`name`, '') AS 'ratename', 
	                            COALESCE(SUM(
		                            CASE
			                            WHEN T.`type` = 1 THEN T.`amount`
			                            WHEN T.`type` = 2 THEN -1 * T.`amount`
			                            WHEN T.`type` = 3 THEN -1 * T.`amount`
		                            END),0) AS 'points'
                            FROM `member` AS M
                            LEFT JOIN `memberratehead` AS R
	                            ON M.`memberrateid` = R.`wid` 
                            LEFT JOIN `memberpointtrans` AS T
	                            ON T.`memberid` = M.`wid` AND T.`show` = 1 AND T.`status` = 1
                            WHERE R.`show` = 1 AND R.`status` = 1 
	                            AND M.`wid` = " + wid;

            if (!is_history)
            {
                sSQL += " AND M.`show` = 1 AND M.`status` = 1 ";
            }
            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb_main(sSQL);
            if (dt.Rows.Count <= 0)
            {
                memberButOffline = (wid != 0);
                this.wid = 0;
                return;
            }

            DataRow dr = dt.Rows[0];
            this.fullname = dr["fullname"].ToString();
            this.mobile = dr["mobile"].ToString();
            DateTime.TryParse(dr["birthdate"].ToString(), out this.birthdate);
            this.cardid = dr["cardid"].ToString();
            this.rateid = Convert.ToInt32(dr["memberrateid"].ToString());
            this.barcode = dr["barcode"].ToString();
            this.address = dr["address"].ToString();
            this.ratename = dr["ratename"].ToString();
            this.points = Convert.ToDecimal(dr["points"]);
            setcls_memberrate_by_wid(this.rateid);
        }

        public void setcls_memberrate_by_wid(int memberrateid_d)
        {
            string startdate = cls_globalvariables.companystartdate.ToString("yyyy-MM-dd");
            string enddate = cls_globalvariables.companymaxdate.ToString("yyyy-MM-dd");

            try
            {
                this.memberrateid = memberrateid_d;

                string sql = @"SELECT `amountfrom` , `amountto`, `percent`, `ismultipleonly`,
                                            IF (DATE(`datefrom`) = '0000-00-00','" + startdate + @"',`datefrom`) as `datefrom`,
                                            IF (DATE(`datefrom`) = '0000-00-00','" + enddate + @"',`dateto`) as `dateto`
                                            FROM memberratedetail as D, memberratehead as H
                                            WHERE D.`headid` = '" + memberrateid_d + @"' AND D.`headid` = H.`wid`
	                                            AND (DATE(`datefrom`) <= DATE(NOW()) OR DATE(`datefrom`) = '0000-00-00' )
                                                AND (DATE(`dateto`) >= DATE(NOW()) OR DATE(`dateto`) = '0000-00-00' )
	                                            AND `percent` > 0 
	                                            AND H.`status` = 1 and H.`show` = 1";
                DataTable dt = mySQLFunc.getdb_main(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    decimal amtfrom = Convert.ToDecimal(dr["amountfrom"]);
                    decimal amtto = Convert.ToDecimal(dr["amountto"]);
                    decimal percent_d = Convert.ToDecimal(dr["percent"]);
                    bool ismultiple = (Convert.ToInt32(dr["ismultipleonly"]) == 1);
                    DateTime datefrom = Convert.ToDateTime(dr["datefrom"]);
                    DateTime dateto = Convert.ToDateTime(dr["dateto"]);
                    //this.memberdisc_list.Add(new cls_memberdiscount(amtfrom, amtto, percent_d, ismultiple));
                   // DateTime datefrom = new DateTime(0001, 1, 1, 0, 0, 0);
                   // DateTime dateto = new DateTime(3000, 1, 1, 0, 0, 0);

                   // if (dt.Columns["datefrom"].DataType == typeof(DateTime)) datefrom = Convert.ToDateTime(dr["datefrom"]);
                   // if (dt.Columns["datefrom"].DataType == typeof(MySqlDateTime))
                   // {
                   //     try { datefrom = ((MySqlDateTime)dr["datefrom"]).GetDateTime(); }
                   //     catch (Exception) { datefrom = Convert.ToDateTime("01/01/0001 00:00:00"); }
                   // }
                   // else datefrom = Convert.ToDateTime(dr["datefrom"]);

                   //if (dt.Columns["dateto"].DataType == typeof(DateTime)) dateto = Convert.ToDateTime(dr["dateto"]);
                   //if (dt.Columns["dateto"].DataType == typeof(MySqlDateTime))
                   // {
                   //     try { dateto = ((MySqlDateTime)dr["dateto"]).GetDateTime(); }
                   //     catch (Exception) { dateto = Convert.ToDateTime("01/01/3000 00:00:00"); }
                   // }
                   // else dateto = Convert.ToDateTime(dr["dateto"]);
                    this.memberdisc_list.Add(new cls_memberdiscount(amtfrom, amtto, percent_d, ismultiple, datefrom, dateto));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public decimal get_member_discount_amount(decimal totalamt)
        {
            decimal total_disc = 0;
            decimal default_percent = 0;
            foreach (cls_memberdiscount memdisc in this.memberdisc_list)
            {
                total_disc = memdisc.compute_for_discount(totalamt);
                if (memdisc.getAmtFrom() == 0 && memdisc.getAmtTo() == 0)
                {
                    default_percent = memdisc.getPercent() / 100 * totalamt;
                }
                if (totalamt >= memdisc.getAmtFrom() && totalamt <= memdisc.getAmtTo() && total_disc > 0)
                {
                    return total_disc * totalamt;
                }
            }
            return default_percent;
        }

        public void setcls_member_by_wid(int wid_d)
        {
            setcls_member_by_wid(wid_d, false);
        }

        public string get_memberrate_name()
        {
            return this.ratename;
        }

        public int get_memberrate_id()
        {
            return this.rateid;
        }
        public List<cls_memberdiscount> getMemberDiscList() { return this.memberdisc_list; } 
    }
}
