using Stocks.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Stocks.Entity;
using System.Collections.Generic;

namespace Stocks.Tests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for InsertPosition
        ///</summary>
        [TestMethod()]
        public void InsertPositionTest()
        {
            Repository target = new Repository();
            Position position = new Position()
            {
                Symbol = "PCL",
                DateOpened = DateTime.Now,
                CurrentPrice = 39.35m,
                High = 39.55m,
                TrailingStop = 0.25m
            };

            int expected = 0; 
            int actual;
            actual = target.InsertPosition(position);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for InsertPurchase
        ///</summary>
        [TestMethod()]
        public void InsertPurchaseTest()
        {
            Repository target = new Repository();
            Purchase purchase = new Purchase()
            {
                PositionID = 5,
                NumberOfShares = 45,
                TotalPrice = 1779.7m,
                R = 45m * 39.35m * 0.25m + 8.95m,
                PurchaseDate = DateTime.Now
            };
            int expected = 0; 
            int actual;
            actual = target.InsertPurchase(purchase);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for InsertSale
        ///</summary>
        [TestMethod()]
        public void InsertSaleTest()
        {
            Repository target = new Repository();
            Sale sale = new Sale()
            {
                SaleDate = DateTime.Now,
                NumberOfShares = 45,
                TotalPrice = 1770.75m,
                PositionID = 5
            };
            int expected = 0; 
            int actual;
            actual = target.InsertSale(sale);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for InsertDividend
        ///</summary>
        [TestMethod()]
        public void InsertDividendTest()
        {
            Repository target = new Repository();
            Dividend dividend = new Dividend()
            {
                PositionID = 5,
                PaymentDate = DateTime.Now,
                Amount = 10m
            };
            int expected = 0; 
            int actual;
            actual = target.InsertDividend(dividend);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetCurrentPositions
        ///</summary>
        [TestMethod()]
        public void GetCurrentPositionsTest()
        {
            Repository target = new Repository(); 
            List<Position> actual;
            actual = target.GetCurrentPositions();
            Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetHistoryPositions
        ///</summary>
        [TestMethod()]
        public void GetHistoryPositionsTest()
        {
            Repository target = new Repository();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();         
            List<Position> actual;
            actual = target.GetHistoryPositions(startDate, endDate);
            Assert.AreNotEqual(0, actual.Count);           
        }

        /// <summary>
        ///A test for ClosePosition
        ///</summary>
        [TestMethod()]
        public void ClosePositionTest()
        {
            Repository target = new Repository();
            Position position = new Position()
            {
                ID = 5,
                TotalProfit = 1000m
            };
            target.ClosePosition(position);
        }

        /// <summary>
        ///A test for UpdateTrailingStop
        ///</summary>
        [TestMethod()]
        public void UpdateTrailingStopTest()
        {
            Repository target = new Repository();
            Position position = new Position()
            {
                ID = 5,
                TrailingStop = 0.1m
            };
            target.UpdateTrailingStop(position);
        }

        /// <summary>
        ///A test for UpdateHigh
        ///</summary>
        [TestMethod()]
        public void UpdateCurrentTest()
        {
            Repository target = new Repository();
            Position position = new Position()
            {
                ID = 5,
                High = 39.75m,
                CurrentPrice = 39.55m
            };
            target.UpdateCurrent(position);
        }

        /// <summary>
        ///A test for GetPositionID
        ///</summary>
        [TestMethod()]
        public void GetPositionIDTest()
        {
            Repository target = new Repository(); 
            string symbol = "PCL"; 
            int expected = 5; 
            int actual;
            actual = target.GetPositionID(symbol);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPositionBySymbol
        ///</summary>
        [TestMethod()]
        public void GetPositionBySymbolTest()
        {
            Repository target = new Repository();
            string symbol = "PCL";
            Position actual;
            actual = target.GetPositionBySymbol(symbol);
            Assert.IsNotNull(actual);            
        }
    }
}
