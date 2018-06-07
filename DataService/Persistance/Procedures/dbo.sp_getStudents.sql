CREATE PROCEDURE [dbo].[sp_getStudents]
AS
	SET NOCOUNT ON;
	SELECT s.StudentID,s.FirstName, s.LastName, s.Email, s.ClassID, s.UserID FROM Student s INNER JOIN [User] u on s.UserID=u.UserID WHERE u.IsActive = 1;	
GO