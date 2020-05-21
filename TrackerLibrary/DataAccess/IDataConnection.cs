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
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel p);
        TeamModel CreateTeam(TeamModel T);
        TournamentModel CreateTournament(TournamentModel model);
    }
}
