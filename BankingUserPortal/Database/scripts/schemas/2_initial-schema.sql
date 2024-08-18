CREATE TABLE Users (
    UserId INT PRIMARY KEY AUTO_INCREMENT,
    UserName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE Accounts (
    AccountId INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT,
    AccountType VARCHAR(50) NOT NULL,  -- E.g., Savings, Checking, Investment
    Balance DECIMAL(15, 2) DEFAULT 0.00,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY AUTO_INCREMENT,
    AccountId INT,
    TransactionType VARCHAR(50) NOT NULL,  -- E.g., Deposit, Withdrawal, Transfer
    Amount DECIMAL(15, 2) NOT NULL,
    TransactionDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Description VARCHAR(255),
    FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);


CREATE TABLE InvestmentAccounts (
    InvestmentAccountId INT PRIMARY KEY AUTO_INCREMENT,
    AccountId INT,
    InvestmentType VARCHAR(50) NOT NULL,  -- E.g., Stocks, Bonds
    InvestmentAmount DECIMAL(15, 2) NOT NULL,
    CurrentValue DECIMAL(15, 2) DEFAULT 0.00,
    FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);


CREATE TABLE Roles (
    RoleId INT PRIMARY KEY AUTO_INCREMENT,
    RoleName VARCHAR(50) UNIQUE NOT NULL
);


CREATE TABLE UserRoles (
    UserId INT,
    RoleId INT,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);


CREATE TABLE AccountTypes (
    AccountTypeId INT PRIMARY KEY AUTO_INCREMENT,
    TypeName VARCHAR(50) UNIQUE NOT NULL
);


CREATE TABLE AuditLogs (
    LogId INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT,
    Operation VARCHAR(255) NOT NULL,
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

