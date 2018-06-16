CREATE PROCEDURE [dbo].[sp_updateAnswer]
	@ANSWER_ID int,
	@ANSWER nvarchar(300)
AS
	UPDATE Answers SET Answer=@ANSWER WHERE AnswerID=@ANSWER_ID;
	SELECT AnswerID FROM Answers WHERE AnswerID=@ANSWER_ID;
GO