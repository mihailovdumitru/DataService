CREATE PROCEDURE [dbo].[sp_insertTestParams]
	@TEST_ID int, 
	@TEACHER_ID int,
	@CLASSID int,
	@DURATION int,
	@PENALTY float,
	@START_TEST datetime,
	@FINISH_TEST datetime
AS
IF NOT EXISTS(SELECT 1 FROM TestParameters WHERE TeacherID=@TEACHER_ID AND ClassID=@CLASSID AND TestID=@TEST_ID) 
BEGIN
	SET NOCOUNT ON;
	INSERT INTO TestParameters(TestID, TeacherID, ClassID, Duration, Penalty, StartTest, FinishTest) 
			     VALUES (@TEST_ID, @TEACHER_ID, @CLASSID, @DURATION, @PENALTY, @START_TEST, @FINISH_TEST); 
END
ELSE 
	UPDATE TestParameters SET Duration=@DURATION, Penalty=@PENALTY, StartTest=@START_TEST, FinishTest = @FINISH_TEST WHERE TeacherID=@TEACHER_ID AND ClassID=@CLASSID AND TestID=@TEST_ID;
GO


