-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               8.0.30 - MySQL Community Server - GPL
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              12.1.0.6537
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Zrzut struktury bazy danych alcoholshop
CREATE DATABASE IF NOT EXISTS `alcoholshop` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `alcoholshop`;

-- Zrzut struktury widok alcoholshop.activeproductsview
-- Tworzenie tymczasowej tabeli, aby przezwyciężyć błędy z zależnościami w WIDOKU
CREATE TABLE `activeproductsview` (
	`ProductID` INT(10) NOT NULL,
	`Name` VARCHAR(200) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Description` TEXT NULL COLLATE 'utf8mb4_general_ci',
	`Price` DECIMAL(10,2) NOT NULL,
	`VolumeML` INT(10) NOT NULL,
	`AlcoholPercentage` DECIMAL(4,2) NOT NULL,
	`Availability` TINYINT(1) NULL,
	`Category` VARCHAR(100) NULL COLLATE 'utf8mb4_general_ci',
	`Producer` VARCHAR(100) NULL COLLATE 'utf8mb4_general_ci',
	`Country` VARCHAR(100) NULL COLLATE 'utf8mb4_general_ci'
) ENGINE=MyISAM;

-- Zrzut struktury tabela alcoholshop.aging
CREATE TABLE IF NOT EXISTS `aging` (
  `AgingID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`AgingID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.aging: ~6 rows (około)
INSERT INTO `aging` (`AgingID`, `Name`, `Description`) VALUES
	(1, 'Beczka dębowa', 'Starzenie w beczkach dębowych.'),
	(2, 'Beczka po sherry', 'Starzenie w beczkach po sherry dla dodatkowego aromatu.'),
	(3, 'Beczka po bourbonie', 'Starzenie w beczkach po bourbonie.'),
	(4, 'Stal nierdzewna', 'Starzenie w zbiornikach ze stali nierdzewnej.'),
	(5, 'Brak starzenia', 'Produkt nie jest starzony.'),
	(6, 'Beczka po porto', 'Ciemniejsze, słodsze nuty.');

-- Zrzut struktury tabela alcoholshop.blogcategories
CREATE TABLE IF NOT EXISTS `blogcategories` (
  `BlogCategoryID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`BlogCategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.blogcategories: ~6 rows (około)
INSERT INTO `blogcategories` (`BlogCategoryID`, `Name`) VALUES
	(1, 'Poradnik'),
	(2, 'Nowości'),
	(3, 'Degustacja'),
	(4, 'Historia alkoholi'),
	(5, 'Ciekawostki'),
	(6, 'Przepisy z alkoholem');

-- Zrzut struktury tabela alcoholshop.blogposts
CREATE TABLE IF NOT EXISTS `blogposts` (
  `PostID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Blogcategoryid` int DEFAULT NULL,
  `IsPublished` tinyint(1) DEFAULT '1',
  `PublishDate` datetime DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PostID`),
  KEY `blogcategories_ibfk_1` (`Blogcategoryid`),
  CONSTRAINT `blogcategories_ibfk_1` FOREIGN KEY (`Blogcategoryid`) REFERENCES `blogcategories` (`BlogCategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.blogposts: ~3 rows (około)
INSERT INTO `blogposts` (`PostID`, `Title`, `Content`, `Blogcategoryid`, `IsPublished`, `PublishDate`, `CreatedAt`) VALUES
	(1, 'Sztuka degustacji whisky — poradnik dla początkujących', 'Dowiedz się, jak prawidłowo degustować whisky i na co zwracać uwagę podczas próbowania różnych trunków.', 3, 1, '2025-06-15 10:00:00', '2025-06-29 02:21:17'),
	(2, 'Nowe piwa z Browaru Raciborskiego w naszym sklepie!', 'Z radością informujemy, że do naszej oferty dołączyły klasyczne, twierdzowe i miodowe piwa z Browaru Raciborskiego.', 2, 1, '2025-06-18 12:00:00', '2025-06-29 02:21:17'),
	(3, 'Historia ginu — od lekarstwa do koktajli', 'Poznaj fascynującą historię ginu i jego ewolucję na przestrzeni wieków.', 4, 1, '2025-06-20 14:30:00', '2025-06-29 02:21:17');

-- Zrzut struktury tabela alcoholshop.blogposttags
CREATE TABLE IF NOT EXISTS `blogposttags` (
  `PostID` int NOT NULL,
  `TagID` int NOT NULL,
  PRIMARY KEY (`PostID`,`TagID`),
  KEY `TagID` (`TagID`),
  CONSTRAINT `blogposttags_ibfk_1` FOREIGN KEY (`PostID`) REFERENCES `blogposts` (`PostID`),
  CONSTRAINT `blogposttags_ibfk_2` FOREIGN KEY (`TagID`) REFERENCES `blogtags` (`TagID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.blogposttags: ~4 rows (około)
INSERT INTO `blogposttags` (`PostID`, `TagID`) VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(3, 4);

-- Zrzut struktury tabela alcoholshop.blogtags
CREATE TABLE IF NOT EXISTS `blogtags` (
  `TagID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`TagID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.blogtags: ~5 rows (około)
INSERT INTO `blogtags` (`TagID`, `Name`) VALUES
	(1, 'Poradnik'),
	(2, 'Wina'),
	(3, 'Whisky'),
	(4, 'Nowości'),
	(5, 'Piwo');

-- Zrzut struktury tabela alcoholshop.cart
CREATE TABLE IF NOT EXISTS `cart` (
  `CartId` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`CartId`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `cart_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.cart: ~0 rows (około)

-- Zrzut struktury tabela alcoholshop.cartitems
CREATE TABLE IF NOT EXISTS `cartitems` (
  `CartItemId` int NOT NULL AUTO_INCREMENT,
  `CartId` int NOT NULL,
  `ProductId` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`CartItemId`),
  KEY `CartId` (`CartId`),
  KEY `ProductId` (`ProductId`),
  CONSTRAINT `cartitems_ibfk_1` FOREIGN KEY (`CartId`) REFERENCES `cart` (`CartId`) ON DELETE CASCADE,
  CONSTRAINT `cartitems_ibfk_2` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductID`) ON DELETE CASCADE,
  CONSTRAINT `cartitems_chk_1` CHECK ((`Quantity` > 0))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.cartitems: ~0 rows (około)

-- Zrzut struktury tabela alcoholshop.categories
CREATE TABLE IF NOT EXISTS `categories` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`CategoryID`),
  UNIQUE KEY `Indeks 2` (`Name`) USING BTREE,
  UNIQUE KEY `UQ_Categories_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.categories: ~11 rows (około)
INSERT INTO `categories` (`CategoryID`, `Name`) VALUES
	(9, 'Brandy'),
	(10, 'Cydr'),
	(5, 'Gin'),
	(8, 'Likiery'),
	(11, 'Mead (Miód Pitny)'),
	(1, 'Piwo'),
	(6, 'Rum'),
	(7, 'Tequila'),
	(4, 'Whisky'),
	(2, 'Wino'),
	(3, 'Wódka');

-- Zrzut struktury tabela alcoholshop.countries
CREATE TABLE IF NOT EXISTS `countries` (
  `CountryID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`CountryID`),
  UNIQUE KEY `UQ_Countries_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.countries: ~15 rows (około)
INSERT INTO `countries` (`CountryID`, `Name`) VALUES
	(14, 'Australia'),
	(6, 'Czechy'),
	(8, 'Francja'),
	(10, 'Hiszpania'),
	(11, 'Holandia'),
	(3, 'Irlandia'),
	(12, 'Japonia'),
	(13, 'Kanada'),
	(7, 'Meksyk'),
	(5, 'Niemcy'),
	(1, 'Polska'),
	(2, 'Szkocja'),
	(15, 'Szwecja'),
	(4, 'USA'),
	(9, 'Włochy');

-- Zrzut struktury tabela alcoholshop.deliverymethods
CREATE TABLE IF NOT EXISTS `deliverymethods` (
  `DeliveryMethodID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`DeliveryMethodID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.deliverymethods: ~3 rows (około)
INSERT INTO `deliverymethods` (`DeliveryMethodID`, `Name`) VALUES
	(1, 'Kurier'),
	(2, 'Paczkomat'),
	(3, 'Odbiór osobisty');

-- Zrzut struktury tabela alcoholshop.logs
CREATE TABLE IF NOT EXISTS `logs` (
  `LogID` int NOT NULL AUTO_INCREMENT,
  `UserID` int DEFAULT NULL,
  `Action` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`LogID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `logs_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.logs: ~0 rows (około)
INSERT INTO `logs` (`LogID`, `UserID`, `Action`, `Description`, `CreatedAt`) VALUES
	(1, 3, 'LOGIN', 'Admin zalogował się do systemu', '2025-06-29 02:42:32');

-- Zrzut struktury widok alcoholshop.orderdetailsview
-- Tworzenie tymczasowej tabeli, aby przezwyciężyć błędy z zależnościami w WIDOKU
CREATE TABLE `orderdetailsview` (
	`OrderID` INT(10) NOT NULL,
	`Email` VARCHAR(150) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Name` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Address` VARCHAR(300) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Status` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`DeliveryMethod` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`PaymentMethod` VARCHAR(100) NOT NULL COLLATE 'utf8mb4_general_ci',
	`ProductID` INT(10) NOT NULL,
	`ProductName` VARCHAR(200) NOT NULL COLLATE 'utf8mb4_general_ci',
	`Quantity` INT(10) NOT NULL,
	`UnitPrice` DECIMAL(10,2) NOT NULL,
	`TotalPrice` DECIMAL(20,2) NOT NULL
) ENGINE=MyISAM;

-- Zrzut struktury tabela alcoholshop.orderitems
CREATE TABLE IF NOT EXISTS `orderitems` (
  `OrderItemID` int NOT NULL AUTO_INCREMENT,
  `OrderID` int NOT NULL,
  `ProductID` int NOT NULL,
  `Quantity` int NOT NULL,
  `UnitPrice` decimal(10,2) NOT NULL,
  PRIMARY KEY (`OrderItemID`),
  KEY `OrderID` (`OrderID`),
  KEY `ProductID` (`ProductID`),
  CONSTRAINT `orderitems_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`),
  CONSTRAINT `orderitems_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.orderitems: ~3 rows (około)
INSERT INTO `orderitems` (`OrderItemID`, `OrderID`, `ProductID`, `Quantity`, `UnitPrice`) VALUES
	(1, 1, 1, 1, 120.50),
	(2, 2, 2, 6, 10.50),
	(3, 3, 3, 2, 40.00);

-- Zrzut struktury tabela alcoholshop.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `UserID` int DEFAULT NULL,
  `Email` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Address` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `StatusID` int NOT NULL,
  `DeliveryMethodID` int NOT NULL,
  `PaymentMethodID` int NOT NULL,
  `TotalAmount` decimal(10,2) NOT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`OrderID`),
  KEY `UserID` (`UserID`),
  KEY `StatusID` (`StatusID`),
  KEY `DeliveryMethodID` (`DeliveryMethodID`),
  KEY `PaymentMethodID` (`PaymentMethodID`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`),
  CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`StatusID`) REFERENCES `orderstatus` (`StatusID`),
  CONSTRAINT `orders_ibfk_3` FOREIGN KEY (`DeliveryMethodID`) REFERENCES `deliverymethods` (`DeliveryMethodID`),
  CONSTRAINT `orders_ibfk_4` FOREIGN KEY (`PaymentMethodID`) REFERENCES `paymentmethods` (`PaymentMethodID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.orders: ~3 rows (około)
INSERT INTO `orders` (`OrderID`, `UserID`, `Email`, `Name`, `Address`, `StatusID`, `DeliveryMethodID`, `PaymentMethodID`, `TotalAmount`, `CreatedAt`) VALUES
	(1, 1, 'jan.kowalski@example.com', 'Jan Kowalski', 'ul. Mickiewicza 10, Warszawa', 1, 1, 1, 120.50, '2025-06-20 10:15:00'),
	(2, 2, 'anna.nowak@example.com', 'Anna Nowak', 'ul. Piękna 5, Kraków', 2, 2, 2, 240.99, '2025-06-25 14:30:00'),
	(3, NULL, 'gosciexample@example.com', 'Gość Zakupowy', 'ul. Spacerowa 7, Gdańsk', 1, 1, 4, 80.00, '2025-06-26 09:00:00');

-- Zrzut struktury tabela alcoholshop.orderstatus
CREATE TABLE IF NOT EXISTS `orderstatus` (
  `StatusID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`StatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.orderstatus: ~5 rows (około)
INSERT INTO `orderstatus` (`StatusID`, `Name`) VALUES
	(1, 'Nowe'),
	(2, 'W realizacji'),
	(3, 'Wysłane'),
	(4, 'Zrealizowane'),
	(5, 'Anulowane');

-- Zrzut struktury tabela alcoholshop.paymentmethods
CREATE TABLE IF NOT EXISTS `paymentmethods` (
  `PaymentMethodID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`PaymentMethodID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.paymentmethods: ~4 rows (około)
INSERT INTO `paymentmethods` (`PaymentMethodID`, `Name`) VALUES
	(1, 'Przelew'),
	(2, 'Karta'),
	(3, 'PayPal'),
	(4, 'Płatność przy odbiorze');

-- Zrzut struktury tabela alcoholshop.producers
CREATE TABLE IF NOT EXISTS `producers` (
  `ProducerID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`ProducerID`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.producers: ~36 rows (około)
INSERT INTO `producers` (`ProducerID`, `Name`, `Description`) VALUES
	(1, 'Browar Raciborski', 'Tradycyjny polski browar z Raciborza.'),
	(2, 'Jack Daniel\'s', 'Amerykańska destylarnia whisky.'),
	(3, 'Jameson', 'Irlandzka whiskey produkowana w Dublinie.'),
	(4, 'Johnnie Walker', 'Szkocka whisky typu blended.'),
	(5, 'Żubrówka', 'Polska marka wódki z dodatkiem trawy żubrowej.'),
	(6, 'Bacardi', 'Znana marka rumu z Karaibów.'),
	(7, 'Bombay Sapphire', 'Popularny gin z Anglii.'),
	(8, 'Jose Cuervo', 'Meksykańska tequila.'),
	(9, 'Moët & Chandon', 'Francuski producent szampana.'),
	(10, 'Browar Raciborski', 'Tradycyjny polski browar.'),
	(11, 'Browar Żywiec', 'Jeden z największych polskich browarów.'),
	(12, 'Polmos Lublin', 'Producent znanej wódki Żołądkowej Gorzkiej.'),
	(13, 'Belvedere', 'Luksusowa polska wódka.'),
	(14, 'Soplica', 'Znana marka polskich wódek smakowych.'),
	(15, 'Jack Daniel\'s', 'Amerykańska Tennessee whiskey.'),
	(16, 'Jameson', 'Irlandzka whiskey.'),
	(17, 'Johnnie Walker', 'Szkocka whisky typu blended.'),
	(18, 'Glenfiddich', 'Single malt whisky ze Szkocji.'),
	(19, 'Yamazaki', 'Pierwsza japońska whisky single malt.'),
	(20, 'Bombay Sapphire', 'Angielski gin.'),
	(21, 'Tanqueray', 'London Dry Gin z Anglii.'),
	(22, 'Hendrick\'s', 'Gin infuzowany ogórkiem i różą.'),
	(23, 'Bacardi', 'Rum z Karaibów.'),
	(24, 'Captain Morgan', 'Rum z Jamajki.'),
	(25, 'Mount Gay', 'Najstarsza destylarnia rumu na Barbados.'),
	(26, 'Jose Cuervo', 'Meksykańska tequila.'),
	(27, 'Patrón', 'Luksusowa tequila.'),
	(28, 'Moët & Chandon', 'Francuski producent szampana.'),
	(29, 'Dom Pérignon', 'Luksusowy szampan.'),
	(30, 'Antinori', 'Włoski producent win.'),
	(31, 'Campo Viejo', 'Hiszpańska marka win Rioja.'),
	(32, 'Hennessy', 'Francuski koniak.'),
	(33, 'Somersby', 'Cydr duńskiej produkcji.'),
	(34, 'Apis', 'Polski producent miodów pitnych.'),
	(35, 'Baileys', 'Producent likieru Baileys Irish Cream, należący do Diageo.'),
	(36, 'Cointreau', 'Francuski producent słynnego likieru pomarańczowego triple sec.');

-- Zrzut struktury tabela alcoholshop.productionmethods
CREATE TABLE IF NOT EXISTS `productionmethods` (
  `ProductionMethodID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`ProductionMethodID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.productionmethods: ~12 rows (około)
INSERT INTO `productionmethods` (`ProductionMethodID`, `Name`, `Description`) VALUES
	(1, 'Destylacja', 'Proces podgrzewania cieczy i kondensacji pary.'),
	(2, 'Fermentacja', 'Przemiana cukrów w alkohol przy pomocy drożdży.'),
	(3, 'Podwójna destylacja', 'Dwie rundy destylacji dla czystości.'),
	(4, 'Pot still', 'Tradycyjna metoda destylacji w miedzianych alembikach.'),
	(5, 'Column still', 'Destylacja kolumnowa dla większej wydajności.'),
	(6, 'Potrójna destylacja', 'Dodatkowa destylacja dla uzyskania wyjątkowej czystości.'),
	(7, 'Leżakowanie w beczkach po winie', 'Dodatkowy etap dojrzewania.'),
	(8, 'Chmielenie na zimno', 'Dodawanie chmielu po fermentacji dla aromatu.'),
	(9, 'Destylacja próżniowa', 'Zachowuje więcej aromatów w niższej temperaturze.'),
	(10, 'Filtracja przez węgiel brzozowy', 'Stosowana w wódkach dla czystości smaku.'),
	(11, 'Mieszanie alkoholu z kremem', 'Proces produkcji likierów kremowych, polegający na połączeniu alkoholu z mlecznymi składnikami.'),
	(12, 'Destylacja skórek pomarańczy', 'Proces destylacji skórek gorzkich i słodkich pomarańczy w celu uzyskania aromatycznego likieru.');

-- Zrzut struktury tabela alcoholshop.products
CREATE TABLE IF NOT EXISTS `products` (
  `ProductID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `VolumeML` int NOT NULL,
  `AlcoholPercentage` decimal(4,2) NOT NULL,
  `Year` int DEFAULT NULL,
  `AgingDuration` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Availability` tinyint(1) DEFAULT '1',
  `CategoryID` int DEFAULT NULL,
  `ProducerID` int DEFAULT NULL,
  `CountryID` int DEFAULT NULL,
  `ProductionMethodID` int DEFAULT NULL,
  `AgingID` int DEFAULT NULL,
  `ImageURL` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ProductID`),
  UNIQUE KEY `UQ_Products_Name` (`Name`),
  KEY `CategoryID` (`CategoryID`),
  KEY `ProducerID` (`ProducerID`),
  KEY `CountryID` (`CountryID`),
  KEY `ProductionMethodID` (`ProductionMethodID`),
  KEY `AgingID` (`AgingID`),
  CONSTRAINT `products_ibfk_1` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`),
  CONSTRAINT `products_ibfk_2` FOREIGN KEY (`ProducerID`) REFERENCES `producers` (`ProducerID`),
  CONSTRAINT `products_ibfk_3` FOREIGN KEY (`CountryID`) REFERENCES `countries` (`CountryID`),
  CONSTRAINT `products_ibfk_4` FOREIGN KEY (`ProductionMethodID`) REFERENCES `productionmethods` (`ProductionMethodID`),
  CONSTRAINT `products_ibfk_5` FOREIGN KEY (`AgingID`) REFERENCES `aging` (`AgingID`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.products: ~28 rows (około)
INSERT INTO `products` (`ProductID`, `Name`, `Description`, `VolumeML`, `AlcoholPercentage`, `Year`, `AgingDuration`, `Price`, `Availability`, `CategoryID`, `ProducerID`, `CountryID`, `ProductionMethodID`, `AgingID`, `ImageURL`, `CreatedAt`) VALUES
	(1, 'Piwo Raciborskie Klasyczne', 'Tradycyjne piwo jasne.', 500, 5.00, 2024, NULL, 6.50, 1, 1, 1, 1, 2, 5, NULL, '2025-06-28 23:08:15'),
	(2, 'Piwo Żywiec Jasne Pełne', 'Klasyczne piwo typu lager.', 500, 5.60, 2024, NULL, 5.90, 1, 1, 2, 1, 2, 5, NULL, '2025-06-28 23:08:15'),
	(3, 'Piwo Raciborskie Twierdzowe', 'Mocniejsze piwo jasne.', 500, 6.20, 2024, NULL, 7.50, 1, 1, 1, 1, 2, 5, NULL, '2025-06-28 23:08:15'),
	(4, 'Piwo Raciborskie Miodowe', 'Piwo z dodatkiem miodu.', 500, 5.60, 2024, NULL, 7.00, 1, 1, 1, 1, 2, 5, NULL, '2025-06-28 23:08:15'),
	(5, 'Somersby Apple Cider', 'Orzeźwiający cydr jabłkowy.', 500, 4.50, 2024, NULL, 6.00, 1, 10, 26, 5, 2, 5, NULL, '2025-06-28 23:08:15'),
	(6, 'Jack Daniel\'s Old No. 7', 'Tennessee whiskey.', 700, 40.00, 2022, '4 lata', 120.00, 1, 4, 6, 4, 1, 3, NULL, '2025-06-28 23:08:15'),
	(7, 'Jameson Irish Whiskey', 'Delikatna irlandzka whiskey.', 700, 40.00, 2022, '3 lata', 100.00, 1, 4, 7, 3, 4, 1, NULL, '2025-06-28 23:08:15'),
	(8, 'Johnnie Walker Black Label', '12-letnia blended whisky.', 700, 40.00, 2010, '12 lat', 150.00, 1, 4, 8, 2, 4, 1, NULL, '2025-06-28 23:08:15'),
	(9, 'Glenfiddich 15', 'Single malt whisky.', 700, 40.00, 2008, '15 lat', 200.00, 1, 4, 9, 2, 4, 2, NULL, '2025-06-28 23:08:15'),
	(10, 'Yamazaki 12', 'Japońska whisky single malt.', 700, 43.00, 2012, '12 lat', 500.00, 1, 4, 10, 11, 4, 2, NULL, '2025-06-28 23:08:15'),
	(11, 'Bombay Sapphire', 'Angielski gin.', 700, 40.00, 2022, NULL, 95.00, 1, 5, 11, 5, 1, 5, NULL, '2025-06-28 23:08:15'),
	(12, 'Tanqueray', 'London Dry Gin.', 700, 43.10, 2022, NULL, 110.00, 1, 5, 12, 5, 1, 5, NULL, '2025-06-28 23:08:15'),
	(13, 'Hendrick\'s', 'Gin z ogórkiem i różą.', 700, 41.40, 2022, NULL, 125.00, 1, 5, 13, 5, 1, 5, NULL, '2025-06-28 23:08:15'),
	(14, 'Bacardi Carta Blanca', 'Lekki biały rum.', 700, 37.50, 2022, NULL, 85.00, 1, 6, 14, 4, 1, 5, NULL, '2025-06-28 23:08:15'),
	(15, 'Captain Morgan Spiced Gold', 'Rum korzenny.', 700, 35.00, 2022, NULL, 90.00, 1, 6, 15, 4, 1, 5, NULL, '2025-06-28 23:08:15'),
	(16, 'Mount Gay Eclipse', 'Rum z Barbados.', 700, 40.00, 2022, NULL, 105.00, 1, 6, 16, 4, 1, 5, NULL, '2025-06-28 23:08:15'),
	(17, 'Jose Cuervo Especial', 'Tequila reposado.', 700, 38.00, 2022, NULL, 110.00, 1, 7, 17, 7, 1, 5, NULL, '2025-06-28 23:08:15'),
	(18, 'Patrón Silver', 'Tequila premium.', 700, 40.00, 2022, NULL, 300.00, 1, 7, 18, 7, 1, 5, NULL, '2025-06-28 23:08:15'),
	(19, 'Moët & Chandon Imperial', 'Francuski szampan.', 750, 12.00, 2022, '3 lata', 250.00, 1, 2, 19, 8, 2, 5, NULL, '2025-06-28 23:08:15'),
	(20, 'Dom Pérignon Vintage 2013', 'Ekskluzywny szampan.', 750, 12.50, 2013, '7 lat', 600.00, 1, 2, 20, 8, 2, 5, NULL, '2025-06-28 23:08:15'),
	(21, 'Antinori Tignanello', 'Czerwone wino z Toskanii.', 750, 13.50, 2020, '2 lata', 450.00, 1, 2, 21, 9, 2, 5, NULL, '2025-06-28 23:08:15'),
	(22, 'Campo Viejo Reserva', 'Hiszpańskie Rioja.', 750, 13.50, 2018, '3 lata', 70.00, 1, 2, 22, 10, 2, 5, NULL, '2025-06-28 23:08:15'),
	(23, 'Hennessy VS', 'Francuski koniak.', 700, 40.00, 2020, '2 lata', 250.00, 1, 9, 23, 8, 1, 1, NULL, '2025-06-28 23:08:15'),
	(24, 'Apis Dwójniak', 'Polski miód pitny półsłodki.', 750, 16.00, 2022, '1 rok', 50.00, 1, 11, 25, 1, 2, 5, NULL, '2025-06-28 23:08:15'),
	(25, 'Żubrówka Biała', 'Wódka czysta inspirowana Puszczą Białowieską. Delikatna i łagodna w smaku.', 500, 40.00, 2025, NULL, 44.99, 1, 3, NULL, 1, NULL, NULL, 'https://example.com/images/zubrowka_biala.jpg', '2025-06-29 02:31:05'),
	(26, 'Stock Prestige', 'Wysokiej jakości wódka klasy premium, destylowana z wyselekcjonowanych zbóż.', 700, 40.00, 2025, NULL, 64.99, 1, 3, NULL, 1, NULL, NULL, 'https://example.com/images/stock_prestige.jpg', '2025-06-29 02:31:05'),
	(29, 'Baileys Irish Cream', 'Likier na bazie irlandzkiej whisky i śmietanki. Kremowy, słodki i aksamitny w smaku.', 700, 17.00, 2025, NULL, 69.99, 1, 8, NULL, 3, NULL, NULL, 'https://example.com/images/baileys.jpg', '2025-06-29 02:35:20'),
	(30, 'Cointreau', 'Francuski likier pomarańczowy typu triple sec. Aromatyczny, z wyczuwalnym skórką pomarańczy.', 700, 40.00, 2024, NULL, 89.99, 1, 8, NULL, 8, NULL, NULL, 'https://example.com/images/cointreau.jpg', '2025-06-29 02:35:20');

-- Zrzut struktury tabela alcoholshop.reviews
CREATE TABLE IF NOT EXISTS `reviews` (
  `ReviewID` int NOT NULL AUTO_INCREMENT,
  `ProductID` int NOT NULL,
  `UserID` int NOT NULL,
  `Rating` int DEFAULT NULL,
  `Comment` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ReviewID`),
  KEY `ProductID` (`ProductID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `reviews_ibfk_1` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`),
  CONSTRAINT `reviews_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`),
  CONSTRAINT `reviews_chk_1` CHECK ((`Rating` between 1 and 5))
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.reviews: ~8 rows (około)
INSERT INTO `reviews` (`ReviewID`, `ProductID`, `UserID`, `Rating`, `Comment`, `CreatedAt`) VALUES
	(1, 1, 2, 5, 'Rewelacyjne piwo, smak nie do podrobienia!', '2025-06-29 02:25:30'),
	(2, 6, 2, 4, 'Dobra whisky, choć trochę zbyt dymna dla mnie.', '2025-06-29 02:25:30'),
	(3, 13, 2, 3, 'Gin poprawny, ale piłem lepsze.', '2025-06-29 02:25:30'),
	(4, 21, 1, 5, 'Fantastyczne wino, idealne do kolacji.', '2025-06-29 02:25:30'),
	(5, 25, 1, 2, 'Wódka nie spełniła moich oczekiwań.', '2025-06-29 02:25:30'),
	(6, 3, 2, 4, 'Twierdzowe piwo ma świetny aromat.', '2025-06-29 02:25:30'),
	(7, 16, 2, 5, 'Rum bardzo aromatyczny, polecam do koktajli.', '2025-06-29 02:25:30'),
	(8, 30, 1, 4, 'Ciekawy likier, bardzo słodki.', '2025-06-29 02:25:30');

-- Zrzut struktury tabela alcoholshop.users
CREATE TABLE IF NOT EXISTS `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `PasswordHash` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Role` enum('Admin','Client') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Zrzucanie danych dla tabeli alcoholshop.users: ~3 rows (około)
INSERT INTO `users` (`UserID`, `Email`, `PasswordHash`, `FirstName`, `LastName`, `Role`, `CreatedAt`) VALUES
	(1, 'jan.kowalski@example.com', 'hashed_password_1', 'Jan', 'Kowalski', 'Client', '2025-06-28 23:29:24'),
	(2, 'anna.nowak@example.com', 'hashed_password_2', 'Anna', 'Nowak', 'Client', '2025-06-28 23:29:24'),
	(3, 'admin@example.com', 'hashed_password_admin', 'Admin', 'Adminowski', 'Admin', '2025-06-29 02:42:31');

-- Zrzut struktury widok alcoholshop.activeproductsview
-- Usuwanie tabeli tymczasowej i tworzenie ostatecznej struktury WIDOKU
DROP TABLE IF EXISTS `activeproductsview`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `activeproductsview` AS select `p`.`ProductID` AS `ProductID`,`p`.`Name` AS `Name`,`p`.`Description` AS `Description`,`p`.`Price` AS `Price`,`p`.`VolumeML` AS `VolumeML`,`p`.`AlcoholPercentage` AS `AlcoholPercentage`,`p`.`Availability` AS `Availability`,`c`.`Name` AS `Category`,`pr`.`Name` AS `Producer`,`co`.`Name` AS `Country` from (((`products` `p` left join `categories` `c` on((`p`.`CategoryID` = `c`.`CategoryID`))) left join `producers` `pr` on((`p`.`ProducerID` = `pr`.`ProducerID`))) left join `countries` `co` on((`p`.`CountryID` = `co`.`CountryID`))) where (`p`.`Availability` = true);

-- Zrzut struktury widok alcoholshop.orderdetailsview
-- Usuwanie tabeli tymczasowej i tworzenie ostatecznej struktury WIDOKU
DROP TABLE IF EXISTS `orderdetailsview`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `orderdetailsview` AS select `o`.`OrderID` AS `OrderID`,`o`.`Email` AS `Email`,`o`.`Name` AS `Name`,`o`.`Address` AS `Address`,`os`.`Name` AS `Status`,`dm`.`Name` AS `DeliveryMethod`,`pm`.`Name` AS `PaymentMethod`,`oi`.`ProductID` AS `ProductID`,`p`.`Name` AS `ProductName`,`oi`.`Quantity` AS `Quantity`,`oi`.`UnitPrice` AS `UnitPrice`,(`oi`.`Quantity` * `oi`.`UnitPrice`) AS `TotalPrice` from (((((`orders` `o` join `orderitems` `oi` on((`o`.`OrderID` = `oi`.`OrderID`))) join `products` `p` on((`oi`.`ProductID` = `p`.`ProductID`))) join `orderstatus` `os` on((`o`.`StatusID` = `os`.`StatusID`))) join `deliverymethods` `dm` on((`o`.`DeliveryMethodID` = `dm`.`DeliveryMethodID`))) join `paymentmethods` `pm` on((`o`.`PaymentMethodID` = `pm`.`PaymentMethodID`)));

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
