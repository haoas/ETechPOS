namespace ETech
{
    partial class frmCardInfo
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
            this.lblCreditCardInfo = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblCardHolder = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.lblValidThru = new System.Windows.Forms.Label();
            this.txtAmount_d = new System.Windows.Forms.TextBox();
            this.txtCardHolder_d = new System.Windows.Forms.TextBox();
            this.txtCardNo_d = new System.Windows.Forms.TextBox();
            this.txtValidThru_m = new System.Windows.Forms.TextBox();
            this.txtValidThru_y = new System.Windows.Forms.TextBox();
            this.dgvCardInfo = new System.Windows.Forms.DataGridView();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount_d = new System.Windows.Forms.Label();
            this.txtApprovalCode_d = new System.Windows.Forms.TextBox();
            this.lblApprovalCode = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.colCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardHolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValidThru_m = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValidThru_y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApprovalCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCreditCardInfo
            // 
            this.lblCreditCardInfo.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCardInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCreditCardInfo.Location = new System.Drawing.Point(0, 8);
            this.lblCreditCardInfo.Name = "lblCreditCardInfo";
            this.lblCreditCardInfo.Size = new System.Drawing.Size(503, 55);
            this.lblCreditCardInfo.TabIndex = 1;
            this.lblCreditCardInfo.Text = "CARD INFORMATION";
            this.lblCreditCardInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(22, 68);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(116, 32);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Amount:";
            // 
            // lblCardHolder
            // 
            this.lblCardHolder.AutoSize = true;
            this.lblCardHolder.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardHolder.Location = new System.Drawing.Point(22, 114);
            this.lblCardHolder.Name = "lblCardHolder";
            this.lblCardHolder.Size = new System.Drawing.Size(169, 32);
            this.lblCardHolder.TabIndex = 3;
            this.lblCardHolder.Text = "Card Holder:";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNo.Location = new System.Drawing.Point(22, 162);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(132, 32);
            this.lblCardNo.TabIndex = 4;
            this.lblCardNo.Text = "Card No.:";
            // 
            // lblValidThru
            // 
            this.lblValidThru.AutoSize = true;
            this.lblValidThru.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidThru.Location = new System.Drawing.Point(22, 212);
            this.lblValidThru.Name = "lblValidThru";
            this.lblValidThru.Size = new System.Drawing.Size(269, 32);
            this.lblValidThru.TabIndex = 5;
            this.lblValidThru.Text = "Valid Thru(mm/yyyy):";
            // 
            // txtAmount_d
            // 
            this.txtAmount_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount_d.Location = new System.Drawing.Point(216, 66);
            this.txtAmount_d.MaxLength = 10;
            this.txtAmount_d.Name = "txtAmount_d";
            this.txtAmount_d.Size = new System.Drawing.Size(274, 39);
            this.txtAmount_d.TabIndex = 6;
            this.txtAmount_d.Tag = "num";
            this.txtAmount_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCardHolder_d
            // 
            this.txtCardHolder_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardHolder_d.Location = new System.Drawing.Point(216, 112);
            this.txtCardHolder_d.MaxLength = 100;
            this.txtCardHolder_d.Name = "txtCardHolder_d";
            this.txtCardHolder_d.Size = new System.Drawing.Size(274, 39);
            this.txtCardHolder_d.TabIndex = 7;
            this.txtCardHolder_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCardNo_d
            // 
            this.txtCardNo_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNo_d.Location = new System.Drawing.Point(184, 160);
            this.txtCardNo_d.MaxLength = 19;
            this.txtCardNo_d.Name = "txtCardNo_d";
            this.txtCardNo_d.Size = new System.Drawing.Size(306, 39);
            this.txtCardNo_d.TabIndex = 8;
            this.txtCardNo_d.Tag = "num";
            this.txtCardNo_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValidThru_m
            // 
            this.txtValidThru_m.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidThru_m.Location = new System.Drawing.Point(327, 210);
            this.txtValidThru_m.MaxLength = 2;
            this.txtValidThru_m.Name = "txtValidThru_m";
            this.txtValidThru_m.Size = new System.Drawing.Size(56, 39);
            this.txtValidThru_m.TabIndex = 9;
            this.txtValidThru_m.Tag = "integer";
            this.txtValidThru_m.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValidThru_y
            // 
            this.txtValidThru_y.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidThru_y.Location = new System.Drawing.Point(398, 210);
            this.txtValidThru_y.MaxLength = 4;
            this.txtValidThru_y.Name = "txtValidThru_y";
            this.txtValidThru_y.Size = new System.Drawing.Size(92, 39);
            this.txtValidThru_y.TabIndex = 10;
            this.txtValidThru_y.Tag = "integer";
            this.txtValidThru_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvCardInfo
            // 
            this.dgvCardInfo.AllowUserToAddRows = false;
            this.dgvCardInfo.AllowUserToDeleteRows = false;
            this.dgvCardInfo.AllowUserToResizeColumns = false;
            this.dgvCardInfo.AllowUserToResizeRows = false;
            this.dgvCardInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCardInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCardInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCardInfo.ColumnHeadersHeight = 30;
            this.dgvCardInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCardInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardNo,
            this.colCardtype,
            this.colAmount,
            this.colCardHolder,
            this.colValidThru_m,
            this.colValidThru_y,
            this.colApprovalCode});
            this.dgvCardInfo.Location = new System.Drawing.Point(12, 299);
            this.dgvCardInfo.Name = "dgvCardInfo";
            this.dgvCardInfo.ReadOnly = true;
            this.dgvCardInfo.RowHeadersVisible = false;
            this.dgvCardInfo.Size = new System.Drawing.Size(478, 162);
            this.dgvCardInfo.TabIndex = 11;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(12, 464);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(211, 38);
            this.lblTotalAmount.TabIndex = 12;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // lblTotalAmount_d
            // 
            this.lblTotalAmount_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount_d.Location = new System.Drawing.Point(264, 464);
            this.lblTotalAmount_d.Name = "lblTotalAmount_d";
            this.lblTotalAmount_d.Size = new System.Drawing.Size(226, 43);
            this.lblTotalAmount_d.TabIndex = 13;
            this.lblTotalAmount_d.Text = "0.00";
            this.lblTotalAmount_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtApprovalCode_d
            // 
            this.txtApprovalCode_d.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApprovalCode_d.Location = new System.Drawing.Point(233, 254);
            this.txtApprovalCode_d.MaxLength = 100;
            this.txtApprovalCode_d.Name = "txtApprovalCode_d";
            this.txtApprovalCode_d.Size = new System.Drawing.Size(137, 39);
            this.txtApprovalCode_d.TabIndex = 11;
            this.txtApprovalCode_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblApprovalCode
            // 
            this.lblApprovalCode.AutoSize = true;
            this.lblApprovalCode.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApprovalCode.Location = new System.Drawing.Point(22, 259);
            this.lblApprovalCode.Name = "lblApprovalCode";
            this.lblApprovalCode.Size = new System.Drawing.Size(202, 32);
            this.lblApprovalCode.TabIndex = 20;
            this.lblApprovalCode.Text = "Approval Code:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(227, 510);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 38);
            this.btnOK.TabIndex = 99;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(353, 510);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 100;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(10, 510);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete (F7)";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(376, 255);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(114, 38);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add (F4)";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // colCardNo
            // 
            this.colCardNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCardNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCardNo.FillWeight = 70.77962F;
            this.colCardNo.HeaderText = " Card No";
            this.colCardNo.MinimumWidth = 80;
            this.colCardNo.Name = "colCardNo";
            this.colCardNo.ReadOnly = true;
            this.colCardNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCardNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCardtype
            // 
            this.colCardtype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCardtype.FillWeight = 61.38795F;
            this.colCardtype.HeaderText = "Type";
            this.colCardtype.MinimumWidth = 40;
            this.colCardtype.Name = "colCardtype";
            this.colCardtype.ReadOnly = true;
            this.colCardtype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCardtype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAmount
            // 
            this.colAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAmount.FillWeight = 60F;
            this.colAmount.HeaderText = "Amount";
            this.colAmount.MinimumWidth = 60;
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCardHolder
            // 
            this.colCardHolder.HeaderText = "Card Holder";
            this.colCardHolder.Name = "colCardHolder";
            this.colCardHolder.ReadOnly = true;
            this.colCardHolder.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCardHolder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCardHolder.Visible = false;
            // 
            // colValidThru_m
            // 
            this.colValidThru_m.HeaderText = "Valid Thru (M)";
            this.colValidThru_m.Name = "colValidThru_m";
            this.colValidThru_m.ReadOnly = true;
            this.colValidThru_m.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colValidThru_m.Visible = false;
            // 
            // colValidThru_y
            // 
            this.colValidThru_y.HeaderText = "Valid Thru (Y)";
            this.colValidThru_y.Name = "colValidThru_y";
            this.colValidThru_y.ReadOnly = true;
            this.colValidThru_y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colValidThru_y.Visible = false;
            // 
            // colApprovalCode
            // 
            this.colApprovalCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colApprovalCode.FillWeight = 94.07813F;
            this.colApprovalCode.HeaderText = "Approval Code";
            this.colApprovalCode.Name = "colApprovalCode";
            this.colApprovalCode.ReadOnly = true;
            this.colApprovalCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colApprovalCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmCardInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 552);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtApprovalCode_d);
            this.Controls.Add(this.lblApprovalCode);
            this.Controls.Add(this.lblTotalAmount_d);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dgvCardInfo);
            this.Controls.Add(this.txtValidThru_y);
            this.Controls.Add(this.txtValidThru_m);
            this.Controls.Add(this.txtCardNo_d);
            this.Controls.Add(this.txtCardHolder_d);
            this.Controls.Add(this.txtAmount_d);
            this.Controls.Add(this.lblValidThru);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.lblCardHolder);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblCreditCardInfo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmCardInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Card Information";
            this.Load += new System.EventHandler(this.frmCardInfo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmCardInfo_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCardInfo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCreditCardInfo;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblCardHolder;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label lblValidThru;
        private System.Windows.Forms.DataGridView dgvCardInfo;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount_d;
        private System.Windows.Forms.Label lblApprovalCode;
        public System.Windows.Forms.TextBox txtAmount_d;
        public System.Windows.Forms.TextBox txtCardHolder_d;
        public System.Windows.Forms.TextBox txtCardNo_d;
        public System.Windows.Forms.TextBox txtValidThru_m;
        public System.Windows.Forms.TextBox txtValidThru_y;
        public System.Windows.Forms.TextBox txtApprovalCode_d;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardHolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValidThru_m;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValidThru_y;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApprovalCode;
    }
}