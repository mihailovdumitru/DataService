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
    public class QuestionAnswerRepository : SqlBase, IQuestionAnswerRespository
    {
        public void AddOrUpdateQuestionAnswer(int questionID, int answerID, bool correct, SqlConnection conn = null)
        {
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

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

        public List<QuestionWithAnswers> GetQuestionAnswers(SqlConnection conn = null)
        {
            bool nullConnection = false;
            QuestionWithAnswers questionAnswer = null;
            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());
            List<QuestionWithAnswers> questionsWithAnswers = new List<QuestionWithAnswers>();

            using (var cmd = new SqlCommand("sp_getQuestionAnswers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questionAnswer = new QuestionWithAnswers{                          
                            QuestionID = DataUtil.GetDataReaderValue<int>("QuestionID", reader),
                            AnswerID = DataUtil.GetDataReaderValue<int>("AnswerID", reader),
                            Correct = DataUtil.GetDataReaderValue<bool>("Correct", reader)
                        };
                        questionsWithAnswers.Add(questionAnswer);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return questionsWithAnswers;
        }

    }
}
