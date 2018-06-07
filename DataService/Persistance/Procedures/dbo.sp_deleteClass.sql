CREATE PROCEDURE [dbo].[sp_deleteClass]
	@CLASS_ID int
AS
	UPDATE Class SET IsActive=0 WHERE ClassID = @CLASS_ID;
GO