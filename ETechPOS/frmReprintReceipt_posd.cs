using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.cls;

namespace ETechPOS
{
    public partial class frmReprintReceipt_posd : Form
    {
        public string or_number;
        public bool is_switch_posd;

        public frmReprintReceipt_posd()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            fncFilter.set_theme_color(this.btnOK);
            this.or_number = "";
            this.is_switch_posd = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }

        private void frmReprintReceipt_posd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
            {
                done_process();
            }
            else if (e.KeyCode == Keys.H)
            {
                this.or_number = "";
                this.is_switch_posd = true;
                this.Close();
                return;
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.or_number = "";
                this.Close();
                return;
            }
            else
                return;
        }

        private void done_process()
        {
            string or_num = this.txtORNumber_d.Text.Trim();

            if (or_num.Length <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtORNumber_d.Focus();
                this.txtORNumber_d.SelectAll();
                return;
            }

            this.or_number = or_num;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmReprintReceipt_posd_Load(object sender, EventArgs e)
        {
            string sSQL = @"SELECT `ornumber` FROM `saleshead` 
                            WHERE `show` = 1 AND `status` = 1 AND `terminalno` = " + cls_globalvariables.terminalno_v + @"
                                AND `branchid` = " + cls_globalvariables.branchid_v + @"
                            ORDER BY `lastmodifieddate` DESC LIMIT 1";
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtORNumber_d.Focus();
                return;
            }

            this.txtORNumber_d.Text = dt.Rows[0]["ornumber"].ToString();
            this.txtORNumber_d.Focus();
        }

        
    }
}
