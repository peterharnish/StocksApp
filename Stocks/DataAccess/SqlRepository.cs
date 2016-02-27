using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using Logging;
using Stocks.Entity;

namespace Stocks.DataAccess
{
    public class SqlRepository : IRepository
    {
        string connString = System.Configuration.ConfigurationManager.AppSettings["connstring"];
        const string ID = "ID";
        const string Symbol = "Symbol";
        const string DateOpened = "DateOpened";
        const string CurrentPrice = "CurrentPrice";
        const string High = "High";
        const string Stop = "Stop";
        const string TargetSalePrice = "TargetSalePrice";
        const string PositionId = "PositionId";
        const string PaymentDate = "PaymentDate";
        const string Amount = "Amount";
        const string NumberOfShares = "NumberOfShares";
        const string TotalPrice = "TotalPrice";
        const string R = "R";
        const string PurchaseDate = "PurchaseDate";
        const string SaleDate = "SaleDate";
        const string TotalProfit = "TotalProfit";

        /// <summary>
        /// Inserts a position. 
        /// </summary>
        /// <param name="position"> Position object. </param>
        /// <returns> ID of inserted position. </returns>
        public int InsertPosition(Position position)
        {
            //LogHelper.LogInfo("Entering SqlRepository.InsertPosition.");
            
            int id;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Symbol", position.Symbol);
                arrParam[1] = new SqlParameter("@Stop", position.TrailingStop);
                arrParam[2] = new SqlParameter("@TargetSalePrice", position.TargetSalePrice);
                arrParam[3] = new SqlParameter("@Ret", SqlDbType.Int, 4);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "InsertPosition", arrParam);
                id = Convert.ToInt32(arrParam[3].Value);
            }

            //LogHelper.LogInfo(string.Format("Exiting SqlRepository.InsertPosition with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Inserts a purchase. 
        /// </summary>
        /// <param name="purchase"> Purchase entity. </param>
        /// <returns> ID of inserted purchase. </returns>
        public int InsertPurchase(Purchase purchase)
        {
            //LogHelper.LogInfo("Entering SqlRepository.InsertPurchase.");

            int id;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[5];
                arrParam[0] = new SqlParameter("@PositionId", purchase.PositionID);
                arrParam[1] = new SqlParameter("@NumberOfShares", purchase.NumberOfShares);
                arrParam[2] = new SqlParameter("@TotalPrice", purchase.TotalPrice);
                arrParam[3] = new SqlParameter("@R", purchase.R);
                arrParam[4] = new SqlParameter("@Ret", SqlDbType.Int, 4);
                arrParam[4].Direction = ParameterDirection.InputOutput;

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "InsertPurchase", arrParam);
                id = Convert.ToInt32(arrParam[4].Value);
            }

            //LogHelper.LogInfo(string.Format("Exiting SqlRepository.InsertPurchase with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Inserts a sale. 
        /// </summary>
        /// <param name="sale"> Sale entity. </param>
        /// <returns> ID of inserted sale. </returns>
        public int InsertSale(Sale sale)
        {
            //LogHelper.LogInfo("Entering SqlRepository.InsertSale.");

            int id;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@NumberOfShares", sale.NumberOfShares);
                arrParam[1] = new SqlParameter("@TotalPrice", sale.TotalPrice);
                arrParam[2] = new SqlParameter("@PositionId", sale.PositionID);
                arrParam[3] = new SqlParameter("@Ret", SqlDbType.Int, 4);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "InsertSale", arrParam);
                id = Convert.ToInt32(arrParam[3].Value);
            }

            //LogHelper.LogInfo(string.Format("Exiting SqlRepository.InsertSale with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Inserts a dividend record. 
        /// </summary>
        /// <param name="dividend"> Dividend entity. </param>
        /// <returns> ID of inserted dividend record. </returns>
        public int InsertDividend(Dividend dividend)
        {
           // LogHelper.LogInfo("Entering SqlRepository.InsertDividend");

            int id;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@PositionId", dividend.PositionID);
                arrParam[1] = new SqlParameter("@Amount", dividend.Amount);
                arrParam[2] = new SqlParameter("@Ret", SqlDbType.Int, 4);
                arrParam[2].Direction = ParameterDirection.InputOutput;

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "InsertDividend", arrParam);
                id = Convert.ToInt32(arrParam[2].Value);
            }

            //LogHelper.LogInfo(string.Format("Exiting SqlRepository.InsertDividend with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Gets a list of open positions. 
        /// </summary>
        /// <returns>A list of open positions. </returns>
        public List<Position> GetCurrentPositions()
        {
            //LogHelper.LogInfo("Entering SqlRepository.GetCurrentPositions.");

            List<Position> positions = new List<Position>();
            DataSet ds;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GetCurrentPositions");
            }

            DataTable dtPositions = ds.Tables[0];

            foreach (DataRow dr in dtPositions.Rows)
            {
                Position position = new Position();
                position.ID = Convert.ToInt32(dr[ID]);
                position.Symbol = Convert.ToString(dr[Symbol]);
                position.DateOpened = Convert.ToDateTime(dr[DateOpened]);
                position.CurrentPrice = Convert.ToDecimal(dr[CurrentPrice]);
                position.High = Convert.ToDecimal(dr[High]);
                position.TrailingStop = Convert.ToDecimal(dr[Stop]);
                position.TargetSalePrice = Convert.ToDecimal(dr[TargetSalePrice]);
                position.Dividends = new List<Dividend>();
                position.Sales = new List<Sale>();
                position.Purchases = new List<Purchase>();
                positions.Add(position);
            }

            DataTable dtDividends = ds.Tables[1];

            foreach (DataRow dr in dtDividends.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);
                Position position = positions.Where(x => x.ID == positionId).First();

                Dividend dividend = new Dividend();
                dividend.ID = Convert.ToInt32(dr[ID]);
                dividend.PositionID = positionId;
                dividend.PaymentDate = Convert.ToDateTime(dr[PaymentDate]);
                dividend.Amount = Convert.ToDecimal(dr[Amount]);
                dividend.Symbol = position.Symbol;

                position.Dividends.Add(dividend);
            }

            DataTable dtPurchases = ds.Tables[2];

            foreach (DataRow dr in dtPurchases.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);
                Position position = positions.Where(x => x.ID == positionId).First();

                Purchase purchase = new Purchase();
                purchase.ID = Convert.ToInt32(dr[ID]);
                purchase.PositionID = positionId;
                purchase.NumberOfShares = Convert.ToInt32(dr[NumberOfShares]);
                purchase.TotalPrice = Convert.ToDecimal(dr[TotalPrice]);
                purchase.R = Convert.ToDecimal(dr[R]);
                purchase.PurchaseDate = Convert.ToDateTime(dr[PurchaseDate]);
                purchase.Symbol = position.Symbol;

                position.Purchases.Add(purchase);
            }

            DataTable dtSales = ds.Tables[3];

            foreach (DataRow dr in dtSales.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);
                Position position = positions.Where(x => x.ID == positionId).First();

                Sale sale = new Sale();
                sale.ID = Convert.ToInt32(dr[ID]);
                sale.PositionID = positionId;
                sale.SaleDate = Convert.ToDateTime(dr[SaleDate]);
                sale.NumberOfShares = Convert.ToInt32(dr[NumberOfShares]);
                sale.TotalPrice = Convert.ToDecimal(dr[TotalPrice]);
                sale.Symbol = position.Symbol;

                position.Sales.Add(sale);
            }

            //LogHelper.LogInfo("Exiting SqlRepository.GetCurrentPositions.");
            return positions;
        }

        /// <summary>
        /// Gets the positions between two different dates. 
        /// </summary>
        /// <param name="startDate"> Start date. </param>
        /// <param name="endDate"> End date. </param>
        /// <returns> A list of positions opened and closed between 2 different dates. </returns>
        public List<Position> GetHistoryPositions(DateTime startDate, DateTime endDate)
        {
            //LogHelper.LogInfo(string.Format("Entering SqlRepository.GetHistoryPositions with startDate = {0} and endDate = {1}.", startDate.ToString(), endDate.ToString()));

            List<Position> positions = new List<Position>();
            DataSet ds;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@StartDate", startDate);
                arrParam[1] = new SqlParameter("@DateClosed", endDate);
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GetHistoryPositions", arrParam);
            }

            DataTable dtPositions = ds.Tables[0];

            foreach (DataRow dr in dtPositions.Rows)
            {
                Position position = new Position();
                position.ID = Convert.ToInt32(dr[ID]);
                position.Symbol = Convert.ToString(dr[Symbol]);
                position.DateOpened = Convert.ToDateTime(dr[DateOpened]);
                position.CurrentPrice = Convert.ToDecimal(dr[CurrentPrice]);
                position.High = Convert.ToDecimal(dr[High]);
                position.TrailingStop = Convert.ToDecimal(dr[Stop]);
                position.DateClosed = Convert.ToDateTime(dr["DateClosed"]);
                position.TotalProfit = Convert.ToDecimal(dr[TotalProfit]);
                position.Dividends = new List<Dividend>();
                position.Sales = new List<Sale>();
                position.Purchases = new List<Purchase>();
                positions.Add(position);
            }

            DataTable dtDividends = ds.Tables[1];

            foreach (DataRow dr in dtDividends.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);
                Position position = positions.Where(x => x.ID == positionId).First();

                Dividend dividend = new Dividend();
                dividend.ID = Convert.ToInt32(dr[ID]);
                dividend.PositionID = positionId;
                dividend.PaymentDate = Convert.ToDateTime(dr[PaymentDate]);
                dividend.Amount = Convert.ToDecimal(dr[Amount]);
                dividend.Symbol = position.Symbol;

                position.Dividends.Add(dividend);
            }

            DataTable dtPurchases = ds.Tables[2];

            foreach (DataRow dr in dtPurchases.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);
                Position position = positions.Where(x => x.ID == positionId).First();

                Purchase purchase = new Purchase();
                purchase.ID = Convert.ToInt32(dr[ID]);
                purchase.PositionID = positionId;
                purchase.NumberOfShares = Convert.ToInt32(dr[NumberOfShares]);
                purchase.TotalPrice = Convert.ToDecimal(dr[TotalPrice]);
                purchase.R = Convert.ToDecimal(dr[R]);
                purchase.PurchaseDate = Convert.ToDateTime(dr[PurchaseDate]);
                purchase.Symbol = position.Symbol;

                position.Purchases.Add(purchase);
            }

            DataTable dtSales = ds.Tables[3];

            foreach (DataRow dr in dtSales.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);
                Position position = positions.Where(x => x.ID == positionId).First();

                Sale sale = new Sale();
                sale.ID = Convert.ToInt32(dr[ID]);
                sale.PositionID = positionId;
                sale.SaleDate = Convert.ToDateTime(dr[SaleDate]);
                sale.NumberOfShares = Convert.ToInt32(dr[NumberOfShares]);
                sale.TotalPrice = Convert.ToDecimal(dr[TotalPrice]);
                sale.Symbol = position.Symbol;

                position.Sales.Add(sale);
            }

            //LogHelper.LogInfo("Exiting SqlRepository.GetHistoryPositions.");
            return positions;
        }

        /// <summary>
        /// Closes a position.
        /// </summary>
        /// <param name="position"> Position entity. </param>
        public void ClosePosition(Position position)
        {
            //LogHelper.LogInfo("Entering SqlRepository.ClosePosition.");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[2];
                arrParam[0] = new SqlParameter("@ID", position.ID);
                arrParam[1] = new SqlParameter("@TotalProfit", position.TotalProfit);
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ClosePosition", arrParam);
            }

            //LogHelper.LogInfo("Exiting SqlRepository.ClosePosition.");
        }

        public void UpdateTrailingStop(Position position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the current stock price. 
        /// </summary>
        /// <param name="position"> Position entity. </param>
        public void UpdateCurrent(Position position)
        {
            //LogHelper.LogInfo("Entering SqlRepository.UpdateCurrent.");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[3];
                arrParam[0] = new SqlParameter("@ID", position.ID);
                arrParam[1] = new SqlParameter("@CurrentPrice", position.CurrentPrice);
                arrParam[2] = new SqlParameter("@High", position.High);
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "UpdateCurrentPrice", arrParam);
            }

            //LogHelper.LogInfo("Exiting SqlRepository.UpdateCurrent.");
        }

        /// <summary>
        /// Gets the position with the given ID. 
        /// </summary>
        /// <param name="symbol">Sticker symbol. </param>
        /// <returns> ID of open position with the given symbol. </returns>
        public int GetPositionID(string symbol)
        {
            //LogHelper.LogInfo(string.Format("Entering SqlRepository.GetPositionID with symbol = {0}.", symbol));

            int id = 0;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetPositionID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Symbol", symbol));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    break;
                }
            }

            //LogHelper.LogInfo(string.Format("Exiting SqlRepository.GetPositionID with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Gets the position with the given symbol. 
        /// </summary>
        /// <param name="symbol"> Stock ticker symbol. </param>
        /// <returns> Open position with the given symbol. </returns>
        public Position GetPositionBySymbol(string symbol)
        {
            //LogHelper.LogInfo(string.Format("Entering SqlRepository.GetPositionBySymbol with symbol = {0}.", symbol));

            Position position = null;

            DataSet ds;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@Symbol", symbol);
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GetPositionBySymbol", arrParam);
            }

            DataTable dtPositions = ds.Tables[0];

            foreach (DataRow dr in dtPositions.Rows)
            {
                position = new Position();
                position.ID = Convert.ToInt32(dr[ID]);
                position.Symbol = Convert.ToString(dr[Symbol]);
                position.DateOpened = Convert.ToDateTime(dr[DateOpened]);
                position.CurrentPrice = Convert.ToDecimal(dr[CurrentPrice]);
                position.High = Convert.ToDecimal(dr[High]);
                position.TrailingStop = Convert.ToDecimal(dr[Stop]);
                position.TargetSalePrice = Convert.ToDecimal(dr[TargetSalePrice]);
                position.TotalProfit = dr[TotalProfit] != DBNull.Value ? Convert.ToDecimal(dr[TotalProfit]) : (decimal?)null;
                position.Dividends = new List<Dividend>();
                position.Sales = new List<Sale>();
                position.Purchases = new List<Purchase>();
            }

            DataTable dtDividends = ds.Tables[1];

            foreach (DataRow dr in dtDividends.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);

                Dividend dividend = new Dividend();
                dividend.ID = Convert.ToInt32(dr[ID]);
                dividend.PositionID = positionId;
                dividend.PaymentDate = Convert.ToDateTime(dr[PaymentDate]);
                dividend.Amount = Convert.ToDecimal(dr[Amount]);
                dividend.Symbol = position.Symbol;

                position.Dividends.Add(dividend);
            }

            DataTable dtPurchases = ds.Tables[2];

            foreach (DataRow dr in dtPurchases.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);

                Purchase purchase = new Purchase();
                purchase.ID = Convert.ToInt32(dr[ID]);
                purchase.PositionID = positionId;
                purchase.NumberOfShares = Convert.ToInt32(dr[NumberOfShares]);
                purchase.TotalPrice = Convert.ToDecimal(dr[TotalPrice]);
                purchase.R = Convert.ToDecimal(dr[R]);
                purchase.PurchaseDate = Convert.ToDateTime(dr[PurchaseDate]);
                purchase.Symbol = position.Symbol;

                position.Purchases.Add(purchase);
            }

            DataTable dtSales = ds.Tables[3];

            foreach (DataRow dr in dtSales.Rows)
            {
                int positionId = Convert.ToInt32(dr[PositionId]);

                Sale sale = new Sale();
                sale.ID = Convert.ToInt32(dr[ID]);
                sale.PositionID = positionId;
                sale.SaleDate = Convert.ToDateTime(dr[SaleDate]);
                sale.NumberOfShares = Convert.ToInt32(dr[NumberOfShares]);
                sale.TotalPrice = Convert.ToDecimal(dr[TotalPrice]);
                sale.Symbol = position.Symbol;

                position.Sales.Add(sale);
            }

            //LogHelper.LogInfo("Exiting SqlRepository.GetPositionBySymbol.");
            return position;
        }
    }
}
