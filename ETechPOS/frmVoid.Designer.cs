namespace ETech
{
    partial class frmVoid
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
            this.txtORNumber_d = new System.Windows.Forms.TextBox();
            this.lblORNumber = new System.Windows.Forms.Label();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnVoid = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtORNumber_d
            // 
            this.txtORNumber_d.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORNumber_d.Location = new System.Drawing.Point(108, 5);
            this.txtORNumber_d.MaxLength = 20;
            this.txtORNumber_d.Name = "txtORNumber_d";
            this.txtORNumber_d.Size = new System.Drawing.Size(313, 41);
            this.txtORNumber_d.TabIndex = 14;
            this.txtORNumber_d.Tag = "num";
            this.txtORNumber_d.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtORNumber_d_KeyPress);
            // 
            // lblORNumber
            // 
            this.lblORNumber.AutoSize = true;
            this.lblORNumber.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber.Location = new System.Drawing.Point(12, 9);
            this.lblORNumber.Name = "lblORNumber";
            this.lblORNumber.Size = new System.Drawing.Size(90, 36);
            this.lblORNumber.TabIndex = 13;
            this.lblORNumber.Text = "OR#:";
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(282, 52);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 34;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnVoid
            // 
            this.btnVoid.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoid.Location = new System.Drawing.Point(156, 52);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(120, 38);
            this.btnVoid.TabIndex = 33;
            this.btnVoid.Text = "Void (F1)";
            this.btnVoid.UseVisualStyleBackColor = false;
            this.btnVoid.Click += new System.EventHandler(this.btnVoid_Click);
            // 
            // frmVoid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 100);
            this.Controls.Add(this.btnVoid);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtORNumber_d);
            this.Controls.Add(this.lblORNumber);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmVoid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Void Transaction";
            this.Load += new System.EventHandler(this.frmVoid_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVoid_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtORNumber_d;
        private System.Windows.Forms.Label lblORNumber;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnVoid;
    }
}