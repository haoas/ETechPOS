using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ETech.Views.Generic_Forms
{
    public partial class KeyboardForm : Form
    {
        public KeyboardForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            TopMost = true;
        }
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams; // Retrieve the normal parameters.
                cp.Style = 0x40000000 | 0x4000000; // WS_CHILD | WS_CLIPSIBLINGS
                cp.ExStyle &= 0x00080000; // WS_EX_LAYERED
                cp.Parent = GetDesktopWindow(); // Make "GetDesktopWindow()" from your own namespace.
                return cp;
            }
        }

        private void keyboardControl1_Load(object sender, EventArgs e)
        {
            Location = new Point(Location.X, Screen.PrimaryScreen.Bounds.Height - Size.Height);
        }
    }
}
