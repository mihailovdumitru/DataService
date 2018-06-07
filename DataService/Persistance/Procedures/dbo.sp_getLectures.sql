CREATE PROCEDURE [dbo].[sp_getLectures]
AS
	SET NOCOUNT ON;
	SELECT * FROM Lecture WHERE IsActive = 1;	
GO