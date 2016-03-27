using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using ETech.cls;
using System.IO;
using ETech.Helpers;
using ETech.Models.Global;
using ETech.fnc;
using ETech.Models.Database;

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
                    DialogHelper.ShowDialog("POS is Already Open");
                    return;

                }
                if (!ConnectionSettingsController.TryGetData(out cls_globalvariables.ConnectionSettings))
                    return;
                if (!MySqlFunction.HasConnection())
                    return;
                cls_globalvariables.Branch = BranchController.GetDataFromConfigurationTable();
                GC.Collect();
                Application.Run(new POSMain());
            }
        }
    }
}
