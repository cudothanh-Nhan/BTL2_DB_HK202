
DROP PROCEDURE IF EXISTS GetAllOrder;
DROP PROCEDURE IF EXISTS GetNOfOrderItem;
DROP PROCEDURE IF EXISTS GetBookInfo;
DROP PROCEDURE IF EXISTS GetMagazineInfo;
DROP PROCEDURE IF EXISTS IsBook;
DROP PROCEDURE IF EXISTS UpdateStatus;
DROP PROCEDURE IF EXISTS GetAllShipment;
DROP PROCEDURE IF EXISTS GetAllCustomer;
DROP PROCEDURE IF EXISTS UpdateShip;
DROP PROCEDURE IF EXISTS GetProductQuantity;
DROP PROCEDURE IF EXISTS UpdateOrderItemQuantity;
DROP PROCEDURE IF EXISTS InsertCompletedTime;
DROP PROCEDURE IF EXISTS UpdateProductQuantity;
DROP PROCEDURE IF EXISTS InsertReview;

DELIMITER //

CREATE PROCEDURE GetAllOrder(
	IN customerId INT,
    IN statusVal VARCHAR(20))
BEGIN
	SELECT DISTINCT o.Order_ID, p.P_Product_ID, s.Status, o.Ship_cash, o.Ship_Name, p.Order_quantity, s.Submission_Time, s.Delivering_Time, s.Completed_Time, s.Cancelled_Time
	FROM customers as c, orders as o, status as s, p_part_of as p
	WHERE o.Order_ID = p.P_Order_ID and s.Status = statusVal and o.Order_ID = s.Sta_Order_ID and  p.P_Order_ID IN (
						SELECT o.Order_ID
						FROM customers as c, orders as o, status as s
						WHERE 	o.Or_cus_ID = customerId)
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
	SELECT b.Name, p.Price, p.Quantity, p.ImgUrl
    FROM books as b, products p
    where b.Book_Pro_Id = productId
			and p.Product_ID = productId;
END //

CREATE PROCEDURE GetMagazineInfo(IN productId INT)
BEGIN
	SELECT concat(n.Name, " No.", m.No) as Name, p.Price, p.Quantity, p.ImgUrl
    FROM magazines as m, products as p, magazine_seri_names as n
    where m.Maga_Pro_ID = productId
			and p.Product_ID = productId
            and n.Seri_name_ID = m.Magazine_ID;
END //

CREATE PROCEDURE UpdateStatus(IN orderId INT, IN statusVal CHAR(20))
BEGIN
	if statusVal <> "Deleted" then
		UPDATE status as s
		SET	s.status = statusVal
		WHERE s.Sta_Order_ID = orderId;
    end if;
    
	if statusVal = "Submitted" then
		UPDATE status as s
		SET	s.Submission_Time = now()
		WHERE s.Sta_Order_ID = orderId;
	elseif statusVal = "Delivering" then
		UPDATE status as s
        SET s.Delivering_Time = now()
        WHERE s.Sta_Order_ID = orderId;
	elseif statusVal ="Canceled" then
		UPDATE status as s
        SET s.Cancelled_Time = now()
        WHERE s.Sta_Order_ID = orderId;
	elseif statusVal = "Deleted" then
		DELETE FROM orders as o
        WHERE o.Order_ID = orderId;
    end if;
END //
CREATE PROCEDURE UpdateShip(IN orderId INT, IN shipName CHAR(20), IN shipVal INT)
BEGIN
	UPDATE orders as o
    SET o.Ship_cash = shipVal, o.Ship_name = shipName
    where o.Order_ID = orderId;
END //


CREATE PROCEDURE GetAllCustomer()
BEGIN
	SELECT c.Customer_ID
    FROM customers as c;
END //

CREATE PROCEDURE GetAllShipment()
BEGIN
	SELECT *
    FROM shipment;
END //

CREATE PROCEDURE GetProductQuantity(IN productId INT)
BEGIN
	SELECT p.Quantity
    FROM products as p
    WHERE p.Product_Id = productId;
END //

CREATE PROCEDURE UpdateOrderItemQuantity(IN orderId INT, IN productId INT, IN newQuantity INT)
BEGIN
    
	if(newQuantity = 0)
    THEN
		DELETE FROM p_part_of as p
        WHERE p.P_Order_Id = orderId and p.P_Product_ID = productId;
    ELSE
		UPDATE p_part_of as p
		SET p.Order_quantity = newQuantity
		WHERE p.P_Order_Id = orderId and p.P_Product_Id = productId;
    END IF;

END //
CREATE PROCEDURE InsertCompletedTime(IN orderId INT)
BEGIN
	UPDATE status as s
    SET Completed_Time = now()
    WHERE s.Sta_Order_Id = orderId;
END //

CREATE PROCEDURE UpdateProductQuantity (IN productId INT, IN subQuantity INT)
BEGIN
	UPDATE products as pro
	SET pro.Quantity = pro.Quantity - subQuantity
	WHERE pro.Product_Id = productId;
END //

CREATE PROCEDURE InsertReview(IN productId INT, IN imgUrl CHAR(255), IN cmt CHAR(255),  IN rating INT, IN customerId INT)
BEGIN
	insert reviews
	values(customerId, now(), rating, imgUrl, cmt, NULL, productId);
END //
DELIMITER ;
call GetAllCustomer();
-- call UpdateStatus(0, "Submitted");
-- call GetAllShipment;
-- call GetAllOrder(0, "Submitted");
-- call GetMagazineInfo(0);
-- call GetBookInfo(0);
-- call GetMagazineInfo(2);
-- call UpdateStatus(0, "Canceled");
-- call GetAllOrder(0, "Submitted");
-- call GetProductQuantity(0);
-- call UpdateOrderItemQuantity(1, 2, 3);


-- insert products
-- values("Kha Van Can", "Ho Chi Minh", "Tieng Viet", 45000, 5, 3, "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/t/u/tuong-lai-sau-dai-dich-covid_b_a.jpg");
-- insert books
-- values("Tuong lai sau dai dich Covid", "NXB Tre", 2021, 60, 3);

-- insert products
-- values("Kha Van Can", "Ho Chi Minh", "Tieng Viet", 45000, 20, 4, "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/d/e/decheamazonvacuocchienthuongmaitoancau_bia1.jpg");
-- insert books
-- values("De che Amazon va cuoc chien thuong mai toan cau", "NXB Lao Dong", 2020, 200, 4);
