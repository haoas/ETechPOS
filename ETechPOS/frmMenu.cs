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
using ETech.fnc;

namespace ETech
{
    public partial class frmMenu : Form
    {
        public bool F1flag = false;
        public string commandentered = "none";
        
        public frmMenu()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
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
        public void F3()
        {
            this.commandentered = "F3";
            this.Close();
            return;
        }
        public void F4()
        {
            this.commandentered = "F4";
            this.Close();
            return;
        }
        public void F5()
        {
            this.commandentered = "F5";
            this.Close();
            return;
        }
        public void F6()
        {
            this.commandentered = "F6";
            this.Close();
            return;
        }
        public void F7()
        {
            this.commandentered = "F7";
            this.Close();
            return;
        }
        public void F8()
        {
            this.commandentered = "F8";
            this.Close();
            return;
        }
        public void F9()
        {
            this.commandentered = "F9";
            this.Close();
            return;
        }
        public void F10()
        {
            this.commandentered = "F10";
            this.Close();
            return;
        }
        public void F11()
        {
            this.commandentered = "F11";
            this.Close();
            return;
        }
        public void F12()
        {
            this.commandentered = "F12";
            this.Close();
            return;
        }
        public void Old_data()
        {
            this.commandentered = "O";
            this.Close();
            return;
        }
        public void Settings()
        {
            this.commandentered = "S";
            this.Close();
            return;
        }
        public void Settings2()
        {
            this.commandentered = "0";
            this.Close();
            return;
        }
        public void P()
        {
            this.commandentered = "P";
            this.Close();
            return;
        }
        public void L()
        {
            this.commandentered = "L";
            this.Close();
            return;
        }
        public void M()
        {
            this.commandentered = "M";
            this.Close();
            return;
        }
        public void H()
        {
            this.commandentered = "H";
            this.Close();
            return;
        }
        public void Escape()
        {
            this.commandentered = "Escape";
            this.Close();
            return;
        }

        private void frmMenu_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    F1();
                    break;
                case Keys.F2:
                    F2();
                    break;
                case Keys.F3:
                    F3();
                    break;
                case Keys.F4:
                    F4();
                    break;
                case Keys.F5:
                    F5();
                    break;
                case Keys.F6:
                    if (F1flag == true)   
                        F6();
                    break;
                case Keys.F7:
                    if (F1flag == true)
                        F7();
                    break;
                case Keys.F8:
                    if (F1flag == true)
                        F8();
                    break;
                case Keys.F9:
                    if (F1flag == true)
                        F9();
                    break;
                case Keys.F10:
                    if (F1flag == true)
                        F10();
                    e.Handled = true;
                    break;
                case Keys.F11:
                    if (F1flag == true)
                        F11();
                    break;
                case Keys.F12:
                    if (F1flag == true)
                        F12();
                    break;
                case Keys.O:
                    Old_data();
                    break;
                case Keys.S:
                    Settings();
                    break;
                case Keys.D0:
                    Settings2();
                    break;
                case Keys.P:
                    if (F1flag == true)
                        P(); 
                    break;
                case Keys.L:
                    if (F1flag == true)
                        L();
                    break;
                case Keys.H:
                    if (F1flag == true)
                        H();
                    break;
                case Keys.M: 
                    M(); 
                    break;
                case Keys.Escape:
                    Escape();
                    break;
                default:    
                    return;
            }
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            F1();
        }
        private void btnF2_Click(object sender, EventArgs e)
        {
            F2();
        }
        private void btnF3_Click(object sender, EventArgs e)
        {
            F3();
        }
        private void btnF4_Click(object sender, EventArgs e)
        {
            F4();
        }
        private void btnF5_Click(object sender, EventArgs e)
        {
            F5();
        }
        private void btnF6_Click(object sender, EventArgs e)
        {
            F6();
        }
        private void btnF7_Click(object sender, EventArgs e)
        {
            F7();
        }
        private void btnF8_Click(object sender, EventArgs e)
        {
            F8();
        }
        private void btnF9_Click(object sender, EventArgs e)
        {
            F9();
        }
        private void btnF10_Click(object sender, EventArgs e)
        {
            F10();
        }
        private void btnF11_Click(object sender, EventArgs e)
        {
            F11();
        }
        private void btnF12_Click(object sender, EventArgs e)
        {
            F12();
        }
        private void btnP_Click(object sender, EventArgs e)
        {
            P();
        }
        private void btnL_Click(object sender, EventArgs e)
        {
            L();
        }
        private void btnS_Click(object sender, EventArgs e)
        {
            Settings();
        }
        private void btnM_Click(object sender, EventArgs e)
        {
            M();
        }
        private void btnH_Click(object sender, EventArgs e)
        {
            H();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            Escape();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            if (F1flag == true)
            {
                btnF6.Enabled = true;
                btnF7.Enabled = true;
                btnF8.Enabled = true;
                btnF9.Enabled = true;
                btnF10.Enabled = true;
                btnF11.Enabled = true;
                btnF12.Enabled = true;
                btnP.Enabled = true;
                btnL.Enabled = true;
                btnM.Enabled = true;
                btnH.Enabled = true;
            }

            if (cls_globalvariables.hide_reprintreceipt_v == "1")
                btnF4.Visible = false;

            if (cls_globalvariables.posdautoxz_v == "0")
                btnF3.Text = "F3\nOther Readings";
            else
                btnF3.Text = "F3\nExit";
            
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        private void frmMenu_Leave(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
