-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 16 Eyl 2020, 12:15:47
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

CREATE TABLE `brands` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `brands`
--

INSERT INTO `brands` (`Id`, `Name`, `IsDeleted`) VALUES
(1, 'TOFAŞ', 0),
(2, 'BMW', 0),
(3, 'AUDİ', 0),
(4, 'HONDA', 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `campaigns`
--

CREATE TABLE `campaigns` (
  `Id` int(11) NOT NULL,
  `ParkPlaceId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `PricePerMin` decimal(5,3) NOT NULL,
  `StartingDate` date NOT NULL,
  `FinishingDate` date NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `customers`
--

CREATE TABLE `customers` (
  `Id` int(11) NOT NULL,
  `PersonId` int(11) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `customers`
--

INSERT INTO `customers` (`Id`, `PersonId`, `IsDeleted`) VALUES
(4, 5, 1),
(5, 6, 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `employees`
--

CREATE TABLE `employees` (
  `Id` int(11) NOT NULL,
  `PersonId` int(11) NOT NULL,
  `UserName` varchar(25) NOT NULL,
  `Password` varchar(25) NOT NULL,
  `LastLoginDateTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `employees`
--

INSERT INTO `employees` (`Id`, `PersonId`, `UserName`, `Password`, `LastLoginDateTime`, `IsDeleted`) VALUES
(1, 1, 'admin', '123', '2020-09-16 13:14:56', 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `models`
--

CREATE TABLE `models` (
  `Id` int(11) NOT NULL,
  `BrandId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsDeleted` tinyint(4) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `models`
--

INSERT INTO `models` (`Id`, `BrandId`, `Name`, `IsDeleted`) VALUES
(1, 3, 'A3', 1),
(2, 1, 'ŞAHİN', 1),
(3, 1, 'DOĞAN', 1),
(4, 1, 'KARTAL', 1),
(5, 2, 'E30', 1),
(6, 2, 'E36', 1),
(7, 2, 'E21', 1),
(9, 2, 'E46', 1),
(10, 2, 'E90', 1),
(11, 3, 'A5', 1),
(12, 4, 'CIVIC', 0),
(13, 4, 'CRV', 0),
(14, 1, 'ŞAHİN', 0),
(15, 1, 'DOĞAN', 0),
(16, 1, 'SERÇE', 0),
(17, 2, 'E36', 0),
(18, 2, 'E39', 0),
(19, 2, 'E30', 0),
(20, 2, 'E46', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `parkhistories`
--

CREATE TABLE `parkhistories` (
  `Id` int(11) NOT NULL,
  `ParkPlaceId` int(11) NOT NULL,
  `CampaignId` int(11) DEFAULT NULL,
  `CustomerId` int(11) DEFAULT NULL,
  `VehicleId` int(11) NOT NULL,
  `EmployeeId` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `ParkingStartDateTime` datetime NOT NULL,
  `ParkedLeaveDateTime` datetime DEFAULT NULL,
  `CreationDateTime` datetime NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `parkhistories`
--

INSERT INTO `parkhistories` (`Id`, `ParkPlaceId`, `CampaignId`, `CustomerId`, `VehicleId`, `EmployeeId`, `Price`, `ParkingStartDateTime`, `ParkedLeaveDateTime`, `CreationDateTime`, `IsDeleted`) VALUES
(1, 46, NULL, 4, 11, 1, '20.00', '2020-09-08 22:57:00', '2020-09-08 22:57:58', '2020-09-08 22:57:55', 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `parkplaces`
--

CREATE TABLE `parkplaces` (
  `Id` int(11) NOT NULL,
  `X` int(11) NOT NULL,
  `Y` int(11) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `Name` varchar(25) DEFAULT NULL,
  `CreationDate` date NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `parkplaces`
--

INSERT INTO `parkplaces` (`Id`, `X`, `Y`, `Width`, `Height`, `Name`, `CreationDate`, `IsDeleted`) VALUES
(46, 567, 24, 174, 244, 'Araç Alanı ', '2020-09-08', 1),
(47, 568, 478, 174, 244, 'Araç Alanı 2', '2020-09-08', 1),
(48, 93, 18, 188, 246, 'Park Alanı: 1', '2020-09-13', 1),
(49, 328, 20, 188, 246, 'Park Alanı: 2', '2020-09-13', 1),
(50, 561, 22, 187, 242, 'Park Alanı: 3', '2020-09-13', 1),
(51, 790, 28, 187, 242, 'Park Alanı: 4', '2020-09-13', 1),
(52, 1032, 30, 188, 240, 'Park Alanı: 5', '2020-09-13', 1),
(53, 1261, 32, 188, 240, 'Park Alanı: 6', '2020-09-13', 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `parks`
--

CREATE TABLE `parks` (
  `Id` int(11) NOT NULL,
  `ParkPlaceId` int(11) NOT NULL,
  `CampaignId` int(11) DEFAULT NULL,
  `CustomerId` int(11) DEFAULT NULL,
  `VehicleId` int(11) NOT NULL,
  `EmployeeId` int(11) NOT NULL,
  `ParkingStartDateTime` datetime NOT NULL,
  `CreationDateTime` datetime NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `persons`
--

CREATE TABLE `persons` (
  `Id` int(4) NOT NULL,
  `NationalIdentityNumber` varchar(20) NOT NULL,
  `FirstName` varchar(25) NOT NULL,
  `LastName` varchar(25) NOT NULL,
  `TelephoneNumber` varchar(11) NOT NULL,
  `CreationDate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `persons`
--

INSERT INTO `persons` (`Id`, `NationalIdentityNumber`, `FirstName`, `LastName`, `TelephoneNumber`, `CreationDate`) VALUES
(1, '11111111111', 'Varsayılan', 'Varsayılan', '5050000000', '2020-04-02'),
(5, '22222222222', 'Fatih', 'ATEŞ', '5050001111', '2020-05-04'),
(6, '12332112311', 'CEYHUN', 'SALLAMA', '555555555', '2020-09-16');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `vehicleprices`
--

CREATE TABLE `vehicleprices` (
  `Id` int(11) NOT NULL,
  `VehicleTypeId` int(11) NOT NULL,
  `Hour` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `vehicleprices`
--

INSERT INTO `vehicleprices` (`Id`, `VehicleTypeId`, `Hour`, `Price`, `IsDeleted`) VALUES
(32, 3, 1, '20.00', 0),
(33, 3, 2, '30.00', 1),
(34, 3, 20, '90.00', 1),
(35, 1, 60, '10.00', 1),
(36, 1, 120, '15.00', 1),
(37, 5, 200, '50.00', 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `vehicles`
--

CREATE TABLE `vehicles` (
  `Id` int(11) NOT NULL,
  `VehicleTypeId` int(11) NOT NULL,
  `EmployeeId` int(11) NOT NULL,
  `BrandId` int(11) DEFAULT NULL,
  `ModelId` int(11) DEFAULT NULL,
  `Plate` varchar(11) NOT NULL,
  `Color` varchar(25) DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `vehicles`
--

INSERT INTO `vehicles` (`Id`, `VehicleTypeId`, `EmployeeId`, `BrandId`, `ModelId`, `Plate`, `Color`, `IsDeleted`) VALUES
(11, 3, 1, 2, 6, '07DNM123', 'BEYAZ', 1),
(12, 3, 1, 3, 1, '07KZP34', 'BEYAZ', 1),
(13, 3, 1, 1, 15, '07TKD4', 'BORDO', 0),
(14, 1, 1, 2, 7, '07KJK39', 'MAVİ', 1),
(15, 1, 1, 2, 17, '08KK55', 'BEYAZ', 0),
(16, 2, 1, 4, 13, '123DNM123', 'SİYAH', 0);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `vehicletypes`
--

CREATE TABLE `vehicletypes` (
  `Id` int(11) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `vehicletypes`
--

INSERT INTO `vehicletypes` (`Id`, `Name`, `IsDeleted`) VALUES
(1, 'Motorsiklet', 0),
(2, 'Kamyonet', 0),
(3, 'Otomobil', 0),
(5, 'Ticari Otomobil', 0);

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `brands`
--
ALTER TABLE `brands`
  ADD PRIMARY KEY (`Id`);

--
-- Tablo için indeksler `campaigns`
--
ALTER TABLE `campaigns`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ParkPlaceId` (`ParkPlaceId`);

--
-- Tablo için indeksler `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `PersonId` (`PersonId`);

--
-- Tablo için indeksler `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `PersonId` (`PersonId`);

--
-- Tablo için indeksler `models`
--
ALTER TABLE `models`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `BrandId` (`BrandId`);

--
-- Tablo için indeksler `parkhistories`
--
ALTER TABLE `parkhistories`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ParkPlaceId` (`ParkPlaceId`,`CampaignId`,`CustomerId`,`VehicleId`,`EmployeeId`),
  ADD KEY `CampaignId` (`CampaignId`),
  ADD KEY `CustomerId` (`CustomerId`),
  ADD KEY `VehicleId` (`VehicleId`),
  ADD KEY `EmployeeId` (`EmployeeId`);

--
-- Tablo için indeksler `parkplaces`
--
ALTER TABLE `parkplaces`
  ADD PRIMARY KEY (`Id`);

--
-- Tablo için indeksler `parks`
--
ALTER TABLE `parks`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ParkPlaceId` (`ParkPlaceId`,`CampaignId`,`CustomerId`,`VehicleId`,`EmployeeId`),
  ADD KEY `CampaignId` (`CampaignId`),
  ADD KEY `CustomerId` (`CustomerId`),
  ADD KEY `VehicleId` (`VehicleId`),
  ADD KEY `EmployeeId` (`EmployeeId`);

--
-- Tablo için indeksler `persons`
--
ALTER TABLE `persons`
  ADD PRIMARY KEY (`Id`);

--
-- Tablo için indeksler `vehicleprices`
--
ALTER TABLE `vehicleprices`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `VehicleTypeId` (`VehicleTypeId`);

--
-- Tablo için indeksler `vehicles`
--
ALTER TABLE `vehicles`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `VehicleTypeId` (`VehicleTypeId`,`EmployeeId`),
  ADD KEY `EmployeeId` (`EmployeeId`),
  ADD KEY `BrandId` (`BrandId`,`ModelId`),
  ADD KEY `vehicles_ibfk_4` (`ModelId`);

--
-- Tablo için indeksler `vehicletypes`
--
ALTER TABLE `vehicletypes`
  ADD PRIMARY KEY (`Id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `brands`
--
ALTER TABLE `brands`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Tablo için AUTO_INCREMENT değeri `campaigns`
--
ALTER TABLE `campaigns`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Tablo için AUTO_INCREMENT değeri `customers`
--
ALTER TABLE `customers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Tablo için AUTO_INCREMENT değeri `employees`
--
ALTER TABLE `employees`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Tablo için AUTO_INCREMENT değeri `models`
--
ALTER TABLE `models`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Tablo için AUTO_INCREMENT değeri `parkhistories`
--
ALTER TABLE `parkhistories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Tablo için AUTO_INCREMENT değeri `parkplaces`
--
ALTER TABLE `parkplaces`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=54;

--
-- Tablo için AUTO_INCREMENT değeri `parks`
--
ALTER TABLE `parks`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Tablo için AUTO_INCREMENT değeri `persons`
--
ALTER TABLE `persons`
  MODIFY `Id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Tablo için AUTO_INCREMENT değeri `vehicleprices`
--
ALTER TABLE `vehicleprices`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- Tablo için AUTO_INCREMENT değeri `vehicles`
--
ALTER TABLE `vehicles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- Tablo için AUTO_INCREMENT değeri `vehicletypes`
--
ALTER TABLE `vehicletypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

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
