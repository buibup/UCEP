CREATE TABLE FsCatalogues(
	`Id` int AUTO_INCREMENT NOT NULL,
	`HospitalCode` nvarchar(20) NOT NULL,
	`FSCodeNIEMS` nvarchar(50) NOT NULL,
	`FSCodeHos` nvarchar(50) NOT NULL,
	`Category` nvarchar(10) NOT NULL,
	`Meaning` Longtext NOT NULL,
	`Unit` nvarchar(50) NOT NULL,
	`Price` decimal(18, 2) NOT NULL,
	`EffectiveDate` Datetime(6) NOT NULL,
	`Status` nvarchar(50) NOT NULL,
	`ApprovalDate` Datetime(6) NOT NULL,
 CONSTRAINT `PK_dbo.FsCatalogues` PRIMARY KEY 
(
	`Id` ASC
) 
); 
CREATE TABLE `DrugCatalogues` (
  `HOSPDRUGCODE` varchar(50) NOT NULL,
  `PRODUCTCAT` int(11) DEFAULT NULL,
  `TMTID` varchar(50) DEFAULT NULL,
  `SPECPREP` text,
  `GENERICNAME` text,
  `TRADENAME` text,
  `DFSCODE` text,
  `DOSAGEFORM` text,
  `STRENGTH` text,
  `CONTENT` text,
  `UNITPRICE` double DEFAULT NULL,
  `DISTRIBUTOR` text,
  `MANUFACTURER` text,
  `ISED` varchar(20) DEFAULT NULL,
  `NDC24` text,
  `PACKSIZE` text,
  `PACKPRICE` text,
  `UPDATEFLAG` varchar(10) DEFAULT NULL,
  `DATECHANGE` datetime DEFAULT NULL,
  `DATEUPDATE` datetime DEFAULT NULL,
  `DATEEFFECTIVE` datetime DEFAULT NULL,
  `ISED_APPROVED` varchar(20) DEFAULT NULL,
  `NDC24_APPROVED` text,
  `DATE_APPROVED` datetime DEFAULT NULL,
  `ISED_STATUS` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`﻿HOSPDRUGCODE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
