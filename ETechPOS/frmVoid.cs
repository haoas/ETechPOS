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
    public partial class frmVoid : Form
    {
        public long or_number;

        public frmVoid()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.or_number = 0;
        }

        private void done_process()
        {
            long or_num = long.Parse(this.txtORNumber_d.Text);

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

        private void frmVoid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                this.or_number = 0;
                this.Close();
                return;
            }
            else
                return;
        }
        private void frmVoid_Load(object sender, EventArgs e)
        {
            string sSQL = @"SELECT MAX(`ornumber`) as 'ornumber' FROM `saleshead`
                            WHERE `terminalno` = " + cls_globalvariables.terminalno_v + @"
                                AND `branchid` = " + cls_globalvariables.BranchCode + @" AND `status`=1";
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtORNumber_d.Focus();
                return;
            }

            this.txtORNumber_d.Text = dt.Rows[0]["ornumber"].ToString();
            this.txtORNumber_d.Focus();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.or_number = 0;
            this.Close();
        }

        private void txtORNumber_d_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
