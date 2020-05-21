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
        List<PersonModel> GetPerson_All();
        List<TeamModel> GetTeam_All();
        List<TournamentModel> GetTournament_All();
        //Todo convert all of the create methods to return void
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel p);
        TeamModel CreateTeam(TeamModel T);
        void CreateTournament(TournamentModel model);
    }
}
