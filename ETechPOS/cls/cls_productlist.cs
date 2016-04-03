using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_productlist
    {
        public List<cls_product> list_product;
        private DataTable dtproducts;
        private cls_discountlist transDiscount;

        private bool isnonvat;
        private bool iswholesale;
        private bool issenior;
        private int pricingtype = 1;
        private decimal pricingrate = 0;
        private cls_customer customer;

        public cls_productlist()
        {
            this.transDiscount = new cls_discountlist(0);

            this.list_product = new List<cls_product>();

            this.dtproducts = new DataTable();
            this.dtproducts.Columns.Add("VatStatus");
            this.dtproducts.Columns.Add("productname");
            this.dtproducts.Columns.Add("qty");
            this.dtproducts.Columns.Add("price");
            this.dtproducts.Columns.Add("MemDisc");
            this.dtproducts.Columns.Add("RegDisc");
            this.dtproducts.Columns.Add("TransRegDisc");
            this.dtproducts.Columns.Add("amount");

            this.isnonvat = false;
            this.iswholesale = false;
            this.issenior = false;
            this.customer = new cls_customer();
        }

        public void sync_product_all()
        {
            for (int i = 0; i < list_product.Count; i++)
                sync_product_row(i);
        }
        public void sync_product_row(int row_index)
        {
            try
            {
                this.dtproducts.Rows[row_index]["VatStatus"] = this.list_product[row_index].VatStatus;
                this.dtproducts.Rows[row_index]["productname"] = this.list_product[row_index].Name;
                this.dtproducts.Rows[row_index]["qty"] = this.list_product[row_index].Quantity.ToString("G29");
                this.dtproducts.Rows[row_index]["price"] = this.list_product[row_index].Price.ToString();
                this.dtproducts.Rows[row_index]["MemDisc"] = this.list_product[row_index].TransactionMemberDiscountValue.ToString();
                this.dtproducts.Rows[row_index]["RegDisc"]
                    = (this.list_product[row_index].RegularDiscountValue + this.list_product[row_index].RegularFixedDiscount).ToString();
                this.dtproducts.Rows[row_index]["TransRegDisc"] 
                    = (this.list_product[row_index].TransactionRegularDiscountValue + this.list_product[row_index].TransactionRegularFixedDiscount).ToString();
                this.dtproducts.Rows[row_index]["amount"] = Convert.ToString(this.list_product[row_index].Amount);
            }
            catch (Exception) { }
        }

        public void reset_product_data(int row_index)
        {
            cls_product prod = this.list_product[row_index];
            prod.reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
        }

        public int add_product(cls_product prod)
        {
            //prod.reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype);
            if (prod.Quantity == 0)
                return -1;
            for (int i = 0; i < list_product.Count; i++)
            {
                if (list_product[i].SyncId == prod.SyncId
                        && prod.SyncId != 0
                        && prod.Barcode != "-")
                {
                    decimal qty = list_product[i].Quantity + prod.Quantity;
                    if (qty == 0)
                    {
                        this.remove_product(i);
                        return i;
                    }
                    list_product[i].Quantity = qty;
                    list_product[i].reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
                    this.sync_product_row(i);
                    return i;
                }
            }

            int row_index = this.list_product.Count;
            this.list_product.Add(prod);
            this.dtproducts.Rows.Add(dtproducts.NewRow());
            this.sync_product_row(row_index);
            return row_index;
        }

        public int add_product_by_barcode(string barcode_d)
        {
            cls_product prod = new cls_product(barcode_d);
            if (prod.SyncId == 0)
                return -1;

            return this.add_product(prod);
        }

        public DataTable get_dtproduct() { return this.dtproducts; }
        public List<cls_product> get_productlist() { return this.list_product; }
        public cls_product get_product(int row_index) { return this.list_product[row_index]; }


        public void refresh_product_data_by_mode()
        {

            for (int i = 0; i < list_product.Count; i++)
            {
                list_product[i].reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
                //Console.WriteLine(list_product[i].Price);
                sync_product_row(i);
            }
            //refresh_product_amounts_w_headdisc();
        }
        public void set_isnonvat(bool isnonvat_d)
        {
            this.isnonvat = isnonvat_d;
            refresh_product_data_by_mode();
        }
        public bool get_isnonvat() { return this.isnonvat; }
        public void set_iswholesale(bool iswholesale_d)
        {
            this.iswholesale = iswholesale_d;
            refresh_product_data_by_mode();
        }
        public cls_customer get_customer() { return this.customer; }
        public void set_customer(cls_customer customer) { this.customer = customer; }

        public bool get_iswholesale() { return this.iswholesale; }

        public void set_pricingtype_rate(int pricingtype_d, decimal pricingrate_d)
        {
            this.pricingtype = pricingtype_d;
            this.pricingrate = pricingrate_d;
        }
        public int get_pricingtype() { return this.pricingtype; }
        public decimal get_pricingrate() { return this.pricingrate; }

        public void set_issenior(bool issenior_d)
        {
            this.issenior = issenior_d;
            refresh_product_data_by_mode();
        }
        public bool get_issenior() { return this.issenior; }

        public decimal get_seniordiscount()
        {
            decimal sum = 0;
            foreach (cls_product prd in list_product)
            {
                decimal disc = 0;
                if (prd.VatStatus == "S")
                {
                    //disc = prd.get_discount_amt(cls_globalvariables.dcdetail_senior);
                }
                else if (prd.VatStatus == "S5")
                {
                    //disc = prd.get_discount_amt(cls_globalvariables.dcdetail_senior5);
                }
                sum += prd.Quantity * disc;
            }
            return sum;
        }
        public decimal get_subtotal_nonvat()
        {
            return list_product.Sum(P => P.NonVatAmount);
        }
        public decimal get_subtotal_vat()
        {
            return list_product.Sum(P => P.VatableAmount);
        }
        public decimal get_subtotal_pre_vat() //pre-vat sale (less vat)
        {
            return get_subtotal_vat() / (1 + cls_globalvariables.vat);
        }

        public decimal get_totalsale()
        {
            return this.get_subtotal_nonvat() + this.get_subtotal_vat();
        }

        public cls_discountlist getTransDisc() { return this.transDiscount; }

        public void refresh_product_amounts_w_headdisc()
        {
            decimal totalProdAmt = get_totalamount_no_head_discount();
            decimal total_head_disc_perc = this.transDiscount.get_discounts_percentage(totalProdAmt);
            this.distribute_head_discount(total_head_disc_perc);
        }

        //get totalamount w/o discount
        public decimal get_totalamount_no_head_discount()
        {
            decimal sum = 0;
            foreach (cls_product prod in list_product)
            {
                //sum += prod.Quantity * Math.Round(prod.getProductDiscountList().get_amount_after_discount(prod.OriginalPrice), 2);
            }
            return sum;
        }

        // update product quantity
        public void set_quantity(int row_index, decimal new_qty)
        {
            if (row_index < 0 || row_index >= this.list_product.Count)
                return;

            if (new_qty == 0)
            {
                this.remove_product(row_index);
            }
            else
            {
                this.list_product[row_index].Quantity = new_qty;
                this.list_product[row_index].reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
                //Console.WriteLine(this.list_product[row_index].Price + "");
                this.sync_product_row(row_index);
            }
        }

        public void set_salesdetailmemo(int row_index, string memo)
        {
            if (row_index < 0 || row_index >= this.list_product.Count)
                return;

            this.list_product[row_index].Memo = memo;
            this.sync_product_row(row_index);
        }

        public void append_adjustdiscount_all(decimal totaladjust, decimal totaldiscount)
        {
            decimal proddiscount = totaldiscount;
            if (totaladjust != 0)
            {
                proddiscount = totaladjust * -1 / list_product.Sum(S => S.Amount);
            }

            foreach (cls_product cprod in this.list_product)
            {
                decimal cadjust = cprod.RegularFixedDiscount;
                decimal cdiscount = cprod.RegularDiscountValue;

                if (cadjust != 0 && proddiscount != 0)
                {
                    //add discount to adjust
                    decimal discountvalue = proddiscount * cprod.Price;
                    cprod.RegularFixedDiscount = cadjust - discountvalue;
                    /////cprod.RegularDiscountValue = 0;
                }
                else if (cdiscount != 0 && proddiscount != 0)
                {
                    //add discount to discount
                    //cprod.setDiscount(cdiscount + proddiscount);
                    decimal discountvalue = proddiscount * cprod.Price;
                    decimal prevdiscountvalue = cprod.Price * cdiscount / (1 - cdiscount);

                    cprod.RegularFixedDiscount = 0 - discountvalue - prevdiscountvalue;
                    /////cprod.RegularDiscountValue = 0;
                }
                else if (proddiscount != 0)
                {
                    //add discount to discount
                    cprod.RegularFixedDiscount = 0;
                    /////cprod.RegularDiscountValue = proddiscount;
                }
                cprod.reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
            }

            this.sync_product_all();

        }

        public void distribute_head_discount(decimal totaldiscount)
        {
            //totaldiscount = percentage
            foreach (cls_product cprod in this.list_product)
            {
                cprod.RegularFixedDiscount = 0;
                /////cprod.RegularDiscountValue = totaldiscount;
                cprod.reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
            }

            this.sync_product_all();

        }

        //remove product
        public void remove_product(int row_index)
        {
            this.list_product.RemoveAt(row_index);
            this.dtproducts.Rows.RemoveAt(row_index);
        }

        public void set_productlist_by_wid(long syncid_d, bool is_history)
        {
            string sSQL = "SELECT * FROM `saleshead` WHERE `SyncId` = " + syncid_d;

            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
                return;

            DataRow dr = dt.Rows[0];
            this.isnonvat = (Convert.ToInt32(dr["isnonvat"]) == 1);
            this.iswholesale = (Convert.ToInt32(dr["iswholesale"]) == 1);
            this.issenior = (dr["seniorno"].ToString() != "");

            sSQL = @"SELECT P.`product`, SD.`SyncId`,SD.`productid`, SD.`quantity`, SD.`oprice`, SD.`price` AS 'price', 
	                    SD.`discount1`, SD.`vat`, SD.`soldby` 
                    FROM `salesdetail` AS SD
                    LEFT JOIN `product` AS P
                        ON P.`SyncId` = SD.`productid`
                    WHERE SD.`headid` = " + syncid_d + " ORDER BY SD.`id` ";
            dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
                return;

            foreach (DataRow dr_d in dt.Rows)
            {
                int pwid = Convert.ToInt32(dr_d["productid"]);
                Console.WriteLine("1: " + pwid + ": " + dr_d["aprice"]);
                cls_product prod;
                if (pwid == 0)
                {
                    prod = new cls_product(0, false);
                    prod.Name = dr_d["product"].ToString();
                }
                else if (pwid == 1)
                {
                    prod = new cls_product(1, true);
                    prod.Name = "Service Charge: " + cls_globalvariables.ServiceCharge_v + "%";
                }
                else if (pwid == 2)
                {
                    prod = new cls_product(2, true);
                    prod.Name = "Local Tax: " + cls_globalvariables.LocalTax_v + "%";
                    prod.OriginalPrice = Convert.ToDecimal(dr_d["oprice"]);
                }
                else if (pwid != 0)
                {
                    prod = new cls_product(pwid, true);
                }
                else
                {
                    prod = new cls_product(pwid, true);
                }

                int tempWid = int.TryParse(dr_d["SyncId"].ToString(), out tempWid) ? tempWid : 0;
                prod.OriginalPrice = Convert.ToDecimal(dr_d["oprice"]);
                prod.Quantity = Convert.ToDecimal(dr_d["quantity"]);
                prod.SoldBy = new cls_user(Convert.ToInt32(dr_d["soldby"]));
                if (prod.OriginalPrice != 0)
                {
                    decimal dc = decimal.Divide(prod.Price, prod.OriginalPrice);
                    /////prod.RegularDiscountValue = 1M - dc;
                    prod.RegularFixedDiscount = 0;
                }

                decimal oprice = Convert.ToDecimal(dr_d["oprice"]);
                decimal oprice_temp = Convert.ToDecimal(dr_d["oprice"]);
                decimal temp_price = Convert.ToDecimal(dr_d["aprice"]);
                decimal temp_discount = Convert.ToDecimal(dr_d["discount1"]);
                decimal temp_adjust = Convert.ToDecimal(dr_d["aprice"]) - (Convert.ToDecimal(dr_d["oprice"]) * (1 - Convert.ToDecimal(dr_d["discount1"])));

                if (this.issenior && (prod.VatStatus == "S"))
                {
                    oprice_temp = (oprice / (1 + cls_globalvariables.vat)) * (1 - cls_globalvariables.senior);
                }
                if (this.issenior && (prod.VatStatus == "S5"))
                {
                    oprice_temp = (oprice * (1 - cls_globalvariables.senior5));
                }
                else if (this.isnonvat && prod.VatStatus == "V")
                {
                    oprice_temp = oprice / (1 + cls_globalvariables.vat);
                }
                else { }

                if (Math.Round(oprice_temp, 3, MidpointRounding.AwayFromZero) != Math.Round(temp_price, 3, MidpointRounding.AwayFromZero) && temp_discount > 0)
                {
                    /////prod.RegularDiscountValue = temp_discount;
                }
                else if (Math.Round(oprice_temp, 3, MidpointRounding.AwayFromZero) != Math.Round(temp_price, 3, MidpointRounding.AwayFromZero) && temp_discount == 0)
                {
                    prod.RegularFixedDiscount = temp_adjust;
                }
                else { }

                prod.reprint_reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype);
                this.list_product.Add(prod);
                this.dtproducts.Rows.Add(dtproducts.NewRow());

                Console.WriteLine(prod.Name + ": " + prod.Price);
            }

            this.sync_product_all();
        }

        public void refresh_discountlist() { this.transDiscount.refresh_discountlist(this.get_totalamount_no_head_discount()); }
        public decimal get_discount_percentage() { return this.transDiscount.get_discounts_percentage(this.get_totalamount_no_head_discount()); }
        public void refresh_all_discounts()
        {
            //this.refresh_discountlist();
            this.refresh_product_data_by_mode();
        }
    }
}
