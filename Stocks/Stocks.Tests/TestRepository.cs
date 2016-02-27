using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stocks.DataAccess;
using Stocks.Entity;

namespace Stocks.Tests
{
    public class TestRepository : IRepository
    {
        #region public

        #region methods

        public int InsertPosition(Entity.Position position)
        {
            return 1;
        }

        public int InsertPurchase(Entity.Purchase purchase)
        {
            return 1;
        }

        public int InsertSale(Entity.Sale sale)
        {
            return 1;
        }

        public int InsertDividend(Entity.Dividend dividend)
        {
            return 1;
        }

        public List<Entity.Position> GetCurrentPositions()
        {
            return this.Positions;
        }

        public List<Entity.Position> GetHistoryPositions(DateTime startDate, DateTime endDate)
        {
            return this.Positions;
        }

        public void ClosePosition(Entity.Position position)
        {
            
        }

        public void UpdateTrailingStop(Entity.Position position)
        {
            throw new NotImplementedException();
        }

        public void UpdateCurrent(Entity.Position position)
        {
            throw new NotImplementedException();
        }

        public int GetPositionID(string symbol)
        {
            return 1;
        }

        public Position GetPositionBySymbol(string symbol)
        {
            return new Position() { Purchases = new List<Purchase>(), Sales = new List<Sale>(), Dividends = new List<Dividend>() };
        }

        #endregion methods

        #region properties

        public List<Position> Positions { get; set; }

        #endregion properties

        #endregion public





        
    }
}
