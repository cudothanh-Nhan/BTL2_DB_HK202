
DROP PROCEDURE IF EXISTS GetAllOrder;
DROP PROCEDURE IF EXISTS GetNOfOrderItem;
DROP PROCEDURE IF EXISTS GetBookInfo;
DROP PROCEDURE IF EXISTS GetMagazineInfo;
DROP PROCEDURE IF EXISTS IsBook;
DROP PROCEDURE IF EXISTS UpdateStatus;
DROP PROCEDURE IF EXISTS GetAllShipment;
DELIMITER //

CREATE PROCEDURE GetAllOrder(
	IN customerId INT,
    IN statusVal VARCHAR(20))
BEGIN
	SELECT DISTINCT o.Order_ID, p.P_Product_ID, s.Status, o.Ship_cash, o.Ship_Name, p.Order_quantity
	FROM customers as c, orders as o, status as s, p_part_of as p
	WHERE o.Order_ID = p.P_Order_ID and  p.P_Order_ID IN (
						SELECT o.Order_ID
						FROM customers as c, orders as o, status as s
						WHERE (o.Or_cus_ID = customerId or @cutomerId = 2)
								and o.Order_ID = s.Sta_Order_ID
								and s.Status = statusVal)
	ORDER BY o.Order_ID;
END //

CREATE PROCEDURE getNOfOrderItem(
	IN customerId INT,
    IN statusVal VARCHAR(20))
BEGIN
	SELECT COUNT(p.P_Product_ID)
	FROM customers as c, orders as o, status as s, p_part_of as p
	WHERE p.P_Order_ID IN (
						SELECT o.Order_ID
						FROM customers as c, orders as o, status as s
						WHERE o.Or_cus_ID = customerId
								and o.Order_ID = s.Sta_Order_ID
								and s.Status = statusVal);
END //
CREATE PROCEDURE GetBookInfo(IN productId INT)
BEGIN
	SELECT b.Name, p.Price, p.ImgUrl
    FROM books as b, products p
    where b.Book_Pro_Id = productId
			and p.Product_ID = productId;
END //

CREATE PROCEDURE GetMagazineInfo(IN productId INT)
BEGIN
	SELECT concat(n.Name, " No.", m.No) as Name, p.Price, p.ImgUrl
    FROM magazines as m, products as p, magazine_seri_names as n
    where m.Maga_Pro_ID = productId
			and p.Product_ID = productId
            and n.Seri_name_ID = m.Magazine_ID;
END //

CREATE PROCEDURE UpdateStatus(IN productId INT, IN statusVal CHAR(20))
BEGIN
	UPDATE status as s
    SET	s.status = statusVal
    WHERE s.Sta_Order_ID = productId;
END //

CREATE PROCEDỦE GetAllCustomer()
BEGIN
	SELECT c.Customer_ID
    FROM customers as c;
END

CREATE PROCEDURE GetAllShipment()
BEGIN
	SELECT *
    FROM shipment;
END
DELIMITER ;
call GetAllShipment();
-- call GetAllOrder(0, "OnCart");
-- call GetMagazineInfo(2);
-- call GetBookInfo(2);
-- call GetMagazineInOrder(0, 0);
-- call GetMagazineInfo(2);
-- call UpdateStatus(0, "Canceled");
