CREATE TABLE Customers (
    CustomerID SERIAL PRIMARY KEY,
    CustomerName VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Country VARCHAR(100),
    SignupDate DATE
);

INSERT INTO Customers (CustomerName, Email, Country, SignupDate) VALUES
('Alice Johnson', 'mailto:alice@example.com', 'USA', '2025-01-15'),
('Bob Smith', 'mailto:bob@example.com', 'Canada', '2025-01-20'),
('Charlie Brown', 'mailto:charlie@example.com', 'UK', '2025-02-10'),
('David White', 'mailto:david@example.com', 'Australia', '2025-02-05'),
('Emma Watson', 'mailto:emma@example.com', 'Germany', '2025-01-25');

CREATE TABLE Products (
    ProductID SERIAL PRIMARY KEY ,
    ProductName VARCHAR(255) NOT NULL,
    Category VARCHAR(100),
    Price DECIMAL(10,2),
    StockQuantity INT
);

INSERT INTO Products (ProductName, Category, Price, StockQuantity) VALUES
('Laptop', 'Electronics', 1000.00, 50),
('Smartphone', 'Electronics', 700.00, 100),
('Washing Machine', 'Electronics', 750.00, 100),
('T-shirt', 'Clothing', 25.00, 200),
('Headphones', 'Electronics', 150.00, 80),
('Shoes', 'Footwear', 60.00, 150);

CREATE TABLE Orders (
    OrderID SERIAL PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10,2),
    Status INT,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
	FOREIGN KEY (Status) REFERENCES Orders_Master(StatusID)
);

CREATE TABLE Orders_Master (
	StatusID SERIAL PRIMARY KEY,
	StatusName VARCHAR(50)
	)

INSERT INTO Orders_Master (StatusID, StatusName) VALUES
(1, 'Pending'),
(2, 'Shipped'),
(3, 'Delivered'),
(4, 'Cancelled')
	
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, Status) VALUES
	(3, '2025-02-11', 50.00, 3),
(1, '2025-02-15', 1200.00, 3),
(2, '2025-01-20', 750.00, 2),
(3, '2025-02-10', 50.00, 1),
(4, '2025-02-05', 150.00, 3),
(5, '2025-01-25', 60.00, 4);

CREATE TABLE OrderDetails (
    OrderDetailID SERIAL PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    Price DECIMAL(10,2),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price) VALUES
(1, 1, 1, 1000.00),
(1, 4, 2, 100.00),
(2, 2, 1, 700.00),
(3, 3, 2, 25.00),
(4, 4, 1, 150.00),
(5, 5, 1, 60.00);

CREATE TABLE Payments (
    PaymentID SERIAL PRIMARY KEY,
    OrderID INT,
    PaymentDate DATE,
    PaymentMethod INT,
    Amount DECIMAL(10,2),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

CREATE TABLE Payment_Method_Master (
	Payment_Method_MasterID SERIAL PRIMARY KEY,
	Payment_Method_Name VARCHAR(50)
);

INSERT INTO Payment_Method_Master (Payment_Method_MasterID, Payment_Method_Name) VALUES
	(1, 'Credit Card'),
	(2, 'PayPal'),
	(3, 'Bank Transfer')

INSERT INTO Payments (OrderID, PaymentDate, PaymentMethod, Amount) VALUES
(1, '2023-06-02', 1, 1200.00),
(2, '2023-06-06', 2, 750.00),
(4, '2023-06-16', 3, 150.00);

SELECT * FROM Customers;
SELECT * FROM Products;
SELECT * FROM Orders;
SELECT * FROM OrderDetails;
SELECT * FROM Payments;
