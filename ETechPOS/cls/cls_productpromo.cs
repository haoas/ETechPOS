using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.Types;
using System.Windows.Forms;

namespace ETech.cls
{
    public class cls_productpromo
    {
        private List<cls_qtypromo> qtypromolist;
        private List<cls_promoproddc> promoProdDiscountList;

        public cls_productpromo()
        {
            qtypromolist = new List<cls_qtypromo>();
            promoProdDiscountList = new List<cls_promoproddc>();
        }
        public void set_PromoProdDiscount(int productWid)
        {
            set_PromoProdDiscount(productWid, 0);
        }
        public void set_PromoProdDiscount(int productWid, int salesDetailWid)
        {
            promoProdDiscountList = new List<cls_promoproddc>();
            string startdate = cls_globalvariables.companystartdate.ToString("yyyy-MM-dd HH:mm:ss");
            string enddate = cls_globalvariables.companymaxdate.ToString("yyyy-MM-dd HH:mm:ss");
            string dateBasis = "NOW()";
            if(salesDetailWid != 0)
            {
                string sql = @"
                    SELECT
                        SH.`date` AS 'date'
                    FROM
                        `saleshead` AS SH
                    LEFT JOIN
                        `salesdetail` AS SD
                        ON SH.`wid` = SD.`headid`
                    WHERE
                        SD.`wid` = " + salesDetailWid;
                try
                {
                    dateBasis = "'" + DateTime.Parse(mySQLFunc.getdb(sql).Rows[0]["date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                catch
                {
                    salesDetailWid = 0;
                }
            }
            string sSQLpromoProdDiscount =
                @"SELECT `oprice`,`discount`,`adjust`,
                    IF (`datefrom` = '0000-00-00 00:00:00','" + startdate + @"',`datefrom`) as `datefrom`,
                    IF (`datefrom` = '0000-00-00 00:00:00','" + enddate + @"',`dateto`) as `dateto`,
                    `show`, `lastmodifieddate`
                    FROM `productdiscount`
                    WHERE `productid` = " + productWid + @" 
                        AND (`datefrom` <= " + dateBasis + @" OR `datefrom` = '0000-00-00 00:00:00' )
                        AND (`dateto` >= " + dateBasis + @" OR `dateto` = '0000-00-00 00:00:00' )
                        AND `tobranchid` = " + cls_globalvariables.branchid_v + (salesDetailWid == 0 ? @" 
                        AND `show` = 1" : "") + @"
                    ORDER BY `datefrom`, `dateto`, `show` DESC";
            try
            {
                DataTable dtpromoproddc = mySQLFunc.getdb(sSQLpromoProdDiscount);
                if (dtpromoproddc.Rows.Count <= 0)
                    return;
                DateTime dateTimeBasis = new DateTime();
                if (dateBasis == "NOW()")
                    dateTimeBasis = DateTime.Now;
                else
                    DateTime.TryParse(dateBasis.Substring(1,19), out dateTimeBasis);
                foreach (DataRow drpromoproddc in dtpromoproddc.Rows)
                {
                    // skips adding those that are deleted before transaction being reprinted occured
                    DateTime lastmodifieddate = new DateTime();
                    DateTime.TryParse(drpromoproddc["lastmodifieddate"].ToString(), out lastmodifieddate);
                    int show;
                    int.TryParse(drpromoproddc["show"].ToString(), out show);
                    if (show == 0 && lastmodifieddate < dateTimeBasis)
                        continue;

                    this.promoProdDiscountList.Add(new cls_promoproddc(
                    Convert.ToDecimal(drpromoproddc["oprice"]),
                    Convert.ToDecimal(drpromoproddc["discount"]),
                    Convert.ToDecimal(drpromoproddc["adjust"]),
                    Convert.ToDateTime(drpromoproddc["datefrom"]),
                    Convert.ToDateTime(drpromoproddc["dateto"]),
                    dateTimeBasis)
                    );
                }
            }
            catch (Exception){
                cls_globalfunc.MSGBXLOG("EXCEPTION in set_PromoProdDiscount()!");
            } 
        }

        public decimal get_PromoProdDiscount_price()
        {
            if (promoProdDiscountList.Count <= 0)
                return -1;

            decimal price_ret = -1;

            foreach (cls_promoproddc prproddc in this.promoProdDiscountList)
            {
                price_ret = prproddc.get_discountedprice();
            }

            return price_ret;
        }

        public void set_qtypromo(int pwid)
        {
            set_qtypromo(pwid, 0);
        }
        public void set_qtypromo(int pwid, int salesDetailWid)
        {
            DateTime dateBasis = new DateTime();
            if (salesDetailWid != 0)
            {
                string sql = @"
                        SELECT
                            SH.`date` AS 'date'
                        FROM
                            `saleshead` AS SH
                        LEFT JOIN
                            `salesdetail` AS SD
                            ON SH.`wid` = SD.`headid`
                        WHERE
                            SD.`wid` = " + salesDetailWid;
                try
                {
                    dateBasis = DateTime.Parse(mySQLFunc.getdb(sql).Rows[0]["date"].ToString());
                }
                catch
                {
                    salesDetailWid = 0;
                }
            }

            qtypromolist = new List<cls_qtypromo>();

            string sSQLqtypromo = @"
                        SELECT *
                        FROM `promoqty`
                        WHERE `productid` = " + pwid + @"
	                        AND `forbranchid` = " + cls_globalvariables.branchid_v + ((salesDetailWid == 0) ? @" 
	                        AND `show` = 1" : "");
            try
            {
                DataTable dtpromo = mySQLFunc.getdb(sSQLqtypromo);
                if (dtpromo.Rows.Count <= 0)
                    return;

                if (salesDetailWid != 0)
                {
                    DateTime lastModified = new DateTime();
                    DateTime dateCreated = new DateTime();
                    int show = 0;
                    for (int i = 0; i < dtpromo.Rows.Count; i++)
                    {
                        DataRow row = dtpromo.Rows[i];
                        lastModified = DateTime.TryParse(row["lastmodifieddate"].ToString(), out lastModified) ? lastModified : new DateTime();
                        dateCreated = DateTime.TryParse(row["datecreated"].ToString(), out dateCreated) ? dateCreated : new DateTime();
                        show = int.TryParse(row["show"].ToString(), out show) ? show : 0;

                        // Remove those that are not available during dateBasis
                        if (dateCreated > dateBasis || (show == 0 && dateBasis > lastModified))
                        {
                            dtpromo.Rows.Remove(row);
                            i--;
                            continue;
                        }
                    }
                }

                foreach (DataRow drpromo in dtpromo.Rows)
                {
                    this.qtypromolist.Add(new cls_qtypromo(
                        Convert.ToDecimal(drpromo["qty"]),
                        Convert.ToDecimal(drpromo["price"])));
                }
            }
            catch { }
        }

        public decimal get_qtypromo_price(decimal qty)
        {
            if (qtypromolist.Count <= 0)
                return -1;

            decimal price_ret = -1;
            foreach (cls_qtypromo qtypr in this.qtypromolist)
            {
                if (qtypr.get_qty() <= qty)
                {
                    price_ret = qtypr.get_price();
                }
            }

            return price_ret;
        }
    }
}
