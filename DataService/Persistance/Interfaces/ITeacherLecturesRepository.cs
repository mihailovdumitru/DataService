using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface ITeacherLecturesRepository
    {
        void AddTeacherLectures(int teacherID, int lectureID, SqlConnection conn = null);
        void DeleteTeacherLecturesForTeacher(int teacherID, SqlConnection conn = null);
    }
}