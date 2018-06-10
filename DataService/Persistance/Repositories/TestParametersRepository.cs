﻿using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Repositories
{
    public class TestParametersRepository:SqlBase, ITestParametersRepository
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
    }
}