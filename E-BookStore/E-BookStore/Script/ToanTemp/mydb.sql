create database DB;
use DB;

create table CUSTOMERS (
Firstname 			varchar(255) 	NOT NULL,
Last_name 			varchar(255) 	NOT NULL,
Customer_ID 		int 			NOT NULL	 AUTO_INCREMENT,
Cus_Username 		varchar(60) 	NOT NULL,
Email 				varchar(255),
Street 				varchar(255)		NOT NULL,
City 				varchar(255)		NOT NULL,
Telephone_number 	char(10) 		,
PRIMARY KEY (Customer_ID)
);


create table ACCOUNTS (
Username 			varchar(60)		NOT NULL,
Role 				varchar(20) 	NOT NULL,
check (Role = 'Customer' || Role = 'Warehouse Staff' || Role = 'Normal Staff' || Role = 'Manager'),
Password 			varchar(32),
PRIMARY KEY (Username)
);

create table REVIEWS (
ReCustomer_ID		int 			NOT NULL,
Date				Date 			NOT NULL,
Rating 				int				NOT NULL,
check (Rating between 1 and 5),
Image_URL			blob,
Comment_text 		text,
Review_Reply_Date 	Date,
Re_Pro_ID 			int 			NOT NULL,
PRIMARY KEY(Date,ReCustomer_ID)
);

create table ORDERS (
Payment_method 		varchar(20)		DEFAULT 'Cash',
check (Payment_method = 'Cash' || Payment_method = 'Bank Card'),
Note 				text,
Order_ID			int				NOT NULL	AUTO_INCREMENT,
Or_cus_ID			int				NOT NULL,
Ship_cash 			int,
check (Ship_cash >= 0),
PRIMARY KEY (Order_ID)
);

create table STATUS (
Status 				varchar(20)		NOT NULL,
check (Status = 'onCart' || Status = 'Submitted' || Status = 'Pending' || Status = 'Processing' || Status = 'Delivering' || Status = 'Completed' || Status = 'Canceled'),
Cancelled_Time		datetime,
Completed_Time		datetime,
Submission_Time 	datetime,
Delivering_Time 	datetime,
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
check (Pages >= 0),	
Book_Pro_ID			int				NOT NULL,
PRIMARY KEY (Book_Pro_ID)
);

create table PRODUCTS (
Sto_Street			varchar(25)		NOT NULL,
Sto_City			varchar(25)		NOT NULL,
Language			varchar(15),
Price				int				NOT NULL,
check (Price > 0),
Quantity			int				NOT NULL,
check (Quantity >= 0),
Product_ID			int 			NOT NULL,
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
check (BoundDistance >= 0),
Name				varchar(20)		NOT NULL,
Shipment_ID			int 			NOT NULL,
Price 				int 			NOT NULL,
check (Price >= 0),
PRIMARY KEY (Shipment_ID)		
);

create table MAGAZINES(
Publish_date 		date			NOT NULL,
NO					int 			NOT NULL,
check (NO > 0),
Maga_Pro_ID			int				NOT NULL,
Magazine_ID			int				NOT NULL,
PRIMARY KEY (Maga_Pro_ID)
);



Insert into Customers (Firstname, Last_name, Cus_Username,Street,City) values ('toan','lam','toang@gmai.com','tang bat ho','ho chi minh');
Insert into Accounts (username,role,password) values ('toang@gmai.com','Customer','12345678');
insert into products (Sto_street,sto_city, price,quantity,product_ID) values ('123','123',12,12,1);
insert into products (Sto_street,sto_city, price,quantity,product_ID) values ('123','123',12,12,2);
insert into products (Sto_street,sto_city, price,quantity,product_ID) values ('123','123',12,12,3);
insert into products (Sto_street,sto_city, price,quantity,product_ID) values ('123','123',12,12,4);
insert into products (Sto_street,sto_city, price,quantity,product_ID) values ('123','123',12,12,5);


delimiter $$
create procedure totalProCate(cus_Id int)
begin
select count(*) as totall
from p_part_of,Orders
where p_order_id = order_ID and or_cus_id = cus_Id  ; 
end $$
delimiter ;

delimiter $$
create procedure OrderIDuse(cus_Id int)
begin
select Order_ID
from Orders
where or_cus_id = cus_Id  ; 
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