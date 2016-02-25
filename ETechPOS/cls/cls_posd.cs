using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using ETech.cls;
using ETech.fnc;

namespace ETech.cls
{
    class cls_posd
    {
        mySQLClass mysqlclass = new mySQLClass();

        public int save_transaction_posd(cls_POSTransaction trans)
        {
            if (((trans.getpayments().get_creditamount() > 0) || (trans.getpayments().get_debitamount() > 0))
                || cls_globalvariables.posd_percent_v == 100
                || (Convert.ToDecimal(cls_globalvariables.posdminamt_v) >= trans.get_productlist().get_totalamount()
                && trans.get_productlist().get_totalamount() >= 0))
            {
                return save_transaction(trans);
            }
            else if (Convert.ToDecimal(cls_globalvariables.posdmaxamt_v) <= trans.get_productlist().get_totalamount()
                || trans.get_productlist().get_totalamount() < 0)
            {
                cls_POSTransaction new_tran = generate_trans(100, 1);
                if (cls_globalvariables.isvat_v == "0") new_tran.get_productlist().set_isnonvat(true);
                new_tran.setWid(trans.getWid());
                new_tran.setORnumber(trans.getORnumber());
                new_tran.settransactionno(trans.gettransactionno());
                new_tran.setclerk(trans.getclerk());
                cls_payment temppayment = new cls_payment();
                temppayment.set_cash(new_tran.get_productlist().get_totalamount());
                new_tran.setpayments(temppayment);
                return save_transaction(new_tran);
            }
            else
            {
                cls_POSTransaction new_tran = generate_trans(trans.get_productlist().get_totalamount(), trans.get_productlist().get_totalqty());
                if (cls_globalvariables.isvat_v == "0") new_tran.get_productlist().set_isnonvat(true);
                new_tran.setWid(trans.getWid());
                new_tran.setORnumber(trans.getORnumber());
                new_tran.settransactionno(trans.gettransactionno());
                new_tran.setclerk(trans.getclerk());
                cls_payment temppayment = new cls_payment();
                temppayment.set_cash(new_tran.get_productlist().get_totalamount());
                new_tran.setpayments(temppayment);
                return save_transaction(new_tran);
            }
        }

        public int save_transaction(cls_POSTransaction trans)
        {
            mySQLClass mysqlclass = new mySQLClass();

            string tbl_saleshead = "saleshead_posd";
            string tbl_salesdetail = "salesdetail_posd";
            string tbl_collectionhead = "collectionhead_posd";
            string tbl_collectiondetail = "collectiondetail_posd";
            string tbl_collectionsales = "collectionsales_posd";
            string tbl_poscardpayment = "poscardpayment_posd";
            string tbl_receivecheck = "receivecheck_posd";

            string datetime_d = trans.getdatetime().ToString("yyyy-MM-dd HH:mm:ss");
            string branchid = cls_globalvariables.branchid_v;
            int salesheadwid = trans.getWid();
            int customerid = trans.getcustomer().getwid();
            string customername = trans.getcustomer().getfullname();
            decimal adjust = trans.getadjust();
            string seniorno = trans.getsenior().get_idnumber();
            string seniorname = trans.getsenior().get_fullname();
            int userid = trans.getclerk().getwid();
            int memberid = trans.getmember().getwid();
            int checkerid = trans.getchecker().getwid();
            decimal totalamt = trans.get_productlist().get_totalamount();
            bool iswholesale = trans.get_productlist().get_iswholesale();
            bool isnonvat = trans.get_productlist().get_isnonvat();
            decimal discount = trans.getdiscount();
            decimal totalpaidamt = trans.getpayments().get_totalamount();

            decimal t = trans.get_productlist().get_totalamount();
            decimal t2 = trans.get_productlist().get_totalamount_no_head_discount();
            decimal headDiscountPercentage = (t2 == 0) ? 0 : (t / t2);

            string sSQL = "";

            sSQL = @"INSERT INTO `" + tbl_saleshead + @"`
                        (`wid`, `status`, `branchid`, `type`, `date`,
                         `userid`, `lastmodifiedby`, `lastmodifieddate`, `datecreated`, 
                         `show`, `transactionno`, `ornumber`, `terminalno`)
                     VALUES
                        (" + salesheadwid + @", 1, " + branchid + @", 3, NOW(),
                         " + userid + @", " + userid + @", NOW(), NOW(),
                          1, '" + trans.gettransactionno() + @"', 
                        '" + trans.getORnumber() + "', " + cls_globalvariables.terminalno_v + @" )";
            mysqlclass.setdb(sSQL);

            foreach (cls_product prod in trans.get_productlist().get_productlist())
            {
                string qty = prod.getQty().ToString("G29");
                string price = (prod.getPrice()).ToString();
                string vat = prod.getVat().ToString();
                vat = (cls_globalvariables.isvat_v == "0") ? "0" : vat;

                cls_user soldby = (cls_user)prod.getSoldBy();
                int soldbywid = 0;
                try { soldbywid = soldby.getwid(); }
                catch { soldbywid = userid; }

                decimal addbackqty = (prod.getQty() < 0) ? -1 * prod.getQty() : 0;
                decimal addbackbigqty = (prod.getQty() < 0) ? prod.getBigQty() : 1;

                if (prod.getPrice() <= 0) continue;
                int next_detailwid = mysqlclass.get_next_wid_withlock(tbl_salesdetail);
                int issenior = (trans.getsenior().get_idnumber().Length >= 1 && prod.getIsSenior() != 0) ? prod.getIsSenior() : 0;
                string sSQLdetail = @"UPDATE `" + tbl_salesdetail + @"` SET
                                `headid` = '" + salesheadwid + @"', 
                                `productid` = '" + prod.getWid().ToString() + @"',  
                                `quantity` = '" + qty + @"',  
                                `bigquantity` = '" + prod.getBigQty() + @"',  
                                `oprice` = '" + prod.getOrigPrice().ToString() + @"',  
                                `discount1` = '" + prod.getDiscount().ToString() + @"', 
                                `price` = '" + price + @"',  
                                `pprice` = '" + prod.getPurchasePrice().ToString() + @"', 
                                `vat` = '" + vat + @"', 
                                `senior` = " + issenior + @", 
                                `soldby` = '" + soldbywid + @"',  
                                `addbackqty` = '" + addbackqty.ToString() + @"',  
                                `addbackbigqty` = '" + addbackbigqty.ToString() + @"' 
                            WHERE `wid` = " + next_detailwid;
                //Console.WriteLine(sSQLdetail);
                mysqlclass.setdb(sSQLdetail);
            }

            int collectionheadwid = mysqlclass.get_next_wid_withlock(tbl_collectionhead);

            string sSQLch = @"UPDATE `" + tbl_collectionhead + @"` SET
                                `customerid` = " + customerid + @", 
                                `collectiondate` = NOW(), 
                                `userid` = " + userid + @",  
                                `status` = 1,
                                `branchid` = " + branchid + @", 
                                `lastmodifieddate` = NOW(), 
                                `lastmodifiedby` = " + userid + @", 
                                `istransfer` = 0, 
                                `datecreated` = NOW(),
                                `show` = 1
                            WHERE `wid` = " + collectionheadwid;

            //Console.WriteLine(sSQLch);
            mysqlclass.setdb(sSQLch);

            string sSQLcs = @"INSERT INTO `" + tbl_collectionsales + @"`
                            (`headid`, `saleswid`, `amount`)
                            VALUES
                            ( " + collectionheadwid + ", " + salesheadwid + ", " + (totalpaidamt - trans.get_changeamount()) + ")";
            //Console.WriteLine(sSQLcs);
            mysqlclass.setdb(sSQLcs);

            decimal cash = trans.getpayments().get_cash();
            decimal mem_points = trans.getpayments().get_points();
            decimal change = trans.get_changeamount();
            List<cls_cardinfo> creditcards = trans.getpayments().get_creditcard();
            List<cls_cardinfo> debitcards = trans.getpayments().get_debitcard();
            List<cls_giftchequeinfo> giftcheques = trans.getpayments().get_giftcheque();
            List<cls_bankcheque> bankcheques = trans.getpayments().get_bankcheque();
            int collectiondetailwid = 0;
            string sSQLcd = "";

            if (cash != 0)
            {
                collectiondetailwid = mysqlclass.get_next_wid_withlock(tbl_collectiondetail);
                //Console.WriteLine("detail: "+collectiondetailwid);

                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 1, 
                                `amount` = " + cash + @"
                           WHERE `wid` = " + collectiondetailwid;

                mysqlclass.setdb(sSQLcd);
            }

            if (change > 0)
            {
                collectiondetailwid = mysqlclass.get_next_wid_withlock(tbl_collectiondetail);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 1,
                                `amount` = -" + change + @"
                           WHERE `wid` = " + collectiondetailwid;

                mysqlclass.setdb(sSQLcd);
            }

            foreach (cls_cardinfo creditcard in creditcards)
            {
                collectiondetailwid = mysqlclass.get_next_wid_withlock(tbl_collectiondetail);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 5, 
                                `amount` = " + creditcard.getamount() + @"
                           WHERE `wid` = " + collectiondetailwid;
                mysqlclass.setdb(sSQLcd);

                int creditpaymentwid = mysqlclass.get_next_wid_withlock(tbl_poscardpayment);
                sSQLcd = @"UPDATE `" + tbl_poscardpayment + @"` SET
                            `collectiondetailid` = '" + collectiondetailwid + @"', 
                            `cardsettingwid` = '" + cls_globalfunc.getcardwid(creditcard.getcardno()) + @"', 
                            `cardno` = '" + creditcard.getcardno() + @"', 
                            `fullname` = '" + escapeString(creditcard.getname()) + @"',
                            `expdate` = '" + creditcard.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `type` = '0', 
                            `approvalcode` = '" + escapeString(creditcard.getapprovalcode()) + @"',
                            `amount` = '" + creditcard.getamount() + @"'
                           WHERE `wid` = " + creditpaymentwid;
                mysqlclass.setdb(sSQLcd);
            }

            foreach (cls_cardinfo debitcard in debitcards)
            {
                collectiondetailwid = mysqlclass.get_next_wid_withlock(tbl_collectiondetail);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 6, 
                                `amount` = " + debitcard.getamount() + @"
                           WHERE `wid` = " + collectiondetailwid;

                //Console.WriteLine(sSQLcd);
                mysqlclass.setdb(sSQLcd);

                int debitpaymentwid = mysqlclass.get_next_wid_withlock(tbl_poscardpayment);
                sSQLcd = @"UPDATE `" + tbl_poscardpayment + @"` SET
                            `collectiondetailid` = '" + collectiondetailwid + @"',
                            `cardsettingwid` = '" + cls_globalfunc.getcardwid(debitcard.getcardno()) + @"', 
                            `cardno` = '" + debitcard.getcardno() + @"', 
                            `fullname` = '" + escapeString(debitcard.getname()) + @"',
                            `expdate` = '" + debitcard.getexpdate().ToString("yyyy-MM-dd") + @"', 
                            `type` = '1', 
                            `approvalcode` = '" + escapeString(debitcard.getapprovalcode()) + @"',
                            `amount` = '" + debitcard.getamount() + @"'
                           WHERE `wid` = " + debitpaymentwid;
                //Console.WriteLine(sSQLcd);
                mysqlclass.setdb(sSQLcd);
            }

            foreach (cls_bankcheque bankcheque in bankcheques)
            {
                //collectiondetailwid = get_next_wid(tbl_collectiondetail, "headid", collectionheadwid.ToString());
                collectiondetailwid = mysqlclass.get_next_wid_withlock(tbl_collectiondetail);
                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 2, 
                                `amount` = " + bankcheque.getamount() + @"
                           WHERE `wid` = " + collectiondetailwid;
                mysqlclass.setdb(sSQLcd);

                int receivecheckwid = mysqlclass.get_next_wid_withlock(tbl_receivecheck);
                sSQLcd = @"UPDATE `" + tbl_receivecheck + @"` SET
                            `reference` = '1', 
                            `referenceno` = '" + collectiondetailwid + @"', 
                            `checkno` = '" + bankcheque.getchequeno() + @"', 
                            `checkbank` = '" + escapeString(bankcheque.getbank()) + @"', 
                            `checkdate` = '" + bankcheque.getchequedate().ToString("yyyy-MM-dd") + @"', 
                            `status` = '1',
                            `amount` = '" + bankcheque.getamount() + @"', 
                            `receivedate` = NOW(), 
                            `istransfer` = '0'
                           WHERE `wid` = " + receivecheckwid;

                //Console.WriteLine(sSQLcd);
                mysqlclass.setdb(sSQLcd);
            }

            if (mem_points > 0)
            {
                collectiondetailwid = mysqlclass.get_next_wid_withlock(tbl_collectiondetail);
                //Console.WriteLine("detail: "+collectiondetailwid);

                sSQLcd = @"UPDATE `" + tbl_collectiondetail + @"` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 8, 
                                `amount` = " + mem_points + @"
                           WHERE `wid` = " + collectiondetailwid;

                mysqlclass.setdb(sSQLcd);
            }
            return 0;
        }

        public cls_POSTransaction generate_trans(decimal total_amt_d, decimal total_qty_d)
        {
            decimal posd_percent = Convert.ToDecimal(cls_globalvariables.posd_percent_v);
            decimal exp_total_amt = total_amt_d * (posd_percent / 100);

            cls_POSTransaction tran_temp = new cls_POSTransaction();

            decimal total_amt = 0;
            while (total_amt < exp_total_amt)
            {
                decimal exp_price = exp_total_amt - total_amt;
                exp_price = Math.Round(exp_price, 0, MidpointRounding.AwayFromZero);
                decimal orig_exp_price = exp_price;

                while (exp_price > 1000)
                {
                    exp_price = exp_price / 10;
                    exp_price = Math.Round(exp_price, 0, MidpointRounding.AwayFromZero);
                }

                decimal llimit = (exp_price > 5) ? exp_price - 5 : 0;

                string sSQL = @"SELECT P.`wid`, BP.`sellingprice`
	                        FROM `product` AS P, `branchprice` AS BP
	                        WHERE P.`wid` = BP.`productid` AND BP.`branchid` = " + cls_globalvariables.branchid_v + @"
		                        AND P.`show` = 1 AND P.`status` = 1
		                        AND BP.`sellingprice` < " + exp_price + @"
                                AND BP.`sellingprice` > " + llimit + @" 
                                AND BP.`wholesaleprice` > 0 
                            ORDER BY BP.`sellingprice` DESC
	                        LIMIT 1";
                DataTable dt = mysqlclass.getdb(sSQL);
                if (dt.Rows.Count <= 0)
                {
                    if (exp_price < 5 && tran_temp.get_productlist().get_totalamount() > 0)
                    {
                        return tran_temp;
                    }
                    else
                    {
                        tran_temp.get_productlist().add_product(new cls_product(orig_exp_price, 0, 1));
                        return tran_temp;
                    }
                }


                Random rnd = new Random();
                int index = 0;
                if (dt.Rows.Count > 1)
                {
                    index = rnd.Next(0, dt.Rows.Count - 1);
                }


                int pwid = Convert.ToInt32(dt.Rows[index]["wid"]);
                decimal sellingprice = Convert.ToDecimal(dt.Rows[index]["sellingprice"]);

                cls_product prod_temp = new cls_product(pwid);
                if (prod_temp.getWid() == 0)
                {
                    prod_temp = new cls_product(sellingprice, 1, 1);
                }

                if ((total_amt + prod_temp.getPrice()) > exp_total_amt && tran_temp.get_productlist().get_totalamount() > 0)
                {
                    break;
                }

                tran_temp.get_productlist().add_product(prod_temp);

                total_amt += prod_temp.getPrice();

            }

            return tran_temp;
        }

        public void print_reading_posd(DateTime datetime_d, int Readtype, int userwid)
        {
            if (Readtype == 3)
            {
                if (datetime_d.Date < DateTime.Now.Date)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += (sender, e) => { fncHardware.printpage_read(sender, e, null, Readtype, datetime_d, datetime_d, true, userwid); };
                    fnc.fncHardware.start_print(pd);
                    RawPrinterHelper.OpenCashDrawer(true);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to Generate Z reading?", "Confirm Box",
                                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (MessageBox.Show("Final Confirmation to Generate Z reading?", "Confirm Box",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            PrintDocument pd = new PrintDocument();
                            pd.PrintPage += (sender, e) => { fncHardware.printpage_read(sender, e, null, Readtype, datetime_d, datetime_d, true, userwid); };
                            fnc.fncHardware.start_print(pd);
                            RawPrinterHelper.OpenCashDrawer(true);
                        }
                    }
                }
            }
            else
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (sender, e) => { fncHardware.printpage_read(sender, e, null, Readtype, datetime_d, datetime_d, true, userwid); };
                fnc.fncHardware.start_print(pd);
                RawPrinterHelper.OpenCashDrawer(true);
            }
        }

        public void print_reading_summary(DateTime datetime_from, DateTime datetime_to)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) => { printpage_readsummary_posd(sender, e, datetime_from, datetime_to, true); };
            fncHardware.start_print(pd);
        }

        private void printpage_readsummary_posd(object sender, PrintPageEventArgs e, DateTime datetime_from, DateTime datetime_to, bool is_posd)
        {
            int nY = 0;
            int nX = 0;
            int maxwidth = 280;

            Graphics g = e.Graphics;

            //business Title
            string sBusinessName = cls_globalvariables.BusinessName_v;
            Rectangle rect_title = new Rectangle(nX, nY, maxwidth, 15);
            nY += fncHardware.DrawString(g, sBusinessName, fncHardware.font_Title, rect_title, fncHardware.brush_Black, fncHardware.format_center());

            //header 1
            DataTable dt_header1 = fncHardware.get_header1();
            List<Rectangle> rect_header1 = fncHardware.create_rect_list(nX, nY, new int[] { maxwidth });
            List<StringFormat> sf_header1 = fncHardware.create_stringformat_list(new int[] { 2 });
            List<Font> font_header1 = fncHardware.create_font_list(new int[] { 3 });
            dt_header1.Rows.Add("Terminal no.: " + cls_globalvariables.terminalno_v);
            nY = fncHardware.DrawStringDataTable(g, dt_header1, font_header1, rect_header1, fncHardware.brush_Black, sf_header1);

            //-----------line-------------
            nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, 280, nY); nY += 5;

            //business Title
            Rectangle rect_title_header = new Rectangle(nX, nY, maxwidth, 15);
            nY += fncHardware.DrawString(g, "Sales Summary Report", fncHardware.font_Title, rect_title_header, fncHardware.brush_Black, fncHardware.format_center());

            DataTable dt_summary = new DataTable();
            dt_summary.Columns.Add(); dt_summary.Columns.Add(); dt_summary.Columns.Add(); dt_summary.Columns.Add();

            string sSQL = @"SELECT DATE_FORMAT(`date`, '%Y-%m-%d') AS 'date', 
	                            `oldgrandtotal`, 
	                            `newgrandtotal`,
	                            `newgrandtotal` - `oldgrandtotal` AS 'sales'
                            FROM posxyzread_posd 
                            WHERE `branchid` = " + cls_globalvariables.branchid_v + @" 
                                AND `terminalno` = " + cls_globalvariables.terminalno_v + @" AND `readtype` = 3 
	                            AND `date` >= '" + datetime_from.ToString("yyyy-MM-dd") + @"'
	                            AND `date` <= '" + datetime_to.ToString("yyyy-MM-dd") + @" 23:59:59'
                            ORDER BY `date`";

            //Console.WriteLine(sSQL);
            DataTable dt = mySQLFunc.getdb(sSQL);
            decimal total = 0;
            for (var day = datetime_from.Date; day.Date <= datetime_to.Date; day = day.AddDays(1))
            {
                string date_cur_str = day.ToString("yyyy-MM-dd");
                DataRow[] found_record = dt.Select("date = '" + date_cur_str + "'");
                if (found_record.Length > 0)
                {
                    decimal sales = Math.Round(Convert.ToDecimal(found_record[0]["sales"]), 2, MidpointRounding.AwayFromZero);
                    dt_summary.Rows.Add(" " + date_cur_str, sales.ToString("N2") + " ");
                    total += sales;
                }
                else
                {
                    dt_summary.Rows.Add(" " + date_cur_str, "-------- ");
                }
            }
            dt_summary.Rows.Add(" ", " ");
            dt_summary.Rows.Add(" Total:", total.ToString("N2") + " ");

            //-----------line-------------
            nY += 5; g.DrawLine(new Pen(fncHardware.brush_Black), 0, nY, 280, nY); nY += 5;

            //items
            List<Rectangle> rect_item = fncHardware.create_rect_list(nX, nY, new int[] { 140, 140 });
            List<StringFormat> sf_item = fncHardware.create_stringformat_list(new int[] { 1, 3 });
            List<Font> font_item = fncHardware.create_font_list(new int[] { 3, 3 });
            nY = fncHardware.DrawStringDataTable(g, dt_summary, font_item, rect_item, fncHardware.brush_Black, sf_item);
            g.Dispose();
        }

        public void void_transaction_posd(cls_POSTransaction tran)
        {
            int lastmodifiedby = tran.get_permissiongiver_wid();

            mySQLFunc.setdb(@"UPDATE saleshead_posd SET `lastmodifieddate`=NOW(), `lastmodifiedby`=" + lastmodifiedby + ", `show` = 0 WHERE `wid` = '" + tran.getWid() + "' LIMIT 1");
            mySQLFunc.setdb(@"UPDATE collectionhead_posd SET `lastmodifieddate`=NOW(), `lastmodifiedby`=" + lastmodifiedby + ", `show`=0 WHERE wid = ( SELECT `headid` FROM collectionsales_posd where `saleswid` = '" + tran.getWid() + "') LIMIT 1");
        }

        public cls_POSTransaction get_modified_trans(cls_POSTransaction trans)
        {
            if (Convert.ToDecimal(cls_globalvariables.posdminamt_v) >= trans.get_productlist().get_totalamount()
                && trans.get_productlist().get_totalamount() >= 0)
            {
                return trans;
            }
            else if (Convert.ToDecimal(cls_globalvariables.posdmaxamt_v) <= trans.get_productlist().get_totalamount()
                || trans.get_productlist().get_totalamount() < 0)
            {
                cls_POSTransaction new_tran = generate_trans(100, 1);
                if (cls_globalvariables.isvat_v == "0") new_tran.get_productlist().set_isnonvat(true);
                new_tran.setWid(trans.getWid());
                new_tran.setORnumber(trans.getORnumber());
                new_tran.settransactionno(trans.gettransactionno());
                new_tran.setclerk(trans.getclerk());
                cls_payment temppayment = new cls_payment();
                temppayment.set_cash(new_tran.get_productlist().get_totalamount());
                new_tran.setpayments(temppayment);
                return new_tran;
            }
            else
            {
                cls_POSTransaction new_tran = generate_trans(trans.get_productlist().get_totalamount(), trans.get_productlist().get_totalqty());
                if (cls_globalvariables.isvat_v == "0") new_tran.get_productlist().set_isnonvat(true);
                new_tran.setWid(trans.getWid());
                new_tran.setORnumber(trans.getORnumber());
                new_tran.settransactionno(trans.gettransactionno());
                new_tran.setclerk(trans.getclerk());
                cls_payment temppayment = new cls_payment();
                temppayment.set_cash(new_tran.get_productlist().get_totalamount());
                new_tran.setpayments(temppayment);
                return new_tran;
            }
        }

        public string escapeString(string str)
        {
            return MySql.Data.MySqlClient.MySqlHelper.EscapeString(str);
        }
    }
}
