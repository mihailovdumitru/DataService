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
    public class LectureRepository: SqlBase, ILectureRepository
    {
        private static readonly LectureRepository _instance = new LectureRepository();
        public int AddLecture(Lecture lecture, SqlConnection conn = null)
        {
            int lectureID = -1;
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertLecture", conn))
            {
                cmd.Parameters.AddWithValue("@NAME", lecture.Name);
                cmd.Parameters.AddWithValue("@YEAR_OF_STUDY", lecture.YearOfStudy);

                cmd.CommandType = CommandType.StoredProcedure;
                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lectureID = DataUtil.GetDataReaderValue<int>("LectureID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return lectureID;
        }
    }
}