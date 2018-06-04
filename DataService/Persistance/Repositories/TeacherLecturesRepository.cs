using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Repositories
{
    public class TeacherLecturesRepository : SqlBase,ITeacherLecturesRepository
    {
        public void AddTeacherLectures(int teacherID, int lectureID, SqlConnection conn = null)
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


        public void DeleteTeacherLecturesForTeacher(int teacherID, SqlConnection conn = null)
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
    }
}
