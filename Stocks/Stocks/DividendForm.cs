using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Logging;
using System.Windows.Forms;
using Stocks.BusinessRules;
using Stocks.Entity;

namespace Stocks
{
    public partial class DividendForm : Form
    {
        #region public

        #region methods

        public DividendForm()
        {
            InitializeComponent();
            this.dataGridView1.Columns["PaymentDate"].DefaultCellStyle.Format = "d";
            this.dataGridView1.Columns["Amount"].DefaultCellStyle.Format = "c";
        }

        /// <summary>
        /// Data grid view. 
        /// </summary>
        public DataGridView DGView { get { return this.dataGridView1; } }

        #endregion methods

        #region properties

        public DividendsList Dividends { get; set; }

        /// <summary>
        /// Data repository. 
        /// </summary>
        public static Stocks.DataAccess.IRepository Repository;

        #endregion properties        

        #endregion public

        #region private

        #region methods

        /// <summary>
        /// Saves the dividends. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Logging.LogHelper.LogInfo("Entering DividendForm.btnOK_Click.");

            try
            {
                BR br = new BR(Repository);

                foreach (Dividend dividend in (List<Dividend>)this.dataGridView1.DataSource)
                {
                    if ((dividend.ID == 0) && !string.IsNullOrEmpty(dividend.Symbol))
                    {
                        dividend.Symbol = dividend.Symbol.ToUpper();
                        br.AddDividend(dividend);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting DividendForm.btnOK_Click.");
        }

        /// <summary>
        /// Closes the form. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering DividendForm.btnCancel_Click.");

            this.Close();

            LogHelper.LogInfo("Exiting DividendForm.btnCancel_Click.");
        }

        #endregion methods

       

        #endregion private
    }
}
