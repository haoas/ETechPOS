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
    public partial class frmReadingSummary : Form
    {
        public string commandentered = "none";
        public DateTime datetime_from_d = DateTime.Now;
        public DateTime datetime_to_d = DateTime.Now;

        public frmReadingSummary()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.commandentered = "print";
            this.Close();
            return;
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            this.datetime_from_d = this.dtpFrom.Value;
        }
        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            this.datetime_to_d = this.dtpTo.Value;
        }

        private void frmReadingSummary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.commandentered = "print";
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void frmReadingSummary_Load(object sender, EventArgs e)
        {
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
    }
}
