using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ITeacherRepository
    {
        int AddTeacher(Teacher teacher, SqlConnection conn = null);
    }
}
