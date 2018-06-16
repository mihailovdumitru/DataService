using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ITestRepository
    {
        int AddTest(Test test, SqlConnection conn = null);
        IEnumerable<Test> GetTests(SqlConnection conn = null);
        int UpdateTest(Test test, SqlConnection conn = null);
    }
}
