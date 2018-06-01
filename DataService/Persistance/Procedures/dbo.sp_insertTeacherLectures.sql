CREATE PROCEDURE [dbo].[sp_insertTeacherLectures]
	@TEACHER_ID int, 
	@LECTURE_ID int
AS
IF NOT EXISTS(SELECT 1 FROM TeacherLectures WHERE TeacherID=@TEACHER_ID AND LectureID=@LECTURE_ID) 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO TeacherLectures(TeacherID, LectureID) 
			     VALUES (@TEACHER_ID, @LECTURE_ID); 
END
GO