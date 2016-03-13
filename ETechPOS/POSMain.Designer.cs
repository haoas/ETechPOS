namespace ETech
{
    partial class POSMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnF1 = new System.Windows.Forms.Button();
            this.btnF2 = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnF4 = new System.Windows.Forms.Button();
            this.btnF5 = new System.Windows.Forms.Button();
            this.btnF6 = new System.Windows.Forms.Button();
            this.btnF7 = new System.Windows.Forms.Button();
            this.btnF8 = new System.Windows.Forms.Button();
            this.btnF9 = new System.Windows.Forms.Button();
            this.btnF11 = new System.Windows.Forms.Button();
            this.btnF12 = new System.Windows.Forms.Button();
            this.pnlOtherInfo = new System.Windows.Forms.Panel();
            this.lbl_smemo = new System.Windows.Forms.Label();
            this.lblSalesMemo = new System.Windows.Forms.Label();
            this.lblCustomermemo_d = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblMember_d = new System.Windows.Forms.Label();
            this.lblMember = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblMode_d = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblChecker_d = new System.Windows.Forms.Label();
            this.lblChecker = new System.Windows.Forms.Label();
            this.lblClerk = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblQty_d = new System.Windows.Forms.Label();
            this.lblTendered = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.spcustdisp = new System.IO.Ports.SerialPort(this.components);
            this.tmrcheckposdsetting = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.ssApplicationDetails = new System.Windows.Forms.StatusStrip();
            this.tsslApplicationVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslBranchCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslBranchName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTerminalNumberTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTerminalNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTransactionsTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTransactions = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslOfficialReceiptNumberTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslOfficialReceiptNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslClerkTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslClerk = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCustomerTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCustomer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrUpdateDateTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.pnlOtherInfo.SuspendLayout();
            this.ssApplicationDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.description,
            this.qty,
            this.price,
            this.amount});
            this.dgvProduct.Enabled = false;
            this.dgvProduct.Location = new System.Drawing.Point(606, 165);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(662, 307);
            this.dgvProduct.TabIndex = 1;
            this.dgvProduct.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellClick);
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.DataPropertyName = "productname";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.description.DefaultCellStyle = dataGridViewCellStyle13;
            this.description.FillWeight = 200F;
            this.description.HeaderText = "Description";
            this.description.MinimumWidth = 300;
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.qty.DefaultCellStyle = dataGridViewCellStyle14;
            this.qty.FillWeight = 46.81124F;
            this.qty.HeaderText = "Qty";
            this.qty.MinimumWidth = 100;
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.DataPropertyName = "price";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.price.DefaultCellStyle = dataGridViewCellStyle15;
            this.price.FillWeight = 61.91541F;
            this.price.HeaderText = "Price";
            this.price.MinimumWidth = 100;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // amount
            // 
            this.amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.amount.DataPropertyName = "amount";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.amount.DefaultCellStyle = dataGridViewCellStyle16;
            this.amount.HeaderText = "Amount";
            this.amount.MinimumWidth = 100;
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Enabled = false;
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(12, 82);
            this.txtBarcode.MaxLength = 50;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(408, 35);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.Tag = "";
            this.txtBarcode.Leave += new System.EventHandler(this.txtBarcode_Leave);
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // btnF1
            // 
            this.btnF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF1.ForeColor = System.Drawing.Color.White;
            this.btnF1.Location = new System.Drawing.Point(111, 132);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(99, 73);
            this.btnF1.TabIndex = 6;
            this.btnF1.Text = "F1\r\nNew";
            this.btnF1.UseVisualStyleBackColor = false;
            // 
            // btnF2
            // 
            this.btnF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF2.Enabled = false;
            this.btnF2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF2.ForeColor = System.Drawing.Color.White;
            this.btnF2.Location = new System.Drawing.Point(219, 132);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(99, 73);
            this.btnF2.TabIndex = 7;
            this.btnF2.Text = "F2\r\nSwitch";
            this.btnF2.UseVisualStyleBackColor = false;
            // 
            // btnESC
            // 
            this.btnESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnESC.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.ForeColor = System.Drawing.Color.White;
            this.btnESC.Location = new System.Drawing.Point(9, 132);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(99, 73);
            this.btnESC.TabIndex = 8;
            this.btnESC.Text = "ESC\r\nExit\r\nPOS";
            this.btnESC.UseVisualStyleBackColor = false;
            // 
            // btnF4
            // 
            this.btnF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF4.Enabled = false;
            this.btnF4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF4.ForeColor = System.Drawing.Color.White;
            this.btnF4.Location = new System.Drawing.Point(321, 132);
            this.btnF4.Name = "btnF4";
            this.btnF4.Size = new System.Drawing.Size(99, 73);
            this.btnF4.TabIndex = 9;
            this.btnF4.Text = "F4\r\nSearch";
            this.btnF4.UseVisualStyleBackColor = false;
            // 
            // btnF5
            // 
            this.btnF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF5.Enabled = false;
            this.btnF5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF5.ForeColor = System.Drawing.Color.White;
            this.btnF5.Location = new System.Drawing.Point(9, 211);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(99, 73);
            this.btnF5.TabIndex = 10;
            this.btnF5.Text = "F5\r\nQty";
            this.btnF5.UseVisualStyleBackColor = false;
            // 
            // btnF6
            // 
            this.btnF6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF6.Enabled = false;
            this.btnF6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF6.ForeColor = System.Drawing.Color.White;
            this.btnF6.Location = new System.Drawing.Point(114, 211);
            this.btnF6.Name = "btnF6";
            this.btnF6.Size = new System.Drawing.Size(99, 73);
            this.btnF6.TabIndex = 11;
            this.btnF6.Text = "F6\r\nVoid";
            this.btnF6.UseVisualStyleBackColor = false;
            // 
            // btnF7
            // 
            this.btnF7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF7.Enabled = false;
            this.btnF7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF7.ForeColor = System.Drawing.Color.White;
            this.btnF7.Location = new System.Drawing.Point(219, 211);
            this.btnF7.Name = "btnF7";
            this.btnF7.Size = new System.Drawing.Size(99, 73);
            this.btnF7.TabIndex = 12;
            this.btnF7.Text = "F7\r\nDelete";
            this.btnF7.UseVisualStyleBackColor = false;
            // 
            // btnF8
            // 
            this.btnF8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF8.Enabled = false;
            this.btnF8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF8.ForeColor = System.Drawing.Color.White;
            this.btnF8.Location = new System.Drawing.Point(324, 211);
            this.btnF8.Name = "btnF8";
            this.btnF8.Size = new System.Drawing.Size(99, 73);
            this.btnF8.TabIndex = 13;
            this.btnF8.Text = "F8\r\nPayment";
            this.btnF8.UseVisualStyleBackColor = false;
            // 
            // btnF9
            // 
            this.btnF9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF9.Enabled = false;
            this.btnF9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF9.ForeColor = System.Drawing.Color.White;
            this.btnF9.Location = new System.Drawing.Point(9, 290);
            this.btnF9.Name = "btnF9";
            this.btnF9.Size = new System.Drawing.Size(99, 73);
            this.btnF9.TabIndex = 14;
            this.btnF9.Text = "F9\r\nOpen\r\nItem";
            this.btnF9.UseVisualStyleBackColor = false;
            // 
            // btnF11
            // 
            this.btnF11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF11.Enabled = false;
            this.btnF11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF11.ForeColor = System.Drawing.Color.White;
            this.btnF11.Location = new System.Drawing.Point(114, 290);
            this.btnF11.Name = "btnF11";
            this.btnF11.Size = new System.Drawing.Size(99, 73);
            this.btnF11.TabIndex = 16;
            this.btnF11.Text = "F11 \r\nItem\r\nDiscount";
            this.btnF11.UseVisualStyleBackColor = false;
            // 
            // btnF12
            // 
            this.btnF12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnF12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF12.ForeColor = System.Drawing.Color.White;
            this.btnF12.Location = new System.Drawing.Point(219, 290);
            this.btnF12.Name = "btnF12";
            this.btnF12.Size = new System.Drawing.Size(99, 73);
            this.btnF12.TabIndex = 17;
            this.btnF12.Text = "F12\r\nMenu";
            this.btnF12.UseVisualStyleBackColor = false;
            // 
            // pnlOtherInfo
            // 
            this.pnlOtherInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pnlOtherInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOtherInfo.Controls.Add(this.lbl_smemo);
            this.pnlOtherInfo.Controls.Add(this.lblSalesMemo);
            this.pnlOtherInfo.Controls.Add(this.lblCustomermemo_d);
            this.pnlOtherInfo.Controls.Add(this.lblWarning);
            this.pnlOtherInfo.Controls.Add(this.lblMember_d);
            this.pnlOtherInfo.Controls.Add(this.lblMember);
            this.pnlOtherInfo.Controls.Add(this.lblCustomer);
            this.pnlOtherInfo.Controls.Add(this.lblMode_d);
            this.pnlOtherInfo.Controls.Add(this.lblMode);
            this.pnlOtherInfo.Controls.Add(this.lblChecker_d);
            this.pnlOtherInfo.Controls.Add(this.lblChecker);
            this.pnlOtherInfo.Controls.Add(this.lblClerk);
            this.pnlOtherInfo.Location = new System.Drawing.Point(12, 369);
            this.pnlOtherInfo.Name = "pnlOtherInfo";
            this.pnlOtherInfo.Size = new System.Drawing.Size(584, 203);
            this.pnlOtherInfo.TabIndex = 18;
            // 
            // lbl_smemo
            // 
            this.lbl_smemo.AutoSize = true;
            this.lbl_smemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_smemo.ForeColor = System.Drawing.Color.Black;
            this.lbl_smemo.Location = new System.Drawing.Point(303, 72);
            this.lbl_smemo.Name = "lbl_smemo";
            this.lbl_smemo.Size = new System.Drawing.Size(69, 24);
            this.lbl_smemo.TabIndex = 15;
            this.lbl_smemo.Text = "Memo:";
            // 
            // lblSalesMemo
            // 
            this.lblSalesMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSalesMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblSalesMemo.Location = new System.Drawing.Point(401, 72);
            this.lblSalesMemo.Name = "lblSalesMemo";
            this.lblSalesMemo.Size = new System.Drawing.Size(169, 24);
            this.lblSalesMemo.TabIndex = 14;
            // 
            // lblCustomermemo_d
            // 
            this.lblCustomermemo_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCustomermemo_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCustomermemo_d.ForeColor = System.Drawing.Color.Black;
            this.lblCustomermemo_d.Location = new System.Drawing.Point(13, 103);
            this.lblCustomermemo_d.Name = "lblCustomermemo_d";
            this.lblCustomermemo_d.Size = new System.Drawing.Size(557, 40);
            this.lblCustomermemo_d.TabIndex = 13;
            // 
            // lblWarning
            // 
            this.lblWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(13, 149);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(557, 40);
            this.lblWarning.TabIndex = 12;
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMember_d
            // 
            this.lblMember_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMember_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMember_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMember_d.Location = new System.Drawing.Point(401, 12);
            this.lblMember_d.Name = "lblMember_d";
            this.lblMember_d.Size = new System.Drawing.Size(169, 24);
            this.lblMember_d.TabIndex = 11;
            // 
            // lblMember
            // 
            this.lblMember.AutoSize = true;
            this.lblMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMember.ForeColor = System.Drawing.Color.Black;
            this.lblMember.Location = new System.Drawing.Point(303, 12);
            this.lblMember.Name = "lblMember";
            this.lblMember.Size = new System.Drawing.Size(86, 24);
            this.lblMember.TabIndex = 9;
            this.lblMember.Text = "Member:";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.ForeColor = System.Drawing.Color.Black;
            this.lblCustomer.Location = new System.Drawing.Point(14, 70);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(96, 24);
            this.lblCustomer.TabIndex = 8;
            this.lblCustomer.Text = "Customer:";
            // 
            // lblMode_d
            // 
            this.lblMode_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMode_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMode_d.Location = new System.Drawing.Point(401, 42);
            this.lblMode_d.Name = "lblMode_d";
            this.lblMode_d.Size = new System.Drawing.Size(169, 24);
            this.lblMode_d.TabIndex = 5;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Black;
            this.lblMode.Location = new System.Drawing.Point(303, 42);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(64, 24);
            this.lblMode.TabIndex = 4;
            this.lblMode.Text = "Mode:";
            // 
            // lblChecker_d
            // 
            this.lblChecker_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChecker_d.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecker_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblChecker_d.Location = new System.Drawing.Point(117, 42);
            this.lblChecker_d.Name = "lblChecker_d";
            this.lblChecker_d.Size = new System.Drawing.Size(169, 24);
            this.lblChecker_d.TabIndex = 3;
            // 
            // lblChecker
            // 
            this.lblChecker.AutoSize = true;
            this.lblChecker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecker.ForeColor = System.Drawing.Color.Black;
            this.lblChecker.Location = new System.Drawing.Point(15, 41);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(98, 24);
            this.lblChecker.TabIndex = 1;
            this.lblChecker.Text = "Salesman:";
            // 
            // lblClerk
            // 
            this.lblClerk.AutoSize = true;
            this.lblClerk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClerk.ForeColor = System.Drawing.Color.Black;
            this.lblClerk.Location = new System.Drawing.Point(16, 13);
            this.lblClerk.Name = "lblClerk";
            this.lblClerk.Size = new System.Drawing.Size(58, 24);
            this.lblClerk.TabIndex = 0;
            this.lblClerk.Text = "Clerk:";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.BackColor = System.Drawing.Color.Transparent;
            this.lblBarcode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.Location = new System.Drawing.Point(5, 57);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(87, 22);
            this.lblBarcode.TabIndex = 20;
            this.lblBarcode.Text = "Barcode:";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.BackColor = System.Drawing.Color.Transparent;
            this.lblQty.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(399, 700);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(76, 38);
            this.lblQty.TabIndex = 21;
            this.lblQty.Text = "Qty:";
            // 
            // lblQty_d
            // 
            this.lblQty_d.BackColor = System.Drawing.Color.Transparent;
            this.lblQty_d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQty_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty_d.Location = new System.Drawing.Point(481, 700);
            this.lblQty_d.Name = "lblQty_d";
            this.lblQty_d.Size = new System.Drawing.Size(115, 39);
            this.lblQty_d.TabIndex = 22;
            this.lblQty_d.Text = "0";
            this.lblQty_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTendered
            // 
            this.lblTendered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblTendered.Font = new System.Drawing.Font("Arial Black", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTendered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTendered.Location = new System.Drawing.Point(990, 575);
            this.lblTendered.Name = "lblTendered";
            this.lblTendered.Size = new System.Drawing.Size(277, 73);
            this.lblTendered.TabIndex = 1;
            this.lblTendered.Text = "0.00";
            this.lblTendered.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblRemaining
            // 
            this.lblRemaining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRemaining.Font = new System.Drawing.Font("Arial Black", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblRemaining.Location = new System.Drawing.Point(992, 658);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(276, 73);
            this.lblRemaining.TabIndex = 2;
            this.lblRemaining.Text = "0.00";
            this.lblRemaining.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // spcustdisp
            // 
            this.spcustdisp.PortName = "COM3";
            // 
            // tmrcheckposdsetting
            // 
            this.tmrcheckposdsetting.Enabled = true;
            this.tmrcheckposdsetting.Interval = 5000;
            this.tmrcheckposdsetting.Tick += new System.EventHandler(this.TimerRefreshSettings_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(902, 620);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Tendered";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(902, 676);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Remaining";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblTotal.Font = new System.Drawing.Font("Arial Black", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTotal.Location = new System.Drawing.Point(989, 488);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(278, 74);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ssApplicationDetails
            // 
            this.ssApplicationDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslApplicationVersion,
            this.tsslBranchCode,
            this.tsslBranchName,
            this.tsslTerminalNumberTxt,
            this.tsslTerminalNumber,
            this.tsslTransactionsTxt,
            this.tsslTransactions,
            this.tsslOfficialReceiptNumberTxt,
            this.tsslOfficialReceiptNumber,
            this.tsslClerkTxt,
            this.tsslClerk,
            this.tsslCustomerTxt,
            this.tsslCustomer,
            this.tsslSpace,
            this.tsslDateTime});
            this.ssApplicationDetails.Location = new System.Drawing.Point(0, 748);
            this.ssApplicationDetails.Name = "ssApplicationDetails";
            this.ssApplicationDetails.Size = new System.Drawing.Size(1280, 24);
            this.ssApplicationDetails.SizingGrip = false;
            this.ssApplicationDetails.TabIndex = 31;
            this.ssApplicationDetails.Text = "Status";
            // 
            // tsslApplicationVersion
            // 
            this.tsslApplicationVersion.AutoSize = false;
            this.tsslApplicationVersion.Name = "tsslApplicationVersion";
            this.tsslApplicationVersion.Size = new System.Drawing.Size(55, 19);
            this.tsslApplicationVersion.Text = "v1.0.0.0";
            this.tsslApplicationVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslBranchCode
            // 
            this.tsslBranchCode.AutoSize = false;
            this.tsslBranchCode.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslBranchCode.Name = "tsslBranchCode";
            this.tsslBranchCode.Size = new System.Drawing.Size(55, 19);
            this.tsslBranchCode.Text = "0000";
            this.tsslBranchCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslBranchName
            // 
            this.tsslBranchName.AutoSize = false;
            this.tsslBranchName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslBranchName.Name = "tsslBranchName";
            this.tsslBranchName.Size = new System.Drawing.Size(100, 19);
            this.tsslBranchName.Text = "Branch";
            this.tsslBranchName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTerminalNumberTxt
            // 
            this.tsslTerminalNumberTxt.AutoSize = false;
            this.tsslTerminalNumberTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslTerminalNumberTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTerminalNumberTxt.Name = "tsslTerminalNumberTxt";
            this.tsslTerminalNumberTxt.Size = new System.Drawing.Size(60, 19);
            this.tsslTerminalNumberTxt.Text = "Terminal:";
            this.tsslTerminalNumberTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTerminalNumber
            // 
            this.tsslTerminalNumber.AutoSize = false;
            this.tsslTerminalNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTerminalNumber.Name = "tsslTerminalNumber";
            this.tsslTerminalNumber.Size = new System.Drawing.Size(35, 19);
            this.tsslTerminalNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTransactionsTxt
            // 
            this.tsslTransactionsTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslTransactionsTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTransactionsTxt.Name = "tsslTransactionsTxt";
            this.tsslTransactionsTxt.Size = new System.Drawing.Size(80, 19);
            this.tsslTransactionsTxt.Text = "Transactions:";
            this.tsslTransactionsTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTransactions
            // 
            this.tsslTransactions.AutoSize = false;
            this.tsslTransactions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTransactions.Name = "tsslTransactions";
            this.tsslTransactions.Size = new System.Drawing.Size(70, 19);
            this.tsslTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslOfficialReceiptNumberTxt
            // 
            this.tsslOfficialReceiptNumberTxt.AutoSize = false;
            this.tsslOfficialReceiptNumberTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslOfficialReceiptNumberTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslOfficialReceiptNumberTxt.Name = "tsslOfficialReceiptNumberTxt";
            this.tsslOfficialReceiptNumberTxt.Size = new System.Drawing.Size(60, 19);
            this.tsslOfficialReceiptNumberTxt.Text = "OR No.:";
            this.tsslOfficialReceiptNumberTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslOfficialReceiptNumber
            // 
            this.tsslOfficialReceiptNumber.AutoSize = false;
            this.tsslOfficialReceiptNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslOfficialReceiptNumber.Name = "tsslOfficialReceiptNumber";
            this.tsslOfficialReceiptNumber.Size = new System.Drawing.Size(180, 19);
            this.tsslOfficialReceiptNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslClerkTxt
            // 
            this.tsslClerkTxt.AutoSize = false;
            this.tsslClerkTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslClerkTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslClerkTxt.Name = "tsslClerkTxt";
            this.tsslClerkTxt.Size = new System.Drawing.Size(50, 19);
            this.tsslClerkTxt.Text = "Clerk:";
            this.tsslClerkTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslClerk
            // 
            this.tsslClerk.AutoSize = false;
            this.tsslClerk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslClerk.Name = "tsslClerk";
            this.tsslClerk.Size = new System.Drawing.Size(100, 19);
            this.tsslClerk.Text = "Clerk 1";
            this.tsslClerk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslCustomerTxt
            // 
            this.tsslCustomerTxt.AutoSize = false;
            this.tsslCustomerTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslCustomerTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCustomerTxt.Name = "tsslCustomerTxt";
            this.tsslCustomerTxt.Size = new System.Drawing.Size(70, 19);
            this.tsslCustomerTxt.Text = "Customer:";
            this.tsslCustomerTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslCustomer
            // 
            this.tsslCustomer.AutoSize = false;
            this.tsslCustomer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCustomer.Name = "tsslCustomer";
            this.tsslCustomer.Size = new System.Drawing.Size(100, 19);
            this.tsslCustomer.Text = "Customer 1";
            this.tsslCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSpace
            // 
            this.tsslSpace.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslSpace.Name = "tsslSpace";
            this.tsslSpace.Size = new System.Drawing.Size(1, 19);
            this.tsslSpace.Spring = true;
            this.tsslSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslDateTime
            // 
            this.tsslDateTime.AutoSize = false;
            this.tsslDateTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslDateTime.Name = "tsslDateTime";
            this.tsslDateTime.Size = new System.Drawing.Size(250, 19);
            this.tsslDateTime.Text = "JANUARY 01, 2016 08:00:00 AM";
            this.tsslDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrUpdateDateTime
            // 
            this.tmrUpdateDateTime.Enabled = true;
            this.tmrUpdateDateTime.Interval = 1000;
            this.tmrUpdateDateTime.Tick += new System.EventHandler(this.tmrUpdateDateTime_Tick);
            // 
            // POSMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1280, 772);
            this.Controls.Add(this.ssApplicationDetails);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.lblTendered);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.lblQty_d);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.pnlOtherInfo);
            this.Controls.Add(this.btnF12);
            this.Controls.Add(this.btnF11);
            this.Controls.Add(this.btnF9);
            this.Controls.Add(this.btnF8);
            this.Controls.Add(this.btnF7);
            this.Controls.Add(this.btnF6);
            this.Controls.Add(this.btnF5);
            this.Controls.Add(this.btnF4);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtBarcode);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "POSMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ETECH POS SYSTEM";
            this.Load += new System.EventHandler(this.POSMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.POSMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.pnlOtherInfo.ResumeLayout(false);
            this.pnlOtherInfo.PerformLayout();
            this.ssApplicationDetails.ResumeLayout(false);
            this.ssApplicationDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnF1;
        private System.Windows.Forms.Button btnF2;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnF4;
        private System.Windows.Forms.Button btnF5;
        private System.Windows.Forms.Button btnF6;
        private System.Windows.Forms.Button btnF7;
        private System.Windows.Forms.Button btnF8;
        private System.Windows.Forms.Button btnF9;
        private System.Windows.Forms.Button btnF11;
        private System.Windows.Forms.Button btnF12;
        private System.Windows.Forms.Panel pnlOtherInfo;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblQty_d;
        private System.Windows.Forms.Label lblTendered;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblChecker_d;
        private System.Windows.Forms.Label lblChecker;
        private System.Windows.Forms.Label lblClerk;
        private System.Windows.Forms.Label lblMode_d;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblMember_d;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblWarning;
        private System.IO.Ports.SerialPort spcustdisp;
        public System.Windows.Forms.DataGridView dgvProduct;
        public System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblCustomermemo_d;
        private System.Windows.Forms.Timer tmrcheckposdsetting;
        private System.Windows.Forms.Label lblSalesMemo;
        private System.Windows.Forms.Label lbl_smemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.StatusStrip ssApplicationDetails;
        private System.Windows.Forms.ToolStripStatusLabel tsslBranchCode;
        private System.Windows.Forms.ToolStripStatusLabel tsslBranchName;
        private System.Windows.Forms.ToolStripStatusLabel tsslDateTime;
        private System.Windows.Forms.ToolStripStatusLabel tsslTerminalNumber;
        private System.Windows.Forms.ToolStripStatusLabel tsslTransactionsTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslTransactions;
        private System.Windows.Forms.ToolStripStatusLabel tsslOfficialReceiptNumber;
        private System.Windows.Forms.ToolStripStatusLabel tsslOfficialReceiptNumberTxt;
        private System.Windows.Forms.Timer tmrUpdateDateTime;
        private System.Windows.Forms.ToolStripStatusLabel tsslApplicationVersion;
        private System.Windows.Forms.ToolStripStatusLabel tsslTerminalNumberTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslClerkTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslClerk;
        private System.Windows.Forms.ToolStripStatusLabel tsslCustomerTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslCustomer;
        private System.Windows.Forms.ToolStripStatusLabel tsslSpace;
    }
}