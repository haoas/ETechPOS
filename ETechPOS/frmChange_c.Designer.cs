namespace ETech
{
    partial class frmChange_c
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
            this.lblChange_d = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChange_d
            // 
            this.lblChange_d.Font = new System.Drawing.Font("Arial Narrow", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange_d.ForeColor = System.Drawing.Color.Green;
            this.lblChange_d.Location = new System.Drawing.Point(254, 9);
            this.lblChange_d.Name = "lblChange_d";
            this.lblChange_d.Size = new System.Drawing.Size(297, 121);
            this.lblChange_d.TabIndex = 21;
            this.lblChange_d.Text = "P 0.00";
            this.lblChange_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("Arial", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.Location = new System.Drawing.Point(12, 9);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(236, 121);
            this.lblChange.TabIndex = 19;
            this.lblChange.Text = "Change:";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(412, 143);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 104;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // frmChange_c
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 191);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.lblChange_d);
            this.Controls.Add(this.lblChange);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmChange_c";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change";
            this.Load += new System.EventHandler(this.frmChange_c_Load);
            this.Shown += new System.EventHandler(this.frmChange_c_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChange_c_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblChange_d;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnESC;
    }
}