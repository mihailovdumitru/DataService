using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

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
