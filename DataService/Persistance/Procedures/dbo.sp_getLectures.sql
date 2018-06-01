CREATE PROCEDURE [dbo].[sp_getLectures]
AS
	SET NOCOUNT ON;
	SELECT * FROM Lecture;	
GO