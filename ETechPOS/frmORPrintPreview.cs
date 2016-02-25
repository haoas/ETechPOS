using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ETech.cls;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using ETech.fnc;

namespace ETech
{
    public partial class frmORPrintPreview : Form
    {
        public string or_number;
        public string currenttrans_ornumber;
        public bool is_switch_posd;
        public List<int> cur_permissions;
        public frmPermissionCode frmpermcode;
        public int origwidth = 296;
        public int origheight = 3000;
        public int zoompercent = 200;

        public frmORPrintPreview()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.or_number = "";
            this.cur_permissions = new List<int>();
            this.is_switch_posd = (cls_globalvariables.posdreceiptautoswitch_v == "1");
            this.Text = this.is_switch_posd ? "Reprint Receipt" : "Reprint Receipt_";

            pbPreview.Width = origwidth;
            pbPreview.Height = origheight;

            if (!Directory.Exists(cls_globalvariables.mydocumentpath))
                Directory.CreateDirectory(cls_globalvariables.mydocumentpath);
        }

        private void ORPrintPreview_Load(object sender, EventArgs e)
        {
            if (!Check_ReprintReceiptPermission(false))
                this.Close();
            string sSQL = @"SELECT MAX(`ornumber`) as `ornumber` FROM `saleshead` 
                            WHERE `terminalno` = " + cls_globalvariables.terminalno_v + @"
                                AND `branchid` = " + cls_globalvariables.branchid_v + @" AND `status`=1";
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtORNumber_d.Focus();
                return;
            }

            string maxtenderedOR = dt.Rows[0]["ornumber"].ToString();
            if (maxtenderedOR == this.currenttrans_ornumber)
                maxtenderedOR = (Convert.ToInt64(maxtenderedOR) - 1).ToString();

            this.txtORNumber_d.Text = maxtenderedOR;
            this.txtORNumber_d.Focus();

            if (!bgwLoadReceipt.IsBusy)
                bgwLoadReceipt.RunWorkerAsync();
        }
        private void ORPrintPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                done_process();
            //else if (e.KeyCode == Keys.H)
            //{
            //    if (Check_ReprintReceiptPermission(true))
            //    {
            //        ChangePosd();
            //    }
            //    e.Handled = true;
            //}
            else if (e.KeyCode == Keys.Escape)
            {
                this.or_number = "";
                this.Close();
                return;
            }
            else
                return;
        }

        private void txtORNumber_d_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 'h' || e.KeyChar == 'H')
            //    e.Handled = true;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtORNumber_d_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (!bgwLoadReceipt.IsBusy)
                    bgwLoadReceipt.RunWorkerAsync();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (!bgwLoadReceipt.IsBusy)
                bgwLoadReceipt.RunWorkerAsync();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            LOGS.LOG_PRINT("isposd: " + this.is_switch_posd);
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.or_number = "";
            this.Close();
            return;
        }

        private void bgwLoadReceipt_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadReceipt(txtORNumber_d.Text);
        }

        private void done_process()
        {
            string or_num = this.txtORNumber_d.Text.Trim().Replace("/", "");

            if (or_num.Length <= 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtORNumber_d.Focus();
                this.txtORNumber_d.SelectAll();
                return;
            }

            this.or_number = or_num;

            this.Close();
        }
        private void ChangePosd()
        {
            this.or_number = "";
            this.is_switch_posd = !this.is_switch_posd;
            this.Text = this.is_switch_posd ? "Reprint Receipt" : "Reprint Receipt_";
        }
        private void LoadReceipt(string or_num)
        {
            or_num = or_num.Trim().Replace("/", "");

            if (or_num.Length <= 0)
            {
                ClearGraphics(pbPreview);
                return;
            }
            cls_POSTransaction temp_tran = new cls_POSTransaction();
            temp_tran.set_transaction_by_ornumber(or_num, is_switch_posd);

            if (((temp_tran.getShow() == 0) && (temp_tran.getStatus() == 0)) ||
                (temp_tran.getWid() == 0))
            {
                ClearGraphics(pbPreview);
                fncFilter.alert(cls_globalvariables.warning_ornumber_invalid);
                return;
            }

            string checkIfVoidSql = @"SELECT Count(*) as cnt FROM Saleshead WHERE (`show`=0 OR `status`=0) AND `wid` = '" + temp_tran.getWid() + @"'";
            bool isVoid = Convert.ToBoolean(mySQLFunc.getdb(checkIfVoidSql).Rows[0]["cnt"]);

            SaveBitmap(temp_tran, isVoid);
        }
        private void SaveBitmap(cls_POSTransaction temp_tran, bool isVoid)
        {
            int width = Convert.ToInt32(origwidth * (zoompercent / (decimal)100));
            int height = Convert.ToInt32(origheight * (zoompercent / (decimal)100));
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(zoompercent, zoompercent);
            fncHardware.printpage_receipt(null, null, bmp, temp_tran, isVoid, true);
            bmp.Save(cls_globalvariables.mydocumentpath + "Receipt.jpg");
            bmp.Dispose();
            Bitmap bitmap;
            using (var bmpTemp = new Bitmap(cls_globalvariables.mydocumentpath + "Receipt.jpg"))
                bitmap = new Bitmap(bmpTemp);
            pbPreview.Image = bitmap;
        }

        private bool isInput_permission_code(int permissioncode)
        {
            bool permcheck = false;
            frmpermcode = new frmPermissionCode();
            frmpermcode.permission_needed = permissioncode;
            frmpermcode.ShowDialog();
            permcheck = frmpermcode.permcode;

            return permcheck;
        }
        private bool Check_ReprintReceiptPermission(bool isSwitch)
        {
            if (isSwitch ? !this.is_switch_posd : this.is_switch_posd)
            {
                bool permcheck_reprintreceipt_posd = false;
                if (fncFilter.check_permission_reprint_posd(this.cur_permissions))
                    permcheck_reprintreceipt_posd = true;
                else
                {
                    permcheck_reprintreceipt_posd = isInput_permission_code(fncFilter.get_permission_reprint_posd());
                }

                return permcheck_reprintreceipt_posd;
            }
            else
            {
                bool permcheck_reprintreceipt = false;
                if (fncFilter.check_permission_reprint(this.cur_permissions))
                    permcheck_reprintreceipt = true;
                else
                {
                    permcheck_reprintreceipt = isInput_permission_code(fncFilter.get_permission_reprint());
                }
                return permcheck_reprintreceipt;
            }
        }
        private void ClearGraphics(Control control)
        {
            Graphics g = control.CreateGraphics();
            g.Clear(Color.White);
        }
    }
}