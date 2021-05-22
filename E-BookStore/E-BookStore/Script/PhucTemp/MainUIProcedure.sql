create database db;
use db;
create table CUSTOMERS (
Firstname 			varchar(20) 	NOT NULL,
Last_name 			varchar(20) 	NOT NULL,
Customer_ID 		int 			NOT NULL,
Cus_Username 		varchar(16) 	NOT NULL,
Email 				varchar(25),
Street 				varchar(25)		NOT NULL,
City 				varchar(25)		NOT NULL,
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
Image_URL			varchar(25),
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
H_City 				varchar(25)		NOT NULL,
H_Street 			varchar(25) 	NOT NULL,
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
Name				varchar(20)		NOT NULL,	
Publisher			varchar(20)		NOT NULL,
Publish_year		year,
Pages				int,	
Book_Pro_ID			int				NOT NULL,
PRIMARY KEY (Book_Pro_ID)
);

create table PRODUCTS (
Sto_Street			varchar(25)		NOT NULL,
Sto_City			varchar(25)		NOT NULL,
Language			varchar(15),
Price				int				NOT NULL,
Quantity			int				NOT NULL,
Product_ID			int 			NOT NULL,
imgUrl				varchar(255),
PRIMARY KEY (Product_ID,Sto_Street,Sto_City)
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


insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("1", "2", "3", 4, 5, 6, "afafa");
insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("7", "8", "9", 10, 11, 12, "Adfafa");
insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("13", "14", "15", 16, 17, 18, "Hako.re");

insert into BOOKS(name, publisher, publish_year, pages, book_Pro_ID) values ("Sach", "Giao", 2000, 6996, 6);
insert into BOOKS(name, publisher, publish_year, pages, book_Pro_ID) values ("Giao", "Sach", 1999, 4196, 18);


insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("maga1", "2", "3", 4, 5, 12, "Hako.re");
insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("maga2", "2", "3", 4, 12, 13, "Hako.re");
insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("maga2", "3", "3", 4, 34, 13, "Hako.re");

insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("maga2", "8", "9", 10, 11, 14, "Hako.re");
insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("maga322", "8", "9", 10, 13, 14, "Hako.re");
insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values("maga32", "8", "9", 10, 14, 14, "Hako.re");

insert into BOOKS(name, publisher, publish_year, pages, book_Pro_ID) values ("Sach", "Giao", 2000, 6996, 14);


insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID) values("maga3", "14", "15", 16, 17, 15);

insert into MAGAZINE_SERI_NAMES(Seri_name_ID, Publisher, Name) values (1,"Kim Dong","Tuoi Gia");
insert into MAGAZINE_SERI_NAMES(Seri_name_ID, Publisher, Name) values (2,"Dong Kim","Tuoi Tre");

insert into magazines(Publish_date, NO, Maga_Pro_ID, Magazine_ID) values (STR_TO_DATE("30/12/1988", "%d/%m/%Y"), 128, 6, 1);
insert into magazines(Publish_date, NO, Maga_Pro_ID, Magazine_ID) values (STR_TO_DATE("30/12/1981", "%d/%m/%Y"), 1, 12, 1);
insert into magazines(Publish_date, NO, Maga_Pro_ID, Magazine_ID) values (STR_TO_DATE("30/12/1912", "%d/%m/%Y"), 15, 18, 2);


insert into reviews(ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID) values (1, STR_TO_DATE("30/05/1981", "%d/%m/%Y"), 5, "a", 'test', STR_TO_DATE("30/12/1981", "%d/%m/%Y"), 12);
insert into reviews(ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID) values (1, STR_TO_DATE("30/01/1981", "%d/%m/%Y"), 5, "a", 'test', STR_TO_DATE("30/11/1981", "%d/%m/%Y"), 12);
insert into reviews(ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID) values (1, STR_TO_DATE("30/12/1980", "%d/%m/%Y"), 5, "a", 'test', STR_TO_DATE("30/01/1981", "%d/%m/%Y"), 12);

insert into reviews(ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID) values (2, STR_TO_DATE("30/11/1981", "%d/%m/%Y"), 5, "a", 'test', STR_TO_DATE("30/12/1981", "%d/%m/%Y"), 12);
insert into reviews(ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID) values (1, STR_TO_DATE("30/10/1981", "%d/%m/%Y"), 5, "a", 'test', STR_TO_DATE("30/12/1981", "%d/%m/%Y"),14);
insert into reviews(ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID) values (1, STR_TO_DATE("30/9/1981", "%d/%m/%Y"), 5, "a", 'test', STR_TO_DATE("30/12/1981", "%d/%m/%Y"), 13);

insert into customers(Firstname, Last_name, Customer_ID, Cus_Username, Email, Street, City, Telephone_number) values ("Rem", "So Kawaii", 1, "Cute", "Kurumi", "Tokisaki", "Touka Kirishima", "10");
insert into customers(Firstname, Last_name, Customer_ID, Cus_Username, Email, Street, City, Telephone_number) values ("Alice", "Synthesis", 2, "30", "Xiao", "Ganyu", "Yuuki Asuna", "01");
insert into customers(Firstname, Last_name, Customer_ID, Cus_Username, Email, Street, City, Telephone_number) values ("Kei", "Karuizawa", 3, "kawaii", "Gaeun", "A", "Yuuki", "0112");


insert into authors(Name, Author_ID, DOB) values ("Cu Do Thanh Nhan", 1, STR_TO_DATE("30/9/1981", "%d/%m/%Y"));
insert into authors(Name, Author_ID, DOB) values ("Cu Do Thanh Nhan1", 2, STR_TO_DATE("30/9/1981", "%d/%m/%Y"));
insert into authors(Name, Author_ID, DOB) values ("Cu Do Thanh Nhan2", 3, STR_TO_DATE("30/9/1981", "%d/%m/%Y"));

insert into written_by(WBook_Pro_ID, WAuthor_ID) values(6, 2);
insert into written_by(WBook_Pro_ID, WAuthor_ID) values(6, 1);
insert into written_by(WBook_Pro_ID, WAuthor_ID) values(6, 3);
insert into written_by(WBook_Pro_ID, WAuthor_ID) values(18, 2);

-- Completed
DELIMITER $$
create procedure getBookUI()
begin
	select imgUrl, name, Product_ID, sum(Quantity), price
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
	select imgUrl, Name, NO, Product_ID, sum(Quantity), price
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
create procedure getReview(Pro_ID INT)
begin
	select ReCustomer_ID, Date, Rating, Image_URL, Comment_text, Review_Reply_Date, Re_Pro_ID, Firstname, Last_name, Customer_ID
    from reviews, customers
    where Re_Pro_ID = Pro_ID and Customer_ID = ReCustomer_ID;
end$$
DELIMITER ;

DELIMITER $$
create procedure getCustomerReview(cus_ID INT)
begin
	select Firstname, Last_name, Customer_ID
    from customers
    where Customer_ID = cus_ID;
end$$
DELIMITER ;

DELIMITER $$
create procedure getAuthor(Book_ID INT)
begin
	select authors.Name, Author_ID, DOB
    from authors, written_by, books
    where WAuthor_ID = Author_ID and WBook_Pro_ID = Book_Pro_ID and Book_Pro_ID = Book_ID;
end$$
DELIMITER ;


-- need check
DELIMITER $$
create procedure getSubribeOfID(input_ID INT)
begin
	select imgUrl, Name, NO, price, Quantity, Magazine_ID, Product_ID, Sto_Street, Sto_City, Language
    from customers, subcribe, magazine_seri_names, magazines
    where Product_ID = Maga_Pro_ID and Magazine_ID = Seri_name_ID and Magazine_ID = Sub_seriname_ID and Seri_name_ID  = Sub_seriname_ID and S_Cus_ID = Customer_ID and Customer_ID = input_ID;
end$$
DELIMITER ;

call getReview(12);
-- call getBookUI();


    