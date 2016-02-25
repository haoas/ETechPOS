namespace ETech
{
    partial class frmSetting2
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
            this.lblPosdPercentTxt = new System.Windows.Forms.Label();
            this.chkIsAutoXZ = new System.Windows.Forms.CheckBox();
            this.nudPOSDPercent = new System.Windows.Forms.NumericUpDown();
            this.lblPosdMininumTxt = new System.Windows.Forms.Label();
            this.lblPosdMaximumTxt = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.nudPosdMininum = new System.Windows.Forms.NumericUpDown();
            this.nudPosdMaximum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudPOSDPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosdMininum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosdMaximum)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPosdPercentTxt
            // 
            this.lblPosdPercentTxt.AutoSize = true;
            this.lblPosdPercentTxt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosdPercentTxt.Location = new System.Drawing.Point(8, 37);
            this.lblPosdPercentTxt.Name = "lblPosdPercentTxt";
            this.lblPosdPercentTxt.Size = new System.Drawing.Size(67, 17);
            this.lblPosdPercentTxt.TabIndex = 5;
            this.lblPosdPercentTxt.Text = "Percent :";
            // 
            // chkIsAutoXZ
            // 
            this.chkIsAutoXZ.AutoSize = true;
            this.chkIsAutoXZ.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsAutoXZ.Location = new System.Drawing.Point(11, 9);
            this.chkIsAutoXZ.Name = "chkIsAutoXZ";
            this.chkIsAutoXZ.Size = new System.Drawing.Size(121, 20);
            this.chkIsAutoXZ.TabIndex = 0;
            this.chkIsAutoXZ.Text = "XZ-read Enabled";
            this.chkIsAutoXZ.UseVisualStyleBackColor = true;
            // 
            // nudPOSDPercent
            // 
            this.nudPOSDPercent.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPOSDPercent.Location = new System.Drawing.Point(118, 35);
            this.nudPOSDPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPOSDPercent.Name = "nudPOSDPercent";
            this.nudPOSDPercent.Size = new System.Drawing.Size(56, 25);
            this.nudPOSDPercent.TabIndex = 1;
            this.nudPOSDPercent.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblPosdMininumTxt
            // 
            this.lblPosdMininumTxt.AutoSize = true;
            this.lblPosdMininumTxt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosdMininumTxt.Location = new System.Drawing.Point(8, 68);
            this.lblPosdMininumTxt.Name = "lblPosdMininumTxt";
            this.lblPosdMininumTxt.Size = new System.Drawing.Size(38, 17);
            this.lblPosdMininumTxt.TabIndex = 25;
            this.lblPosdMininumTxt.Text = "Min :";
            // 
            // lblPosdMaximumTxt
            // 
            this.lblPosdMaximumTxt.AutoSize = true;
            this.lblPosdMaximumTxt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosdMaximumTxt.Location = new System.Drawing.Point(8, 99);
            this.lblPosdMaximumTxt.Name = "lblPosdMaximumTxt";
            this.lblPosdMaximumTxt.Size = new System.Drawing.Size(42, 17);
            this.lblPosdMaximumTxt.TabIndex = 26;
            this.lblPosdMaximumTxt.Text = "Max :";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(11, 128);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(163, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save (F1)";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // nudPosdMininum
            // 
            this.nudPosdMininum.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPosdMininum.Location = new System.Drawing.Point(56, 66);
            this.nudPosdMininum.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.nudPosdMininum.Name = "nudPosdMininum";
            this.nudPosdMininum.Size = new System.Drawing.Size(118, 25);
            this.nudPosdMininum.TabIndex = 2;
            // 
            // nudPosdMaximum
            // 
            this.nudPosdMaximum.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPosdMaximum.Location = new System.Drawing.Point(56, 97);
            this.nudPosdMaximum.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.nudPosdMaximum.Name = "nudPosdMaximum";
            this.nudPosdMaximum.Size = new System.Drawing.Size(118, 25);
            this.nudPosdMaximum.TabIndex = 3;
            // 
            // frmSetting2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 174);
            this.Controls.Add(this.nudPosdMaximum);
            this.Controls.Add(this.nudPosdMininum);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblPosdMaximumTxt);
            this.Controls.Add(this.lblPosdMininumTxt);
            this.Controls.Add(this.nudPOSDPercent);
            this.Controls.Add(this.chkIsAutoXZ);
            this.Controls.Add(this.lblPosdPercentTxt);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSetting2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.frmSetting2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSetting2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nudPOSDPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosdMininum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosdMaximum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPosdPercentTxt;
        private System.Windows.Forms.CheckBox chkIsAutoXZ;
        private System.Windows.Forms.NumericUpDown nudPOSDPercent;
        private System.Windows.Forms.Label lblPosdMininumTxt;
        private System.Windows.Forms.Label lblPosdMaximumTxt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown nudPosdMininum;
        private System.Windows.Forms.NumericUpDown nudPosdMaximum;
    }
}