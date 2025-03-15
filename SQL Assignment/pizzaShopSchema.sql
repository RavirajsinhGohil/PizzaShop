create table Roles (
    RoleID SERIAL PRIMARY KEY,
    RoleName VARCHAR(50) UNIQUE NOT NULL,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

alter table Roles (
	add column FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	add column FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

create table MainNavbar(
	NavbarItemId SERIAL PRIMARY KEY,
	NavbarItemName INT NOT NULL,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

create table Permissions (
    PermissionID SERIAL PRIMARY KEY,
	PermissionName VARCHAR(50),
    RoleID INT NOT NULL,
    MenuId INT NOT NULL,
    CanView BIT DEFAULT '0',
    CanAddEdit BIT DEFAULT '0',
    CanDelete BIT DEFAULT '0',
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);

create table Users (
    UserID SERIAL PRIMARY KEY,
    UserName VARCHAR(50),
    FirstName VARCHAR(50),
	LastName VARCHAR(50),
	Email VARCHAR(50),
	Phone VARCHAR(50),
	Country VARCHAR(50),
	States VARCHAR(50),
	City VARCHAR(50),
	Address VARCHAR(500),
	ZipCode INT,
	Profile BYTEA,
	RoleID INT,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);

create table Customer (
    CustomerID SERIAL PRIMARY KEY,
    FirstName VARCHAR(50),
	LastName VARCHAR(50),
	Email VARCHAR(50),
	Phone VARCHAR(50),
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (CustomerID) REFERENCES Roles(RoleID) ON DELETE CASCADE
);

create table MenuCategory(
	MenuCategoryID SERIAL PRIMARY KEY,
	CategoryName VARCHAR(50),
	Description VARCHAR,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

create table Items(
	ItemID SERIAL PRIMARY KEY,
	ItemName VARCHAR(50),
	ItemType VARCHAR(50),
	Rate INT,
	Quantity INT,
	Available BIT Default '1',
	CategoryID INT,
	ItemImage BYTEA,
	isModifiable BIT Default '0',
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (ItemID) REFERENCES MenuCategory(MenuCategoryID) ON DELETE CASCADE
);

create table ModifierGroup(
	ModifierGroupID SERIAL PRIMARY KEY,
	ModifierName VARCHAR(50),
	Description VARCHAR,
	MenuCategoryID INT,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP NOT NULL,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (MenuCategoryID) REFERENCES MenuCategory(MenuCategoryID)
);
	
create table Orders (
    OrderID SERIAL PRIMARY KEY,
    CustomerID INT,
	TableID INT,
    Status INT NOT NULL DEFAULT 0 ,
    PaymentMode INT NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(10,2) NOT NULL,
    AdminComment VARCHAR(500),
    CustomerFeedback VARCHAR(500),
    AvgRating DECIMAL(5,2),
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID) ON DELETE SET NULL,
    FOREIGN KEY (TableID) REFERENCES Tables(TableID) ON DELETE SET NULL
);

create table OrderRatings
(
	OrderRatingID SERIAL PRIMARY KEY, 
	OrderID INT FK,
	RatingID INT,
	RatingOrder INT,
	Description VARCHAR(500),
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

create table OrderDetails (
    OrderDetailID SERIAL PRIMARY KEY,
    OrderID INT,
    ItemID INT,
    Quantity INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Description VARCHAR(500),
    Status INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID) ON DELETE CASCADE,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

create table OrderModifierDetails(
	OrderModifierDetailID SERIAL PRIMARY KEY,
    OrderDetailID INT,
    ItemID INT,
    Quantity INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID) ON DELETE CASCADE,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
)

create table UserFavouriteItem
(
	UserFavouriteItem PK,
	UserId INT,
	ItemId INT,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (UserId) REFERENCES Users(UserID),
	FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID) ON DELETE CASCADE
);

create table WaitingTicket
(
	WaitingTicketID SERIAL PRIMARY KEY,
	CustomerID INT,
	SectionID INT,
	People INT,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
	FOREIGN KEY (SectionID) REFERENCES Users(UserID)
);

create table Sections (
    SectionID SERIAL PRIMARY KEY,
    SectionName VARCHAR(50) NOT NULL,
    Description VARCHAR(500),
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

create table Tables (
    TableID SERIAL PRIMARY KEY,
    SectionID INT,
    TableName VARCHAR(50),
    Capacity INT NOT NULL,
    Status BIT DEFAULT '1',
    FOREIGN KEY (SectionID) REFERENCES Sections(SectionID) ON DELETE CASCADE,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID)
);

create table TableGrouping
(
	TableGroupingID SERIAL PRIMARY KEY,
	OrderId INT,
	TableID INT,
	isDeleted BIT DEFAULT '0',
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	CreatedBy INT,
	FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
	UpdatedAt TIMESTAMP,
	UpdatedBy INT,
	FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID),
	FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
	FOREIGN KEY (TableID) REFERENCES Tables(TableID)
);

create table TaxesAndFees (
    TaxID INT PRIMARY KEY AUTO_INCREMENT,
    TaxName VARCHAR(100) NOT NULL,
    TaxType INT NOT NULL  ('Fixed', 'Percentage'),
    TaxValue DECIMAL(10,2) NOT NULL,
    IsEnabled BIT DEFAULT 1,
    IsDefault BIT DEFAULT 0
);


create table OrderStatus
(
	OrderStatusChangeLogId INT PK,
	OrderId INT FK,
	Status INT,
	UpdatedAt DATTIME,
	UpdatedBy INT FK
)
