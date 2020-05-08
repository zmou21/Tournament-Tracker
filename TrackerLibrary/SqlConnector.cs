using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TrackerLibrary
{
    public class SqlConnector : IDataConnection
    {
        public static readonly string Provider = "sqloledb";

        public static readonly string DataSource = "service address";

        public static readonly string InitialCatalog = "databasename";

        public static readonly string UserID = "id";

        public static readonly string password = "password";

        public PrizeModel CreatePrize(PrizeModel model)
        {
            return model;
        }

        public static void Connect()
        {
            string ConnectionString = $"Provider={Provider}; Data Source={DataSource};";
            ConnectionString += $"Initial Catalog={InitialCatalog}; User Id={UserID}; Password={password};";

            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();
            }
        }
    }
}
