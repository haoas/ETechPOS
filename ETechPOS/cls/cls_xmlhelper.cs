using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ETech.cls;
using System.Windows.Forms;
using ETech.fnc;

namespace ETech.cls
{
    class cls_xmlhelper
    {
        public static bool hasxmlerror = false;

        public static void create_xml_if_missing()
        {
            XDocument document;
            if (!File.Exists(cls_globalvariables.OfflineXMLpath))
            {
                document = new XDocument(new XElement("offlinexml"));
                document.Save(cls_globalvariables.OfflineXMLpath);
            }
            else
            {
                try
                {
                    document = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                }
                catch (Exception)
                {
                    document = new XDocument(new XElement("offlinexml"));
                    document.Save(cls_globalvariables.OfflineXMLpath);
                }
            }
        }

        public static Int64 Get_ORno_From_Xml()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                XElement offlinexml = xmlDoc.Element("offlinexml");
                XElement Xelem_NextORno = offlinexml.Element("NextORNo");
                return (Int64)Xelem_NextORno;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Int64 Get_Transno_From_Xml()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                XElement offlinexml = xmlDoc.Element("offlinexml");
                XElement Xelem_NextTransno = offlinexml.Element("NextTransNo");
                return (Int64)Xelem_NextTransno;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string Get_LockedDate_From_Xml()
        {
            //should be in string format, has problem on date format
            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                XElement offlinexml = xmlDoc.Element("offlinexml");
                XElement Xelem_LockedDate = offlinexml.Element("LockedDate");
                return (string)Xelem_LockedDate;
            }
            catch (Exception)
            {
                return DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd"); ;
            }
        }

        public static void setOrAndTransNoToXML(cls_POSTransaction trans)
        {
            create_xml_if_missing();

            Int64 NextOR = Convert.ToInt64(trans.getORnumber()) + 1;
            Int64 NextTrans = Convert.ToInt64(trans.gettransactionno()) + 1;

            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                xmlDoc.Root.Descendants("NextORNo").Remove();
                xmlDoc.Root.Descendants("NextTransNo").Remove();
                xmlDoc.Root.Add(new XElement("NextORNo", NextOR));
                xmlDoc.Root.Add(new XElement("NextTransNo", NextTrans));
                xmlDoc.Save(cls_globalvariables.OfflineXMLpath);
            }
            catch (Exception)
            {}
        }

        public static void setLockedDateToXML(DateTime datetime_d)
        {
            create_xml_if_missing();
            string datetime_s = datetime_d.ToString("yyyy-MM-dd");

            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                xmlDoc.Root.Descendants("LockedDate").Remove();
                xmlDoc.Root.Add(new XElement("LockedDate", datetime_s));
                xmlDoc.Save(cls_globalvariables.OfflineXMLpath);
            }
            catch (Exception)
            {}
        }

        public static bool SaveTranToXML(cls_POSTransaction offlinetran)
        {
            string ORno = offlinetran.getORnumber();
            string Transno = offlinetran.gettransactionno();
            string cash = offlinetran.getpayments().get_cash().ToString("0.00");
            string datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string memo = offlinetran.getpayments().get_memo();

            cls_xmlhelper.create_xml_if_missing();

            XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
            XElement productlist = new XElement("productlist");
            foreach (cls_product prod in offlinetran.get_productlist().get_productlist())
            {
                string barcode = prod.getBarcode();
                string price = prod.getPrice().ToString("0.00");
                string qty = prod.getQty().ToString("0.00");

                productlist.Add(new XElement("product",
                   new XAttribute("barcode", barcode),
                   new XElement("price", price),
                   new XElement("qty", qty))
               );
            }

            xmlDoc.Root.Add(
                new XElement("offlinetrans",
                    new XAttribute("OR", ORno),
                    new XElement("Transno", Transno),
                    new XElement("datetime", datetime),
                    new XElement("payments", new XElement("cash", cash), new XElement("memo", memo)),
                       productlist)
                    );
            xmlDoc.Save(cls_globalvariables.OfflineXMLpath);
            return true;
        }

        public static cls_POSTransaction Get_FirstTrans_from_OfflineXML()
        {
            //Should Be connected to SQL server
            //Gets the POSTransaction of the FIRST OR in OfflineXML
            //returns null if unsuccessful
            create_xml_if_missing();
            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                XElement offlinexml = xmlDoc.Element("offlinexml");
                XElement offlinetrans = offlinexml.Element("offlinetrans");

                if (offlinetrans == null)
                    return null;

                XAttribute Xattr_ORno = offlinetrans.Attribute("OR");
                XElement Xelem_Transno = offlinetrans.Element("Transno");
                XElement datetime = offlinetrans.Element("datetime");

                XElement payments = offlinetrans.Element("payments");
                XElement Xelem_cash = payments.Element("cash");
                XElement Xelem_memo = payments.Element("memo");

                string ORno = (string)Xattr_ORno;
                string Transno = (string)Xelem_Transno;
                DateTime datetime_d = (DateTime)datetime;
                decimal cashtender = (Decimal)Xelem_cash;
                string memo = (string)Xelem_memo;

                XElement productlist = offlinetrans.Element("productlist");

                cls_POSTransaction offline_postrans = new cls_POSTransaction();
                offline_postrans.setORnumber(ORno);
                offline_postrans.settransactionno(Transno);
                offline_postrans.setdatetime(datetime_d);

                cls_user offlineuser = new cls_user();
                List<int> offlineperm = new List<int>();
                offlineperm.Add(100);
                offlineuser.setcls_user("0000", "offline user", offlineperm, 2);
                offline_postrans.setclerk(offlineuser);

                cls_payment offlinepayment = new cls_payment();
                offlinepayment.set_cash(cashtender);
                offlinepayment.set_memo(memo);
                offline_postrans.setpayments(offlinepayment);

                foreach (XElement childprod in productlist.Elements("product"))
                {
                    XAttribute Xattr_barcode = childprod.Attribute("barcode");
                    XElement Xelem_price = childprod.Element("price");
                    XElement Xelem_qty = childprod.Element("qty");

                    string barcode = (String)Xattr_barcode;
                    decimal price = (Decimal)Xelem_price;
                    decimal qty = (Decimal)Xelem_qty;

                    cls_product offlineproduct = new cls_product(barcode, price, qty);
                    offline_postrans.get_productlist().add_offline_product(offlineproduct);
                }

                return offline_postrans;
            }
            catch (Exception ex)
            {
                hasxmlerror = true;
                MessageBox.Show(ex.ToString());
                fncFilter.alert("Error in Getting Transaction from OfflineXML");
                return null;
            }
        }

        public static bool Delete_FirstTrans_In_OfflineXML()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(cls_globalvariables.OfflineXMLpath);
                XElement offlinexml = xmlDoc.Element("offlinexml");
                XElement offlinetrans = offlinexml.Element("offlinetrans");

                if (offlinetrans == null)
                    return true;

                offlinetrans.Remove();
                xmlDoc.Save(cls_globalvariables.OfflineXMLpath);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                fncFilter.alert("Error in Deleting Transaction in OfflineXML");
                return false;
            }
        }

        public static bool SaveOfflineTransToDB(cls_POSTransaction offlinetran)
        {
            List<string> SQLQueryList = new List<string>();

            string datetime_d = offlinetran.getdatetime().ToString("yyyy-MM-dd hh:mm:ss");
            string branchid = cls_globalvariables.branchid_v;
            string terminalno = cls_globalvariables.terminalno_v;

            int userwid = 2; //offlinetran.getclerk().getwid();

            string transactionno = offlinetran.gettransactionno();
            string ornumber = offlinetran.getORnumber();
            string payment_memo = offlinetran.getpayments().get_memo();

            string startwid = branchid + "0000000";
            string endwid = branchid + "9999999";

            //SALESHEAD---------------------
            SQLQueryList.Add("SET @salesheadwid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @") 
                                INTO @salesheadwid_d FROM `saleshead` 
                                WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");
            SQLQueryList.Add(@"SET @salesheadwid_d = @salesheadwid_d +1;");
            SQLQueryList.Add(@"INSERT INTO `saleshead` (`wid`) VALUES ( @salesheadwid_d );");
            SQLQueryList.Add(@"UPDATE `saleshead` SET
                                `status` = 1,
                                `branchid` = '" + branchid + @"', 
                                `type` = '3',
                                `date` = '" + datetime_d + @"', 
                                `userid` = '" + userwid + @"', 
                                `lastmodifiedby` = '" + userwid + @"', 
                                `lastmodifieddate` = NOW(), 
                                `datecreated` = NOW(),  
                                `show` = 1,
                                `transactionno` = '" + transactionno + @"', 
                                `ornumber` = '" + ornumber + @"',  
                                `terminalno` = '" + terminalno + @"'
                                WHERE `wid` = @salesheadwid_d LIMIT 1;");
            //END SALESHEAD------------------

            //SALESDETAIL--------------------
            SQLQueryList.Add("SET @salesdetailwid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @")
                                INTO @salesdetailwid_d FROM `salesdetail` 
                                WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");

            string salesdetail_sqlbuilder = @"INSERT INTO `salesdetail`
            (`wid`,`headid`,`productid`,`description`,`quantity`,`oprice`,`price`,`pprice`,`vat`) VALUES ";

            foreach (cls_product prod in offlinetran.get_productlist().get_productlist())
            {
                string prod_id = prod.getWid().ToString();
                string prod_memo = prod.getMemo();
                string prod_qty = prod.getQty().ToString("G29");
                string prod_oprice = prod.getOrigPrice().ToString();
                string prod_price = prod.getPrice().ToString();
                string prod_pprice = prod.getPPrice().ToString();
                string prod_vat = prod.getVat().ToString();

                salesdetail_sqlbuilder +=
                @"( @salesdetailwid_d:=@salesdetailwid_d+1,@salesheadwid_d, '" + prod_id + @"', '" + prod_memo + @"',
                    '" + prod_qty + @"','" + prod_oprice + "', '" + prod_price + @"','" + prod_pprice + "','" + prod_vat + "'),";
            }
            salesdetail_sqlbuilder = salesdetail_sqlbuilder.Remove(salesdetail_sqlbuilder.Length - 1);

            SQLQueryList.Add(salesdetail_sqlbuilder + @";");
            //END SALESDETAIL----------------

            //COLLECTIONHEAD-----------------
            SQLQueryList.Add("SET @collectionheadwid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @") 
                                INTO @collectionheadwid_d FROM `collectionhead` 
                                WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");
            SQLQueryList.Add(@"SET @collectionheadwid_d = @collectionheadwid_d +1;");
            SQLQueryList.Add(@"INSERT INTO `collectionhead` (`wid`) VALUES ( @collectionheadwid_d );");
            SQLQueryList.Add(@"UPDATE `collectionhead` SET
                                `collectiondate` = '" + datetime_d + @"', 
                                `userid` = " + userwid + @",
                                `status` = 1,
                                `branchid` = " + branchid + @",
                                `memo` = '" + payment_memo + @"',
                                `lastmodifieddate` = NOW(),
                                `lastmodifiedby` = " + userwid + @",
                                `datecreated` = NOW(),
                                `show` = 1
                            WHERE `wid` = @collectionheadwid_d LIMIT 1;");
            //END COLLECTIONHEAD-------------

            //COLLECTION SALES---------------
            decimal totalamtpaid = offlinetran.getpayments().get_totalamount();
            decimal changeamt = offlinetran.get_changeamount();
            string collectionsalesamt = (totalamtpaid - changeamt).ToString();
            SQLQueryList.Add(@"INSERT INTO `collectionsales` (`headid`, `saleswid`, `amount`)
                               VALUES ( @collectionheadwid_d, @salesheadwid_d,  " + collectionsalesamt + @");");
            //END COLLECTION SALES-----------

            //COLLECTION DETAIL--------------
            decimal cash = offlinetran.getpayments().get_cash();
            decimal change = offlinetran.get_changeamount();

            SQLQueryList.Add(@"SET @collectiondetailwid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @") 
                                INTO @collectiondetailwid_d FROM `collectiondetail` 
                                WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");

            string collectiondetail_sqlbuilder =
                @"INSERT INTO `collectiondetail`(`wid`,`headid`,`method`,`amount`) VALUES 
                 ((@collectiondetailwid_d:=@collectiondetailwid_d+1),@collectionheadwid_d,1," + cash + @")";
            if (change > 0)
                collectiondetail_sqlbuilder += @",((@collectiondetailwid_d:=@collectiondetailwid_d+1),@collectionheadwid_d,1,-" + change + @")";
            SQLQueryList.Add(collectiondetail_sqlbuilder + @";");
            //END COLLECTION DETAIL----------

            //SYNC---------------------------
            if (cls_globalvariables.branchid_v != "10")
            {
                SQLQueryList.Add(@"DELETE FROM `sync` WHERE `tablename`='saleshead' AND `wid`=@salesheadwid_d;");
                SQLQueryList.Add(@"INSERT INTO `sync` (`tablename`,`wid`,`branchid`) VALUES ('saleshead',@salesheadwid_d,10);");

                SQLQueryList.Add(@"DELETE FROM `sync` WHERE `tablename`='collectionhead' AND `wid`=@collectionheadwid_d;");
                SQLQueryList.Add(@"INSERT INTO `sync` (`tablename`,`wid`,`branchid`) VALUES ('collectionhead',@collectionheadwid_d,10);");
            }//END SYNC-----------------------

            cls_posd posdclass = new cls_posd();
            cls_POSTransaction offlinetrans_posd = posdclass.get_modified_trans(offlinetran);

            //SALESHEAD_POSD------------------
            SQLQueryList.Add(@"INSERT INTO `saleshead_posd`
                            (`wid`, `status`, `branchid`, `type`, `date`,
                             `userid`, `lastmodifiedby`, `lastmodifieddate`, `datecreated`, 
                             `show`, `transactionno`, `ornumber`, `terminalno`) 
                            VALUES
                            (@salesheadwid_d, 1, " + branchid + @", 3, '" + datetime_d + @"',
                            " + userwid + @", " + userwid + @", NOW(), NOW(),
                            1, '" + transactionno + @"', '" + ornumber + "', " + terminalno + @" );");
            //END SALESHEAD_POSD--------------

            //SALESDETAIL_POSD----------------
            SQLQueryList.Add("SET @salesdetail_posd_wid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @")
			                    INTO @salesdetail_posd_wid_d FROM `salesdetail_posd` 
			                    WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");

            string salesdetail_posd_sqlbuilder = @"INSERT INTO `salesdetail_posd`
            (`wid`,`headid`,`productid`,`description`,`quantity`,`oprice`,`price`,`pprice`,`vat`) VALUES ";

            foreach (cls_product prod in offlinetrans_posd.get_productlist().get_productlist())
            {
                string prod_id = prod.getWid().ToString();
                string prod_memo = prod.getMemo();
                string prod_qty = prod.getQty().ToString("G29");
                string prod_oprice = prod.getOrigPrice().ToString();
                string prod_price = prod.getPrice().ToString();
                string prod_pprice = prod.getPPrice().ToString();
                string prod_vat = prod.getVat().ToString();

                salesdetail_posd_sqlbuilder +=
                @"( @salesdetail_posd_wid_d:=@salesdetail_posd_wid_d+1,@salesheadwid_d, '" + prod_id + @"', '" + prod_memo + @"',
                    '" + prod_qty + @"','" + prod_oprice + "', '" + prod_price + @"','" + prod_pprice + "','" + prod_vat + "'),";
            }
            salesdetail_posd_sqlbuilder = salesdetail_posd_sqlbuilder.Remove(salesdetail_posd_sqlbuilder.Length - 1);

            SQLQueryList.Add(salesdetail_posd_sqlbuilder + @";");
            //END SALESDETAIL_POSD------------

            //COLLECTIONHEAD_POSD-------------
            SQLQueryList.Add("SET @collectionhead_posd_wid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @") 
                                INTO @collectionhead_posd_wid_d FROM `collectionhead_posd` 
                                WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");
            SQLQueryList.Add(@"SET @collectionhead_posd_wid_d = @collectionhead_posd_wid_d +1;");
            SQLQueryList.Add(@"INSERT INTO `collectionhead_posd` (`wid`) VALUES ( @collectionhead_posd_wid_d );");
            SQLQueryList.Add(@"UPDATE `collectionhead_posd` SET
                                `collectiondate` = '" + datetime_d + @"', 
                                `userid` = " + userwid + @",
                                `status` = 1,
                                `branchid` = " + branchid + @",
                                `memo` = '" + payment_memo + @"',
                                `lastmodifieddate` = NOW(),
                                `lastmodifiedby` = " + userwid + @",
                                `datecreated` = NOW(),
                                `show` = 1
                            WHERE `wid` = @collectionhead_posd_wid_d LIMIT 1;");
            //END COLLECTIONHEAD_POSD---------

            //COLLECTION SALES POSD-----------
            totalamtpaid = offlinetrans_posd.getpayments().get_totalamount();
            changeamt = offlinetrans_posd.get_changeamount();
            collectionsalesamt = (totalamtpaid - changeamt).ToString();
            SQLQueryList.Add(@"INSERT INTO `collectionsales_posd` (`headid`, `saleswid`, `amount`)
                               VALUES (@collectionhead_posd_wid_d,@salesheadwid_d," + collectionsalesamt + @");");
            //END COLLECTION SALES POSD-------

            //COLLECTION DETAIL POSD--------------
            cash = offlinetrans_posd.getpayments().get_cash();
            change = offlinetrans_posd.get_changeamount();

            SQLQueryList.Add(@"SET @collectiondetail_posd_wid_d=0;");
            SQLQueryList.Add(@"SELECT COALESCE(IF(MAX(`wid`)=0," + startwid + @",MAX(`wid`))," + startwid + @") 
                                INTO @collectiondetail_posd_wid_d FROM `collectiondetail_posd` 
                                WHERE `wid` BETWEEN " + startwid + @" AND " + endwid + @" FOR UPDATE;");
            string collectiondetailposd_sqlbuilder =
               @"INSERT INTO `collectiondetail_posd`(`wid`,`headid`,`method`,`amount`) VALUES 
                 ((@collectiondetail_posd_wid_d:=@collectiondetail_posd_wid_d+1),@collectionhead_posd_wid_d,1," + cash + @")";
            if (change > 0)
                collectiondetailposd_sqlbuilder += @",((@collectiondetail_posd_wid_d:=@collectiondetail_posd_wid_d+1),@collectionhead_posd_wid_d,1,-" + change + @")";
            SQLQueryList.Add(collectiondetailposd_sqlbuilder + @";");
            //END COLLECTION DETAIL POSD----------

            SQLQueryList.Add(@"select 'SUCCESS'");

            mySQLClass sqlclass = new mySQLClass();

            //foreach (string str in SQLQueryList)
            //    sqlclass.WriteToErrorLog(" \n " + str);

            string success = sqlclass.exec_trans(SQLQueryList);
            return (success == "SUCCESS") ? true : false;
        }
    }
}
