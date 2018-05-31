using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Repositories
{
    public class TeacherRepository : SqlBase, ITeacherRepository
    {
        public int AddTeacher(Teacher teacher, SqlConnection conn = null)
        {
            int teacherId = -1;
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertTeacher", conn))
            {
                cmd.Parameters.AddWithValue("@FIRSTNAME", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", teacher.LastName);
                cmd.Parameters.AddWithValue("@EMAIL", teacher.Email);
                cmd.CommandType = CommandType.StoredProcedure;
                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teacherId = DataUtil.GetDataReaderValue<int>("TeacherID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return teacherId;
        }
    }
}
