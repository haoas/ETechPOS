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
using ETech.FormatDesigner;

namespace ETech
{
    public partial class frmReprintReceipt : Form
    {
        public long or_number;
        public long currenttrans_ornumber;
        public List<int> cur_permissions;
        public frmPermissionCode frmpermcode;

        public frmReprintReceipt()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.or_number = 0;
            this.cur_permissions = new List<int>();
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.or_number = 0;
            this.Close();
        }

        public void frmReprintReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.H)
            {
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.or_number = 0;
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
            long or_num = long.Parse(this.txtORNumber_d.Text.Trim());

            if (or_num == 0)
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
                @"SELECT MAX(`ornumber`) as `ornumber` FROM `saleshead`
                WHERE `terminalno` = " + cls_globalvariables.terminalno_v + @"
                    AND `branchid` = " + cls_globalvariables.BranchCode + @"
                    AND `status`=1";

            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtORNumber_d.Focus();
                return;
            }

            long maxtenderedOR = 0;
            long.TryParse(dt.Rows[0]["ornumber"].ToString(), out maxtenderedOR);

            if (maxtenderedOR == this.currenttrans_ornumber)
                maxtenderedOR = maxtenderedOR - 1;

            this.txtORNumber_d.Text = maxtenderedOR.ToString();
            this.txtORNumber_d.Focus();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            txtORNumber_d.AsInteger();
        }

        private bool Check_ReprintReceiptPermission(bool isSwitch)
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
