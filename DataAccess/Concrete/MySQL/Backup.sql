-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 19 Eyl 2020, 13:31:07
-- Sunucu sürümü: 10.4.11-MariaDB
-- PHP Sürümü: 7.4.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `otopark`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `brands`
--

CREATE TABLE IF NOT EXISTS `brands` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `campaigns`
--

CREATE TABLE IF NOT EXISTS `campaigns` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParkPlaceId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `PricePerMin` decimal(5,3) NOT NULL,
  `StartingDate` date NOT NULL,
  `FinishingDate` date NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `ParkPlaceId` (`ParkPlaceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `customers`
--

CREATE TABLE IF NOT EXISTS `customers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PersonId` int(11) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `PersonId` (`PersonId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `employees`
--

CREATE TABLE IF NOT EXISTS `employees` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PersonId` int(11) NOT NULL,
  `UserName` varchar(25) NOT NULL,
  `Password` varchar(25) NOT NULL,
  `LastLoginDateTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `PersonId` (`PersonId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `employees`
--

INSERT INTO `employees` (`Id`, `PersonId`, `UserName`, `Password`, `LastLoginDateTime`, `IsDeleted`) VALUES
(1, 1, 'admin', '123', '2020-09-19 14:17:41', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `models`
--

CREATE TABLE IF NOT EXISTS `models` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BrandId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsDeleted` tinyint(4) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `BrandId` (`BrandId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `parkhistories`
--

CREATE TABLE IF NOT EXISTS `parkhistories` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParkPlaceId` int(11) NOT NULL,
  `CampaignId` int(11) DEFAULT NULL,
  `CustomerId` int(11) DEFAULT NULL,
  `VehicleId` int(11) NOT NULL,
  `EmployeeId` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `ParkingStartDateTime` datetime NOT NULL,
  `ParkedLeaveDateTime` datetime DEFAULT NULL,
  `CreationDateTime` datetime NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `ParkPlaceId` (`ParkPlaceId`,`CampaignId`,`CustomerId`,`VehicleId`,`EmployeeId`),
  KEY `CampaignId` (`CampaignId`),
  KEY `CustomerId` (`CustomerId`),
  KEY `VehicleId` (`VehicleId`),
  KEY `EmployeeId` (`EmployeeId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `parkplaces`
--

CREATE TABLE IF NOT EXISTS `parkplaces` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `X` int(11) NOT NULL,
  `Y` int(11) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `Name` varchar(25) DEFAULT NULL,
  `CreationDate` date NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `parks`
--

CREATE TABLE IF NOT EXISTS `parks` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParkPlaceId` int(11) NOT NULL,
  `CampaignId` int(11) DEFAULT NULL,
  `CustomerId` int(11) DEFAULT NULL,
  `VehicleId` int(11) NOT NULL,
  `EmployeeId` int(11) NOT NULL,
  `ParkingStartDateTime` datetime NOT NULL,
  `CreationDateTime` datetime NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `ParkPlaceId` (`ParkPlaceId`,`CampaignId`,`CustomerId`,`VehicleId`,`EmployeeId`),
  KEY `CampaignId` (`CampaignId`),
  KEY `CustomerId` (`CustomerId`),
  KEY `VehicleId` (`VehicleId`),
  KEY `EmployeeId` (`EmployeeId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `persons`
--

CREATE TABLE IF NOT EXISTS `persons` (
  `Id` int(4) NOT NULL AUTO_INCREMENT,
  `NationalIdentityNumber` varchar(20) NOT NULL,
  `FirstName` varchar(25) NOT NULL,
  `LastName` varchar(25) NOT NULL,
  `TelephoneNumber` varchar(11) NOT NULL,
  `CreationDate` date NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `persons`
--

INSERT INTO `persons` (`Id`, `NationalIdentityNumber`, `FirstName`, `LastName`, `TelephoneNumber`, `CreationDate`) VALUES
(1, '11111111111', 'Varsayılan', 'Varsayılan', '5050000000', '2020-04-02');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `vehicleprices`
--

CREATE TABLE IF NOT EXISTS `vehicleprices` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `VehicleTypeId` int(11) NOT NULL,
  `Hour` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `VehicleTypeId` (`VehicleTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `vehicles`
--

CREATE TABLE IF NOT EXISTS `vehicles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `VehicleTypeId` int(11) NOT NULL,
  `EmployeeId` int(11) NOT NULL,
  `BrandId` int(11) DEFAULT NULL,
  `ModelId` int(11) DEFAULT NULL,
  `Plate` varchar(11) NOT NULL,
  `Color` varchar(25) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `VehicleTypeId` (`VehicleTypeId`,`EmployeeId`),
  KEY `EmployeeId` (`EmployeeId`),
  KEY `BrandId` (`BrandId`,`ModelId`),
  KEY `vehicles_ibfk_4` (`ModelId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `vehicletypes`
--

CREATE TABLE IF NOT EXISTS `vehicletypes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

--
-- Dökümü yapılmış tablolar için kısıtlamalar
--

--
-- Tablo kısıtlamaları `campaigns`
--
ALTER TABLE `campaigns`
  ADD CONSTRAINT `campaigns_ibfk_1` FOREIGN KEY (`ParkPlaceId`) REFERENCES `parkplaces` (`Id`);

--
-- Tablo kısıtlamaları `customers`
--
ALTER TABLE `customers`
  ADD CONSTRAINT `customers_ibfk_1` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`);

--
-- Tablo kısıtlamaları `employees`
--
ALTER TABLE `employees`
  ADD CONSTRAINT `employees_ibfk_1` FOREIGN KEY (`PersonId`) REFERENCES `persons` (`Id`);

--
-- Tablo kısıtlamaları `models`
--
ALTER TABLE `models`
  ADD CONSTRAINT `models_ibfk_1` FOREIGN KEY (`BrandId`) REFERENCES `brands` (`Id`);

--
-- Tablo kısıtlamaları `parkhistories`
--
ALTER TABLE `parkhistories`
  ADD CONSTRAINT `parkhistories_ibfk_1` FOREIGN KEY (`ParkPlaceId`) REFERENCES `parkplaces` (`Id`),
  ADD CONSTRAINT `parkhistories_ibfk_2` FOREIGN KEY (`CampaignId`) REFERENCES `campaigns` (`Id`),
  ADD CONSTRAINT `parkhistories_ibfk_3` FOREIGN KEY (`CustomerId`) REFERENCES `customers` (`Id`),
  ADD CONSTRAINT `parkhistories_ibfk_4` FOREIGN KEY (`VehicleId`) REFERENCES `vehicles` (`Id`),
  ADD CONSTRAINT `parkhistories_ibfk_5` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`Id`);

--
-- Tablo kısıtlamaları `parks`
--
ALTER TABLE `parks`
  ADD CONSTRAINT `parks_ibfk_1` FOREIGN KEY (`ParkPlaceId`) REFERENCES `parkplaces` (`Id`),
  ADD CONSTRAINT `parks_ibfk_2` FOREIGN KEY (`CampaignId`) REFERENCES `campaigns` (`Id`),
  ADD CONSTRAINT `parks_ibfk_3` FOREIGN KEY (`CustomerId`) REFERENCES `customers` (`Id`),
  ADD CONSTRAINT `parks_ibfk_4` FOREIGN KEY (`VehicleId`) REFERENCES `vehicles` (`Id`),
  ADD CONSTRAINT `parks_ibfk_5` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`Id`);

--
-- Tablo kısıtlamaları `vehicleprices`
--
ALTER TABLE `vehicleprices`
  ADD CONSTRAINT `vehicleprices_ibfk_1` FOREIGN KEY (`VehicleTypeId`) REFERENCES `vehicletypes` (`Id`);

--
-- Tablo kısıtlamaları `vehicles`
--
ALTER TABLE `vehicles`
  ADD CONSTRAINT `vehicles_ibfk_1` FOREIGN KEY (`VehicleTypeId`) REFERENCES `vehicletypes` (`Id`),
  ADD CONSTRAINT `vehicles_ibfk_2` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`Id`),
  ADD CONSTRAINT `vehicles_ibfk_3` FOREIGN KEY (`BrandId`) REFERENCES `brands` (`Id`),
  ADD CONSTRAINT `vehicles_ibfk_4` FOREIGN KEY (`ModelId`) REFERENCES `models` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
