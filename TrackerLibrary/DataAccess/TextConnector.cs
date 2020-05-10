using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        public PrizeModel CreatePrize(PrizeModel model)
        {

            private const string PrizesFile = "PrizeModels.csv";

            TextConnectorProcess.FullFilePath(PrizesFile);

        //load txt file
        //convert txt to list<prizeModel>
        //find the max ID
        //add the new record with the new ID (naxID +=1)
        //convert the prizes to list<string>
        //save list<string> to txt file
    }
    }
}
