-- Query 1
-- write a SQL query to find the salesperson and customer who reside in the same city. Return Salesman, cust_name and city
SELECT S.Name,  C.cust_name, S.city FROM Salesman S 
JOIN Customer  C ON S.city=C.city;

-- Query 2
-- write a SQL query to find those orders where the order amount exists between 500 and 2000. Return ord_no, purch_amt, cust_name, city
SELECT O.ord_no, O.Purch_Amt, C.cust_name, C.city FROM Orders O  
JOIN Customer C ON O.Purch_Amt BETWEEN 500 AND 2000 WHERE O.Customer_ID = C.Customer_ID;

-- Query 3
-- write a SQL query to find the salesperson(s) and the customer(s) he represents. Return Customer Name, city, Salesman, commission
SELECT S.name, C.cust_name, S.city, S.commission FROM Salesman S 
JOIN Customer C ON S.Salesman_ID = C.Salesman_ID;

-- Query 4
-- write a SQL query to find salespeople who received commissions of more than 12 percent from the company. Return Customer Name, customer city, Salesman, commission.
SELECT C.cust_name, C.city, S.name, S.Commission FROM Customer C 
JOIN Salesman S ON S.Salesman_ID = C.Salesman_ID WHERE S.Commission > 12 AND C.Salesman_ID = S.Salesman_ID;

-- Query 5
-- write a SQL query to locate those salespeople who do not live in the same city where their customers live and have received a commission of more than 12% from the company. Return Customer Name, customer city, Salesman, salesman city, commission
SELECT C.cust_name, C.city, S.name, S.City, S.Commission FROM Customer C 
JOIN Salesman S ON S.Salesman_ID = C.Salesman_ID  WHERE S.Commission > 12 AND C.City != S.City;

-- Query 6
-- write a SQL query to find the details of an order. Return ord_no, ord_date, purch_amt, Customer Name, grade, Salesman, commission
SELECT O.Ord_No, O.Ord_Date, O.Purch_Amt, C.Cust_Name, C.Grade, S.Name, S.Commission  FROM Orders O 
JOIN Customer C ON O.Customer_ID = C.Customer_ID INNER Join Salesman S ON O.Salesman_ID = S.Salesman_ID;

-- Query 7
-- Write a SQL statement to join the tables salesman, customer and orders so that the same column of each table appears once and only the relational rows are returned. 
SELECT S.Salesman_ID, S.Name, S.City, S.Commission, C.Customer_ID, C.Cust_Name, C.Grade, O.Ord_No, O.Purch_Amt, O.Ord_Date FROM Salesman S  
Join Customer C ON C.Salesman_ID =  S.Salesman_ID JOIN Orders O On O.Salesman_ID = S.Salesman_ID;

-- Query 8
-- write a SQL query to display the customer name, customer city, grade, salesman, salesman city. The results should be sorted by ascending customer_id.
SELECT C.Cust_Name, C.City, C.Grade, S.Name, S.City FROM Customer C 
JOIN Salesman S ON C.Salesman_ID = S.Salesman_ID ORDER BY C.Customer_ID;

-- Query 9
-- write a SQL query to find those customers with a grade less than 300. Return cust_name, customer city, grade, Salesman, salesmancity. The result should be ordered by ascending customer_id. 
SELECT C.Cust_Name, C.City, C.Grade, S.Name,S.City  FROM Customer C 
JOIN Salesman S ON C.Salesman_ID = S.Salesman_ID  WHERE C.Grade <300 ORDER BY C.Customer_ID;

-- Query 10
-- Write a SQL statement to make a report with customer name, city, order number, order date, and order amount in ascending order according to the order date to determine whether any of the existing customers have placed an order or not
SELECT C.Cust_Name, C.City, O.Ord_No, O.Ord_Date, O.Purch_Amt AS Order_Amount FROM Customer C 
JOIN Orders O On C.Customer_ID = O.Customer_ID ORDER BY O.Ord_Date;

-- Query 11
-- Write a SQL statement to generate a report with customer name, city, order number, order date, order amount, salesperson name, and commission to determine if any of the existing customers have not placed orders or if they have placed orders through their salesman or by themselves
SELECT C.Cust_Name, C.City, O.Ord_No, O.Ord_Date, O.Purch_Amt AS Order_Amount,S.Name, S.Commission FROM Customer C 
JOIN Orders O On C.Customer_ID = O.Customer_ID 
JOIN Salesman S ON S.Salesman_ID = C.Salesman_ID;

-- Query 12
-- Write a SQL statement to generate a list in ascending order of salespersons who work either for one or more customers or have not yet joined any of the customers
SELECT S.Salesman_ID, S.Name, S.City, S.Commission FROM Salesman S 
JOIN Customer C ON S.Salesman_ID = C.Salesman_ID ORDER BY S.Name;

-- Query 13
-- write a SQL query to list all salespersons along with customer name, city, grade, order number, date, and amount.
SELECT S.Name, C.Cust_Name, C.City, C.Grade, O.Ord_No, O.Ord_Date, O.Purch_Amt FROM Salesman S 
JOIN Customer C ON S.Salesman_ID = C.Salesman_ID
JOIN Orders O ON C.Customer_ID = O.customer_id;

-- Query 14
-- Write a SQL statement to make a list for the salesmen who either work for one or more customers or yet to join any of the customers. The customer may have placed, either one or more orders on or above order amount 2000 and must have a grade, or he may not have placed any order to the associated supplier.
SELECT S.Name, C.Cust_Name, C.Grade, O.Purch_Amt FROM Salesman S 
JOIN Customer C ON S.Salesman_ID = C.Salesman_ID
JOIN Orders O ON O.customer_id = C.Customer_ID WHERE O.Purch_Amt >= 2000 OR C.Grade IS NOT NULL;


-- Query 15
-- Write a SQL statement to generate a list of all the salesmen who either work for one or more customers or have yet to join any of them. The customer may have placed one or more orders at or above order amount 2000, and must have a grade, or he may not have placed any orders to the associated supplier.
SELECT S.Name, C.Cust_Name, C.Grade, O.Purch_Amt FROM Salesman S 
JOIN Customer C ON S.Salesman_ID = C.Salesman_ID
JOIN Orders O ON O.customer_id = C.Customer_ID WHERE O.Purch_Amt >= 2000 OR C.Grade IS NOT NULL;

-- Query 16
-- Write a SQL statement to generate a report with the customer name, city, order no. order date, purchase amount for only those customers on the list who must have a grade and placed one or more orders or which order(s) have been placed by the customer who neither is on the list nor has a grade.
SELECT C.Cust_Name, C.City, O.Ord_No, O.Ord_Date, O.Purch_Amt FROM Customer C 
JOIN Orders O ON O.Customer_ID = C.Customer_ID
WHERE C.Grade IS NOT NULL;

-- Query 17
-- Write a SQL query to combine each row of the salesman table with each row of the customer table
SELECT * FROM Salesman S 
JOIN Customer C ON S.Salesman_ID = C.Salesman_ID;

-- Query 18
-- Write a SQL statement to create a Cartesian product between salesperson and customer, i.e. each salesperson will appear for all customers and vice versa for that salesperson who belongs to that city
SELECT S.Name AS Salesman_Name, S.City AS Salesman_City, C.Cust_Name AS Customer_Name, C.City AS Customer_City FROM Salesman S 
CROSS JOIN Customer C WHERE S.City = C.City;

-- Query 19
-- Write a SQL statement to create a Cartesian product between salesperson and customer, i.e. each salesperson will appear for every customer and vice versa for those salesmen who belong to a city and customers who require a grade
SELECT S.Name AS Salesman_Name, S.City AS Salesman_City, C.Cust_Name AS Customer_Name, C.City AS Customer_City, C.Grade FROM Salesman S 
CROSS JOIN Customer C WHERE S.City IS NOT NULL AND C.Grade IS NOT NULL;

-- Query 20
-- Write a SQL statement to make a Cartesian product between salesman and customer i.e. each salesman will appear for all customers and vice versa for those salesmen who must belong to a city which is not the same as his customer and the customers should have their own grade
SELECT S.Name AS Salesman_Name, S.City AS Salesman_City, C.Cust_Name AS Customer_Name, C.City AS Customer_City, C.Grade FROM Customer C 
CROSS JOIN  Salesman S WHERE C.City != S.City AND C.Grade IS NOT NULL;