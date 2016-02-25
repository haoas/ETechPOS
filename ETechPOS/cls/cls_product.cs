using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_product
    {
        private int wid;
        private string barcode;
        private string singlebarcode;
        private string packbarcode;
        private string productname;

        private decimal retailpprice;
        private decimal retailprice;
        private decimal retailwholesaleprice;
        private decimal packpprice;
        private decimal packsellingprice;
        private decimal packwholesaleprice;
        private decimal packqty;

        private decimal pprice;
        private decimal origprice;
        private cls_user soldby;
        private string productname_prefix;
        private string productname_suffix;
        private decimal adjust;
        private decimal discount;
        private string memo;

        private int retail_or_pack;
        private bool ispackage;
        private bool isvat;
        private int issenior;
        private decimal qty;
        private decimal bigqty;

        private decimal price;
        private decimal wholesaleprice;
        private decimal pricea;
        private decimal priceb;
        private decimal pricec;
        private decimal priced;
        private decimal pricee;
        private decimal amount;
        private decimal vat;
        private string price_suffix;
        private string productmode;

        private bool customerHistoricalPricingFlag;

        public cls_productpromo productpromo;

        //private cls_productDiscount productdiscount;
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

        public void init()
        {
            this.customerHistoricalPricingFlag = false;
            this.wid = 0;
            this.barcode = "";
            this.singlebarcode = "";
            this.packbarcode = "";
            this.productname = "";
            this.qty = 1;
            this.bigqty = 1;
            this.price = 0;
            this.wholesaleprice = 0;
            this.pricea = 0;
            this.priceb = 0;
            this.pricec = 0;
            this.priced = 0;
            this.pricee = 0;
            this.memo = "";

            this.retail_or_pack = 0;

            this.retailpprice = 0;
            this.retailprice = 0;
            this.retailwholesaleprice = 0;
            this.packpprice = 0;
            this.packsellingprice = 0;
            this.packwholesaleprice = 0;
            this.packqty = 1;

            this.amount = 0;
            this.pprice = 0;
            this.origprice = 0;
            this.vat = 0;
            this.soldby = new cls_user();
            this.productname_prefix = "";
            this.productname_suffix = "";
            this.price_suffix = "";
            this.ispackage = false;
            this.isvat = true;
            this.issenior = 0;
            this.adjust = 0;
            this.discount = 0;
            this.productmode = "";
            this.transaction_mode = "vatable_sale";

            this.productpromo = new cls_productpromo();
            this.productdiscount = new cls_discountlist(1);

            this.is_history = false;
        }

        //constructor
        public cls_product()
        {
            this.init();
        }

        public cls_product(string barcode_d)
        {
            this.init();

            barcode_d = barcode_d.Trim().Replace("'", "''");

            //search for clientbarcode
            string sSQL = @"SELECT `wid` FROM `product` 
                            WHERE `show` = 1 AND `status` = 1 
                                AND ( `clientbarcode` = @barcode_d OR `clientbarcode2` = @barcode_d ) ";
            DataTable dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d" }),
                                new List<string>(new string[] { barcode_d }));

            if (dt.Rows.Count > 0)
            {
                int pwid = Convert.ToInt32(dt.Rows[0]["wid"]);
                this.setcls_product_by_wid(pwid);
                return;
            }

            if (barcode_d.Length < 12)
                return;

            if (barcode_d.Length > 13)
            {
                sSQL = @"SELECT `wid` FROM `product` 
                            WHERE `show` = 1 AND `status` = 1 
                                AND ( `barcode` = @barcode_d 
                                    OR `clientbarcode` = @barcode_d OR `clientbarcode2` = @barcode_d ) ";
                dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d" }),
                                new List<string>(new string[] { barcode_d }));
                if (dt.Rows.Count > 0)
                {
                    int pwid = Convert.ToInt32(dt.Rows[0]["wid"]);
                    this.setcls_product_by_wid(pwid);
                    return;
                }

                sSQL = @"SELECT P.`wid` FROM `product` AS P, `branchprice` AS B 
                            WHERE P.`show` = 1 AND P.`status` = 1 AND P.`wid` = B.`productid` 
                                AND B.`ispackage` = 1 AND B.`branchid` = " + cls_globalvariables.branchid_v + @"
                                AND ( P.`packbarcode` = @barcode_d 
                                    OR P.`packbarcode2` = @barcode_d )";
                dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d" }),
                                new List<string>(new string[] { barcode_d }));

                if (dt.Rows.Count > 0)
                {
                    int pwid = Convert.ToInt32(dt.Rows[0]["wid"]);
                    this.setcls_product_by_wid(pwid, true, false);
                    return;
                }
            }


            //search for clientbarcode and barcode
            barcode_d = barcode_d.Substring(0, 12);
            sSQL = @"SELECT `wid` FROM `product` 
                            WHERE `show` = 1 AND `status` = 1 
                                AND ( `barcode` LIKE @barcode_d_like 
                                    OR `clientbarcode` = @barcode_d OR `clientbarcode2` = @barcode_d ) ";
            dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d", "barcode_d_like" }),
                                new List<string>(new string[] { barcode_d, barcode_d + "%" }));

            if (dt.Rows.Count > 0)
            {
                int pwid = Convert.ToInt32(dt.Rows[0]["wid"]);
                this.setcls_product_by_wid(pwid);
                return;
            }

            //search for package barcode
            sSQL = @"SELECT P.`wid` FROM `product` AS P, `branchprice` AS B 
                            WHERE P.`show` = 1 AND P.`status` = 1 AND P.`wid` = B.`productid` 
                                AND B.`ispackage` = 1 AND B.`branchid` = " + cls_globalvariables.branchid_v + @"
                                AND ( P.`packbarcode` LIKE @barcode_d_like 
                                    OR P.`packbarcode2` LIKE @barcode_d_like )";
            dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d_like" }),
                                new List<string>(new string[] { barcode_d + "%" }));
            if (dt.Rows.Count > 0)
            {
                int pwid = Convert.ToInt32(dt.Rows[0]["wid"]);
                this.setcls_product_by_wid(pwid, true, false);
                return;
            }

            //search for E13 Barcode with leading zero
            barcode_d = "0" + barcode_d.Substring(0, 11);
            sSQL = @"SELECT `wid` FROM `product` 
                            WHERE `show` = 1 AND `status` = 1 
                                AND ( `barcode` LIKE @barcode_d_like 
                                    OR `clientbarcode` = @barcode_d OR `clientbarcode2` = @barcode_d ) ";
            dt = mySQLFunc.getdb(sSQL,
                                new List<string>(new string[] { "barcode_d", "barcode_d_like" }),
                                new List<string>(new string[] { barcode_d, barcode_d + "%" }));

            if (dt.Rows.Count > 0)
            {
                int pwid = Convert.ToInt32(dt.Rows[0]["wid"]);
                this.setcls_product_by_wid(pwid);
                return;
            }
        }

        public cls_product(int wid_d)
        {
            this.init();
            this.setcls_product_by_wid(wid_d);
        }

        public cls_product(int wid_d, bool is_package, bool is_history)
        {
            this.init();
            this.setcls_product_by_wid(wid_d, is_package, is_history);
        }

        public cls_product(string barcode_d, decimal price_d, decimal qty_d)
        {
            //Required Connection to SQL
            this.init();

            this.barcode = barcode_d;
            this.price = price_d;
            this.qty = qty_d;
            this.amount = this.price * this.qty;
            this.vat = calculate_vat(this.isvat, price_d);

            string sSQL = "";
            sSQL = @"SELECT Count(0),
                    COALESCE(P.`wid`,0) AS 'pwid',
                    COALESCE(P.`product`,'RETAIL') AS 'productname', 
                    COALESCE(B.`purchaseprice`, 0) AS 'pprice',
                    COALESCE(B.`sellingprice`, " + price_d + @") AS 'oprice'
                    FROM `product` AS P
                    LEFT JOIN `branchprice` AS B ON B.`productid`=P.`wid` AND
                        B.`branchid` = " + cls_globalvariables.branchid_v + @" 
                    WHERE (P.`barcode` = '" + barcode_d + @"' OR P.`clientbarcode` = '" + barcode_d + @"')";

            Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            DataRow dr = dt.Rows[0];

            this.wid = Convert.ToInt32(dr["pwid"]);
            this.origprice = Convert.ToDecimal(dr["oprice"]);
            this.pprice = Convert.ToDecimal(dr["pprice"]);
        }

        public cls_product(decimal price_d, int wid_d, decimal qty_d)
        {
            this.init();

            this.barcode = "-";

            this.price = price_d;
            this.origprice = price_d;
            this.retailprice = price_d;
            this.retailwholesaleprice = price_d;
            this.wholesaleprice = price_d;
            this.pricea = price_d;
            this.priceb = price_d;
            this.pricec = price_d;
            this.priced = price_d;
            this.pricee = price_d;
            this.amount = this.qty * price_d;
            string sql = @"
                SELECT `product` 
                FROM `product`
                WHERE `wid` = " + wid;
            DataTable table = mySQLFunc.getdb(sql);
            if (table != null && table.Rows.Count > 0)
                this.productname = table.Rows[0]["product"].ToString();
            this.wid = wid_d;
            if (this.wid == 0)
            {
                this.qty = qty_d;
                this.vat = calculate_vat(this.isvat, price_d);
                this.isvat = (cls_globalvariables.isvat_v == "1");
            }
            else if (this.wid == 1)
            {
                this.productname = "Service Charge: " + cls_globalvariables.ServiceCharge_v + "%";
                this.isvat = false;
                this.qty = 1;
                this.vat = 0;
                this.isvat = false;
                this.transaction_mode = "nonvat_sale";
            }
            else if (this.wid == 2)
            {
                this.productname = "Local Tax: " + cls_globalvariables.LocalTax_v + "%";
                this.isvat = false;
                this.qty = 1;
                this.vat = 0;
                this.isvat = false;
                this.transaction_mode = "nonvat_sale";
            }

        }

        private decimal calculate_vat(bool isvat_d, decimal oprice_d)
        {
            if (!isvat_d) return 0;
            return (Math.Round(oprice_d * cls_globalvariables.vat / (1 + cls_globalvariables.vat), 6, MidpointRounding.AwayFromZero));
        }

        public void setcls_product_by_wid(int wid_d)
        {
            setcls_product_by_wid(wid_d, false, false);
        }

        public void setcls_product_by_wid(int wid_d, bool is_package, bool is_history)
        {
            string sSQL = "";
            sSQL = @"SELECT P.`wid` AS 'pwid', P.`product` AS 'productname', 
                            P.`packbarcode`, P.`barcode`, P.`isvat`, P.`senior`,
                            COALESCE(B.`sellingprice`, 0) AS 'price', 
                            COALESCE(B.`wholesaleprice`, 0) AS 'wholesaleprice', 
                            COALESCE(B.`pricea`, 0) AS 'pricea',
                            COALESCE(B.`priceb`, 0) AS 'priceb',
                            COALESCE(B.`pricec`, 0) AS 'pricec',
                            COALESCE(B.`priced`, 0) AS 'priced',
                            COALESCE(B.`pricee`, 0) AS 'pricee',
                            COALESCE(B.`purchaseprice`, 0) AS 'pprice',
                            COALESCE(B.`packageqty`, 1) AS 'packageqty',
                            COALESCE(B.`packagesellingprice`, 0) AS 'packprice', 
                            COALESCE(B.`packagewholesaleprice`, 0) AS 'packwholesaleprice', 
                            COALESCE(B.`packagepurchaseprice`, 0) AS 'packpprice',
                            COALESCE(B.`ispackage`, 0) AS 'ispackage'
                        FROM `product` AS P
                        LEFT JOIN `branchprice` AS B ON B.`productid` = P.`wid` 
                            AND B.`branchid` = " + cls_globalvariables.branchid_v + @" 
                        WHERE P.`wid` = " + wid_d;

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
            this.wid = wid_d;
            this.singlebarcode = dr["barcode"].ToString();
            this.packbarcode = dr["packbarcode"].ToString();
            this.productname = dr["productname"].ToString();

            this.retailpprice = Convert.ToDecimal(dr["pprice"]);
            this.retailwholesaleprice = Convert.ToDecimal(dr["wholesaleprice"]);
            this.retailprice = Convert.ToDecimal(dr["price"]);
            this.pricea = Convert.ToDecimal(dr["pricea"]);
            this.priceb = Convert.ToDecimal(dr["priceb"]);
            this.pricec = Convert.ToDecimal(dr["pricec"]);
            this.priced = Convert.ToDecimal(dr["priced"]);
            this.pricee = Convert.ToDecimal(dr["pricee"]);
            this.pricea = (this.pricea == 0) ? this.retailwholesaleprice : this.pricea;
            this.priceb = (this.priceb == 0) ? this.retailwholesaleprice : this.priceb;
            this.pricec = (this.pricec == 0) ? this.retailwholesaleprice : this.pricec;
            this.priced = (this.priced == 0) ? this.retailwholesaleprice : this.priced;
            this.pricee = (this.pricee == 0) ? this.retailwholesaleprice : this.pricee;
            this.packqty = Convert.ToDecimal(dr["packageqty"]);
            this.packpprice = Convert.ToDecimal(dr["packpprice"]);
            this.packwholesaleprice = Convert.ToDecimal(dr["packwholesaleprice"]);
            this.packsellingprice = Convert.ToDecimal(dr["packprice"]);
            this.ispackage = Convert.ToInt32(dr["ispackage"]) == 1;
            if (this.packwholesaleprice <= 0 || this.packsellingprice <= 0)
                this.ispackage = false;

            this.isvat = (cls_globalvariables.isvat_v == "0")? false : Convert.ToInt32(dr["isvat"]) == 1;
            this.issenior = Convert.ToInt32(dr["senior"]);

            if (!is_package)
            {
                this.retail_or_pack = 0;
                this.barcode = this.singlebarcode;
                this.bigqty = 1;
                this.pprice = this.retailpprice;
                this.wholesaleprice = this.retailwholesaleprice;
                this.origprice = this.retailprice;
                this.price = this.retailprice;
                this.amount = this.retailprice;
            }
            else
            {
                this.retail_or_pack = 1;
                this.barcode = this.packbarcode;
                this.bigqty = this.packqty;
                this.pprice = this.packpprice;
                this.wholesaleprice = this.packwholesaleprice;
                this.origprice = this.packsellingprice;
                this.price = this.packsellingprice;
                this.amount = this.packsellingprice;
            }

            this.vat = calculate_vat(this.isvat, price);

            this.productpromo.set_qtypromo(wid_d);
            this.productpromo.set_PromoProdDiscount(wid_d);
        }

        public void reset_data_by_mode(bool isnonvat_d, bool issenior_d, bool iswholesale, int pricingtype, decimal pricingrate, cls_customer customer)
        {
            if (this.retail_or_pack == 0)
            {
                this.barcode = this.singlebarcode;
                this.bigqty = 1;
                this.pprice = this.retailpprice;
                this.wholesaleprice = this.retailwholesaleprice;
                this.origprice = this.retailprice;
                this.price = this.retailprice;

                if (iswholesale && this.wid != 0)
                {
                    if (pricingtype == 0 && customer.getItemPriceDictionary().ContainsKey(wid))
                    {
                        this.origprice = wholesaleprice;
                        this.price = wholesaleprice;
                        if (!this.customerHistoricalPricingFlag)
                        {
                            getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_adjusttype, customer.getItemPriceDictionary()[wid] - this.origprice, false);
                            customerHistoricalPricingFlag = true;
                        }
                    }
                    else
                    {
                        if (pricingtype == 2) this.origprice = Math.Round(this.pprice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                        else if (pricingtype == 3) this.origprice = Math.Round(this.wholesaleprice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                        else if (pricingtype == 4) this.origprice = this.pricea;
                        else if (pricingtype == 5) this.origprice = this.priceb;
                        else if (pricingtype == 6) this.origprice = this.pricec;
                        else if (pricingtype == 7) this.origprice = this.priced;
                        else if (pricingtype == 8) this.origprice = this.pricee;
                        else if (pricingtype == 9) this.origprice = this.retailprice;
                        else this.origprice = this.wholesaleprice;
                        this.price = this.origprice;
                        if (this.customerHistoricalPricingFlag)
                        {
                            getProductDiscountList().disable_all_discounts();
                            customerHistoricalPricingFlag = false;
                        }
                    }
                }
                else if (this.customerHistoricalPricingFlag)
                {
                    getProductDiscountList().disable_all_discounts();
                    customerHistoricalPricingFlag = false;
                }
            }
            else
            {
                this.barcode = this.packbarcode;
                this.bigqty = this.packqty;
                this.pprice = this.packpprice;
                this.wholesaleprice = this.packwholesaleprice;
                this.origprice = this.packsellingprice;
                this.price = this.packsellingprice;
                if (iswholesale)
                {
                    this.origprice = this.packwholesaleprice;
                    this.price = this.packwholesaleprice;
                }
                if (this.customerHistoricalPricingFlag)
                {
                    getProductDiscountList().disable_all_discounts();
                    customerHistoricalPricingFlag = false;
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
            else if (isnonvat_d && this.isvat)
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.origprice);

            if (!this.is_history && !iswholesale)
            {
                decimal promo_qty = this.productpromo.get_qtypromo_price(this.qty);
                if (this.ispackage)
                    promo_qty = 0;
                decimal promo_proddc = this.productpromo.get_PromoProdDiscount_price();
                Console.WriteLine("reset_data_by_mode 3A: oprice: " + this.origprice);

                if (promo_qty > 0 && (promo_proddc < 0 || promo_proddc > 0 && promo_qty < promo_proddc))
                    this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_promoqty, promo_qty - this.origprice, false);
                else if (promo_proddc > 0)
                    this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_promoqty, promo_proddc/this.origprice, true);

                Console.WriteLine("reset_data_by_mode 3C oprice: " + this.origprice);
            }
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

            //When fixing reset data by mode
            //setPrice((getOrigPrice() + this.adjust) * (1 - this.discount));
            //if (this.discount != 0)
            //    this.price = this.origprice * (1 - this.discount);
            //else
            //    this.price = this.origprice + this.adjust;

            this.amount = this.qty * getPrice();
            Console.WriteLine("reset_data_by_mode dc: " + this.price);

            if (isnonvat_d || (!this.isvat) || (issenior_d && (this.issenior == 1)))
                this.vat = 0;
            else
            {
                this.vat = calculate_vat(this.isvat, this.price);
            }

            if (this.adjust != 0)
                this.price_suffix = "ADJ by " + this.adjust.ToString("N2") + " (Orig. Price: " + this.origprice.ToString("N2") + " )";
            else if (this.discount != 0)
                this.price_suffix = "Discount by " + (this.discount * 100).ToString("N2") + "%" + " (Orig. Price: " + this.origprice.ToString("N2") + " )";
            else
                this.price_suffix = "";

            this.productmode = (this.isvat) ? "V " : "NV ";
            this.productmode += ((this.issenior != 0) && issenior_d) ? "S" : "NS";

            if (this.qty > 0 && (!this.isvat || isnonvat_d) && (!issenior_d || this.issenior != 1) || (this.wid < 10 && this.wid > 0))
                this.transaction_mode = "nonvat_sale";
            else if (this.qty < 0 && (!this.isvat || isnonvat_d) && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "nonvat_return";
            else if (this.qty > 0 && this.isvat && !isnonvat_d && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_sale";
            else if (this.qty < 0 && this.isvat && !isnonvat_d && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_return";
            else if (this.qty > 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_sale";
            else if (this.qty < 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_return";
            else
                this.transaction_mode = "vatable_sale";
        }

        //set to attributes
        public void setBarcode(string barcode_d)
        {
            this.barcode = barcode_d;
        }

        public void setProductName(string productname_d)
        {
            this.productname = productname_d;
        }

        public void reprint_reset_data_by_mode(bool isnonvat_d, bool issenior_d, bool iswholesale, int pricingtype)
        {
            if (this.retail_or_pack == 0)
            {
                this.barcode = this.singlebarcode;
                this.bigqty = 1;
                this.pprice = this.retailpprice;
                this.wholesaleprice = this.retailwholesaleprice;
                this.origprice = this.retailprice;
            }
            else
            {
                this.barcode = this.packbarcode;
                this.bigqty = this.packqty;
                this.pprice = this.packpprice;
                this.wholesaleprice = this.packwholesaleprice;
                this.origprice = this.packsellingprice;
                this.price = this.packsellingprice;
                if (iswholesale)
                {
                    this.origprice = this.packwholesaleprice;
                    this.price = this.packwholesaleprice;
                }
            }
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
            else if (isnonvat_d && this.isvat)
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.origprice);
            if (!this.is_history && !iswholesale)
            {
                decimal promo_qty = this.productpromo.get_qtypromo_price(this.qty);
                decimal promo_proddc = this.productpromo.get_PromoProdDiscount_price();
                Console.WriteLine("reset_data_by_mode 3A: oprice: " + this.origprice);

                if (promo_qty > 0 && (promo_proddc < 0 || promo_proddc > 0 && promo_qty < promo_proddc))
                    this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_promoqty, promo_qty - this.origprice, false);
                else if (promo_proddc > 0)
                    this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_promoqty, promo_proddc / this.origprice, true);

                Console.WriteLine("reset_data_by_mode 3C oprice: " + this.origprice);
            }

            /*-------------------------------------------------*/
            //ROBI

            Console.WriteLine("reset_data_by_mode price: " + this.price);
            Console.WriteLine("reset_data_by_mode discount : " + this.discount);
            Console.WriteLine("reset_data_by_mode adjust : " + this.adjust);

            if (this.discount != 0)
                this.price = this.origprice * (1 - this.discount);
            else
                this.price = this.origprice + this.adjust;

            this.amount = this.qty * getPrice();
            Console.WriteLine("reset_data_by_mode dc: " + this.price);

            if (isnonvat_d || (!this.isvat) || (issenior_d && (this.issenior == 1)))
                this.vat = 0;
            else
            {
                this.vat = calculate_vat(this.isvat, this.price);
            }

            if (this.adjust != 0)
                this.price_suffix = "ADJ by " + this.adjust.ToString("N2") + " (Orig. Price: " + this.origprice.ToString("N2") + " )";
            else if (this.discount != 0)
                this.price_suffix = "Discount by " + (this.discount * 100).ToString("N2") + "%" + " (Orig. Price: " + this.origprice.ToString("N2") + " )";
            else
                this.price_suffix = "";

            this.productmode = (this.isvat) ? "V " : "NV ";
            this.productmode += ((this.issenior != 0) && issenior_d) ? "S" : "NS";

            if (this.qty > 0 && (!this.isvat || isnonvat_d) && (!issenior_d || this.issenior != 1) || (this.wid < 10 && this.wid > 0))
                this.transaction_mode = "nonvat_sale";
            else if (this.qty < 0 && (!this.isvat || isnonvat_d) && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "nonvat_return";
            else if (this.qty > 0 && this.isvat && !isnonvat_d && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_sale";
            else if (this.qty < 0 && this.isvat && !isnonvat_d && (!issenior_d || this.issenior != 1))
                this.transaction_mode = "vatable_return";
            else if (this.qty > 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_sale";
            else if (this.qty < 0 && (issenior_d && this.issenior == 1))
                this.transaction_mode = "nonvat_return";
            else
                this.transaction_mode = "vatable_sale";
        }

        public void setPackSellingPrice(decimal val)
        {
            this.packsellingprice = val;
        }

        public void setRetailPrice(decimal val)
        {
            this.retailprice = val;
        }

        public void setQty(decimal qty_d)
        {
            this.qty = qty_d;
        }

        public void setMemo(string memo_d)
        {
            this.memo = memo_d;
        }

        public void setPrice(decimal price_d)
        {
            this.price = price_d;
            this.amount = this.price * this.qty;
        }

        public void setOrigPrice(decimal origprice_d)
        {
            this.origprice = origprice_d;
        }

        public void setRetailWholesalePrice(decimal wholesaleprice_d)
        {
            //this.wholesaleprice = wholesaleprice_d;
            this.retailwholesaleprice = wholesaleprice_d;
        }

        public void setRetail_or_pack(int retail_pack)
        {
            this.retail_or_pack = retail_pack;
        }

        public void setVat(decimal vat_d)
        {
            this.vat = vat_d;
        }

        public void setSoldBy(cls_user soldby_d)
        {
            this.soldby = soldby_d;
        }

        public void setProductName_Prefix(string prodname_prefix_d)
        {
            this.productname_prefix = prodname_prefix_d;
        }

        public void setProductName_Suffix(string prodname_suffix_d)
        {
            this.productname_suffix = prodname_suffix_d;
        }

        public void setPrice_Suffix(string price_suffix_d)
        {
            this.price_suffix = price_suffix_d;
        }
        public void setAdjust(decimal adjust_d) { this.adjust = adjust_d; }
        public void setDiscount(decimal discount_d) { this.discount = discount_d; }

        //get from attributes
        public int getWid() { return this.wid; }
        public string getBarcode()
        {
            return this.barcode;
        }

        public string getProductName()
        {
            return (this.productname == "" ? this.barcode : this.productname);
        }

        public decimal getQty()
        {
            return Math.Round(this.qty, cls_globalvariables.qty_places, MidpointRounding.AwayFromZero);
        }

        public string getMemo()
        {
            return this.memo;
        }

        public decimal getBigQty() { return this.bigqty; }

        public decimal getPrice()
        {
            return Math.Round(this.price, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getPrice(string pricingtype)
        {
            if (pricingtype == "A") return Math.Round(this.pricea, 2, MidpointRounding.AwayFromZero);
            else if (pricingtype == "B") return Math.Round(this.priceb, 2, MidpointRounding.AwayFromZero);
            else if (pricingtype == "C") return Math.Round(this.pricec, 2, MidpointRounding.AwayFromZero);
            else if (pricingtype == "D") return Math.Round(this.priced, 2, MidpointRounding.AwayFromZero);
            else if (pricingtype == "E") return Math.Round(this.pricee, 2, MidpointRounding.AwayFromZero);
            else return Math.Round(this.price, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getRetailPrice()
        {
            return Math.Round(this.retailprice, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getPPrice()
        {
            return Math.Round(this.pprice, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getAmount()
        {
            return Math.Round(Math.Round(this.price, 2, MidpointRounding.AwayFromZero) * Math.Round(this.qty, cls_globalvariables.qty_places, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero);
        }

        public decimal getOrigPrice()
        {
            return Math.Round(this.origprice, 2, MidpointRounding.AwayFromZero);
        }

        public decimal getRetailWholesalePrice()
        {
            return Math.Round(this.retailwholesaleprice, 2, MidpointRounding.AwayFromZero);
        }
        public decimal getWholesalePrice()
        {
            return Math.Round(this.wholesaleprice, 2, MidpointRounding.AwayFromZero);
        }

        public int getRetail_or_pack()
        {
            return this.retail_or_pack;
        }

        public bool getIsPackage()
        {
            return this.ispackage;
        }

        public decimal getPackQty()
        {
            return this.packqty;
        }

        public decimal getVat()
        {
            return Math.Round(this.vat, 2, MidpointRounding.AwayFromZero);
        }

        public cls_user getSoldBy()
        {
            return this.soldby;
        }

        public string getProductName_Prefix()
        {
            return this.productname_prefix;
        }

        public string getProductName_Suffix()
        {
            return this.productname_suffix;
        }

        public string getPrice_Suffix()
        {
            return this.price_suffix;
        }

        public string getProduct_Mode()
        {
            return this.productmode;
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
        public decimal get_last_amt_before_discount(int filter) { return this.productdiscount.get_last_amt_before_discount(filter, this.getOrigPrice()); }
        public decimal get_basis_before_discount(int filter) { return this.productdiscount.get_basis_before_discount(filter, this.getOrigPrice()); }
        public decimal get_price_no_head_discount() { return this.getOrigPrice() * (1 - this.getProductDiscountList().get_discounts_percentage(this.getOrigPrice())); }
        public decimal get_discount_amt(int type) { return this.productdiscount.get_all_discount_amount_of_type(type); }
    }
}
