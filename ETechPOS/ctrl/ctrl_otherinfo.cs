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
        private ToolStripStatusLabel tsslclerkname;
        private ToolStripStatusLabel tsslcheckername;
        private Label lblmode;
        private ToolStripStatusLabel tsslcustomer;
        private ToolStripStatusLabel tsslcustomermemo;
        private ToolStripStatusLabel tsslmember;
        private ToolStripStatusLabel tsslwarning;

        private cls_POSTransaction POSTrans;

        public ctrl_otherinfo(ToolStripStatusLabel lblclerkname_d, ToolStripStatusLabel tsslcheckername_d, Label lblmode_d,
            ToolStripStatusLabel lblcustomer_d, ToolStripStatusLabel tsslcustomermemo_d, ToolStripStatusLabel tsslmember_d, ToolStripStatusLabel tsslwarning_d)
        {
            this.tsslclerkname = lblclerkname_d;
            this.tsslcheckername = tsslcheckername_d;
            this.lblmode = lblmode_d;
            this.tsslcustomer = lblcustomer_d;
            this.tsslcustomermemo = tsslcustomermemo_d;
            this.tsslmember = tsslmember_d;
            this.tsslwarning = tsslwarning_d;
            this.POSTrans = null;
        }

        public void initial_display()
        {
            this.tsslclerkname.Text = "";
            this.tsslcheckername.Text = "";
            this.lblmode.Text = "";
            this.tsslcustomer.Text = "";
            this.tsslcustomermemo.Text = "";
            this.tsslmember.Text = "";
            this.tsslwarning.Text = "";
        }

        public void set_databinding(cls_POSTransaction tran)
        {
            this.POSTrans = tran;
            refresh_display();
        }

        public void refresh_display()
        {
            if (this.POSTrans == null) return;
            this.tsslclerkname.Text = this.POSTrans.getclerk().getfullname();
            this.tsslcheckername.Text = this.POSTrans.getchecker().getfullname();
            this.lblmode.Text = this.POSTrans.getmode_str();
            this.tsslcustomer.Text = this.POSTrans.getcustomer().getfullname();
            this.tsslcustomermemo.Text = this.POSTrans.getcustomer().getMemo();
            this.tsslmember.Text = this.POSTrans.getmember().getfullname();
        }

        public void set_warning(string warning)
        {
            this.tsslwarning.Text = warning;
        }



    }
}
