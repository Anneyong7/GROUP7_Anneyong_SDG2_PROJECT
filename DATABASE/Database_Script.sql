-- Create the main database
CREATE DATABASE SDG2_ZeroHungerDB;
GO

USE SDG2_ZeroHungerDB;
GO

-- 1. Users Table (New: For FR4 Login System)
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    user_role VARCHAR(20) NOT NULL -- Use 'Admin' or 'Standard User'
);
-- Add the admin account immediately after making the table
INSERT INTO Users (username, password_hash, user_role) 
VALUES ('staff', 'staff123', 'Standard User');
GO

-- 2. Donors Table (Updated to IDENTITY)
CREATE TABLE Donors (
    donor_id INT IDENTITY(1,1) PRIMARY KEY,
    donor_name VARCHAR(100) NOT NULL,
    donor_email VARCHAR(100) UNIQUE,
    join_date DATE NOT NULL
);

-- 3. FoodInventory Table (Updated to IDENTITY and kept CHECK constraint)
CREATE TABLE FoodInventory (
    item_id INT IDENTITY(1,1) PRIMARY KEY,
    item_name VARCHAR(100) NOT NULL,
    category VARCHAR(50),
    expiration_date DATE NOT NULL,
    stock_quantity INT,
    CONSTRAINT chk_positive_stock CHECK (stock_quantity >= 0)
);

-- 4. DonationsLogs Table (Updated to IDENTITY and GETDATE)
CREATE TABLE DonationsLogs (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    donor_id INT,
    item_id INT,
    qty_donated INT NOT NULL,
    donation_date DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_donor FOREIGN KEY (donor_id) REFERENCES Donors(donor_id),
    CONSTRAINT fk_item_donated FOREIGN KEY (item_id) REFERENCES FoodInventory(item_id)
);

-- 5. Distributions Table (Updated to IDENTITY and GETDATE)
CREATE TABLE Distributions (
    dist_id INT IDENTITY(1,1) PRIMARY KEY,
    item_id INT NOT NULL,
    beneficiary_name VARCHAR(100) NOT NULL,
    quantity_given INT NOT NULL,
    date_given DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_dist_item FOREIGN KEY (item_id) REFERENCES FoodInventory(item_id) 
    ON UPDATE CASCADE
);
GO

-- 6. View for Expiring Stock (Updated to SQL Server DATEDIFF syntax)
CREATE VIEW View_ExpiringStock AS
SELECT 
    item_name, 
    expiration_date, 
    stock_quantity
FROM 
    FoodInventory
WHERE 
    DATEDIFF(day, GETDATE(), expiration_date) < 30;
GO

-- run this line of code to check users
USE SDG2_ZeroHungerDB;

SELECT * FROM Users;