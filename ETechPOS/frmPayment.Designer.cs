namespace ETech
{
    partial class frmPayment
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
            this.lblTendered = new System.Windows.Forms.Label();
            this.lblCashRcv = new System.Windows.Forms.Label();
            this.txtCashRcv_d = new System.Windows.Forms.TextBox();
            this.lblCreditCard_d = new System.Windows.Forms.Label();
            this.lblDebitCard_d = new System.Windows.Forms.Label();
            this.lblGiftCheque_d = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblBankCheque_d = new System.Windows.Forms.Label();
            this.lblRemaining_d = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.lblPointUsed = new System.Windows.Forms.Label();
            this.txtPointUsed_d = new System.Windows.Forms.TextBox();
            this.lblremainingpts = new System.Windows.Forms.Label();
            this.lblremainingpts_d = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOnlineDealRcv_d = new System.Windows.Forms.TextBox();
            this.txtEplusRcv_d = new System.Windows.Forms.TextBox();
            this.txtSMACPtsRcv_d = new System.Windows.Forms.TextBox();
            this.txtCouponsRcv_d = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnCreditCard = new System.Windows.Forms.Button();
            this.btnDebitCard = new System.Windows.Forms.Button();
            this.btnGiftCheque = new System.Windows.Forms.Button();
            this.btnBankCheque = new System.Windows.Forms.Button();
            this.btnCustomPayment = new System.Windows.Forms.Button();
            this.lblCustomAmt_d = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTendered
            // 
            this.lblTendered.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTendered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTendered.Location = new System.Drawing.Point(12, 21);
            this.lblTendered.Name = "lblTendered";
            this.lblTendered.Size = new System.Drawing.Size(544, 55);
            this.lblTendered.TabIndex = 0;
            this.lblTendered.Text = "TENDERED";
            this.lblTendered.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCashRcv
            // 
            this.lblCashRcv.AutoSize = true;
            this.lblCashRcv.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblCashRcv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCashRcv.Location = new System.Drawing.Point(12, 96);
            this.lblCashRcv.Name = "lblCashRcv";
            this.lblCashRcv.Size = new System.Drawing.Size(296, 38);
            this.lblCashRcv.TabIndex = 1;
            this.lblCashRcv.Text = "CASH RECEIVED:";
            // 
            // txtCashRcv_d
            // 
            this.txtCashRcv_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtCashRcv_d.Location = new System.Drawing.Point(371, 93);
            this.txtCashRcv_d.MaxLength = 20;
            this.txtCashRcv_d.Name = "txtCashRcv_d";
            this.txtCashRcv_d.Size = new System.Drawing.Size(185, 45);
            this.txtCashRcv_d.TabIndex = 5;
            this.txtCashRcv_d.Tag = "num";
            this.txtCashRcv_d.Text = "0.00";
            this.txtCashRcv_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCashRcv_d.TextChanged += new System.EventHandler(this.txtCashRcv_d_TextChanged);
            // 
            // lblCreditCard_d
            // 
            this.lblCreditCard_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblCreditCard_d.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCreditCard_d.Location = new System.Drawing.Point(371, 140);
            this.lblCreditCard_d.Name = "lblCreditCard_d";
            this.lblCreditCard_d.Size = new System.Drawing.Size(185, 36);
            this.lblCreditCard_d.TabIndex = 6;
            this.lblCreditCard_d.Text = "0.00";
            this.lblCreditCard_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDebitCard_d
            // 
            this.lblDebitCard_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblDebitCard_d.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDebitCard_d.Location = new System.Drawing.Point(371, 185);
            this.lblDebitCard_d.Name = "lblDebitCard_d";
            this.lblDebitCard_d.Size = new System.Drawing.Size(185, 36);
            this.lblDebitCard_d.TabIndex = 7;
            this.lblDebitCard_d.Text = "0.00";
            this.lblDebitCard_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGiftCheque_d
            // 
            this.lblGiftCheque_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblGiftCheque_d.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGiftCheque_d.Location = new System.Drawing.Point(371, 230);
            this.lblGiftCheque_d.Name = "lblGiftCheque_d";
            this.lblGiftCheque_d.Size = new System.Drawing.Size(185, 36);
            this.lblGiftCheque_d.TabIndex = 8;
            this.lblGiftCheque_d.Text = "0.00";
            this.lblGiftCheque_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTotal.Location = new System.Drawing.Point(320, 423);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(236, 45);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLine
            // 
            this.lblLine.Location = new System.Drawing.Point(371, 410);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(187, 13);
            this.lblLine.TabIndex = 12;
            this.lblLine.Text = "______________________________________";
            // 
            // lblBankCheque_d
            // 
            this.lblBankCheque_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblBankCheque_d.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBankCheque_d.Location = new System.Drawing.Point(371, 275);
            this.lblBankCheque_d.Name = "lblBankCheque_d";
            this.lblBankCheque_d.Size = new System.Drawing.Size(185, 36);
            this.lblBankCheque_d.TabIndex = 15;
            this.lblBankCheque_d.Text = "0.00";
            this.lblBankCheque_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemaining_d
            // 
            this.lblRemaining_d.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining_d.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRemaining_d.Location = new System.Drawing.Point(465, 465);
            this.lblRemaining_d.Name = "lblRemaining_d";
            this.lblRemaining_d.Size = new System.Drawing.Size(91, 22);
            this.lblRemaining_d.TabIndex = 16;
            this.lblRemaining_d.Text = "0.00";
            this.lblRemaining_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemaining
            // 
            this.lblRemaining.AutoSize = true;
            this.lblRemaining.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRemaining.Location = new System.Drawing.Point(373, 467);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(86, 16);
            this.lblRemaining.TabIndex = 17;
            this.lblRemaining.Text = "Remaining:";
            // 
            // lblPointUsed
            // 
            this.lblPointUsed.AutoSize = true;
            this.lblPointUsed.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblPointUsed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPointUsed.Location = new System.Drawing.Point(12, 365);
            this.lblPointUsed.Name = "lblPointUsed";
            this.lblPointUsed.Size = new System.Drawing.Size(228, 38);
            this.lblPointUsed.TabIndex = 18;
            this.lblPointUsed.Text = "POINT USED:";
            // 
            // txtPointUsed_d
            // 
            this.txtPointUsed_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtPointUsed_d.Location = new System.Drawing.Point(371, 362);
            this.txtPointUsed_d.MaxLength = 20;
            this.txtPointUsed_d.Name = "txtPointUsed_d";
            this.txtPointUsed_d.Size = new System.Drawing.Size(185, 45);
            this.txtPointUsed_d.TabIndex = 6;
            this.txtPointUsed_d.Tag = "num";
            this.txtPointUsed_d.Text = "0.00";
            this.txtPointUsed_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPointUsed_d.TextChanged += new System.EventHandler(this.txtPointUsed_d_TextChanged);
            // 
            // lblremainingpts
            // 
            this.lblremainingpts.AutoSize = true;
            this.lblremainingpts.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblremainingpts.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblremainingpts.Location = new System.Drawing.Point(45, 403);
            this.lblremainingpts.Name = "lblremainingpts";
            this.lblremainingpts.Size = new System.Drawing.Size(112, 16);
            this.lblremainingpts.TabIndex = 21;
            this.lblremainingpts.Text = "Remaining pts:";
            // 
            // lblremainingpts_d
            // 
            this.lblremainingpts_d.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblremainingpts_d.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblremainingpts_d.Location = new System.Drawing.Point(163, 401);
            this.lblremainingpts_d.Name = "lblremainingpts_d";
            this.lblremainingpts_d.Size = new System.Drawing.Size(91, 22);
            this.lblremainingpts_d.TabIndex = 20;
            this.lblremainingpts_d.Text = "0.00";
            this.lblremainingpts_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(565, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 38);
            this.label2.TabIndex = 22;
            this.label2.Text = "E-PLUS:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(565, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 38);
            this.label3.TabIndex = 23;
            this.label3.Text = "SMAC PTS:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(565, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 38);
            this.label4.TabIndex = 24;
            this.label4.Text = "Online Deals:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 24.75F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(565, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 38);
            this.label5.TabIndex = 25;
            this.label5.Text = "Coupons:";
            // 
            // txtOnlineDealRcv_d
            // 
            this.txtOnlineDealRcv_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtOnlineDealRcv_d.Location = new System.Drawing.Point(779, 190);
            this.txtOnlineDealRcv_d.MaxLength = 20;
            this.txtOnlineDealRcv_d.Name = "txtOnlineDealRcv_d";
            this.txtOnlineDealRcv_d.Size = new System.Drawing.Size(184, 45);
            this.txtOnlineDealRcv_d.TabIndex = 8;
            this.txtOnlineDealRcv_d.Tag = "num";
            this.txtOnlineDealRcv_d.Text = "0.00";
            this.txtOnlineDealRcv_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOnlineDealRcv_d.TextChanged += new System.EventHandler(this.txtOnlineDealRcv_d_TextChanged);
            // 
            // txtEplusRcv_d
            // 
            this.txtEplusRcv_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtEplusRcv_d.Location = new System.Drawing.Point(779, 89);
            this.txtEplusRcv_d.MaxLength = 20;
            this.txtEplusRcv_d.Name = "txtEplusRcv_d";
            this.txtEplusRcv_d.Size = new System.Drawing.Size(184, 45);
            this.txtEplusRcv_d.TabIndex = 6;
            this.txtEplusRcv_d.Tag = "num";
            this.txtEplusRcv_d.Text = "0.00";
            this.txtEplusRcv_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEplusRcv_d.TextChanged += new System.EventHandler(this.txtEplusRcv_d_TextChanged);
            // 
            // txtSMACPtsRcv_d
            // 
            this.txtSMACPtsRcv_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtSMACPtsRcv_d.Location = new System.Drawing.Point(779, 140);
            this.txtSMACPtsRcv_d.MaxLength = 20;
            this.txtSMACPtsRcv_d.Name = "txtSMACPtsRcv_d";
            this.txtSMACPtsRcv_d.Size = new System.Drawing.Size(184, 45);
            this.txtSMACPtsRcv_d.TabIndex = 7;
            this.txtSMACPtsRcv_d.Tag = "num";
            this.txtSMACPtsRcv_d.Text = "0.00";
            this.txtSMACPtsRcv_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSMACPtsRcv_d.TextChanged += new System.EventHandler(this.txtSMACPtsRcv_d_TextChanged);
            // 
            // txtCouponsRcv_d
            // 
            this.txtCouponsRcv_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.txtCouponsRcv_d.Location = new System.Drawing.Point(779, 236);
            this.txtCouponsRcv_d.MaxLength = 20;
            this.txtCouponsRcv_d.Name = "txtCouponsRcv_d";
            this.txtCouponsRcv_d.Size = new System.Drawing.Size(184, 45);
            this.txtCouponsRcv_d.TabIndex = 9;
            this.txtCouponsRcv_d.Tag = "num";
            this.txtCouponsRcv_d.Text = "0.00";
            this.txtCouponsRcv_d.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCouponsRcv_d.TextChanged += new System.EventHandler(this.txtCouponsRcv_d_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(17, 449);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 38);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "Okay (F1)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnESC
            // 
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(143, 449);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(139, 38);
            this.btnESC.TabIndex = 34;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnCreditCard
            // 
            this.btnCreditCard.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreditCard.Location = new System.Drawing.Point(9, 137);
            this.btnCreditCard.Name = "btnCreditCard";
            this.btnCreditCard.Size = new System.Drawing.Size(348, 45);
            this.btnCreditCard.TabIndex = 36;
            this.btnCreditCard.Text = "CREDIT CARD: (F8)";
            this.btnCreditCard.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreditCard.UseVisualStyleBackColor = true;
            this.btnCreditCard.Click += new System.EventHandler(this.btnCreditCard_Click);
            // 
            // btnDebitCard
            // 
            this.btnDebitCard.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebitCard.Location = new System.Drawing.Point(9, 182);
            this.btnDebitCard.Name = "btnDebitCard";
            this.btnDebitCard.Size = new System.Drawing.Size(348, 45);
            this.btnDebitCard.TabIndex = 37;
            this.btnDebitCard.Text = "DEBIT CARD: (F9)";
            this.btnDebitCard.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDebitCard.UseVisualStyleBackColor = true;
            this.btnDebitCard.Click += new System.EventHandler(this.btnDebitCard_Click);
            // 
            // btnGiftCheque
            // 
            this.btnGiftCheque.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiftCheque.Location = new System.Drawing.Point(9, 227);
            this.btnGiftCheque.Name = "btnGiftCheque";
            this.btnGiftCheque.Size = new System.Drawing.Size(348, 45);
            this.btnGiftCheque.TabIndex = 38;
            this.btnGiftCheque.Text = "GIFT CHEQUE: (F10)";
            this.btnGiftCheque.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGiftCheque.UseVisualStyleBackColor = true;
            this.btnGiftCheque.Click += new System.EventHandler(this.btnGiftCheque_Click);
            // 
            // btnBankCheque
            // 
            this.btnBankCheque.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBankCheque.Location = new System.Drawing.Point(9, 272);
            this.btnBankCheque.Name = "btnBankCheque";
            this.btnBankCheque.Size = new System.Drawing.Size(348, 45);
            this.btnBankCheque.TabIndex = 39;
            this.btnBankCheque.Text = "BANK CHEQUE: (F11)";
            this.btnBankCheque.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBankCheque.UseVisualStyleBackColor = true;
            // 
            // btnCustomPayment
            // 
            this.btnCustomPayment.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomPayment.Location = new System.Drawing.Point(9, 318);
            this.btnCustomPayment.Name = "btnCustomPayment";
            this.btnCustomPayment.Size = new System.Drawing.Size(348, 45);
            this.btnCustomPayment.TabIndex = 41;
            this.btnCustomPayment.Text = "CUSTOM PAYMENT (F12)";
            this.btnCustomPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomPayment.UseVisualStyleBackColor = true;
            this.btnCustomPayment.Click += new System.EventHandler(this.btnCustomPayment_Click);
            // 
            // lblCustomAmt_d
            // 
            this.lblCustomAmt_d.Font = new System.Drawing.Font("Arial", 24.75F);
            this.lblCustomAmt_d.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCustomAmt_d.Location = new System.Drawing.Point(371, 321);
            this.lblCustomAmt_d.Name = "lblCustomAmt_d";
            this.lblCustomAmt_d.Size = new System.Drawing.Size(185, 36);
            this.lblCustomAmt_d.TabIndex = 40;
            this.lblCustomAmt_d.Text = "0.00";
            this.lblCustomAmt_d.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCustomAmt_d.TextChanged += new System.EventHandler(this.lblCustomAmt_TextChanged);
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 493);
            this.Controls.Add(this.btnCustomPayment);
            this.Controls.Add(this.lblCustomAmt_d);
            this.Controls.Add(this.btnBankCheque);
            this.Controls.Add(this.btnGiftCheque);
            this.Controls.Add(this.btnDebitCard);
            this.Controls.Add(this.btnCreditCard);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.txtCouponsRcv_d);
            this.Controls.Add(this.txtSMACPtsRcv_d);
            this.Controls.Add(this.txtEplusRcv_d);
            this.Controls.Add(this.txtOnlineDealRcv_d);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblremainingpts);
            this.Controls.Add(this.lblremainingpts_d);
            this.Controls.Add(this.txtPointUsed_d);
            this.Controls.Add(this.lblPointUsed);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.lblRemaining_d);
            this.Controls.Add(this.lblBankCheque_d);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblGiftCheque_d);
            this.Controls.Add(this.lblDebitCard_d);
            this.Controls.Add(this.lblCreditCard_d);
            this.Controls.Add(this.txtCashRcv_d);
            this.Controls.Add(this.lblCashRcv);
            this.Controls.Add(this.lblTendered);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPayment_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTendered;
        private System.Windows.Forms.Label lblCashRcv;
        private System.Windows.Forms.TextBox txtCashRcv_d;
        private System.Windows.Forms.Label lblCreditCard_d;
        private System.Windows.Forms.Label lblDebitCard_d;
        private System.Windows.Forms.Label lblGiftCheque_d;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblBankCheque_d;
        private System.Windows.Forms.Label lblRemaining_d;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblPointUsed;
        private System.Windows.Forms.TextBox txtPointUsed_d;
        private System.Windows.Forms.Label lblremainingpts;
        private System.Windows.Forms.Label lblremainingpts_d;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOnlineDealRcv_d;
        private System.Windows.Forms.TextBox txtEplusRcv_d;
        private System.Windows.Forms.TextBox txtSMACPtsRcv_d;
        private System.Windows.Forms.TextBox txtCouponsRcv_d;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnCreditCard;
        private System.Windows.Forms.Button btnDebitCard;
        private System.Windows.Forms.Button btnGiftCheque;
        private System.Windows.Forms.Button btnBankCheque;
        private System.Windows.Forms.Button btnCustomPayment;
        private System.Windows.Forms.Label lblCustomAmt_d;
    }
}