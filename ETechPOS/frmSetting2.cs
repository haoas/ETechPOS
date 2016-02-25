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

namespace ETech
{
    public partial class frmSetting2 : Form
    {
        public frmSetting2()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }
        
        private void frmSetting2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Save();
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void frmSetting2_Load(object sender, EventArgs e)
        {
            this.chkIsAutoXZ.Checked = (cls_globalvariables.posdautoxz_v == "1");
            this.nudPOSDPercent.Value = Convert.ToDecimal(cls_globalvariables.posd_percent_v);
            nudPosdMininum.Value = cls_globalvariables.posdminamt_v;
            nudPosdMaximum.Value = cls_globalvariables.posdmaxamt_v;

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            StreamReader reader = new StreamReader(cls_globalvariables.settingspath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, "posdautoxz=.*", "posdautoxz=" + (chkIsAutoXZ.Checked ? "1" : "0") + "\r");
            content = Regex.Replace(content, "posdpercent=.*", "posdpercent=" + nudPOSDPercent.Value.ToString() + "\r");
            content = Regex.Replace(content, "posdminamt=.*", "posdminamt=" + nudPosdMininum.Value + "\r");
            content = Regex.Replace(content, "posdmaxamt=.*", "posdmaxamt=" + nudPosdMaximum.Value + "\r");

            StreamWriter writer = new StreamWriter(cls_globalvariables.settingspath);
            writer.Write(content);
            writer.Close();

            cls_globalvariables.posdautoxz_v = (this.chkIsAutoXZ.Checked ? "1" : "0");
            cls_globalvariables.posd_percent_v = (int)nudPOSDPercent.Value;
            cls_globalvariables.posdminamt_v = (int)nudPosdMininum.Value;
            cls_globalvariables.posdmaxamt_v = (int)nudPosdMaximum.Value;

            MessageBox.Show("Saved. Please restart the program.");
            this.Close();
        }
    }
}
