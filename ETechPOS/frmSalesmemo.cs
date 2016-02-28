using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using MySql.Data.MySqlClient;
using ETech.fnc;

namespace ETech
{
    public partial class frmSalesmemo : Form
    {
        public string txtmemo;
        public Int32 salesheadwid;

        public frmSalesmemo()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.txtmemo = "";
        }

        private void done_process()
        {
            string txtmemo = this.txtMemo_d.Text.Trim();

            if (txtmemo.Length <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtMemo_d.Focus();
                this.txtMemo_d.SelectAll();
                return;
            }

            this.txtmemo = txtmemo;

            string sql = @"UPDATE saleshead SET memo='" + MySqlHelper.EscapeString(this.txtmemo)
                + @"' WHERE wid=" + salesheadwid + @" LIMIT 1";
            mySQLFunc.setdb(sql);

            this.Close();
        }

        private void frmRefundmemo_Load(object sender, EventArgs e)
        {
            string sSQL = @"SELECT `memo` FROM `saleshead` 
                            WHERE `wid`= "+ salesheadwid+ @" LIMIT 1";
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtMemo_d.Focus();
                return;
            }

            this.txtMemo_d.Text = dt.Rows[0]["memo"].ToString();
            this.txtMemo_d.Focus();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.txtmemo = "";
            this.Close();
        }

        public void frmRefundmemo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                this.txtmemo = "";
                this.Close();
                return;
            }
            else
                return;
        }
    }
}
