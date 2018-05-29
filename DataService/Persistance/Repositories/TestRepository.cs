using Model.Teacher;
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

        public void CreateConnection(ref bool nullConnection, ref SqlConnection conn)
        {
            nullConnection = false;

            if (conn is null)
            {
                conn = new SqlConnection(base.GetConnectionString());
                nullConnection = true;
            }
        }

        public void AddQuestionAnswer(int questionID, int answerID, bool correct, SqlConnection conn = null)
        {
            bool nullConnection = false;

            CreateConnection(ref nullConnection, ref conn);

            using (var cmd = new SqlCommand("sp_insertQuestionAnswers", conn))
            {
                cmd.Parameters.AddWithValue("@QUESTION_ID", questionID);
                cmd.Parameters.AddWithValue("@ANSWER_ID", answerID);
                cmd.Parameters.AddWithValue("@CORRECT", correct);

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

        public int AddAnswer(Answer answer, SqlConnection conn = null)
        {
            int answerID = -1;
            bool nullConnection = false;

            CreateConnection(ref nullConnection, ref conn);

            using (var cmd = new SqlCommand("sp_InsertAnswer", conn))
            {
                cmd.Parameters.AddWithValue("@ANSWER", answer.Content);

                cmd.CommandType = CommandType.StoredProcedure;
                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        answerID = DataUtil.GetDataReaderValue<int>("AnswerID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return answerID;
        }


        public int AddQuestion(Question question, SqlConnection conn = null)
        {
            int questionID = -1;
            bool nullConnection = false;

            CreateConnection(ref nullConnection, ref conn);

            using (var cmd = new SqlCommand("sp_InsertQuestion", conn))
            {
                cmd.Parameters.AddWithValue("@QUESTION", question.Content);
                cmd.Parameters.AddWithValue("@POINTS", question.Points);
                cmd.Parameters.AddWithValue("@TEST_ID", question.TestID);
                cmd.CommandType = CommandType.StoredProcedure;

                if(nullConnection)
                    conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questionID = DataUtil.GetDataReaderValue<int>("QuestionID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return questionID;
        }

        public int AddTest(Test test, SqlConnection conn = null)
        {
            int testID = -1;
            bool nullConnection = false;

            CreateConnection(ref nullConnection, ref conn);

            using (var cmd = new SqlCommand("sp_InsertTest", conn))
            {
                cmd.Parameters.AddWithValue("@NAME", test.Naming);
                cmd.Parameters.AddWithValue("@TEACHER_ID", test.TeacherID);
                cmd.Parameters.AddWithValue("@DISCIPLINE_ID", test.LectureID);
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

        public int AddTeacher(Teacher teacher)
        {
            int teacherId = -1;
            using (var conn = new SqlConnection(base.GetConnectionString()))
            using (var cmd = new SqlCommand("sp_InsertTeacher", conn))
            {
                cmd.Parameters.AddWithValue("@FIRSTNAME", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", teacher.Lastname);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teacherId = DataUtil.GetDataReaderValue<int>("TeacherID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return teacherId;
        }

    }
}
