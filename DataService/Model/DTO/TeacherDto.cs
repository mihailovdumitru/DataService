using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class TeacherDto : Teacher
    {
        // new
        public List<int> Lectures { get; set; }
    }
}
