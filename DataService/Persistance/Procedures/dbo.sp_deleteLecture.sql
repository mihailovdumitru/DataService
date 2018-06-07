CREATE PROCEDURE [dbo].[sp_deleteLecture]
	@LECTURE_ID int
AS
	UPDATE Lecture SET IsActive=0 WHERE LectureID = @LECTURE_ID;
GO