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
    public class LectureRepository : SqlBase, ILectureRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly LectureRepository _instance = new LectureRepository();

        public int AddOrUpdate(Lecture lecture, SqlConnection conn = null, int lectureID = -1)
        {
            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_insertOrUpdateLecture", conn))
                {
                    cmd.Parameters.AddWithValue("@NAME", lecture.Name);
                    cmd.Parameters.AddWithValue("@YEAR_OF_STUDY", lecture.YearOfStudy);
                    cmd.Parameters.AddWithValue("@LECTURE_ID", lectureID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lectureID = DataUtil.GetDataReaderValue<int>("LectureID", reader);
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
                _log.Error("AddOrUpdate() error. Lecture: " + lecture.Name, e);
            }

            return lectureID;
        }

        public List<Lecture> GetLectures(SqlConnection conn = null)
        {
            List<Lecture> lectures = new List<Lecture>();

            try
            {
                bool nullConnection = false;
                Lecture lecture = null;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_getLectures", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lecture = new Lecture
                            {
                                LectureID = DataUtil.GetDataReaderValue<int>("LectureID", reader),
                                Name = DataUtil.GetDataReaderValue<string>("Name", reader),
                                YearOfStudy = DataUtil.GetDataReaderValue<int>("YearOfStudy", reader)
                            };

                            lectures.Add(lecture);
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
                _log.Error("GetLectures() error.", e);
            }

            return lectures;
        }

        public bool DeleteLecture(int lectureID, SqlConnection conn = null)
        {
            bool succes = true;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_deleteLecture", conn))
                {
                    cmd.Parameters.AddWithValue("@LECTURE_ID", lectureID);
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
            catch (Exception e)
            {
                _log.Error("DeleteLecture() error. LectureId: " + lectureID, e);
            }

            return succes;
        }
    }
}