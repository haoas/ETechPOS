namespace ETech
{
    partial class frmSearchProduct
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.keyDownTimer = new System.Windows.Forms.Timer(this.components);
            this.btnESC = new System.Windows.Forms.Button();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.colBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductWid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Arial", 14.25F);
            this.lblProduct.Location = new System.Drawing.Point(12, 14);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(81, 22);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product:";
            // 
            // txtProduct
            // 
            this.txtProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProduct.Font = new System.Drawing.Font("Arial", 14.25F);
            this.txtProduct.Location = new System.Drawing.Point(99, 11);
            this.txtProduct.MaxLength = 100;
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(685, 29);
            this.txtProduct.TabIndex = 1;
            this.txtProduct.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            this.txtProduct.Leave += new System.EventHandler(this.txtProduct_Leave);
            // 
            // keyDownTimer
            // 
            this.keyDownTimer.Interval = 400;
            this.keyDownTimer.Tick += new System.EventHandler(this.keyDownTimer_Tick);
            // 
            // btnESC
            // 
            this.btnESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(647, 532);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 101;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Visible = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBarcode,
            this.colStockNo,
            this.colSellingPrice,
            this.colProduct,
            this.description,
            this.memo,
            this.colProductWid});
            this.dgvProduct.Location = new System.Drawing.Point(12, 46);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.RowHeadersWidth = 20;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvProduct.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(772, 480);
            this.dgvProduct.TabIndex = 2;
            this.dgvProduct.Tag = "";
            this.dgvProduct.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellDoubleClick);
            // 
            // colBarcode
            // 
            this.colBarcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBarcode.DataPropertyName = "productbarcode";
            this.colBarcode.FillWeight = 99.49239F;
            this.colBarcode.HeaderText = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.ReadOnly = true;
            this.colBarcode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colStockNo
            // 
            this.colStockNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStockNo.DataPropertyName = "stockno";
            this.colStockNo.FillWeight = 100.6748F;
            this.colStockNo.HeaderText = "Stock No.";
            this.colStockNo.Name = "colStockNo";
            this.colStockNo.ReadOnly = true;
            this.colStockNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colStockNo.Visible = false;
            // 
            // colSellingPrice
            // 
            this.colSellingPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSellingPrice.DataPropertyName = "price";
            this.colSellingPrice.FillWeight = 100.6763F;
            this.colSellingPrice.HeaderText = "Price";
            this.colSellingPrice.Name = "colSellingPrice";
            this.colSellingPrice.ReadOnly = true;
            this.colSellingPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colProduct
            // 
            this.colProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProduct.DataPropertyName = "productname";
            this.colProduct.FillWeight = 99.49926F;
            this.colProduct.HeaderText = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.ReadOnly = true;
            this.colProduct.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.DataPropertyName = "desc";
            this.description.FillWeight = 100.671F;
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            // 
            // memo
            // 
            this.memo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.memo.DataPropertyName = "memo";
            this.memo.FillWeight = 99.48953F;
            this.memo.HeaderText = "Memo";
            this.memo.Name = "memo";
            this.memo.Visible = false;
            // 
            // colProductWid
            // 
            this.colProductWid.DataPropertyName = "productwid";
            this.colProductWid.HeaderText = "SyncId";
            this.colProductWid.Name = "colProductSyncId";
            this.colProductWid.Visible = false;
            this.colProductWid.Width = 51;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(12, 532);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(188, 38);
            this.btnOk.TabIndex = 100;
            this.btnOk.Text = "Select (ENTER)";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmSearchProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 575);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.lblProduct);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSearchProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Product";
            this.Load += new System.EventHandler(this.frmSearchProduct_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearchProduct_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Timer keyDownTimer;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn memo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductWid;
    }
}