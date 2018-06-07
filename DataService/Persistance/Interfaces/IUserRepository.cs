using Model.DBObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistance.Interfaces
{
    public interface IUserRepository
    {
        int AddOrUpdateUser(User user, SqlConnection conn = null, int userID = -1);
        bool DeleteUser(int userID, SqlConnection conn = null);
    }
}
