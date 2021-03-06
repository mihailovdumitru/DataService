﻿using log4net;
using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class StudentRepository : SqlBase, IStudentRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int AddOrUpdateStudent(Student student, SqlConnection conn = null, int studentID = -1)
        {
            try
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
                    cmd.Parameters.AddWithValue("@USER_ID", student.UserID);
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
            }
            catch (Exception e)
            {
                _log.Error("AddOrUpdateStudent() error. Student: " + student.FirstName + " " + student.LastName, e);
            }

            return studentID;
        }

        public List<Student> GetStudents(SqlConnection conn = null)
        {
            List<Student> students = new List<Student>();

            try
            {
                bool nullConnection = false;
                Student student = null;

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
                                ClassID = DataUtil.GetDataReaderValue<int>("ClassID", reader),
                                UserID = DataUtil.GetDataReaderValue<int>("UserID", reader)
                            };

                            students.Add(student);
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
                _log.Error("GetStudents() error.", e);
            }

            return students;
        }
    }
}