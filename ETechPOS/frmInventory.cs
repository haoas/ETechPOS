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
    public partial class frmInventory : Form
    {
        public string commandentered = "none";
        public DateTime datetime_d = DateTime.Now;
        public DateTime datetimeTO_d = DateTime.Now;
        public List<string> UserAuthorizationList;
        public frmPermissionCode frmauthcode;

        public frmInventory()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);

            DateTime MinSalesDate = zreadFunc.GetMinSalesDate();
            this.dateTimePicker1.MinDate = MinSalesDate;
            this.dateTimePicker2.MinDate = MinSalesDate;
        }

        public void F1()
        {
            this.commandentered = "F1";
            this.Close();
            return;
        }
        public void F2()
        {
            this.commandentered = "F2";
            this.Close();
            return;
        }
        public void Escape()
        {
            this.commandentered = "none";
            this.Close();
            return;
        }
        public void F4()
        {
            this.commandentered = "F4";
            this.Close();
            return;
        }

        public void frmInventory_Load(object sender, EventArgs e)
        {
            if (!UserAuthorizationList.Contains("ZREAD"))
            {
                bool permcheck_collectioncash = isInput_permission_code("ZREAD");
                if (!permcheck_collectioncash)
                {
                    this.Close();
                    return;
                }
            }

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private bool isInput_permission_code(string permissioncode)
        {
            bool permcheck = false;
            frmauthcode = new frmPermissionCode();
            frmauthcode.auth_needed = permissioncode;
            frmauthcode.ShowDialog();
            permcheck = frmauthcode.permcode;

            return permcheck;
        }

        private void frmInventory_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    F1();
                    break;
                case Keys.F2:
                    F2();
                    break;
                case Keys.Escape:
                    Escape();
                    break;
                case Keys.F4:
                    F4();
                    break;
                case Keys.D:
                    if (cls_globalvariables.readDateRange_v == 1)
                        this.Size = new Size(this.Width, (int)(this.Height * 1.5));
                    else if (cls_globalvariables.readDateRange_v == 2)
                        this.Size = new Size(this.Width, (int)(this.Height * 1.8));
                    break;
                default:
                    return;
            }
        }
        private void frmInventory_Leave(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            F1();
        }
        private void btnF2_Click(object sender, EventArgs e)
        {
            F2();
        }
        private void btnF4_Click(object sender, EventArgs e)
        {
            F4();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.datetime_d = this.dateTimePicker1.Value;
            this.dateTimePicker2.Value = this.datetime_d; 
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.datetimeTO_d = this.dateTimePicker2.Value;
        }
    }
}
