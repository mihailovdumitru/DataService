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
    public class QuestionRepository: SqlBase, IQuestionRepository
    {

        public int AddQuestion(Question question, SqlConnection conn = null)
        {
            int questionID = -1;
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

            return questionID;
        }
    }
}
