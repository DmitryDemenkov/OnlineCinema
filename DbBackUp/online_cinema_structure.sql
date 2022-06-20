CREATE DATABASE  IF NOT EXISTS `online_cinema` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `online_cinema`;
-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: localhost    Database: online_cinema
-- ------------------------------------------------------
-- Server version	8.0.28

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
-- Table structure for table `age_restrictions`
--

DROP TABLE IF EXISTS `age_restrictions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `age_restrictions` (
  `idage_restriction` int NOT NULL AUTO_INCREMENT,
  `russion` enum('0+','6+','12+','16+','18+') DEFAULT NULL,
  `motion_picture` enum('G','PG','PG-13','R','NC-17') DEFAULT NULL,
  PRIMARY KEY (`idage_restriction`),
  KEY `russion_idx` (`russion`)
) ENGINE=InnoDB AUTO_INCREMENT=2009 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `critic_review`
--

DROP TABLE IF EXISTS `critic_review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `critic_review` (
  `idcritic_review` int NOT NULL AUTO_INCREMENT,
  `author` varchar(45) NOT NULL,
  `sours` varchar(5000) NOT NULL,
  `idfilm` int NOT NULL,
  PRIMARY KEY (`idcritic_review`),
  KEY `idreview_to_film_idx` (`idfilm`),
  CONSTRAINT `fk_review_to_film` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`)
) ENGINE=InnoDB AUTO_INCREMENT=2010 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `film_information`
--

DROP TABLE IF EXISTS `film_information`;
/*!50001 DROP VIEW IF EXISTS `film_information`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `film_information` AS SELECT 
 1 AS `idfilm`,
 1 AS `title`,
 1 AS `category`,
 1 AS `annotation`,
 1 AS `release_date`,
 1 AS `purchase_price`,
 1 AS `rental_price`,
 1 AS `rental_duration`,
 1 AS `russion`,
 1 AS `middle_rating`,
 1 AS `rating_amount`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `film_production`
--

DROP TABLE IF EXISTS `film_production`;
/*!50001 DROP VIEW IF EXISTS `film_production`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `film_production` AS SELECT 
 1 AS `idfilm`,
 1 AS `title`,
 1 AS `category`,
 1 AS `release_date`,
 1 AS `name`,
 1 AS `post`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `film_to_order`
--

DROP TABLE IF EXISTS `film_to_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `film_to_order` (
  `idfilm` int NOT NULL,
  `idorder` int NOT NULL,
  `type` enum('Покупка','Аренда') NOT NULL,
  PRIMARY KEY (`idfilm`,`idorder`),
  KEY `idorder_to_film_idx` (`idorder`),
  CONSTRAINT `fk_film_to_order` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`),
  CONSTRAINT `fk_order_to_film` FOREIGN KEY (`idorder`) REFERENCES `orders` (`idorder`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `film_to_subscription`
--

DROP TABLE IF EXISTS `film_to_subscription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `film_to_subscription` (
  `idfilm` int NOT NULL,
  `idsubscription` int NOT NULL,
  PRIMARY KEY (`idfilm`,`idsubscription`),
  KEY `idsubscription_to_film_idx` (`idsubscription`),
  CONSTRAINT `fk_film_to_subscription` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`),
  CONSTRAINT `fk_subscription_to_film` FOREIGN KEY (`idsubscription`) REFERENCES `subscriptions` (`idsubscription`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `films`
--

DROP TABLE IF EXISTS `films`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `films` (
  `idfilm` int NOT NULL AUTO_INCREMENT,
  `title` varchar(200) NOT NULL,
  `category` varchar(20) NOT NULL,
  `annotation` varchar(200) DEFAULT NULL,
  `release_date` date NOT NULL,
  `purchase_price` int NOT NULL,
  `rental_price` int NOT NULL,
  `rental_duration` int NOT NULL,
  `idage_restriction` int NOT NULL,
  PRIMARY KEY (`idfilm`),
  KEY `fk_film_to_age_restriction_idx` (`idage_restriction`),
  KEY `film_title_idx` (`title`),
  CONSTRAINT `fk_film_to_age_restriction` FOREIGN KEY (`idage_restriction`) REFERENCES `age_restrictions` (`idage_restriction`)
) ENGINE=InnoDB AUTO_INCREMENT=33843 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `films_of_genre`
--

DROP TABLE IF EXISTS `films_of_genre`;
/*!50001 DROP VIEW IF EXISTS `films_of_genre`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `films_of_genre` AS SELECT 
 1 AS `idfilm`,
 1 AS `title`,
 1 AS `category`,
 1 AS `release_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `films_tmp`
--

DROP TABLE IF EXISTS `films_tmp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `films_tmp` (
  `idfilm` int NOT NULL,
  PRIMARY KEY (`idfilm`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `genres`
--

DROP TABLE IF EXISTS `genres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genres` (
  `idgenre` int NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`idgenre`),
  UNIQUE KEY `genres_name_idx` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `genres_to_film`
--

DROP TABLE IF EXISTS `genres_to_film`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genres_to_film` (
  `idgenre` int NOT NULL,
  `idfilm` int NOT NULL,
  PRIMARY KEY (`idgenre`,`idfilm`),
  KEY `fk_film_to_genre_idx` (`idfilm`),
  CONSTRAINT `fk_film_to_genre` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`),
  CONSTRAINT `fk_genre_to_film` FOREIGN KEY (`idgenre`) REFERENCES `genres` (`idgenre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `idorder` int NOT NULL AUTO_INCREMENT,
  `date` datetime NOT NULL,
  `iduser` int NOT NULL,
  PRIMARY KEY (`idorder`),
  KEY `idorder_to_user_idx` (`iduser`),
  KEY `order_date_idx` (`date`),
  CONSTRAINT `fk_order_to_user` FOREIGN KEY (`iduser`) REFERENCES `users` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=32790 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `password_log`
--

DROP TABLE IF EXISTS `password_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `password_log` (
  `idpassword_log` int NOT NULL AUTO_INCREMENT,
  `iduser` int DEFAULT NULL,
  `oldpassword` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idpassword_log`),
  KEY `fk_password_log_to_user` (`iduser`),
  CONSTRAINT `fk_password_log_to_user` FOREIGN KEY (`iduser`) REFERENCES `users` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `persons`
--

DROP TABLE IF EXISTS `persons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `persons` (
  `idperson` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `information` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`idperson`),
  KEY `person_name_idx` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=20511 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `posts`
--

DROP TABLE IF EXISTS `posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts` (
  `idpost` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`idpost`),
  UNIQUE KEY `post_name_idx` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `productions`
--

DROP TABLE IF EXISTS `productions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productions` (
  `idperson` int NOT NULL,
  `idpost` int NOT NULL,
  `idfilm` int NOT NULL,
  PRIMARY KEY (`idperson`,`idpost`,`idfilm`),
  KEY `fk_production_to_film_idx` (`idfilm`),
  KEY `fk_production_to_post_idx` (`idpost`),
  CONSTRAINT `fk_production_to_film` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`),
  CONSTRAINT `fk_production_to_person` FOREIGN KEY (`idperson`) REFERENCES `persons` (`idperson`),
  CONSTRAINT `fk_production_to_post` FOREIGN KEY (`idpost`) REFERENCES `posts` (`idpost`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ratings`
--

DROP TABLE IF EXISTS `ratings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ratings` (
  `iduser` int NOT NULL,
  `idfilm` int NOT NULL,
  `plot` int NOT NULL,
  `action` int NOT NULL,
  `actor_play` int NOT NULL,
  `effects` int NOT NULL,
  PRIMARY KEY (`idfilm`,`iduser`),
  KEY `idfilm_idx` (`idfilm`),
  KEY `iduser_idy` (`iduser`),
  CONSTRAINT `fk_film_to_rating` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`),
  CONSTRAINT `fk_user_to_rating` FOREIGN KEY (`iduser`) REFERENCES `users` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `studio_to_film`
--

DROP TABLE IF EXISTS `studio_to_film`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studio_to_film` (
  `idfilm` int NOT NULL,
  `idstudio` int NOT NULL,
  PRIMARY KEY (`idfilm`,`idstudio`),
  KEY `fk_studio_to_film_idx` (`idstudio`),
  CONSTRAINT `fk_film_to_studio` FOREIGN KEY (`idfilm`) REFERENCES `films` (`idfilm`),
  CONSTRAINT `fk_studio_to_film` FOREIGN KEY (`idstudio`) REFERENCES `studios` (`idstudio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `studios`
--

DROP TABLE IF EXISTS `studios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studios` (
  `idstudio` int NOT NULL AUTO_INCREMENT,
  `name` varchar(150) NOT NULL,
  `information` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`idstudio`)
) ENGINE=InnoDB AUTO_INCREMENT=2006 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `subscription_to_user`
--

DROP TABLE IF EXISTS `subscription_to_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscription_to_user` (
  `idsubscription_to_user` int NOT NULL AUTO_INCREMENT,
  `iduser` int NOT NULL,
  `idsubscription` int NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`idsubscription_to_user`),
  KEY `iduser_idx` (`iduser`),
  KEY `idsubscription_idx` (`idsubscription`),
  KEY `subscription_to_user_date_idx` (`date`),
  CONSTRAINT `fk_subscription_to_user` FOREIGN KEY (`idsubscription`) REFERENCES `subscriptions` (`idsubscription`),
  CONSTRAINT `fk_user_to_subscription` FOREIGN KEY (`iduser`) REFERENCES `users` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=32768 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `subscriptions`
--

DROP TABLE IF EXISTS `subscriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscriptions` (
  `idsubscription` int NOT NULL AUTO_INCREMENT,
  `type` varchar(20) NOT NULL,
  `price` int NOT NULL,
  `duration` int NOT NULL,
  PRIMARY KEY (`idsubscription`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `user_library`
--

DROP TABLE IF EXISTS `user_library`;
/*!50001 DROP VIEW IF EXISTS `user_library`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `user_library` AS SELECT 
 1 AS `idfilm`,
 1 AS `title`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `iduser` int NOT NULL AUTO_INCREMENT,
  `login` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `birth_date` date DEFAULT '0001-01-01',
  PRIMARY KEY (`iduser`),
  UNIQUE KEY `login_UNIQUE` (`login`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=14155 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `password_update` BEFORE UPDATE ON `users` FOR EACH ROW BEGIN
	IF NOT NEW.`password` = OLD.`password` THEN
		INSERT INTO password_log
        SET iduser = OLD.iduser,
        oldpassword = OLD.`password`;
	END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Dumping events for database 'online_cinema'
--

--
-- Dumping routines for database 'online_cinema'
--
/*!50003 DROP FUNCTION IF EXISTS `count_total_order_cost` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `count_total_order_cost`(idorder INT) RETURNS int
    DETERMINISTIC
BEGIN
	DECLARE total_cost INT;
    SET total_cost = 0;
	SELECT SUM(IF(film_to_order.`type`='Покупка', films.purchase_price, films.rental_price)) INTO total_cost
    FROM film_to_order
    JOIN films ON films.idfilm=film_to_order.idfilm
    WHERE film_to_order.idorder=idorder;
    RETURN total_cost;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `middle_rate` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `middle_rate`(idfilm INT) RETURNS float
    DETERMINISTIC
BEGIN
	DECLARE middle FLOAT DEFAULT 0;
	SELECT AVG((r.plot+r.`action`+r.actor_play+r.effects)/4) INTO middle
	FROM films AS f
	LEFT JOIN ratings AS r ON r.idfilm=f.idfilm
	WHERE f.idfilm=idfilm;
    SET middle = ROUND(middle, 2);
    RETURN middle;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `drop_user` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `drop_user`(IN id INT)
BEGIN
	DELETE FROM film_to_order 
	WHERE (idorder IN (SELECT idorder FROM orders WHERE iduser=id));

	DELETE FROM orders WHERE (iduser=id);

	DELETE FROM subscription_to_user WHERE (iduser=id);

	DELETE FROM ratings WHERE (iduser=id);

	DELETE FROM users WHERE (iduser=id);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `update_rating` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_rating`(IN idfilm INT, IN iduser INT, INOUT plot FLOAT, INOUT `action` FLOAT, INOUT actor_play FLOAT, INOUT effects FLOAT)
BEGIN
	DECLARE film INT DEFAULT 0;
	
    SELECT ratings.idfilm INTO film
    FROM ratings 
    WHERE ratings.idfilm=idfilm AND ratings.iduser=iduser;
    
    IF film=0 THEN
		INSERT INTO ratings VALUES (iduser, idfilm, plot, `action`, actor_play, effects);
	ELSE
		UPDATE ratings AS r SET r.plot=plot, r.`action`=`action`, r.actor_play=actor_play, r.effects=effects
        WHERE r.idfilm=idfilm AND r.iduser=iduser;
	END IF;
    
    SELECT AVG(r.plot) AS plot, AVG(r.`action`) AS `action`, AVG(r.actor_play) AS actor_play, AVG(r.effects) AS effects
    INTO plot, `action`, actor_play, effects
	FROM films AS f
	JOIN ratings AS r ON r.idfilm=f.idfilm
    WHERE f.idfilm=idfilm;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `film_information`
--

/*!50001 DROP VIEW IF EXISTS `film_information`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `film_information` AS select `films`.`idfilm` AS `idfilm`,`films`.`title` AS `title`,`films`.`category` AS `category`,`films`.`annotation` AS `annotation`,`films`.`release_date` AS `release_date`,`films`.`purchase_price` AS `purchase_price`,`films`.`rental_price` AS `rental_price`,`films`.`rental_duration` AS `rental_duration`,`age_restrictions`.`russion` AS `russion`,ifnull(round(avg(((((`ratings`.`action` + `ratings`.`actor_play`) + `ratings`.`effects`) + `ratings`.`plot`) / 4)),2),0) AS `middle_rating`,count(`ratings`.`iduser`) AS `rating_amount` from ((`films` join `age_restrictions` on((`age_restrictions`.`idage_restriction` = `films`.`idage_restriction`))) left join `ratings` on((`ratings`.`idfilm` = `films`.`idfilm`))) group by `films`.`idfilm` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `film_production`
--

/*!50001 DROP VIEW IF EXISTS `film_production`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `film_production` AS select `films`.`idfilm` AS `idfilm`,`films`.`title` AS `title`,`films`.`category` AS `category`,`films`.`release_date` AS `release_date`,`persons`.`name` AS `name`,`posts`.`name` AS `post` from (((`films` join `productions` on((`films`.`idfilm` = `productions`.`idfilm`))) join `persons` on((`persons`.`idperson` = `productions`.`idperson`))) join `posts` on((`posts`.`idpost` = `productions`.`idpost`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `films_of_genre`
--

/*!50001 DROP VIEW IF EXISTS `films_of_genre`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `films_of_genre` AS select `films`.`idfilm` AS `idfilm`,`films`.`title` AS `title`,`films`.`category` AS `category`,`films`.`release_date` AS `release_date` from ((`films` join `genres_to_film` on((`genres_to_film`.`idfilm` = `films`.`idfilm`))) join `genres` on((`genres`.`idgenre` = `genres_to_film`.`idgenre`))) where (`genres`.`name` = 'комедия') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `user_library`
--

/*!50001 DROP VIEW IF EXISTS `user_library`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `user_library` AS select `films`.`idfilm` AS `idfilm`,`films`.`title` AS `title` from ((`orders` join `film_to_order` on((`film_to_order`.`idorder` = `orders`.`idorder`))) join `films` on((`film_to_order`.`idfilm` = `films`.`idfilm`))) where ((`orders`.`iduser` = 5020) and ((`film_to_order`.`type` = 'Покупка') or ((`orders`.`date` + interval `films`.`rental_duration` hour) > now()))) union select distinct `films`.`idfilm` AS `idfilm`,`films`.`title` AS `title` from (((`subscription_to_user` join `subscriptions` on((`subscription_to_user`.`idsubscription` = `subscriptions`.`idsubscription`))) join `film_to_subscription` on((`film_to_subscription`.`idsubscription` = `subscriptions`.`idsubscription`))) join `films` on((`films`.`idfilm` = `film_to_subscription`.`idfilm`))) where ((`subscription_to_user`.`iduser` = 5020) and ((`subscription_to_user`.`date` + interval `subscriptions`.`duration` month) > now())) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-06-19 16:34:19
