Create Database SasinShopDB
Go

Use SasinShopDB
Go

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
	CategoryID		Varchar(10),
	Constraint PK_Products Primary Key (ProductId) 
)

Create Table Categories
(
	CategoryId		Varchar(10),
	CategoryName	Nvarchar(255),
	Thumnail		Varchar(255),
	Published		Bit,
)