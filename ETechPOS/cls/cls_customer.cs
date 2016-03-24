using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_customer
    {
        //attributes
        private string code;
        private string fullname;
        private long syncid;
        private int pricingtype = 1;
        private decimal pricingrate = 0;
        private string memo;
        private Dictionary<long, decimal> itemPriceDictionary;

        public void init()
        {
            this.code = "";
            this.fullname = "";
            this.syncid = 0;
            this.pricingtype = 1;
            this.pricingrate = 0;
            this.memo = "";
            this.itemPriceDictionary = new Dictionary<long, decimal>();
        }

        //constructor
        public cls_customer()
        {
            init();
        }

        public cls_customer(long syncid_d)
        {
            init();
            setcls_customer_by_wid(syncid_d);
        }

        public cls_customer(int wid_d, bool is_history)
        {
            init();
            setcls_customer_by_wid(wid_d, is_history);
        }

        public cls_customer(string code_d, string fullname_d, int wid_d)
        {
            this.code = code_d;
            this.fullname = fullname_d;
            this.syncid = wid_d;
        }

        public string getCode() { return this.code; }

        public string getfullname()
        {
            return this.fullname;
        }

        public long getwid()
        {
            return this.syncid;
        }

        public int getPricingType()
        {
            return this.pricingtype;
        }

        public decimal getPricingRate()
        {
            return this.pricingrate;
        }

        public string getMemo()
        {
            return this.memo;
        }

        public Dictionary<long, decimal> getItemPriceDictionary()
        {
            return itemPriceDictionary;
        }

        public void setcls_customer_by_wid(long SyncId, bool is_history)
        {
            this.syncid = SyncId;
            string sSQL =
            @"SELECT COALESCE(`customercode`,'') as customercode,
                     COALESCE(`fullname`,'') as fullname,
                     COALESCE(`pricingtype`,'1') as pricingtype ,
                     COALESCE(`pricingrate`,'0') as pricingrate ,
                     COALESCE(`memo`,'') as memo 
            FROM `customer` WHERE `SyncId` = " + SyncId;

            if (!is_history)
            {
                sSQL += " AND `show` = 1 AND `status` = 1 ";
            }

            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.syncid = 0;
                return;
            }
            DataRow dr = dt.Rows[0];
            this.code = dr["customercode"].ToString();
            this.fullname = dr["fullname"].ToString();
            this.pricingtype = Convert.ToInt32(dr["pricingtype"].ToString());
            this.pricingrate = Convert.ToDecimal(dr["pricingrate"].ToString());
            this.memo = dr["memo"].ToString();

            // Historical Pricing
            if (pricingtype == 0)
            {
                sSQL = @"
                SELECT H.`SyncId`
                     FROM `saleshead` AS H
                     WHERE H.`status` = 1
                      AND H.`branchid` = " + cls_globalvariables.BranchCode + @" AND H.`customerid` = " + SyncId;
                DataTable tempDataTable = mySQLFunc.getdb(sSQL);
                string groupConcat = "";
                foreach (DataRow row in tempDataTable.Rows)
                    groupConcat += ", " + row["SyncId"].ToString();

                if (groupConcat != "")
                {
                    sSQL = @"
                    SELECT * FROM
                    (
                        SELECT `productid` AS 'pwid', `price`
                        FROM 
                            salesdetail
                        WHERE `headid` IN( " + groupConcat.Substring(1) + @")
                        ORDER BY `SyncId` DESC
                    )A
                    GROUP BY `pwid`";
                    tempDataTable = mySQLFunc.getdb(sSQL);
                    long itemWid = 0;
                    decimal price = 0;
                    foreach (DataRow row in tempDataTable.Rows)
                    {
                        itemWid = long.TryParse(row["pwid"].ToString(), out itemWid) ? itemWid : 0;
                        price = decimal.TryParse(row["price"].ToString(), out price) ? price : 0;
                        this.itemPriceDictionary.Add(itemWid, price);
                    }
                }
            }
        }

        public void setcls_customer_by_wid(long SyncId)
        {
            setcls_customer_by_wid(SyncId, false);
            
        }
    }
}
