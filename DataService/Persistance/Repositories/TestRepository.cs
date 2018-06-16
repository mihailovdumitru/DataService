using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model.DBObjects;

namespace Persistance.Repositories
{
    public class TestRepository: SqlBase, ITestRepository
    {
        private static readonly TestRepository _instance = new TestRepository();

        public int AddTest(Test test, SqlConnection conn = null)
        {
            int testID = -1;
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

            return testID;
        }

        public int UpdateTest(Test test, SqlConnection conn = null)
        {
            int testID = -1;
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

            return testID;
        }

        public IEnumerable<Test> GetTests(SqlConnection conn = null)
        {
            bool nullConnection = false;
            Test test = null;
            List<Test> tests = new List<Test>();

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

            return tests;
        }

    }
}
