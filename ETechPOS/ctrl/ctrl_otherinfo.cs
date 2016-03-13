using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETech.cls;
using System.Windows.Forms;

namespace ETech.ctrl
{
    public class ctrl_otherinfo
    {
        private Panel pnlOtherinfo;

        private ToolStripStatusLabel tsslclerkname;
        private Label lblcheckername;
        private Label lblmode;
        private ToolStripStatusLabel tsslcustomer;
        private Label lblcustomermemo;
        private Label lblmember;
        private Label lblwarning;

        private cls_POSTransaction POSTrans;

        public ctrl_otherinfo(Panel pnl, ToolStripStatusLabel lblclerkname_d, Label lblcheckername_d, Label lblmode_d,
            ToolStripStatusLabel lblcustomer_d, Label lblcustomermemo_d, Label lblmember_d, Label lblwarning_d)
        {
            this.pnlOtherinfo = pnl;
            this.tsslclerkname = lblclerkname_d;
            this.lblcheckername = lblcheckername_d;
            this.lblmode = lblmode_d;
            this.tsslcustomer = lblcustomer_d;
            this.lblcustomermemo = lblcustomermemo_d;
            this.lblmember = lblmember_d;
            this.lblwarning = lblwarning_d;
            this.POSTrans = null;
        }

        public void initial_display()
        {
            this.pnlOtherinfo.Enabled = false;
            this.tsslclerkname.Text = "";
            this.lblcheckername.Text = "";
            this.lblmode.Text = "";
            this.tsslcustomer.Text = "";
            this.lblcustomermemo.Text = "";
            this.lblmember.Text = "";
            this.lblwarning.Text = "";
        }

        public void set_databinding(cls_POSTransaction tran)
        {
            this.POSTrans = tran;
            set_enable(true);
            refresh_display();
        }

        public void set_enable(bool b)
        {
            this.pnlOtherinfo.Enabled = b;
        }

        public void refresh_display()
        {
            if (this.POSTrans == null) return;
            this.tsslclerkname.Text = this.POSTrans.getclerk().getfullname();
            this.lblcheckername.Text = this.POSTrans.getchecker().getfullname();
            this.lblmode.Text = this.POSTrans.getmode_str();
            this.tsslcustomer.Text = this.POSTrans.getcustomer().getfullname();
            this.lblcustomermemo.Text = this.POSTrans.getcustomer().getMemo();
            this.lblmember.Text = this.POSTrans.getmember().getfullname();
        }

        public void set_warning(string warning)
        {
            this.lblwarning.Text = warning;
        }



    }
}
