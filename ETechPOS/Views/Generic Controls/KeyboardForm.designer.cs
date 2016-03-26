namespace ETech.Views.Generic_Controls
{
    partial class KeyboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardForm));
            this.keyboardControl1 = new ETech.Views.Generic_Controls.KeyboardControl();
            this.SuspendLayout();
            // 
            // keyboardControl1
            // 
            this.keyboardControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardControl1.BackColor = System.Drawing.Color.Transparent;
            this.keyboardControl1.Capslock = false;
            this.keyboardControl1.Font = new System.Drawing.Font("Calibri", 12F);
            this.keyboardControl1.HorizontalButtonSpacing = 0;
            this.keyboardControl1.Location = new System.Drawing.Point(4, 2);
            this.keyboardControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.keyboardControl1.Name = "keyboardControl1";
            this.keyboardControl1.Shift = false;
            this.keyboardControl1.Size = new System.Drawing.Size(793, 209);
            this.keyboardControl1.Symbols = false;
            this.keyboardControl1.TabIndex = 0;
            this.keyboardControl1.VerticalButtonSpacing = 0;
            this.keyboardControl1.Load += new System.EventHandler(this.keyboardControl1_Load);
            // 
            // KeyboardForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 213);
            this.Controls.Add(this.keyboardControl1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Name = "KeyboardForm";
            this.Text = "Keyboard";
            this.ResumeLayout(false);

        }

        #endregion

        private KeyboardControl keyboardControl1;
    }
}