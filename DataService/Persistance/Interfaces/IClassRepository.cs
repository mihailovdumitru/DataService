using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface IClassRepository
    {
        int AddOrUpdateClass(StudyClass studyClass, SqlConnection conn = null, int classID = -1);
        List<StudyClass> GetClasses(SqlConnection conn = null);
        bool DeleteClass(int studyClassID, SqlConnection conn = null);
    }
}