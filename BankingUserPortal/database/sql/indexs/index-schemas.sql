-- Users Table Indexes
CREATE INDEX idx_users_email ON Users(Email);
CREATE INDEX idx_users_username ON Users(UserName);

-- Accounts Table Indexes
CREATE INDEX idx_accounts_userid ON Accounts(UserId);
CREATE INDEX idx_accounts_accounttype ON Accounts(AccountType);

-- Transactions Table Indexes
CREATE INDEX idx_transactions_accountid ON Transactions(AccountId);
CREATE INDEX idx_transactions_transactiondate ON Transactions(TransactionDate);

-- InvestmentAccounts Table Indexes
CREATE INDEX idx_investmentaccounts_accountid ON InvestmentAccounts(AccountId);

-- Roles Table Indexes
CREATE INDEX idx_roles_rolename ON Roles(RoleName);

-- UserRoles Table Indexes
CREATE INDEX idx_userroles_userid_roleid ON UserRoles(UserId, RoleId);

-- AccountTypes Table Indexes (Optional)
CREATE INDEX idx_accounttypes_typename ON AccountTypes(TypeName);

-- AuditLogs Table Indexes (Optional)
CREATE INDEX idx_auditlogs_userid ON AuditLogs(UserId);
CREATE INDEX idx_auditlogs_timestamp ON AuditLogs(Timestamp);
