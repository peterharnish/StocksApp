namespace Stocks
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.High = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetSalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOpened = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrailingStop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalInvested = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalSharesOwned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDividends = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateClosed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitOverR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.positionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Dividends = new System.Windows.Forms.ToolStripMenuItem();
            this.Purchases = new System.Windows.Forms.ToolStripMenuItem();
            this.Sales = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionBindingSource)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Date:";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(78, 25);
            this.dtpStart.MinDate = new System.DateTime(2009, 1, 1, 0, 0, 0, 0);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 1;
            this.dtpStart.Value = new System.DateTime(2009, 1, 1, 0, 0, 0, 0);
            this.dtpStart.TextChanged += new System.EventHandler(this.datePicker_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "End Date:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(78, 57);
            this.dtpEndDate.MinDate = new System.DateTime(2009, 1, 1, 0, 0, 0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.TextChanged += new System.EventHandler(this.datePicker_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mode:";
            // 
            // cbxMode
            // 
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Items.AddRange(new object[] {
            "Current",
            "History"});
            this.cbxMode.Location = new System.Drawing.Point(346, 24);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(121, 21);
            this.cbxMode.TabIndex = 5;
            this.cbxMode.Text = "Current";
            this.cbxMode.SelectedIndexChanged += new System.EventHandler(this.cbxMode_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbol,
            this.CurrentPrice,
            this.High,
            this.TargetSalePrice,
            this.DateOpened,
            this.TrailingStop,
            this.TotalInvested,
            this.TotalSharesOwned,
            this.TotalDividends,
            this.TotalR,
            this.DateClosed,
            this.TotalProfit,
            this.ProfitOverR});
            this.dataGridView1.DataSource = this.positionBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1044, 488);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // Symbol
            // 
            this.Symbol.DataPropertyName = "Symbol";
            this.Symbol.HeaderText = "Symbol";
            this.Symbol.Name = "Symbol";
            // 
            // CurrentPrice
            // 
            this.CurrentPrice.DataPropertyName = "CurrentPrice";
            this.CurrentPrice.HeaderText = "Current Price";
            this.CurrentPrice.Name = "CurrentPrice";
            // 
            // High
            // 
            this.High.DataPropertyName = "High";
            this.High.HeaderText = "High";
            this.High.Name = "High";
            // 
            // TargetSalePrice
            // 
            this.TargetSalePrice.DataPropertyName = "TargetSalePrice";
            this.TargetSalePrice.HeaderText = "Target Sale Price";
            this.TargetSalePrice.Name = "TargetSalePrice";
            // 
            // DateOpened
            // 
            this.DateOpened.DataPropertyName = "DateOpened";
            this.DateOpened.HeaderText = "Date Opened";
            this.DateOpened.Name = "DateOpened";
            // 
            // TrailingStop
            // 
            this.TrailingStop.DataPropertyName = "TrailingStop";
            this.TrailingStop.HeaderText = "Trailing Stop";
            this.TrailingStop.Name = "TrailingStop";
            // 
            // TotalInvested
            // 
            this.TotalInvested.DataPropertyName = "TotalInvested";
            this.TotalInvested.HeaderText = "Total Invested";
            this.TotalInvested.Name = "TotalInvested";
            // 
            // TotalSharesOwned
            // 
            this.TotalSharesOwned.DataPropertyName = "TotalSharesOwned";
            this.TotalSharesOwned.HeaderText = "Total Shares Owned";
            this.TotalSharesOwned.Name = "TotalSharesOwned";
            // 
            // TotalDividends
            // 
            this.TotalDividends.DataPropertyName = "TotalDividends";
            this.TotalDividends.HeaderText = "Total Dividends";
            this.TotalDividends.Name = "TotalDividends";
            // 
            // TotalR
            // 
            this.TotalR.DataPropertyName = "TotalR";
            this.TotalR.HeaderText = "Total R";
            this.TotalR.Name = "TotalR";
            // 
            // DateClosed
            // 
            this.DateClosed.DataPropertyName = "DateClosed";
            this.DateClosed.HeaderText = "Date Closed";
            this.DateClosed.Name = "DateClosed";
            this.DateClosed.Visible = false;
            // 
            // TotalProfit
            // 
            this.TotalProfit.DataPropertyName = "TotalProfit";
            this.TotalProfit.HeaderText = "Total Profit";
            this.TotalProfit.Name = "TotalProfit";
            this.TotalProfit.Visible = false;
            // 
            // ProfitOverR
            // 
            this.ProfitOverR.DataPropertyName = "ProfitOverR";
            this.ProfitOverR.HeaderText = "Profit Over R";
            this.ProfitOverR.Name = "ProfitOverR";
            this.ProfitOverR.Visible = false;
            // 
            // positionBindingSource
            // 
            this.positionBindingSource.DataSource = typeof(Stocks.Entity.Position);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Dividends,
            this.Purchases,
            this.Sales});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 70);
            // 
            // Dividends
            // 
            this.Dividends.Name = "Dividends";
            this.Dividends.Size = new System.Drawing.Size(127, 22);
            this.Dividends.Text = "Dividends";
            this.Dividends.Click += new System.EventHandler(this.Dividends_Click);
            // 
            // Purchases
            // 
            this.Purchases.Name = "Purchases";
            this.Purchases.Size = new System.Drawing.Size(127, 22);
            this.Purchases.Text = "Purchases";
            this.Purchases.Click += new System.EventHandler(this.Purchases_Click);
            // 
            // Sales
            // 
            this.Sales.Name = "Sales";
            this.Sales.Size = new System.Drawing.Size(127, 22);
            this.Sales.Text = "Sales";
            this.Sales.Click += new System.EventHandler(this.Sales_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(504, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 600);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbxMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Stocks";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionBindingSource)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

       

       

        

        

       
        

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxMode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource positionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn High;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetSalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOpened;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrailingStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalInvested;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalSharesOwned;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDividends;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalR;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateClosed;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProfit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitOverR;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Dividends;
        private System.Windows.Forms.ToolStripMenuItem Purchases;
        private System.Windows.Forms.ToolStripMenuItem Sales;
        private System.Windows.Forms.Button btnSave;
    }
}

