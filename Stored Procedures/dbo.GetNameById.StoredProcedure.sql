USE [TestSahil]
GO
/****** Object:  StoredProcedure [dbo].[GetNameById]    Script Date: 1/20/2025 4:35:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNameById]
	@FirstName VARCHAR(50),
	@LastName VArchar(50),
	@Message VARCHAR(500) OUTPUT 
AS
BEGIN
	SET NOCOUNT ON;

	SET @FirstName = RTRIM(LTRIM(TRIM(@FirstName)))
	SET @LastName = RTRIM(LTRIM(TRIM(@LastName)))

	DECLARE @Result TABLE (EmpID INT, FirstName VARCHAR(50), LastName VARCHAR(50), Email VARCHAR(50), PhoneNo VARCHAR(50))
	INSERT INTO @Result (EmpID, FirstName, LastName, Email, PhoneNo) 
	SELECT EmpID, FirstName, LastName, Email, PhoneNo FROM Employees WHERE (FirstName = @FirstName AND LastName = @LastName)
	
	IF EXISTS( SELECT 1 FROM @Result)
	BEGIN
		SELECT EmpID, FirstName, LastName, Email, PhoneNo FROM @Result
		SET @Message = 'Employee found';
	END
	ELSE
	BEGIN
		SET @Message = 'Employee dosen''t exist'
	END
END
GO
