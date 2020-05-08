using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connection { get; private set; } = new List<IDataConnection>();

        public static void InitializeConnection(bool database, bool textfiles)
        {

            if(database)
            {
                //TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();

                Connection.Add(sql);
            }

            if (textfiles)
            {
                //TODO - Set up txt file connection properly
                TextConnector txt = new TextConnector();

                Connection.Add(txt);
            }
        }
    }
}
