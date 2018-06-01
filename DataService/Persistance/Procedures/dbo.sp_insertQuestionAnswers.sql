CREATE PROCEDURE [dbo].[sp_insertQuestionAnswers]
	@QUESTION_ID int, 
	@ANSWER_ID int,
	@CORRECT bit
AS
IF NOT EXISTS(SELECT 1 FROM QuestionAnswers WHERE QuestionID=@QUESTION_ID AND AnswerID=@ANSWER_ID AND Correct=@CORRECT) 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO QuestionAnswers(QuestionID, AnswerID, Correct) 
			     VALUES (@QUESTION_ID, @ANSWER_ID, @CORRECT); 
END
GO