using Model.DBObjects;
using Model.DTO;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Persistance.Repositories
{
    public class TeacherRepository : SqlBase, ITeacherRepository
    {
        public int AddTeacher(Teacher teacher, SqlConnection conn = null, int teacherId = -1)
        {
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertOrUpdateTeacher", conn))
            {
                cmd.Parameters.AddWithValue("@FIRSTNAME", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", teacher.LastName);
                cmd.Parameters.AddWithValue("@EMAIL", teacher.Email);
                cmd.Parameters.AddWithValue("@TEACHER_ID", teacherId);
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
        public List<TeacherWithLecturesDto> GetTeachersWithLectures(SqlConnection conn = null)
        {
            bool nullConnection = false;
            TeacherWithLecturesDto teacher = null;
            List<TeacherWithLecturesDto> teachers = new List<TeacherWithLecturesDto>();
            Lecture lecture = null;
            int teacherIndex = -1;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_getTeachersWithLectures", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teacher = new TeacherWithLecturesDto
                        {
                            TeacherID = DataUtil.GetDataReaderValue<int>("TeacherID", reader),
                            FirstName = DataUtil.GetDataReaderValue<string>("FirstName", reader),
                            LastName = DataUtil.GetDataReaderValue<string>("LastName", reader),
                            Email = DataUtil.GetDataReaderValue<string>("Email", reader),
                            Lectures = new List<Lecture>()
                        };

                        lecture = new Lecture
                        {
                            LectureID = DataUtil.GetDataReaderValue<int>("LectureID", reader),
                            Name = DataUtil.GetDataReaderValue<string>("Name", reader),
                            YearOfStudy = DataUtil.GetDataReaderValue<int>("YearOfStudy", reader)
                        };

                        teacherIndex = teachers.FindIndex(teacherObj => teacherObj.TeacherID == teacher.TeacherID);
                        if(teacherIndex != -1)
                        {
                            teachers[teacherIndex].Lectures.Add(lecture);
                        }
                        else
                        {
                            teacher.Lectures.Add(lecture);
                            teachers.Add(teacher);
                        }
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return teachers;
        }

        public List<TeacherDto> GetTeachers(SqlConnection conn = null)
        {
            bool nullConnection = false;
            TeacherDto teacher = null;
            List<TeacherDto> teachers = new List<TeacherDto>();
            int teacherIndex = 0;
            int lectureID = 0;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_getTeachersWithLectures", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teacher = new TeacherDto()
                        {
                            TeacherID = DataUtil.GetDataReaderValue<int>("TeacherID", reader),
                            FirstName = DataUtil.GetDataReaderValue<string>("FirstName", reader),
                            LastName = DataUtil.GetDataReaderValue<string>("LastName", reader),
                            Email = DataUtil.GetDataReaderValue<string>("Email", reader),
                            Lectures = new List<int>()
                        };
                        lectureID = DataUtil.GetDataReaderValue<int>("LectureID", reader);

                        teacherIndex = teachers.FindIndex(teacherObj => teacherObj.TeacherID == teacher.TeacherID);
                        if (teacherIndex != -1 && lectureID > 0)
                        {

                            teachers[teacherIndex].Lectures.Add(lectureID);
                        }
                        else
                        {
                            if (lectureID > 0)
                            {
                                teacher.Lectures.Add(lectureID);
                            }
                            teachers.Add(teacher);
                        }
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return teachers;
        }
    }


}
