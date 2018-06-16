using Model.DBObjects;
using Model.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface ITeacherRepository
    {
        int AddTeacher(Teacher teacher, SqlConnection conn = null, int teacherId = -1);
        List<TeacherWithLecturesDto> GetTeachersWithLectures(SqlConnection conn = null);
        List<TeacherDto> GetTeachers(SqlConnection conn = null);
        Teacher GetTeacherUserAuth(string email, SqlConnection conn = null);
    }
}