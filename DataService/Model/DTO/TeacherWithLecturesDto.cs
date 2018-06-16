using Model.DBObjects;
using System.Collections.Generic;

namespace Model.DTO
{
    public class TeacherWithLecturesDto : Teacher
    {
        public List<Lecture> Lectures { get; set; }
    }
}