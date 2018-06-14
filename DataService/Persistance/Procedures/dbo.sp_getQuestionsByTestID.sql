CREATE PROCEDURE [dbo].[sp_getQuestionsByTestID]
	@TEST_ID int
AS
	SET NOCOUNT ON;
	SELECT * FROM Question WHERE TestID = @TEST_ID;	
GO