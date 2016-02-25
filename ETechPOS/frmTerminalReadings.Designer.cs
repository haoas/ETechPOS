namespace ETech
{
    partial class frmTerminalReadings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbHourlySales = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.bgwLoadReceipt = new System.ComponentModel.BackgroundWorker();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.num_printcnt = new System.Windows.Forms.NumericUpDown();
            this.lbl_printcnt = new System.Windows.Forms.Label();
            this.DGVcashiers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.gbHourlySales.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_printcnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVcashiers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "FROM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "TO:";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Items.AddRange(new object[] {
            "HOURLY SALES",
            "DAILY SALES",
            "VAT SALES",
            "NONVAT SALES",
            "SENIOR SALES",
            "DEPARTMENT SALES",
            "ITEM SALES",
            "Z-READING",
            "CASHIER ACCOUNTABILITY",
            "TERMINAL ACCOUNTABILITY"});
            this.listBox1.Location = new System.Drawing.Point(12, 104);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(359, 204);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 24);
            this.label3.TabIndex = 46;
            this.label3.Text = "REPORTS:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(118, 15);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(253, 22);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Location = new System.Drawing.Point(118, 47);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(253, 22);
            this.dateTimePicker2.TabIndex = 2;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(178, 13);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 32);
            this.numericUpDown1.TabIndex = 47;
            this.numericUpDown1.Tag = "num";
            this.numericUpDown1.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.Location = new System.Drawing.Point(178, 50);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(61, 32);
            this.numericUpDown2.TabIndex = 48;
            this.numericUpDown2.Tag = "num";
            this.numericUpDown2.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 24);
            this.label4.TabIndex = 49;
            this.label4.Text = "HOUR START:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 24);
            this.label5.TabIndex = 50;
            this.label5.Text = "HOUR END:";
            // 
            // gbHourlySales
            // 
            this.gbHourlySales.Controls.Add(this.numericUpDown2);
            this.gbHourlySales.Controls.Add(this.label5);
            this.gbHourlySales.Controls.Add(this.numericUpDown1);
            this.gbHourlySales.Controls.Add(this.label4);
            this.gbHourlySales.Location = new System.Drawing.Point(12, 314);
            this.gbHourlySales.Name = "gbHourlySales";
            this.gbHourlySales.Size = new System.Drawing.Size(359, 91);
            this.gbHourlySales.TabIndex = 51;
            this.gbHourlySales.TabStop = false;
            this.gbHourlySales.Visible = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Location = new System.Drawing.Point(125, 487);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(112, 38);
            this.btnPreview.TabIndex = 108;
            this.btnPreview.Text = "View (F2)";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(13, 487);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(106, 38);
            this.btnPrint.TabIndex = 107;
            this.btnPrint.Text = "Print (F1)";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(243, 487);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(128, 38);
            this.btnESC.TabIndex = 109;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // bgwLoadReceipt
            // 
            this.bgwLoadReceipt.WorkerReportsProgress = true;
            this.bgwLoadReceipt.WorkerSupportsCancellation = true;
            this.bgwLoadReceipt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoadReceipt_RunWorkerCompleted);
            // 
            // pnlPreview
            // 
            this.pnlPreview.AutoScroll = true;
            this.pnlPreview.BackColor = System.Drawing.Color.White;
            this.pnlPreview.Controls.Add(this.pbPreview);
            this.pnlPreview.Location = new System.Drawing.Point(377, 16);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(313, 500);
            this.pnlPreview.TabIndex = 110;
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
            // num_printcnt
            // 
            this.num_printcnt.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_printcnt.Location = new System.Drawing.Point(176, 449);
            this.num_printcnt.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_printcnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_printcnt.Name = "num_printcnt";
            this.num_printcnt.Size = new System.Drawing.Size(61, 32);
            this.num_printcnt.TabIndex = 51;
            this.num_printcnt.Tag = "num";
            this.num_printcnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbl_printcnt
            // 
            this.lbl_printcnt.AutoSize = true;
            this.lbl_printcnt.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_printcnt.Location = new System.Drawing.Point(19, 455);
            this.lbl_printcnt.Name = "lbl_printcnt";
            this.lbl_printcnt.Size = new System.Drawing.Size(120, 24);
            this.lbl_printcnt.TabIndex = 52;
            this.lbl_printcnt.Text = "Print Count:";
            // 
            // DGVcashiers
            // 
            this.DGVcashiers.AllowUserToAddRows = false;
            this.DGVcashiers.AllowUserToDeleteRows = false;
            this.DGVcashiers.AllowUserToOrderColumns = true;
            this.DGVcashiers.AllowUserToResizeColumns = false;
            this.DGVcashiers.AllowUserToResizeRows = false;
            this.DGVcashiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVcashiers.Location = new System.Drawing.Point(13, 314);
            this.DGVcashiers.Name = "DGVcashiers";
            this.DGVcashiers.ReadOnly = true;
            this.DGVcashiers.RowHeadersVisible = false;
            this.DGVcashiers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVcashiers.Size = new System.Drawing.Size(358, 129);
            this.DGVcashiers.TabIndex = 111;
            this.DGVcashiers.Visible = false;
            // 
            // frmTerminalReadings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 526);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.DGVcashiers);
            this.Controls.Add(this.num_printcnt);
            this.Controls.Add(this.lbl_printcnt);
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.gbHourlySales);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmTerminalReadings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal Readings";
            this.Load += new System.EventHandler(this.frmTerminalReadings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTerminalReadings_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.gbHourlySales.ResumeLayout(false);
            this.gbHourlySales.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_printcnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVcashiers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbHourlySales;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnESC;
        private System.ComponentModel.BackgroundWorker bgwLoadReceipt;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.NumericUpDown num_printcnt;
        private System.Windows.Forms.Label lbl_printcnt;
        private System.Windows.Forms.DataGridView DGVcashiers;
    }
}