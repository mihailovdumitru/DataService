﻿CREATE PROCEDURE [dbo].[sp_updateTest]
	@TEST_ID int,
	@NAME nvarchar(50), 
	@TEACHER_ID int,
	@LECTURE_ID int
AS
	UPDATE Test SET Name=@NAME,TeacherID=@TEACHER_ID, LectureID=@LECTURE_ID WHERE TestID=@TEST_ID;
	SELECT TestID FROM Test WHERE TestID=@TEST_ID;
GO