CREATE PROCEDURE [dbo].[sp_getClasses]
AS
	SET NOCOUNT ON;
	SELECT * FROM Class WHERE IsActive = 1;	
GO