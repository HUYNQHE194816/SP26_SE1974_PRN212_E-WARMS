
-- Warranty Repair Center Database
CREATE DATABASE WarrantyRepairCenter;
GO

USE WarrantyRepairCenter;
GO

CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Devices (
    DeviceId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    DeviceName NVARCHAR(100) NOT NULL,
    SerialNumber NVARCHAR(100),
    PurchaseDate DATE,
    Notes NVARCHAR(255),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE Technicians (
    TechnicianId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    SkillLevel NVARCHAR(50),
    IsActive BIT DEFAULT 1
);

CREATE TABLE RepairOrders (
    RepairOrderId INT IDENTITY(1,1) PRIMARY KEY,
    DeviceId INT NOT NULL,
    TechnicianId INT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    Diagnostic NVARCHAR(MAX),
    LaborCost DECIMAL(10,2) DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CompletedAt DATETIME NULL,
    FOREIGN KEY (DeviceId) REFERENCES Devices(DeviceId),
    FOREIGN KEY (TechnicianId) REFERENCES Technicians(TechnicianId)
);

CREATE TABLE SpareParts (
    PartId INT IDENTITY(1,1) PRIMARY KEY,
    PartName NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INT DEFAULT 0
);

CREATE TABLE RepairOrderParts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RepairOrderId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    FOREIGN KEY (RepairOrderId) REFERENCES RepairOrders(RepairOrderId),
    FOREIGN KEY (PartId) REFERENCES SpareParts(PartId)
);

CREATE TABLE Invoices (
    InvoiceId INT IDENTITY(1,1) PRIMARY KEY,
    RepairOrderId INT NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    IssuedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RepairOrderId) REFERENCES RepairOrders(RepairOrderId)
);

CREATE TABLE Warranty (
    WarrantyId INT IDENTITY(1,1) PRIMARY KEY,
    DeviceId INT NOT NULL,
    RepairOrderId INT NOT NULL,
    WarrantyMonths INT DEFAULT 3,
    ExpirationDate DATE,
    FOREIGN KEY (DeviceId) REFERENCES Devices(DeviceId),
    FOREIGN KEY (RepairOrderId) REFERENCES RepairOrders(RepairOrderId)
);
