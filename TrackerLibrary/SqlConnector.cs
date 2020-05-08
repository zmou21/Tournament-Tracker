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
        //TODO - Make the create prize method save to the database
        
        /// <summary>
        /// These readonly fields all instantiate a new database connection
        /// </summary>
        public static readonly string Provider = "sqloledb";

        public static readonly string DataSource = "service address";

        public static readonly string InitialCatalog = "databasename";

        public static readonly string UserID = "id";

        public static readonly string password = "password";

        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">the prize information</param>
        /// <returns>The prize informaiton, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;

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
