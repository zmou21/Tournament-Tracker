using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        //SqlConnection SQLDataConnection();
        //string TextFile();
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel p);
    }
}
