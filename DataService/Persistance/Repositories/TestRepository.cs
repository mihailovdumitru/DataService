﻿using Persistance.Interfaces;
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
    }
}
