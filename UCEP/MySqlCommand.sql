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