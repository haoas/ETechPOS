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
    public partial class frmLoad : Form
    {
        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public frmLoad(string message, string caption)
        {
            InitializeComponent();
            Message = message;
            Caption = caption;
            fncFilter.set_theme_color(this);
        }

        private void frmLoad_Shown(object sender, EventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                return;
            BackgroundWorker.RunWorkerAsync();
        }

        private void frmLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                e.Cancel = true;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}
