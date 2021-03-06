﻿using log4net;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class TeacherLecturesRepository : SqlBase, ITeacherLecturesRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void AddTeacherLectures(int teacherID, int lectureID, SqlConnection conn = null)
        {
            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_insertTeacherLectures", conn))
                {
                    cmd.Parameters.AddWithValue("@TEACHER_ID", teacherID);
                    cmd.Parameters.AddWithValue("@LECTURE_ID", lectureID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    if (conn.State == ConnectionState.Open && nullConnection)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("AddTeacherLectures() error. TeacherId: " + teacherID, e);
            }
        }

        public void DeleteTeacherLecturesForTeacher(int teacherID, SqlConnection conn = null)
        {
            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_deleteTeacherLecturesForTeacher", conn))
                {
                    cmd.Parameters.AddWithValue("@TEACHER_ID", teacherID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    if (conn.State == ConnectionState.Open && nullConnection)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("DeleteTeacherLecturesForTeacher() error. TeacherId: " + teacherID, e);
            }
        }
    }
}