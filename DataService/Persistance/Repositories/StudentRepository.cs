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
    public class StudentRepository : SqlBase, IStudentRepository
    {
        public int AddOrUpdateStudent(Student student, SqlConnection conn = null, int studentID = -1)
        {
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertOrUpdateStudent", conn))
            {
                cmd.Parameters.AddWithValue("@FIRSTNAME", student.FirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", student.LastName);
                cmd.Parameters.AddWithValue("@EMAIL", student.Email);
                cmd.Parameters.AddWithValue("@CLASS_ID", student.ClassID);
                cmd.Parameters.AddWithValue("@STUDENT_ID", studentID);
                cmd.CommandType = CommandType.StoredProcedure;
                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentID = DataUtil.GetDataReaderValue<int>("StudentID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return studentID;
        }

        public List<Student> GetStudents(SqlConnection conn = null)
        {
            bool nullConnection = false;
            Student student = null;
            List<Student> students = new List<Student>();

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_getStudents", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        student = new Student
                        {
                            StudentID = DataUtil.GetDataReaderValue<int>("StudentID", reader),
                            FirstName = DataUtil.GetDataReaderValue<string>("FirstName", reader),
                            LastName = DataUtil.GetDataReaderValue<string>("LastName", reader),
                            Email = DataUtil.GetDataReaderValue<string>("Email", reader),
                            ClassID = DataUtil.GetDataReaderValue<int>("ClassID", reader)
                        };
                        students.Add(student);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return students;
        }
    }
}
