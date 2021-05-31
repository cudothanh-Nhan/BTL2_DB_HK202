create database db;
use db;
create table CUSTOMERS (
Firstname 			varchar(20) 	NOT NULL,
Last_name 			varchar(20) 	NOT NULL,
Customer_ID 		int 			NOT NULL,
Cus_Username 		varchar(16) 	NOT NULL,
Email 				varchar(25),
Street 				varchar(255)		NOT NULL,
City 				varchar(255)		NOT NULL,
Telephone_number 	char(10) 		NOT NULL,
PRIMARY KEY (Customer_ID)
);

create table ACCOUNTS (
Username 			varchar(16)		NOT NULL,
Role 				varchar(20) 	NOT NULL,
Password 			varchar(20),
PRIMARY KEY (Username)
);

create table REVIEWS (
ReCustomer_ID		int 			NOT NULL,
Date				Date 			NOT NULL,
Rating 				int				NOT NULL,
Image_URL			blob,
Comment_text 		text,
Review_Reply_Date 	Date,
Re_Pro_ID 			int 			NOT NULL,
PRIMARY KEY(Date,ReCustomer_ID)
);

create table ORDERS (
Payment_method 		varchar(20)		NOT NULL,
Note 				text,
Order_ID			int				NOT NULL,
Or_cus_ID			int				NOT NULL,
Ship_cash 			int 			NOT NULL,
PRIMARY KEY (Order_ID)
);

create table STATUS (
Status 				varchar(20)		NOT NULL,
Cancelled_Time		datetime,
Completed_Time		datetime,
Submission_Time 	datetime		NOT NULL,
Delivering_Time 	datetime		NOT NULL,
Sta_Order_ID 		int				NOT NULL,
PRIMARY KEY (Sta_Order_ID)
);

create table STORES (
Street				varchar(25) 	NOT NULL,
City				varchar(25)		NOT NULL,
CONSTRAINT PK_STORES PRIMARY KEY (Street,City)
);

create table STO_HAS_SHIP (
H_City 				varchar(255)		NOT NULL,
H_Street 			varchar(255) 	NOT NULL,
H_Ship_ID			int				NOT NULL,
PRIMARY KEY (H_Street,H_City,H_Ship_ID)
);

create table MAGAZINE_SERI_NAMES (
Seri_name_ID 		int 			NOT NULL,
Publisher			varchar(20)		NOT NULL,
Name				varchar(20)		NOT NULL,
PRIMARY KEY (Seri_name_ID)
);

create table SUBCRIBE (
S_Cus_ID 			int				NOT NULL,
Sub_seriname_ID		int				NOT NULL,
PRIMARY KEY (S_Cus_ID,Sub_seriname_ID)
);

create table AUTHORS (
Name 				varchar(20)		NOT NULL,
Author_ID			int				NOT NULL,
DOB					date			NOT NULL,
PRIMARY KEY (Author_ID)
);

create table WRITTEN_BY (
WBook_Pro_ID		int 			NOT NULL,
WAuthor_ID			int				NOT NULL,
PRIMARY KEY (WBook_Pro_ID,WAuthor_ID)
);

create table BOOKS (
Name				varchar(255)		NOT NULL,	
Publisher			varchar(20)		NOT NULL,
Publish_year		year,
Pages				int,	
Book_Pro_ID			int				NOT NULL,
PRIMARY KEY (Book_Pro_ID)
);

create table PRODUCTS (
Sto_Street			varchar(255)		NOT NULL,
Sto_City			varchar(255)		NOT NULL,
Language			varchar(255),
Price				int				NOT NULL,
imgUrl				varchar(255),
Quantity			int				NOT NULL,
Product_ID			int 			NOT NULL,
PRIMARY KEY (Product_ID)
);

create table P_PART_OF (
P_Order_ID 			int 			NOT NULL,
P_Product_ID		int 			NOT NULL,
Order_quantity 		int				NOT NULL,
PRIMARY KEY(P_Order_ID,P_Product_ID)
);

create table SHIPMENT(
BoundDistance		int 			NOT NULL,
Name				varchar(20)		NOT NULL,
Shipment_ID			int 			NOT NULL,
Price 				int 			NOT NULL,
PRIMARY KEY (Shipment_ID)		
);

create table MAGAZINES(
Publish_date 		date			NOT NULL,
NO					int 			NOT NULL,
Maga_Pro_ID			int				NOT NULL,
Magazine_ID			int				NOT NULL,
PRIMARY KEY (Maga_Pro_ID)
);

alter table books
modify Name varchar (10000), modify Publisher varchar(255);


-- Completed
DELIMITER $$
create procedure getBookUI()
begin
	select imgUrl, name, Product_ID, sum(Quantity), price, Publish_year
    from books, products
    where Book_Pro_ID = Product_ID
    group by Product_ID;
end$$
DELIMITER ;

-- Completed
DELIMITER $$
create procedure getBookDetail(Book_ID INT)
begin
	select imgUrl, name, price, sum(Quantity), Product_ID, Sto_Street, Sto_City, Language, Publisher, Publish_year, Pages
    from products as p, books as b
    where Product_ID = Book_Pro_ID and Book_Pro_ID = Book_ID
    group by Product_ID;
end$$
DELIMITER ;

DELIMITER $$
create procedure getMagazineUI()
begin
	select imgUrl, Name, NO, Product_ID, sum(Quantity), price, Publish_date
    from products, magazines, MAGAZINE_SERI_NAMES
    where Product_ID = Maga_Pro_ID and Magazine_ID = Seri_name_ID
    group by Product_ID;
end$$
DELIMITER ;

DELIMITER $$
create procedure getMagazineDetail(Maga_ID INT)
begin
	select imgUrl, Name, NO, Product_ID, sum(Quantity), price, Sto_Street, Sto_City, Language, Publisher, Publish_date
    from products, magazines, MAGAZINE_SERI_NAMES
    where Product_ID = Maga_Pro_ID and Magazine_ID = Seri_name_ID and Maga_Pro_ID = Maga_ID
    group by Product_ID;
end$$
DELIMITER ;


DELIMITER $$
create procedure getAllProID()
begin
	select Product_ID
    from products;
end$$
DELIMITER ;

DELIMITER $$
create procedure getAllBookID()
begin
	select Book_Pro_ID
    from books;
end$$
DELIMITER ;

DELIMITER $$
create procedure getAllMagazineID()
begin
	select Maga_Pro_ID
    from magazines;
end$$
DELIMITER ;

DELIMITER $$
create procedure getAllMagazineSeriID()
begin
	select Seri_name_ID
    from magazine_seri_names;
end$$
DELIMITER ;

DELIMITER $$
create procedure removeBook(Book_ID int)
begin
	DELETE FROM products
	WHERE Product_ID = Book_ID;
	DELETE FROM books
	WHERE Book_Pro_ID = Book_ID;
end$$
DELIMITER ;

DELIMITER $$
create procedure removeMagazine(Maga_ID int)
begin
	DELETE FROM products
	WHERE Product_ID = Maga_ID;
	DELETE FROM magazines
	WHERE Maga_Pro_ID = Maga_ID;
end$$
DELIMITER ;


call getMagazineUI();
call getBookUI();
    