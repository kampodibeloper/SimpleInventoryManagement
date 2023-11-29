USE MyInventoryDB;

-- Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1, 1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

-- Create Inventory table
CREATE TABLE Inventory (
    ItemId INT PRIMARY KEY IDENTITY(1, 1),
    ItemName NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Manufacturer NVARCHAR(50),
    Description NVARCHAR(MAX)
);

-- Create Transaction table
CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY IDENTITY(1, 1),
    TransactionDate DATETIME NOT NULL,
    ItemId INT FOREIGN KEY REFERENCES Inventory(ItemId),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    Quantity INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL
);