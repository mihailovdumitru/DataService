CREATE PROCEDURE [dbo].[sp_getTestResults]
AS
	SET NOCOUNT ON;
	SELECT * FROM TestResults;	
GO