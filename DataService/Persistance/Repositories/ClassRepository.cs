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
    public class ClassRepository : SqlBase, IClassRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int AddOrUpdateClass(StudyClass studyClass, SqlConnection conn = null, int classID = -1)
        {
            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_insertOrUpdateClass", conn))
                {
                    cmd.Parameters.AddWithValue("@NAME", studyClass.Name);
                    cmd.Parameters.AddWithValue("@CLASS_ID", classID);
                    cmd.Parameters.AddWithValue("@IS_ACTIVE", studyClass.IsValid);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classID = DataUtil.GetDataReaderValue<int>("ClassID", reader);
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
                _log.Error("AddOrUpdateClass() error. Class: " + studyClass.Name, e);
            }

            return classID;
        }

        public List<StudyClass> GetClasses(SqlConnection conn = null)
        {
            List<StudyClass> studyClasses = new List<StudyClass>();

            try
            {
                bool nullConnection = false;
                StudyClass studyClass = null;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_getClasses", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studyClass = new StudyClass
                            {
                                ClassID = DataUtil.GetDataReaderValue<int>("ClassID", reader),
                                Name = DataUtil.GetDataReaderValue<string>("Name", reader)
                            };

                            studyClasses.Add(studyClass);
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
                _log.Error("GetClasses() error. ", e);
            }

            return studyClasses;
        }

        public bool DeleteClass(int studyClassID, SqlConnection conn = null)
        {
            bool succes = true;

            try
            {
                bool nullConnection = false;

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_deleteClass", conn))
                {
                    cmd.Parameters.AddWithValue("@CLASS_ID", studyClassID);
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
                _log.Error("DeleteClass() error. ClassId: " + studyClassID, e);
            }

            return succes;
        }
    }
}