using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logging;
using Stocks.BusinessRules;
using Stocks.Entity;

namespace Stocks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormatColumns();
            GetCurrentPositions();
            SetDates();
        }

        #region private

        #region members

        /// <summary>
        ///  List of positions.
        /// </summary>
        private List<Position> Positions;

        /// <summary>
        /// Business rules. 
        /// </summary>
        private BR br = new BR(Repository);

        /// <summary>
        /// Data grid view row index.
        /// </summary>
        private int rowIndex;

        #endregion members

        #region methods       

        /// <summary>
        /// Fills the data grid view with the current positions.
        /// </summary>
        private void GetCurrentPositions()
        {
            LogHelper.LogInfo("Entering Form1.GetCurrentPositions.");

            try
            {
                this.Positions = this.br.GetCurrent();
                this.dataGridView1.DataSource = this.Positions;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.GetCurrentPositions.");
        }

        /// <summary>
        /// Formats the columns. 
        /// </summary>
        private void FormatColumns()
        {
            LogHelper.LogInfo("Entering Form1.FormatColumns.");

            this.dataGridView1.Columns["CurrentPrice"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["High"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["TargetSalePrice"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["TotalInvested"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["TotalDividends"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["TotalR"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["TotalProfit"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["DateOpened"].DefaultCellStyle.Format = "d";
            this.dataGridView1.Columns["DateClosed"].DefaultCellStyle.Format = "d";
            this.dataGridView1.Columns["TrailingStop"].DefaultCellStyle.Format = "P0";
            this.dataGridView1.Columns["ProfitOverR"].DefaultCellStyle.Format = "P0";

            LogHelper.LogInfo("Exiting Form1.FormatColumns.");
        }

        /// <summary>
        /// Sets maximum date times. 
        /// </summary>
        private void SetDates()
        {
            LogHelper.LogInfo("Entering Form1.SetDates.");

            this.dtpStart.MaxDate = DateTime.Now;
            this.dtpEndDate.Value = DateTime.Now;
            this.dtpEndDate.MaxDate = DateTime.Now;
            this.dtpStart.Value = new DateTime(DateTime.Now.Year, 1, 1);

            LogHelper.LogInfo("Exiting Form1.SetDates.");
        }

        /// <summary>
        /// Sets start date and end dates. 
        /// </summary>
        /// <param name="sender"> Date picker. </param>
        /// <param name="e"></param>
        private void datePicker_TextChanged(object sender, System.EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.datePicker_TextChanged.");

            if (this.cbxMode.Text == "History")
            {
                GetHistory();
            }

            LogHelper.LogInfo("Exiting Form1.datePicker_TextChanged.");
        }

        /// <summary>
        /// Gets closed positions. 
        /// </summary>
        private void GetHistory()
        {
            LogHelper.LogInfo("Entering Form1.GetHistory.");

            try
            {
                this.Positions = this.br.GetHistory(this.dtpStart.Value, new DateTime(this.dtpEndDate.Value.Year, this.dtpEndDate.Value.Month, this.dtpEndDate.Value.Day, 23, 59, 59));
                this.dataGridView1.DataSource = this.Positions;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.GetHistory.");
        }

        /// <summary>
        /// Changes the mode from current to history.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.cbxMode_SelectedIndexChanged.");

            if (this.cbxMode.Text == "Current")
            {
                GetCurrentPositions();
                this.CurrentPrice.Visible = true;
                this.High.Visible = true;
                this.TargetSalePrice.Visible = true;
                this.TrailingStop.Visible = true;
                this.DateClosed.Visible = false;
                this.TotalProfit.Visible = false;
                this.ProfitOverR.Visible = false;
                this.dataGridView1.Width = 1050;
            }
            else
            {
                GetHistory();
                this.DateClosed.Visible = true;
                this.TotalProfit.Visible = true;
                this.ProfitOverR.Visible = true;
                this.CurrentPrice.Visible = false;
                this.High.Visible = false;
                this.TargetSalePrice.Visible = false;
                this.TrailingStop.Visible = false;
                this.dataGridView1.Width = 950;
            }

            LogHelper.LogInfo("Exiting Form1.cbxMode_SelectedIndexChanged.");
        }

        /// <summary>
        /// Opens the context menu. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.dataGridView1_CellMouseClick.");

            if ((e.Button == System.Windows.Forms.MouseButtons.Right) && (this.cbxMode.Text == "Current"))
            {
                this.rowIndex = e.RowIndex;
                this.contextMenuStrip1.Show(MousePosition);
            }

            LogHelper.LogInfo("Exiting Form1.dateGridView1_CellMouseClick.");
        }

        /// <summary>
        /// Brings up the dividends form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dividends_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.Dividends_Click.");

            DividendForm.Repository = Repository;
            DividendForm dividendForm = new DividendForm();
            dividendForm.Dividends = new DividendsList(((Position)this.dataGridView1.Rows[this.rowIndex].DataBoundItem).Dividends.OrderByDescending(x => x.PaymentDate).Take(10).ToList<Dividend>());            
            dividendForm.DGView.DataSource = dividendForm.Dividends;

            if (dividendForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetCurrentPositions();
            }

            LogHelper.LogInfo("Exiting Form1.Dividends_Click.");
        }

        /// <summary>
        /// Displays the purchases form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Purchases_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.Purchases_Click.");

            PurchasesForm.Repository = Repository;
            PurchasesForm purchasesForm = new PurchasesForm();
            purchasesForm.Purchases = new PurchasesList(((Position)this.dataGridView1.Rows[this.rowIndex].DataBoundItem).Purchases.OrderByDescending(x => x.PurchaseDate).Take(10).ToList<Purchase>());
            purchasesForm.DGView.DataSource = purchasesForm.Purchases;

            if (purchasesForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetCurrentPositions();
            }

            LogHelper.LogInfo("Exiting Form1.Purchases_Click.");
        }

        /// <summary>
        /// Brings up the sales form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sales_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.Sales_Click.");

            SalesForm.Repository = Repository;
            SalesForm salesForm = new SalesForm();
            salesForm.Sales = new SalesList(((Position)this.dataGridView1.Rows[this.rowIndex].DataBoundItem).Sales.OrderByDescending(x => x.SaleDate).Take(10).ToList<Sale>());
            salesForm.DGView.DataSource = salesForm.Sales;

            if (salesForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetCurrentPositions();
            }

            LogHelper.LogInfo("Exiting Form1.Sales_Click.");
        }

        /// <summary>
        /// Saves the trailing stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.btnSave_Click.");

            if (this.cbxMode.Text == "Current")
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    try
                    {
                        Position position = (Position)row.DataBoundItem;
                        position.TrailingStop = Convert.ToDecimal(row.Cells["TrailingStop"].Value);
                        this.br.UpdateTrailingStop(position);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(ex.Message, ex);
                        MessageBox.Show(ex.Message);
                    }
                }

                GetCurrentPositions();
            }

            LogHelper.LogInfo("Exiting Form1.btnSave_Click.");
        }

        /// <summary>
        /// Colors certain cells red.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataBindingComplete(object sender, System.Windows.Forms.DataGridViewBindingCompleteEventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.dataGridView1_DataBindingComplete.");

            DataGridViewRow lastRow = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1];

            if (this.cbxMode.Text == "Current")
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    Position position = (Position)row.DataBoundItem;

                    if (position.IsTrailingStopHigh)
                    {
                        row.Cells["TrailingStop"].Style.ForeColor = Color.Red;
                    }

                    if (position.IsCurrentPriceLow)
                    {
                        row.Cells["CurrentPrice"].Style.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["TotalProfit"].Value) < 0)
                    {
                        row.Cells["TotalProfit"].Style.ForeColor = Color.Red;
                    }

                    if (Convert.ToDouble(row.Cells["ProfitOverR"].Value) < 0)
                    {
                        row.Cells["ProfitOverR"].Style.ForeColor = Color.Red;
                    }
                }
            }

            foreach (DataGridViewCell cell in lastRow.Cells)
            {
                cell.Style.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, dataGridView1.DefaultCellStyle.Font.Size, FontStyle.Bold);
            }

            LogHelper.LogInfo("Entering Form1.dataGridView1_DataBindingComplete.");
        }

        #endregion methods   

        #endregion private

        #region public

        /// <summary>
        /// Data repository.
        /// </summary>
        public static Stocks.DataAccess.IRepository Repository;

        #endregion public
    }
}
