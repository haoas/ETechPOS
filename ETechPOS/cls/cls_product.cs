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
        private decimal adjust;

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

        public string Name { get; set; }
        public RoundedDecimal OriginalPrice { get; set; }
        public RoundedDecimal RegularDiscount { get; set; }
        public RoundedDecimal Price { get; set; }
        public cls_user SoldBy;

        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Barcode { get; set; }
        public string Memo { get; set; }
        public decimal Amount { get { return Price * Quantity; } }
        public decimal Vat
        {
            get
            {
                if (!isvat)
                    return 0;
                else
                    return (Price * cls_globalvariables.vat / (1 + cls_globalvariables.vat));
            }
        }

        public void init()
        {
            this.syncid = 0;
            this.Barcode = "";
            this.Name = "";
            this.Quantity = 1;
            this.Price = 0;
            this.wholesaleprice = 0;
            this.Memo = "";

            this.Price = 0;

            this.PurchasePrice = 0;
            this.OriginalPrice = 0;
            this.SoldBy = new cls_user();
            this.isvat = true;
            this.issenior = 0;
            this.adjust = 0;
            this.RegularDiscount = 0;
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

            this.Barcode = barcode_d;
            this.Price = price_d;
            this.Quantity = qty_d;

            string sSQL = "";
            sSQL = @"SELECT Count(0),
                    COALESCE(P.`SyncId`,0) AS 'pwid',
                    COALESCE(P.`product`,'OPENITEM') AS 'productname', 
                    COALESCE(B.`purchaseprice`, 0) AS 'pprice',
                    COALESCE(B.`sellingprice`, " + price_d + @") AS 'oprice'
                    FROM `product` AS P
                    LEFT JOIN `branchprice` AS B ON B.`productid`=P.`SyncId` AND
                        B.`branchid` = " + cls_globalvariables.Branch.Id + @"
                    WHERE (P.`barcode` = '" + barcode_d + @"')";

            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            DataRow dr = dt.Rows[0];

            this.syncid = Convert.ToInt32(dr["pwid"]);
            this.OriginalPrice = Convert.ToDecimal(dr["oprice"]);
            this.PurchasePrice = Convert.ToDecimal(dr["pprice"]);
        }

        public cls_product(decimal price_d, long syncid_d, decimal qty_d)
        {
            this.init();

            this.Barcode = "-";

            this.Price = price_d;
            this.OriginalPrice = price_d;
            this.Price = price_d;
            this.wholesaleprice = price_d;
            string sql = @"
                SELECT `product` 
                FROM `product`
                WHERE `SyncId` = " + syncid;
            DataTable table = mySQLFunc.getdb(sql);
            if (table != null && table.Rows.Count > 0)
                this.Name = table.Rows[0]["product"].ToString();
            this.syncid = syncid_d;
            if (this.syncid == 0)
            {
                this.Quantity = qty_d;
                this.isvat = true;
            }
            else if (this.syncid == 1)
            {
                this.Name = "Service Charge: " + cls_globalvariables.ServiceCharge_v + "%";
                this.isvat = false;
                this.Quantity = 1;
                this.isvat = false;
                this.transaction_mode = "nonvat_sale";
            }
            else if (this.syncid == 2)
            {
                this.Name = "Local Tax: " + cls_globalvariables.LocalTax_v + "%";
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
                            AND B.`branchid` = " + cls_globalvariables.Branch.Id + @"
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
            this.Barcode = dr["barcode"].ToString();
            this.Name = dr["productname"].ToString();
            this.PurchasePrice = Convert.ToDecimal(dr["pprice"]);
            this.Price = Convert.ToDecimal(dr["price"]);

            this.isvat = (Convert.ToInt32(dr["isvat"]) == 1);
            this.issenior = Convert.ToInt32(dr["senior"]);

            this.OriginalPrice = this.Price;
        }

        public void reset_data_by_mode(bool nonvattrans, bool issenior_d, bool iswholesale, int pricingtype, decimal pricingrate, cls_customer customer)
        {
            this.OriginalPrice = this.Price;

            if (iswholesale && this.syncid != 0)
            {
                if (pricingtype == 0 && customer.getItemPriceDictionary().ContainsKey(syncid))
                {
                    this.OriginalPrice = wholesaleprice;
                    this.Price = wholesaleprice;
                    getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_adjusttype, customer.getItemPriceDictionary()[syncid] - this.OriginalPrice, false);
                }
                else
                {
                    if (pricingtype == 2) this.OriginalPrice = Math.Round(this.PurchasePrice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                    else if (pricingtype == 3) this.OriginalPrice = Math.Round(this.wholesaleprice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                    else if (pricingtype == 9) this.OriginalPrice = this.Price;
                    else this.OriginalPrice = this.wholesaleprice;
                    this.Price = this.OriginalPrice;
                }
            }

            Console.WriteLine("reset_data_by_mode discount : " + this.RegularDiscount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);
            Console.WriteLine("reset_data_by_mode 1: oprice: " + this.OriginalPrice);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior5);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_nonvat);
            if (!this.is_history)
                this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_promoqty);

            Console.WriteLine("reset_data_by_mode 2: oprice: " + this.OriginalPrice);
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
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.OriginalPrice);

            //detail discount
            decimal tmp_disc = this.productdiscount.get_discounts_percentage(this.OriginalPrice);
            /*-------------------------------------------------*/
            //ROBI

            Console.WriteLine("reset_data_by_mode price: " + this.Price);
            Console.WriteLine("reset_data_by_mode discount : " + this.RegularDiscount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);

            // COMPUTE PRICE
            //detaildiscount = distributed head discounts and detaildiscount
            //1st run set detail discounts
            //2nd run distribute head discounts
            this.RegularDiscount = 1 - (1M - this.RegularDiscount) * (1M - tmp_disc);
            this.Price = (this.Price + this.adjust) * (1 - this.RegularDiscount);

            Console.WriteLine("reset_data_by_mode dc: " + this.Price);

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

        public void reprint_reset_data_by_mode(bool nonvattrans, bool issenior_d, bool iswholesale, int pricingtype)
        {
            this.OriginalPrice = this.Price;

            Console.WriteLine("reset_data_by_mode discount : " + this.RegularDiscount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);
            Console.WriteLine("reset_data_by_mode 1: oprice: " + this.OriginalPrice);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_nonvat);
            if (!this.is_history)
                this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_promoqty);

            Console.WriteLine("reset_data_by_mode 2: oprice: " + this.OriginalPrice);
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
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.OriginalPrice);

            Console.WriteLine("reset_data_by_mode price: " + this.Price);
            Console.WriteLine("reset_data_by_mode discount : " + this.RegularDiscount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);

            if (this.RegularDiscount != 0)
                this.Price = this.OriginalPrice * (1 - this.RegularDiscount);
            else
                this.Price = this.OriginalPrice + this.adjust;

            Console.WriteLine("reset_data_by_mode dc: " + this.Price);

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

        public void setAdjust(decimal adjust_d) { this.adjust = adjust_d; }

        //get from attributes
        public long getSyncId() { return this.syncid; }

        public decimal getWholesalePrice()
        {
            return Math.Round(this.wholesaleprice, 2, MidpointRounding.AwayFromZero);
        }

        public string getTransaction_Mode()
        {
            return this.transaction_mode;
        }

        public decimal getPurchasePrice() { return Math.Round(this.PurchasePrice, 2, MidpointRounding.AwayFromZero); }
        public decimal getAdjust() { return Math.Round(this.adjust, 2, MidpointRounding.AwayFromZero); }

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
