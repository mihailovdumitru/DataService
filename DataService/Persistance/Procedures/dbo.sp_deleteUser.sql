CREATE PROCEDURE [dbo].[sp_deleteUser]
	@USER_ID int
AS
	UPDATE [User] SET IsActive=0 WHERE UserID = @USER_ID;
GO