using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface ITestParametersRepository
    {
        bool AddTestParameters(TestParameters testParam, SqlConnection conn = null);
    }
}
