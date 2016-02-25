using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.fnc;

namespace ETech
{
    public partial class frmReprintReceipt : Form
    {
        public string or_number;
        public string currenttrans_ornumber;
        public bool is_switch_posd;
        public List<int> cur_permissions;
        public frmPermissionCode frmpermcode;

        public frmReprintReceipt()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.or_number = "";
            this.cur_permissions = new List<int>();
            this.is_switch_posd = (cls_globalvariables.posdreceiptautoswitch_v == "1");
            this.Text = this.is_switch_posd ? "Reprint Receipt" : "Reprint Receipt_";
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.or_number = "";
            this.Close();
        }

        public void frmReprintReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.H)
            {
                if (cls_globalvariables.posddisableswitch_v == "1")
                    return;
                if (Check_ReprintReceiptPermission(true))
                {
                    ChangePosd();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.or_number = "";
                this.Close();
                return;
            }
            else
                return;
        }

        private bool isInput_permission_code(int permissioncode)
        {
            bool permcheck = false;
            frmpermcode = new frmPermissionCode();
            frmpermcode.permission_needed = permissioncode;
            frmpermcode.ShowDialog();
            permcheck = frmpermcode.permcode;

            return permcheck;
        }

        private void done_process()
        {
            string or_num = this.txtORNumber_d.Text.Trim().Replace("/", "");

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

        public void frmReprintReceipt_Load(object sender, EventArgs e)
        {
            if (!Check_ReprintReceiptPermission(false))
                this.Close();

            string sSQL =
                @"SELECT `ornumber` FROM `saleshead`
                WHERE `terminalno` = " + cls_globalvariables.terminalno_v + @"
                    AND `branchid` = " + cls_globalvariables.branchid_v + @"
                    AND `status`=1
                ORDER BY `ornumber` DESC
                LIMIT 1;";
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtORNumber_d.Focus();
                return;
            }

            string maxtenderedOR = dt.Rows[0]["ornumber"].ToString();
            if (maxtenderedOR == this.currenttrans_ornumber)
                maxtenderedOR = (Convert.ToInt64(maxtenderedOR) - 1).ToString();

            this.txtORNumber_d.Text = maxtenderedOR;
            this.txtORNumber_d.Focus();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void txtORNumber_d_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'h' || e.KeyChar == 'H')
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ChangePosd()
        {
            this.or_number = "";
            this.is_switch_posd = !this.is_switch_posd;
            this.Text = this.is_switch_posd ? "Reprint Receipt" : "Reprint Receipt_";
        }

        private bool Check_ReprintReceiptPermission(bool isSwitch)
        {
            if (isSwitch ? !this.is_switch_posd : this.is_switch_posd)
            {
                bool permcheck_reprintreceipt_posd = false;
                if (fncFilter.check_permission_reprint_posd(this.cur_permissions))
                    permcheck_reprintreceipt_posd = true;
                else
                {
                    permcheck_reprintreceipt_posd = isInput_permission_code(fncFilter.get_permission_reprint_posd());
                }

                return permcheck_reprintreceipt_posd;
            }
            else
            {
                bool permcheck_reprintreceipt = false;
                if (fncFilter.check_permission_reprint(this.cur_permissions))
                    permcheck_reprintreceipt = true;
                else
                {
                    permcheck_reprintreceipt = isInput_permission_code(fncFilter.get_permission_reprint());
                }
                return permcheck_reprintreceipt;
            }
        }
    }
}
