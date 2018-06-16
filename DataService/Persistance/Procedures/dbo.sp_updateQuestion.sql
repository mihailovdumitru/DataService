CREATE PROCEDURE [dbo].[sp_updateQuestion]
	@QUESTION_ID int,
	@QUESTION nvarchar(400),
	@POINTS float,
	@TEST_ID int
AS
	UPDATE Question SET Question=@QUESTION, Points=@POINTS WHERE QuestionID=@QUESTION_ID;
	SELECT QuestionID FROM Question WHERE QuestionID=@QUESTION_ID;
GO