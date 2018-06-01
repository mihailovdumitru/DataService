using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ITeacherLecturesRepository
    {
        void AddTeacherLectures(int teacherID, int lectureID, SqlConnection conn = null);
    }
}
