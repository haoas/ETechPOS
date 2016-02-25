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
    public partial class frmPromo : Form
    {
        private List<int> promoids;
        public int promochosen;

        public frmPromo()
        {
            InitializeComponent();

            this.promochosen = 0;
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
        }

        private void frmPromo_Load(object sender, EventArgs e)
        {
            this.promoids = new List<int>();

            this.btnF5.Enabled = false; 
            this.btnF6.Enabled = false;
            this.btnF7.Enabled = false;
            this.btnF8.Enabled = false;
            this.btnF9.Enabled = false;

            this.btnF5.Visible = false;
            this.btnF6.Visible = false;
            this.btnF7.Visible = false;
            this.btnF8.Visible = false;
            this.btnF9.Visible = false;

            DataTable dt = mySQLFunc.getdb(@"SELECT `wid`, `name`, `description` 
                                            FROM `pospromotion` WHERE `show` = 1 AND `status` = 1
                                                AND (date(NOW()) >= date(`datefrom`) OR `datefrom` = '0000-00-00 00:00:00' )
	                                            AND (date(NOW()) <= date(`dateto`) OR `dateto` = '0000-00-00 00:00:00' )
                                                AND branchid = " + cls_globalvariables.branchid_v );
            int cnt = 0;
            foreach (DataRow dr in dt.Rows)
            {
                this.promoids.Add(Convert.ToInt32(dr["wid"]));
                switch (cnt)
                {
                    case 0:
                        this.lblPromo1Name.Text = dr["name"].ToString();
                        this.lblPromo1Desc.Text = dr["description"].ToString();
                        this.btnF5.Enabled = true;
                        this.btnF5.Visible = true;
                        break;
                    case 1:
                        this.lblPromo2Name.Text = dr["name"].ToString();
                        this.lblPromo2Desc.Text = dr["description"].ToString();
                        this.btnF6.Enabled = true;
                        this.btnF6.Visible = true;
                        break;
                    case 2:
                        this.lblPromo3Name.Text = dr["name"].ToString();
                        this.lblPromo3Desc.Text = dr["description"].ToString();
                        this.btnF7.Enabled = true;
                        this.btnF7.Visible = true;
                        break;
                    case 3:
                        this.lblPromo4Name.Text = dr["name"].ToString();
                        this.lblPromo4Desc.Text = dr["description"].ToString();
                        this.btnF8.Enabled = true;
                        this.btnF8.Visible = true;
                        break;
                    case 4:
                        this.lblPromo5Name.Text = dr["name"].ToString();
                        this.lblPromo5Desc.Text = dr["description"].ToString();
                        this.btnF9.Enabled = true;
                        this.btnF9.Visible = true;
                        break;
                }
                cnt++;
            }

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }
        
        public void F5()
        {
            this.promochosen = this.promoids[0];
            this.Close();
            return;
        }
        public void F6()
        {
            this.promochosen = this.promoids[1];
            this.Close();
            return;
        }
        public void F7()
        {
            this.promochosen = this.promoids[2];
            this.Close();
            return;
        }
        public void F8()
        {
            this.promochosen = this.promoids[3];
            this.Close();
            return;
        }
        public void F9()
        {
            this.promochosen = this.promoids[4];
            this.Close();
            return;
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
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void frmPromo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if(btnF5.Enabled) F5();
                    break;
                case Keys.F6:
                    if(btnF6.Enabled) F6();
                    break;
                case Keys.F7:
                    if(btnF7.Enabled) F7();
                    break;
                case Keys.F8:
                    if (btnF8.Enabled) F8();
                    break;
                case Keys.F9:
                    if (btnF9.Enabled) F9();
                    break;
                case Keys.Escape:
                    this.promochosen = 0;
                    this.Close();
                    break;
                default:
                    return;
            }
        }
        private void frmPromo_Leave(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
