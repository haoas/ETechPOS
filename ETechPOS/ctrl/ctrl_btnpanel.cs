using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;

namespace ETech.ctrl
{
    public class ButtonKeyEventhandler
    {
        private List<Button> ButtonList;
        private POSMain posmain;

        public ButtonKeyEventhandler(List<Button> buttonlist, POSMain posmain_d)
        {
            this.ButtonList = buttonlist;
            this.posmain = posmain_d;

            //this.ButtonList[0].Click += new System.EventHandler(this.btnEscape_Click);
            this.ButtonList[1].Click += new System.EventHandler(this.btnF1_Click);
            this.ButtonList[2].Click += new System.EventHandler(this.btnF2_Click);
            this.ButtonList[3].Click += new System.EventHandler(this.btnF3_Click);
            this.ButtonList[4].Click += new System.EventHandler(this.btnF4_Click);
            this.ButtonList[5].Click += new System.EventHandler(this.btnF5_Click);
            this.ButtonList[6].Click += new System.EventHandler(this.btnF6_Click);
            this.ButtonList[7].Click += new System.EventHandler(this.btnF7_Click);
            this.ButtonList[8].Click += new System.EventHandler(this.btnF8_Click);
            this.ButtonList[9].Click += new System.EventHandler(this.btnF9_Click);
            this.ButtonList[10].Click += new System.EventHandler(this.btnF10_Click);
            this.ButtonList[11].Click += new System.EventHandler(this.btnF11_Click);
            this.ButtonList[12].Click += new System.EventHandler(this.btnF12_Click);
        }

        private void btnclickfnc(KeyEventArgs e)
        {
            this.posmain.processShortCutKey(e);
        }

        private void btnEscape_Click(object sender, EventArgs e)
        {
            this.btnclickfnc(new KeyEventArgs(Keys.Escape));
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
