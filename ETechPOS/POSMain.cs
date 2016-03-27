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
using ETech.FormatDesigner;
using ETech.Helpers;
using ETech;
using ETech.Views.Forms.Generics;

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
        private ButtonKeyEventhandler ctrlbtnpanel;
        private ctrl_otherinfo ctrlOther;
        private ctrl_CustomerDisplay ctrlCustDisp;
        public DialogResult dialogResult;
        public int row_index;
        private fncFullScreen fncfullscreen;
        private frmPOSMainExt frmposmainext;
        private UserAuthorizationFunction _UserAuthorizationFunction;

        private int lastaddedrownumber = -1;
        private bool isLoadSuccessful = true;

        bool gofullscreen = true;

        int FPage = 0; // 0-Basic 1-Advanced 2-BackOffice

        #endregion

        #region Declaration
        public POSMain()
        {
            InitializeComponent();

            if (0 > FPage || FPage > 2)
                FPage = 0;

            dgvProduct.Standardize();
            dgvProduct.FillColumn(new List<string> { "Description" });

            LogsHelper.ClearTLog();
            isLoadSuccessful = mySQLFunc.initialize_global_variables();

            if (!isLoadSuccessful)
                return;

            //add directories
            cls_globalfunc.CreateIfMissing(@"EOD Reports\");
            cls_globalfunc.CreateIfMissing(@"TEMP\");

            List<Button> btnlist = new List<Button>();
            btnlist.Add(new Button()); btnlist.Add(this.ButtonF01); btnlist.Add(this.ButtonF02);
            btnlist.Add(this.ButtonF03); btnlist.Add(this.ButtonF04); btnlist.Add(this.ButtonF05);
            btnlist.Add(this.ButtonF06); btnlist.Add(this.ButtonF07); btnlist.Add(this.ButtonF08);
            btnlist.Add(this.ButtonF09); btnlist.Add(this.ButtonF10); btnlist.Add(this.ButtonF11);
            btnlist.Add(this.ButtonF12);

            this.Trans = new List<cls_POSTransaction>();
            this.ctrlproductgridview = new ctrl_productgrid(this.dgvProduct);
            this.ctrlpaymentlabel = new ctrl_payment(this.lblTotal, this.lblTendered, this.lblRemaining);
            this.ctrlbtnpanel = new ButtonKeyEventhandler(btnlist, this);
            this.ctrlOther = new ctrl_otherinfo(this.tsslClerk, this.tsslSalesMan,
                                                this.lblMode_d, this.tsslCustomer, this.tsslCustomerMemo,
                                                this.tsslMember, this.tsslWarning);
            this.ctrlCustDisp = new ctrl_CustomerDisplay(this.spcustdisp);

            frmposmainext = new frmPOSMainExt(this);

            myPrinters.SetDefaultPrinter(cls_globalvariables.DefaultPrinter_v);

            this.WindowState = FormWindowState.Maximized;
            //this.lblTerminal_d.Text = cls_globalvariables.terminalno_v;
            this.tsslTerminalNumber.Text = cls_globalvariables.TerminalNumber.ToString();

            this.cur_trans_index = -1;

            initial_display();

            //this.lblORNumber_d.Text = "";
            this.tsslOfficialReceiptNumber.Text = "";

            // cashier log in
            this.cur_cashier = new cls_user();
            frmLogInMain loginmain = new frmLogInMain();
            loginmain.cashier = this.cur_cashier;
            loginmain.ShowDialog();

            if (loginmain.cashier.getsyncid() == 0)
            {
                isLoadSuccessful = false;
                return;
            }
            _UserAuthorizationFunction = new UserAuthorizationFunction();
            _UserAuthorizationFunction.UserAuthorizations = cur_cashier.AuthorizationList;

            LogsHelper.WriteToTLog("LOGGED IN: " + cur_cashier.getfullname());

            this.cur_checker = new cls_user();

            if (cur_cashier.getsyncid() != 0)
            {

                //Does not continue if Database Version is lower
                if (!cls_globalfunc.CheckDatabaseVersion() && cur_cashier.getsyncid() != 1)
                {
                    fncFilter.alert("Incompatible Database Version!");
                    isLoadSuccessful = false;
                    return;
                }

                cls_globalfunc.DeleteUnusedSalesHead();

                //Does not continue if Servertime is not equal to POStime
                string servertime = mySQLFunc.getServerDateTime();
                string systemtime = DateTime.Now.ToString("yyyy-MM-dd hh tt");

                if (systemtime != servertime)
                {
                    fncFilter.alert("Warning!\n\nServer Time: " + servertime + " \nPOS Time: " + systemtime + " \nPlease Adjust POS time\n to Server Time");
                    isLoadSuccessful = false;
                    return;
                }

                //Check branchid
                string sql = @"Select `value` FROM config WHERE particular='branchid'";
                DataTable DT = mySQLFunc.getdb(sql);
                if (DT.Rows.Count == 0)
                {
                    DialogHelper.ShowDialog("Config Table Doesn't have branchid!");
                    isLoadSuccessful = false;
                    return;
                }
                if (cls_globalvariables.Branch.Id != Convert.ToInt64(DT.Rows[0]["value"]))
                {
                    DialogHelper.ShowDialog("Branchid in Config is not the same in settings!");
                    isLoadSuccessful = false;
                    return;
                }

                //autogenerate previous readings
                frmGenerateReadings GR = new frmGenerateReadings();
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

                this.create_new_invoice();
            }
        }
        #endregion

        #region Events
        private void POSMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.isLoadSuccessful)
                return;
            if (this.spcustdisp.IsOpen)
                this.spcustdisp.Close();
            if (DialogHelper.ShowDialog(cls_globalvariables.confirm_logout_voidtran, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Trans.Exists(x => x.get_productlist().get_productlist().Count <= 0))
                    return;
                if (_UserAuthorizationFunction.IsVerifiedAuthorization("VOIDTRANS"))
                {
                    // reverse traverse and delete salesheads
                    for (int i = Trans.Count - 1; i >= 0; i--)
                        Delete_Unused_saleshead(Trans[i]);
                    ctrl_CustomerDisplay.initial_display();
                    LogsHelper.WriteToTLog("[EXIT]LOGGED OUT: " + cur_cashier.getfullname());
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
                cls_globalvariables.Settings.SetValue1(localMacAddress, "POS Mac Address");
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
                if (searchedproduct.getSyncId() == 0)
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

                if (cls_globalvariables.avoidinvalidpprice_v)
                {
                    if ((searchedproduct.getPrice() <= searchedproduct.pprice && !tran.get_productlist().get_iswholesale()) ||
                        (searchedproduct.getWholesalePrice() <= searchedproduct.pprice && tran.get_productlist().get_iswholesale()))
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
                LogsHelper.WriteToTLog("[BARCODE]Product Added: " + searchedproduct.getProductName());
            }
        }
        private void POSMain_Load(object sender, EventArgs e)
        {
            if (!isLoadSuccessful)
            {
                Application.Exit();
                return;
            }

            if (cur_cashier.getsyncid() == 0)
                this.Close();

            lblMode_d.BorderStyle = BorderStyle.None;

            //Text = cls_globalvariables.ApplicationName;
            tsslApplicationVersion.Text = "v1.0.0.0";
            tsslBranchCode.Text = cls_globalvariables.Branch.Id.ToString();
            tsslBranchName.Text = cls_globalvariables.Branch.Name;

            fncFilter.set_theme_color(this);
            fncFilter.set_dgv_inherit(dgvProduct);

            fncfullscreen = new fncFullScreen(this);
            gofullscreen = fncfullscreen.ResizeScreen(gofullscreen);
            this.Top = 0;
            this.Left = 0;
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

        //Do not delete
        private void TimerRefreshSettings_Tick(object sender, EventArgs e)
        {
            TimerRefreshSettings();
        }
        private void tmrUpdateDateTime_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }
        #endregion

        #region Return methods
        public bool processShortCutKey(KeyEventArgs e)
        {
            cls_POSTransaction tran = this.get_curtrans();

            if (tran == null)
                DialogHelper.ShowDialog("NULL TRANS");

            bool isdetected = false;

            /* 0 - product info
             * 1 - payment info
             */
            int mode = 0;

            cls_user UserAuthorizer = new cls_user();
            switch (e.KeyCode)
            {

                case Keys.Escape:
                    this.Close();
                    isdetected = true; break;

                case Keys.Up:
                    this.ctrlproductgridview.select_previous();

                    isdetected = true; break;

                case Keys.Down:
                    this.ctrlproductgridview.select_next();

                    isdetected = true; break;
                case Keys.Enter: // Search Product
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
                            LogsHelper.WriteToTLog("Product Added (" + tempprod.Quantity + "): " + tempprod.getProductName());
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
                        cls_product searchedproduct = new cls_product();
                        searchedproduct.setcls_product_by_syncid(productwid, false);
                        if (searchedproduct.getSyncId() == 0)
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

                        if (cls_globalvariables.avoidinvalidpprice_v)
                        {
                            if ((searchedproduct.getPrice() <= searchedproduct.pprice && !tran.get_productlist().get_iswholesale()) ||
                                (searchedproduct.getWholesalePrice() <= searchedproduct.pprice && tran.get_productlist().get_iswholesale()))
                            {
                                fncFilter.alert(cls_globalvariables.warning_price_invalid);
                                this.txtBarcode.SelectAll();
                                isdetected = true; break;
                            }

                        }
                        //if (searchedproduct != null) {
                        LogsHelper.WriteToTLog("[Product Added: " + searchedproduct.getProductName());
                        lastaddedrownumber = tran.get_productlist().add_product(searchedproduct);
                        refresh_productlist_data(tran);
                        //}

                        txtBarcode.Text = "";
                    }
                    frmposmainext.UpdateDGV(tran);
                    //frmposmainext.UpdateControls(tran);

                    isdetected = true; break;

                case Keys.F1:
                    if (FPage == 0)
                    {
                        //Open Item
                        if (_UserAuthorizationFunction.IsVerifiedAuthorization("OPENITEM"))
                        {
                            frmOpenItem openitemform = new frmOpenItem();
                            openitemform.ShowDialog();

                            if (openitemform.openitem != null)
                                lastaddedrownumber = tran.get_productlist().add_product(openitemform.openitem);
                        }

                        refresh_productlist_data(tran);
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        if (_UserAuthorizationFunction.IsVerifiedAuthorization("OPENDRAWER"))
                            RawPrinterHelper.OpenCashDrawer(false);
                    }
                    else if (FPage == 2)
                    {
                        if (_UserAuthorizationFunction.IsVerifiedAuthorization("MODIFYUSER"))
                        {
                            AddUserForm userform = new AddUserForm(cur_cashier);
                            userform.ShowDialog();
                        }
                    }
                    break;
                //Do not delete
                //DateTime now = mySQLFunc.DateTimeNow();
                //DateTime cutofftimestart = now.Date.AddSeconds(cls_globalvariables.endtime_v);
                //DateTime cutofftimeend = now.Date.AddSeconds(cls_globalvariables.starttime_v);
                //if ((cls_globalvariables.endtime_v > 0) &&
                //    (now >= cutofftimestart) &&
                //    (now < cutofftimeend))
                //{
                //    cls_globalfunc.MSGBXLOG("Cannot Create Invoice, Still in Cut-Off time!");
                //    return true;
                //}

                //DateTime ZreadDateToday = zreadFunc.getZreadDate(now).Date;
                //DateTime maxDateInZread = zreadFunc.getZreadDate(zreadFunc.GetMaxDateTimeFromPosXYZRead()).Date;
                //if (ZreadDateToday <= maxDateInZread)
                //{
                //    cls_globalfunc.MSGBXLOG("POS can no longer Create Invoice since Zread is already created!");
                //    break;
                //}
                //this.create_new_invoice();
                //isdetected = true; break;
                case Keys.F2:
                    if (FPage == 0)
                    {
                        if (!ctrlproductgridview.hasRows())
                            break;

                        bool permcheck_deleteproduct = false;
                        bool permcheck_return = false;

                        if (this.cur_cashier.CheckAuth("REMOVEITEM"))
                            permcheck_deleteproduct = true;
                        if (this.cur_cashier.CheckAuth("REFUNDITEM"))
                            permcheck_return = true;

                        row_index = this.ctrlproductgridview.get_currentrow().Index;
                        cls_product prod = tran.get_productlist().get_product(row_index);
                        string productname = prod.getProductName();
                        if ((prod.getSyncId() == 1) || (prod.getSyncId() == 2)) { isdetected = true; break; }
                        decimal cur_prodqty = prod.Quantity;
                        string cur_prodmemo = prod.memo;

                        frmProductQuantity frmprodqty = new frmProductQuantity();
                        frmprodqty.productid = prod.getSyncId();
                        frmprodqty.productname = productname;
                        frmprodqty.new_qty = cur_prodqty;
                        frmprodqty.delete_auth = permcheck_deleteproduct;
                        frmprodqty.return_auth = permcheck_return;
                        frmprodqty.ShowDialog();

                        if (cur_prodqty == frmprodqty.new_qty)
                            break;

                        cur_prodqty = frmprodqty.new_qty;
                        cur_prodmemo = frmprodqty.salesdetailmemo;

                        LogsHelper.WriteToTLog("Product Changed Qty : " + productname + " " + cur_prodqty);
                        tran.get_productlist().set_quantity(row_index, cur_prodqty);
                        tran.get_productlist().set_salesdetailmemo(row_index, cur_prodmemo);
                        refresh_productlist_data(tran);
                        tran.get_productlist().sync_product_row(row_index);
                        lastaddedrownumber = row_index;

                        frmposmainext.UpdateDGV(tran);
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        if (_UserAuthorizationFunction.IsVerifiedAuthorization("PICKUPCASH"))
                        {
                            RawPrinterHelper.OpenCashDrawer(false);
                            frmCashDenomination cashform = new frmCashDenomination();
                            cashform.cash_bills = new cls_bills();
                            cashform.ShowDialog();
                            cashform.cash_bills.set_type(2);
                            cashform.cash_bills.save_cashdenomination(this.cur_cashier);

                            LogsHelper.WriteToTLog("[F5] Print Pickup Cash");
                            fncHardware.print_pickupcash(DateTime.Now, cur_cashier.getsyncid());
                        }
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;

                //Do not delete
                //tran = this.get_curtrans();
                //if (tran == null) return true;

                //if (!check_permission("wholesale"))
                //{
                //    isdetected = true; break;
                //}

                //if (tran.getcustomer().getwid() == 0 &&
                //    !tran.get_productlist().get_iswholesale())
                //{
                //    frmSearchCustomer custform = new frmSearchCustomer();
                //    custform.ShowDialog();

                //    cls_customer cust = custform.customer;
                //    if (cust.getwid() != 0)
                //    {
                //        tran.setcustomer(cust);
                //        LOGS.LOG_PRINT("[F2][Switch] Set Customer/PricingType: " + cust.getfullname() + " " + cust.getPricingType());
                //    }
                //    else
                //    {
                //        tran.setcustomer(new cls_customer());
                //        tran.get_productlist().set_iswholesale(false);
                //        LOGS.LOG_PRINT("[F2][Switch] Cancelled Customer");
                //        isdetected = true; break;
                //    }
                //    tran.get_productlist().set_iswholesale(true);
                //    tran.get_productlist().set_pricingtype_rate(tran.getcustomer().getPricingType(), tran.getcustomer().getPricingRate());
                //    refresh_productlist_data(tran);
                //    isdetected = true; break;
                //}
                //else if (tran.getcustomer().getwid() != 0)
                //{
                //    if (tran.get_productlist().get_iswholesale())
                //    {
                //        tran.setcustomer(new cls_customer());
                //        tran.get_productlist().set_iswholesale(false);
                //        LOGS.LOG_PRINT("[F2][Switch] Cancelled Customer");
                //    }
                //    else if (!tran.get_productlist().get_iswholesale())
                //    {
                //        tran.get_productlist().set_iswholesale(true);
                //        tran.get_productlist().set_pricingtype_rate(tran.getcustomer().getPricingType(), tran.getcustomer().getPricingRate());
                //        LOGS.LOG_PRINT("[F2][Switch] Set Customer/PricingType: " +
                //            get_curtrans().getcustomer().getfullname() + " " +
                //            get_curtrans().getcustomer().getPricingType());
                //    }
                //}
                //refresh_productlist_data(tran);
                //isdetected = true; break;
                case Keys.F3:
                    if (FPage == 0)
                    {
                        if (!ctrlproductgridview.hasRows())
                            break;

                        long prodWid = tran.get_productlist().get_product(ctrlproductgridview.get_currentrow().Index).getSyncId();
                        if (prodWid == 1 || prodWid == 2)
                        {
                            fncFilter.alert(@"Not allowed!");
                            isdetected = true; break;
                        }

                        bool permcheck_delete = false;
                        if (this.cur_cashier.CheckAuth("REMOVEITEM"))
                        {
                            permcheck_delete = true;
                            if (DialogHelper.ShowDialog(cls_globalvariables.confirm_logout_deleteitem, MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                isdetected = true;
                                break;
                            }
                        }
                        else
                        {
                            UserAuthenticationForm userAuthenticationForm = new UserAuthenticationForm();
                            userAuthenticationForm.UserAuthorization = "REMOVEITEM";
                            userAuthenticationForm.ShowDialog();
                            permcheck_delete = userAuthenticationForm.HasAuthorization;
                            if (permcheck_delete)
                            {
                                UserAuthorizer.setcls_user_by_wid(userAuthenticationForm.User.syncid, false);
                                tran.set_UserAuthorizer(UserAuthorizer);
                            }
                        }

                        int row_index_delete = this.ctrlproductgridview.get_currentrow().Index;
                        if (permcheck_delete)
                        {
                            LogsHelper.WriteToTLog("Product Removed : " + tran.get_productlist().get_product(row_index_delete).getProductName() + " BY " + tran.get_permissiongiver_fullname());
                            tran.get_productlist().remove_product(row_index_delete);
                            lastaddedrownumber = tran.get_productlist().get_productlist().Count - 1;
                        }

                        refresh_productlist_data(tran);

                        tran.get_productlist().sync_product_row(row_index_delete);

                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        if (_UserAuthorizationFunction.IsVerifiedAuthorization("NONVATTRANS"))
                        {
                            if (tran.get_productlist().get_isnonvat())
                                LogsHelper.WriteToTLog("NonVat Transaction Deactivated");
                            else
                                LogsHelper.WriteToTLog("NonVat Transaction Activated");
                            tran.get_productlist().set_isnonvat(!tran.get_productlist().get_isnonvat());

                            frmNonVatInfo nonvattrans = new frmNonVatInfo();
                            nonvattrans.nonvat = tran.getnonvat();
                            nonvattrans.ShowDialog();

                            tran.setnonvat(nonvattrans.nonvat);
                        }
                        refresh_productlist_data(tran);
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F4:
                    if (FPage == 0)
                    {
                        if (DialogHelper.ShowDialog("Are you sure you want to clear the current transaction?", MessageBoxButtons.YesNo) == DialogResult.No)
                            break;

                        Delete_Unused_saleshead(tran);
                        LogsHelper.WriteToTLog("Invoice Cancelled: " + tran.getORnumber().ToString());
                        remove_transaction();
                        create_new_invoice();
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        frmMember memberform = new frmMember();
                        memberform.member = tran.getmember().ShallowCopy();
                        memberform.ShowDialog();

                        if (memberform.member.getSyncId() != 0)
                            LogsHelper.WriteToTLog("[F8] Set Member: " + memberform.member.getfullname() + ", "
                                + memberform.member.get_memberrate_name());
                        else
                            LogsHelper.WriteToTLog("[F8] Removed Member");

                        tran.setmember(memberform.member);
                        decimal dcpercent = tran.getmember().get_member_discount_amount(tran.get_productlist().get_totalamount());
                        tran.get_productlist().append_adjustdiscount_all(0, dcpercent);

                        tran.get_productlist().getTransDisc().setMember(memberform.member, 0, 0, false);
                        refresh_productlist_data(tran);
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F5:
                    if (FPage == 0) // Transaction Discount
                    {
                        if (!ctrlproductgridview.hasRows())
                            break;

                        long prodWid = tran.get_productlist().get_product(ctrlproductgridview.get_currentrow().Index).getSyncId();
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
                        if (this.cur_cashier.CheckAuth("DISCOUNT"))
                            permcheck_discountproduct = true;
                        else
                        {
                            UserAuthenticationForm userAuthenticationForm = new UserAuthenticationForm();
                            userAuthenticationForm.UserAuthorization = "DISCOUNT";
                            userAuthenticationForm.ShowDialog();
                            permcheck_discountproduct = userAuthenticationForm.HasAuthorization;
                            if (permcheck_discountproduct)
                            {
                                UserAuthorizer.setcls_user_by_wid(userAuthenticationForm.User.syncid, false);
                                tran.set_UserAuthorizer(UserAuthorizer);
                            }
                        }

                        if (permcheck_discountproduct)
                        {
                            int row_index_discount = this.ctrlproductgridview.get_currentrow().Index;
                            cls_product prod_discount = tran.get_productlist().get_product(row_index_discount);

                            decimal cur_prodadjust = prod_discount.getAdjust();
                            decimal cur_proddiscount = prod_discount.getDiscount();
                            decimal cur_prodprice = prod_discount.getPrice();

                            frmProductAdjust frmprodadjust = new frmProductAdjust();
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
                                if (frmprodadjust.disc.get_SyncId() != 0)
                                {
                                    prod_discount.getProductDiscountList().activateDiscount_using_wid(frmprodadjust.disc.get_SyncId(), 1 - cur_proddiscount, true);
                                    refresh_productlist_data(tran);
                                    tran.get_productlist().sync_product_row(row_index_discount);
                                    LogsHelper.WriteToTLog("[F11]Product Custom Discount (" + frmprodadjust.disc.get_name() + "(" + frmprodadjust.disc.get_value() + "%)): " +
                                        frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                        " BY " + tran.get_permissiongiver_fullname());
                                }
                                else if (cur_prodadjust != 0)
                                {
                                    prod_discount.getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_adjusttype, cur_prodadjust, false);
                                    refresh_productlist_data(tran);
                                    tran.get_productlist().sync_product_row(row_index_discount);
                                    LogsHelper.WriteToTLog("[F11]Product Adjusted (" + (cur_prodadjust * 100) + "): " + frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                        " BY " + tran.get_permissiongiver_fullname());
                                }
                                else if (cur_proddiscount != 0)
                                {
                                    prod_discount.getProductDiscountList().appendDiscount(cls_globalvariables.dcdetail_discounttype, 1 - cur_proddiscount, true);
                                    refresh_productlist_data(tran);
                                    tran.get_productlist().sync_product_row(row_index_discount);
                                    LogsHelper.WriteToTLog("[F11]Product Discounted (" + (cur_proddiscount * 100) + "%): " + frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                        " BY " + tran.get_permissiongiver_fullname());
                                }
                                else
                                {
                                    refresh_productlist_data(tran);
                                    tran.get_productlist().sync_product_row(row_index_discount);
                                    LogsHelper.WriteToTLog("[F11]Product Adjust/Discount Removed: " + frmprodadjust.productname + " " + prod_discount.getOrigPrice() + " -> " + prod_discount.getPrice() +
                                           " BY " + tran.get_permissiongiver_fullname());
                                }
                                lastaddedrownumber = row_index_discount;
                            }
                        }

                        //frmposmainext.UpdateDGV(tran);
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        if (_UserAuthorizationFunction.IsVerifiedAuthorization("SENIORTRANS"))
                        {
                            frmSenior seniorform = new frmSenior();
                            seniorform.senior = tran.getsenior();
                            seniorform.ShowDialog();
                            tran.setsenior(seniorform.senior);
                        }
                        refresh_productlist_data(tran);
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;

                case Keys.F6:
                    if (FPage == 0)
                    {
                        LogsHelper.WriteToTLog("[F6] Transaction Discount/Adjust");
                        if (tran.get_productlist().get_totalqty() == 0)
                        {
                            fncFilter.alert("No Products to Adjust!");
                            return true;
                        }
                        bool permcheck_discounttransaction = false;
                        if (this.cur_cashier.CheckAuth("DISCOUNT"))
                            permcheck_discounttransaction = true;
                        else
                        {
                            UserAuthenticationForm userAuthenticationForm = new UserAuthenticationForm();
                            userAuthenticationForm.UserAuthorization = "DISCOUNT";
                            userAuthenticationForm.ShowDialog();
                            permcheck_discounttransaction = userAuthenticationForm.HasAuthorization;
                            if (permcheck_discounttransaction)
                            {
                                UserAuthorizer.setcls_user_by_wid(userAuthenticationForm.User.syncid, false);
                                tran.set_UserAuthorizer(UserAuthorizer);
                            }
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
                            if (transAdjust.disc.get_SyncId() != 0)
                            {
                                transdisc.activateDiscount_using_wid(transAdjust.disc.get_SyncId(), 1 - transAdjust.new_discount, true);
                                refresh_productlist_data(tran);
                                LogsHelper.WriteToTLog("[F12][F6]Transaction Custom Discount (" + transAdjust.disc.get_name() + " (" + (transAdjust.disc.get_value() * 100) + "%)): OR: "
                                    + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                    " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                            }
                            else if (transAdjust.new_adjust != 0)
                            {
                                transdisc.appendDiscount(cls_globalvariables.dchead_adjusttype, transAdjust.new_adjust, false);
                                refresh_productlist_data(tran);
                                LogsHelper.WriteToTLog("[F12][F6]Transaction Adjust (" + transAdjust.new_adjust + ")): OR: "
                                    + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                    " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                            }
                            else if (transAdjust.new_discount != 0)
                            {
                                transdisc.appendDiscount(cls_globalvariables.dchead_discounttype, 1 - transAdjust.new_discount, true);
                                refresh_productlist_data(tran);
                                LogsHelper.WriteToTLog("[F12][F6]Transaction Discount (" + (transAdjust.new_discount * 100) + "%)): OR: "
                                    + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                    " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                            }
                            else
                            {
                                refresh_productlist_data(tran);
                                LogsHelper.WriteToTLog("[F12][F6]Transaction Adjust/Discount Removed: OR: "
                                    + tran.getORnumber() + " " + tran.get_productlist().get_totalamount_gross() +
                                    " -> " + tran.get_productlist().get_totalamount() + " BY " + tran.get_permissiongiver_fullname());
                            }
                        }
                    }
                    else if (FPage == 1)
                    {

                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                //bool permcheck_void = false;
                //if (this.get_curtrans().get_productlist().get_productlist().Count <= 0)
                //{
                //    permcheck_void = true;
                //}
                //else if (fncFilter.check_permission_void(this.cur_cashier.getpermission()))
                //{
                //    permcheck_void = true;

                //if (DialogHelper.ShowDialog(cls_globalvariables.confirm_logout_deletetran, MessageBoxButtons.YesNo) == DialogResult.No)
                //    {
                //        isdetected = true; break;
                //    }
                //}
                //else
                //{
                //    //permcheck_void = isInput_permission_code(fncFilter.get_permission_void());
                //    frmPermissionCode frmpermcode = new frmPermissionCode();
                //    frmpermcode.permission_needed = fncFilter.get_permission_void();
                //    frmpermcode.ShowDialog();
                //    permcheck_void = frmpermcode.permcode;
                //    permissiongiver.setcls_user_by_wid(Convert.ToInt32(frmpermcode.permissionwid), false);
                //}

                //if (permcheck_void)
                //{
                //    if (true)
                //    {

                //    }
                //    else
                //    {
                //        frmVoid frmvoid = new frmVoid();
                //        frmvoid.ShowDialog();

                //        long or_num = frmvoid.or_number;

                //        if (or_num == 0)
                //            break;

                //        if (TransactionHasRefundedItem(or_num))
                //        {
                //            fncFilter.alert(cls_globalvariables.warning_refunded_transaction_cannot_be_voided);
                //            break;
                //        }

                //        if (zreadFunc.HasZReadingToday())
                //        {
                //            Int64 maxORinPosxyzread = zreadFunc.get_max_OR_in_posxyzread();
                //            if ((maxORinPosxyzread != 0) && (maxORinPosxyzread >= Convert.ToInt64(or_num)))
                //            {
                //                cls_globalfunc.MSGBXLOG("OR cannot be voided. Z-Reading is already reported.");
                //                break;
                //            }
                //        }

                //        cls_POSTransaction temp_tran = new cls_POSTransaction();
                //        temp_tran.set_transaction_by_ornumber(or_num);
                //        temp_tran.set_permissiongiver(permissiongiver);
                //        if (temp_tran.getWid() == 0)
                //        {
                //            fncFilter.alert(cls_globalvariables.warning_ornumber_invalid);
                //            break;
                //        }
                //        if (temp_tran.getShow() == 0)
                //        {
                //            if (if (DialogHelper.ShowDialog("This OR# is already voided!\nDo you want to reprint?", MessageBoxButtons.YesNo) == DialogResult.Yes))
                //                fncHardware.print_receipt(temp_tran, true, true);
                //            break;
                //        }
                //        if (temp_tran.getmember().MemberButOffline)
                //        {
                //            fncFilter.alert("This OR cannot be voided since Member feature is offline.");
                //            break;
                //        }

                //        fncHardware.void_transaction(temp_tran);
                //        fncHardware.print_receipt(temp_tran, false, true);
                //    }
                //}
                //frmposmainext.AfterTran();

                //isdetected = true; break;

                case Keys.F7:
                    if (FPage == 0)
                    {
                        if (tran.get_productlist().get_productlist().Count == 0)
                            return true;

                        if (cls_globalvariables.RefundMemo_v == 1)
                        {
                            for (int i = 0; i < tran.get_productlist().get_productlist().Count; i++)
                            {
                                if (tran.get_productlist().get_product(i).Quantity < 0)
                                {
                                    frmSalesmemo salesmemo = new frmSalesmemo();
                                    salesmemo.salesheadwid = tran.getSyncId();
                                    salesmemo.ShowDialog();
                                    continue;
                                }
                            }
                        }

                        frmPayment payment = new frmPayment();
                        payment.paymentdata = tran.getpayments().DeepCopy();
                        payment.totalamtdue = tran.get_productlist().get_totalamount();
                        payment.totalpoints = tran.getmember().getPreviousPoints();
                        payment.hasMember = tran.getmember().getSyncId() != 0;
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
                        if (ispaymentdone && total_amount_due <= (total_amount_paid))
                        {
                            istransactiondone = true;
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
                            LogsHelper.WriteToTLog("[F8] AMOUNT DUE: " + total_amount_due + " CASH: " + payment.paymentdata.get_cash());

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

                            remove_transaction();

                            LogsHelper.WriteToTLog("Transaction Complete: " + tran.getORnumber());

                            if (fncHardware.PulloutCashCollection())
                            {
                                DialogHelper.ShowDialog("Cash amount already exceeds. Please remove the money.");
                                LogsHelper.WriteToTLog("Cash Collection Warning");
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
                    }
                    else if (FPage == 1)
                    {
                        long ORNumber = 0;
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
                            frmorprintpreview.CurrentUserAuthlist = this.cur_cashier.AuthorizationList;
                            frmorprintpreview.ShowDialog();
                            ORNumber = frmorprintpreview.or_number;
                        }
                        else
                        {
                            reprintfrm.CurrentUserAuthList = this.cur_cashier.AuthorizationList;
                            reprintfrm.ShowDialog();
                            ORNumber = reprintfrm.or_number;
                        }

                        frmLoad loadForm = new frmLoad("Loading... Please wait", "Loading Screen");
                        loadForm.BackgroundWorker.DoWork += (sender, e1) =>
                        {
                            if (cls_globalfunc.isReceiptInTransList(Trans, ORNumber))
                                return;

                            cls_POSTransaction temp_tran = new cls_POSTransaction();
                            temp_tran.set_transaction_by_ornumber(ORNumber);

                            if (temp_tran.getSyncId() == 0)
                                temp_tran.set_transaction_by_ornumber(ORNumber);

                            if (temp_tran.getSyncId() == 0)
                            {
                                fncFilter.alert(cls_globalvariables.warning_ornumber_invalid);
                                return;
                            }
                            fncHardware.print_receipt(temp_tran, true, false);
                        };
                        loadForm.ShowDialog();
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F8:
                    if (FPage == 0)
                    {
                        frmSalesmemo salesnotes = new frmSalesmemo();
                        salesnotes.salesheadwid = this.get_curtrans().getSyncId();
                        salesnotes.ShowDialog();
                        tran.setmemo(salesnotes.txtmemo);
                        tsslSalesMemo.Text = salesnotes.txtmemo;
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        tran = this.get_curtrans();
                        if (tran != null)
                        {
                            DialogHelper.ShowDialog("Please Void Current Transaction First! (F6 - Void)");
                            break;
                        }

                        frmTerminalReadings TR = new frmTerminalReadings();
                        TR.ShowDialog();
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F9:
                    if (FPage == 0)
                    {
                        LogsHelper.WriteToTLog("[F9] Create New Transaction");
                        this.create_new_invoice();
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        tran = this.get_curtrans();
                        if (tran != null)
                        {
                            DialogHelper.ShowDialog("Please Void Current Transaction First! (F6 - Void)");
                            break;
                        }
                        frmInventory invform = new frmInventory();
                        invform.UserAuthorizationList = this.cur_cashier.AuthorizationList;
                        invform.ShowDialog();
                        string inv_cmd = invform.commandentered;
                        DateTime datetime_d = zreadFunc.getZreadDate(invform.datetime_d);
                        DateTime datetimeTO_d = zreadFunc.getZreadDate(invform.datetimeTO_d);
                        LogsHelper.WriteToTLog("[F1] Open Drawer");

                        RawPrinterHelper.OpenCashDrawer(false);

                        frmCashDenomination cashcheckform = new frmCashDenomination();
                        cashcheckform.cash_bills = new cls_bills();
                        cashcheckform.ShowDialog();
                        cls_bills end_bills = cashcheckform.cash_bills;
                        end_bills.set_type(3);
                        end_bills.save_cashdenomination(this.cur_cashier);
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F10:
                    if (FPage == 0)
                    {
                        LogsHelper.WriteToTLog("[F10] Previous Transaction");
                        this.view_previoustransaction();
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        //tran = this.get_curtrans();
                        if (tran != null)
                        {
                            DialogHelper.ShowDialog("Please Void Current Transaction First! (F6 - Void)");
                            break;
                        }
                        frmInventory invform = new frmInventory();
                        invform.UserAuthorizationList = this.cur_cashier.AuthorizationList;
                        invform.ShowDialog();
                        string inv_cmd = invform.commandentered;
                        DateTime datetime_d = zreadFunc.getZreadDate(invform.datetime_d);
                        DateTime datetimeTO_d = zreadFunc.getZreadDate(invform.datetimeTO_d);

                        LogsHelper.WriteToTLog("[F2] Print Z-Reading: " + datetime_d.ToString());
                        Print_Date_Ranged_Zread(datetime_d, datetimeTO_d);

                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F11:
                    if (FPage == 0)
                    {
                        LogsHelper.WriteToTLog("[F11] Next Transaction");
                        this.view_nexttransaction();
                        isdetected = true;
                    }
                    else if (FPage == 1)
                    {
                        LogsHelper.WriteToTLog("[S] Settings");
                        frmSetting settingform = new frmSetting();
                        settingform.ShowDialog();
                    }
                    else if (FPage == 2)
                    {

                    }
                    break;
                case Keys.F12:
                    LogsHelper.WriteToTLog("[F12] Open Advanced Functions");
                    SwitchFunctionPage();
                    isdetected = true;
                    break;
                //case Keys.ControlKey:
                //    gofullscreen = fncfullscreen.ResizeScreen(gofullscreen);
                //    break;
            }

            if (isdetected == true)
                this.refresh_all_display(mode);

            this.txtBarcode.Focus();
            return isdetected;
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
        private bool TransactionHasRefundedItem(long orNumber)
        {
            string selectSql =
                @"SELECT
	                SH.`ornumber`
                FROM
	                `saleshead` AS SH,
                    `salesdetail` AS SD
                WHERE
                    SH.`SyncId` = SD.`headid`
                    AND SH.`status` = 1
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
            if (mode == 0)
            {
                int row_index = (this.ctrlproductgridview.get_currentrow() != null) ?
                    this.ctrlproductgridview.get_currentrow().Index : -1;
                this.ctrlpaymentlabel.mode_product_display(row_index);
                this.ctrlCustDisp.refresh_display_addproduct(row_index == -1 ? -1 : lastaddedrownumber);
                if (ctrlproductgridview.get_productgrid().Rows.Count == 0)
                    ctrl_CustomerDisplay.initial_display();
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
                if (tran.get_productlist().get_product(i).getSyncId() == 1)
                    tran.get_productlist().remove_product(i);
            }
            //remove local tax
            for (int i = 0; i < tran.get_productlist().get_productlist().Count; i++)
            {
                if (tran.get_productlist().get_product(i).getSyncId() == 2)
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

            LogsHelper.WriteToTLog("Invoice Created: " + tran.getORnumber());
        }
        public void add_transaction(cls_POSTransaction tran)
        {
            this.Trans.Add(tran);
            this.cur_trans_index = this.Trans.Count - 1;

            //this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            //this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.tsslTransactions.Text = (this.cur_trans_index + 1).ToString() + " / " + this.Trans.Count.ToString();

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

            //this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            //this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.tsslTransactions.Text = (this.cur_trans_index + 1).ToString() + " / " + this.Trans.Count.ToString();
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
            //this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            //this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.tsslTransactions.Text = (this.cur_trans_index + 1).ToString() + " / " + this.Trans.Count.ToString();
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
            //this.lbltransaction_d.Text = (this.cur_trans_index + 1).ToString();
            //this.lbltransaction_total.Text = this.Trans.Count.ToString();
            this.tsslTransactions.Text = (this.cur_trans_index + 1).ToString() + " / " + this.Trans.Count.ToString();
            this.dispay_current_POStran();
            frmposmainext.UpdateDGV(Trans[cur_trans_index]);
        }
        private void display_POStran(cls_POSTransaction tran)
        {
            LogsHelper.WriteToTLog("CURRENT OR: " + tran.getORnumber());
            //this.lblORNumber_d.Text = tran.getORnumber().ToString();
            this.tsslOfficialReceiptNumber.Text = tran.getORnumber().ToString();
            this.lblQty_d.Text = tran.get_productlist().get_totalqty().ToString();

            this.ctrlproductgridview.set_databinding(tran.get_productlist().get_dtproduct());
            this.ctrlpaymentlabel.set_databinding(tran);
            this.ctrlOther.set_databinding(tran);
            this.ctrlCustDisp.set_databinding(tran);

            //this.lblORNumber_d.Enabled = true;
            this.tsslOfficialReceiptNumber.Enabled = true;
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

            this.tsslOfficialReceiptNumber.Enabled = false;
            this.lblQty_d.Enabled = false;
            this.txtBarcode.Enabled = false;

            this.tsslOfficialReceiptNumber.Text = "";
            this.lblQty_d.Text = "0";
            this.txtBarcode.Text = "";
            this.tsslTransactions.Text = "";

            this.ctrlOther.initial_display();
            this.ctrlpaymentlabel.initial_display();
            this.ctrlproductgridview.initial_display();
            ctrl_CustomerDisplay.initial_display();

            this.tsslSalesMemo.Text = "";
        }
        public static int save_transaction_thread(cls_POSTransaction tran)
        {
            mySQLClass sqlclass = new mySQLClass();
            int x = sqlclass.save_transaction(tran);
            return x;
        }
        private void TimerRefreshSettings()
        {
            //Dont delete this code
        }

        private void UpdateDateTime()
        {
            tsslDateTime.Text = DateTime.Now.ToString("MMMMMMMMM dd, yyyy hh:mm:ss tt");
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
                    `branchid` = " + cls_globalvariables.Branch.Id + @" AND
                    `terminalno` = " + cls_globalvariables.TerminalNumber;
            DataTable x = mySQLFunc.getdb(SQLSelect);

            long latestSalesheadOrNumber = 0;
            long currentTransactionOr = 0;
            if (x == null || x.Rows.Count == 0)
                return;

            long.TryParse(x.Rows[0]["lastornumber"].ToString(), out latestSalesheadOrNumber);
            currentTransactionOr = tran.getORnumber();
            if (latestSalesheadOrNumber == 0 || latestSalesheadOrNumber > currentTransactionOr)
                return;

            string SQLDelete = @"
                DELETE FROM `saleshead`
                WHERE
                    `status`=0 AND
                    `branchid` = " + cls_globalvariables.Branch.Id + @" AND
                    `terminalno` = " + cls_globalvariables.TerminalNumber + @" AND 
                    `SyncId` = '" + tran.getSyncId() + @"' LIMIT 1";
            mySQLFunc.setdb(SQLDelete);
            LogsHelper.WriteToTLog("DELETED1 in saleshead or = " + tran.getORnumber());
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
                fncHardware.print_zread(datefrom, cur_cashier.getsyncid());
            }
        }

        public void SwitchFunctionPage()
        {
            ButtonF04.Visible = true;
            ButtonF05.Visible = true;
            ButtonF06.Visible = true;
            ButtonF07.Visible = true;
            ButtonF08.Visible = true;
            ButtonF09.Visible = true;
            ButtonF10.Visible = true;
            ButtonF11.Visible = true;
            if (FPage == 0)
            {
                FPage = 1;
                ButtonF01.Text = "[ F1 ]\r\nOPEN\r\nDRAWER";
                ButtonF02.Text = "[ F2 ]\r\nPICKUP\r\nCASH";
                ButtonF03.Text = "[ F3 ]\r\nNONVAT\r\nTRANS";
                ButtonF04.Text = "[ F4 ]\r\nMEMBER\r\nTRANS";
                ButtonF05.Text = "[ F5 ]\r\nSENIOR\r\nTRANS";
                ButtonF06.Text = "[ F6 ]\r\nVOID\r\nTRANS";
                ButtonF07.Text = "[ F7 ]\r\nREPRINT\r\nRECEIPT";
                ButtonF08.Text = "[ F8 ]\r\nOTHER\r\nREADINGS";
                ButtonF09.Text = "[ F9 ]\r\nX\r\nREADING";
                ButtonF10.Text = "[ F10 ]\r\nZ\r\nREADING";
                ButtonF11.Text = "[ F11 ]\r\nMODIFY\r\nSETTINGS";
                ButtonF12.Text = "[ F12 ]\r\nBACKOFFICE\r\nFUNCS";
            }
            else if (FPage == 1)
            {
                FPage = 2;
                ButtonF01.Text = "[ F1 ]\r\nADD\r\nUSER";
                ButtonF02.Text = "[ F2 ]\r\nADD\r\nPRODUCT";
                ButtonF03.Text = "[ F3 ]\r\nADD\r\nMEMBER";
                ButtonF04.Visible = false;
                ButtonF04.Text = "[ F4 ]\r\n \r\n ";
                ButtonF05.Visible = false;
                ButtonF05.Text = "[ F5 ]\r\n \r\n ";
                ButtonF06.Visible = false;
                ButtonF06.Text = "[ F6 ]\r\n \r\n ";
                ButtonF07.Visible = false;
                ButtonF07.Text = "[ F7 ]\r\n \r\n ";
                ButtonF08.Visible = false;
                ButtonF08.Text = "[ F8 ]\r\n \r\n ";
                ButtonF09.Visible = false;
                ButtonF09.Text = "[ F9 ]\r\n \r\n ";
                ButtonF10.Visible = false;
                ButtonF10.Text = "[ F10 ]\r\n \r\n ";
                ButtonF11.Visible = false;
                ButtonF11.Text = "[ F11 ]\r\n \r\n ";
                ButtonF12.Text = "[ F12 ]\r\nBASIC\r\nFUNCS";
            }
            else
            {
                FPage = 0;
                ButtonF01.Text = "[ F1 ]\r\nOPEN\r\nITEM";
                ButtonF02.Text = "[ F2 ]\r\nCHANGE\r\nQUANTITY";
                ButtonF03.Text = "[ F3 ]\r\nREMOVE\r\nITEM";
                ButtonF04.Text = "[ F4 ]\r\nCLEAR\r\nTRANS";
                ButtonF05.Text = "[ F5 ]\r\nITEM\r\nDISCOUNT";
                ButtonF06.Text = "[ F6 ]\r\nTRANS\r\nDISCOUNT";
                ButtonF07.Text = "[ F7 ]\r\nTENDER\r\nPAYMENT";
                ButtonF08.Text = "[ F8 ]\r\nADD\r\nNOTES";
                ButtonF09.Text = "[ F9 ]\r\nNEW\r\nTRANS";
                ButtonF10.Text = "[ F10 ]\r\nPREV\r\nTRANS";
                ButtonF11.Text = "[ F11 ]\r\nNEXT\r\nTRANS";
                ButtonF12.Text = "[ F12 ]\r\nADVANCED\r\nFUNCS";
            }

        }
    }

    public static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
    }
}
