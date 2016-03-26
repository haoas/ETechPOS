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
using ETech.FormatDesigner;

namespace ETech
{
    public partial class frmORPrintPreview : Form
    {
        public long or_number;
        public long currenttrans_ornumber;
        public List<string> CurrentUserAuthlist;
        public int origwidth = 296;
        public int origheight = 3000;
        public int zoompercent = 200;

        public frmORPrintPreview()
        {
            InitializeComponent();
            mySQLFunc.initialize_global_variables();

            fncFilter.set_theme_color(this);
            cls_globalfunc.formaddkbkpevent(this);

            this.or_number = 0;
            this.CurrentUserAuthlist = new List<string>();

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
                                AND `branchid` = " + cls_globalvariables.BranchCode + @" AND `status`=1";
            DataTable dt = mySQLFunc.getdb(sSQL);
            if (dt.Rows.Count <= 0)
            {
                this.txtORNumber_d.Focus();
                return;
            }

            long maxtenderedOR = 0;
            long.TryParse(dt.Rows[0]["ornumber"].ToString(), out maxtenderedOR);
            if (maxtenderedOR == this.currenttrans_ornumber)
                maxtenderedOR = maxtenderedOR - 1;

            this.txtORNumber_d.Text = maxtenderedOR.ToString();
            this.txtORNumber_d.Focus();

            if (!bgwLoadReceipt.IsBusy)
                bgwLoadReceipt.RunWorkerAsync();

            txtORNumber_d.AsInteger();
        }
        private void ORPrintPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                done_process();
            else if (e.KeyCode == Keys.Escape)
            {
                this.or_number = 0;
                this.Close();
                return;
            }
            else
                return;
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
            done_process();
        }
        private void btnESC_Click(object sender, EventArgs e)
        {
            this.or_number = 0;
            this.Close();
            return;
        }

        private void bgwLoadReceipt_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadReceipt(long.Parse(txtORNumber_d.Text));
        }

        private void done_process()
        {
            long or_num = long.Parse(this.txtORNumber_d.Text.Trim());

            if (or_num == 0)
            {
                fncFilter.alert(cls_globalvariables.warning_input_invalid);
                this.txtORNumber_d.Focus();
                this.txtORNumber_d.SelectAll();
                return;
            }

            this.or_number = or_num;

            this.Close();
        }

        private void LoadReceipt(long or_num)
        {
            if (or_num == 0)
            {
                ClearGraphics(pbPreview);
                return;
            }
            cls_POSTransaction temp_tran = new cls_POSTransaction();
            temp_tran.set_transaction_by_ornumber(or_num);

            if (((temp_tran.getShow() == 0) && (temp_tran.getStatus() == 0)) ||
                (temp_tran.getSyncId() == 0))
            {
                ClearGraphics(pbPreview);
                fncFilter.alert(cls_globalvariables.warning_ornumber_invalid);
                return;
            }

            string checkIfVoidSql = @"SELECT Count(*) as cnt FROM Saleshead WHERE (`status`=0) AND `SyncId` = '" + temp_tran.getSyncId() + @"'";
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

        private bool Check_ReprintReceiptPermission(bool isSwitch)
        {
            UserAuthorizationFunction userAuthorizationFunction = new UserAuthorizationFunction(CurrentUserAuthlist);
            return userAuthorizationFunction.IsVerifiedAuthorization("REPRINTOR");
        }
        private void ClearGraphics(Control control)
        {
            Graphics g = control.CreateGraphics();
            g.Clear(Color.White);
        }
    }
}