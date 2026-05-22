-- 0. Forcefully delete the old database if it is stuck
USE master;
GO

IF DB_ID('SDG2_ZeroHungerDB') IS NOT NULL
BEGIN
    ALTER DATABASE SDG2_ZeroHungerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SDG2_ZeroHungerDB;
END
GO

CREATE DATABASE SDG2_ZeroHungerDB;
GO

USE SDG2_ZeroHungerDB;
GO

-- 1. Users Table
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    user_role VARCHAR(20) NOT NULL
);

INSERT INTO Users (username, password_hash, user_role) 
VALUES ('staff', 'staff123', 'Standard User'),
       ('admin', 'admin123', 'Admin');
GO

-- 2. Inventory Table 
CREATE TABLE Inventory (
    item_id INT IDENTITY(1,1) PRIMARY KEY,
    item_name VARCHAR(100) NOT NULL,
    category VARCHAR(50),
    expiration_date DATE NOT NULL,
    stock_quantity INT,
    CONSTRAINT chk_positive_stock CHECK (stock_quantity >= 0)
);
GO

-- 3. DonationLogs Table 
CREATE TABLE DonationLogs (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    item_name VARCHAR(100) NOT NULL,
    quantity_donated INT NOT NULL,
    donor_name VARCHAR(100) NOT NULL,
    date_received DATETIME DEFAULT GETDATE()
);
GO

-- 4. Distributions Table 
CREATE TABLE Distributions (
    dist_id INT IDENTITY(1,1) PRIMARY KEY,
    item_name VARCHAR(100) NOT NULL,
    beneficiary_name VARCHAR(100) NOT NULL,
    quantity_given INT NOT NULL,
    date_given DATETIME DEFAULT GETDATE()
);
GO

-- 5. View for Expiring Stock
CREATE VIEW View_ExpiringStock AS
SELECT 
    item_name, 
    expiration_date, 
    stock_quantity
FROM 
    Inventory
WHERE 
    DATEDIFF(day, GETDATE(), expiration_date) < 30;
GO
