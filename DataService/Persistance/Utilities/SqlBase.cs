using System.Configuration;

namespace Persistance.Utilities
{
    public abstract class SqlBase
    {
        protected string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DisDbConnectionString"].ConnectionString;
        }
    }
}