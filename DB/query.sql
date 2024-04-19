use DBEB;

   
   ---
SELECT * FROM Owner o ;

INSERT INTO Owner  (Pwd)
VALUES
   	('1234');
   
SELECT * FROM  ServiceTypes st 
   
SELECT * FROM  Vehicles v 
   
SELECT * from ServiceDetalles sd 

SELECT * FROM Owner ; 

SELECT * FROM ServiceTypes st ;
   
   
SELECT * FROM  Countries  ;

SELECT * FROM  Departments d  ;

SELECT * FROM  Municipalities m2   ;

SELECT  * FROM ServiceTypes st 

                new ServiceType {Id = 1,Type = "Motor" },
                new ServiceType {Id = 2,Type = "Frenos" },
                new ServiceType { Id = 3, Type = "Hidraulico" },
                new ServiceType { Id = 4, Type = "Carroceria" },
            
UPDATE ServiceTypes 
SET Type = 'Frenos'
WHERE Id  = 2;

INSERT INTO ServiceTypes  (Type)
VALUES
   ('Hidraulico'),
   ('Carroceria');


SELECT * FROM TypeClients tc 


SELECT * FROM Clients c ;

UPDATE Clients  
SET Name = 'Juan'
WHERE Id  = 1;



SELECT * FROM TypeClients tc ;

SELECT * FROM Addresses a ;

SELECT * FROM VehicleBrands vb ;

SELECT * FROM VehicleModels vm ;

SELECT * FROM VehicleLineas vl ;

SELECT * FROM ServiceDetalles sd ;

UPDATE ServiceDetalles 
SET State  = 3
WHERE Id  = 1;


SELECT * FROM ServiceTypes st 

SELECT * FROM  ServiceWorks sw 

SELECT * FROM VehicleParts vp 

SELECT * FROM Vehicles v ;

INSERT INTO Vehicles (Placa, VehicleLineaId)
VALUES
	('789ABC',3);

UPDATE Vehicles 
SET Placa = '987PCD', VehicleLineaId = 1
WHERE Id  = 2;



select * from VehicleParts vp 


SELECT * FROM Logs l ;

SELECT * from ServiceTypes st 

SELECT  * from ServiceWorks sw 
---
INSERT INTO ServiceWorks  (work,priceWork)
VALUES
   ( 'Servicio por flotilla',10000010);




SELECT * FROM ServiceListWorks slw 

SELECT * FROM  ServiceTypes st 

SELECT * FROM TypeClients tc 


select * from TypeClients tc ;


SELECT * FROM VpartsCompatibles vc ;

SELECT * FROM  VehicleBrands vb 


SELECT  * FROM Invoices i 


  ---
SELECT  * FROM  ServiceWorks slw 

INSERT INTO ServiceWorks  (Work,PriceWork)
VALUES
   ( 'Servicio Menor',300),
   ( 'Servicio Recurrente',400),
   ('Servicio Mayor',500);

-- -----------------------------------------------------
use DBEB;
ALTER TABLE ServiceWorks
ALTER COLUMN PriceWork int;

ALTER TABLE DBEB.dbo.ServiceWorks ALTER COLUMN PriceWork int ;


-- ------------------------------------------------------------------------------------------------------------------
USE DBEB;

DECLARE @sql NVARCHAR(MAX) = N'';

-- Eliminar tablas en cascada
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ' DROP CONSTRAINT ' + QUOTENAME(rc.CONSTRAINT_NAME) + ';' + CHAR(13) + CHAR(10)
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc ON tc.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
WHERE tc.CONSTRAINT_TYPE = 'FOREIGN KEY';

-- Eliminar las tablas
SELECT @sql += 'DROP TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ';' + CHAR(13) + CHAR(10)
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';

-- Ejecutar el comando SQL
EXEC sp_executesql @sql;



 