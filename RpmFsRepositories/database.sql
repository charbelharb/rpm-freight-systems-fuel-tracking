CREATE DATABASE Hangfire;

USE Hangfire;
GO;

CREATE LOGIN hangfire
WITH PASSWORD = 'RpmFsFuelTracking2022';
GO;
    
CREATE USER hangfire
FOR LOGIN hangfire;
GO;
    
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE [name] = 'HangFire') EXEC ('CREATE SCHEMA [HangFire]')
GO;

ALTER AUTHORIZATION ON SCHEMA::[HangFire] TO [HangFire]
GO;

GRANT CREATE TABLE TO [HangFire]
    GO;

CREATE DATABASE RpmFsFuelTracking;
USE RpmFsFuelTracking;
GO;
    
CREATE LOGIN RpmFsUser
WITH PASSWORD ='RpmFsFuelTracking2022';
GO;
    
CREATE USER RpmFsUser
FOR LOGIN RpmFsUser;
GO;
    
CREATE TABLE FuelPrices(
    id int IDENTITY(1,1) PRIMARY KEY,
    priceDate VARCHAR(8) NOT NULL UNIQUE,
    price DECIMAL(19,4) NOT NULL
);
GO;
    
USE RpmFsFuelTracking;
GO;

GRANT SELECT,INSERT,UPDATE ,DELETE
    TO [RpmFsUser];
GO;
