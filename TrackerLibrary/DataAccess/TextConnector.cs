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
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModel.csv";

        public PrizeModel CreatePrize(PrizeModel model)
        {
            //load txt file and convert txt to list<prizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConverttoPrizeModel();

            //finds max ID
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            //add the new record with the new ID (naxID +=1)
            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> person = PeopleFile.FullFilePath().LoadFile().ConverttoPersonModel();

            int currentId = 1;
            if(person.Count > 0)
            {
                currentId = person.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            person.Add(model);
            person.SaveToPersonFile(PeopleFile);
            return model;
        }

        public List<PersonModel> GetPerson_All()
        {
            throw new NotImplementedException();
        }
    }
}
