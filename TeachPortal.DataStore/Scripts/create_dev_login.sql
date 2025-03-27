CREATE LOGIN dev WITH PASSWORD = 'StrongPassword123';
GO

USE TeachPortal;
GO

CREATE USER dev FOR LOGIN dev;
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO dev;
GO

ALTER ROLE db_owner ADD MEMBER dev;
GO

SELECT name FROM sys.sql_logins WHERE name = 'dev';
GO

SELECT name FROM sys.database_principals WHERE name = 'dev';
GO
