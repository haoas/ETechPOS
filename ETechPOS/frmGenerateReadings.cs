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
    public partial class frmGenerateReadings : Form
    {
        public int process = 0;
        //1 - Generate Ungenerated Zreadings

        public frmGenerateReadings()
        {
            InitializeComponent();
            fncFilter.set_theme_color(this);
        }

        ~frmGenerateReadings() { }

        private void GenerateReadings_Load(object sender, EventArgs e)
        {
            if (process == 1 && !BGW.IsBusy)
            {
                label1.Text = "Please Wait while POS generates previously ungenerated Zreadings";
                BGW.RunWorkerAsync();
            }
        }

        private void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            zreadFunc.generate_ungenerated_readings();
        }

        private void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
