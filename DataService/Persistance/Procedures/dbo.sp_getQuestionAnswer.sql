CREATE PROCEDURE [dbo].[sp_getQuestionAnswers]
AS
	SET NOCOUNT ON;
	SELECT * FROM QuestionAnswers;	
GO