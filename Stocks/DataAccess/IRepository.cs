using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Stocks.Entity;

namespace Stocks.DataAccess
{
    public interface IRepository
    {
        int InsertPosition(Position position);
        int InsertPurchase(Purchase purchase);
        int InsertSale(Sale sale);
        int InsertDividend(Dividend dividend);
        List<Position> GetCurrentPositions();
        List<Position> GetHistoryPositions(DateTime startDate, DateTime endDate);
        void ClosePosition(Position position);
        void UpdateTrailingStop(Position position);
        void UpdateCurrent(Position position);
        int GetPositionID(string symbol);
        Position GetPositionBySymbol(string symbol);
    }
}
