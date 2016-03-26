using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETech.Helpers
{
    public static class ControlEventHelper
    {
        public static void Control_Hand_MouseEnter(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.Cursor = Cursors.Hand;
        }
        public static void Control_Arrow_MouseLeave(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.Cursor = Cursors.Arrow;
        }
        public static void Control_Trim_Leave(object sender, EventArgs e)
        {
            ControlManipulationHelper.TrimTextInControl((Control)sender);
        }
    }
}
