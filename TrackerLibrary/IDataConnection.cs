using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public interface IDataConnection
    {
        SqlConnection SQLDataConnection();
        //string TextFile();
        PrizeModel CreatePrize(PrizeModel model);
    }
}
