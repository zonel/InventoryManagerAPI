CREATE DATABASE InventoryManagerDb;
USE InventoryManagerDb;

CREATE TABLE Products (
    ID INT PRIMARY KEY,
    SKU VARCHAR(255) UNIQUE,
    Name VARCHAR(255),
    EAN VARCHAR(255),
    ProducerName VARCHAR(255),
    Category VARCHAR(255),
    IsWire BIT,
    Available BIT,
    IsVendor BIT,
    DefaultImage VARCHAR(500)
);

CREATE TABLE Inventory (
    ProductId INT PRIMARY KEY,
    SKU VARCHAR(255),
    Unit VARCHAR(50),
    Qty INT,
    Manufacturer VARCHAR(255),
    Shipping VARCHAR(50),
    ShippingCost DECIMAL(18, 2),
);

CREATE TABLE Prices (
    PriceID VARCHAR(50) PRIMARY KEY, 
    SKU VARCHAR(255), 
    NettProductPrice DECIMAL(18, 2),
    NettProductPriceDiscount DECIMAL(18, 2),
    VatRate DECIMAL(5, 2),
    NettProductPriceDiscountLogistic DECIMAL(18, 2)
);