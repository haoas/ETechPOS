using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_product
    {
        public cls_discountlist productdiscount;

        public long SyncId { get; private set; }
        public string Name { get; set; }
        public RoundedDecimal WholesalePrice { get; private set; }
        public RoundedDecimal OriginalPrice { get; set; }

        private RoundedDecimal _RegularDiscountPercentage;
        public RoundedDecimal RegularDiscountPercentage
        {
            get { return _RegularDiscountPercentage; }
            set
            {
                _RegularDiscountPercentage = value;
                _RegularFixedDiscount = 0;
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
            }
        }

        public cls_user SoldBy { get; set; }

        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Barcode { get; set; }
        public string Memo { get; set; }
        public string VatStatus { get; set; }

        //TransactionFields
        public string TransactionVatStatus { get { return "V"; } set { } }

        //Computed Get Fields
        public RoundedDecimal RegularDiscountValue { get { return OriginalPrice * (RegularDiscountPercentage / 100); } }
        public RoundedDecimal Price { get { return OriginalPrice - RegularDiscountValue - RegularFixedDiscount; } }
        public decimal Amount { get { return Price * Quantity; } }
        public decimal Vat
        {
            //Do not change this computation without approval
            get
            {
                if (TransactionVatStatus == "NV" || TransactionVatStatus == "S")
                    return 0;
                else if (TransactionVatStatus == "V")
                {
                    if (VatStatus == "NV")
                        return 0;
                    else
                        return (Price * cls_globalvariables.vat / (1 + cls_globalvariables.vat));
                }
                else
                    throw new Exception("Error in TransactionVatStatus");
            }
        }

        public bool HasNonVatAmount { get { return (TransactionVatStatus == "NV" || TransactionVatStatus == "S" || VatStatus == "NV"); } }
        public decimal NonVatSalesAmount { get { return (Quantity > 0 && HasNonVatAmount) ? Amount : 0; } }
        public decimal NonVatReturnsAmount { get { return (Quantity < 0 && HasNonVatAmount) ? Amount : 0; } }
        public decimal NonVatAmount { get { return NonVatSalesAmount + NonVatReturnsAmount; } }

        public bool HasVatableAmount { get { return (TransactionVatStatus == "V" && VatStatus != "NV"); } }
        public decimal VatableSalesAmount { get { return (Quantity > 0 && HasVatableAmount) ? Amount : 0; } }
        public decimal VatableReturnsAmount { get { return (Quantity < 0 && HasVatableAmount) ? Amount : 0; } }
        public decimal VatableAmount { get { return VatableSalesAmount + VatableReturnsAmount; } }


        public void init()
        {
            this.SyncId = 0;
            this.Barcode = "";
            this.Name = "";
            this.Quantity = 1;
            this.WholesalePrice = 0;
            this.Memo = "";

            this.PurchasePrice = 0;
            this.OriginalPrice = 0;
            this.SoldBy = new cls_user();
            this.RegularFixedDiscount = 0;
            this.VatStatus = "V";

            this.productdiscount = new cls_discountlist(1);
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

            this.SyncId = Convert.ToInt32(dr["pwid"]);
            this.OriginalPrice = Convert.ToDecimal(dr["oprice"]);
            this.PurchasePrice = Convert.ToDecimal(dr["pprice"]);
        }

        public cls_product(decimal price_d, long syncid_d, decimal qty_d)
        {
            this.init();

            this.Barcode = "-";
            this.OriginalPrice = price_d;
            this.WholesalePrice = price_d;
            string sql = @"
                SELECT `product` 
                FROM `product`
                WHERE `SyncId` = " + SyncId;
            DataTable table = mySQLFunc.getdb(sql);
            if (table != null && table.Rows.Count > 0)
                this.Name = table.Rows[0]["product"].ToString();
            this.SyncId = syncid_d;
            if (this.SyncId == 0)
            {
                this.Quantity = qty_d;
                this.VatStatus = "V";
            }
            else if (this.SyncId == 1)
            {
                this.Name = "Service Charge: " + cls_globalvariables.ServiceCharge_v + "%";
                this.VatStatus = "NV";
                this.Quantity = 1;
            }
            else if (this.SyncId == 2)
            {
                this.Name = "Local Tax: " + cls_globalvariables.LocalTax_v + "%";
                this.VatStatus = "NV";
                this.Quantity = 1;
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
                            P.`barcode`, P.`VatStatus`,
                            COALESCE(B.`sellingprice`, 0) AS 'oprice', 
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
            this.SyncId = syncid_d;
            this.Barcode = dr["barcode"].ToString();
            this.Name = dr["productname"].ToString();
            this.PurchasePrice = Convert.ToDecimal(dr["pprice"]);
            this.OriginalPrice = Convert.ToDecimal(dr["oprice"]);
            this.VatStatus = dr["VatStatus"].ToString();
        }

        public void reset_data_by_mode(bool nonvattrans, bool issenior_d, bool iswholesale, int pricingtype, decimal pricingrate, cls_customer customer)
        {
            return;
            this.OriginalPrice = this.Price;

            if (iswholesale && this.SyncId != 0)
            {
                if (pricingtype == 0 && customer.getItemPriceDictionary().ContainsKey(SyncId))
                {
                    this.OriginalPrice = WholesalePrice;
                    getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_adjusttype, customer.getItemPriceDictionary()[SyncId] - this.OriginalPrice, false);
                }
                else
                {
                    if (pricingtype == 2) this.OriginalPrice = Math.Round(this.PurchasePrice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                    else if (pricingtype == 3) this.OriginalPrice = Math.Round(this.WholesalePrice * (1 + (pricingrate / 100)), 2, MidpointRounding.AwayFromZero);
                    else if (pricingtype == 9) this.OriginalPrice = this.Price;
                    else this.OriginalPrice = this.WholesalePrice;
                }
            }

            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior5);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_nonvat);

            Console.WriteLine("reset_data_by_mode 2: oprice: " + this.OriginalPrice);
            if (TransactionVatStatus == "S" && VatStatus == "S")
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_senior, 1 - cls_globalvariables.senior, true);
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            if (TransactionVatStatus == "S" && VatStatus == "S5")
            {
                //Senior Transaction, Senior 5% item
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_senior5, 1 - cls_globalvariables.senior5, true);
            }
            else if (TransactionVatStatus == "NV" && VatStatus == "V")
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            Console.WriteLine("reset_data_by_mode 3: oprice: " + this.OriginalPrice);

            //detail discount
            decimal tmp_disc = this.productdiscount.get_discounts_percentage(this.OriginalPrice);
            /*-------------------------------------------------*/

            // COMPUTE PRICE
            //detaildiscount = distributed head discounts and detaildiscount
            //1st run set detail discounts
            //2nd run distribute head discounts
            //this.RegularDiscount = 1 - (1M - this.RegularDiscount) * (1M - tmp_disc);
            //this.Price = (this.Price + this.FixedAdjustment) * (1 - this.RegularDiscount);

            Console.WriteLine("reset_data_by_mode dc: " + this.Price);
        }

        public void reprint_reset_data_by_mode(bool nonvattrans, bool issenior_d, bool iswholesale, int pricingtype)
        {
            return;
            this.OriginalPrice = this.Price;

            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_senior);
            this.productdiscount.deactivateDiscount(cls_globalvariables.dcdetail_nonvat);

            Console.WriteLine("reset_data_by_mode 2: oprice: " + this.OriginalPrice);
            if (TransactionVatStatus == "S" && VatStatus == "S")
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_senior, 1 - cls_globalvariables.senior, true);
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }
            else if (TransactionVatStatus == "NV" && VatStatus == "V")
            {
                this.productdiscount.activateDiscount(cls_globalvariables.dcdetail_nonvat, 1 / (1 + cls_globalvariables.vat), true);
            }

            //if (this.RegularDiscount != 0)
            //    this.Price = this.OriginalPrice * (1 - this.RegularDiscount);
            //else
            //    this.Price = this.OriginalPrice + this.FixedAdjustment;

            Console.WriteLine("reset_data_by_mode dc: " + this.Price);
        }

        public cls_discountlist getProductDiscountList() { return this.productdiscount; }
        public decimal get_discount_amt(int type) { return this.productdiscount.get_all_discount_amount_of_type(type); }
    }
}
