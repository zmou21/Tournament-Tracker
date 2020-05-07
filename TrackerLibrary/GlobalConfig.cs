using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static readonly string Provider = "sqloledb";

        public static readonly string DataSource = "service address";

        public static readonly string InitialCatalog = "databasename";

        public static readonly string UserID = "id";

        public static readonly string password = "password";

        public static List<IDataConnection> Connection { get; private set; }

        public static void InitializeConnection(bool database, bool textfiles)
        {
            string ConnectionString = $"Provider={Provider}; Data Source={DataSource};";
            ConnectionString += $"Initial Catalog={InitialCatalog}; User Id={UserID}; Password={password};";

            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();
            }

            if(database)
            {
                //TODO - Create database connection
            }

            if(textfiles)
            {
                //TODO - Create connection for textfiles
            }
        }
    }
}
