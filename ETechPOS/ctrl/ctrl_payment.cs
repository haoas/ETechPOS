using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETech.cls;
using System.Windows.Forms;
using System.Data;


namespace ETech.ctrl
{
    public class ctrl_payment
    {
        private GroupBox gbTotal;
        private GroupBox gbTender;
        private GroupBox gbChange;
        private GroupBox gbDiscounts;

        private Label lblTotal;
        private Label lblTender;
        private Label lblRemaining;

        private GroupBox gbProdInfo;
        private Label lblProdBarcode;
        private Label lblProdDesc;
        private Label lblProdPrice;
        private Label lblQuantity;
        private Label lblAmount;
        private Label lblSwitchPack;

        private Label lblRegDisc;
        private Label lblMemDisc;
        private Label lblAdjustments;
        private Label lblPosPromo;

        private cls_POSTransaction POSTrans;

        public ctrl_payment(GroupBox gbTotal_d, GroupBox gbTender_d, GroupBox gbChange_d, GroupBox gbDiscounts_d,
                            Label lblTotal_d, Label lblTender_d, Label lblRemaining_d,
                            GroupBox gbProdInfo_d, Label lblProdBarcode_d, Label lblProdDesc_d, Label lblProdPrice_d,
                            Label lblQuantity_d, Label lblAmount_d, Label lblSwitchPack_d,
                            Label lblRegDisc_d, Label lblMemDisc_d, Label lblAdjustments_d, Label lbl_pospromodiscount_d)
        {
            this.gbTotal = gbTotal_d;
            this.gbTender = gbTender_d;
            this.gbChange = gbChange_d;
            this.gbDiscounts = gbDiscounts_d;

            this.lblTotal = lblTotal_d;
            this.lblTender = lblTender_d;
            this.lblRemaining = lblRemaining_d;

            this.gbProdInfo = gbProdInfo_d;
            this.lblProdBarcode = lblProdBarcode_d;
            this.lblProdDesc = lblProdDesc_d;
            this.lblProdPrice = lblProdPrice_d;
            this.lblQuantity = lblQuantity_d;
            this.lblAmount = lblAmount_d;

            this.lblRegDisc = lblRegDisc_d;
            this.lblMemDisc = lblMemDisc_d;
            this.lblAdjustments = lblAdjustments_d;
            this.lblPosPromo = lbl_pospromodiscount_d;

            this.lblSwitchPack = lblSwitchPack_d;

            this.POSTrans = null;
        }

        public void mode_amt_display()
        {
            this.gbTender.Visible = true;
            this.gbChange.Visible = true;
            this.gbProdInfo.Visible = false;
            this.gbDiscounts.Visible = false;
        }
        public void mode_product_display(int row_index)
        {
            this.gbTender.Visible = false;
            this.gbChange.Visible = false;
            this.gbProdInfo.Visible = true;
            this.gbDiscounts.Visible = true;

            if (this.POSTrans == null || row_index < 0)
                return;

            if (this.POSTrans.get_productlist().get_productlist().Count <= row_index)
                return;

            cls_product prod = this.POSTrans.get_productlist().get_product(row_index);
            this.lblProdBarcode.Text = prod.getBarcode();
            this.lblProdDesc.Text = prod.getProductName_Prefix() + " " + prod.getProductName() + " "
                                + prod.getProductName_Suffix() + " " + prod.getProduct_Mode();

            this.lblProdPrice.Text = prod.getPrice().ToString("N2") + " " + prod.getPrice_Suffix();

            this.lblQuantity.Text = prod.getQty().ToString("G29");

            this.lblSwitchPack.Visible = false;
            if (prod.getRetail_or_pack() == 0 && prod.getIsPackage())
            {
                this.lblSwitchPack.Text = "F10 - Switch to Per-Package (" + prod.getPackQty() + "/Pack)";
                this.lblSwitchPack.Visible = true;
            }
            else if (prod.getRetail_or_pack() == 1)
            {
                this.lblSwitchPack.Text = "F10 - Switch to Single-Item ";
                this.lblSwitchPack.Visible = true;
                this.lblQuantity.Text = prod.getQty().ToString("G29") + " Pack (" + prod.getPackQty() + "/Pack)";
            }


            /*
            if (prod.getPrice() == prod.getOrigPrice())
            {
                this.lblProdPrice.Text = prod.getPrice().ToString("N2") + " " + prod.getPrice_Suffix();
            }
            else
            {
                this.lblProdPrice.Text = prod.getPrice().ToString("N2") + " " + prod.getPrice_Suffix()
                                + " (Orig. Price: " + prod.getOrigPrice().ToString("N2") + " )";
            }
            */

            this.lblAmount.Text = prod.getAmount().ToString("N2");
        }

        public void initial_display()
        {
            this.set_enable(false);
            mode_product_display(0);
            this.POSTrans = null;
            this.lblTotal.Text = "0.00";
            this.lblTender.Text = "0.00";
            this.lblRemaining.Text = "0.00";
            this.lblProdBarcode.Text = "";
            this.lblProdDesc.Text = "";
            this.lblProdPrice.Text = "";
            this.lblSwitchPack.Text = "";
            this.lblQuantity.Text = "";
            this.lblAmount.Text = "";
            this.lblRegDisc.Text = "0.00";
            this.lblMemDisc.Text = "0.00";
            this.lblAdjustments.Text = "0.00";
            this.lblPosPromo.Text = "0.00";
        }

        public void set_databinding(cls_POSTransaction POSTrans_d)
        {
            this.POSTrans = POSTrans_d;
            this.set_enable(true);
            refresh_display();
        }

        public void set_enable(bool b)
        {
            this.gbTotal.Enabled = b;
            this.gbTender.Enabled = b;
            this.gbChange.Enabled = b;
            this.gbProdInfo.Enabled = b;
            this.gbDiscounts.Enabled = b;
        }

        public void refresh_display()
        {
            if (this.POSTrans == null)
                return;

            decimal totalamt = POSTrans.getpayments().get_totalamount();
            decimal totalamtdue = POSTrans.get_productlist().get_totalamount();

            this.lblTotal.Text = this.POSTrans.get_productlist().get_totalamount().ToString("N2");
            this.lblTender.Text = this.POSTrans.getpayments().get_totalamount().ToString("N2");
            this.lblRemaining.Text = (totalamtdue - totalamt).ToString("N2");

            if (this.POSTrans.get_productlist().getTransDisc() == null)
                return;

            this.lblRegDisc.Text = this.POSTrans.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_customdiscounttype).ToString("N2");

            this.lblMemDisc.Text = this.POSTrans.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_membertype).ToString("N2");
            this.lblAdjustments.Text = (this.POSTrans.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_adjusttype)
                                            + this.POSTrans.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_discounttype)).ToString("N2");
            this.lblPosPromo.Text = this.POSTrans.get_productlist().getTransDisc().get_all_discount_amount_of_type(cls_globalvariables.dchead_pospromotype).ToString("N2");
        }
    }
}
