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

        private Label lblTotal;
        private Label lblTender;
        private Label lblRemaining;

        private cls_POSTransaction POSTrans;

        public ctrl_payment(GroupBox gbTotal_d, GroupBox gbTender_d, GroupBox gbChange_d,
                            Label lblTotal_d, Label lblTender_d, Label lblRemaining_d)
        {
            this.gbTotal = gbTotal_d;
            this.gbTender = gbTender_d;
            this.gbChange = gbChange_d;
            this.lblTotal = lblTotal_d;
            this.lblTender = lblTender_d;
            this.lblRemaining = lblRemaining_d;
            this.POSTrans = null;
        }

        public void mode_amt_display()
        {
            this.gbTender.Visible = true;
            this.gbChange.Visible = true;
        }
        public void mode_product_display(int row_index)
        {
            this.gbTender.Visible = false;
            this.gbChange.Visible = false;

            if (this.POSTrans == null || row_index < 0)
                return;

            if (this.POSTrans.get_productlist().get_productlist().Count <= row_index)
                return;
        }

        public void initial_display()
        {
            this.set_enable(false);
            mode_product_display(0);
            this.POSTrans = null;
            this.lblTotal.Text = "0.00";
            this.lblTender.Text = "0.00";
            this.lblRemaining.Text = "0.00";
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
        }
    }
}
