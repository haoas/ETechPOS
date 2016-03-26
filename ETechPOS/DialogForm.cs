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
using ETECHPOS.Helpers;
using ETech;

namespace ETECHPOS
{
    public partial class DialogForm : Form
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
            _Message = message;
            _MessageBoxButtons = messageBoxButtons;
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

        private void btnCommand1_Click(object sender, EventArgs e)
        {
            CommandButton_Click((Button)sender);
        }
        private void btnCommand2_Click(object sender, EventArgs e)
        {
            CommandButton_Click((Button)sender);
        }
        private void btnCommand3_Click(object sender, EventArgs e)
        {
            CommandButton_Click((Button)sender);
        }

        private void CommandButton_Click(Button button)
        {
            DialogResult = button.DialogResult;
            Close();
        }
        private void ActivateCommandButton_Click(int commandButtonNumber, DialogResult dialogResult)
        {
            Button button = null;
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
            button.Text = dialogResult.ToString();
            button.DialogResult = dialogResult;
        }
    }
}
