namespace ETech
{
    partial class frmSalesmemo
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
            this.txtMemo_d = new System.Windows.Forms.TextBox();
            this.BtnF12 = new System.Windows.Forms.Button();
            this.BtnESC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMemo_d
            // 
            this.txtMemo_d.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemo_d.Location = new System.Drawing.Point(24, 77);
            this.txtMemo_d.MaxLength = 30000;
            this.txtMemo_d.Multiline = true;
            this.txtMemo_d.Name = "txtMemo_d";
            this.txtMemo_d.Size = new System.Drawing.Size(495, 165);
            this.txtMemo_d.TabIndex = 18;
            this.txtMemo_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BtnF12
            // 
            this.BtnF12.BackColor = System.Drawing.Color.White;
            this.BtnF12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnF12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnF12.ForeColor = System.Drawing.Color.Black;
            this.BtnF12.Location = new System.Drawing.Point(525, 162);
            this.BtnF12.Name = "BtnF12";
            this.BtnF12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnF12.Size = new System.Drawing.Size(100, 80);
            this.BtnF12.TabIndex = 120;
            this.BtnF12.Text = "[ F12 ]\r\nPROCEED";
            this.BtnF12.UseVisualStyleBackColor = false;
            this.BtnF12.Click += new System.EventHandler(this.BtnF12_Click);
            // 
            // BtnESC
            // 
            this.BtnESC.BackColor = System.Drawing.Color.White;
            this.BtnESC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnESC.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnESC.ForeColor = System.Drawing.Color.Black;
            this.BtnESC.Location = new System.Drawing.Point(525, 76);
            this.BtnESC.Name = "BtnESC";
            this.BtnESC.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.BtnESC.Size = new System.Drawing.Size(100, 80);
            this.BtnESC.TabIndex = 119;
            this.BtnESC.Text = "[ ESC ]\r\nCANCEL";
            this.BtnESC.UseVisualStyleBackColor = false;
            this.BtnESC.Click += new System.EventHandler(this.BtnESC_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(180, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 51);
            this.label1.TabIndex = 121;
            this.label1.Text = "Transaction Memo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmSalesmemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 257);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnF12);
            this.Controls.Add(this.BtnESC);
            this.Controls.Add(this.txtMemo_d);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSalesmemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sales Memo";
            this.Load += new System.EventHandler(this.frmRefundmemo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRefundmemo_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtMemo_d;
        private System.Windows.Forms.Button BtnF12;
        private System.Windows.Forms.Button BtnESC;
        private System.Windows.Forms.Label label1;
    }
}