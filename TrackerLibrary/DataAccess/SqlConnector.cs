using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TrackerLibrary.Models;
using System.Data;
using Dapper;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "TournamentTracker"; 
    
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">the prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //IDbConnection interface is extensible to text file connection, mysql, etc.
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@placeNumber", model.PlaceNumber);
                p.Add("@placeName", model.PlaceName);
                p.Add("@prizeAmount", model.PrizeAmount);
                p.Add("@prizePercentage", model.PrizePercentage);
                //Using dapper library, this is the way to output a value from the sql db
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }   
        }

        /// <summary>
        /// Create and saves a new person created on the create team form
        /// </summary>
        /// <param name="model">personal ID information</param>
        /// <returns>The person created and a unique identifier</returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.PhoneNumber);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("[dbo].[spPeople_Insert]", p, commandType: CommandType.StoredProcedure);

                model.PeopleID = p.Get<int>("@Id");
                return model;
            }
        }
        //This is the method/process to get data from a DB and send it into a list
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> getPersons;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                getPersons = connection.Query<PersonModel>("dbo.spGetPeople").ToList();
            }
            return getPersons;
        }

        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> getTeams;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                getTeams = connection.Query<TeamModel>("dbo.spGet_Team").ToList();

                foreach (TeamModel t in getTeams)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId", t.TeamModelID);
                    t.TeamMembers = connection.Query<PersonModel>("dbo.spGetByTeam_TeamMembers", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return getTeams;
        }

        public List<TournamentModel> GetTournament_All()
        {
            List<TournamentModel> getTournaments;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                getTournaments = connection.Query<TournamentModel>("dbo.spGet_Tournaments").ToList();
                foreach (var tour in getTournaments)
                {
                    var p = new DynamicParameters();
                    p.Add("@TournamentId", tour.Id);
                    //TODO - add the rest of these fields
                    //tour.TeamMembers = connection.Query<PersonModel>("dbo.spGetByTeam_TeamMembers", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            return getTournaments;
        }

        public TeamModel CreateTeam(TeamModel T)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", T.TeamName);
                p.Add("@TeamId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("[dbo].[spInsert_Teams]", p, commandType: CommandType.StoredProcedure);

                T.TeamModelID = p.Get<int>("@TeamId");

                foreach(var t in T.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", T.TeamModelID);
                    p.Add("@PeopleID", t.PeopleID);

                    connection.Execute("dbo.spInsert_TeamMembers", p, commandType: CommandType.StoredProcedure);

                }

                return T;
            }
        }

        public TournamentModel CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveTournament(model, connection);
                SaveTournamentPrizes(model, connection);
                SaveTournamentTeams(model, connection);

                return model;
            }
        }

        private void SaveTournamentTeams(TournamentModel model, IDbConnection connection)
        {
            foreach (var team in model.EnteredTeams)
            {
                var t = new DynamicParameters();
                t.Add("@TournamentID", model.Id);
                t.Add("@TeamID", team.TeamModelID);

                connection.Execute("dbo.spInsert_TournamentTeams", t, commandType: CommandType.StoredProcedure);
            }
        }

        private void SaveTournamentPrizes(TournamentModel model, IDbConnection connection)
        {
            foreach (var prize in model.Prizes)
            {
                var t = new DynamicParameters();
                t.Add("@TournamentID", model.Id);
                t.Add("@PrizeID", prize.Id);

                connection.Execute("dbo.spInsert_TournamentPrizes", t, commandType: CommandType.StoredProcedure);
            }

        }

        private void SaveTournament(TournamentModel model, IDbConnection connection)
        {
            var t = new DynamicParameters();
            t.Add("@TournamentName", model.TournamentName);
            t.Add("@EntryFee", model.EntryFee);
            t.Add("@Active", 1);
            t.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            connection.Execute("[dbo].[spInsert_Tournament]", t, commandType: CommandType.StoredProcedure);
            model.Id = t.Get<int>("Id");
        }

    }
}
