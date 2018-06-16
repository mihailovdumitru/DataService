using AutoMapper;
using log4net;
using Model.DBObjects;
using Model.DTO;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Facade.Implementation
{
    public class TeacherFacade : SqlBase, ITeacherFacade
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IMapper mapper;
        private readonly ITeacherRepository teacherRepo;
        private readonly ITeacherLecturesRepository teacherLecturesRepo;

        public TeacherFacade(IMapper mapper, ITeacherRepository teacherRepo, ITeacherLecturesRepository teacherLecturesRepo)
        {
            this.mapper = mapper;
            this.teacherRepo = teacherRepo;
            this.teacherLecturesRepo = teacherLecturesRepo;
        }

        public int AddTeacher(TeacherDto teacher)
        {
            int teacherID = -1;

            try
            {
                Teacher teacherObj = null;

                using (var conn = new SqlConnection(base.GetConnectionString()))
                {
                    conn.Open();
                    teacherObj = mapper.Map<TeacherDto, Teacher>(teacher);
                    teacherID = teacherRepo.AddTeacher(teacherObj, conn);
                    if (teacher.Lectures != null)
                    {
                        foreach (var lecture in teacher.Lectures)
                        {
                            teacherLecturesRepo.AddTeacherLectures(teacherID, lecture, conn);
                        }
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("AddTeacher() error. Teacher: " + teacher.FirstName + " " + teacher.LastName, e);
            }

            return teacherID;
        }

        public int UpdateTeacher(TeacherDto teacher, int teacherID)
        {
            try
            {
                Teacher teacherObj = null;

                using (var conn = new SqlConnection(base.GetConnectionString()))
                {
                    conn.Open();
                    teacherObj = mapper.Map<TeacherDto, Teacher>(teacher);
                    teacherID = teacherRepo.AddTeacher(teacherObj, conn, teacherID);
                    teacherLecturesRepo.DeleteTeacherLecturesForTeacher(teacherID, conn);

                    if (teacher.Lectures != null)
                    {
                        foreach (var lectureID in teacher.Lectures)
                        {
                            teacherLecturesRepo.AddTeacherLectures(teacherID, lectureID, conn);
                        }
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("UpdateTeacher() error. Teacher: " + teacher.FirstName + " " + teacher.LastName, e);
            }

            return teacherID;
        }
    }
}