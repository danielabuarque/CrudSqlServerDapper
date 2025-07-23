using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSqlServerDapper.Settings
{
    /// <summary>
    /// Class representing application global settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Read property that returns the connection string.
        /// </summary>
        public string ConnectionString { 
            get
            {
                return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBClients;Integrated Security=True";
            }
        }
    }
}
