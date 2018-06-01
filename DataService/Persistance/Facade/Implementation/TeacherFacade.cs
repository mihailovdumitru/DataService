﻿using AutoMapper;
using Model.DBObjects;
using Model.DTO;
using Persistance.Facade.Interfaces;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Facade.Implementation
{
    public class TeacherFacade : SqlBase, ITeacherFacade
    {
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
            Teacher teacherObj = null;

            using (var conn = new SqlConnection(base.GetConnectionString()))
            {
                conn.Open();
                teacherObj = mapper.Map<TeacherDto, Teacher>(teacher);
                teacherID = teacherRepo.AddTeacher(teacherObj, conn);
                foreach (var lecture in teacher.Lectures)
                {
                    teacherLecturesRepo.AddTeacherLectures(teacherID, lecture, conn);
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return teacherID;
        }
    }
}
