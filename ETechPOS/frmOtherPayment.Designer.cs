namespace ETech
{
    partial class frmOtherPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtMemo_d = new System.Windows.Forms.TextBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.lblTotalAmount_d = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.dgvGCInfo = new System.Windows.Forms.DataGridView();
            this.colRefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colexpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRefNo_d = new System.Windows.Forms.TextBox();
            this.txtAmount_d = new System.Windows.Forms.TextBox();
            this.lblValidThru = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGCInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo_d
            // 
            this.txtMemo_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemo_d.Location = new System.Drawing.Point(235, 180);
            this.txtMemo_d.MaxLength = 45;
            this.txtMemo_d.Name = "txtMemo_d";
            this.txtMemo_d.Size = new System.Drawing.Size(262, 39);
            this.txtMemo_d.TabIndex = 4;
            this.txtMemo_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemo.Location = new System.Drawing.Point(13, 183);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(98, 32);
            this.lblMemo.TabIndex = 38;
            this.lblMemo.Text = "Memo:";
            // 
            // lblTotalAmount_d
            // 
            this.lblTotalAmount_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount_d.Location = new System.Drawing.Point(271, 439);
            this.lblTotalAmount_d.Name = "lblTotalAmount_d";
            this.lblTotalAmount_d.Size = new System.Drawing.Size(226, 43);
            this.lblTotalAmount_d.TabIndex = 34;
            this.lblTotalAmount_d.Text = "0.00";
            this.lblTotalAmount_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(15, 444);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(211, 38);
            this.lblTotalAmount.TabIndex = 33;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // dgvGCInfo
            // 
            this.dgvGCInfo.AllowUserToAddRows = false;
            this.dgvGCInfo.AllowUserToDeleteRows = false;
            this.dgvGCInfo.AllowUserToResizeColumns = false;
            this.dgvGCInfo.AllowUserToResizeRows = false;
            this.dgvGCInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGCInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGCInfo.ColumnHeadersHeight = 40;
            this.dgvGCInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGCInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRefNo,
            this.colAmount,
            this.colexpdate,
            this.colMemo});
            this.dgvGCInfo.Location = new System.Drawing.Point(19, 230);
            this.dgvGCInfo.Name = "dgvGCInfo";
            this.dgvGCInfo.ReadOnly = true;
            this.dgvGCInfo.RowHeadersVisible = false;
            this.dgvGCInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGCInfo.Size = new System.Drawing.Size(478, 162);
            this.dgvGCInfo.TabIndex = 31;
            // 
            // colRefNo
            // 
            this.colRefNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colRefNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colRefNo.FillWeight = 14.65593F;
            this.colRefNo.HeaderText = "Reference No";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.ReadOnly = true;
            this.colRefNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRefNo.Width = 165;
            // 
            // colAmount
            // 
            this.colAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colAmount.FillWeight = 187.5959F;
            this.colAmount.HeaderText = "Amount";
            this.colAmount.MaxInputLength = 10;
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 104;
            // 
            // colexpdate
            // 
            this.colexpdate.HeaderText = "Exp Date";
            this.colexpdate.Name = "colexpdate";
            this.colexpdate.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colMemo.FillWeight = 187.5959F;
            this.colMemo.HeaderText = "Memo";
            this.colMemo.MaxInputLength = 45;
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            this.colMemo.Width = 92;
            // 
            // txtRefNo_d
            // 
            this.txtRefNo_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefNo_d.Location = new System.Drawing.Point(235, 58);
            this.txtRefNo_d.MaxLength = 100;
            this.txtRefNo_d.Name = "txtRefNo_d";
            this.txtRefNo_d.Size = new System.Drawing.Size(261, 39);
            this.txtRefNo_d.TabIndex = 1;
            this.txtRefNo_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAmount_d
            // 
            this.txtAmount_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount_d.Location = new System.Drawing.Point(235, 103);
            this.txtAmount_d.MaxLength = 20;
            this.txtAmount_d.Name = "txtAmount_d";
            this.txtAmount_d.Size = new System.Drawing.Size(261, 39);
            this.txtAmount_d.TabIndex = 2;
            this.txtAmount_d.Tag = "num";
            this.txtAmount_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount_d.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_d_KeyPress);
            // 
            // lblValidThru
            // 
            this.lblValidThru.AutoSize = true;
            this.lblValidThru.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidThru.Location = new System.Drawing.Point(13, 142);
            this.lblValidThru.Name = "lblValidThru";
            this.lblValidThru.Size = new System.Drawing.Size(143, 32);
            this.lblValidThru.TabIndex = 25;
            this.lblValidThru.Text = "Valid Thru:";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNo.Location = new System.Drawing.Point(13, 60);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(218, 32);
            this.lblCardNo.TabIndex = 24;
            this.lblCardNo.Text = "Gift Cheque No.:";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(13, 101);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(116, 32);
            this.lblAmount.TabIndex = 22;
            this.lblAmount.Text = "Amount:";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblInfo.Location = new System.Drawing.Point(12, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(480, 55);
            this.lblInfo.TabIndex = 21;
            this.lblInfo.Text = "GIFT CHEQUE INFO";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(235, 148);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(262, 26);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(227, 494);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 38);
            this.btnSave.TabIndex = 104;
            this.btnSave.Text = "Save (F1)";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(353, 494);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(19, 398);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 38);
            this.btnAdd.TabIndex = 100;
            this.btnAdd.Text = "Add (F4)";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(145, 398);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 101;
            this.btnDelete.Text = "Delete (F7)";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmOtherPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 544);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtMemo_d);
            this.Controls.Add(this.lblMemo);
            this.Controls.Add(this.lblTotalAmount_d);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dgvGCInfo);
            this.Controls.Add(this.txtRefNo_d);
            this.Controls.Add(this.txtAmount_d);
            this.Controls.Add(this.lblValidThru);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblInfo);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmOtherPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OtherPayment";
            this.Load += new System.EventHandler(this.OtherPayment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOtherPayment_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGCInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtMemo_d;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.Label lblTotalAmount_d;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridView dgvGCInfo;
        public System.Windows.Forms.TextBox txtRefNo_d;
        public System.Windows.Forms.TextBox txtAmount_d;
        private System.Windows.Forms.Label lblValidThru;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colexpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}