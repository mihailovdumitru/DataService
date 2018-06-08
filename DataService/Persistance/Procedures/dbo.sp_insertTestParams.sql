CREATE PROCEDURE [dbo].[sp_insertTestParams]
	@TEST_ID int, 
	@TEACHER_ID int,
	@CLASSID int,
	@DURATION int,
	@PENALTY float,
	@START_TEST datetime,
	@FINISH_TEST datetime
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO TestParameters(TestID, TeacherID, ClassID, Duration, Penalty, StartTest, FinishTest) 
			     VALUES (@TEST_ID, @TEACHER_ID, @CLASSID, @DURATION, @PENALTY, @START_TEST, @FINISH_TEST); 
END
GO
