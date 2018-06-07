CREATE PROCEDURE [dbo].[sp_getTeachers]
AS
	SET NOCOUNT ON;
	SELECT t.TeacherID,t.FirstName, t.LastName, t.Email, t.UserID FROM Teacher t INNER JOIN [User] u on t.UserID=u.UserID WHERE u.IsActive = 1;	
GO