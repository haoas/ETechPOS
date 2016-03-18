namespace ETech
{
    partial class frmORPrintPreview
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
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.bgwLoadReceipt = new System.ComponentModel.BackgroundWorker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtORNumber_d
            // 
            this.txtORNumber_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORNumber_d.Location = new System.Drawing.Point(108, 8);
            this.txtORNumber_d.MaxLength = 20;
            this.txtORNumber_d.Name = "txtORNumber_d";
            this.txtORNumber_d.Size = new System.Drawing.Size(340, 40);
            this.txtORNumber_d.TabIndex = 10;
            this.txtORNumber_d.Tag = "num";
            this.txtORNumber_d.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtORNumber_d_KeyDown);
            // 
            // lblORNumber
            // 
            this.lblORNumber.AutoSize = true;
            this.lblORNumber.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblORNumber.Location = new System.Drawing.Point(12, 9);
            this.lblORNumber.Name = "lblORNumber";
            this.lblORNumber.Size = new System.Drawing.Size(90, 36);
            this.lblORNumber.TabIndex = 9;
            this.lblORNumber.Text = "OR#:";
            // 
            // pnlPreview
            // 
            this.pnlPreview.AutoScroll = true;
            this.pnlPreview.BackColor = System.Drawing.Color.White;
            this.pnlPreview.Controls.Add(this.pbPreview);
            this.pnlPreview.Location = new System.Drawing.Point(9, 52);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(313, 500);
            this.pnlPreview.TabIndex = 13;
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.White;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(296, 500);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            // 
            // bgwLoadReceipt
            // 
            this.bgwLoadReceipt.WorkerReportsProgress = true;
            this.bgwLoadReceipt.WorkerSupportsCancellation = true;
            this.bgwLoadReceipt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadReceipt_DoWork);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(328, 130);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 72);
            this.btnPrint.TabIndex = 104;
            this.btnPrint.Text = "Print\r\n(F1)";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(328, 208);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(120, 72);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Location = new System.Drawing.Point(328, 52);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(120, 72);
            this.btnPreview.TabIndex = 106;
            this.btnPreview.Text = "Preview (Enter)";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // frmORPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 564);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtORNumber_d);
            this.Controls.Add(this.lblORNumber);
            this.Controls.Add(this.pnlPreview);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmORPrintPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OR Print Preview";
            this.Load += new System.EventHandler(this.ORPrintPreview_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ORPrintPreview_KeyDown);
            this.pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtORNumber_d;
        private System.Windows.Forms.Label lblORNumber;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.ComponentModel.BackgroundWorker bgwLoadReceipt;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnPreview;
    }
}