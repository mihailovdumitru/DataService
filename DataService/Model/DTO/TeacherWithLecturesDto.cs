using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class TeacherWithLecturesDto:Teacher
    {
        public List<Lecture> Lectures { get; set; }
    }
}
