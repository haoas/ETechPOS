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
using ETech.Helpers;

namespace ETech
{
    public partial class frmOldData : Form
    {
        public frmOldData()
        {
            InitializeComponent();

            fncFilter.set_theme_color(this);
        }

        private void frmOldData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Save();
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }
        }
        private void frmOldData_Load(object sender, EventArgs e)
        {
            int terminalNumber = cls_globalvariables.TerminalNumber;

            textBox1.Text = terminalNumber.ToString();
            textBox3.Text = cls_globalvariables.Branch.Id.ToString();

            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
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
            mySQLClass mysqlclass = new mySQLClass();

            string ornumber = this.txtLastOR.Text;
            decimal amount = Convert.ToDecimal(this.txtAmount.Text.Trim());
            string lastdate = this.calLastDate.SelectionRange.Start.ToString("yyyy-MM-dd");

            string branchid = textBox3.Text;
            int terminalNumber = Convert.ToInt32(textBox1.Text);

            if ((ornumber.Length != 7))
            {
                DialogHelper.ShowDialog("ornumber should be 7");
                return;
            }

            ornumber = (branchid + terminalNumber + ornumber).TrimStart('0');

            long next_wid = mysqlclass.GetAndInsertNextSyncId("saleshead");
            string sSQL = @"UPDATE `saleshead` SET
                                `branchid` = '" + branchid + @"', 
                                `type` = '3', 
                                `date` = '" + lastdate + @"', 
                                `userid` = '0', 
                                `lastmodifiedby` = '0', 
                                `lastmodifieddate` = NOW(), 
                                `datecreated` = NOW(),  
                                `ornumber` = '" + ornumber + @"',  
                                `terminalno` = '" + terminalNumber + @"',
                                `status` = 1, `customerid` = 0
                            WHERE `SyncId` = '" + next_wid + @"'";
            mySQLFunc.setdb(sSQL);


            long next_detailwid = mysqlclass.GetAndInsertNextSyncId("salesdetail");
            string sSQLdetail = @"UPDATE `salesdetail` SET
                                `headid` = '" + next_wid + @"', 
                                `productid` = '0',  
                                `quantity` = '1',
                                `oprice` = '" + amount.ToString() + @"',  
                                `discount1` = '0', 
                                `price` = '" + amount.ToString() + @"',  
                                `pprice` = '" + amount.ToString() + @"', 
                                `vat` = '" + (amount*0.12M/1.12M).ToString() + @"', 
                                `senior` = 0, 
                                `soldby` = '0',  
                                `addbackqty` = '0',  
                                `addbackbigqty` = '0' 
                            WHERE `SyncId` = " + next_detailwid;
            mySQLFunc.setdb(sSQLdetail);

            long collectionheadwid = mysqlclass.GetAndInsertNextSyncId("collectionhead");
            string sSQLch = @"UPDATE `collectionhead` SET
                                `customerid` = 0, 
                                `collectiondate` = '" + lastdate + @"', 
                                `userid` = 0,  
                                `status` = 1,
                                `branchid` = " + branchid + @", 
                                `lastmodifieddate` = NOW(), 
                                `lastmodifiedby` = 0, 
                                `datecreated` = NOW()
                            WHERE `SyncId` = " + collectionheadwid;
            mySQLFunc.setdb(sSQLch);

            string sSQLcs = @"INSERT INTO `collectionsales`
                            (`headid`, `saleswid`, `amount`)
                            VALUES
                            ( " + collectionheadwid + ", " + next_wid + ", " + amount.ToString() + ")";
            mySQLFunc.setdb(sSQLcs);


            long collectiondetailwid = mysqlclass.GetAndInsertNextSyncId("collectiondetail");
            string sSQLcd = @"UPDATE `collectiondetail` SET
                                `headid` = " + collectionheadwid.ToString() + @",
                                `method` = 1, 
                                `amount` = " + amount.ToString() + @"
                           WHERE `SyncId` = " + collectiondetailwid;
            mySQLFunc.setdb(sSQLcd);

            DialogHelper.ShowDialog("Saved");

            this.Close();
        }
    }
}
