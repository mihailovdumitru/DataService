using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface ILectureRepository
    {
        int AddOrUpdate(Lecture lecture, SqlConnection conn = null, int lectureID = -1);
        List<Lecture> GetLectures(SqlConnection conn = null);
        bool DeleteLecture(int lectureID, SqlConnection conn = null);
    }
}