using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ETech.cls;
using ETech.ctrl;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ETech.fnc;

namespace ETech
{
    public partial class POSMain : Form
    {
        #region Variables
        public List<cls_POSTransaction> Trans;
        private int cur_trans_index;

        public cls_user cur_cashier;
        public cls_user cur_checker;
        private cls_bills cur_cash;

        private ctrl_productgrid ctrlproductgridview;
        private ctrl_payment ctrlpaymentlabel;
        private ctrl_btnpanel ctrlbtnpanel;
        private ctrl_otherinfo ctrlOther;
        private ctrl_CustomerDisplay ctrlCustDisp;
        public DialogResult dialogResult;
        public int row_index;
        private fncFullScreen fncfullscreen;
        private frmPOSMainExt frmposmainext;

        private int lastaddedrownumber = -1;
        private bool isLoadSuccessful = true;

        bool gofullscreen = false;
        #endregion

        #region Declaration
        public POSMain()
        {
            InitializeComponent();

            LOGS.CLEAR();
            isLoadSuccessful = mySQLFunc.initialize_global_variables();

            if (!isLoadSuccessful)
                return;

            Install_Fonts();

            //add directories
            cls_globalfunc.CreateIfMissing(@"EOD Reports\");
            cls_globalfunc.CreateIfMissing(@"TEMP\");

            List<Button> btnlist = new List<Button>();
            btnlist.Add(this.btnF1); btnlist.Add(this.btnF2); btnlist.Add(this.btnF3);
            btnlist.Add(this.btnF4); btnlist.Add(this.btnF5); btnlist.Add(this.btnF6);
            btnlist.Add(this.btnF7); btnlist.Add(this.btnF8); btnlist.Add(this.btnF9);
            btnlist.Add(this.btnF10); btnlist.Add(this.btnF11); btnlist.Add(this.btnF12);

            this.Trans = new List<cls_POSTransaction>();
            this.ctrlproductgridview = new ctrl_productgrid(this.dgvProduct);
            this.ctrlpaymentlabel = new ctrl_payment(this.gbTotal, this.gbTendered, this.gbRemaining, this.gbDiscounts,
                                                this.lblTotal, this.lblTendered, this.lblRemaining,
                                                this.gbProductInfo, this.lblProductBarcode_d, this.lblProductDesc_d, this.lblProductPrice_d,
                                                this.lblQuantity_d, this.lblAmount_d, this.lblSwitchPack,
                                                this.lbl_regdisc, this.lbl_memberdisc, this.lbl_adjustments, this.lbl_pospromodiscount);
            this.ctrlbtnpanel = new ctrl_btnpanel(btnlist, this);
            this.ctrlOther = new ctrl_otherinfo(this.pnlOtherInfo, this.lblClerk_d, this.lblChecker_d,
                                                this.lblMode_d, this.lblCustomer_d, this.lblCustomermemo_d,
                                                this.lblMember_d, this.lblWarning);
            this.ctrlCustDisp = new ctrl_CustomerDisplay(this.spcustdisp);

            frmposmainext = new frmPOSMainExt(this);

            myPrinters.SetDefaultPrinter(cls_globalvariables.DefaultPrinter_v);

            this.WindowState = FormWindowState.Maximized;
            this.lblTerminal_d.Text = cls_globalvariables.terminalno_v;

            this.cur_trans_index = -1;

            initial_display();

            this.lblORNumber_d.Text = "";

            // cashier log in
            this.cur_cashier = new cls_user();
            frmLogInMain loginmain = new frmLogInMain();
            loginmain.cashier = this.cur_cashier;
            loginmain.ShowDialog();

            if (loginmain.cashier.getwid() == 0)
            {
                isLoadSuccessful = false;
                return;
            }

            LOGS.LOG_PRINT("LOGGED IN: " + cur_cashier.getfullname());

            this.cur_checker = new cls_user();

            if (cur_cashier.getwid() != 0)
            {

                //Does not continue if Database Version is lower
                if (!cls_globalfunc.CheckDatabaseVersion() && cur_cashier.getwid() != 1)
                {
                    fncFilter.alert("Incompatible Database Version!");
                    isLoadSuccessful = false;
                    return;
                }

                DataTable table = mySQLFunc.getdb(@"
                    SELECT *
                    FROM `posxyzread`
                    LIMIT 1");
                if (table != null)
                {
                    if (!table.Columns.Contains("member_points_amt"))
                    {
                        mySQLFunc.setdb(@"ALTER TABLE `posxyzread`
                            ADD COLUMN `member_points_amt` DOUBLE NULL DEFAULT '0'");
                    }
                }

                table = mySQLFunc.getdb(@"
                    SELECT *
                    FROM `posxyzread_posd`
                    LIMIT 1");
                if (table != null)
                {
                    if (!table.Columns.Contains("member_points_amt"))
                    {
                        mySQLFunc.setdb(@"ALTER TABLE `posxyzread_posd`
                            ADD COLUMN `member_points_amt` DOUBLE NULL DEFAULT '0'");
                    }
                }

                table = mySQLFunc.getdb(@"
                    SELECT *
                    FROM `salesdetaildiscounts`
                    LIMIT 1");
                if (table != null)
                {
                    if (!table.Columns.Contains("discountwid"))
                    {
                        mySQLFunc.setdb(@"ALTER TABLE `salesdetaildiscounts`
                            ADD COLUMN `discountwid` INT(11) DEFAULT '0'");
                    }
                }

                table = mySQLFunc.getdb(@"
                    SELECT *
                    FROM `salesheaddiscounts`
                    LIMIT 1");
                if (table != null)
                {
                    if (!table.Columns.Contains("discountwid"))
                    {
                        mySQLFunc.setdb(@"ALTER TABLE `salesheaddiscounts`
                            ADD COLUMN `discountwid` INT(11) DEFAULT '0'");
                    }
                }

                cls_globalfunc.database_validator();
                cls_globalfunc.DeleteUnusedSalesHead();

                //Does not continue if Servertime is not equal to POStime
                string sql = @"Select NOW() as `now`";
                DataTable DT = mySQLFunc.getdb(sql);
                DateTime datetime = Convert.ToDateTime(DT.Rows[0]["now"].ToString());
                string servertime = datetime.ToString("yyyy-MM-dd hh tt");
                string systemtime = DateTime.Now.ToString("yyyy-MM-dd hh tt");
                if (servertime != systemtime)
                {
                    fncFilter.alert("Warning!\n\nServer Time: " + servertime + " \nPOS Time: " + systemtime + " \nPlease Adjust POS time\n to Server Time");
                    isLoadSuccessful = false;
                    return;
                }



                //add offlineuser if not existing
                DataTable DTO = mySQLFunc.getdb(@"SELECT `wid` FROM `user` WHERE wid=2 LIMIT 1");
                if (DTO.Rows.Count <= 0)
                {
                    string sqlO = @"INSERT INTO `user` 
                    (`wid`, `username`, `fullname`, `type`, `status`, `show`, `usercode`, `position`,`datecreate`,`lastmodifieddate`,`datelogin`) 
                                VALUES ('2', 'offline user', 'offline user', '1', '1', '1', '0000', 'offlineuser',NOW(),NOW(),NOW())";
                    mySQLFunc.setdb(sqlO);
                }

                //Save Offline XML to SQL
                frmGenerateReadings GR = new frmGenerateReadings();
                GR.process = 2;
                GR.ShowDialog();

                //Check branchid
                sql = @"Select `value` FROM config WHERE particular='branchid'";
                DT = mySQLFunc.getdb(sql);
                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Config Table Doesn't have branchid!");
                    isLoadSuccessful = false;
                    return;
                }
                if (cls_globalvariables.branchid_v != DT.Rows[0]["value"].ToString())
                {
                    MessageBox.Show("Branchid in Config is not the same in settings!");
                    isLoadSuccessful = false;
                    return;
                }

                //procedure checker
                // DT = mySQLFunc.getdb(@"SELECT COUNT(*) as cnt FROM INFORMATION_SCHEMA.ROUTINES 
                // WHERE ROUTINE_TYPE='PROCEDURE' AND ROUTINE_SCHEMA='" + cls_globalvariables.database_v + @"';");
                // if (Convert.ToInt16(DT.Rows[0]["cnt"]) <= 15)
                // {
                //     MessageBox.Show("Incomplete Procedures");
                //     isLoadSuccessful = false;
                //     return;
                // }

                //refresh discounttype and discountthierarchy tables
                sql = @"SELECT SUM(A.A) AS 'A' FROM(
                        Select IF(Count(*)=12,1,0) as A FROM discounttype WHERE `branchid`=" + cls_globalvariables.branchid_v + @" AND `status`=1 AND `headdetail`=0 AND (`type`=2 OR `type`=3 OR (`type`>=10 AND `type`<=19))
                        UNION ALL
                        Select IF(Count(*)=4,1,0) as A FROM discounttype WHERE `branchid`=" + cls_globalvariables.branchid_v + @" AND `status`=1 AND `headdetail`=1 AND (`type`>=2 AND `type`<=5)
                        )A";
                DT = mySQLFunc.getdb(sql);
                if (Convert.ToInt16(DT.Rows[0]["A"]) != 2)
                {
                    sql = @"DELETE FROM discounttype";
                    mySQLFunc.setdb(sql);
                    sql = @"DELETE FROM discounthierarchy";
                    mySQLFunc.setdb(sql);
                    Int64 basewid = (Convert.ToInt64(cls_globalvariables.branchid_v) * 10000000);
                    string branchid = cls_globalvariables.branchid_v;
                    sql = @"INSERT INTO `discounthierarchy` (`discountid`,`position`,`basis`) VALUES
			                (" + (basewid + 1) + @",0,-1),
                            (" + (basewid + 2) + @",1,0),
                            (" + (basewid + 3) + @",2,1),
                            (" + (basewid + 4) + @",3,2),
                            (" + (basewid + 5) + @",4,3),
                            (" + (basewid + 6) + @",5,4),
                            (" + (basewid + 7) + @",6,5),
                            (" + (basewid + 8) + @",7,6),
                            (" + (basewid + 9) + @",8,7),
                            (" + (basewid + 10) + @",9,8),
                            (" + (basewid + 11) + @",10,9),
                            (" + (basewid + 12) + @",11,10),
                            (" + (basewid + 13) + @",1,0),
                            (" + (basewid + 14) + @",0,-1),
                            (" + (basewid + 15) + @",2,1),
                            (" + (basewid + 16) + @",3,2);";
                    mySQLFunc.setdb(sql);
                    sql = @"INSERT INTO `discounttype` (`wid`,`branchid`,`particular`,`type`,`value`,`headdetail`,`status`) VALUES 
			                (" + (basewid + 1) + @"," + branchid + @",'Member Discount',2,0,0,1),
                            (" + (basewid + 2) + @"," + branchid + @",'POS Promotion',3,0,0,1),
                            (" + (basewid + 3) + @"," + branchid + @",'Employee Discount',10,0.05,0,1),
                            (" + (basewid + 4) + @"," + branchid + @",'PWD Discount',11,0.2,0,1),
                            (" + (basewid + 5) + @"," + branchid + @",'GPC Discount',12,0.05,0,1),
                            (" + (basewid + 6) + @"," + branchid + @",'VIP Discount',13,0.1,0,1),
                            (" + (basewid + 7) + @"," + branchid + @",'Discount 1',14,0.05,0,1),
                            (" + (basewid + 8) + @"," + branchid + @",'Discount 2',15,0.10,0,1),
                            (" + (basewid + 9) + @"," + branchid + @",'Discount 3',16,0.15,0,1),
                            (" + (basewid + 10) + @"," + branchid + @",'Discount 4',17,0.20,0,1),
                            (" + (basewid + 11) + @"," + branchid + @",'Discount 5',18,0.25,0,1),
                            (" + (basewid + 12) + @"," + branchid + @",'Discount 6',19,0.3,0,1),
                            (" + (basewid + 13) + @"," + branchid + @",'Senior',2,0,1,1),
                            (" + (basewid + 14) + @"," + branchid + @",'Non-VAT',3,0,1,1),
                            (" + (basewid + 15) + @"," + branchid + @",'Senior 5',5,0.05,1,1),
                            (" + (basewid + 16) + @"," + branchid + @",'Promo QTY',4,0,1,1);";
                    mySQLFunc.setdb(sql);
                }
                sql = @"SELECT SUM(A.A) AS 'A' FROM(
                        Select IF(Count(*)=12,1,0) as A FROM discounttype WHERE `branchid`=" + cls_globalvariables.branchid_v + @" AND `status`=1 AND `headdetail`=0 AND (`type`=2 OR `type`=3 OR (`type`>=10 AND `type`<=19))
                        UNION ALL
                        Select IF(Count(*)=4,1,0) as A FROM discounttype WHERE `branchid`=" + cls_globalvariables.branchid_v + @" AND `status`=1 AND `headdetail`=1 AND (`type`>=2 AND `type`<=5)
                        )A";
                DT = mySQLFunc.getdb(sql);
                if (Convert.ToInt16(DT.Rows[0]["A"]) != 2)
                {
                    MessageBox.Show("Database error Please contact ETech");
                    isLoadSuccessful = false;
                    return;
                }

                //autogenerate previous readings
                GR = new frmGenerateReadings();
                GR.process = 1;
                GR.ShowDialog();

                this.cur_cash = new cls_bills();
                cur_cash.get_cashdenomination(cur_cashier, 1);
                if (cur_cash.get_type() == -1)
                {
                    RawPrinterHelper.OpenCashDrawer(false);
                    // cash denomination
                    frmCashDenomination frmCash = new frmCashDenomination();
                    frmCash.cash_bills = this.cur_cash;
                    frmCash.ShowDialog();

                    this.cur_cash = frmCash.cash_bills;
                    this.cur_cash.set_type(1);
                    if (cur_cash.get_totalamount() > 0)
                        this.cur_cash.save_cashdenomination(this.cur_cashier);
                }
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Top);
                cls_globalfunc.formaddkbkpevent(this);

                if (cls_globalvariables.ads_url_v.Length > 0)
                    frmposmainext.Show();
            }
            fncfullscreen = new fncFullScreen(this);
            gofullscreen = fncfullscreen.ResizeScreen(gofullscreen);
            this.Top = 0;
            this.Left = 0;
        }
        #endregion

        #region Events
        private void POSMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isLoadSuccessful)
                return;

            if (this.spcustdisp.IsOpen)
            {
                this.spcustdisp.Close();
            }

            bool permcheck_exit = false;

            if (btnF1.Enabled == false && !Trans.Exists(x => x.get_productlist().get_productlist().Count > 0))
                permcheck_exit = true;
            else if (fncFilter.check_permission_void(this.cur_cashier.getpermission()))
                permcheck_exit = true;
            else
                permcheck_exit = isInput_permission_code(fncFilter.get_permission_void());

            if (permcheck_exit)
            {
                if (MessageBox.Show(cls_globalvariables.confirm_logout_voidtran, "Confirm Box",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // reverse traverse and delete salesheads
                    for (int i = Trans.Count - 1; i >= 0; i--)
                        Delete_Unused_saleshead(Trans[i]);
                    ctrl_CustomerDisplay.initial_display();
                    LOGS.LOG_PRINT("[EXIT]LOGGED OUT: " + cur_cashier.getfullname());
                    return;
                }
            }

            e.Cancel = true;
        }
        public void POSMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F9)
            {
                string localMacAddress = cls_globalfunc.GetMacAddress("Local Area Connection");
                StreamReader reader = new StreamReader(cls_globalvariables.settingspath);
                string content = reader.ReadToEnd();
                reader.Close();
                content = Regex.Replace(content, "POSMacAddress=.*", "POSMacAddress=" + localMacAddress + "\r");
                StreamWriter writer = new StreamWriter(cls_globalvariables.settingspath);
                writer.Write(content);
                writer.Close();
                cls_globalvariables.POSMacAddress_v = localMacAddress;
            }
            else if (this.processShortCutKey(e))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyValue == 13 && this.cur_trans_index >= 0)
            {
                e.Handled = true;

                cls_POSTransaction tran = this.get_curtrans();
                if (tran == null) return;

                if (this.txtBarcode.Text.Trim() == "") return;

                cls_product searchedproduct = new cls_product(this.txtBarcode.Text.Trim());
                if (searchedproduct.getWid() == 0)
                {
                    fncFilter.alert(cls_globalvariables.warning_product_notfound);
                    this.txtBarcode.SelectAll();
                    return;
                }

                if (tran.get_productlist().get_iswholesale() &&
                            (tran.get_productlist().get_pricingtype() == 1))
                {
                    frmWholesale wholesalefrm = new frmWholesale();
                    wholesalefrm.cproduct = searchedproduct;
                    wholesalefrm.ShowDialog();
                }

                if (cls_globalvariables.avoidinvalidpprice_v == "1")
                {
                    if ((searchedproduct.getPrice() <= searchedproduct.getPPrice() && !tran.get_productlist().get_iswholesale()) ||
                        (searchedproduct.getWholesalePrice() <= searchedproduct.getPPrice() && tran.get_productlist().get_iswholesale()))
                    {
                        fncFilter.alert(cls_globalvariables.warning_price_invalid + "\n Purchase Price is Greater Than Selling Price");
                        this.txtBarcode.SelectAll();
                        return;
                    }
                }

                lastaddedrownumber = tran.get_productlist().add_product(searchedproduct);
                tran.get_productlist().refresh_all_discounts();
                this.refresh_all_display(0);
                txtBarcode.Text = "";
                frmposmainext.UpdateDGV(tran);
                refresh_productlist_data(get_curtrans());
                LOGS.LOG_PRINT("[BARCODE]Product Added: " + searchedproduct.getProductName());
            }
        }
        private void POSMain_Load(object sender, EventArgs e)
        {
            if (!isLoadSuccessful)
            {
                Application.Exit();
                return;
            }

            set_app_name();

            if (cur_cashier.getwid() == 0)
                this.Close();

            lblClerk_d.BorderStyle = BorderStyle.None;
            lblChecker_d.BorderStyle = BorderStyle.None;
            lblCustomer_d.BorderStyle = BorderStyle.None;
            lblMember_d.BorderStyle = BorderStyle.None;
            lblMode_d.BorderStyle = BorderStyle.None;
            lblCustomermemo_d.BorderStyle = BorderStyle.None;
            lblWarning.BorderStyle = BorderStyle.None;
            lblSalesMemo.BorderStyle = BorderStyle.None;

            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_inherit(dgvProduct);
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                e.Handled = true;
        }
        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            this.txtBarcode.Focus();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.RowCount != 0)
            {
                fncFilter.gridview_selectrow(this.dgvProduct, this.dgvProduct.CurrentRow.Index);
                this.ctrlpaymentlabel.mode_product_display(this.dgvProduct.CurrentRow.Index);
            }
        }

        private void tmrcheckposdsetting_Tick(object sender, EventArgs e)
        {
            tmrCheckPosdSettingTicked();
        }
        #endregion

        #region Return methods
        public bool processShortCutKey(KeyEventArgs e)
        {
            cls_POSTransaction tran;
            bool isdetected = false;

            /* 0 - product info
             * 1 - payment info
             */
            int mode = 0;
            cls_posd posdclass = new cls_posd();
            cls_user permissiongiver = new cls_user();
            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.ctrlproductgridview.select_previous();

                    isdetected = true; break;

                case Keys.Down:
                    this.ctrlproductgridview.select_next();

                    isdetected = true; break;

                case Keys.F1:
                    if (btnF1.Enabled == false) return true;  //create invoice
                    DateTime now = mySQLFunc.DateTimeNow();
                    DateTime cutofftimestart = now.Date.AddSeconds(cls_globalvariables.endtime_v);
                    DateTime cutofftimeend = now.Date.AddSeconds(cls_globalvariables.starttime_v);
                    if ((cls_globalvariables.endtime_v > 0) &&
                        (now >= cutofftimestart) &&
                        (now < cutofftimeend))
                    {
                        cls_globalfunc.MSGBXLOG("Cannot Create Invoice, Still in Cut-Off time!");
                        return true;
                    }

                    DateTime ZreadDateToday = zreadFunc.getZreadDate(now).Date;
                    DateTime maxDateInZread = zreadFunc.getZreadDate(zreadFunc.GetMaxDateTimeFromPosXYZRead()).Date;
                    if (ZreadDateToday <= maxDateInZread)
                    {
                        cls_globalfunc.MSGBXLOG("POS can no longer Create Invoice since Zread is already created!");
                        break;
                    }
                    this.create_new_invoice();
                    isdetected = true; break;

                case Keys.F2: if (btnF2.Enabled == false) return true;  //switch single or wholesale
                    tran = this.get_curtrans();
                    if (tran == null) return true;

                    if (!check_permission("wholesale"))
                    {
                        isdetected = true; break;
                    }

                    if (tran.getcustomer().getwid() == 0 &&
                        !tran.get_productlist().get_iswholesale())
                    {
                        frmSearchCustomer custform = new frmSearchCustomer();
                        custform.ShowDialog();

                        cls_customer cust = custform.customer;
                        if (cust.getwid() != 0)
                        {
                            tran.setcustomer(cust);
                            LOGS.LOG_PRINT("[F2][Switch] Set Customer/PricingType: " + cust.getfullname() + " " + cust.getPricingType());
                        }
                        else
                        {
                            tran.setcustomer(new cls_customer());
                            tran.get_productlist().set_iswholesale(false);
                            LOGS.LOG_PRINT("[F2][Switch] Cancelled Customer");
                            isdetected = true; break;
                        }
                        tran.get_productlist().set_iswholesale(true);
                        tran.get_productlist().set_pricingtype_rate(tran.getcustomer().getPricingType(), tran.getcustomer().getPricingRate());
                        refresh_productlist_data(tran);
                        isdetected = true; break;
                    }
                    else if (tran.getcustomer().getwid() != 0)
                    {
                        if (tran.get_productlist().get_iswholesale())
                        {
                            tran.setcustomer(new cls_customer());
                            tran.get_productlist().set_iswholesale(false);
                            LOGS.LOG_PRINT("[F2][Switch] Cancelled Customer");
                        }
                        else if (!tran.get_productlist().get_iswholesale())
                        {
                            tran.get_productlist().set_iswholesale(true);
                            tran.get_productlist().set_pricingtype_rate(tran.getcustomer().getPricingType(), tran.getcustomer().getPricingRate());
                            LOGS.LOG_PRINT("[F2][Switch] Set Customer/PricingType: " +
                                get_curtrans().getcustomer().getfullname() + " " +
                                get_curtrans().getcustomer().getPricingType());
                        }
                    }
                    refresh_productlist_data(tran);
                    isdetected = true; break;

                case Keys.F3:   //exit
                    this.Close();
                    isdetected = true; break;

                case Keys.F4: if (btnF4.Enabled == false) return true;  //search product
                    tran = this.get_curtrans();
                    if (tran == null) return true;

                    if (cls_globalvariables.prodsearchstyle_v == "2")
                    {
                        cls_product tempprod2 = (lastaddedrownumber == -1 || Trans[cur_trans_index].get_productlist().get_productlist().Count == 0)
                            ? null : Trans[cur_trans_index].get_productlist().get_product(lastaddedrownumber);
                        frmSearchProductButton searchprodbtn = new frmSearchProductButton(tran, tempprod2);
                        searchprodbtn.ShowDialog();

                        foreach (cls_product tempprod in searchprodbtn.tempProductlist.get_productlist())
                        {
                            lastaddedrownumber = tran.get_productlist().add_product(tempprod);
                            LOGS.LOG_PRINT("[F4-2]Product Added (" + tempprod.getQty() + "): " + tempprod.getProductName());
                        }

                        refresh_productlist_data(tran);
                        isdetected = true;
                        break;
                    }

                    frmSearchProduct prodform = new frmSearchProduct();
                    prodform.ShowDialog();

                    int productwid = prodform.productwid;
                    if (productwid != 0)
                    {
                        cls_product searchedproduct = new cls_product(productwid, false, false);
                        if (searchedproduct.getWid() == 0)
                        {
                            fncFilter.alert(cls_globalvariables.warning_product_notfound);
                            this.txtBarcode.SelectAll();
                            isdetected = true; break;
                        }

                        if ((tran.get_productlist().get_iswholesale()) &&
                            (tran.get_productlist().get_pricingtype() == 1))
                        {
                            frmWholesale wholesalefrm = new frmWholesale();
                            wholesalefrm.cproduct = searchedproduct;
                            wholesalefrm.ShowDialog();
                        }

                        if (cls_globalvariables.avoidinvalidpprice_v == "1")
                        {
                            if ((searchedproduct.getPrice() <= searchedproduct.getPPrice() && !tran.get_productlist().get_iswholesale()) ||
                                (searchedproduct.getWholesalePrice() <= searchedproduct.getPPrice() && tran.get_productlist().get_iswholesale()))
                            {
                                fncFilter.alert(cls_globalvariables.warning_price_invalid);
                                this.txtBarcode.SelectAll();
                                isdetected = true; break;
                            }

                        }
                        //if (searchedproduct != null) {
                        LOGS.LOG_PRINT("[F4-1]Product Added: " + searchedproduct.getProductName());
                        lastaddedrownumber = tran.get_productlist().add_product(searchedproduct);
                        refresh_productlist_data(tran);
                        //}

                        txtBarcode.Text = "";
                    }
                    frmposmainext.UpdateDGV(tran);
                    //frmposmainext.UpdateControls(tran);

                    isdetected = true; break;

                case Keys.F5: if (btnF5.Enabled == false) return true;  //change qty
                    tran = this.get_curtrans();
                    if (tran == null) return true;

                    bool permcheck_deleteproduct = false;
                    bool permcheck_return = false;
                    bool permcheck_forcereturn = false;
                    if (fncFilter.check_permission_delete(this.cur_cashier.getpermission()))
                        permcheck_deleteproduct = true;
                    if (fncFilter.check_permission_return(this.cur_cashier.getpermission()))
                        permcheck_return = true;
                    if (fncFilter.check_permission_forcereturn(this.cur_cashier.getpermission()))
                        permcheck_forcereturn = true;

                    row_index = this.ctrlproductgridview.get_currentrow().Index;
                    cls_product prod = tran.get_productlist().get_product(row_index);
                    string productname = prod.getProductName();
                    if ((prod.getWid() == 1) || (prod.getWid() == 2)) { isdetected = true; break; }
                    decimal cur_prodqty = prod.getQty();
                    string cur_prodmemo = prod.getMemo();

                    frmProductQuantity frmprodqty = new frmProductQuantity();
                    frmprodqty.productid = prod.getWid();
                    frmprodqty.productname = productname;
                    frmprodqty.new_qty = cur_prodqty;
                    frmprodqty.delete_permission = permcheck_deleteproduct;
                    frmprodqty.return_permission = permcheck_return;
                    frmprodqty.forcereturn_permission = permcheck_forcereturn;
                    frmprodqty.ShowDialog();

                    if (cur_prodqty == frmprodqty.new_qty)
                        break;

                    cur_prodqty = frmprodqty.new_qty;
                    cur_prodmemo = frmprodqty.salesdetailmemo;

                    LOGS.LOG_PRINT("Product Changed Qty : " + productname + " " + cur_prodqty);
                    tran.get_productlist().set_quantity(row_index, cur_prodqty);
                    tran.get_productlist().set_salesdetailmemo(row_index, cur_prodmemo);
                    refresh_productlist_data(tran);
                    tran.get_productlist().sync_product_row(row_index);
                    lastaddedrownumber = row_index;

                    frmposmainext.UpdateDGV(tran);
                    isdetected = true; break;

                case Keys.F6:
                    if (btnF6.Enabled == false) return true; //remove transaction

                    bool permcheck_void = false;
                    if (btnF1.Enabled == false && this.get_curtrans().get_productlist().get_productlist().Count <= 0)
                    {
                        permcheck_void = true;
                    }
                    else if (fncFilter.check_permission_void(this.cur_cashier.getpermission()))
                    {
                        permcheck_void = true;

                        if (btnF1.Enabled == false)
                        {
                            if (MessageBox.Show(cls_globalvariables.confirm_logout_deletetran, "Confirm Box",
                                    MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                isdetected = true; break;
                            }
                        }
                    }
                    else
                    {
                        //permcheck_void = isInput_permission_code(fncFilter.get_permission_void());
                        frmPermissionCode frmpermcode = new frmPermissionCode();
                        frmpermcode.permission_needed = fncFilter.get_permission_void();
                        frmpermcode.ShowDialog();
                        permcheck_void = frmpermcode.permcode;
                        permissiongiver.setcls_user_by_wid(Convert.ToInt32(frmpermcode.permissionwid), false);
                    }

                    if (permcheck_void)
                    {
                        if (btnF1.Enabled == false)
                        {
                            Delete_Unused_saleshead(this.get_curtrans());
                            LOGS.LOG_PRINT("Invoice Cancelled: " + this.get_curtrans().getORnumber().ToString());
                            remove_transaction();
                        }
                        else
                        {
                            frmVoid frmvoid = new frmVoid();
                            string or_num = frmvoid.or_number;

                            if (or_num.Length <= 0)
                                break;

                            if (TransactionHasRefundedItem(or_num))
                            {
                                fncFilter.alert(cls_globalvariables.warning_refunded_transaction_cannot_be_voided);
                                break;
                            }

                            if (zreadFunc.Zcount())
                            {
                                Int64 maxORinPosxyzread = zreadFunc.get_max_OR_in_posxyzread();
                                if ((maxORinPosxyzread != 0) && (maxORinPosxyzread >= Convert.ToInt64(or_num)))
                                {
                                    cls_globalfunc.MSGBXLOG("OR cannot be voided. Z-Reading is already reported.");
                                    break;
                                }
                            }

                            cls_POSTransaction temp_tran = new cls_POSTransaction();
                            temp_tran.set_transaction_by_ornumber(or_num, false);
                            temp_tran.set_permissiongiver(permissiongiver);
                            if (temp_tran.getWid() == 0)
                            {
                                fncFilter.alert(cls_globalvariables.warning_ornumber_invalid);
                                break;
                            }
                            if (temp_tran.getShow() == 0)
                            {
                                DialogResult dialogResult =
                                    MessageBox.Show("This OR# is already voided!\nDo you want to reprint?", "Message", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                    fncHardware.print_receipt(temp_tran, true, true);
                                break;
                            }
                            if (temp_tran.getmember().MemberButOffline)
                            {
                                fncFilter.alert("This OR cannot be voided since Member feature is offline.");
                                break;
                            }

                            fncHardware.void_transaction(temp_tran);
                            fncHardware.print_receipt(temp_tran, false, true);

                            posdclass.void_transaction_posd(temp_tran);
                        }
                    }
                    frmposmainext.AfterTran();

                    isdetected = true; break;

                case Keys.F7: if (btnF7.Enabled == false) return true; //delete product
                    tran = this.get_curtrans();
                    if (tran == null) return true;

                    int prodWid = tran.get_productlist().get_product(ctrlproductgridview.get_currentrow().Index).getWid();
                    if (prodWid == 1 || prodWid == 2)
                    {
                        fncFilter.alert(@"Not allowed!");
                        isdetected = true; break;
                    }

                    bool permcheck_delete = false;
                    if (fncFilter.check_permission_delete(this.cur_cashier.getpermission()))
                    {
                        permcheck_delete = true;

                        if (MessageBox.Show(cls_globalvariables.confirm_logout_deleteitem, "Confirm Box",
                                MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            isdetected = true; break;
                        }

                    }
                    else
                    {
                        //permcheck_delete = isInput_permission_code(fncFilter.get_permission_delete());
                        frmPermissionCode frmpermcode = new frmPermissionCode();
                        frmpermcode.permission_needed = fncFilter.get_permission_delete();
                        frmpermcode.ShowDialog();
                        permcheck_delete = frmpermcode.permcode;
                        permissiongiver.setcls_user_by_wid(Convert.ToInt32(frmpermcode.permissionwid), false);
                        tran.set_permissiongiver(permissiongiver);
                    }

                    int row_index_delete = this.ctrlproductgridview.get_currentrow().Index;
                    if (permcheck_delete)
                    {
                        LOGS.LOG_PRINT("Product Removed : " + tran.get_productlist().get_product(row_index_delete).getProductName() + " BY " + tran.get_permissiongiver_fullname());
                        tran.get_productlist().remove_product(row_index_delete);
                        lastaddedrownumber = tran.get_productlist().get_productlist().Count - 1;
                    }

                    refresh_productlist_data(tran);

                    tran.get_productlist().sync_product_row(row_index_delete);

                    //frmposmainext.UpdateDGV(tran);
                    isdetected = true; break;

                case Keys.F8: if (btnF8.Enabled == false) return true; // payment
                    tran = this.get_curtrans();
                    if ((tran == null) || (tran.get_productlist().get_productlist().Count == 0)) return true;
                    if (cls_globalvariables.RefundMemo_v == "1")
                    {
                        for (int i = 0; i < tran.get_productlist().get_productlist().Count; i++)
                        {
                            if (tran.get_productlist().get_product(i).getQty() < 0)
                            {
                                frmSalesmemo salesmemo = new frmSalesmemo();
                                salesmemo.salesheadwid = tran.getWid();
                                salesmemo.ShowDialog();
                                continue;
                            }
                        }
                    }

                    frmPayment payment = new frmPayment();
                    payment.paymentdata = tran.getpayments().DeepCopy();
                    payment.totalamtdue = tran.get_productlist().get_totalamount();
                    payment.totalpoints = tran.getmember().getPreviousPoints();
                    payment.hasMember = tran.getmember().getwid() != 0;
                    payment.ShowDialog();

                    if (payment.changeupdated)
                    {
                        tran.setpayments(payment.paymentdata);
                    }

                    if (tran.get_productlist().get_productlist().Count <= 0)
                    {
                        mode = 1;
                        isdetected = true; break;
                    }

                    bool ispaymentdone = payment.transactiondone;
                    decimal total_amount_due = tran.get_productlist().get_totalamount();
                    decimal total_amount_paid = tran.getpayments().get_totalamount();

                    bool istransactiondone = false;
                    //LESTER
                    //if (total_amount_due < -0.1)
                    //{
                    //    fncFilter.alert(cls_globalvariables.warning_transaction_invalid);
                    //    istransactiondone = false;
                    //}
                    //else 
                    if (ispaymentdone && total_amount_due <= (total_amount_paid))
                    {
                        istransactiondone = true;
                    }
                    else if (ispaymentdone && total_amount_due > (total_amount_paid) && tran.getcustomer().getwid() > 0)
                    {
                        if (MessageBox.Show(cls_globalvariables.confirm_customer_debt, "Confirm Box",
                                MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            istransactiondone = false;
                        }
                        else
                        {
                            bool permcheck_debt = false;
                            if (fncFilter.check_permission_debt(this.cur_cashier.getpermission()))
                            {
                                permcheck_debt = true;
                            }
                            else
                                permcheck_debt = isInput_permission_code(fncFilter.get_permission_debt());

                            if (!permcheck_debt)
                                istransactiondone = false;

                            istransactiondone = true;
                            tran.getpayments().set_dept(total_amount_due - total_amount_paid);
                            LOGS.LOG_PRINT("Customer Transacts with Debt: " + tran.getcustomer().getfullname() + " "
                                + (total_amount_due - total_amount_paid).ToString());
                        }
                    }
                    else if (ispaymentdone && total_amount_due > (total_amount_paid))
                    {
                        fncFilter.alert(cls_globalvariables.warning_lack_of_payment);
                        istransactiondone = false;
                    }
                    int temp = 0;
                    if (istransactiondone)
                    {
                        //save transaction to db
                        tran.setdatetime(mySQLFunc.DateTimeNow()); // IMPT!
                        //Thread mythread = new Thread(() => this.save_transaction_thread(tran));
                        //mythread.Start();
                        //if (cls_globalvariables.testmode_v)
                        //    while (mythread.IsAlive) { }
                        LOGS.LOG_PRINT("[F8] AMOUNT DUE: " + total_amount_due + " CASH: " + payment.paymentdata.get_cash());

                        //print receipt
                        for (int x = 0; x < cls_globalvariables.ORPrintCount_v + (temp == -1 ? 1 : 0); x++)
                            fncHardware.print_receipt(tran, false, false);

                        frmposmainext.UpdateTenderChange(tran);
                        frmChange_c changeform = new frmChange_c();
                        changeform.StartPosition = FormStartPosition.Manual;
                        changeform.Location = new Point(
                            Screen.PrimaryScreen.WorkingArea.Width / 2 - changeform.Size.Width / 2,
                            100);
                        changeform.changeamount = tran.get_changeamount().ToString("N2");
                        changeform.tran = tran;
                        changeform.ShowDialog();
                        istransactiondone = changeform.isTransactionDone;
                    }
                    if (istransactiondone)
                    {
                        this.ctrlCustDisp.refresh_display_payment();

                        if (tran.getpayments().get_dept() > 0)
                        {
                            //print receipt copy
                            fncHardware.print_receipt(tran, false, false);
                        }
                        remove_transaction();

                        LOGS.LOG_PRINT("Transaction Complete: " + tran.getORnumber());

                        if (fncHardware.PulloutCashCollection())
                        {
                            MessageBox.Show("Cash amount already exceeds. Please remove the money.", "Cash Collection Warning");
                            LOGS.LOG_PRINT("Cash Collection Warning");
                        }

                        //After successful payment, create new payment if no other open transactions
                        if (Trans.Count == 0)
                            this.create_new_invoice();
                    }

                    mode = 1;
                    frmposmainext.AfterTran();
                    if (cur_trans_index != -1)
                        frmposmainext.UpdateDGV(Trans[cur_trans_index]);

                    isdetected = true;
                    break;

                case Keys.F9: if (btnF9.Enabled == false) return true;  //retail
                    tran = this.get_curtrans();
                    if (tran == null) return true;

                    bool permcheck_retail = false;
                    if (fncFilter.check_permission_retail(this.cur_cashier.getpermission()))
                    {
                        permcheck_retail = true;
                    }
                    else
                        permcheck_retail = isInput_permission_code(fncFilter.get_permission_retail());

                    if (permcheck_retail)
                    {
                        frmRetail retailform = new frmRetail();
                        retailform.ShowDialog();

                        decimal price = retailform.prodprice;
                        decimal qty = retailform.quantity;
                        string type = retailform.type;

                        if (type == "ServiceCharge")
                        {
                            tran.get_productlist().add_product(new cls_product(price, 1, 1));
                        }
                        else
                        {
                            if (price != 0 && qty != 0)
                                lastaddedrownumber = tran.get_productlist().add_product(new cls_product(price, 0, qty));
                            LOGS.LOG_PRINT("[F9]Retail: QTY (" + qty + ") PRICE (" + price + ") ");
                        }
                    }

                    refresh_productlist_data(tran);
                    //frmposmainext.UpdateDGV(tran);
                    isdetected = true; break;
                case Keys.F10: if (btnF10.Enabled == false) return true; //switch package
                    tran = this.get_curtrans();
                    if (tran == null) return true;

                    int f10row_index = this.ctrlproductgridview.get_currentrow().Index;
                    cls_product f10prod = tran.get_productlist().get_product(f10row_index);
                    int retail_package = f10prod.getRetail_or_pack();
                    if (retail_package == 0 && f10prod.getIsPackage())
                        tran.get_productlist().set_retail_or_package(f10row_index, 1);
                    else if (retail_package == 1)
                        tran.get_productlist().set_retail_or_package(f10row_index, 0);

                    refresh_productlist_data(tran);
                    e.Handled = true;
                    isdetected = true; break;
                case Keys.F11: if (btnF11.Enabled == false) return true; //product adjust or discount
                    tran = this.get_curtrans();

                    prodWid = tran.get_productlist().get_product(ctrlproductgridview.get_currentrow().Index).getWid();
                    if (prodWid == 1 || prodWid == 2)
                    {
                        fncFilter.alert(@"Not allowed!");
                        isdetected = true; break;
                    }

                    if (tran == null) return true;

                    if (tran.get_productlist().get_discount_percentage() != 0)
                    {
                        fncFilter.alert(@"Please clear the transaction discounts first.");
                        isdetected = true; break;
                    }

                    bool permcheck_discountproduct = false;
                    if (fncFilter.check_permission_discount(this.cur_cashier.getpermission()))
                        permcheck_discountproduct = true;
                    else
                    {
                        //permcheck_discountproduct = isInput_permission_code(fncFilter.get_permission_discount());
                        frmPermissionCode frmpermcode = new frmPermissionCode();
                        frmpermcode.permission_needed = fncFilter.get_permission_discount();
                        frmpermcode.ShowDialog();
                        permcheck_discountproduct = frmpermcode.permcode;
                        permissiongiver.setcls_user_by_wid(Convert.ToInt32(frmpermcode.permissionwid), false);
                        tran.set_permissiongiver(permissiongiver);
                    }

                    if (permcheck_discountproduct)
                    {
                        int row_index_discount = this.ctrlproductgridview.get_currentrow().Index;
                        cls_product prod_discount = tran.get_productlist().get_product(row_index_discount);

                        decimal cur_prodadjust = prod_discount.getAdjust();
                        decimal cur_proddiscount = prod_discount.getDiscount();
                        decimal cur_prodprice = prod_discount.getPrice();

                        frmProductAdjust frmprodadjust = new frmProductAdjust();
                        if (tran.getcustomer().getwid() == 0)
                        {
                            frmprodadjust.Width = 500;
                        }
                        else
                        {
                            frmprodadjust.orig_pricea = prod_discount.getPrice("A");
                            frmprodadjust.orig_priceb = prod_discount.getPrice("B");
                            frmprodadjust.orig_pricec = prod_discount.getPrice("C");
                            frmprodadjust.orig_priced = prod_discount.getPrice("D");
                            frmprodadjust.orig_pricee = prod_discount.getPrice("E");
                        }
                        frmprodadjust.orig_price = prod_discount.getOrigPrice();
                        frmprodadjust.disclist = prod_discount.getProductDiscountList();
                        frmprodadjust.productname = prod_discount.getProductName();
                        frmprodadjust.new_price = cur_prodprice;
                        //frmprodadjust.new_adjust = cur_prodadjust;
                        //frmprodadjust.new_discount = cur_proddiscount;
                        frmprodadjust.ShowDialog();

                        cur_prodadjust = frmprodadjust.new_adjust;
                        cur_proddiscount = frmprodadjust.new_discount;

                        if (frmprodadjust.iscomplete)
                        {
                            if (frmprodadjust.disc.get_wid() != 0)
                            {
                                prod_discount.getProductDiscountList().activateDiscount_using_wid(frmprodadjust.disc.get_wid(), 1 - cur_proddiscount, true);
                                refresh_productlist_data(tran);
                                tran.get_productlist().sync_product_row(row_index_discount);
                                LOGS.LOG_PRINT("[F11]Product Custom Discount (" + frmprodadjust.disc.get_name() + "(" + frmprodadjust.disc.get_value() + "%)): " +
                                    frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                    " BY " + tran.get_permissiongiver_fullname());
                            }
                            else if (cur_prodadjust != 0)
                            {
                                prod_discount.getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_adjusttype, cur_prodadjust, false);
                                refresh_productlist_data(tran);
                                tran.get_productlist().sync_product_row(row_index_discount);
                                LOGS.LOG_PRINT("[F11]Product Adjusted (" + (cur_prodadjust * 100) + "): " + frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                    " BY " + tran.get_permissiongiver_fullname());
                            }
                            else if (cur_proddiscount != 0)
                            {
                                prod_discount.getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_discounttype, 1 - cur_proddiscount, true);
                                refresh_productlist_data(tran);
                                tran.get_productlist().sync_product_row(row_index_discount);
                                LOGS.LOG_PRINT("[F11]Product Discounted (" + (cur_proddiscount * 100) + "%): " + frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                    " BY " + tran.get_permissiongiver_fullname());
                            }
                            else
                            {
                                refresh_productlist_data(tran);
                                tran.get_productlist().sync_product_row(row_index_discount);
                                LOGS.LOG_PRINT("[F11]Product Adjust/Discount Removed: " + frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                       " BY " + tran.get_permissiongiver_fullname());
                            }
                            lastaddedrownumber = row_index_discount;
                        }
                    }

                    //frmposmainext.UpdateDGV(tran);
                    isdetected = true; break;

                case Keys.F12: if (btnF12.Enabled == false) return true; // open menu
                    LOGS.LOG_PRINT("[F12] Menu Opened");

                    frmMenu menuform = new frmMenu();
                    if (btnF1.Enabled == false)
                        menuform.F1flag = true;
                    menuform.ShowDialog();

                    string cmd = menuform.commandentered;
                    switch (cmd)
                    {
                        case "F1": //inventory
                            tran = this.get_curtrans();
                            if (tran != null)
                            {
                                MessageBox.Show("Please Void Current Transaction First! (F6 - Void)");
                                break;
                            }
                            if (cls_globalvariables.posdautoswitch_v == "1")
                            {
                                frmInventory_posd invform_posd = new frmInventory_posd();
                                invform_posd.cur_permissions = this.cur_cashier.getpermission();
                                invform_posd.ShowDialog();
                                string invposd_cmd = invform_posd.commandentered;
                                DateTime datetime_posd_d = zreadFunc.getZreadDate(invform_posd.datetime_d);
                                DateTime datetimeTO_posd_d = zreadFunc.getZreadDate(invform_posd.datetimeTO_d);
                                switch (invposd_cmd)
                                {
                                    case "F1": posdclass.print_reading_posd(datetime_posd_d, 1, cur_cashier.getwid()); break;
                                    case "F2":
                                        Print_Date_Ranged_Zreadposd(datetime_posd_d, datetimeTO_posd_d);
                                        break;
                                    case "H":
                                        if (cls_globalvariables.posddisableswitch_v == "1")
                                            break;
                                        frmInventory invform = new frmInventory();
                                        invform.cur_permissions = this.cur_cashier.getpermission();
                                        invform.ShowDialog();
                                        string inv_cmd = invform.commandentered;
                                        DateTime datetime_d = zreadFunc.getZreadDate(invform.datetime_d);
                                        DateTime datetimeTO_d = zreadFunc.getZreadDate(invform.datetimeTO_d);
                                        switch (inv_cmd)
                                        {
                                            case "F1":
                                                LOGS.LOG_PRINT("[F1] Open Drawer");
                                                RawPrinterHelper.OpenCashDrawer(false);
                                                frmCashDenomination cashcheckform = new frmCashDenomination();
                                                cashcheckform.cash_bills = new cls_bills();
                                                cashcheckform.ShowDialog();
                                                cls_bills end_bills = cashcheckform.cash_bills;
                                                end_bills.set_type(3);
                                                end_bills.save_cashdenomination(this.cur_cashier);

                                                LOGS.LOG_PRINT("[F1] Print X-Reading: " + datetime_d.ToString());
                                                fncHardware.print_xread(datetime_d, cur_cashier.getwid());
                                                break;
                                            case "F2":
                                                LOGS.LOG_PRINT("[F2] Print Z-Reading: " + datetime_d.ToString());
                                                Print_Date_Ranged_Zread(datetime_d, datetimeTO_d);
                                                break;
                                            case "none": break;
                                        }
                                        break;
                                    case "none": break;
                                }
                            }
                            else
                            {
                                frmInventory invform = new frmInventory();
                                invform.cur_permissions = this.cur_cashier.getpermission();
                                invform.ShowDialog();
                                string inv_cmd = invform.commandentered;
                                DateTime datetime_d = zreadFunc.getZreadDate(invform.datetime_d);
                                DateTime datetimeTO_d = zreadFunc.getZreadDate(invform.datetimeTO_d);
                                switch (inv_cmd)
                                {
                                    case "F1":
                                        LOGS.LOG_PRINT("[F1] Open Drawer");

                                        RawPrinterHelper.OpenCashDrawer(false);

                                        frmCashDenomination cashcheckform = new frmCashDenomination();
                                        cashcheckform.cash_bills = new cls_bills();
                                        cashcheckform.ShowDialog();
                                        cls_bills end_bills = cashcheckform.cash_bills;
                                        end_bills.set_type(3);
                                        end_bills.save_cashdenomination(this.cur_cashier);

                                        LOGS.LOG_PRINT("[F1] Print X-Reading: " + datetime_d.ToString());
                                        fncHardware.print_xread(datetime_d, cur_cashier.getwid());
                                        break;
                                    case "F2":
                                        LOGS.LOG_PRINT("[F2] Print Z-Reading: " + datetime_d.ToString());
                                        Print_Date_Ranged_Zread(datetime_d, datetimeTO_d);
                                        break;
                                    case "F4":
                                        if (cls_globalvariables.TermAcct_v == "1")
                                        {
                                            LOGS.LOG_PRINT("[F4] Print Terminal Accountability");
                                            fncHardware.print_zread(1, datetime_d, datetimeTO_d, 0);
                                        }
                                        break;
                                    case "H":
                                        if (cls_globalvariables.posddisableswitch_v == "1")
                                            break;
                                        frmInventory_posd invform_posd = new frmInventory_posd();
                                        invform_posd.cur_permissions = this.cur_cashier.getpermission();
                                        invform_posd.ShowDialog();
                                        string invposd_cmd = invform_posd.commandentered;
                                        DateTime datetime_posd_d = zreadFunc.getZreadDate(invform_posd.datetime_d);
                                        DateTime datetimeTO_posd_d = zreadFunc.getZreadDate(invform_posd.datetimeTO_d);
                                        Console.WriteLine(datetime_posd_d.ToString("MM/dd/yyyy HH:mm:ss"));
                                        switch (invposd_cmd)
                                        {
                                            case "F1": posdclass.print_reading_posd(datetime_posd_d, 1, cur_cashier.getwid()); break;
                                            case "F2":
                                                Print_Date_Ranged_Zreadposd(datetime_posd_d, datetimeTO_posd_d);
                                                break;
                                            case "none": break;
                                        }
                                        break;
                                    case "none": break;
                                }
                            }
                            break;
                        case "F2": //open drawer

                            if (check_permission("opendrawer"))
                                RawPrinterHelper.OpenCashDrawer(false);

                            break;
                        case "F3":
                            if (cls_globalvariables.posdautoxz_v == "1")
                                break;

                            tran = this.get_curtrans();
                            if (tran != null)
                            {
                                MessageBox.Show("Please Void Current Transaction First!");
                                break;
                            }

                            frmTerminalReadings TR = new frmTerminalReadings();
                            TR.ShowDialog();

                            break;
                        case "F4": //reprint receipt

                            string or_num = "";
                            frmReprintReceipt reprintfrm = new frmReprintReceipt();
                            frmORPrintPreview frmorprintpreview = new frmORPrintPreview();

                            tran = this.get_curtrans();
                            if (tran != null)
                            {
                                reprintfrm.currenttrans_ornumber = tran.getORnumber();
                                frmorprintpreview.currenttrans_ornumber = tran.getORnumber();
                            }

                            if (cls_globalvariables.PreviewOR_v)
                            {
                                frmorprintpreview.cur_permissions = this.cur_cashier.getpermission();
                                frmorprintpreview.ShowDialog();
                                or_num = frmorprintpreview.or_number;
                            }
                            else
                            {
                                reprintfrm.cur_permissions = this.cur_cashier.getpermission();
                                reprintfrm.ShowDialog();
                                or_num = reprintfrm.or_number;
                            }

                            if (or_num.Length <= 0)
                                break;

                            frmLoad loadForm = new frmLoad("Loading Transaction Data", "Loading Screen");
                            loadForm.BackgroundWorker.DoWork += (sender, e1) =>
                            {
                                if (cls_globalfunc.isReceiptInTransList(Trans, or_num))
                                    return;

                                cls_POSTransaction temp_tran = new cls_POSTransaction();
                                temp_tran.set_transaction_by_ornumber(or_num, reprintfrm.is_switch_posd);

                                if ((reprintfrm.is_switch_posd) && (temp_tran.getWid() == 0))
                                    temp_tran.set_transaction_by_ornumber(or_num, false);

                                if (temp_tran.getWid() == 0)
                                {
                                    fncFilter.alert(cls_globalvariables.warning_ornumber_invalid);
                                    return;
                                }
                                fncHardware.print_receipt(temp_tran, true, false);
                            };
                            loadForm.ShowDialog();
                            break;
                        case "F5": //pickup cash
                            if (check_permission("opendrawer"))
                            {
                                RawPrinterHelper.OpenCashDrawer(false);
                                frmCashDenomination cashform = new frmCashDenomination();
                                cashform.cash_bills = new cls_bills();
                                cashform.ShowDialog();
                                cashform.cash_bills.set_type(2);
                                cashform.cash_bills.save_cashdenomination(this.cur_cashier);

                                LOGS.LOG_PRINT("[F5] Print Pickup Cash");
                                fncHardware.print_pickupcash(DateTime.Now, cur_cashier.getwid());
                            }
                            break;
                        case "F6": //discount
                            LOGS.LOG_PRINT("[F6] Transaction Discount/Adjust");
                            tran = this.get_curtrans();
                            if ((tran == null) || (tran.get_productlist().get_totalqty() == 0))
                            {
                                fncFilter.alert("No Products to Adjust!");
                                return true;
                            }
                            bool permcheck_discounttransaction = false;
                            if (fncFilter.check_permission_discount(this.cur_cashier.getpermission()))
                                permcheck_discounttransaction = true;
                            else
                            {
                                //permcheck_discounttransaction = isInput_permission_code(fncFilter.get_permission_discount());
                                frmPermissionCode frmpermcode = new frmPermissionCode();
                                frmpermcode.permission_needed = fncFilter.get_permission_discount();
                                frmpermcode.ShowDialog();
                                permcheck_discounttransaction = frmpermcode.permcode;
                                permissiongiver.setcls_user_by_wid(Convert.ToInt32(frmpermcode.permissionwid), false);
                                tran.set_permissiongiver(permissiongiver);
                            }

                            if (permcheck_discounttransaction)
                            {
                                frmTransactionAdjust transAdjust = new frmTransactionAdjust();
                                transAdjust.orig_price = tran.get_productlist().get_totalamount_no_head_discount();
                                transAdjust.disclist = tran.get_productlist().getTransDisc();
                                transAdjust.new_price = tran.get_productlist().get_totalamount();
                                transAdjust.ShowDialog();


                                if (!transAdjust.iscomplete)
                                    break;

                                cls_discountlist transdisc = tran.get_productlist().getTransDisc();
                                if (transAdjust.disc.get_wid() != 0)
                                {
                                    transdisc.activateDiscount_using_wid(transAdjust.disc.get_wid(), 1 - transAdjust.new_discount, true);
                                    refresh_productlist_data(tran);
                                    LOGS.LOG_PRINT("[F12][F6]Transaction Custom Discount (" + transAdjust.disc.get_name() + " (" + (transAdjust.disc.get_value() * 100) + "%)): OR: "
                                        + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                        " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                                }
                                else if (transAdjust.new_adjust != 0)
                                {
                                    transdisc.appendDiscount(cls_globalvariables.dchead_adjusttype, transAdjust.new_adjust, false);
                                    refresh_productlist_data(tran);
                                    LOGS.LOG_PRINT("[F12][F6]Transaction Adjust (" + transAdjust.new_adjust + ")): OR: "
                                        + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                        " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                                }
                                else if (transAdjust.new_discount != 0)
                                {
                                    transdisc.appendDiscount(cls_globalvariables.dchead_discounttype, 1 - transAdjust.new_discount, true);
                                    refresh_productlist_data(tran);
                                    LOGS.LOG_PRINT("[F12][F6]Transaction Discount (" + (transAdjust.new_discount * 100) + "%)): OR: "
                                        + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                        " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                                }
                                else
                                {
                                    refresh_productlist_data(tran);
                                    LOGS.LOG_PRINT("[F12][F6]Transaction Adjust/Discount Removed: OR: "
                                        + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                        " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                                }
                            }

                            break;
                        case "F7": // Senior
                            tran = this.get_curtrans();
                            if (tran == null) return true;

                            if (check_permission("senior"))
                            {
                                frmSenior seniorform = new frmSenior();
                                seniorform.senior = tran.getsenior();
                                seniorform.ShowDialog();
                                tran.setsenior(seniorform.senior);
                            }
                            refresh_productlist_data(tran);
                            break;
                        case "F8": // Member
                            tran = this.get_curtrans();
                            if (tran == null) return true;
                            frmMember memberform = new frmMember();
                            memberform.member = tran.getmember().ShallowCopy();
                            memberform.ShowDialog();

                            if (memberform.member.getwid() != 0)
                                LOGS.LOG_PRINT("[F8] Set Member: " + memberform.member.getfullname() + ", "
                                    + memberform.member.get_memberrate_name());
                            else
                                LOGS.LOG_PRINT("[F8] Removed Member");

                            tran.setmember(memberform.member);
                            decimal dcpercent = tran.getmember().get_member_discount_amount(tran.get_productlist().get_totalamount());
                            tran.get_productlist().append_adjustdiscount_all(0, dcpercent);

                            tran.get_productlist().getTransDisc().setMember(memberform.member, 0, 0, false);
                            refresh_productlist_data(tran);
                            break;
                        case "F9": // Non-Vat

                            tran = this.get_curtrans();
                            if (tran == null) return true;

                            if (check_permission("nonvat"))
                            {
                                if (tran.get_productlist().get_isnonvat())
                                    LOGS.LOG_PRINT("[F9] NonVat Transaction Deactivated");
                                else
                                    LOGS.LOG_PRINT("[F9] NonVat Transaction Activated");
                                tran.get_productlist().set_isnonvat(!tran.get_productlist().get_isnonvat());

                                frmNonVatInfo nonvattrans = new frmNonVatInfo();
                                nonvattrans.nonvat = tran.getnonvat();
                                nonvattrans.ShowDialog();

                                tran.setnonvat(nonvattrans.nonvat);
                            }

                            refresh_productlist_data(tran);

                            break;
                        case "F10":
                            LOGS.LOG_PRINT("[F10] View Previous Transaction");
                            this.view_previoustransaction();
                            break;
                        case "F11":
                            LOGS.LOG_PRINT("[F11] Create New Invoice");
                            this.create_new_invoice();
                            break;
                        case "F12":
                            LOGS.LOG_PRINT("[F12] View Next Transaction");
                            this.view_nexttransaction();
                            break;
                        case "O":
                            LOGS.LOG_PRINT("[O] Old Data Form");
                            frmOldData olddataform = new frmOldData();
                            olddataform.ShowDialog();
                            break;
                        case "S":
                            LOGS.LOG_PRINT("[S] Settings");
                            frmSetting settingform = new frmSetting();
                            settingform.ShowDialog();
                            break;
                        case "0":
                            frmSetting2 settingform2 = new frmSetting2();
                            settingform2.ShowDialog();
                            break;
                        case "L":
                            LOGS.LOG_PRINT("[L] Discount Lists");
                            tran = this.get_curtrans();
                            if (tran == null) return true;

                            frmChooseDiscount dList = new frmChooseDiscount();
                            dList.passProductList(tran.get_productlist());
                            dList.ShowDialog();

                            break;
                        case "M":
                            LOGS.LOG_PRINT("[S] Salesman");
                            tran = this.get_curtrans();
                            if (tran == null) return true;
                            frmLogIn loggedsalesman = new frmLogIn();
                            loggedsalesman.user = "salesman";
                            loggedsalesman.salesman = tran.getsalesman();
                            loggedsalesman.ShowDialog();

                            tran.setchecker(loggedsalesman.salesman);

                            isdetected = true;
                            break;
                        case "Y":
                            frmReadingSummary readSummary = new frmReadingSummary();
                            readSummary.ShowDialog();
                            string readSummary_reply = readSummary.commandentered;
                            if (readSummary_reply == "print")
                            {
                                posdclass.print_reading_summary(readSummary.datetime_from_d, readSummary.datetime_to_d);
                            }
                            break;
                        case "H":
                            tran = this.get_curtrans();
                            if (tran == null) return true;
                            frmSalesmemo salesmemo = new frmSalesmemo();
                            salesmemo.salesheadwid = this.get_curtrans().getWid();
                            salesmemo.ShowDialog();
                            tran.setmemo(salesmemo.txtmemo);
                            lblSalesMemo.Text = salesmemo.txtmemo;
                            break;
                        case "Escape":
                            break;
                    }
                    isdetected = true; break;

                case Keys.ControlKey:
                    gofullscreen = fncfullscreen.ResizeScreen(gofullscreen);
                    break;
            }

            if (isdetected == true)
                this.refresh_all_display(mode);

            this.txtBarcode.Focus();
            return isdetected;
        }
        private bool isInput_permission_code(int permissioncode)
        {
            bool permcheck = false;
            frmPermissionCode frmpermcode = new frmPermissionCode();
            frmpermcode.permission_needed = permissioncode;
            frmpermcode.ShowDialog();
            permcheck = frmpermcode.permcode;

            return permcheck;
        }
        public bool check_permission(string permission)
        {
            bool permcheck = false;
            int permissioncode = 0;
            string endcaseprint = "";

            switch (permission)
            {
                case "opendrawer":
                    permcheck = (fncFilter.check_permission_opendrawer(this.cur_cashier.getpermission()));
                    permissioncode = fncFilter.get_permission_opendrawer();
                    break;
                case "wholesale":
                    permcheck = (fncFilter.check_permission_wholesale(this.cur_cashier.getpermission()));
                    permissioncode = fncFilter.get_permission_wholesale();
                    break;
                case "nonvat":
                    permcheck = (fncFilter.check_permission_nonvat(this.cur_cashier.getpermission()));
                    permissioncode = fncFilter.get_permission_nonvat();
                    break;
                case "senior":
                    permcheck = (fncFilter.check_permission_senior(this.cur_cashier.getpermission()));
                    permissioncode = fncFilter.get_permission_senior();
                    break;
                default:
                    break;
            }

            if (!permcheck)
            {
                if (!isInput_permission_code(permissioncode))
                {
                    return false;
                }
            }
            LOGS.LOG_PRINT(endcaseprint);
            return true;
        }
        public cls_POSTransaction get_curtrans()
        {
            try
            {
                return this.Trans[this.cur_trans_index];
            }
            catch
            {
                return null;
            }
        }
        private bool TransactionHasRefundedItem(string orNumber)
        {
            string selectSql =
                @"SELECT
	                SH.`ornumber`
                FROM
	                `saleshead` AS SH,
                    `salesdetail` AS SD
                WHERE
                    SH.`wid` = SD.`headid`
                    AND SH.`status` = 1
                    AND SH.`show` = 1
                    #AND SD.`` =
	                AND SD.`description` LIKE '%" + orNumber + "%'";
            DataTable resultDt = mySQLFunc.getdb(selectSql);
            if (resultDt == null || resultDt.Rows.Count <= 0)
                return false;
            else
                return true;
        }
        #endregion

        #region Void methods
        public void refresh_all_display(int mode)
        {
            this.ctrlpaymentlabel.refresh_display();
            this.ctrlOther.refresh_display();
            this.ctrlbtnpanel.refresh_display();
            if (mode == 0)
            {
                int row_index = (this.ctrlproductgridview.get_currentrow() != null) ?
                    this.ctrlproductgridview.get_currentrow().Index : -1;
                this.ctrlpaymentlabel.mode_product_display(row_index);
                this.ctrlCustDisp.refresh_display_addproduct(row_index == -1 ? -1 : lastaddedrownumber);
                if (ctrlproductgridview.get_productgrid().Rows.Count == 0)
                    ctrl_CustomerDisplay.initial_display();
            }
            else if (mode == 1)
            {
                this.ctrlpaymentlabel.mode_amt_display();
            }

            cls_POSTransaction tran = this.get_curtrans();
            if (tran == null) return;

            this.lblQty_d.Text = tran.get_productlist().get_totalqty().ToString();
        }
        public void refresh_productlist_data(cls_POSTransaction tran)
        {
            //remove service charge
            for (int i = 0; i < tran.get_productlist().get_productlist().Count; i++)
            {
                if (tran.get_productlist().get_product(i).getWid() == 1)
                    tran.get_productlist().remove_product(i);
            }
            //remove local tax
            for (int i = 0; i < tran.get_productlist().get_productlist().Count; i++)
            {
                if (tran.get_productlist().get_product(i).getWid() == 2)
                    tran.get_productlist().remove_product(i);
            }
            tran.get_productlist().refresh_all_discounts();

            if (cls_globalvariables.ServiceCharge_v > 0)
            {
                decimal nonvatablesale = tran.get_productlist().get_nonvatsale();// get_subtotal_nonvat();
                decimal vatablesale = tran.get_productlist().get_vatablesale();// get_subtotal_vat();
                decimal vat = cls_globalvariables.vat;
                decimal price = (nonvatablesale + (vatablesale / (1 + vat))) * (cls_globalvariables.ServiceCharge_v / 100);
                tran.get_productlist().add_product(new cls_product(price, 1, 1));
            }
            if (cls_globalvariables.LocalTax_v > 0)
            {
                decimal price = tran.get_productlist().get_subtotal_pre_vat() * (cls_globalvariables.LocalTax_v / 100);
                tran.get_productlist().add_product(new cls_product(price, 2, 1));
            }
            if (lastaddedrownumber > -1 && dgvProduct.Rows.Count > lastaddedrownumber)
            {
                int i = 0;
                foreach (DataGridViewColumn column in dgvProduct.Columns)
                {
                    if (column.Visible)
                        break;
                    i++;
                }
                if (i < dgvProduct.Columns.Count)
                    dgvProduct.CurrentCell = dgvProduct[i, lastaddedrownumber];
            }
            frmposmainext.UpdateDGV(tran);
        }
        public void create_new_invoice()
        {
            initial_display();
            cls_POSTransaction tran = new cls_POSTransaction();
            tran.setclerk(this.cur_cashier);
            tran.setchecker(this.cur_checker);
            mySQLClass sqlclass = new mySQLClass();
            sqlclass.create_invoice(tran);
            add_transaction(tran);

            refresh_productlist_data(tran);

            LOGS.LOG_PRINT("Invoice Created: " + tran.getORnumber());
        }
        public void add_transaction(cls_POSTransaction tran)
        {
            this.Trans.Add(tran);
            this.cur_trans_index = this.Trans.Count - 1;

            this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            this.lbltransaction_total.Text = this.Trans.Count.ToString();

            this.display_POStran(tran);
        }
        private void remove_transaction()
        {
            this.Trans.RemoveAt(this.cur_trans_index);

            if (this.Trans.Count <= 0)
            {
                this.initial_display();
                return;
            }

            if (this.Trans.Count > 0 && this.Trans.Count <= this.cur_trans_index)
            {
                this.cur_trans_index = this.Trans.Count - 1;
            }

            this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.dispay_current_POStran();
        }
        private void view_nexttransaction()
        {
            if (this.Trans.Count <= 0)
            {
                this.initial_display();
                return;
            }

            if (this.cur_trans_index + 1 >= this.Trans.Count)
            {
                return;
            }

            this.cur_trans_index += 1;
            this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.dispay_current_POStran();
            frmposmainext.UpdateDGV(Trans[cur_trans_index]);
        }
        private void view_previoustransaction()
        {
            if (this.Trans.Count <= 0)
            {
                this.initial_display();
                return;
            }

            if (this.cur_trans_index <= 0)
            {
                return;
            }

            this.cur_trans_index -= 1;
            this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.dispay_current_POStran();
            frmposmainext.UpdateDGV(Trans[cur_trans_index]);
        }
        private void display_POStran(cls_POSTransaction tran)
        {
            LOGS.LOG_PRINT("CURRENT OR: " + tran.getORnumber());
            this.lblORNumber_d.Text = tran.getORnumber();
            this.lblQty_d.Text = tran.get_productlist().get_totalqty().ToString();

            this.ctrlproductgridview.set_databinding(tran.get_productlist().get_dtproduct());
            this.ctrlpaymentlabel.set_databinding(tran);
            this.ctrlbtnpanel.refresh_display();
            this.ctrlOther.set_databinding(tran);
            this.ctrlCustDisp.set_databinding(tran);

            this.lblORNumber_d.Enabled = true;
            this.lblQty_d.Enabled = true;
            this.txtBarcode.Enabled = true;

            this.txtBarcode.Focus();

        }
        private void dispay_current_POStran()
        {
            cls_POSTransaction tran = this.get_curtrans();
            if (tran != null)
            {
                this.display_POStran(tran);
                return;
            }

            initial_display();
        }
        private void initial_display()
        {
            this.cur_trans_index = -1;

            this.lblORNumber_d.Enabled = false;
            this.lblQty_d.Enabled = false;
            this.txtBarcode.Enabled = false;

            this.lblORNumber_d.Text = "";
            this.lblQty_d.Text = "0";
            this.txtBarcode.Text = "";
            this.lbltransaction_d.Text = "-";
            this.lbltransaction_total.Text = "-";

            this.ctrlbtnpanel.initial_display();
            this.ctrlOther.initial_display();
            this.ctrlpaymentlabel.initial_display();
            this.ctrlproductgridview.initial_display();
            ctrl_CustomerDisplay.initial_display();

            this.lblSalesMemo.Text = "";
        }
        public static int save_transaction_thread(cls_POSTransaction tran)
        {
            mySQLClass sqlclass = new mySQLClass();
            int x = sqlclass.save_transaction(tran);
            if (x == 0)
            {
                cls_posd posdclass = new cls_posd();
                posdclass.save_transaction_posd(tran);
            }
            return x;
        }
        private void tmrCheckPosdSettingTicked()
        {
            try
            {
                var dic = File.ReadAllLines(cls_globalvariables.settingspath)
                         .Select(l => l.Split(new[] { '=' }))
                         .ToDictionary(s => s[0].Trim(), s => s[1].Trim());
                try
                {
                    cls_globalvariables.posd_percent_v = Convert.ToInt32(dic["posdpercent"]);
                    cls_globalvariables.posdminamt_v = Convert.ToInt32(dic["posdminamt"]);
                    cls_globalvariables.posdmaxamt_v = Convert.ToInt32(dic["posdmaxamt"]);
                }
                catch
                {
                    cls_globalvariables.posd_percent_v = 100;
                    cls_globalvariables.posdminamt_v = 0;
                    cls_globalvariables.posdmaxamt_v = 0;
                }
                try { cls_globalvariables.posdautoxz_v = dic["posdautoxz"]; }
                catch { cls_globalvariables.posdautoxz_v = "0"; }
                try { cls_globalvariables.posdautoswitch_v = dic["posdautoswitch"]; }
                catch { cls_globalvariables.posdautoswitch_v = "0"; }
                try { cls_globalvariables.xzdesign_unite_v = dic["xzdesign_unite"]; }
                catch { cls_globalvariables.xzdesign_unite_v = "0"; }
                try { cls_globalvariables.hide_reprintreceipt_v = dic["hide_reprintreceipt"]; }
                catch { cls_globalvariables.hide_reprintreceipt_v = "0"; }
                try { cls_globalvariables.enableprintposdsummary_v = dic["enableprintposdsummary"]; }
                catch { cls_globalvariables.enableprintposdsummary_v = "0"; }
                try { cls_globalvariables.posdreceiptautoswitch_v = dic["posdreceiptautoswitch"]; }
                catch { cls_globalvariables.posdreceiptautoswitch_v = "0"; }
            }
            catch
            {
            }
            set_app_name();
        }
        private void set_app_name()
        {
            string posd_settings = cls_globalvariables.posd_percent_v + "." + cls_globalvariables.posdminamt_v;
            try
            {
                var dic = File.ReadAllLines("posdcode.txt")
                         .Select(l => l.Split(new[] { '=' }))
                         .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

                StringBuilder sb_posd_settings = new StringBuilder(posd_settings);
                for (int i = 0; i < sb_posd_settings.Length; i++)
                {
                    string replacedvalue = dic[sb_posd_settings[i].ToString()].ToString();

                    sb_posd_settings.Remove(i, 1);
                    sb_posd_settings.Insert(i, replacedvalue);

                    i += replacedvalue.Length - 1;
                }

                this.Text = cls_globalvariables.posname_v + " Version " + cls_globalvariables.version_v + sb_posd_settings.ToString() + " " + cls_globalvariables.Serial_v;
            }
            catch
            {
            }
        }
        #endregion

        public void Delete_Unused_saleshead(cls_POSTransaction tran)
        {
            if (tran == null)
                return;

            string SQLSelect = @"
                SELECT
                    MAX(`ornumber`) AS 'lastornumber'
                FROM
                    `saleshead`
                WHERE
                    `branchid` = " + cls_globalvariables.branchid_v + @" AND
                    `terminalno` = " + cls_globalvariables.terminalno_v;
            DataTable x = mySQLFunc.getdb(SQLSelect);

            long latestSalesheadOrNumber = 0;
            long currentTransactionOr = 0;
            if (x == null || x.Rows.Count == 0)
                return;

            long.TryParse(x.Rows[0]["lastornumber"].ToString(), out latestSalesheadOrNumber);
            long.TryParse(tran.getORnumber(), out currentTransactionOr);
            if (latestSalesheadOrNumber == 0 || latestSalesheadOrNumber > currentTransactionOr)
                return;

            string SQLDelete = @"
                DELETE FROM
                    saleshead 
                WHERE
                    `status`=0 AND `show`=0 AND 
                    `branchid` = " + cls_globalvariables.branchid_v + @" AND
                    `terminalno` = " + cls_globalvariables.terminalno_v + @" AND 
                    `wid` = '" + tran.getWid() + @"'
                ORDER BY `id` DESC
                LIMIT 1";
            mySQLFunc.setdb(SQLDelete);
            LOGS.LOG_PRINT("DELETED1 in saleshead or = " + tran.getORnumber());
        }
        public void Print_Date_Ranged_Zreadposd(DateTime datefrom, DateTime dateto)
        {
            cls_posd posdclass = new cls_posd();
            if (cls_globalvariables.readDateRange_v == 2)
            {
                DateTime datetimeToZread = datefrom;
                while (datetimeToZread.Date <= dateto.Date)
                {
                    posdclass.print_reading_posd(datetimeToZread, 3, 0);
                    datetimeToZread = datetimeToZread.AddDays(1);
                }
            }
            else
            {
                posdclass.print_reading_posd(datefrom, 3, cur_cashier.getwid());
            }
        }
        public void Print_Date_Ranged_Zread(DateTime datefrom, DateTime dateto)
        {
            if (cls_globalvariables.readDateRange_v == 2)
            {
                DateTime datetimeToZread = datefrom;
                while (datetimeToZread.Date <= dateto.Date)
                {
                    fncHardware.print_zread(datetimeToZread, 0);
                    datetimeToZread = datetimeToZread.AddDays(1);
                }
            }
            else
            {
                fncHardware.print_zread(datefrom, cur_cashier.getwid());
            }
        }
        public void Install_Fonts()
        {
            //if (!File.Exists(cls_globalvariables.windowsfont1path))
            //    File.Copy(cls_globalvariables.font1path, cls_globalvariables.windowsfont1path);
            //if (!File.Exists(cls_globalvariables.windowsfont2path))
            //    File.Copy(cls_globalvariables.font1path, cls_globalvariables.windowsfont2path);
        }
    }

    public static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
    }
}
