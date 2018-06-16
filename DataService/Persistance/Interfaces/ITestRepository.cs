using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface ITestRepository
    {
        int AddTest(Test test, SqlConnection conn = null);
        IEnumerable<Test> GetTests(SqlConnection conn = null);
    }
}