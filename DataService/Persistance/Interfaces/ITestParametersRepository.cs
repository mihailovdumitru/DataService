using Model.DBObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface ITestParametersRepository
    {
        bool AddTestParameters(TestParameters testParam, SqlConnection conn = null);
        List<TestParameters> GetTestParameters(SqlConnection conn = null);
    }
}