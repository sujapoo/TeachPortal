-- Create Database
CREATE DATABASE TeachPortal;
GO

-- Use the newly created database
USE TeachPortal;
GO

-- Create Teacher Table
CREATE TABLE Teacher (
Id INT PRIMARY KEY IDENTITY(1,1),
UserName NVARCHAR(50) NOT NULL,
Email NVARCHAR(100) NOT NULL,
PasswordHash NVARCHAR(MAX) NOT NULL,
FirstName NVARCHAR(50) NULL,
LastName NVARCHAR(50) NULL
);
GO

-- Create Student Table
CREATE TABLE Student (
Id INT PRIMARY KEY IDENTITY(1,1),
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Email NVARCHAR(100) NOT NULL,
TeacherId INT NOT NULL,
FOREIGN KEY (TeacherId) REFERENCES Teacher(Id)
);
GO