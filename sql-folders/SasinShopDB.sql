Create Database SasinShopDB
Go

Use SasinShopDB
Go

--------------------------------------------- CREATE TABLE ---------------------------------------------
--------------- CATEGORIES TABLE
Create Table Categories
(
	CategoryId		Varchar(10),
	CategoryName	Nvarchar(255),
	Thumnail		Varchar(255),
	Published		Bit,
	Description		Nvarchar(MAX),
	Constraint PK_Categories Primary Key (CategoryID)
)

--------------- PRODUCTS TABLE
Create Table Products
(
	ProductId		Varchar(10),
	ProductName		Nvarchar(255),
	UnitPrice		Decimal(18, 0),
	Discount		Int,
	Description		Nvarchar(MAX),
	Active			Bit,
	BestSellers		Bit,
	HomeFlag		Bit,
	DateCreated		Datetime,
	DateModified	Datetime,
	UnitsInStock	Int,
	CategoryId		Varchar(10),
	Constraint PK_Products Primary Key (ProductId) 
)

--------------- ACCOUNTS TABLE
Create Table Accounts
(
	AccountId	Varchar(10),
	LoginName	Varchar(255),
	Password	Varchar(50),
	LastLogin	Datetime,
	DateCreated	Datetime,	
	Salt		Nchar(10),
	Active		Bit,
	Constraint PK_Accounts Primary Key (AccountId)
)

--------------- CUSTOMERS TABLE
Create Table Customers
(
	CustomerId	Varchar(10),
	LastName	Nvarchar(255),
	FirstName	Nvarchar(255),
	Gender		Bit,
	Birthday	Date,
	Email		Varchar(255),
	Password	Varchar(50),
	PhoneNumber	Varchar(10),
	Street		Nvarchar(255),
	District	Nvarchar(255),
	Ward		Nvarchar(255),
	City		Nvarchar(255),
	Salt		Nchar(10),
	Active		Bit,
	DateCreated	Datetime,
	LastLogin	Datetime,
	Constraint PK_Customers Primary Key (CustomerId)
)

--------------- TRANSACTSTATUS TABLE
Create Table TransactStatus
(
	TransactStatusId	Int Not Null Identity(1, 1),
	Status				Nvarchar(50),
	Description			Nvarchar(MAX),
	Constraint PK_TransactStatus Primary Key(TransactStatusId)
);

--------------- ORDERS TABLE
Create Table Orders
(
	OrderId				Varchar(20),
	CustomerId			Varchar(10),
	OrderDate			Datetime,
	ShipDate			Datetime,
	TransactStatusId	Int,
	Deleted				Bit,
	Paid				Bit,
	PaymentDate			Datetime,
	Note				Nvarchar(MAX),	
	Constraint PK_Orders Primary Key(OrderId)
);

--------------- ORDERDETAILS TABLE
Create Table OrderDetails
(
	OrderDetailId		Varchar(20),
	OrderId				Varchar(20),
	ProductId			Varchar(10),
	OrderNumber			Int,
	Quantity			Int,
	CodeId				Int,
	Total				Decimal(18, 0),
	ShipDate			Datetime,
	Constraint PK_OrderDetails Primary Key(OrderDetailId)
);

--------------- CODES TABLE
Create Table Codes
(
	CodeId		Int,
	CodeName	Varchar(255),
	Description	Nvarchar(MAX),
	Constraint PK_Codes Primary Key(CodeId)
)

--------------- POSTS TABLE
Create Table Posts
(
	PostId			Int Not Null Identity(1, 1),
	Title			Nvarchar(255),
	ShortContents	Nvarchar(255),
	Contents		Nvarchar(255),
	Thumbnail		Varchar(255),
	Published		Bit,
	CreatedDate		Datetime,
	Author			Nvarchar(255),
	Tags			Nvarchar(MAX),
	IsHot			Bit,
	IsNewFeed		Bit,
	Views			Int,
	Constraint PK_Posts Primary Key(PostId)
);

--------------- PAGES TABLE
Create Table Pages
(
	PageId		Int Not Null Identity(1, 1),
	PageName	Nvarchar(255),
	Contents	Nvarchar(MAX),
	Thumbnail	Varchar(255),
	Published	Bit,
	Title		Nvarchar(255),
	Alias		Nvarchar(255),
	CreatedAt	DateTime,
	Ordering	Int,
	Constraint PK_Pages Primary Key(PageId)
);

--------------------------------------- CREATE CONSTRAINTS FOR DATA TABLES ---------------------------------------
--------------- CREATE A FOREIGN KEY FOR THE PRODUCTS TABLE
Alter Table Products
Add Constraint FK_Products_Categories Foreign Key(CategoryId) References Categories(CategoryId)

--------------- CREATE A FOREIGN KEY FOR THE ORDERS TABLE
Alter Table Orders
Add Constraint FK_Orders_TransactStatus Foreign Key(TransactStatusId) References TransactStatus(TransactStatusId),
	Constraint FK_Orders_Customers Foreign Key(CustomerId) References Customers(CustomerId)

--------------- CREATE A FOREIGN KEY FOR THE ORDERDETAILS TABLE
Alter Table OrderDetails
Add Constraint FK_OrderDetails_Orders Foreign Key(OrderId) References Orders(OrderId),
	Constraint FK_OrderDetails_Products Foreign Key(ProductId) References Products(ProductId),
	Constraint FK_OrderDetails_Codes Foreign Key(CodeId) References Codes(CodeId)