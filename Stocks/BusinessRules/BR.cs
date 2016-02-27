using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Logging;
using Stocks.DataAccess;
using Stocks.Entity;

namespace Stocks.BusinessRules
{
    public class BR
    {
        #region public

        #region properties

        /// <summary>
        /// Repository interface. 
        /// </summary>
        public IRepository Repository;

        #endregion properties

        #region methods

        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="rep"> Repository. </param>
        public BR(IRepository rep)
        {
            this.Repository = rep;
        }

        /// <summary>
        /// Gets open positions. 
        /// </summary>
        /// <returns> A list of open positions. </returns>
        public List<Position> GetCurrent()
        {
            //LogHelper.LogInfo("Entering BR.GetCurrent.");

            List<Position> positions = Repository.GetCurrentPositions();

            foreach (var position in positions)
            {
                CalculateFieldValues(position);
            }

            positions.Add(new Position()
            {
                Symbol = "TOTAL",
                TotalDividends = positions.Sum(x => x.TotalDividends),
                TotalInvested = positions.Sum(x => x.TotalInvested),
                TotalR = positions.Sum(x => x.TotalR)
            });


            //LogHelper.LogInfo("Exiting BR.GetCurrent.");
            return positions;
        }

        /// <summary>
        /// Gets position history. 
        /// </summary>
        /// <param name="startDate"> Start date. </param>
        /// <param name="endDate"> End date. </param>
        /// <returns> A list of positions. </returns>
        public List<Position> GetHistory(DateTime startDate, DateTime endDate)
        {
            //LogHelper.LogInfo("Entering BR.GetHistory.");
            List<Position> positions = Repository.GetHistoryPositions(startDate, endDate);

            foreach (var position in positions)
            {
                CalculateFieldValues(position);
            }

            positions.Add(new Position()
            {
                Symbol = "TOTAL",
                TotalInvested = positions.Sum(x => x.TotalInvested),
                TotalDividends = positions.Sum(x => x.TotalDividends),
                TotalR = positions.Sum(x => x.TotalR),
                TotalProfit = positions.Sum(x => x.TotalProfit),
                ProfitOverR = (double)(positions.Sum(x => x.TotalProfit) / positions.Sum(x => x.TotalR))
            });

            //LogHelper.LogInfo("Exiting BR.GetHistory.");
            return positions;
        }

        /// <summary>
        /// Records a purchase transaction. 
        /// </summary>
        /// <param name="purchase"> Purchase element. </param>
        /// <returns> ID of purchase. </returns>
        public int PurchaseShares(Purchase purchase)
        {
            //LogHelper.LogInfo("Entering BR.PurchaseShares.");
            Position position = this.Repository.GetPositionBySymbol(purchase.Symbol);           

            if (position == null)
            {
                position = new Position()
                {
                    DateOpened = DateTime.Now,
                    Symbol = purchase.Symbol,
                    TrailingStop = purchase.TrailingStop,
                    CurrentPrice = 0m,
                    High = 0m,
                    TargetSalePrice = (1m - (decimal)purchase.TrailingStop) * purchase.TotalPrice / purchase.NumberOfShares
                };

                purchase.PositionID = this.Repository.InsertPosition(position);
            }
            else
            {
                purchase.PositionID = position.ID;
            }

           // purchase.R = ((purchase.TotalPrice - commission) / purchase.NumberOfShares * (decimal)position.TrailingStop) * purchase.NumberOfShares + commission;
            purchase.R = purchase.TotalPrice - (purchase.NumberOfShares * (decimal)position.TargetSalePrice - 2 * commission);
            purchase.ID = this.Repository.InsertPurchase(purchase);

            //LogHelper.LogInfo(string.Format("Exiting BR.PurchaseShares with purchase.ID = {0}.", purchase.ID.ToString()));
            return purchase.ID;
        }

        /// <summary>
        /// Records a dividend. 
        /// </summary>
        /// <param name="dividend"> Dividend to record. </param>
        /// <returns>ID of dividend. </returns>
        public int AddDividend(Dividend dividend)
        {
            //LogHelper.LogInfo("Entering BR.AddDividend.");
            dividend.PositionID = this.Repository.GetPositionID(dividend.Symbol);
            dividend.ID = this.Repository.InsertDividend(dividend);

            //LogHelper.LogInfo(string.Format("Exiting BR.AddDividend with dividend.ID = {0}.", dividend.ID.ToString()));
            return dividend.ID;
        }

        /// <summary>
        /// Records a sale. 
        /// </summary>
        /// <param name="sale"> Sale to record. </param>
        /// <returns>ID of sale. </returns>
        public int SellStock(Sale sale)
        {
            //LogHelper.LogInfo("Entering BR.SellStock.");
            Position position = GetPositionBySymbol(sale.Symbol);

            sale.PositionID = position.ID;
            sale.IsClose |= sale.NumberOfShares == position.TotalSharesOwned;

            if (sale.IsClose)
            {
                this.Repository.ClosePosition(position);
                sale.NumberOfShares = (double)position.TotalSharesOwned;
            }

            sale.ID = this.Repository.InsertSale(sale);

            //LogHelper.LogInfo(string.Format("Exiting BR.SellStock with sale.ID = {0}.", sale.ID.ToString()));
            return sale.ID;
        }

        /// <summary>
        /// Gets the current open position given a symbol. 
        /// </summary>
        /// <param name="symbol"> Stock symbol. </param>
        /// <returns> Current open position. </returns>
        public Position GetPositionBySymbol(string symbol)
        {
            //LogHelper.LogInfo(string.Format("Entering BR.GetPositionBySymbol with symbol = {0}.", symbol));
            Position position = this.Repository.GetPositionBySymbol(symbol);

            CalculateFieldValues(position);

            //LogHelper.LogInfo("Exiting BR.GetPositionBySymbol.");
            return position;
        }

        /// <summary>
        /// Updates the stock price.
        /// </summary>
        /// <param name="position">Position entity. </param>
        public void UpdateStockPrice(Position position)
        {
            //LogHelper.LogInfo("Entering BR.UpdateStockPrice.");

            if (position.CurrentPrice > position.High)
            {
                position.High = position.CurrentPrice;
            }

            this.Repository.UpdateCurrent(position);
            //LogHelper.LogInfo("Exiting BR.UpdateStockPrice.");
        }

        /// <summary>
        /// Updates the trailing stop. 
        /// </summary>
        /// <param name="position"> Position element. </param>
        public void UpdateTrailingStop(Position position)
        {
           // LogHelper.LogInfo("Entering BR.UpdateTrailingStop.");
            this.Repository.UpdateTrailingStop(position);
            //LogHelper.LogInfo("Exiting BR.UpdateTrailingStop.");
        }

        #endregion methods

        #endregion public

        #region private

        #region methods

        /// <summary>
        /// Calculates values of calculated fields. 
        /// </summary>
        /// <param name="position"> Position element to calculate. </param>
        private void CalculateFieldValues(Position position)
        {
            //LogHelper.LogInfo("Entering BR.CalculateFieldValues.");

            try
            {
                if (position.DateClosed == null)
                {
                    position.TotalSharesOwned = position.Purchases.Sum(x => x.NumberOfShares) - position.Sales.Sum(x => x.NumberOfShares);
                }
                else
                {
                    position.TotalSharesOwned = position.Purchases.Sum(x => x.NumberOfShares);
                }

                position.TotalInvested = position.Purchases.Sum(x => x.TotalPrice);
                position.TotalR = position.Purchases.Sum(x => x.R);
                position.TotalDividends = position.Dividends.Sum(x => x.Amount);
                position.TotalProfit = position.Sales.Sum(x => x.TotalPrice) - position.TotalInvested;
                position.ProfitOverR = (double)position.TotalProfit / (double)position.TotalR;

                //if (position.TotalSharesOwned > 0)
                //{
                //    position.TargetSalePrice = position.High * (1m - (decimal)position.TrailingStop);
                //}

                decimal lowPurchasePrice = position.Purchases.Min(x => (x.TotalPrice - commission) / x.NumberOfShares);

                position.IsTrailingStopHigh = (position.High >= 1.5m * position.CurrentPrice && position.TrailingStop > 0.1m)
                    || (position.High >= 2m * lowPurchasePrice && position.TrailingStop > 0.05m);

                position.IsCurrentPriceLow = position.CurrentPrice <= position.TargetSalePrice;
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex.Message, ex);
            }

            //LogHelper.LogInfo("Exiting BR.CalculateFieldValues.");
        }

        /// <summary>
        /// Commission.
        /// </summary>
        private const decimal commission = 8.95m;

        #endregion methods

        #endregion private
    }
}
