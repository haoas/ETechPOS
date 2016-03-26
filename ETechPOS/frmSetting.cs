using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using System.IO;
using System.Text.RegularExpressions;
using ETech.fnc;
using ETech.Helpers;

namespace ETech
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }

        private void frmSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Save();
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void frmSetting_Load(object sender, EventArgs e)
        {
            this.num_orprintcnt.Value = Convert.ToInt32(cls_globalvariables.ORPrintCount_v);
            this.txtBusinessName.Text = cls_globalvariables.BusinessName_v;
            this.txtOwner.Text = cls_globalvariables.Owner_v;
            this.txtAddress.Text = cls_globalvariables.Address_v;

            this.txtPermit.Text = cls_globalvariables.PermitNo_v;
            this.txtACC.Text = cls_globalvariables.ACC_v;
            this.txtSerialNo.Text = cls_globalvariables.Serial_v;
            this.txtMIN.Text = cls_globalvariables.MIN_v;
            this.txtTIN.Text = cls_globalvariables.TIN_v;

            this.txtFooter1.Text = cls_globalvariables.orfooter1_v;
            this.txtFooter2.Text = cls_globalvariables.orfooter2_v;
            this.txtFooter3.Text = cls_globalvariables.orfooter3_v;
            this.txtFooter4.Text = cls_globalvariables.orfooter4_v;

            //fncFullScreen fncfullscreen = new fncFullScreen(this);
            //fncfullscreen.ResizeFormsControls();
        }

        private void btnTestPrinter_Click(object sender, EventArgs e)
        {
            fncHardware.CheckPrinterStatus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save()
        {
            StreamReader reader = new StreamReader(cls_globalvariables.settingspath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, "ORPrintCount=.*", "ORPrintCount=" + num_orprintcnt.Value.ToString() + "\r");
            content = Regex.Replace(content, "BusinessName=.*", "BusinessName=" + txtBusinessName.Text + "\r");
            content = Regex.Replace(content, "Owner=.*", "Owner=" + txtOwner.Text + "\r");
            content = Regex.Replace(content, "Address=" + cls_globalvariables.Address_v, "Address=" + txtAddress.Text);

            content = Regex.Replace(content, "PermitNo=.*", "PermitNo=" + txtPermit.Text + "\r");
            content = Regex.Replace(content, "ACC=.*", "ACC=" + txtACC.Text + "\r");
            content = Regex.Replace(content, "Serial=.*", "Serial=" + txtSerialNo.Text + "\r");
            content = Regex.Replace(content, "MIN=.*", "MIN=" + txtMIN.Text + "\r");
            content = Regex.Replace(content, "TIN=.*", "TIN=" + txtTIN.Text + "\r");

            content = Regex.Replace(content, "orfooter1=.*", "orfooter1=" + txtFooter1.Text + "\r");
            content = Regex.Replace(content, "orfooter2=.*", "orfooter2=" + txtFooter2.Text + "\r");
            content = Regex.Replace(content, "orfooter3=.*", "orfooter3=" + txtFooter3.Text + "\r");
            content = Regex.Replace(content, "orfooter4=.*", "orfooter4=" + txtFooter4.Text + "\r");

            StreamWriter writer = new StreamWriter(cls_globalvariables.settingspath);
            writer.Write(content);
            writer.Close();

            cls_globalvariables.BusinessName_v = this.txtBusinessName.Text;
            cls_globalvariables.Owner_v = this.txtOwner.Text;
            cls_globalvariables.Address_v = this.txtAddress.Text;

            cls_globalvariables.PermitNo_v = this.txtPermit.Text;
            cls_globalvariables.ACC_v = this.txtACC.Text;
            cls_globalvariables.Serial_v = this.txtSerialNo.Text;
            cls_globalvariables.MIN_v = this.txtMIN.Text;
            cls_globalvariables.TIN_v = this.txtTIN.Text;

            cls_globalvariables.orfooter1_v = this.txtFooter1.Text;
            cls_globalvariables.orfooter2_v = this.txtFooter2.Text;
            cls_globalvariables.orfooter3_v = this.txtFooter3.Text;
            cls_globalvariables.orfooter4_v = this.txtFooter4.Text;

            DialogHelper.ShowDialog("Saved. Please restart the program.");
            this.Close();
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void txtFooter1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void txtFooter2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void txtFooter3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void txtFooter4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }
    }
}
