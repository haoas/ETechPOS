using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;

namespace ETech.ctrl
{
    public class ctrl_btnpanel
    {
        private List<Button> btnlist;
        private POSMain posmain;

        public ctrl_btnpanel(List<Button> btnlist_d, POSMain posmain_d)
        {
            this.btnlist = btnlist_d;
            this.posmain = posmain_d;

            this.btnlist[0].Click += new System.EventHandler(this.btnF1_Click);
            this.btnlist[1].Click += new System.EventHandler(this.btnF2_Click);
            this.btnlist[2].Click += new System.EventHandler(this.btnF3_Click);
            this.btnlist[3].Click += new System.EventHandler(this.btnF4_Click);
            this.btnlist[4].Click += new System.EventHandler(this.btnF5_Click);
            this.btnlist[5].Click += new System.EventHandler(this.btnF6_Click);
            this.btnlist[6].Click += new System.EventHandler(this.btnF7_Click);
            this.btnlist[7].Click += new System.EventHandler(this.btnF8_Click);
            this.btnlist[8].Click += new System.EventHandler(this.btnF9_Click);
            this.btnlist[9].Click += new System.EventHandler(this.btnF10_Click);
            this.btnlist[10].Click += new System.EventHandler(this.btnF11_Click);
            this.btnlist[11].Click += new System.EventHandler(this.btnF12_Click);
        }


        public void initial_display()
        {
            int[] btnen = { 0, 2, 5, 11 };
            foreach (int i in btnen)
            {
                btnlist[i].Enabled = true;
            }

            int [] btndis = {1,3,4,6,7,8,9,10};
            foreach(int i in btndis)
            {
                btnlist[i].Enabled = false;
            }
        }

        private void set_enable(bool b)
        {
            foreach (Button btn in btnlist)
            {
                btn.Enabled = b;
            }
        }

        public void mode_invoicecreated()
        {
            int[] btnen = { 1, 2, 3, 7, 8, 11 };
            foreach (int i in btnen)
            {
                btnlist[i].Enabled = true;
            }

            int[] btndis = { 0, 4, 6, 9, 10 };
            foreach (int i in btndis)
            {
                btnlist[i].Enabled = false;
            }
        }

        public void mode_product_existed()
        {
            int[] btnen = { 1, 2, 3, 4, 6, 7, 8, 9, 10, 11 };
            foreach (int i in btnen)
            {
                btnlist[i].Enabled = true;
            }

            int[] btndis = { 0 };
            foreach (int i in btndis)
            {
                btnlist[i].Enabled = false;
            }
        }

        public void refresh_display()
        {
            cls_POSTransaction tran = posmain.get_curtrans();
            if (tran == null)
            {
                initial_display();
                return;
            }

            if (tran.get_productlist().get_productlist().Count == 0)
            {
                mode_invoicecreated();
            }
            else
            {
                mode_product_existed();
            }

        }

        private void btnclickfnc(KeyEventArgs e)
        {
            this.posmain.processShortCutKey(e);
        }


        private void btnF1_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F1));
        }
        private void btnF2_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F2));
        }
        private void btnF3_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F3));
        }
        private void btnF4_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F4));
        }
        private void btnF5_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F5));
        }
        private void btnF6_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F6));
        }
        private void btnF7_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F7));
        }
        private void btnF8_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F8));
        }
        private void btnF9_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F9));
        }
        private void btnF10_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F10));
        }
        private void btnF11_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F11));
        }
        private void btnF12_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.F12));
        }

    }
}
