using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Utilities
{
    public static class UtilitiesClass
    {
        public static void CreateConnection(ref bool nullConnection, ref SqlConnection conn, string connectionString)
        {
            nullConnection = false;

            if (conn is null)
            {
                conn = new SqlConnection(connectionString);
                nullConnection = true;
            }
        }
    }
}
