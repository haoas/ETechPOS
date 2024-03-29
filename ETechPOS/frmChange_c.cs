﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using ETech.fnc;
using ETECHPOS.Helpers;

namespace ETech
{
    public partial class frmChange_c : Form
    {
        private bool flag = true;
        public cls_POSTransaction tran;
        public string changeamount;
        public bool isTransactionDone = true;

        public frmChange_c()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);

            changeamount = "0.00";
        }

        private void frmChange_c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                this.Close();
                return;
            }
            else
                return;
        }
        private void frmChange_c_Load(object sender, EventArgs e)
        {
            this.lblChange_d.Text = this.changeamount;

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmChange_c_Shown(object sender, EventArgs e)
        {
            if (Visible && flag)
            {
                flag = false;
                int temp = 0;
                bool retry = false;
                do
                {
                    retry = false;
                    frmLoad loadForm = new frmLoad("Saving Transaction", "Loading Screen");
                    loadForm.BackgroundWorker.DoWork += (sender1, e1) =>
                    {
                        temp = POSMain.save_transaction_thread(tran);
                    };
                    loadForm.ShowDialog();
                    if (temp == 0)
                    {
                        LogsHelper.Print("Tender success");
                    }
                    else if (temp == 1)
                    {
                        fncFilter.alert("Member transaction was unable to complete due to connection problems. F8 - Payment to try again.");
                        LogsHelper.Print("Tender failed: Member Feature Offline");
                        isTransactionDone = false;
                    }
                    else if (temp == -1)
                    {
                        LogsHelper.Print("Tender failed: General Saving failure");
                        if (DialogHelper.ShowDialog("An error occured in saving transaction. Would you like to retry?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            retry = true;
                            LogsHelper.Print("Tender failure popup: Retry");
                        }
                        else
                        {
                            LogsHelper.Print("Tender failure popup: Ignore");
                            fncHardware.print_receipt(tran, false, false);
                        }
                    }
                }
                while (retry);
            }
        }
    }
}
