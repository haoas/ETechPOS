using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ETech.cls
{
    public class cls_productlist
    {
        private List<cls_product> list_product;
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
                this.dtproducts.Rows[row_index]["VatStatus"] = this.list_product[row_index].CurrentVatStatus;
                this.dtproducts.Rows[row_index]["productname"] = this.list_product[row_index].getProductName();
                this.dtproducts.Rows[row_index]["qty"] = this.list_product[row_index].getQty().ToString("G29");
                this.dtproducts.Rows[row_index]["price"] = this.list_product[row_index].getPrice().ToString("N2");
                this.dtproducts.Rows[row_index]["amount"] = this.list_product[row_index].getAmount().ToString("N2");
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
            if (prod.getQty() == 0)
                return -1;
            for (int i = 0; i < list_product.Count; i++)
            {
                if (list_product[i].getSyncId() == prod.getSyncId()
                        && prod.getSyncId() != 0
                        && prod.getBarcode() != "-")
                {
                    decimal qty = list_product[i].getQty() + prod.getQty();
                    if (qty == 0)
                    {
                        this.remove_product(i);
                        return i;
                    }
                    list_product[i].setQty(qty);
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

        public int add_offline_product(cls_product prod)
        {
            for (int i = 0; i < list_product.Count; i++)
            {
                Console.WriteLine(prod.getBarcode() + " == " + prod.getBarcode());
                if (list_product[i].getBarcode() == prod.getBarcode())
                {
                    decimal qty = list_product[i].getQty() + prod.getQty();
                    if (qty == 0)
                    {
                        this.remove_product(i);
                        return i;
                    }
                    list_product[i].setQty(qty);
                    list_product[i].setPrice(prod.getPrice());
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
            if (prod.getSyncId() == 0)
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
                //Console.WriteLine(list_product[i].getPrice());
                sync_product_row(i);
            }
            refresh_product_amounts_w_headdisc();
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

        /*
        nonvat_sale
        nonvat_return
        vatable_sale
        vatable_return
        senior_sale
        senior_return
        */
        private decimal get_sales_and_returns(string mode)
        {
            decimal sum = 0;
            //decimal total_noheaddisc = this.get_totalamount_no_head_discount();
            //decimal total_wheaddisc = this.get_totalamount();

            foreach (cls_product prod in list_product)
            {
                Console.WriteLine("productwid " + prod.getSyncId() + "\nproductmode " + prod.getTransaction_Mode());
                if (mode.Equals(prod.getTransaction_Mode()))
                {
                    sum += prod.getAmount();
                    //sum += prod.getAmount() / total_noheaddisc * total_wheaddisc;
                }
            }
            return sum;
        }
        public decimal get_nonvatsale() { return get_sales_and_returns("nonvat_sale"); }
        public decimal get_nonvatreturn() { return get_sales_and_returns("nonvat_return"); }
        public decimal get_vatablesale() { return get_sales_and_returns("vatable_sale"); }
        public decimal get_vatablereturn() { return get_sales_and_returns("vatable_return"); }
        public decimal get_seniorsale() { return get_sales_and_returns("senior_sale"); }
        public decimal get_seniorreturn() { return get_sales_and_returns("senior_return"); }
        public decimal get_seniordiscount()
        {
            decimal sum = 0;
            foreach (cls_product prd in list_product)
            {
                decimal disc = 0;
                if (prd.getIsSenior() == 1)
                {
                    disc = prd.get_discount_amt(cls_globalvariables.dcdetail_senior);
                }
                else if (prd.getIsSenior() == 2)
                {
                    disc = prd.get_discount_amt(cls_globalvariables.dcdetail_senior5);
                }
                sum += prd.getQty() * disc;
            }
            return sum;
        }
        public decimal get_subtotal_nonvat()
        {
            return this.get_nonvatsale() + this.get_nonvatreturn() + this.get_seniorsale() + this.get_seniorreturn();
        }
        public decimal get_subtotal_vat()
        {
            return this.get_vatablesale() + this.get_vatablereturn();
        }
        public decimal get_subtotal_pre_vat() //pre-vat sale (less vat)
        {
            return get_subtotal_vat() / (1 + cls_globalvariables.vat);
        }

        public decimal get_vat()
        {
            return this.get_subtotal_vat() * cls_globalvariables.vat / (1 + cls_globalvariables.vat);
        }

        public decimal get_totalsale()
        {
            return this.get_subtotal_nonvat() + this.get_subtotal_vat();
        }

        //get totalqty
        public decimal get_totalqty()
        {
            decimal sum = 0;
            foreach (cls_product prod in list_product)
            {
                sum += prod.getQty();
            }
            return sum;
        }

        public cls_discountlist getTransDisc() { return this.transDiscount; }

        public void refresh_product_amounts_w_headdisc()
        {
            decimal totalProdAmt = get_totalamount_no_head_discount();
            decimal total_head_disc_perc = this.transDiscount.get_discounts_percentage(totalProdAmt);
            this.distribute_head_discount(total_head_disc_perc);
        }

        //get totalamount w/ discount
        public decimal get_totalamount()
        {
            decimal sum = 0;
            foreach (cls_product prod in list_product)
            {
                sum += prod.getAmount();
            }
            return sum;
        }

        //get totalamount w/o discount
        //ROBI
        public decimal get_totalamount_no_head_discount()
        {
            decimal sum = 0;
            foreach (cls_product prod in list_product)
            {
                sum += prod.getQty() * Math.Round(prod.getProductDiscountList().get_amount_after_discount(prod.getOrigPrice()), 2);
            }
            return sum;
        }

        //get total gross amount
        public decimal get_totalamount_gross()
        {
            decimal sum = 0;
            foreach (cls_product prod in list_product)
            {
                sum += prod.getQty() * prod.getOrigPrice();
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
                this.list_product[row_index].setQty(new_qty);
                this.list_product[row_index].reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
                //Console.WriteLine(this.list_product[row_index].getPrice() + "");
                this.sync_product_row(row_index);
            }
        }

        public void set_salesdetailmemo(int row_index, string memo)
        {
            if (row_index < 0 || row_index >= this.list_product.Count)
                return;

            this.list_product[row_index].setMemo(memo);
            this.sync_product_row(row_index);
        }

        public void append_adjustdiscount_all(decimal totaladjust, decimal totaldiscount)
        {
            decimal proddiscount = totaldiscount;
            if (totaladjust != 0)
            {
                proddiscount = totaladjust * -1 / this.get_totalamount();
            }

            foreach (cls_product cprod in this.list_product)
            {
                decimal cadjust = cprod.getAdjust();
                decimal cdiscount = cprod.getDiscount();

                if (cadjust != 0 && proddiscount != 0)
                {
                    //add discount to adjust
                    decimal discountvalue = proddiscount * cprod.getPrice();
                    cprod.setAdjust(cadjust - discountvalue);
                    cprod.setDiscount(0);
                }
                else if (cdiscount != 0 && proddiscount != 0)
                {
                    //add discount to discount
                    //cprod.setDiscount(cdiscount + proddiscount);
                    decimal discountvalue = proddiscount * cprod.getPrice();
                    decimal prevdiscountvalue = cprod.getPrice() * cdiscount / (1 - cdiscount);

                    cprod.setAdjust(0 - discountvalue - prevdiscountvalue);
                    cprod.setDiscount(0);
                }
                else if (proddiscount != 0)
                {
                    //add discount to discount
                    cprod.setAdjust(0);
                    cprod.setDiscount(proddiscount);
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
                cprod.setAdjust(0);
                cprod.setDiscount(totaldiscount);
                cprod.reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
            }

            this.sync_product_all();

        }

        //update product adjustment and discounts
        public void set_adjustdiscount(int row_index, decimal prodadjust, decimal proddiscount)
        {
            if (row_index < 0 || row_index >= this.list_product.Count)
                return;

            this.list_product[row_index].setAdjust(prodadjust);
            this.list_product[row_index].setDiscount(proddiscount);
            this.list_product[row_index].reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype, this.pricingrate, this.customer);
            this.sync_product_row(row_index);
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

            sSQL = @"SELECT P.`product`, SD.`SyncId`,SD.`productid`, SD.`quantity`, SD.`oprice`, SD.`price` AS 'aprice', 
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
                    prod = new cls_product(0, false, false);
                    prod.setProductName(dr_d["product"].ToString());
                }
                else if (pwid == 1)
                {
                    prod = new cls_product(1, false, true);
                    prod.setProductName("Service Charge: " + cls_globalvariables.ServiceCharge_v + "%");
                }
                else if (pwid == 2)
                {
                    prod = new cls_product(2, false, true);
                    prod.setProductName("Local Tax: " + cls_globalvariables.LocalTax_v + "%");
                    prod.setRetailPrice(Convert.ToDecimal(dr_d["oprice"]));
                }
                else if (pwid != 0)
                {
                    prod = new cls_product(pwid, true, true);
                }
                else
                {
                    prod = new cls_product(pwid, false, true);
                }

                int tempWid = int.TryParse(dr_d["SyncId"].ToString(), out tempWid) ? tempWid : 0;
                prod.setRetailPrice(Convert.ToDecimal(dr_d["oprice"]));
                prod.setOrigPrice(Convert.ToDecimal(dr_d["oprice"]));
                prod.setQty(Convert.ToDecimal(dr_d["quantity"]));
                prod.setSoldBy(new cls_user(Convert.ToInt32(dr_d["soldby"])));
                prod.setPrice(Convert.ToDecimal(dr_d["aprice"]));

                if (prod.getOrigPrice() != 0)
                {
                    decimal dc = decimal.Divide(prod.getPrice(), prod.getOrigPrice());
                    prod.setDiscount(1M - dc);
                    prod.setAdjust(0);
                }

                decimal oprice = Convert.ToDecimal(dr_d["oprice"]);
                decimal oprice_temp = Convert.ToDecimal(dr_d["oprice"]);
                decimal temp_price = Convert.ToDecimal(dr_d["aprice"]);
                decimal temp_discount = Convert.ToDecimal(dr_d["discount1"]);
                decimal temp_adjust = Convert.ToDecimal(dr_d["aprice"]) - (Convert.ToDecimal(dr_d["oprice"]) * (1 - Convert.ToDecimal(dr_d["discount1"])));

                if (this.issenior && (prod.getIsSenior() == 1))
                {
                    oprice_temp = (oprice / (1 + cls_globalvariables.vat)) * (1 - cls_globalvariables.senior);
                }
                if (this.issenior && (prod.getIsSenior() == 2))
                {
                    oprice_temp = (oprice * (1 - cls_globalvariables.senior5));
                }
                else if (this.isnonvat && prod.getIsVat())
                {
                    oprice_temp = oprice / (1 + cls_globalvariables.vat);
                }
                else { }

                if (Math.Round(oprice_temp, 3, MidpointRounding.AwayFromZero) != Math.Round(temp_price, 3, MidpointRounding.AwayFromZero) && temp_discount > 0)
                {
                    prod.setDiscount(temp_discount);
                }
                else if (Math.Round(oprice_temp, 3, MidpointRounding.AwayFromZero) != Math.Round(temp_price, 3, MidpointRounding.AwayFromZero) && temp_discount == 0)
                {
                    prod.setAdjust(temp_adjust);
                }
                else { }

                prod.reprint_reset_data_by_mode(this.isnonvat, this.issenior, this.iswholesale, this.pricingtype);
                this.list_product.Add(prod);
                this.dtproducts.Rows.Add(dtproducts.NewRow());

                Console.WriteLine(prod.getProductName() + ": " + prod.getPrice());
            }

            this.sync_product_all();
        }

        //ROBI
        public void refresh_discountlist() { this.transDiscount.refresh_discountlist(this.get_totalamount_no_head_discount()); }
        public decimal get_last_amt_before_discount(int filter) { return this.transDiscount.get_last_amt_before_discount(filter, this.get_totalamount_no_head_discount()); }
        public decimal get_basis_before_discount(int filter) { return this.transDiscount.get_basis_before_discount(filter, this.get_totalamount_no_head_discount()); }
        public decimal get_discount_percentage() { return this.transDiscount.get_discounts_percentage(this.get_totalamount_no_head_discount()); }
        public void refresh_all_discounts()
        {
            this.refresh_discountlist();
            this.refresh_product_data_by_mode();
        }
        public decimal get_detail_discount_amt(int type)
        {
            decimal sum = 0;
            foreach (cls_product prod in this.get_productlist())
            {
                sum += prod.get_discount_amt(type);
            }
            return sum;
        }
        public DataTable get_discount_amt_summary(int choice)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("type");
            dt.Columns.Add("value");
            dt.Columns.Add("amt");

            if (choice == 1)
            {
                foreach (cls_product prod in this.get_productlist())
                {
                    get_summary(prod.getQty(), dt, prod.getProductDiscountList().get_discount_list(), choice);
                }
            }
            else
            {
                get_summary(1, dt, this.transDiscount.get_discount_list(), choice);
            }
            return dt;
        }

        public void get_summary(decimal qty, DataTable dt, List<cls_discount> dclist, int choice)
        {
            foreach (cls_discount disc in dclist)
            {
                if (!disc.get_status())
                    continue;

                bool newType = true;
                decimal disc_value = (disc.get_ismultiple()) ? ((1 - disc.get_value()) * 100) : 0;

                // RIGEL: Commented out to split Promo QTY per product on receipt
                foreach (DataRow dr in dt.Rows)
                {
                    int type = Convert.ToInt32(dr["type"]);
                    decimal value = (disc.get_ismultiple()) ? ((1 - Convert.ToDecimal(dr["value"])) * 100) : 0;
                    decimal amt = Convert.ToDecimal(dr["amt"]);
                    string name = dr["name"].ToString();

                    if (type == disc.get_type()
                        && ((type != cls_globalvariables.dcdetail_customdiscounttype && choice == 1) || name.Equals(disc.get_name())))
                    {
                        dr["value"] = 0;
                        dr["amt"] = amt + Math.Round((disc.get_discounted_amount() * qty), 2);
                        newType = false;
                    }
                }
                if (newType)
                    dt.Rows.Add(disc.get_name(), disc.get_type(), disc_value, Math.Round((disc.get_discounted_amount() * qty), 2));
            }
        }
        //ROBI
    }
}
