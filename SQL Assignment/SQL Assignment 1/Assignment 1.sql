-- Query 1
-- Write a query to get a Product list (id, name, unit price) where current products cost less than $20.
SELECT ProductID, ProductName, UnitPrice FROM dbo.Products WHERE UnitPrice < 25 

-- Query 2
-- Write a query to get Product list (id, name, unit price) where products cost between $15 and $25
SELECT ProductId, ProductName, UnitPrice FROM dbo.Products WHERE UnitPrice BETWEEN 15 AND 25

-- Query 3
-- Write a query to get Product list (name, unit price) of above average price. 
SELECT ProductName, UnitPrice FROM dbo.Products WHERE UnitPrice > (Select AVG(UnitPrice) from dbo.Products)

-- Query 4
-- Write a query to get Product list (name, unit price) of ten most expensive products
SELECT TOP 10 ProductName, UnitPrice FROM dbo.Products ORDER BY UnitPrice DESC

-- Query 5
-- Write a query to count current and discontinued products
SELECT Discontinued, COUNT(*) FROM dbo.Products GROUP BY Discontinued

-- Query 6
-- Write a query to get Product list (name, units on order , units in stock) of stock is less than the quantity on order
SELECT ProductName, UnitsInStock, UnitsOnOrder FROM dbo.Products WHERE UnitsInStock < UnitsOnOrder
