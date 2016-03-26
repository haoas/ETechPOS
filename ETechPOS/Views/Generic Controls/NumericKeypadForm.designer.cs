namespace ETech.Views.Generic_Controls
{
    partial class NumericKeypadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumericKeypadForm));
            this.numericKeypadControl1 = new ETech.Views.Generic_Controls.NumericKeypadControl();
            this.SuspendLayout();
            // 
            // numericKeypadControl1
            // 
            this.numericKeypadControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericKeypadControl1.BackColor = System.Drawing.Color.Transparent;
            this.numericKeypadControl1.Font = new System.Drawing.Font("Calibri", 12F);
            this.numericKeypadControl1.HorizontalButtonSpacing = 0;
            this.numericKeypadControl1.Location = new System.Drawing.Point(3, 3);
            this.numericKeypadControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericKeypadControl1.Name = "numericKeypadControl1";
            this.numericKeypadControl1.Size = new System.Drawing.Size(316, 271);
            this.numericKeypadControl1.TabIndex = 0;
            this.numericKeypadControl1.VerticalButtonSpacing = 0;
            this.numericKeypadControl1.Load += new System.EventHandler(this.numericKeypadControlControl1_Load);
            // 
            // NumericKeypadForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(321, 277);
            this.Controls.Add(this.numericKeypadControl1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Name = "NumericKeypadForm";
            this.Text = "Numeric Keypad";
            this.ResumeLayout(false);

        }

        #endregion

        private NumericKeypadControl numericKeypadControl1;
    }
}