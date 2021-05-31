DROP PROCEDURE IF EXISTS getBookUI;
DROP PROCEDURE IF EXISTS getBookDetail;
DROP PROCEDURE IF EXISTS getMagazineUI;
DROP PROCEDURE IF EXISTS getMagazineDetail;
DROP PROCEDURE IF EXISTS getAllProID;
DROP PROCEDURE IF EXISTS getAllBookID;
DROP PROCEDURE IF EXISTS getAllMagazineID;
DROP PROCEDURE IF EXISTS getAllMagazineSeriID;
DROP PROCEDURE IF EXISTS removeBook;
DROP PROCEDURE IF EXISTS removeMagazine;
DROP PROCEDURE IF EXISTS totalProCate;
DROP PROCEDURE IF EXISTS OrderIDuse;
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

delimiter $$
create procedure totalProCate(cus_Id int)
begin
select count(*) as totall
from p_part_of,Orders,Status as s
where p_order_id = order_ID and s.Sta_Order_Id = order_ID and or_cus_id = cus_Id and s.Status = "OnCart" ; 
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
call totalProCate(1);