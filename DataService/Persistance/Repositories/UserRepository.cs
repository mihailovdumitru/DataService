using log4net;
using Model.DBObjects;
using Persistance.Interfaces;
using Persistance.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Repositories
{
    public class UserRepository : SqlBase, IUserRepository
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int AddOrUpdateUser(User user, SqlConnection conn = null, int userID = -1)
        {
            try
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
            }
            catch (Exception e)
            {
                _log.Error("AddOrUpdateUser() error. UserName: " + user.Username, e);
            }

            return userID;
        }

        public bool DeleteUser(int userID, SqlConnection conn = null)
        {
            bool succes = true;

            try
            {
                bool nullConnection = false;

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
            }
            catch (Exception e)
            {
                _log.Error("DeleteUser() error. UserId: " + userID, e);
            }

            return succes;
        }

        public User GetUserByUsername(string username, SqlConnection conn = null)
        {
            User user = null;

            try
            {
                bool nullConnection = false;
                List<User> studyClasses = new List<User>();

                UtilitiesClass.CreateConnection(ref nullConnection, ref conn, base.GetConnectionString());

                using (var cmd = new SqlCommand("sp_getUserByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USERNAME", username);

                    if (nullConnection)
                        conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User
                            {
                                UserID = DataUtil.GetDataReaderValue<int>("UserID", reader),
                                Username = DataUtil.GetDataReaderValue<string>("Username", reader),
                                Password = DataUtil.GetDataReaderValue<string>("Password", reader),
                                IsActive = DataUtil.GetDataReaderValue<bool>("IsActive", reader),
                                Role = DataUtil.GetDataReaderValue<string>("Role", reader)
                            };
                        }
                    }

                    if (conn.State == ConnectionState.Open && nullConnection)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("GetUserByUsername() error. Username: " + user.Username, e);
            }

            return user;
        }
    }
}