using log4net;
using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class QuestionRepository : SqlBase, IQuestionRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int AddQuestion(Question question, SqlConnection conn = null)
        {
            int questionID = -1;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_insertQuestion", conn))
                {
                    cmd.Parameters.AddWithValue("@QUESTION", question.Content);
                    cmd.Parameters.AddWithValue("@POINTS", question.Points);
                    cmd.Parameters.AddWithValue("@TEST_ID", question.TestID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
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
            }
            catch (Exception e)
            {
                _log.Error("AddQuestion() error. QuestionId: " + question.QuestionID, e);
            }

            return questionID;
        }

        public int UpdateQuestion(Question question, SqlConnection conn = null)
        {
            int questionID = -1;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_updateQuestion", conn))
                {
                    cmd.Parameters.AddWithValue("@QUESTION", question.Content);
                    cmd.Parameters.AddWithValue("@POINTS", question.Points);
                    cmd.Parameters.AddWithValue("@TEST_ID", question.TestID);
                    cmd.Parameters.AddWithValue("@QUESTION_ID", question.QuestionID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
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
            }
            catch (Exception e)
            {
                _log.Error("UpdateQuestion() error. QuestionId: " + question.QuestionID, e);
            }

            return questionID;
        }

        public List<Question> GetQuestionsByTestID(int testID, SqlConnection conn = null)
        {
            List<Question> questions = new List<Question>();

            try
            {
                bool nullConnection = false;
                Question question = null;
                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_getQuestionsByTestID", conn))
                {
                    cmd.Parameters.AddWithValue("@TEST_ID", testID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            question = new Question
                            {
                                QuestionID = DataUtil.GetDataReaderValue<int>("QuestionID", reader),
                                Content = DataUtil.GetDataReaderValue<string>("Question", reader),
                                Points = DataUtil.GetDataReaderValue<int>("Points", reader),
                                TestID = DataUtil.GetDataReaderValue<int>("TestID", reader)
                            };

                            questions.Add(question);
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
                _log.Error("GetQuestionsByTestID() error. TestId: " + testID, e);
            }

            return questions;
        }
    }
}