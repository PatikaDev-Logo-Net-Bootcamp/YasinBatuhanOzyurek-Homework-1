USE LogoDb
GO

CREATE PROCEDURE CreateUser
    @FirstName NVARCHAR(50),   
    @LastName NVARCHAR(20),
	@Email VARCHAR(20),
	@Password VARCHAR(20),
	@Birthdate DATE,
	@Company_ID INT
AS   
	IF EXISTS (SELECT TOP(1) * FROM Companies WHERE ID = @Company_ID)
		BEGIN
			BEGIN TRY
				INSERT INTO Users
					(Users.FirstName, LastName, Email, Password, Birthdate, Company_ID )
				VALUES
					(@FirstName, @LastName, @Email, @Password, @Birthdate, @Company_ID)
			END TRY
			BEGIN CATCH
				print 'Email taken'
			END CATCH
		END
	ELSE
		BEGIN
			print 'Company does not exists!'
		END
GO


CREATE PROCEDURE CreateCompany
    @CompanyName NVARCHAR(100)
AS 
	IF NOT EXISTS (SELECT * FROM Companies WHERE Name = @CompanyName)
		INSERT INTO Companies
			(Name)
		 VALUES
			(@CompanyName)
	ELSE
		print 'Company exists!'
	
GO

-- Execute CreateCompany procedure
EXECUTE CreateCompany 
	'Company1'
GO

-- Execute CreateUser procedure
-- try with valid company id
EXECUTE CreateUser 
  'John', 'Doe', 'johndoe@gmail.com', '123456', '1996-05-21', 1
GO

-- Try with invalid company id
EXECUTE CreateUser 
  'John', 'Doe', 'johndoe@gmail.com', '123456', '1996-05-21', 99
GO

-- Check tables
SELECT * FROM Users
SELECT * FROM Companies
SELECT * FROM Users INNER JOIN Companies ON Users.Company_ID = Companies.ID



 
