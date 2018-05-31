﻿CREATE PROCEDURE [dbo].[sp_insertStudent]
	@FIRSTNAME nvarchar(30),
	@LASTNAME nvarchar(30),
	@EMAIL nvarchar(50),
	@CLASS_ID int
AS
IF NOT EXISTS(SELECT 1 FROM Student WHERE FirstName=@FIRSTNAME AND LastName=@LASTNAME AND Email=@EMAIL AND ClassID=@CLASS_ID) 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Student(FirstName, LastName, Email, ClassID) 
			     VALUES (@FIRSTNAME, @LASTNAME, @EMAIL, @CLASS_ID); 
	SELECT StudentID=SCOPE_IDENTITY()
END
ELSE SELECT StudentID FROM Student WHERE FirstName=@FIRSTNAME AND LastName=@LASTNAME AND Email=@EMAIL AND  ClassID=@CLASS_ID
GO