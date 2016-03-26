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
using ETech;
using ETech.Built_In_Views;

namespace ETech
{
    public partial class DialogForm : StandardDialogForm
    {
        private string _Message;
        private MessageBoxButtons _MessageBoxButtons;

        public DialogForm(string message)
            : this(message, MessageBoxButtons.OK)
        {
        }
        public DialogForm(string message, MessageBoxButtons messageBoxButtons)
        {
            InitializeComponent();

            btnCommand1.Click += new EventHandler(CommandButton_Click);
            btnCommand2.Click += new EventHandler(CommandButton_Click);
            btnCommand3.Click += new EventHandler(CommandButton_Click);

            _Message = message;
            _MessageBoxButtons = messageBoxButtons;
        }

        private void DialogForm_KeyDown(object sender, KeyEventArgs e)
        {
            Button button = GetButtonByDialogResult(e.KeyData);
            if (button != null)
                button.PerformClick();
        }
        private void DialogForm_Load(object sender, EventArgs e)
        {
            lblMessage.Text = _Message;
            switch (_MessageBoxButtons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    ActivateCommandButton_Click(1, DialogResult.Ignore);
                    ActivateCommandButton_Click(2, DialogResult.Retry);
                    ActivateCommandButton_Click(3, DialogResult.Abort);
                    break;
                case MessageBoxButtons.OK:
                    ActivateCommandButton_Click(1, DialogResult.OK);
                    break;
                case MessageBoxButtons.OKCancel:
                    ActivateCommandButton_Click(1, DialogResult.Cancel);
                    ActivateCommandButton_Click(2, DialogResult.OK);
                    break;
                case MessageBoxButtons.RetryCancel:
                    ActivateCommandButton_Click(1, DialogResult.Cancel);
                    ActivateCommandButton_Click(2, DialogResult.Retry);
                    break;
                case MessageBoxButtons.YesNo:
                    ActivateCommandButton_Click(1, DialogResult.No);
                    ActivateCommandButton_Click(2, DialogResult.Yes);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    ActivateCommandButton_Click(1, DialogResult.Cancel);
                    ActivateCommandButton_Click(2, DialogResult.No);
                    ActivateCommandButton_Click(3, DialogResult.Yes);
                    break;
            }
            fncFilter.set_theme_color(this);
            fncFullScreen fncfullscreen = new fncFullScreen(this);
            fncfullscreen.ResizeFormsControls();
        }

        private void CommandButton_Click(object sender, EventArgs e)
        {
            DialogResult = ((Button)sender).DialogResult;
            Close();
        }

        private void ActivateCommandButton_Click(int commandButtonNumber, DialogResult dialogResult)
        {
            Button button = null;
            string tag = "";
            switch (commandButtonNumber)
            {
                case 1:
                    button = btnCommand1;
                    break;
                case 2:
                    button = btnCommand2;
                    break;
                case 3:
                    button = btnCommand3;
                    break;
            }
            button.Visible = true;
            button.Text = dialogResult.ToString() + " (" + ConvertDialogResultToKeyText(dialogResult, ref tag) + ")";
            button.Tag = tag;
            button.DialogResult = dialogResult;
        }
        private string ConvertDialogResultToKeyText(DialogResult dialogResult, ref string tag)
        {
            string keyText = "";
            switch (dialogResult)
            {
                case DialogResult.OK:
                    keyText = "F12";
                    tag = Keys.F12.ToString();
                    break;
                case DialogResult.Yes:
                    keyText = "F12";
                    tag = Keys.F12.ToString();
                    break;
                case DialogResult.No:
                    keyText = "ESC";
                    tag = Keys.Escape.ToString();
                    break;
                case DialogResult.Cancel:
                    keyText = "ENTER";
                    tag = Keys.Enter.ToString();
                    break;
            }
            return keyText;
        }
        private Button GetButtonByDialogResult(Keys keys)
        {
            Button button = null;
            string keyText = keys.ToString();
            if (btnCommand1.Tag.ToString() == keyText)
                button = btnCommand1;
            else if (btnCommand2.Tag.ToString() == keyText)
                button = btnCommand2;
            else if (btnCommand3.Tag.ToString() == keyText)
                button = btnCommand3;
            return button;
        }
    }
}
