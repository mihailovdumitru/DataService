using Persistance.Interfaces;
using Persistance.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model.DBObjects;
using log4net;
using System;

namespace Persistance.Repositories
{
    public class TestRepository : SqlBase, ITestRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly TestRepository _instance = new TestRepository();

        public int AddTest(Test test, SqlConnection conn = null)
        {
            int testID = -1;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_insertTest", conn))
                {
                    cmd.Parameters.AddWithValue("@NAME", test.Naming);
                    cmd.Parameters.AddWithValue("@TEACHER_ID", test.TeacherID);
                    cmd.Parameters.AddWithValue("@LECTURE_ID", test.LectureID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testID = DataUtil.GetDataReaderValue<int>("TestID", reader);
                        }
                    }

                    if (conn.State == ConnectionState.Open && nullConnection)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("AddTest() error. Test: " + test.Naming, e);
            }

            return testID;
        }

        public int UpdateTest(Test test, SqlConnection conn = null)
        {
            int testID = -1;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_updateTest", conn))
                {
                    cmd.Parameters.AddWithValue("@NAME", test.Naming);
                    cmd.Parameters.AddWithValue("@TEACHER_ID", test.TeacherID);
                    cmd.Parameters.AddWithValue("@LECTURE_ID", test.LectureID);
                    cmd.Parameters.AddWithValue("@TEST_ID", test.TestID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (nullConnection)
                        conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testID = DataUtil.GetDataReaderValue<int>("TestID", reader);
                        }
                    }
                    if (conn.State == ConnectionState.Open && nullConnection)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("UpdateTest() error. Test: " + test.Naming, e);
            }

            return testID;
        }

        public IEnumerable<Test> GetTests(SqlConnection conn = null)
        {
            List<Test> tests = new List<Test>();

            try
            {
                bool nullConnection = false;
                Test test = null;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_getTests", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            test = new Test
                            {
                                TestID = DataUtil.GetDataReaderValue<int>("TestID", reader),
                                Naming = DataUtil.GetDataReaderValue<string>("Name", reader),
                                TeacherID = DataUtil.GetDataReaderValue<int>("TeacherID", reader),
                                LectureID = DataUtil.GetDataReaderValue<int>("LectureID", reader)
                            };

                            tests.Add(test);
                        }
                    }

                    if (conn.State == ConnectionState.Open && nullConnection)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("GetTests() error.", e);
            }

            return tests;
        }
    }
}