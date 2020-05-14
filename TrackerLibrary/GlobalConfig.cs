using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        public static string CnnString(string connection)
        {
            //pass in name of the connection i.e. TournamentTracker
            //var c = ConfigurationManager.AppSettings[];
            return ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }

        public static void InitializeConnection(DatabaseType db)
        {
            if (db == DatabaseType.SQL)
            {
                SqlConnector sql = new SqlConnector();

                Connection = sql;
            }
            else if(db == DatabaseType.Text)
            {
                TextConnector txt = new TextConnector();

                Connection = txt;
            }
        }
    }
}
