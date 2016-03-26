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
    public partial class KeyboardControl : UserControl
    {
        private bool _shift , _symbols, _capslock;
        private int _horizontalButtonSpacing;
        private int _verticalButtonSpacing;
        public event EventHandler HorizontalButtonSpacingChanged, VerticalButtonSpacingChanged;
        public event EventHandler ShiftChanged, SymbolsChanged, CapslockChanged;

        public KeyboardControl()
        {
            _shift = false;
            InitializeComponent();

            //Initialize Keys
            btnQ.KeyText = btnQ.Key = "q";
            btnW.KeyText = btnW.Key = "w";
            btnE.KeyText = btnE.Key = "e";
            btnR.KeyText = btnR.Key = "r";
            btnT.KeyText = btnT.Key = "t";
            btnY.KeyText = btnY.Key = "y";
            btnU.KeyText = btnU.Key = "u";
            btnI.KeyText = btnI.Key = "i";
            btnO.KeyText = btnO.Key = "o";
            btnP.KeyText = btnP.Key = "p";
            btnBackspace.Key = "{BACKSPACE}";

            btnA.KeyText = btnA.Key = "a";
            btnS.KeyText = btnS.Key = "s";
            btnD.KeyText = btnD.Key = "d";
            btnF.KeyText = btnF.Key = "f";
            btnG.KeyText = btnG.Key = "g";
            btnH.KeyText = btnH.Key = "h";
            btnJ.KeyText = btnJ.Key = "j";
            btnK.KeyText = btnK.Key = "k";
            btnL.KeyText = btnL.Key = "l";
            btnEnter.Key = "{ENTER}";

            btnLeftShift.KeyText = "Shift"; btnLeftShift.Key = "{SHIFT}";
            btnZ.KeyText = btnZ.Key = "z";
            btnX.KeyText = btnX.Key = "x";
            btnC.KeyText = btnC.Key = "c";
            btnV.KeyText = btnV.Key = "v";
            btnB.KeyText = btnB.Key = "b";
            btnN.KeyText = btnN.Key = "n";
            btnM.KeyText = btnM.Key = "m";
            btnComma.KeyText = btnComma.Key = ",";
            btnPeriod.KeyText = btnPeriod.Key = ".";
            btnRightShift.KeyText = "Shift"; btnRightShift.Key = "{SHIFT}";

            btnCapslock.KeyText = "CapsLk"; btnCapslock.Key = "{CAPSLOCK}";
            btnSymbols.KeyText = "&&123"; btnSymbols.Key = "{SYMBOLS}";
            btnSpace.Key = " ";
            btnExclamationPoint.KeyText = btnExclamationPoint.Key = "!";
            btnQuestionMark.KeyText = btnQuestionMark.Key = "?";

            //Initialize Alternate Keys
            btnQ.AlternateKeyText = btnQ.AlternateKey = "1";
            btnW.AlternateKeyText = btnW.AlternateKey = "2";
            btnE.AlternateKeyText = btnE.AlternateKey = "3";
            btnR.AlternateKeyText = btnR.AlternateKey = "4";
            btnT.AlternateKeyText = btnT.AlternateKey = "5";
            btnY.AlternateKeyText = btnY.AlternateKey = "6";
            btnU.AlternateKeyText = btnU.AlternateKey = "7";
            btnI.AlternateKeyText = btnI.AlternateKey = "8";
            btnO.AlternateKeyText = btnO.AlternateKey = "9";
            btnP.AlternateKeyText = btnP.AlternateKey = "0";
            btnBackspace.AlternateKey = "";

            btnA.AlternateKeyText = btnA.AlternateKey = "@";
            btnS.AlternateKeyText = btnS.AlternateKey = "#";
            btnD.AlternateKeyText = "&&"; btnD.AlternateKey = "&";
            btnF.AlternateKeyText = btnF.AlternateKey = "*";
            btnG.AlternateKeyText = "-"; btnG.AlternateKey = "{SUBTRACT}";
            btnH.AlternateKeyText = "+"; btnH.AlternateKey = "{ADD}";
            btnJ.AlternateKeyText = "="; btnJ.AlternateKey = "=";
            btnK.AlternateKeyText = "("; btnK.AlternateKey = "{(}";
            btnL.AlternateKeyText = ")"; btnL.AlternateKey = "{)}";
            btnEnter.AlternateKey = "";

            btnLeftShift.AlternateKeyText = "Shift"; btnLeftShift.AlternateKey = "{SHIFT}";
            btnZ.AlternateKeyText = btnZ.AlternateKey = "_";
            btnX.AlternateKeyText = btnX.AlternateKey = "$";
            btnC.AlternateKeyText = btnC.AlternateKey = "\"";
            btnV.AlternateKeyText = btnV.AlternateKey = "\'";
            btnB.AlternateKeyText = btnB.AlternateKey = ":";
            btnN.AlternateKeyText = btnN.AlternateKey = ";";
            btnM.AlternateKeyText = btnM.AlternateKey = "/";
            btnComma.AlternateKeyText = btnComma.AlternateKey = ",";
            btnPeriod.AlternateKeyText = btnPeriod.AlternateKey = ".";

            btnSymbols.AlternateKeyText = "ABC"; btnSymbols.AlternateKey = "{SYMBOLS}";

            SetStyle(ControlStyles.Selectable, false);

            //Initialize Event Handlers
            ShiftChanged += (sender, e) => { UpdateKeysFromShift(); };
            SymbolsChanged += (sender, e) => { UpdateKeysFromSymbolKey(); };
            SizeChanged += (sender, e) => { ResizeButtons(); };
            HorizontalButtonSpacingChanged += (sender, e) => { ResizeButtons(); };
            VerticalButtonSpacingChanged += (sender, e) => { ResizeButtons(); };
            FontChanged += (sender, e) => { UpdateFont(); ResizeButtons(); };
            CapslockChanged += (sender, e) => { UpdateCapslockKeys(); };
        }
        private void UpdateCapslockKeys()
        {
            Font font = btnCapslock.Font; 
            if (_capslock)
                font = new Font(font.FontFamily, font.Size, font.Style | FontStyle.Underline);
            else
                font = new Font(font.FontFamily, font.Size, font.Style & ~FontStyle.Underline);
            btnCapslock.Font = font;
            if(!_symbols)
                UpdateKeysFromShift();
        }
        private void UpdateFont()
        {
            foreach (Control control in Controls)
                control.Font = Font;
        }
        private void UpdateKeysFromShift()
        {
            foreach (Control control in Controls)
                if (control is KeyboardButton)
                {
                    KeyboardButton keyboardButton = control as KeyboardButton;
                    if (keyboardButton.Key.ToCharArray()[0] >= 'a' && keyboardButton.Key.ToCharArray()[0] <= 'z')
                    {
                        if ((_shift ^ _capslock))
                            keyboardButton.Text = keyboardButton.KeyText.ToUpper();
                        else
                            keyboardButton.Text = keyboardButton.KeyText.ToLower();
                    }
                }
        }
        private void UpdateKeysFromSymbolKey()
        {
            foreach (Control control in Controls)
                if (control is KeyboardButton)
                {
                    KeyboardButton keyboardButton = control as KeyboardButton;
                    if (!keyboardButton.AlternateKey.Equals(""))
                    {
                        if (_symbols)
                            keyboardButton.Text = keyboardButton.AlternateKeyText;
                        else if (keyboardButton.Key.ToCharArray()[0] >= 'a' && keyboardButton.Key.ToCharArray()[0] <= 'z' && (_shift ^ _capslock))
                            keyboardButton.Text = keyboardButton.KeyText.ToUpper();
                        else
                            keyboardButton.Text = keyboardButton.KeyText;
                    }
                }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            KeyboardButton button = sender as KeyboardButton;
            if (button.Key.Equals("{SYMBOLS}"))
            {
                Symbols = !_symbols;
            } 
            else if(button.Key.Equals("{CAPSLOCK}"))
            {
                Capslock = !_capslock;
            }
            else if (button.Key.Equals("{SHIFT}"))
            {
                if (!_symbols)
                {
                    Shift = !_shift;
                }
            }
            else
            {
                //TO DO: Improve Logic
                if (_symbols)
                {
                    if(button.AlternateKey.Equals(""))
                        SendKeys.SendWait(button.Key.ToString());
                    else
                        SendKeys.SendWait(button.AlternateKey.ToString());
                }
                else if (_shift ^ _capslock && button.Key != "," && button.Key != ".")
                    SendKeys.SendWait("+" + button.Key);
                else
                    SendKeys.SendWait(button.Key);
                Shift = false;
            }
        }
        private void ResizeButtons()
        {
            int regularButtonHeight = (Height - _verticalButtonSpacing * 5)/4;
            int regularButtonWidth = (Width - _horizontalButtonSpacing * 5)/11;

            btnQ.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 0, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnW.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 1, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnE.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnR.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 3, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnT.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 4, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnY.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 5, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnU.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 6, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnI.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 7, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnO.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 8, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnP.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 9, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);
            btnBackspace.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 10, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 0);

            btnA.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 0, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnS.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 1, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnD.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnF.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 3, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnG.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 4, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnH.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 5, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnJ.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 6, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnK.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 7, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnL.Location = new Point(_horizontalButtonSpacing + regularButtonWidth/2 + (regularButtonWidth + _horizontalButtonSpacing) * 8, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);
            btnEnter.Location = new Point(_horizontalButtonSpacing + regularButtonWidth / 2 + (regularButtonWidth + _horizontalButtonSpacing) * 9, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 1);

            btnLeftShift.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 0, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnZ.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 1, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnX.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnC.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 3, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnV.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 4, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnB.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 5, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnN.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 6, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnM.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 7, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnComma.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 8, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnPeriod.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 9, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);
            btnRightShift.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 10, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 2);

            btnCapslock.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 0, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);
            btnSymbols.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 1, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);
            btnSpace.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 2, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);
            btnExclamationPoint.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 9, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);
            btnQuestionMark.Location = new Point(_horizontalButtonSpacing + (regularButtonWidth + _horizontalButtonSpacing) * 10, _verticalButtonSpacing + (regularButtonHeight + _verticalButtonSpacing) * 3);

            Size regularButtonSize = new Size(regularButtonWidth, regularButtonHeight);
            btnQ.Size = regularButtonSize;
            btnW.Size = regularButtonSize;
            btnE.Size = regularButtonSize;
            btnR.Size = regularButtonSize;
            btnT.Size = regularButtonSize;
            btnY.Size = regularButtonSize;
            btnU.Size = regularButtonSize;
            btnI.Size = regularButtonSize;
            btnO.Size = regularButtonSize;
            btnP.Size = regularButtonSize;
            btnBackspace.Size = regularButtonSize;

            btnA.Size = regularButtonSize;
            btnS.Size = regularButtonSize;
            btnD.Size = regularButtonSize;
            btnF.Size = regularButtonSize;
            btnG.Size = regularButtonSize;
            btnH.Size = regularButtonSize;
            btnJ.Size = regularButtonSize;
            btnK.Size = regularButtonSize;
            btnL.Size = regularButtonSize;
            btnEnter.Size = new Size((int)(regularButtonWidth * 1.5f) + _horizontalButtonSpacing, regularButtonHeight);

            btnLeftShift.Size = regularButtonSize;
            btnZ.Size = regularButtonSize;
            btnX.Size = regularButtonSize;
            btnC.Size = regularButtonSize;
            btnV.Size = regularButtonSize;
            btnB.Size = regularButtonSize;
            btnN.Size = regularButtonSize;
            btnM.Size = regularButtonSize;
            btnComma.Size = regularButtonSize;
            btnPeriod.Size = regularButtonSize;
            btnRightShift.Size = regularButtonSize;

            btnCapslock.Size = regularButtonSize;
            btnSymbols.Size = regularButtonSize;
            btnSpace.Size = new Size(((regularButtonWidth) + _horizontalButtonSpacing) * 7 - _horizontalButtonSpacing, regularButtonHeight);
            btnExclamationPoint.Size = regularButtonSize;
            btnQuestionMark.Size = regularButtonSize;


            //btn0.Size = new Size(regularButtonWidth * 2 + _horizontalButtonSpacing, regularButtonHeight);

        }

        #region EventInvokers
        public void OnVerticalButtonSpacingChanged() { VerticalButtonSpacingChanged(this, new EventArgs()); }
        public void OnHorizontalButtonSpacingChanged() { HorizontalButtonSpacingChanged(this, new EventArgs()); }
        #endregion

        #region SettersAndGetters
        public bool Shift
        {
            set
            {
                if (_shift != value)
                {
                    _shift = value;
                    ShiftChanged(this, new EventArgs());
                }
            }
            get { return _shift; }
        }

        public bool Symbols
        {
            set
            {
                if (_symbols != value)
                {
                    _symbols = value;
                    SymbolsChanged(this, new EventArgs());
                }
            }
            get { return _shift; }
        }

        public bool Capslock
        {
            set
            {
                if (_capslock != value)
                {
                    _capslock = value;
                    CapslockChanged(this, new EventArgs());
                }
            }
            get { return _capslock; }
        }
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
        #endregion

        private class KeyboardButton : Button
        {
            public string Key = "";
            public string KeyText = "";
            public string AlternateKeyText = "";
            public string AlternateKey = "";
            public KeyboardButton()
            {
                SetStyle(ControlStyles.Selectable, false);
            }
        }
    }
}
