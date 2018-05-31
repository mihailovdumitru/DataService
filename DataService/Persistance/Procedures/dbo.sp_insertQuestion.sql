﻿CREATE PROCEDURE [dbo].[sp_insertQuestion]
	@QUESTION nvarchar(400), 
	@POINTS int,
	@TEST_ID int
AS
IF NOT EXISTS(SELECT 1 FROM Question WHERE Question=@QUESTION AND Points=@POINTS AND TestID=@TEST_ID) 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Question(Question, Points, TestID) 
			     VALUES (@QUESTION, @POINTS, @TEST_ID); 
	SELECT QuestionID=SCOPE_IDENTITY()
END
ELSE SELECT QuestionID FROM Question WHERE Question=@QUESTION AND Points=@POINTS AND TestID=@TEST_ID
GO