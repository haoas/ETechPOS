using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_product
    {
        //public cls_discountlist productdiscount;

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
        //public string VatStatus { get; set; }
        public string VatStatus { get { return "V"; } set { } }

        //TransactionFields
        private string _TransactionVatStatus = "V";
        public string TransactionVatStatus
        {
            get { return _TransactionVatStatus; }
            set
            {
                _TransactionVatStatus = value;
                TransactionSeniorDiscountPercentage = (TransactionVatStatus == "S" && (VatStatus == "S" || VatStatus == "NV")) ? cls_globalvariables.senior : 0;
                TransactionNonVatDiscountPercentage = (TransactionVatStatus == "NV" || VatStatus == "NV" ||
                    (TransactionVatStatus == "S" && (VatStatus == "S" || VatStatus == "NV"))) ? 100M * (cls_globalvariables.nonvat / (100M + cls_globalvariables.nonvat)) : 0;
                TransactionSenior5DiscountPercentage = (TransactionVatStatus == "S" && VatStatus == "S5") ? cls_globalvariables.senior5 : 0;
            }
        }

        public TruncatedDecimal TransactionNonVatDiscountPercentage { get; private set; }
        public RoundedDecimal TransactionNonVatDiscountValue { get { return OriginalPrice * (TransactionNonVatDiscountPercentage / 100M); } }
        private RoundedDecimal PriceAfterNonVatDiscount { get { return OriginalPrice - TransactionNonVatDiscountValue; } }

        public TruncatedDecimal TransactionSeniorDiscountPercentage { get; private set; }
        public RoundedDecimal TransactionSeniorDiscountValue { get { return PriceAfterNonVatDiscount * (TransactionSeniorDiscountPercentage / 100M); } }
        private RoundedDecimal PriceAfterSeniorDiscount { get { return PriceAfterNonVatDiscount - TransactionSeniorDiscountValue; } }

        public TruncatedDecimal TransactionSenior5DiscountPercentage { get; private set; }
        public RoundedDecimal TransactionSenior5DiscountValue { get { return PriceAfterSeniorDiscount * (TransactionSenior5DiscountPercentage / 100M); } }
        private RoundedDecimal PriceAfterSenior5Discount { get { return PriceAfterSeniorDiscount - TransactionSenior5DiscountValue; } }

        public TruncatedDecimal TransactionMemberDiscountPercentage { get; set; }
        public RoundedDecimal TransactionMemberDiscountValue { get { return PriceAfterSenior5Discount * (TransactionMemberDiscountPercentage / 100M); } }
        private RoundedDecimal PriceAfterMemberDiscount { get { return PriceAfterSenior5Discount - TransactionMemberDiscountValue; } }

        public TruncatedDecimal TransactionRegularDiscountPercentage { get; set; }
        public RoundedDecimal TransactionRegularFixedDiscount { get; set; }
        public RoundedDecimal TransactionRegularDiscountValue { get { return PriceAfterMemberDiscount * (TransactionRegularDiscountPercentage / 100M); } }
        private RoundedDecimal PriceAfterTransactionRegularDiscount { get { return PriceAfterMemberDiscount - TransactionRegularDiscountValue - TransactionRegularFixedDiscount; } }

        //Computed Get Fields
        public RoundedDecimal RegularDiscountValue { get { return PriceAfterTransactionRegularDiscount * (RegularDiscountPercentage / 100M); } }
        public RoundedDecimal Price
        {
            get
            {
                return OriginalPrice
                    - TransactionNonVatDiscountValue
                    - TransactionSeniorDiscountValue
                    - TransactionSenior5DiscountValue
                    - TransactionMemberDiscountValue
                    - TransactionRegularDiscountValue
                    - TransactionRegularFixedDiscount
                    - RegularDiscountValue
                    - RegularFixedDiscount;
            }
        }
        public decimal Amount { get { return Price * Quantity; } }
        public TruncatedDecimal Vat
        {
            get
            {
                return (TransactionVatStatus == "NV" || TransactionVatStatus == "S" ||
                    (TransactionVatStatus == "V" && VatStatus == "NV")) ?
                    0 : (Price * cls_globalvariables.vat / (1 + cls_globalvariables.vat));
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

        public bool HasItemDiscounts { get { return (RegularDiscountPercentage != 0 || RegularFixedDiscount != 0); } }
        public bool HasTransactionDiscount
        {
            get
            {
                return (TransactionMemberDiscountValue != 0 || TransactionRegularDiscountValue != 0 || TransactionRegularFixedDiscount != 0);
            }
        }

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
                this.SetProducyBySyncId(syncid);
                return;
            }
        }

        public cls_product(long syncid_d)
        {
            this.init();
            this.SetProducyBySyncId(syncid_d);
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

        public void SetProducyBySyncId(long syncid_d)
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
    }
}
