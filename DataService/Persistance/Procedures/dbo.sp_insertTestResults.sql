CREATE PROCEDURE [dbo].[sp_insertTestResults]
	@STUDENT_ID int, 
	@TEST_ID int,
	@MARK float,
	@POINTS float,
	@ANSWERS_RESULT nvarchar(350),
	@TEST_RESULT_DATE datetime,
	@NR_OF_CORRECT_ANSWERS int,
	@NR_OF_WRONG_ANSWERS int,
	@NR_OF_UNFILLED_ANSWERS int
AS
IF NOT EXISTS(SELECT 1 FROM TestResults WHERE StudentID=@STUDENT_ID AND TestID=@TEST_ID) 
BEGIN
	SET NOCOUNT ON;
	INSERT INTO TestResults(StudentID, TestID, Mark, Points, AnswersResult, TestResultDate, NrOfCorrectAnswers, NrOfWrongAnswers, NrOfUnfilledAnswers) 
			     VALUES (@STUDENT_ID, @TEST_ID, @MARK, @POINTS, @ANSWERS_RESULT, @TEST_RESULT_DATE, @NR_OF_CORRECT_ANSWERS, @NR_OF_WRONG_ANSWERS, @NR_OF_UNFILLED_ANSWERS); 
END
ELSE 
	UPDATE TestResults SET Mark=@MARK, Points = @POINTS, AnswersResult = @ANSWERS_RESULT, 
						TestResultDate = @TEST_RESULT_DATE, NrOfCorrectAnswers=@NR_OF_CORRECT_ANSWERS,
						NrOfWrongAnswers=@NR_OF_WRONG_ANSWERS, NrOfUnfilledAnswers=@NR_OF_UNFILLED_ANSWERS  
	WHERE StudentID=@STUDENT_ID AND TestID=@TEST_ID;
GO


