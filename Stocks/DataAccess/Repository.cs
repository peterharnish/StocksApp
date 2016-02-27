using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
//using Logging;
using Stocks.Entity;

namespace Stocks.DataAccess
{
    public class Repository : IRepository
    {
        #region public

        #region methods

        /// <summary>
        /// Inserts a position into the database. 
        /// </summary>
        /// <param name="position"> Position to insert. </param>
        /// <returns> ID of position. </returns>
        public int InsertPosition(Entity.Position position)
        {
           // LogHelper.LogInfo("Entering Repository.InsertPosition.");
            OleDbConnection conn = null;
            int id = 0;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("INSERT into Positions (Symbol, DateOpened, CurrentPrice, High, TrailingStop, TargetSalePrice) Values(@Symbol, @DateOpened, @CurrentPrice, @High, @TrailingStop, @TargetSalePrice)");

                command.Connection = conn;
                command.Parameters.Add("@Symbol", OleDbType.VarChar).Value = position.Symbol;
                command.Parameters.Add("@DateOpened", OleDbType.Date).Value = position.DateOpened;
                command.Parameters.Add("@CurrentPrice", OleDbType.Currency).Value = position.CurrentPrice;
                command.Parameters.Add("@High", OleDbType.Currency).Value = position.High;
                command.Parameters.Add("@TrailingStop", OleDbType.Double).Value = position.TrailingStop;
                command.Parameters.Add("@TargetSalePrice", OleDbType.Currency).Value = position.TargetSalePrice;
                command.ExecuteNonQuery();

                command.CommandText = SelectIdentity;
                id = (int)command.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo(string.Format("Exiting Repository.InsertPosition with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Inserts a purchase. 
        /// </summary>
        /// <param name="purchase"> Purchase to insert. </param>
        /// <returns> ID of inserted purchase. </returns>
        public int InsertPurchase(Entity.Purchase purchase)
        {
            //LogHelper.LogInfo("Entering Repository.InsertPurchase.");
            OleDbConnection conn = null;
            int id = 0;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("INSERT into Purchases (PositionID, NumberOfShares, TotalPrice, R, PurchaseDate) Values(@PositionID, @NumberOfShares, @TotalPrice, @R, @PurchaseDate)");

                command.Connection = conn;
                command.Parameters.Add("@PositionID", OleDbType.BigInt).Value = purchase.PositionID;
                command.Parameters.Add("@NumberOfShares", OleDbType.Integer).Value = purchase.NumberOfShares;
                command.Parameters.Add("@TotalPrice", OleDbType.Currency).Value = purchase.TotalPrice;
                command.Parameters.Add("@R", OleDbType.Currency).Value = purchase.R;
                command.Parameters.Add("@PurchaseDate", OleDbType.Date).Value = purchase.PurchaseDate;
                command.ExecuteNonQuery();

                command.CommandText = SelectIdentity;
                id = (int)command.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo(string.Format("Exiting Repository.InsertPurchase with id = {0}.", id.ToString()));
            return id;
        }

        public int InsertSale(Entity.Sale sale)
        {
            //LogHelper.LogInfo("Entering Repository.InsertSale.");
            OleDbConnection conn = null;
            int id = 0;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("INSERT into Sales (SaleDate, NumberOfShares, TotalPrice, PositionID) Values(@SaleDate, @NumberOfShares, @TotalPrice, @PositionID)");

                command.Connection = conn;
                command.Parameters.Add("@SaleDate", OleDbType.Date).Value = sale.SaleDate;
                command.Parameters.Add("@NumberOfShares", OleDbType.Integer).Value = sale.NumberOfShares;
                command.Parameters.Add("@TotalPrice", OleDbType.Currency).Value = sale.TotalPrice;
                command.Parameters.Add("@PositionID", OleDbType.BigInt).Value = sale.PositionID; 
                command.ExecuteNonQuery();

                command.CommandText = SelectIdentity;
                id = (int)command.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo(string.Format("Exiting Repository.InsertSale with id = {0}.", id.ToString()));
            return id;
        }

        public int InsertDividend(Entity.Dividend dividend)
        {
            //LogHelper.LogInfo("Entering Repository.InsertDividend.");
            OleDbConnection conn = null;
            int id = 0;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("INSERT into Dividends (PositionID, PaymentDate, Amount) Values(@PositionID, @PaymentDate, @Amount)");

                command.Connection = conn;
                command.Parameters.Add("@PositionID", OleDbType.BigInt).Value = dividend.PositionID;
                command.Parameters.Add("@PaymentDate", OleDbType.Date).Value = dividend.PaymentDate;
                command.Parameters.Add("@Amount", OleDbType.Currency).Value = dividend.Amount;
                command.ExecuteNonQuery();

                command.CommandText = SelectIdentity;
                id = (int)command.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo(string.Format("Exiting Repository.InsertDividend with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Gets the current positions.
        /// </summary>
        /// <returns> A list of current positions. </returns>
        public List<Entity.Position> GetCurrentPositions()
        {
            //LogHelper.LogInfo("Entering Repository.GetCurrentPositions.");

            OleDbConnection conn = null;
            OleDbDataReader reader = null;
            List<Entity.Position> positions = new List<Entity.Position>();

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("SELECT Positions.* FROM Positions WHERE (((Positions.DateClosed) Is Null)) ORDER BY Positions.Symbol;");
                command.Connection = conn;               
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    positions.Add(new Position()
                    {
                        ID = reader.GetInt32(0),
                        Symbol = reader.GetString(1),
                        DateOpened = reader.GetDateTime(2),
                        CurrentPrice = reader.GetDecimal(3),
                        High = reader.GetDecimal(4),
                        TrailingStop = reader.GetDecimal(5),
                        TargetSalePrice = reader.GetDecimal(8),
                        Dividends = new List<Dividend>(),
                        Purchases = new List<Purchase>(),
                        Sales = new List<Sale>()
                    });
                }

                OleDbCommand command1 = new OleDbCommand("SELECT Dividends.* FROM Dividends INNER JOIN Positions ON Dividends.PositionID = Positions.ID WHERE (((Positions.DateClosed) Is Null));");
                command1.Connection = conn;
                reader = command1.ExecuteReader();

                while (reader.Read())
                {
                    Position position = positions.Where(x => x.ID == reader.GetInt32(1)).First();

                    position.Dividends.Add(new Dividend()
                    {
                        ID = reader.GetInt32(0),
                        PositionID = reader.GetInt32(1),
                        PaymentDate = reader.GetDateTime(2),
                        Amount = reader.GetDecimal(3),
                        Symbol = position.Symbol
                    });
                }

                OleDbCommand command2 = new OleDbCommand("SELECT Purchases.* FROM Purchases INNER JOIN Positions ON Purchases.PositionID = Positions.ID WHERE (((Positions.DateClosed) Is Null));");
                command2.Connection = conn;
                reader = command2.ExecuteReader();

                while (reader.Read())
                {
                    Position position = positions.Where(x => x.ID == reader.GetInt32(1)).First();

                    position.Purchases.Add(new Purchase()
                    {
                        ID = reader.GetInt32(0),
                        PositionID = reader.GetInt32(1),
                        NumberOfShares = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        R = reader.GetDecimal(4),
                        PurchaseDate = reader.GetDateTime(5),
                        Symbol = position.Symbol
                    });
                }

                OleDbCommand command3 = new OleDbCommand("SELECT Sales.* FROM Sales Inner Join Positions on Sales.PositionID = Positions.ID WHERE (((Positions.DateClosed) Is Null));");
                command3.Connection = conn;
                reader = command3.ExecuteReader();

                while (reader.Read())
                {
                    Position position = positions.Where(x => x.ID == reader.GetInt32(4)).First();

                    position.Sales.Add(new Sale()
                    {
                        ID = reader.GetInt32(0),
                        SaleDate = reader.GetDateTime(1),
                        NumberOfShares = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        PositionID = reader.GetInt32(4),
                        Symbol = position.Symbol
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo("Exiting Repository.GetCurrentPositions.");
            return positions;
        }

        /// <summary>
        /// Gets the history of positions.
        /// </summary>
        /// <param name="startDate"> Start date to check from. </param>
        /// <param name="endDate"> End date to query from. </param>
        /// <returns> List of history positions. </returns>
        public List<Entity.Position> GetHistoryPositions(DateTime startDate, DateTime endDate)
        {
            //LogHelper.LogInfo("Entering Repository.GetHistoryPositions.");

            OleDbConnection conn = null;
            OleDbDataReader reader = null;
            List<Entity.Position> positions = new List<Entity.Position>();           

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand(
                    string.Format("SELECT Positions.* FROM Positions WHERE ((((Positions.DateClosed)>=#{0}#)) And (((Positions.DateClosed)<=#{1}#))) ORDER BY Positions.DateClosed;",
                    startDate.ToString("d"),
                    endDate.ToString("d")));

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    positions.Add(new Position()
                    {
                        ID = reader.GetInt32(0),
                        Symbol = reader.GetString(1),
                        DateOpened = reader.GetDateTime(2),
                        DateClosed = !reader.IsDBNull(6) ? reader.GetDateTime(6) : (DateTime?)null,
                        TotalProfit = !reader.IsDBNull(7) ? reader.GetDecimal(7) : (decimal?)null,
                        Dividends = new List<Dividend>(),
                        Purchases = new List<Purchase>(),
                        Sales = new List<Sale>()
                    });
                }

                OleDbCommand command1 = new OleDbCommand(
                    string.Format("SELECT Dividends.* FROM Dividends INNER JOIN Positions ON Dividends.PositionID = Positions.ID WHERE (((Positions.DateClosed)>=#{0}#)) And ((((Positions.DateClosed)<=#{1}#)));",
                    startDate.ToString("d"),
                    endDate.ToString("d")));

                command1.Connection = conn;
                reader = command1.ExecuteReader();

                while (reader.Read())
                {
                    Position position = positions.Where(x => x.ID == reader.GetInt32(1)).First();

                    position.Dividends.Add(new Dividend()
                    {
                        ID = reader.GetInt32(0),
                        PositionID = reader.GetInt32(1),
                        PaymentDate = reader.GetDateTime(2),
                        Amount = reader.GetDecimal(3)
                    });
                }

                OleDbCommand command2 = new OleDbCommand(string.Format("SELECT Purchases.* FROM Purchases INNER JOIN Positions ON Purchases.PositionID = Positions.ID WHERE (((Positions.DateClosed)>=#{0}#)) And ((((Positions.DateClosed)<=#{1}#)));",
                    startDate.ToString("d"),
                    endDate.ToString("d")));

                command2.Connection = conn;
                reader = command2.ExecuteReader();

                while (reader.Read())
                {
                    Position position = positions.Where(x => x.ID == reader.GetInt32(1)).First();

                    position.Purchases.Add(new Purchase()
                    {
                        ID = reader.GetInt32(0),
                        PositionID = reader.GetInt32(1),
                        NumberOfShares = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        R = reader.GetDecimal(4),
                        PurchaseDate = reader.GetDateTime(5)
                    });
                }

                OleDbCommand command3 = new OleDbCommand(string.Format("SELECT Sales.* FROM Sales Inner Join Positions on Sales.PositionID = Positions.ID WHERE (((Positions.DateClosed)>=#{0}#)) And ((((Positions.DateClosed)<=#{1}#)));",
                    startDate.ToString("d"),
                    endDate.ToString("d")));

                command3.Connection = conn;
                reader = command3.ExecuteReader();

                while (reader.Read())
                {
                    Position position = positions.Where(x => x.ID == reader.GetInt32(4)).First();

                    position.Sales.Add(new Sale()
                    {
                        ID = reader.GetInt32(0),
                        SaleDate = reader.GetDateTime(1),
                        NumberOfShares = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        PositionID = reader.GetInt32(4)
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

           // LogHelper.LogInfo("Exiting Repository.GetHistoryPositions.");
            return positions;
        }

        /// <summary>
        /// Closes a position. 
        /// </summary>
        /// <param name="position"> Position to close. </param>
        public void ClosePosition(Entity.Position position)
        {
           // LogHelper.LogInfo("Entering Repository.ClosePosition.");
            OleDbConnection conn = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand update = new OleDbCommand(
                    string.Format("UPDATE Positions SET DateClosed = #{0}#, TotalProfit = {1} WHERE ID = {2}",
                    DateTime.Now.ToString("d"),
                    position.TotalProfit.ToString(),
                    position.ID.ToString()),
                    conn);

                update.ExecuteNonQuery();
                
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

           // LogHelper.LogInfo("Exiting Repository.ClosePosition.");
        }

        /// <summary>
        /// Updates the trailing stop. 
        /// </summary>
        /// <param name="position"> Position entity. </param>
        public void UpdateTrailingStop(Entity.Position position)
        {
            //LogHelper.LogInfo("Entering Repository.UpdateTrailingStop.");
            OleDbConnection conn = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand update = new OleDbCommand(
                    string.Format("UPDATE Positions SET TrailingStop = {0} WHERE ID = {1}",
                    position.TrailingStop.ToString(),
                    position.ID.ToString()),
                    conn);

                update.ExecuteNonQuery();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo("Exiting Repository.UpdateTrailingStop.");
        }

        /// <summary>
        /// Updates the highest stock price.
        /// </summary>
        /// <param name="position"> Position element. </param>
        public void UpdateCurrent(Entity.Position position)
        {
           // LogHelper.LogInfo("Entering Repository.UpdateCurrent.");
            OleDbConnection conn = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand update = new OleDbCommand(
                    string.Format("UPDATE Positions SET CurrentPrice = {0}, High = {1} WHERE ID = {2}",
                    position.CurrentPrice.ToString(),
                    position.High.ToString(),
                    position.ID.ToString()),
                    conn);

                update.ExecuteNonQuery();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo("Exiting Repository.UpdateCurrent.");
        }

        /// <summary>
        /// Gets the position ID. 
        /// </summary>
        /// <param name="symbol"> Stock symbol. </param>
        /// <returns> ID of position. If the position is not there, then 0. </returns>
        public int GetPositionID(string symbol)
        {
            //LogHelper.LogInfo(string.Format("Entering Repository.GetPositionID with symbol = {0}.", symbol));
            OleDbConnection conn = null;
            OleDbDataReader reader = null;
            int positionID = 0;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand(string.Format("SELECT Positions.ID FROM Positions WHERE (((Positions.DateClosed) Is Null) AND (Positions.Symbol = '{0}'));", symbol));
                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    positionID = reader.GetInt32(0);                    
                }                
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo(string.Format("Exiting Repository.GetPositionID with positionID = {0}.", positionID.ToString()));
            return positionID;
        }

        /// <summary>
        /// Gets the current open position given a symbol. 
        /// </summary>
        /// <param name="symbol"> Stock symbol. </param>
        /// <returns> Current open position. </returns>
        public Position GetPositionBySymbol(string symbol)
        {
           // LogHelper.LogInfo(string.Format("Entering Repository.GetPositionBySymbol with symbol = {0}.", symbol));
            OleDbConnection conn = null;
            OleDbDataReader reader = null;
            Entity.Position position = null;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand(string.Format("SELECT Positions.* FROM Positions WHERE ((((Positions.DateClosed) Is Null)) AND (Positions.Symbol = '{0}')) ORDER BY Positions.Symbol;", symbol));
                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    position = new Position()
                    {
                        ID = reader.GetInt32(0),
                        Symbol = reader.GetString(1),
                        DateOpened = reader.GetDateTime(2),
                        CurrentPrice = reader.GetDecimal(3),
                        High = reader.GetDecimal(4),
                        TrailingStop = reader.GetDecimal(5),
                        Dividends = new List<Dividend>(),
                        Purchases = new List<Purchase>(),
                        Sales = new List<Sale>()
                    };
                }

                OleDbCommand command1 = new OleDbCommand(string.Format("SELECT Dividends.* FROM Dividends INNER JOIN Positions ON Dividends.PositionID = Positions.ID WHERE ((((Positions.DateClosed) Is Null)) AND (Positions.Symbol = '{0}'));", symbol));
                command1.Connection = conn;
                reader = command1.ExecuteReader();

                while (reader.Read())
                {
                    position.Dividends.Add(new Dividend()
                    {
                        ID = reader.GetInt32(0),
                        PositionID = reader.GetInt32(1),
                        PaymentDate = reader.GetDateTime(2),
                        Amount = reader.GetDecimal(3)
                    });
                }

                OleDbCommand command2 = new OleDbCommand(string.Format("SELECT Purchases.* FROM Purchases INNER JOIN Positions ON Purchases.PositionID = Positions.ID WHERE ((((Positions.DateClosed) Is Null)) AND (Positions.Symbol = '{0}'));", symbol));
                command2.Connection = conn;
                reader = command2.ExecuteReader();

                while (reader.Read())
                {
                    position.Purchases.Add(new Purchase()
                    {
                        ID = reader.GetInt32(0),
                        PositionID = reader.GetInt32(1),
                        NumberOfShares = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        R = reader.GetDecimal(4),
                        PurchaseDate = reader.GetDateTime(5)
                    });
                }

                OleDbCommand command3 = new OleDbCommand(string.Format("SELECT Sales.* FROM Sales Inner Join Positions on Sales.PositionID = Positions.ID WHERE ((((Positions.DateClosed) Is Null)) AND (Positions.Symbol = '{0}'));", symbol));
                command3.Connection = conn;
                reader = command3.ExecuteReader();

                while (reader.Read())
                {
                    position.Sales.Add(new Sale()
                    {
                        ID = reader.GetInt32(0),
                        SaleDate = reader.GetDateTime(1),
                        NumberOfShares = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        PositionID = reader.GetInt32(4)
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            //LogHelper.LogInfo("Exiting Repository.GetPositionBySymbol.");
            return position;
        }

        #endregion methods

        #endregion public

        #region private

        #region methods

        /// <summary>
        /// Gets an OLE DB connection. 
        /// </summary>
        /// <returns>OLE DB connection. </returns>
        private OleDbConnection GetOleDbConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["connstring"];
            conn.Open();

            return conn;
        }

        #endregion methods

        #region constants

        private const string SelectIdentity = "Select @@Identity";

        #endregion constants

        #endregion private











        
    }
}
