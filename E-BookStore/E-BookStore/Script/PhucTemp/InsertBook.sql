-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: localhost    Database: db
-- ------------------------------------------------------
-- Server version	8.0.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `Username` varchar(16) NOT NULL,
  `Role` varchar(20) NOT NULL,
  `Password` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `authors`
--

DROP TABLE IF EXISTS `authors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `authors` (
  `Name` varchar(20) NOT NULL,
  `Author_ID` int NOT NULL,
  `DOB` date NOT NULL,
  PRIMARY KEY (`Author_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `authors`
--

LOCK TABLES `authors` WRITE;
/*!40000 ALTER TABLE `authors` DISABLE KEYS */;
/*!40000 ALTER TABLE `authors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `books`
--

DROP TABLE IF EXISTS `books`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `books` (
  `Name` varchar(10000) DEFAULT NULL,
  `Publisher` varchar(255) DEFAULT NULL,
  `Publish_year` year DEFAULT NULL,
  `Pages` int DEFAULT NULL,
  `Book_Pro_ID` int NOT NULL,
  PRIMARY KEY (`Book_Pro_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `books`
--

LOCK TABLES `books` WRITE;
/*!40000 ALTER TABLE `books` DISABLE KEYS */;
INSERT INTO `books` VALUES ('Interchange Level 2 Students Book With Online Self-Study, 5th Edition','Cambridge University Press',2017,160,1),('Cambridge IELTS 15 General Training Students Book With Answers','Cambridge University Press',2020,144,2),('Sicher! B1+','Hueber',2012,136,3),('Look 6 Student Book (British English)','National Geographic',2018,200,4),('Le Nouveau Taxi!: Cahier DExercices 1','Hachette',2008,120,5),('Nghĩ Giàu & Làm Giàu (Tái Bản 2020)','NXB Tổng Hợp TPHCM',2020,400,6),('Đắc Nhân Tâm (Khổ Lớn) (Tái Bản)','NXB Tổng Hợp TPHCM',2018,320,7),('Nói Nhiều Không Bằng Nói Đúng','Dân Trí',2012,128,8),('Kỷ Nguyên Mới Của Quản Trị','Cengage Learning',2016,420,9),('Cẩm Nang Tuổi Dậy Thì Dành Cho Bạn Gái','NXB Phụ Nữ',2017,272,10);
/*!40000 ALTER TABLE `books` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customers` (
  `Firstname` varchar(20) NOT NULL,
  `Last_name` varchar(20) NOT NULL,
  `Customer_ID` int NOT NULL,
  `Cus_Username` varchar(16) NOT NULL,
  `Email` varchar(25) DEFAULT NULL,
  `Street` varchar(255) NOT NULL,
  `City` varchar(255) NOT NULL,
  `Telephone_number` char(10) NOT NULL,
  PRIMARY KEY (`Customer_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magazine_seri_names`
--

DROP TABLE IF EXISTS `magazine_seri_names`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magazine_seri_names` (
  `Seri_name_ID` int NOT NULL,
  `Publisher` varchar(20) NOT NULL,
  `Name` varchar(20) NOT NULL,
  PRIMARY KEY (`Seri_name_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magazine_seri_names`
--

LOCK TABLES `magazine_seri_names` WRITE;
/*!40000 ALTER TABLE `magazine_seri_names` DISABLE KEYS */;
/*!40000 ALTER TABLE `magazine_seri_names` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magazines`
--

DROP TABLE IF EXISTS `magazines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magazines` (
  `Publish_date` date NOT NULL,
  `NO` int NOT NULL,
  `Maga_Pro_ID` int NOT NULL,
  `Magazine_ID` int NOT NULL,
  PRIMARY KEY (`Maga_Pro_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magazines`
--

LOCK TABLES `magazines` WRITE;
/*!40000 ALTER TABLE `magazines` DISABLE KEYS */;
/*!40000 ALTER TABLE `magazines` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `Payment_method` varchar(20) NOT NULL,
  `Note` text,
  `Order_ID` int NOT NULL,
  `Or_cus_ID` int NOT NULL,
  `Ship_cash` int NOT NULL,
  PRIMARY KEY (`Order_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `p_part_of`
--

DROP TABLE IF EXISTS `p_part_of`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `p_part_of` (
  `P_Order_ID` int NOT NULL,
  `P_Product_ID` int NOT NULL,
  `Order_quantity` int NOT NULL,
  PRIMARY KEY (`P_Order_ID`,`P_Product_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `p_part_of`
--

LOCK TABLES `p_part_of` WRITE;
/*!40000 ALTER TABLE `p_part_of` DISABLE KEYS */;
/*!40000 ALTER TABLE `p_part_of` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `Sto_Street` varchar(255) NOT NULL,
  `Sto_City` varchar(255) NOT NULL,
  `Language` varchar(255) DEFAULT NULL,
  `Price` int NOT NULL,
  `imgUrl` varchar(255) DEFAULT NULL,
  `Quantity` int NOT NULL,
  `Product_ID` int NOT NULL,
  PRIMARY KEY (`Product_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES ('Cộng Hòa','Hồ Chí Minh','English',540550,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/i/m/image_224309.jpg',100,1),('Hai Bà Trưng','Hà Nội','English',607050,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/i/m/image_224304.jpg',50,2),('Nguyễn Thị Minh Khai','Hà Nội','German',278100,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/i/m/image_109397.jpg',134,3),('Cộng Hòa','Hồ Chí Minh','English',228000,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/i/m/image_228922.jpg',200,4),('Trường Chinh','Hồ Chí Minh','French',142400,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/9/7/9782011555496_1.jpg',10,5),('Cộng Hòa','Hồ Chí Minh','Vietnamese',88000,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/n/g/nghigiaulamgiau_110k-01_bia-1.jpg',250,6),('Cộng Hòa','Hồ Chí Minh','Vietnamese',55480,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/9/5/9558a365adde6688d4c71a200d78310c.jpg',5,7),('Hai Bà Trưng','Hà Nội','Vietnamese',44500,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/i/m/image_195509_1_22478.jpg',50,8),('Trường Chinh','Hồ Chí Minh','Vietnamese',348530,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/i/m/image_193617.jpg',10,9),('Hai Bà Trưng','Hà Nội','Vietnamese',54400,'https://cdn0.fahasa.com/media/catalog/product/cache/2/image/9df78eab33525d08d6e5fb8d27136e95/8/9/8936067597462.jpg',0,10);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reviews`
--

DROP TABLE IF EXISTS `reviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reviews` (
  `ReCustomer_ID` int NOT NULL,
  `Date` date NOT NULL,
  `Rating` int NOT NULL,
  `Image_URL` blob,
  `Comment_text` text,
  `Review_Reply_Date` date DEFAULT NULL,
  `Re_Pro_ID` int NOT NULL,
  PRIMARY KEY (`Date`,`ReCustomer_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reviews`
--

LOCK TABLES `reviews` WRITE;
/*!40000 ALTER TABLE `reviews` DISABLE KEYS */;
/*!40000 ALTER TABLE `reviews` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shipment`
--

DROP TABLE IF EXISTS `shipment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shipment` (
  `BoundDistance` int NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Shipment_ID` int NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Shipment_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shipment`
--

LOCK TABLES `shipment` WRITE;
/*!40000 ALTER TABLE `shipment` DISABLE KEYS */;
/*!40000 ALTER TABLE `shipment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status`
--

DROP TABLE IF EXISTS `status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `status` (
  `Status` varchar(20) NOT NULL,
  `Cancelled_Time` datetime DEFAULT NULL,
  `Completed_Time` datetime DEFAULT NULL,
  `Submission_Time` datetime NOT NULL,
  `Delivering_Time` datetime NOT NULL,
  `Sta_Order_ID` int NOT NULL,
  PRIMARY KEY (`Sta_Order_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status`
--

LOCK TABLES `status` WRITE;
/*!40000 ALTER TABLE `status` DISABLE KEYS */;
/*!40000 ALTER TABLE `status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sto_has_ship`
--

DROP TABLE IF EXISTS `sto_has_ship`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sto_has_ship` (
  `H_City` varchar(255) NOT NULL,
  `H_Street` varchar(255) NOT NULL,
  `H_Ship_ID` int NOT NULL,
  PRIMARY KEY (`H_Street`,`H_City`,`H_Ship_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sto_has_ship`
--

LOCK TABLES `sto_has_ship` WRITE;
/*!40000 ALTER TABLE `sto_has_ship` DISABLE KEYS */;
/*!40000 ALTER TABLE `sto_has_ship` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stores`
--

DROP TABLE IF EXISTS `stores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stores` (
  `Street` varchar(25) NOT NULL,
  `City` varchar(25) NOT NULL,
  PRIMARY KEY (`Street`,`City`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stores`
--

LOCK TABLES `stores` WRITE;
/*!40000 ALTER TABLE `stores` DISABLE KEYS */;
/*!40000 ALTER TABLE `stores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subcribe`
--

DROP TABLE IF EXISTS `subcribe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subcribe` (
  `S_Cus_ID` int NOT NULL,
  `Sub_seriname_ID` int NOT NULL,
  PRIMARY KEY (`S_Cus_ID`,`Sub_seriname_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subcribe`
--

LOCK TABLES `subcribe` WRITE;
/*!40000 ALTER TABLE `subcribe` DISABLE KEYS */;
/*!40000 ALTER TABLE `subcribe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `written_by`
--

DROP TABLE IF EXISTS `written_by`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `written_by` (
  `WBook_Pro_ID` int NOT NULL,
  `WAuthor_ID` int NOT NULL,
  PRIMARY KEY (`WBook_Pro_ID`,`WAuthor_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `written_by`
--

LOCK TABLES `written_by` WRITE;
/*!40000 ALTER TABLE `written_by` DISABLE KEYS */;
/*!40000 ALTER TABLE `written_by` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-05-28 15:51:58
