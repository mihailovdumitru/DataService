CREATE PROCEDURE [dbo].[sp_getTeachers]
AS
	SET NOCOUNT ON;
	SELECT * FROM Teachers;	
GO