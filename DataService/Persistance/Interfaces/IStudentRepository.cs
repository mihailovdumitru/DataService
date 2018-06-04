using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface IStudentRepository
    {
        int AddOrUpdateStudent(Student student, SqlConnection conn = null, int studentID = -1);
        List<Student> GetStudents(SqlConnection conn = null);
    }
}
