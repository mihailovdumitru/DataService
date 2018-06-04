using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ILectureRepository
    {
        int AddOrUpdate(Lecture lecture, SqlConnection conn = null, int lectureID = -1);
        List<Lecture> GetLectures(SqlConnection conn = null);
    }
}
