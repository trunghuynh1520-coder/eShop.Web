USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'eShop')
BEGIN
    CREATE DATABASE eShop;
END
GO

USE eShop;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Product' and xtype='U')
BEGIN
    CREATE TABLE Product (
        Id INT PRIMARY KEY,
        Brand NVARCHAR(100),
        Name NVARCHAR(255),
        Price FLOAT,
        ImageLink NVARCHAR(MAX),
        Description NVARCHAR(MAX)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Order' and xtype='U')
BEGIN
    CREATE TABLE [Order] (
        OrderId INT IDENTITY(1,1) PRIMARY KEY,
        DatePlaced DATETIME,
        DateProcessing DATETIME,
        DateProcessed DATETIME,
        CustomerName NVARCHAR(255),
        CustomerAddress NVARCHAR(255),
        CustomerCity NVARCHAR(100),
        CustomerStateProvince NVARCHAR(100),
        CustomerCountry NVARCHAR(100),
        AdminUser NVARCHAR(255),
        UniqueId NVARCHAR(255)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderLineItem' and xtype='U')
BEGIN
    CREATE TABLE OrderLineItem (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT,
        OrderId INT,
        Quantity INT,
        Price FLOAT,
        FOREIGN KEY (ProductId) REFERENCES Product(Id),
        FOREIGN KEY (OrderId) REFERENCES [Order](OrderId)
    );
END
GO

-- Insert seed data for Products if empty
IF NOT EXISTS (SELECT TOP 1 1 FROM Product)
BEGIN
    INSERT INTO Product (Id, Brand, Name, Price, ImageLink, Description) VALUES 
    (495, 'maybelline', 'Maybelline Face Studio Master Hi-Light Light Booster Bronzer', 14.99, 'https://d3t32hsnjxo7q6.cloudfront.net/i/991799d3e70b8856686979f8ff6dcfe0_ra,w158,h184_pa,w158,h184.png', 'Maybelline Face Studio Master Hi-Light Light Boosting bronzer formula...'),
    (488, 'maybelline', 'Maybelline Fit Me Bronzer', 10.29, 'https://d3t32hsnjxo7q6.cloudfront.net/i/d4f7d82b4858c622bb3c1cef07b9d850_ra,w158,h184_pa,w158,h184.png', 'Why You''ll Love It Lightweight pigments blend easily...'),
    (477, 'maybelline', 'Maybelline Facestudio Master Contour Kit', 15.99, 'https://d3t32hsnjxo7q6.cloudfront.net/i/4f731de249cbd4cb819ea7f5f4cfb5c3_ra,w158,h184_pa,w158,h184.png', 'Maybelline Facestudio Master Contour Kit is the ultimate...'),
    (468, 'maybelline', 'Maybelline Face Studio Master Hi-Light Light Booster Blush', 14.99, 'https://d3t32hsnjxo7q6.cloudfront.net/i/4621032a92cb428ad640c105b944b39c_ra,w158,h184_pa,w158,h184.png', 'Maybelline Face Studio Master Hi-Light Light Boosting blush...'),
    (439, 'maybelline', 'Maybelline Fit Me Blush', 10.29, 'https://d3t32hsnjxo7q6.cloudfront.net/i/53d5f825461117c0d96946e1029510b0_ra,w158,h184_pa,w158,h184.png', 'Maybelline Fit Me Blush has lightweight pigments...');
END
GO
