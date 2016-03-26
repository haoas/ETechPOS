using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETech.Views.Generic_Controls
{
    public partial class NumericKeypadControl : UserControl
    {
        private int _horizontalButtonSpacing;
        private int _verticalButtonSpacing;
        public event EventHandler HorizontalButtonSpacingChanged, VerticalButtonSpacingChanged;

        private class UnfocusableButton : Button
        {
            public UnfocusableButton()
            {
                SetStyle(ControlStyles.Selectable, false);
            }
        }
        public NumericKeypadControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, false);
            SizeChanged += (sender, e) => { ResizeButtons(); };
            HorizontalButtonSpacingChanged += (sender, e) => { ResizeButtons(); };
            VerticalButtonSpacingChanged += (sender, e) => { ResizeButtons(); };
            FontChanged += (sender, e) => { UpdateFont(); ResizeButtons(); };
        }
        private void UpdateFont()
        {
            foreach (Control control in Controls)
                control.Font = Font;
        }
        private void ResizeButtons()
        {
            int regularButtonHeight = (Height - _verticalButtonSpacing * 5)/4;
            int regularButtonWidth = (Width - _horizontalButtonSpacing * 5)/4;

            btn7.Location = new Point(_horizontalButtonSpacing, _verticalButtonSpacing);
            btn8.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing), _verticalButtonSpacing);
            btn9.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing)*2, _verticalButtonSpacing);
            btnMinus.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 3, _verticalButtonSpacing);

            btn4.Location = new Point(_horizontalButtonSpacing, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btn5.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing), _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btn6.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnBackspace.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 3, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);

            btn1.Location = new Point(_horizontalButtonSpacing, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btn2.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing), _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btn3.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnEnter.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 3, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);

            btn0.Location = new Point(_horizontalButtonSpacing, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);
            btnDecimalPoint.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);

            Size regularButtonSize = new Size(regularButtonWidth, regularButtonHeight);
            btn1.Size = regularButtonSize;
            btn2.Size = regularButtonSize;
            btn3.Size = regularButtonSize;
            btn4.Size = regularButtonSize;
            btn5.Size = regularButtonSize;
            btn6.Size = regularButtonSize;
            btn7.Size = regularButtonSize;
            btn8.Size = regularButtonSize;
            btn9.Size = regularButtonSize;
            btnDecimalPoint.Size = regularButtonSize;
            btnBackspace.Size = regularButtonSize;
            btnMinus.Size = regularButtonSize;

            btn0.Size = new Size(regularButtonWidth * 2 + _horizontalButtonSpacing, regularButtonHeight);
            btnEnter.Size = new Size(regularButtonWidth, regularButtonHeight * 2 + _verticalButtonSpacing);

        }
        private void btn0_Click(object sender, EventArgs e)
        {
            SendKeys.Send("0");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            SendKeys.Send("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            SendKeys.Send("9");
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }

        private void btnDecimalPoint_Click(object sender, EventArgs e)
        {
            SendKeys.Send(".");
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            SendKeys.Send("-");
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");
        }

        public void OnVerticalButtonSpacingChanged() { VerticalButtonSpacingChanged(this, new EventArgs()); }
        public void OnHorizontalButtonSpacingChanged() { HorizontalButtonSpacingChanged(this, new EventArgs()); }
        public int HorizontalButtonSpacing
        {
            get { return _horizontalButtonSpacing; }
            set { _horizontalButtonSpacing = value; OnHorizontalButtonSpacingChanged(); }
        }
        public int VerticalButtonSpacing
        {
            get { return _verticalButtonSpacing; }
            set { _verticalButtonSpacing = value; OnVerticalButtonSpacingChanged(); }
        }

    }
}
