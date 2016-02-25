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
    public partial class frmMember : Form
    {
        public cls_member member;
        private cls_member tempMember;

        public frmMember()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);
        }

        private void frmMember_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                done_process();
            if (e.KeyCode == Keys.F7)
                remove();
            else if (e.KeyCode == Keys.Escape)
                cancel();
        }

        private void txtCardID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                search_member();
                e.Handled = true;
            }
            else
                return;
        }

        private bool search_member()
        {
            this.txtCardID.Focus();
            this.txtCardID.SelectAll();

            string str = txtCardID.Text.Replace(";", "").Replace("?", "");

            if (str.Length <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_member_notregistered);
                ClearControlsData();
                return false;
            }

            cls_member member_temp = new cls_member();
            frmLoad loadForm = new frmLoad("Searching for Member", "Loading Screen");
            loadForm.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                member_temp.setcls_member_by_cardid(str);
            };
            loadForm.ShowDialog();

            if (member_temp.getwid() == 0)
            {
                fncFilter.alert(cls_globalvariables.warning_member_notregistered);
                ClearControlsData();
                return false;
            }

            this.lblName.Text = member_temp.getfullname() + "\n" + member_temp.getmobile() + "\n" + member_temp.getbirhtdate();
            this.lblPoints.Text = member_temp.getPreviousPoints().ToString("N2");
            this.lblMemberType.Text = member_temp.get_memberrate_name();
            this.member = member_temp;
            return true;
        }

        public void done_process()
        {
            if (!search_member())
                return;
            this.Close();
        }

        private void remove()
        {
            member = new cls_member();
            this.Close();
        }
        private void cancel()
        {
            member = tempMember;
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            remove();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            cancel();
        }

        private void frmMember_Load(object sender, EventArgs e)
        {
            bool result = false;
            frmLoad loadForm = new frmLoad("Checking Connection.", "Loading Screen");
            loadForm.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                result = mySQLFunc.check_connection_main();
            };
            loadForm.ShowDialog();
            if (!result)
            {
                fncFilter.alert("Member feature is Offline.");
                Close();
                return;
            }
            ClearControlsData();
            if (member.getwid() != 0)
            {
                this.txtCardID.Text = this.member.getcardid();
                this.lblName.Text = this.member.getfullname() + "\n" + this.member.getmobile() + "\n" + this.member.getbirhtdate();
                this.lblMemberType.Text = this.member.get_memberrate_name();
                this.lblPoints.Text = this.member.getPreviousPoints().ToString("N2");
            }
            else
                btnRemove.Visible = false;

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();

            tempMember = member;
        }

        private void ClearControlsData()
        {
            this.lblName.Text = "";
            this.lblMemberType.Text = "";
            this.lblPoints.Text = "0.00";
        }
    }
}
