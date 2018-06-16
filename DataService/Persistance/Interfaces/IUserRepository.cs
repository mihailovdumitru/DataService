using Model.DBObjects;
using System.Data.SqlClient;

namespace Persistance.Interfaces
{
    public interface IUserRepository
    {
        int AddOrUpdateUser(User user, SqlConnection conn = null, int userID = -1);
        bool DeleteUser(int userID, SqlConnection conn = null);
        User GetUserByUsername(string username, SqlConnection conn = null);
    }
}