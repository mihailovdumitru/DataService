CREATE PROCEDURE [dbo].[sp_deleteTeacherLecturesForTeacher]
	@TEACHER_ID int
AS
	SET NOCOUNT ON;
	DELETE FROM TeacherLectures
	WHERE TeacherID = @TEACHER_ID
GO