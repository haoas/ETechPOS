using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_product
    {
        private long syncid;
        private string name;

        private RoundedDecimal price;
        private decimal origprice;
        private cls_user soldby;
        private decimal adjust;
        private decimal discount;

        private bool isvat;
        private int issenior;

        private decimal wholesaleprice;

        public cls_discountlist productdiscount;

        private bool is_history;

        /*
        nonvat_sale
        nonvat_return
        vatable_sale
        vatable_return
        senior_sale
        senior_return
        */
        private string transaction_mode;

        public decimal Quantity { get; set; }
        public decimal pprice { get; set; }
        public string barcode { get; set; }
        public string memo { get; set; }
        public decimal amount { get { return price * Quantity; } }
        public decimal vat
        {
            get
            {
                if (!isvat)
                    return 0;
                else
                    return (price * cls_globalvariables.vat / (1 + cls_globalvariables.vat));
            }
        }

        public void init()
        {
            this.syncid = 0;
            this.barcode = "";
            this.name = "";
            this.Quantity = 1;
            this.price = 0;
            this.wholesaleprice = 0;
            this.memo = "";

            this.price = 0;

            this.pprice = 0;
            this.origprice = 0;
            this.soldby = new cls_user();
            this.isvat = true;
            this.issenior = 0;
            this.adjust = 0;
            this.discount = 0;
            this.transaction_mode = "vatable_sale";

            this.productdiscount = new cls_discountlist(1);

            this.is_history = false;
        }

        //constructor
        public cls_product()
        {
            this.init();
        }

        public cls_product(long syncid_d, bool is_history)
        {
            this.init();
            setcls_product_by_syncid(syncid_d, is_history);
        }

        public cls_product(string barcode_d)
        {
            this.init();

            barcode_d = barcode_d.Trim().Replace("'", "''");
            string sSQL = @"SELECT `SyncId` FROM `product` 
                            WHERE `show` = 1 AND `status` = 1 
                                AND ( `SyncId` = @barcode_d OR `barcode` = @barcode_d ) ";
            DataTable dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d" }),
                                new List<string>(new string[] { barcode_d }));

            if (dt.Rows.Count > 0)
            {
                int syncid = Convert.ToInt32(dt.Rows[0]["SyncId"]);
                this.setcls_product_by_wid(syncid);
                return;
            }
        }

        public cls_product(long syncid_d)
        {
            this.init();
            this.setcls_product_by_wid(syncid_d);
        }

        public cls_product(string barcode_d, decimal price_d, decimal qty_d)
        {
            //Required Connection to SQL
            this.init();

            this.barcode = barcode_d;
            this.price = price_d;
            this.Quantity = qty_d;

            string sSQL = "";
            sSQL = @"SELECT Count(0),
                    COALESCE(P.`SyncId`,0) AS 'pwid',
                    COALESCE(P.`product`,'OPENITEM') AS 'productname', 
                    COALESCE(B.`purchaseprice`, 0) AS 'pprice',
                    COALESCE(B.`sellingprice`, " + price_d + @") AS 'oprice'
                    FROM `product` AS P
                    LEFT JOIN `branchprice` AS B ON B.`productid`=P.`SyncId` AND
                        B.`branchid` = " + cls_globalvariables.BranchCode + @" 
                    WHERE (P.`barcode` = '" + barcode_d + @"')";

            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            DataRow dr = dt.Rows[0];

            this.syncid = Convert.ToInt32(dr["pwid"]);
            this.origprice = Convert.ToDecimal(dr["oprice"]);
            this.pprice = Convert.ToDecimal(dr["pprice"]);
        }

        public cls_product(decimal price_d, long syncid_d, decimal qty_d)
        {
            this.init();

            this.barcode = "-";

            this.price = price_d;
            this.origprice = price_d;
            this.price = price_d;
            this.wholesaleprice = price_d;
            string sql = @"
                SELECT `product` 
                FROM `product`
                WHERE `SyncId` = " + syncid;
            DataTable table = mySQLFunc.getdb(sql);
            if (table != null && table.Rows.Count > 0)
                this.name = table.Rows[0]["product"].ToString();
            this.syncid = syncid_d;
            if (this.syncid == 0)
            {
                this.Quantity = qty_d;
                this.isvat = true;
            }
            else if (this.syncid == 1)
            {
                this.name = "Service Charge: " + cls_globalvariables.ServiceCharge_v + "%";
                this.isvat = false;
                this.Quantity = 1;
                this.isvat = false;
                this.transaction_mode = "nonvat_sale";
            }
            else if (this.syncid == 2)
            {
                this.name = "Local Tax: " + cls_globalvariables.LocalTax_v + "%";
                this.isvat = false;
                this.Quantity = 1;
                this.transaction_mode = "nonvat_sale";
            }

        }

        public void setcls_product_by_wid(long syncid_d)
        {
            setcls_product_by_syncid(syncid_d, false);
        }

        public void setcls_product_by_syncid(long syncid_d, bool is_history)
        {
            string sSQL = "";
            sSQL = @"SELECT P.`SyncId` AS 'pwid', P.`product` AS 'productname', 
                            P.`barcode`, P.`isvat`, P.`senior`,
                            COALESCE(B.`sellingprice`, 0) AS 'price', 
                            COALESCE(B.`wholesaleprice`, 0) AS 'wholesaleprice',
                            COALESCE(B.`purchaseprice`, 0) AS 'pprice'
                        FROM `product` AS P
                        LEFT JOIN `branchprice` AS B ON B.`productid` = P.`SyncId` 
                            AND B.`branchid` = " + cls_globalvariables.BranchCode + @" 
                        WHERE P.`SyncId` = " + syncid_d;

            if (cls_globalvariables.allowZeroPrice_v.ToString() != "1")
            {
                sSQL += " AND B.`sellingprice` > 0 AND B.`wholesaleprice` > 0 ";
            }

            if (!is_history)
            {
                sSQL += " AND P.`show` = 1 AND P.`status` = 1";
            }
            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
                return;

            DataRow dr = dt.Rows[0];
            this.syncid = syncid_d;
            this.barcode = dr["barcode"].ToString();
            this.name = dr["productname"].ToString();
            this.pprice = Convert.ToDecimal(dr["pprice"]);
            this.price = Convert.ToDecimal(dr["price"]);

            this.isvat = (Convert.ToInt32(dr["isvat"]) == 1);
            this.issenior = Convert.ToInt32(dr["senior"]);

            this.origprice = this.price;
        }

        public void reset_data_by_mode(bool nonvattrans, bool issenior_d, bool iswholesale, int pricingtype, decimal pricingrate, cls_customer customer)
        {
            this.origprice = this.price;

            if (iswholesale && this.syncid != 0)
            {
                if (pricingtype == 0 && customer.getItemPriceDictionary().ContainsKey(syncid))
                {
                    this.origprice = wholesaleprice;
                    this.price = wholesaleprice;
                    getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_adjusttype, customer.getItemPriceDictionary()[syncid] - this.origprice, false);
                }
                else
                {
                    if (pricingtype == 2) this.origprice = Math.Round(this.pprice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                    else if (pricingtype == 3) this.origprice = Math.Round(this.wholesaleprice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                    else if (pricingtype == 9) this.origprice = this.price;
                    else this.origprice = this.wholesaleprice;
                    this.price = this.origprice;
                }
            }

            Console.WriteLine("reset_data_by_mode discount : " + this.discount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);
            Console.WriteLine("reset_data_by_mode 1: oprice: " + this.origprice);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior5);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_nonvat);
            if (!this.is_history)
                this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_promoqty);

            Console.WriteLine("reset_data_by_mode 2: oprice: " + this.origprice);
            if (issenior_d && (this.issenior == 1))
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_senior, 1 - cls_globalvariables.senior, true);
                if (this.isvat)
                    this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            if (issenior_d && (this.issenior == 2))
            {
                //Senior Transaction, Senior 5% item
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_senior5, 1 - cls_globalvariables.senior5, true);
            }
            else if (nonvattrans && this.isvat)
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.origprice);

            //detail discount
            decimal tmp_disc = this.productdiscount.get_discounts_percentage(this.origprice);
            /*-------------------------------------------------*/
            //ROBI

            Console.WriteLine("reset_data_by_mode price: " + this.price);
            Console.WriteLine("reset_data_by_mode discount : " + this.discount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);

            // COMPUTE PRICE
            //detaildiscount = distributed head discounts and detaildiscount
            //1st run set detail discounts
            //2nd run distribute head discounts
            this.discount = 1 - (1M - this.discount) * (1M - tmp_disc);
            setPrice((getPrice() + this.adjust) * (1 - this.discount));
            this.price = Math.Round(this.price, 2, MidpointRounding.AwayFromZero);

            Console.WriteLine("reset_data_by_mode dc: " + this.price);

            if (nonvattrans || (issenior_d && (this.issenior == 1)))
                this.isvat = false;

            if (this.Quantity > 0 && (!this.isvat || nonvattrans) && (!issenior_d || this.issenior != 1) || (this.syncid < 10 && this.syncid > 0))
                this.transaction_mode = "nonvat_sale";
            else if (this.Quantity < 0 && (!this.isvat || nonvattrans) && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "nonvat_return";
            else if (this.Quantity > 0 && this.isvat && !nonvattrans && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_sale";
            else if (this.Quantity < 0 && this.isvat && !nonvattrans && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_return";
            else if (this.Quantity > 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_sale";
            else if (this.Quantity < 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_return";
            else
                this.transaction_mode = "vatable_sale";
        }

        public void setProductName(string productname_d)
        {
            this.name = productname_d;
        }

        public void reprint_reset_data_by_mode(bool nonvattrans, bool issenior_d, bool iswholesale, int pricingtype)
        {
            this.origprice = this.price;

            Console.WriteLine("reset_data_by_mode discount : " + this.discount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);
            Console.WriteLine("reset_data_by_mode 1: oprice: " + this.origprice);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_nonvat);
            if (!this.is_history)
                this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_promoqty);

            Console.WriteLine("reset_data_by_mode 2: oprice: " + this.origprice);
            if (issenior_d && (this.issenior == 1))
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_senior, 1 - cls_globalvariables.senior, true);
                if (this.isvat)
                    this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            else if (nonvattrans && this.isvat)
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.origprice);

            Console.WriteLine("reset_data_by_mode price: " + this.price);
            Console.WriteLine("reset_data_by_mode discount : " + this.discount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);

            if (this.discount != 0)
                this.price = this.origprice * (1 - this.discount);
            else
                this.price = this.origprice + this.adjust;

            Console.WriteLine("reset_data_by_mode dc: " + this.price);

            if (nonvattrans || (issenior_d && (this.issenior == 1)))
                this.isvat = false;

            if (this.Quantity > 0 && (!this.isvat || nonvattrans) && (!issenior_d || this.issenior != 1) || (this.syncid < 1000 && this.syncid > 0))
                this.transaction_mode = "nonvat_sale";
            else if (this.Quantity < 0 && (!this.isvat || nonvattrans) && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "nonvat_return";
            else if (this.Quantity > 0 && this.isvat && !nonvattrans && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_sale";
            else if (this.Quantity < 0 && this.isvat && !nonvattrans && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_return";
            else if (this.Quantity > 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_sale";
            else if (this.Quantity < 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_return";
            else
                this.transaction_mode = "vatable_sale";
        }

        public void setRetailPrice(decimal val)
        {
            this.price = val;
        }

        public void setPrice(decimal price_d)
        {
            this.price = price_d;
        }

        public void setOrigPrice(decimal origprice_d)
        {
            this.origprice = origprice_d;
        }

        public void setSoldBy(cls_user soldby_d)
        {
            this.soldby = soldby_d;
        }

        public void setAdjust(decimal adjust_d) { this.adjust = adjust_d; }
        public void setDiscount(decimal discount_d) { this.discount = discount_d; }

        //get from attributes
        public long getSyncId() { return this.syncid; }

        public string getProductName()
        {
            return (this.name == "" ? this.barcode : this.name);
        }

        public decimal getPrice()
        {
            return Math.Round(this.price, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getOrigPrice()
        {
            return Math.Round(this.origprice, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getWholesalePrice()
        {
            return Math.Round(this.wholesaleprice, 2, MidpointRounding.AwayFromZero);
        }

        public cls_user getSoldBy()
        {
            return this.soldby;
        }

        public string getTransaction_Mode()
        {
            return this.transaction_mode;
        }

        public decimal getPurchasePrice() { return Math.Round(this.pprice, 2, MidpointRounding.AwayFromZero); }
        public decimal getAdjust() { return Math.Round(this.adjust, 2, MidpointRounding.AwayFromZero); }
        public decimal getDiscount() { return this.discount; }

        public int getIsSenior() { return this.issenior; }
        public bool getIsVat() { return this.isvat; }

        public cls_discountlist getProductDiscountList() { return this.productdiscount; }
        public decimal get_discount_amt(int type) { return this.productdiscount.get_all_discount_amount_of_type(type); }

        public string VatStatus
        {
            get
            {
                if (issenior == 1)
                    return "S - NV";
                else if (!isvat)
                    return "NV";
                else
                    return "V";
            }

        }
    }
}
