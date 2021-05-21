
DROP PROCEDURE IF EXISTS GetAllOrderItem;
DROP PROCEDURE IF EXISTS GetBookOrderItem;
DELIMITER //

CREATE PROCEDURE GetAllOrderItem(
	IN customerId INT,
    IN statusVal VARCHAR(20))
BEGIN
	SELECT p.P_Product_ID, p.Order_quantity
	FROM customers as c, orders as o, status as s, p_part_of as p
	WHERE p.P_Order_ID IN (
						SELECT o.Order_ID
						FROM customers as c, orders as o, status as s
						WHERE o.Or_cus_ID = customerId
								and o.Order_ID = s.Sta_Order_ID
								and s.Status = statusVal);
END //

CREATE PROCEDURE GetBookOrderItem(
	IN pOrderId INT,
    IN pProductId INT)
BEGIN
	SELECT b.Name, prod.Price, part.Order_quantity
	FROM products as prod, books as b, orders as o, p_part_of as part
    WHERE part.P_Product_ID = pProductId
			and part.P_Order_ID = pOrderId
            and prod.Product_ID = pProductId
            and b.Book_Pro_ID = pProductId;
END //




DELIMITER ;
call GetAllOrderItem(0, "OnCart");
call GetBookOrderItem(0, 0);
call Foo(GetAllOrderItem(0, "OnCart"));