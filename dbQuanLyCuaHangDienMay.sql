CREATE DATABASE dbQuanlycuahangdienmay
go
use dbQuanlycuahangdienmay
go
create table Tbl_Category
(
CategoryID int identity primary key,
CategoryName varchar(500) unique,
IsActive bit null,
IsDelete bit null
)

create table Tbl_Product
(
ProductID int identity primary key,
ProductName varchar(50) unique,
CategoryID int,
IsActive bit null,
IsDelete bit null,
CreateDate datetime null,
ModifiedDate datetime null,
Description datetime null,
ProductImage varchar(max),
IsFeature bit null,
Quantity int
foreign key (CategoryID) references Tbl_Category(CategoryID)
)

create table Tbl_CartStatus
(
CartStatusID int identity primary key,
CartStatus varchar(500)
)

create table Tbl_Members
(
MemberID int identity primary key,
MemberName varchar(200),
EmailID varchar(200) unique,
Password varchar(500),
IsActive bit null,
IsDelete bit null,
CreateOn datetime,
ModifiedOn datetime
)

create table Tbl_ShippingDetails
(
ShippingDetailID int identity primary key,
MemberID int,
Address varchar(500),
OrderID int,
AmountPaid decimal,
PaymentType varchar(50),
foreign key (MemberID) references Tbl_Members(MemberID)
)

create table Tbl_Roles
(
RoleID int identity primary key,
RoleName varchar(200) unique
)

create table Tbl_Cart
(
CartID int identity primary key,
ProductID int,
MemberID int,
CartStatusID int,
foreign key (ProductID) references Tbl_Product(ProductID)
)

create table Tbl_MemberRole
(
MemberRoleID int identity primary key,
MemberID int,
RoleID int
)

create table Tbl_SlideImage
(
SlideID int identity primary key,
SlideTitle varchar(500),
SlideImage varchar(max)
)

create table Tbl_Login
(
LoginID int identity primary key,
Username varchar(50),
Password varchar(50),
Role varchar(50),
)

create table Tbl_Invoice
(
InvoiceID int not null,
Date datetime,
CustomerName nvarchar(50),
Address varchar(50),
PhoneNum varchar(50),
Total decimal(18,0),
constraint pk_invoice primary key(InvoiceID)
)

create table Tbl_InvoiceDetail
(
InvoiceID int,
ProductID int,
Quantity int,
constraint pk_invoicedetail primary key(InvoiceID, ProductID)
)

ALTER TABLE Tbl_InvoiceDetail ADD CONSTRAINT fk01_Tbl_InvoiceDetail FOREIGN KEY(InvoiceID) REFERENCES Tbl_Invoice(InvoiceID)
ALTER TABLE Tbl_InvoiceDetail ADD CONSTRAINT fk02_Tbl_InvoiceDetail FOREIGN KEY(ProductID) REFERENCES Tbl_Product(ProductID)



insert into Tbl_Login values ('admin','admin','admin')
select * from Tbl_Login
drop table Tbl_Login