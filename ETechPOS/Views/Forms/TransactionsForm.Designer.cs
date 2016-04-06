namespace ETech.Views.Forms
{
    partial class TransactionsForm
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
            this.lblCurrentTransactionTxt = new System.Windows.Forms.Label();
            this.lblCurrentTransaction = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalAmountTxt = new System.Windows.Forms.Label();
            this.cgTransactions = new ETech.Views.Generic_Controls.ControlGrid();
            this.btnESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCurrentTransactionTxt
            // 
            this.lblCurrentTransactionTxt.AutoSize = true;
            this.lblCurrentTransactionTxt.Font = new System.Drawing.Font("Arial Narrow", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTransactionTxt.Location = new System.Drawing.Point(12, 18);
            this.lblCurrentTransactionTxt.Name = "lblCurrentTransactionTxt";
            this.lblCurrentTransactionTxt.Size = new System.Drawing.Size(232, 33);
            this.lblCurrentTransactionTxt.TabIndex = 1;
            this.lblCurrentTransactionTxt.Text = "Current Transaction:";
            // 
            // lblCurrentTransaction
            // 
            this.lblCurrentTransaction.AutoSize = true;
            this.lblCurrentTransaction.Font = new System.Drawing.Font("Arial Narrow", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTransaction.Location = new System.Drawing.Point(255, 18);
            this.lblCurrentTransaction.Name = "lblCurrentTransaction";
            this.lblCurrentTransaction.Size = new System.Drawing.Size(139, 33);
            this.lblCurrentTransaction.TabIndex = 2;
            this.lblCurrentTransaction.Text = "Transaction";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial Narrow", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(687, 18);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(97, 33);
            this.lblTotalAmount.TabIndex = 4;
            this.lblTotalAmount.Text = "Amount";
            // 
            // lblTotalAmountTxt
            // 
            this.lblTotalAmountTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmountTxt.AutoSize = true;
            this.lblTotalAmountTxt.Font = new System.Drawing.Font("Arial Narrow", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmountTxt.Location = new System.Drawing.Point(512, 18);
            this.lblTotalAmountTxt.Name = "lblTotalAmountTxt";
            this.lblTotalAmountTxt.Size = new System.Drawing.Size(163, 33);
            this.lblTotalAmountTxt.TabIndex = 3;
            this.lblTotalAmountTxt.Text = "Total Amount:";
            // 
            // cgTransactions
            // 
            this.cgTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cgTransactions.ControlType = typeof(ETech.Views.Controls.TransactionControl);
            this.cgTransactions.DataSource = null;
            this.cgTransactions.GridDimensions = new System.Drawing.Size(3, 4);
            this.cgTransactions.GridSpacing = new System.Drawing.Size(15, 15);
            this.cgTransactions.Location = new System.Drawing.Point(12, 62);
            this.cgTransactions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cgTransactions.Name = "cgTransactions";
            this.cgTransactions.ShowFirstAndLast = false;
            this.cgTransactions.ShowPageNumber = true;
            this.cgTransactions.ShowPagingButtons = true;
            this.cgTransactions.Size = new System.Drawing.Size(1000, 629);
            this.cgTransactions.TabIndex = 1;
            this.cgTransactions.Initialize += new System.EventHandler(this.cgTransactions_Initialize);
            // 
            // btnESC
            // 
            this.btnESC.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnESC.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(420, 706);
            this.btnESC.Margin = new System.Windows.Forms.Padding(4);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(185, 47);
            this.btnESC.TabIndex = 105;
            this.btnESC.Text = "Close (ESC)";
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // TransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btnESC);
            this.Controls.Add(this.cgTransactions);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalAmountTxt);
            this.Controls.Add(this.lblCurrentTransaction);
            this.Controls.Add(this.lblCurrentTransactionTxt);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "TransactionsForm";
            this.Text = "Transactions";
            this.Load += new System.EventHandler(this.TransactionsForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransactionsForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentTransactionTxt;
        private System.Windows.Forms.Label lblCurrentTransaction;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalAmountTxt;
        private ETech.Views.Generic_Controls.ControlGrid cgTransactions;
        private System.Windows.Forms.Button btnESC;
    }
}