CREATE PROCEDURE [dbo].[sp_getStudents]
AS
	SET NOCOUNT ON;
	SELECT * FROM Student;	
GO