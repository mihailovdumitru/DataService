using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ITestResultsRepository
    {
        bool AddTestResults(TestResults testResults, SqlConnection conn = null);
        List<TestResults> GetTestsResults(SqlConnection conn = null);
    }
}
