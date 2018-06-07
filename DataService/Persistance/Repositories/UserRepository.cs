using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Repositories
{
    public class UserRepository: SqlBase, IUserRepository
    {

        public int AddOrUpdateUser(User user, SqlConnection conn = null, int userID = -1)
        {
            bool nullConnection = false;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_insertOrUpdateUser", conn))
            {
                cmd.Parameters.AddWithValue("@USERNAME", user.Username);
                cmd.Parameters.AddWithValue("@PASSWORD", user.Password);
                cmd.Parameters.AddWithValue("@ROLE", user.Role);
                cmd.Parameters.AddWithValue("@USER_ID", userID);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", user.IsActive);
                cmd.CommandType = CommandType.StoredProcedure;
                if (nullConnection)
                    conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userID = DataUtil.GetDataReaderValue<int>("UserID", reader);
                    }
                }
                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return userID;
        }

        public bool DeleteUser(int userID, SqlConnection conn = null)
        {
            bool nullConnection = false;
            bool succes = true;

            UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

            using (var cmd = new SqlCommand("sp_deleteUser", conn))
            {
                cmd.Parameters.AddWithValue("@USER_ID", userID);

                cmd.CommandType = CommandType.StoredProcedure;
                if (nullConnection)
                    conn.Open();
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open && nullConnection)
                {
                    conn.Close();
                }
            }

            return succes;
        }
    }
}
