CREATE PROCEDURE [dbo].[sp_getTeachersWithLectures]
AS
	SET NOCOUNT ON;
	SELECT * FROM Teacher t LEFT JOIN TeacherLectures tl on t.TeacherID = tl.TeacherID 
							LEFT JOIN Lecture l  on tl.LectureID = l.LectureID 
							INNER JOIN [User] u on t.UserID = u.UserID 
							WHERE u.IsActive = 1;	
GO