using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using ETech.cls;
using ETech.cls;
using System.IO;
using System.Threading;
using ETech.fnc;

namespace ETech.ctrl
{
    public class ctrl_CustomerDisplay
    {
        private static int displayLength = cls_globalvariables.CustomerDisplayLength_v;
        private static SerialPort sport;
        private cls_POSTransaction POSTran;

        public ctrl_CustomerDisplay(SerialPort sport_d)
        {
            sport = sport_d;
            this.POSTran = null;
            sport.PortName = "COM" + cls_globalvariables.com_v;
        }

        public void set_databinding(cls_POSTransaction tran)
        {
            this.POSTran = tran;
        }

        public static void initial_display()
        {
            string line1 = cls_globalvariables.disp1_v;
            string line2 = cls_globalvariables.disp2_v;

            line1 = fncFilter.string_align_center(line1, displayLength);
            line2 = fncFilter.string_align_center(line2, displayLength);

            display(line1, line2);
        }

        public void refresh_display_addproduct(int row_index)
        {
            if (this.POSTran == null || row_index < 0)
                return;

            if (this.POSTran.get_productlist().get_productlist().Count <= row_index)
                return;
            refresh_display_addproduct_and_totalamount(POSTran.get_productlist().get_product(row_index), POSTran.get_productlist().get_totalamount());
        }

        public static void refresh_display_addproduct_and_totalamount(cls_product prod, decimal total)
        {
            string productname = prod.getProductName();
            if (productname.Length > displayLength)
                productname = productname.Substring(0, displayLength);
            string productprice = "P" + prod.getPrice().ToString("N2");
            string totalamt = "P" + total.ToString("N2");
            int temp = displayLength / 2;

            if(productprice.Length > temp)
                productprice = productprice.Substring(0, temp);
            if(totalamt.Length > displayLength - temp)
                totalamt = totalamt.Substring(0, displayLength - temp);

            string line2 = String.Format("{0, -" + temp + "}{1, " + (displayLength - temp) + "}", productprice, totalamt);
            display(productname, line2);
        }

        public void refresh_display_payment()
        {
            if (this.POSTran == null)
                return;

            string change = "P" + POSTran.get_changeamount().ToString("N2");
            string line1 = fncFilter.string_align_center("Thank You!", displayLength);
            string line2 = String.Format("{0,-7}{1," + (displayLength - 7) + "}","Change:", change);
            display(line1, line2);
        }

        public static void display(string line1, string line2)
        {
            try
            {
                if (sport.IsOpen == false)
                {
                    sport.Open();
                }

                sport.Write((char)12 + "");
                if (line1.Length > displayLength)
                    line1 = line1.Substring(0, displayLength);
                sport.Write(line1);

                byte[] c = new byte[] { 0x0B, 0x0A, 0x0D };
                sport.Write(c, 0, c.Length);
                if (line2.Length > displayLength)
                    line2 = line2.Substring(0, displayLength);
                sport.Write(line2);
            }
            catch
            {

            }
        }

    }
}
