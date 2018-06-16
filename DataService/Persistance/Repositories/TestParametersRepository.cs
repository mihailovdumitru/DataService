using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class TestParametersRepository : SqlBase, ITestParametersRepository
    {
        public bool AddTestParameters(TestParameters testParam, SqlConnection conn = null)
        {
            bool nullConnection = false;
            bool succes = true;
            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertTestParams", conn))
            {
                cmd.Parameters.AddWithValue("@TEST_ID", testParam.TestID);
                cmd.Parameters.AddWithValue("@TEACHER_ID", testParam.TeacherID);
                cmd.Parameters.AddWithValue("@CLASSID", testParam.ClassID);
                cmd.Parameters.AddWithValue("@DURATION", testParam.Duration);
                cmd.Parameters.AddWithValue("@PENALTY", testParam.Penalty);
                cmd.Parameters.AddWithValue("@START_TEST", testParam.StartTest);
                cmd.Parameters.AddWithValue("@FINISH_TEST", testParam.FinishTest);
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

        public List<TestParameters> GetTestParameters(SqlConnection conn = null)
        {
            bool nullConnection = false;
            TestParameters testParams = null;
            List<TestParameters> testsParams = new List<TestParameters>();

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_getTestParameters", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        testParams = new TestParameters
                        {
                            TestID = DataUtil.GetDataReaderValue<int>("TestID", reader),
                            TeacherID = DataUtil.GetDataReaderValue<int>("TeacherID", reader),
                            ClassID = DataUtil.GetDataReaderValue<int>("ClassID", reader),
                            Duration = DataUtil.GetDataReaderValue<int>("Duration", reader),
                            Penalty = DataUtil.GetDataReaderValue<float>("Penalty", reader),
                            StartTest = DataUtil.GetDataReaderValue<DateTime>("StartTest", reader),
                            FinishTest = DataUtil.GetDataReaderValue<DateTime>("FinishTest", reader)
                        };

                        testsParams.Add(testParams);
                    }
                }

                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return testsParams;
        }
    }
}