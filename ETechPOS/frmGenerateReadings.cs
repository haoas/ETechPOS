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
    public partial class frmGenerateReadings : Form
    {
        public int process = 0;
        //1 - Generate Ungenerated Zreadings
        //2 - Offline XML to SQL

        public frmGenerateReadings()
        {
            InitializeComponent();
            cls_xmlhelper.create_xml_if_missing();
            fncFilter.set_theme_color(this);
        }

        ~frmGenerateReadings() { }

        private void GenerateReadings_Load(object sender, EventArgs e)
        {
            if (process == 1 && !BGW.IsBusy)
            {
                label1.Text = "Please Wait while POS generates previously ungenerated Zreadings";
                BGW.RunWorkerAsync();
            }
            else if (process == 2 && !BGW_XmlToSql.IsBusy)
            {
                label1.Text = "Please Wait while POS Transfers your Offline Transaction to Database";
                BGW_XmlToSql.RunWorkerAsync();
            }
        }

        private void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            zreadFunc.generate_ungenerated_readings(3, true);
            zreadFunc.generate_ungenerated_readings(1, true);
            zreadFunc.generate_ungenerated_readings(1, false);
            zreadFunc.generate_ungenerated_readings(3, false);
        }

        private void BGW_XmlToSql_DoWork(object sender, DoWorkEventArgs e)
        {
            cls_POSTransaction tran = new cls_POSTransaction();
            tran  = cls_xmlhelper.Get_FirstTrans_from_OfflineXML();
            while (tran != null)
            {
                if (cls_xmlhelper.SaveOfflineTransToDB(tran))
                    cls_xmlhelper.Delete_FirstTrans_In_OfflineXML();
                else
                {
                    fncFilter.alert("Failed to save offline trans to database");
                    cls_xmlhelper.hasxmlerror = true;
                    break;
                }
                tran = cls_xmlhelper.Get_FirstTrans_from_OfflineXML();
            }
            cls_xmlhelper.hasxmlerror = false;
        }

        private void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
        private void BGW_XmlToSql_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
