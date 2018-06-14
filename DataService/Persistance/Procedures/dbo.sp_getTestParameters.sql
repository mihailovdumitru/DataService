CREATE PROCEDURE [dbo].[sp_getTestParameters]
AS
	SET NOCOUNT ON;
	SELECT * FROM TestParameters;	
GO