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
    public class AnswerRepository : SqlBase, IAnswerRespository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int AddAnswer(Answer answer, SqlConnection conn = null)
        {
            int answerID = -1;

            try
            {
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
            }
            catch (Exception e)
            {
                _log.Error("AddAnswer() error. Answer: " + answer.AnswerID, e);
            }

            return answerID;
        }

        public int UpdateAnswer(Answer answer, SqlConnection conn = null)
        {
            int answerID = -1;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_updateAnswer", conn))
                {
                    cmd.Parameters.AddWithValue("@ANSWER", answer.Content);
                    cmd.Parameters.AddWithValue("@ANSWER_ID", answer.AnswerID);

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
            }
            catch (Exception e)
            {
                _log.Error("UpdateAnswer() error. Answer: " + answer.AnswerID, e);
            }

            return answerID;
        }

        public List<Answer> GetAnswers(SqlConnection conn = null)
        {
            List<Answer> answers = new List<Answer>();

            try
            {
                bool nullConnection = false;
                Answer answer = null;
                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

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
            }
            catch (Exception e)
            {
                _log.Error("GetAnswers() error.", e);
            }

            return answers;
        }
    }
}