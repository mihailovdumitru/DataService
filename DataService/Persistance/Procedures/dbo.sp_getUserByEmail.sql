CREATE PROCEDURE [dbo].[sp_getUserByEmail]
	@USERNAME nvarchar(50)
AS
	SET NOCOUNT ON;
	SELECT * FROM [User] WHERE Username = @USERNAME;	
GO