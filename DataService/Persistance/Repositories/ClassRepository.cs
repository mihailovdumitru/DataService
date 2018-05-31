﻿using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Repositories
{
    public class ClassRepository : SqlBase, IClassRepository
    {
        public int AddClass(StudyClass studyClass, SqlConnection conn = null)
        {
            int classID = -1;
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertClass", conn))
            {
                cmd.Parameters.AddWithValue("@NAME", studyClass.Name);

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

            return classID;
        }

        public List<StudyClass> GetClasses(SqlConnection conn = null)
        {
            bool nullConnection = false;
            StudyClass studyClass = new StudyClass();
            List<StudyClass> studyClasses = new List<StudyClass>();

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
                        studyClass.ClassID = DataUtil.GetDataReaderValue<int>("ClassID", reader);
                        studyClass.Name = DataUtil.GetDataReaderValue<string>("Name", reader);
                        studyClasses.Add(studyClass);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return studyClasses;
        }
    }
}