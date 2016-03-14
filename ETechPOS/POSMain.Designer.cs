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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lblMode_d = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
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
            this.tsslSpace2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrUpdateDateTime = new System.Windows.Forms.Timer(this.components);
            this.ssOtherInformation = new System.Windows.Forms.StatusStrip();
            this.tsslClerkTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslClerk = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSalesManTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSalesMan = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMemberTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMember = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCustomerTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCustomer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSalesMemoTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSalesMemo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCustomerMemo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel12 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSpace1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.ssApplicationDetails.SuspendLayout();
            this.ssOtherInformation.SuspendLayout();
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
            this.dgvProduct.Location = new System.Drawing.Point(602, 12);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.description.DefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.qty.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.price.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.amount.DefaultCellStyle = dataGridViewCellStyle4;
            this.amount.HeaderText = "Amount";
            this.amount.MinimumWidth = 100;
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Enabled = false;
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(12, 37);
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
            this.btnF1.Location = new System.Drawing.Point(111, 87);
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
            this.btnF2.Location = new System.Drawing.Point(219, 87);
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
            this.btnESC.Location = new System.Drawing.Point(9, 87);
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
            this.btnF4.Location = new System.Drawing.Point(321, 87);
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
            this.btnF5.Location = new System.Drawing.Point(9, 166);
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
            this.btnF6.Location = new System.Drawing.Point(111, 166);
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
            this.btnF7.Location = new System.Drawing.Point(219, 166);
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
            this.btnF8.Location = new System.Drawing.Point(321, 166);
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
            this.btnF9.Location = new System.Drawing.Point(9, 245);
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
            this.btnF11.Location = new System.Drawing.Point(114, 245);
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
            this.btnF12.Location = new System.Drawing.Point(219, 245);
            this.btnF12.Name = "btnF12";
            this.btnF12.Size = new System.Drawing.Size(99, 73);
            this.btnF12.TabIndex = 17;
            this.btnF12.Text = "F12\r\nMenu";
            this.btnF12.UseVisualStyleBackColor = false;
            // 
            // lblMode_d
            // 
            this.lblMode_d.BackColor = System.Drawing.Color.White;
            this.lblMode_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblMode_d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMode_d.Location = new System.Drawing.Point(1095, 322);
            this.lblMode_d.Name = "lblMode_d";
            this.lblMode_d.Size = new System.Drawing.Size(169, 39);
            this.lblMode_d.TabIndex = 5;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.BackColor = System.Drawing.Color.White;
            this.lblMode.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblMode.ForeColor = System.Drawing.Color.Black;
            this.lblMode.Location = new System.Drawing.Point(984, 323);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(105, 38);
            this.lblMode.TabIndex = 4;
            this.lblMode.Text = "Mode:";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.BackColor = System.Drawing.Color.White;
            this.lblBarcode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.Location = new System.Drawing.Point(12, 12);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(87, 22);
            this.lblBarcode.TabIndex = 20;
            this.lblBarcode.Text = "Barcode:";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.BackColor = System.Drawing.Color.White;
            this.lblQty.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(1013, 369);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(76, 38);
            this.lblQty.TabIndex = 21;
            this.lblQty.Text = "Qty:";
            // 
            // lblQty_d
            // 
            this.lblQty_d.BackColor = System.Drawing.Color.White;
            this.lblQty_d.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty_d.Location = new System.Drawing.Point(1095, 369);
            this.lblQty_d.Name = "lblQty_d";
            this.lblQty_d.Size = new System.Drawing.Size(169, 39);
            this.lblQty_d.TabIndex = 22;
            this.lblQty_d.Text = "0";
            this.lblQty_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTendered
            // 
            this.lblTendered.BackColor = System.Drawing.Color.White;
            this.lblTendered.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblTendered.ForeColor = System.Drawing.Color.Black;
            this.lblTendered.Location = new System.Drawing.Point(1095, 463);
            this.lblTendered.Name = "lblTendered";
            this.lblTendered.Size = new System.Drawing.Size(169, 39);
            this.lblTendered.TabIndex = 1;
            this.lblTendered.Text = "0.00";
            this.lblTendered.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblRemaining
            // 
            this.lblRemaining.BackColor = System.Drawing.Color.White;
            this.lblRemaining.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblRemaining.ForeColor = System.Drawing.Color.Black;
            this.lblRemaining.Location = new System.Drawing.Point(1095, 510);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(169, 39);
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
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label1.Location = new System.Drawing.Point(939, 464);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 38);
            this.label1.TabIndex = 29;
            this.label1.Text = "Tendered";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label3.Location = new System.Drawing.Point(919, 511);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 38);
            this.label3.TabIndex = 30;
            this.label3.Text = "Remaining";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.White;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(1095, 416);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(169, 39);
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
            this.tsslSpace2,
            this.tsslDateTime});
            this.ssApplicationDetails.Location = new System.Drawing.Point(0, 718);
            this.ssApplicationDetails.Name = "ssApplicationDetails";
            this.ssApplicationDetails.Size = new System.Drawing.Size(1276, 24);
            this.ssApplicationDetails.SizingGrip = false;
            this.ssApplicationDetails.TabIndex = 31;
            this.ssApplicationDetails.Text = "Status";
            // 
            // tsslApplicationVersion
            // 
            this.tsslApplicationVersion.AutoSize = false;
            this.tsslApplicationVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslApplicationVersion.Name = "tsslApplicationVersion";
            this.tsslApplicationVersion.Size = new System.Drawing.Size(55, 19);
            this.tsslApplicationVersion.Text = "v1.0.0.0";
            this.tsslApplicationVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslBranchCode
            // 
            this.tsslBranchCode.AutoSize = false;
            this.tsslBranchCode.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslBranchCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslBranchCode.Name = "tsslBranchCode";
            this.tsslBranchCode.Size = new System.Drawing.Size(55, 19);
            this.tsslBranchCode.Text = "0000";
            this.tsslBranchCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslBranchName
            // 
            this.tsslBranchName.AutoSize = false;
            this.tsslBranchName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslBranchName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tsslTerminalNumberTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTerminalNumberTxt.Name = "tsslTerminalNumberTxt";
            this.tsslTerminalNumberTxt.Size = new System.Drawing.Size(60, 19);
            this.tsslTerminalNumberTxt.Text = "Terminal:";
            this.tsslTerminalNumberTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTerminalNumber
            // 
            this.tsslTerminalNumber.AutoSize = false;
            this.tsslTerminalNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTerminalNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTerminalNumber.Name = "tsslTerminalNumber";
            this.tsslTerminalNumber.Size = new System.Drawing.Size(35, 19);
            this.tsslTerminalNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTransactionsTxt
            // 
            this.tsslTransactionsTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslTransactionsTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTransactionsTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTransactionsTxt.Name = "tsslTransactionsTxt";
            this.tsslTransactionsTxt.Size = new System.Drawing.Size(83, 19);
            this.tsslTransactionsTxt.Text = "Transactions:";
            this.tsslTransactionsTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTransactions
            // 
            this.tsslTransactions.AutoSize = false;
            this.tsslTransactions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslTransactions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTransactions.Name = "tsslTransactions";
            this.tsslTransactions.Size = new System.Drawing.Size(70, 19);
            this.tsslTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslOfficialReceiptNumberTxt
            // 
            this.tsslOfficialReceiptNumberTxt.AutoSize = false;
            this.tsslOfficialReceiptNumberTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslOfficialReceiptNumberTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslOfficialReceiptNumberTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslOfficialReceiptNumberTxt.Name = "tsslOfficialReceiptNumberTxt";
            this.tsslOfficialReceiptNumberTxt.Size = new System.Drawing.Size(60, 19);
            this.tsslOfficialReceiptNumberTxt.Text = "OR No.:";
            this.tsslOfficialReceiptNumberTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslOfficialReceiptNumber
            // 
            this.tsslOfficialReceiptNumber.AutoSize = false;
            this.tsslOfficialReceiptNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslOfficialReceiptNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslOfficialReceiptNumber.Name = "tsslOfficialReceiptNumber";
            this.tsslOfficialReceiptNumber.Size = new System.Drawing.Size(90, 19);
            this.tsslOfficialReceiptNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSpace2
            // 
            this.tsslSpace2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslSpace2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslSpace2.Name = "tsslSpace2";
            this.tsslSpace2.Size = new System.Drawing.Size(403, 19);
            this.tsslSpace2.Spring = true;
            this.tsslSpace2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslDateTime
            // 
            this.tsslDateTime.AutoSize = false;
            this.tsslDateTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslDateTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // ssOtherInformation
            // 
            this.ssOtherInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslClerkTxt,
            this.tsslClerk,
            this.tsslSalesManTxt,
            this.tsslSalesMan,
            this.tsslMemberTxt,
            this.tsslMember,
            this.tsslCustomerTxt,
            this.tsslCustomer,
            this.tsslSalesMemoTxt,
            this.tsslSalesMemo,
            this.tsslCustomerMemo,
            this.tsslWarning,
            this.toolStripStatusLabel12,
            this.tsslSpace1});
            this.ssOtherInformation.Location = new System.Drawing.Point(0, 688);
            this.ssOtherInformation.Name = "ssOtherInformation";
            this.ssOtherInformation.Size = new System.Drawing.Size(1276, 30);
            this.ssOtherInformation.SizingGrip = false;
            this.ssOtherInformation.TabIndex = 32;
            this.ssOtherInformation.Text = "Status";
            // 
            // tsslClerkTxt
            // 
            this.tsslClerkTxt.AutoSize = false;
            this.tsslClerkTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslClerkTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslClerkTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslClerkTxt.Name = "tsslClerkTxt";
            this.tsslClerkTxt.Size = new System.Drawing.Size(50, 25);
            this.tsslClerkTxt.Text = "Clerk:";
            this.tsslClerkTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslClerk
            // 
            this.tsslClerk.AutoSize = false;
            this.tsslClerk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslClerk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslClerk.Name = "tsslClerk";
            this.tsslClerk.Size = new System.Drawing.Size(100, 25);
            this.tsslClerk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSalesManTxt
            // 
            this.tsslSalesManTxt.AutoSize = false;
            this.tsslSalesManTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslSalesManTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslSalesManTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslSalesManTxt.Name = "tsslSalesManTxt";
            this.tsslSalesManTxt.Size = new System.Drawing.Size(70, 25);
            this.tsslSalesManTxt.Text = "Salesman:";
            this.tsslSalesManTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSalesMan
            // 
            this.tsslSalesMan.AutoSize = false;
            this.tsslSalesMan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslSalesMan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslSalesMan.Name = "tsslSalesMan";
            this.tsslSalesMan.Size = new System.Drawing.Size(100, 25);
            this.tsslSalesMan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslMemberTxt
            // 
            this.tsslMemberTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslMemberTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslMemberTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslMemberTxt.Name = "tsslMemberTxt";
            this.tsslMemberTxt.Size = new System.Drawing.Size(62, 25);
            this.tsslMemberTxt.Text = "Member:";
            this.tsslMemberTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslMember
            // 
            this.tsslMember.AutoSize = false;
            this.tsslMember.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslMember.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslMember.Name = "tsslMember";
            this.tsslMember.Size = new System.Drawing.Size(100, 25);
            this.tsslMember.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslCustomerTxt
            // 
            this.tsslCustomerTxt.AutoSize = false;
            this.tsslCustomerTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslCustomerTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCustomerTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslCustomerTxt.Name = "tsslCustomerTxt";
            this.tsslCustomerTxt.Size = new System.Drawing.Size(70, 25);
            this.tsslCustomerTxt.Text = "Customer:";
            this.tsslCustomerTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslCustomer
            // 
            this.tsslCustomer.AutoSize = false;
            this.tsslCustomer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslCustomer.Name = "tsslCustomer";
            this.tsslCustomer.Size = new System.Drawing.Size(100, 25);
            this.tsslCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSalesMemoTxt
            // 
            this.tsslSalesMemoTxt.AutoSize = false;
            this.tsslSalesMemoTxt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslSalesMemoTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslSalesMemoTxt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslSalesMemoTxt.Name = "tsslSalesMemoTxt";
            this.tsslSalesMemoTxt.Size = new System.Drawing.Size(60, 25);
            this.tsslSalesMemoTxt.Text = "Memo:";
            this.tsslSalesMemoTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSalesMemo
            // 
            this.tsslSalesMemo.AutoSize = false;
            this.tsslSalesMemo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslSalesMemo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslSalesMemo.Name = "tsslSalesMemo";
            this.tsslSalesMemo.Size = new System.Drawing.Size(180, 25);
            this.tsslSalesMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslCustomerMemo
            // 
            this.tsslCustomerMemo.AutoSize = false;
            this.tsslCustomerMemo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslCustomerMemo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslCustomerMemo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslCustomerMemo.Name = "tsslCustomerMemo";
            this.tsslCustomerMemo.Size = new System.Drawing.Size(180, 25);
            this.tsslCustomerMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslWarning
            // 
            this.tsslWarning.AutoSize = false;
            this.tsslWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslWarning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslWarning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslWarning.Name = "tsslWarning";
            this.tsslWarning.Size = new System.Drawing.Size(180, 25);
            this.tsslWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel12
            // 
            this.toolStripStatusLabel12.AutoSize = false;
            this.toolStripStatusLabel12.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel12.Name = "toolStripStatusLabel12";
            this.toolStripStatusLabel12.Size = new System.Drawing.Size(1, 25);
            this.toolStripStatusLabel12.Text = "Customer:";
            this.toolStripStatusLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslSpace1
            // 
            this.tsslSpace1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslSpace1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslSpace1.Name = "tsslSpace1";
            this.tsslSpace1.Size = new System.Drawing.Size(8, 25);
            this.tsslSpace1.Spring = true;
            this.tsslSpace1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label2.Location = new System.Drawing.Point(1005, 417);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 38);
            this.label2.TabIndex = 33;
            this.label2.Text = "Total";
            // 
            // POSMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1276, 742);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ssOtherInformation);
            this.Controls.Add(this.ssApplicationDetails);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.lblTendered);
            this.Controls.Add(this.lblMode_d);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.lblQty_d);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblBarcode);
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
            this.ssApplicationDetails.ResumeLayout(false);
            this.ssApplicationDetails.PerformLayout();
            this.ssOtherInformation.ResumeLayout(false);
            this.ssOtherInformation.PerformLayout();
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
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblQty_d;
        private System.Windows.Forms.Label lblTendered;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblMode_d;
        private System.Windows.Forms.Label lblMode;
        private System.IO.Ports.SerialPort spcustdisp;
        public System.Windows.Forms.DataGridView dgvProduct;
        public System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Timer tmrcheckposdsetting;
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
        private System.Windows.Forms.ToolStripStatusLabel tsslSpace2;
        private System.Windows.Forms.StatusStrip ssOtherInformation;
        private System.Windows.Forms.ToolStripStatusLabel tsslClerk;
        private System.Windows.Forms.ToolStripStatusLabel tsslClerkTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslSalesManTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslSalesMan;
        private System.Windows.Forms.ToolStripStatusLabel tsslMemberTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslMember;
        private System.Windows.Forms.ToolStripStatusLabel tsslSalesMemoTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslSalesMemo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel12;
        private System.Windows.Forms.ToolStripStatusLabel tsslSpace1;
        private System.Windows.Forms.ToolStripStatusLabel tsslCustomerTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsslCustomer;
        private System.Windows.Forms.ToolStripStatusLabel tsslCustomerMemo;
        private System.Windows.Forms.ToolStripStatusLabel tsslWarning;
        private System.Windows.Forms.Label label2;
    }
}