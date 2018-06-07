﻿CREATE PROCEDURE [dbo].[sp_insertOrUpdateUser]
	@USERNAME nvarchar(50),
	@PASSWORD nvarchar(300),
	@ROLE nvarchar(20),
	@IS_ACTIVE bit,
	@USER_ID int
AS
IF (NOT EXISTS(SELECT 1 FROM [User] WHERE Username=@USERNAME)  AND @USER_ID = -1)
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [User](UserName, Password, Role, IsActive) 
			     VALUES (@USERNAME, @PASSWORD, @ROLE, @IS_ACTIVE); 
	SELECT UserID=SCOPE_IDENTITY()
END
ELSE 
	UPDATE [User] SET UserName=@USERNAME,Password=@PASSWORD,Role=@ROLE,IsActive=@IS_ACTIVE WHERE UserID = @USER_ID;
	SELECT UserID FROM [User] WHERE Username=@USERNAME;
GO