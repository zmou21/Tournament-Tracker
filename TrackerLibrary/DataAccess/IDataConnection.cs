using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        //SqlConnection SQLDataConnection();
        //string TextFile();
        Models.PrizeModel CreatePrize(Models.PrizeModel model);
    }
}
