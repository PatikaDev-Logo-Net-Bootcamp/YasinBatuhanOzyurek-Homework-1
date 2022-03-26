USE master
GO

-- Create db if not exists
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'LogoDb')
	CREATE DATABASE LogoDb
GO

USE LogoDb
GO

-- Create Table if not exists
IF OBJECT_ID('Companies') IS NULL
	CREATE TABLE Companies (
		ID INT IDENTITY PRIMARY KEY,
		Name NVARCHAR(100) UNIQUE NOT NULL
	)

-- User is a reserved keyword in MSSQL
IF OBJECT_ID('Users') IS NULL
	CREATE TABLE Users (
		ID INT IDENTITY PRIMARY KEY,
		FirstName NVARCHAR(50) NOT NULL,
		LastName NVARCHAR(20) NOT NULL,
		Email VARCHAR(20) UNIQUE NOT NULL,
		Password VARCHAR(20) NOT NULL,
		Birthdate DATE,
		Company_ID INT FOREIGN KEY REFERENCES Companies(ID)
	)


