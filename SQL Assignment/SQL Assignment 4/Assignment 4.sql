-- Query 1
-- Create a stored procedure in the Northwind database that will calculate the average value of Freight for a specified customer.Then, a business rule will be added that will be triggered before every Update and Insert command in the Orders controller,and will use the stored procedure to verify that the Freight does not exceed the average freight. If it does, a message will be displayed and the command will be cancelled.
CREATE PROCEDURE spAVGFreight_T119
@CustomerID NVARCHAR(5),
@Avg_Freight MONEY OUTPUT
AS
BEGIN
    SELECT @Avg_Freight = AVG(Freight) FROM DemoOrders
    WHERE CustomerID = @CustomerID
END
GO
ALTER TRIGGER checkFreightAverage_T119 ON DemoOrders
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @CustomerID NVARCHAR(5), @New_Freight MONEY, @Avg_Freight MONEY = 0
    SELECT @CustomerID = I.CustomerID, @New_Freight = I.Freight FROM inserted I
    LEFT JOIN deleted D ON D.OrderID = I.OrderID

    EXEC spAVGFreight_T119 @CustomerID, @Avg_Freight

    IF @New_Freight > @Avg_Freight
    BEGIN 
        RAISERROR('Freight Limit is exceeded Average value.',10,1) 
        ROLLBACK TRANSACTION
    END

END

ALTER TABLE DemoOrders DISABLE TRIGGER checkFreightAverage_T119

DECLARE @Avg_freight MONEY
EXEC spAVGFreight_T119 @CustomerID = 'VINET', @Avg_Freight = @Avg_freight OUTPUT
PRINT @Avg_Freight

-- Query 2
-- write a SQL query to Create Stored procedure in the Northwind database to retrieve Employee Sales by Country
spGetEmployeeSalesByCountry_T119

GO
ALTER PROCEDURE spGetEmployeeSalesByCountry_T119
AS
BEGIN
    SELECT DE.FirstName, DE.Country, SUM((DOD.UnitPrice * DOD.Quantity) - (DOD.UnitPrice * DOD.Quantity * DOD.Discount)) AS TotalSales 
    FROM DemoEmployees DE 
    left join DemoOrders DO ON  DO.EmployeeID = DE.EmployeeID
    LEFT JOIN DemoOrderDetails DOD ON DOD.OrderID = DO.OrderID
    GROUP BY DE.Country, DE.FirstName
END



-- Query 3
-- write a SQL query to Create Stored procedure in the Northwind database to retrieve Sales by Year
exec spGetSalesByYear_T119

GO
ALTER PROCEDURE spGetSalesByYear_T119
AS
BEGIN
    SELECT YEAR(DO.ShippedDate) AS YearOfSales, SUM((DOD.UnitPrice * DOD.Quantity) - (DOD.UnitPrice * DOD.Quantity * DOD.Discount)) AS TotalSales 
    FROM DemoOrders DO
    LEFT JOIN DemoOrderDetails DOD ON DOD.OrderID = DO.OrderID
    GROUP BY YEAR(DO.ShippedDate)
END


-- Query 4
-- write a SQL query to Create Stored procedure in the Northwind database to retrieve Sales By Category
exec spGetSalesByCategory_T119

GO
CREATE PROCEDURE spGetSalesByCategory_T119
AS
BEGIN
    SELECT DC.CategoryName ,SUM((DOD.UnitPrice * DOD.Quantity) - (DOD.UnitPrice * DOD.Quantity * DOD.Discount)) FROM DemoCategories DC 
    LEFT JOIN DemoProducts DP ON DP.CategoryID = DC.CategoryID
    LEFT JOIN DemoOrderDetails DOD ON DOD.ProductID = DP.ProductID
    GROUP BY DC.CategoryName
END



-- Query 5
-- write a SQL query to Create Stored procedure in the Northwind database to retrieve Ten Most Expensive Products
exec spGetTop10ExpensiveProducts_T119

GO
ALTER PROCEDURE spGetTop10ExpensiveProducts_T119
AS
BEGIN
    SELECT TOP 10 ProductName, UnitPrice FROM DemoProducts
    ORDER BY UnitPrice DESC
END



-- Query 6
-- write a SQL query to Create Stored procedure in the Northwind database to insert Customer Order Details 
EXEC spInsertCustomerOrderDetails_T119 10, 10, 10, 10, 10

GO
CREATE PROCEDURE spInsertCustomerOrderDetails_T119
@OrderID INT,
@ProductID INT,
@UnitPrice INT,
@Quantity INT,
@Discount INT
AS
BEGIN
    INSERT INTO DemoOrderDetails 
    VALUES(@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount);
END


-- Query 7
-- write a SQL query to Create Stored procedure in the Northwind database to update Customer Order Details
EXEC spUpdateCustomerOrderDetails_T119 10, 15, 12, 9

GO
CREATE PROCEDURE spUpdateCustomerOrderDetails_T119
@OrderID INT,
@UnitPrice INT,
@Quantity INT,
@Discount INT
AS
BEGIN
    UPDATE DemoOrderDetails 
    SET UnitPrice = @UnitPrice, Quantity = @Quantity, Discount = @Discount
    WHERE OrderID = @OrderID
END
