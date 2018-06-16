using Model.DBObjects;
using System.Collections.Generic;

namespace Model.DTO
{
    public class TeacherDto : Teacher
    {
        public List<int> Lectures { get; set; }
    }
}