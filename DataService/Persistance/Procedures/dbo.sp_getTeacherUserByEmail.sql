CREATE PROCEDURE [dbo].[sp_getTeacherUserByEmail]
	@EMAIL nvarchar(50)
AS
	SET NOCOUNT ON;
	SELECT * FROM Teachers WHERE Email = @EMAIL;	
GO