using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using ETech.cls;
using System.IO;

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

                if (!Directory.Exists(cls_globalvariables.ApplicationDataLocalApplicationFolderPath))
                    Directory.CreateDirectory(cls_globalvariables.ApplicationDataLocalApplicationFolderPath);
                
                Application.Run(new POSMain());
            }
        }
    }
}
