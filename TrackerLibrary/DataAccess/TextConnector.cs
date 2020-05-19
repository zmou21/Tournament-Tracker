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
        private const string TeamsFile = "TeamModel.csv";

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConverttoPersonModel();
        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamsFile.FullFilePath().LoadFile().ConvertToTeamModel(PeopleFile);
        }


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
                currentId = person.OrderByDescending(x => x.PeopleID).First().PeopleID + 1;
            }
            model.PeopleID = currentId;

            person.Add(model);
            person.SaveToPersonFile(PeopleFile);
            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> team = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModel(PeopleFile);

            int currentId = 1;
            if (team.Count > 0)
            {
                currentId = team.OrderByDescending(x => x.TeamModelID).First().TeamModelID + 1;
            }
            model.TeamModelID = currentId;

            team.Add(model);
            team.SaveToTeamFile(TeamsFile);
            return model;
        }

        public TournamentModel CreateTournament(TournamentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
