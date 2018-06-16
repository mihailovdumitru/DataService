using System.Data.SqlClient;

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