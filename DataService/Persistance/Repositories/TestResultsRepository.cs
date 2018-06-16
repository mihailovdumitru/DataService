using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class TestResultsRepository : SqlBase, ITestResultsRepository
    {
        public bool AddTestResults(TestResults testResults, SqlConnection conn = null)
        {
            bool succes = true;
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertTestResults", conn))
            {
                cmd.Parameters.AddWithValue("@STUDENT_ID", testResults.StudentID);
                cmd.Parameters.AddWithValue("@TEST_ID", testResults.TestID);
                cmd.Parameters.AddWithValue("@MARK", testResults.Mark);
                cmd.Parameters.AddWithValue("@POINTS", testResults.Points);
                cmd.Parameters.AddWithValue("@ANSWERS_RESULT", testResults.AnswersResult);
                cmd.Parameters.AddWithValue("@TEST_RESULT_DATE", testResults.TestResultDate);
                cmd.Parameters.AddWithValue("@NR_OF_CORRECT_ANSWERS", testResults.NrOfCorrectAnswers);
                cmd.Parameters.AddWithValue("@NR_OF_WRONG_ANSWERS", testResults.NrOfWrongAnswers);
                cmd.Parameters.AddWithValue("@NR_OF_UNFILLED_ANSWERS", testResults.NrOfUnfilledAnswers);
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();

                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return succes;
        }

        public List<TestResults> GetTestsResults(SqlConnection conn = null)
        {
            bool nullConnection = false;
            TestResults testResults = null;
            List<TestResults> testsResults = new List<TestResults>();

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_getTestResults", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        testResults = new TestResults
                        {
                            TestID = DataUtil.GetDataReaderValue<int>("TestID", reader),
                            StudentID = DataUtil.GetDataReaderValue<int>("StudentID", reader),
                            AnswersResult = DataUtil.GetDataReaderValue<string>("AnswersResult", reader),
                            Mark = DataUtil.GetDataReaderValue<float>("Mark", reader),
                            Points = DataUtil.GetDataReaderValue<float>("Points", reader),
                            TestResultDate = DataUtil.GetDataReaderValue<DateTime>("TestResultDate", reader),
                            NrOfCorrectAnswers = DataUtil.GetDataReaderValue<int>("NrOfCorrectAnswers", reader),
                            NrOfWrongAnswers = DataUtil.GetDataReaderValue<int>("NrOfWrongAnswers", reader),
                            NrOfUnfilledAnswers = DataUtil.GetDataReaderValue<int>("NrOfUnfilledAnswers", reader)
                        };

                        testsResults.Add(testResults);
                    }
                }

                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return testsResults;
        }
    }
}