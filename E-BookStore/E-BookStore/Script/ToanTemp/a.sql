create database db;
use db;


create table CUSTOMERS (
Firstname 			varchar(205) 	NOT NULL,
Last_name 			varchar(205) 	NOT NULL,
Customer_ID 		int 			auto_increment,
Cus_Username 		varchar(165) 	NOT NULL,
Email 				varchar(255),
Street 				varchar(255)		NOT NULL,
City 				varchar(255)		NOT NULL,
Telephone_number 	char(10),
PRIMARY KEY (Customer_ID)
);

create table ACCOUNTS (
Username 			varchar(165)		NOT NULL,
Role 				varchar(205) 	NOT NULL,
Password 			varchar(205),
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
Payment_method 		varchar(205),
Note 				text,
Order_ID			int				NOT NULL auto_increment ,
Or_cus_ID			int				,
Ship_cash 			int 			,
PRIMARY KEY (Order_ID)
);

create table STATUS (
Status 				varchar(205)		,
Cancelled_Time		datetime,
Completed_Time		datetime,
Submission_Time 	datetime,
Delivering_Time 	datetime	,
Sta_Order_ID 		int			,
PRIMARY KEY (Sta_Order_ID)
);

create table STORES (
Street				varchar(255) 	,
City				varchar(255)		,
CONSTRAINT PK_STORES PRIMARY KEY (Street,City)
);

create table STO_HAS_SHIP (
H_City 				varchar(255)		,
H_Street 			varchar(255) 	,
H_Ship_ID			int				,
PRIMARY KEY (H_Street,H_City,H_Ship_ID)
);

create table MAGAZINE_SERI_NAMES (
Seri_name_ID 		int 			NOT NULL,
Publisher			varchar(205)		NOT NULL,
Name				varchar(20)		NOT NULL,
PRIMARY KEY (Seri_name_ID)
);

create table SUBCRIBE (
S_Cus_ID 			int				NOT NULL,
Sub_seriname_ID		int				NOT NULL,
PRIMARY KEY (S_Cus_ID,Sub_seriname_ID)
);

create table AUTHORS (
Name 				varchar(205)		NOT NULL,
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
Publisher			varchar(205)		NOT NULL,
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
BoundDistance		int 			,
Name				varchar(205)		,
Shipment_ID			int 			,
Price 				int 			,
PRIMARY KEY (Shipment_ID)		
);

create table MAGAZINES(
Publish_date 		date			NOT NULL,
NO					int 			NOT NULL,
Maga_Pro_ID			int				NOT NULL,
Magazine_ID			int				NOT NULL,
PRIMARY KEY (Maga_Pro_ID)
);



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

INSERT products
VALUES("Kha Van Can", "Ho Chi Minh", "Tieng Viet", 58000, "https://static.wikia.nocookie.net/date-a-live/images/0/0e/Kurumi_Normal.png/revision/latest?cb=20200505041739&path-prefix=vi",10, 0);

INSERT products
VALUES("Kha Van Can", "Ho Chi Minh", "Tieng Viet", 10000, "https://static.wikia.nocookie.net/date-a-live/images/0/0e/Kurumi_Normal.png/revision/latest?cb=20200505041739&path-prefix=vi",2, 1);

INSERT books
VALUES("Day Lam Giau", "ABC", 2020, 100, 0);

INSERT books
VALUES("Trang Quynh tap 99", "Kim Dong", 2002, 89, 1);


call getMagazineUI();
call getBookUI();
    




delimiter $$
create procedure totalProCate(cus_Id int)
begin
select count(*) as totall
from p_part_of,Orders,Status
where p_order_id = order_ID and or_cus_id = cus_Id and Sta_Order_ID = Order_ID and Status = 'onCart' ; 
end $$
delimiter ;

delimiter $$
create procedure UpdateProduct(cus_Id int)
begin
select Order_ID 
from Orders, Status
where  Sta_Order_ID = Order_ID and Status = 'onCart' and Or_Cus_ID = cus_Id;
end $$
delimiter ;


delimiter $$
create procedure OrderIDuse(cus_Id int)
begin
select Order_ID
from Orders,Status
where or_cus_id = cus_Id and Sta_Order_ID = Order_ID and Status = 'onCart' ; 
end $$
delimiter ;


delimiter $$
create procedure OrderIDuse2(cus_Id int)
begin
select Order_ID
from Orders
where or_cus_id = cus_Id ; 
end $$
delimiter ;


--  alter table CUSTOMERS
-- 	add FOREIGN KEY(Cus_Username) 
-- 			REFERENCES ACCOUNTS(Username)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table REVIEWS
-- 	add FOREIGN KEY(ReCustomer_ID) 
-- 			REFERENCES CUSTOMERS(Customer_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(Review_Reply_Date) 
-- 			REFERENCES REVIEWS(Date)
-- 		ON DELETE SET NULL
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(Re_Pro_ID) 
-- 			REFERENCES PRODUCTS(Product_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table ORDERS
-- 	add FOREIGN KEY(Or_cus_ID) 
-- 			REFERENCES CUSTOMERS(Customer_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table STATUS
-- 	add FOREIGN KEY(Sta_Order_ID) 
-- 			REFERENCES ORDERS(Order_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table STO_HAS_SHIP
-- 	add FOREIGN KEY(H_Street,H_City) 
-- 			REFERENCES STORES(Street,City)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(H_Ship_ID) 
-- 			REFERENCES SHIPMENT(Shipment_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table SUBCRIBE
-- 	add FOREIGN KEY(S_Cus_ID) 
-- 			REFERENCES CUSTOMERS(Customer_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(Sub_seriname_ID) 
-- 			REFERENCES MAGAZINE_SERI_NAMES(Seri_name_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table WRITTEN_BY
-- 	add FOREIGN KEY(WBook_Pro_ID) 
-- 			REFERENCES BOOKS(Book_Pro_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(WAuthor_ID) 
-- 			REFERENCES AUTHORS(Author_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;

-- alter table BOOKS
-- 	add FOREIGN KEY(Book_Pro_ID) 
-- 			REFERENCES PRODUCTS(Product_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;



-- alter table P_PART_OF
-- 	add FOREIGN KEY(P_Order_ID) 
-- 			REFERENCES ORDERS(Order_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(P_Product_ID)
-- 			REFERENCES PRODUCTS(Product_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;
-- alter table MAGAZINES
-- 	add FOREIGN KEY(Maga_Pro_ID) 
-- 			REFERENCES PRODUCTS(Product_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE,
-- 	add FOREIGN KEY(Magazine_ID) 
-- 			REFERENCES MAGAZINE_SERI_NAMES(Seri_name_ID)
-- 		ON DELETE CASCADE
-- 		ON UPDATE CASCADE
-- ;