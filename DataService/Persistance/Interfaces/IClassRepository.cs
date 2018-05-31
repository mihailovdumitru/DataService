using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface IClassRepository
    {
        int AddClass(StudyClass studyClass, SqlConnection conn = null);
        List<StudyClass> GetClasses(SqlConnection conn = null);
    }
}
