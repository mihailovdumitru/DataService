﻿CREATE PROCEDURE [dbo].[sp_insertTest]
	@NAME nvarchar(50), 
	@TEACHER_ID int,
	@DISCIPLINE_ID int
AS
IF NOT EXISTS(SELECT 1 FROM Test WHERE Name=@NAME AND TeacherID=@TEACHER_ID AND DisciplineID=@DISCIPLINE_ID) 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Test(Name, TeacherID, DisciplineID) 
			     VALUES (@NAME, @TEACHER_ID,@DISCIPLINE_ID); 
	SELECT TestID=SCOPE_IDENTITY()
END
GO
