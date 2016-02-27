using Stocks.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Stocks.Entity;

namespace Stocks.Tests
{
    
    
    /// <summary>
    ///This is a test class for SqlRepositoryTest and is intended
    ///to contain all SqlRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SqlRepositoryTest
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
            SqlRepository target = new SqlRepository();
            Position position = new Position() { Symbol = "ABC", TrailingStop = 0.2m, TargetSalePrice = 8m };
            int expected = 1;
            int actual;
            actual = target.InsertPosition(position);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateCurrent
        ///</summary>
        [TestMethod()]
        public void UpdateCurrentTest()
        {
            SqlRepository target = new SqlRepository();
            Position position = new Position() { ID = 3, CurrentPrice = 10, High = 10 };
            target.UpdateCurrent(position);
        }
    }
}
