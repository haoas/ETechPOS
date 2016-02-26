using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using ETech.cls;

namespace ETech
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///    private static Mutex _mutex;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Mutex mutex = new Mutex(false, "pos"))
            {
                if (!mutex.WaitOne(1000, false))
                {
                    MessageBox.Show("POS is Already Open");
                    return;

                }

                GC.Collect();

                //try
                //{
                //    cls_globalvariables.is4By3ratio_v = ((double)Screen.PrimaryScreen.Bounds.Width / (double)Screen.PrimaryScreen.Bounds.Height) == 1.3333333333333333;
                
                //}
                //catch (Exception ex )
                //{
                //    MessageBox.Show(ex.ToString());
                //    throw;
                //}
                
                Application.Run(new POSMain());
            }
        }
    }
}
