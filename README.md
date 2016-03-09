# StocksApp
The stocks app is a WinForms application using .NET 4.0. It uses the Repository pattern to communicate with data storage. To generate a SQL Server database, execute the script given at peterharnish/CashFlowApp.
The stocks app keeps track of Positions, Purchases, Sales, and Dividends. 
The stocks app has 2 modes, Current and History. The form opens to Current. It displays a data grid view with current positions, giving the Symbol, Current Price, High, Target Sale Price, Date Opened, Stop, Total Invested, Total Shares Owned, Total Dividends, and Total R. The final row displays totals.
To insert a Position, right-click on the data grid view and select Purchases. The Purchases dialog box opens up. Enter the Symbol, Total Price, Number of Shares, and Stop. Today's date is inserted automatically. Then click OK and the Purchase is saved. R, or Risk, is computed as Total Price * (1 - Stop). 
Hard stops are used. The target sale price is calculated when inserting a position and the formula is (Purchase Price - R) / Number of Shares
The Stocks App is meant to interoperate with the UpdateStockPrices web job given in peterharnish/HarnishBalanceSheet, which runs on Azure. This job queries Yahoo finance every day (meant to be configured to run after the market closes) and inserts the current price, along with the High if a new high is reached since opening the position, to the Finances database.
Dividend insertion is simple. To insert a Sale, right-click the data grid view, and enter the Symbol, Total Price, and Number of Shares. If this is to close the position, check the Close checkbox. Then click OK and the sale is saved.
Changing the Mode dropdown to History will select History mode. This mode shows all the positions that were closed between the Start Date and End Date given in the date pickers. It shows the Date Closed, Total Profit, and Profit over R for each position.

