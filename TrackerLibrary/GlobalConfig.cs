using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connection { get; private set; }

        public static void InitializeConnection(bool database, bool textfiles)
        {

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
