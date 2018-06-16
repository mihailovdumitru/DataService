using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class AnswerRepository : SqlBase, IAnswerRespository
    {
        public int AddAnswer(Answer answer, SqlConnection conn = null)
        {
            int answerID = -1;
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertAnswer", conn))
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

        public List<Answer> GetAnswers(SqlConnection conn = null)
        {
            bool nullConnection = false;
            Answer answer = null;
            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());
            List<Answer> answers = new List<Answer>();

            using (var cmd = new SqlCommand("sp_getAnswers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (nullConnection)
                    conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        answer = new Answer
                        {
                            AnswerID = DataUtil.GetDataReaderValue<int>("AnswerID", reader),
                            Content = DataUtil.GetDataReaderValue<string>("Answer", reader)
                        };

                        answers.Add(answer);
                    }
                }

                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return answers;
        }
    }
}