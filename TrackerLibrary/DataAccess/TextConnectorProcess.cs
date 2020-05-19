using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcess
    {

        public static string FullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        public static List<string> LoadFile(this string file)
        {
            //check if file exists
            if(!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        /// <summary>
        /// Converts the prize model data from the flat file to be used
        /// </summary>
        /// <param name="prizeData"></param>
        /// <returns></returns>
        public static List<PrizeModel> ConverttoPrizeModel(this List<string> prizeData)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (var line in prizeData)
            {

                //split the CSV file and push values into a new array 
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);

                output.Add(p);
            }

            return output;
        }

        public static List<PersonModel> ConverttoPersonModel(this List<string> personModel)
        {
            List<PersonModel> people = new List<PersonModel>();

            foreach (var lines in personModel)
            {
                //split the CSV file and push values into a new array 
                string[] cols = lines.Split(',');

                PersonModel p = new PersonModel();
                p.PeopleID = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.PhoneNumber = cols[4];

                people.Add(p);
            }
            return people;
        }

        public static List<TeamModel> ConvertToTeamModel(this List<string> teamModel, string peopleFileName)
        {
            List<TeamModel> team = new List<TeamModel>();
            List<PersonModel> person = peopleFileName.FullFilePath().LoadFile().ConverttoPersonModel();

            foreach (var lines in teamModel)
            {
                //split the CSV file and push values into a new array 
                string[] cols = lines.Split(',');

                TeamModel t = new TeamModel();
                t.TeamModelID = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIDs = cols[2].Split('|');
                foreach (string id in personIDs)
                {
                    //uses LINQ to filter and parse the ids by comparing the IDs between the two
                    t.TeamMembers.Add(person.Where(x => x.PeopleID == int.Parse(id)).First());
                }
                
                team.Add(t);
            }
            return team;
        }

        public static List<TournamentModel> ConvertToTournamentModel(this List<string> tourModel, string teamFileName, string prizeFileName, string personFileName)
        {
            List<TournamentModel> tour = new List<TournamentModel>();
            List<TeamModel> team = teamFileName.FullFilePath().LoadFile().ConvertToTeamModel(personFileName);
            List<PrizeModel> prize = prizeFileName.FullFilePath().LoadFile().ConverttoPrizeModel();

            foreach(var lines in tourModel)
            {
                string[] cols = lines.Split(',');
                TournamentModel t = new TournamentModel();
                t.Id = int.Parse(cols[0]);
                t.TournamentName = cols[1];
                t.EntryFee = decimal.Parse(cols[2]);

                string[] teamIDs = cols[3].Split('|');
                foreach(var ids in teamIDs)
                {
                    t.EnteredTeams.Add(team.Where(x => x.TeamModelID == int.Parse(ids)).First());
                }

                string[] prizeIds = cols[4].Split('|');
                foreach(var ids in prizeIds)
                {
                    t.Prizes.Add(prize.Where(x => x.Id == int.Parse(ids)).First());
                }

                tour.Add(t);
            }
            return tour;
        }

        //convert the prizes to list<string> and save list<string> to txt file
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }


        //convert the prizes to list<string> and save list<string> to txt file
        public static void SaveToPersonFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.PeopleID},{p.FirstName},{p.LastName},{p.EmailAddress},{p.PhoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {

            List<string> lines = new List<string>();

            foreach(var p in models)
            {
                var t= ConvertPeopleListToString(p.TeamMembers);
                lines.Add($"{p.TeamModelID}, {p.TeamName}, {t}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }


        private static void SaveToTournamentFile(this List<TournamentModel> tour, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (var t in tour)
            {
                var prizeIDs = ConvertPrizeListToString(t.Prizes);
                var teamIDs = ConvertTeamListToString(t.EnteredTeams);
                lines.Add($"{t.Id},{t.TournamentName},{t.EntryFee},{prizeIDs},{teamIDs}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            var t = "";
            if(people.Count > 0)
            {
                foreach (var p in people)
                {
                    t += $"{p.PeopleID}|";
                }
                t = t.Substring(0, t.Length - 1);
            }

            return t;
        }
        private static string ConvertPrizeListToString(List<PrizeModel> prize)
        {
            var p = "";
            if(prize.Count > 0)
            {
                foreach (var item in prize)
                {
                    p += $"{item.Id}|";
                }
                p = p.Substring(0, p.Length - 1);
            }
            return p;
        }

        private static string ConvertTeamListToString(List<TeamModel> team)
        {
            var t = "";
            if (team.Count > 0)
            {
                foreach (var item in team)
                {
                    t += $"{item.TeamModelID}|";
                }
                t = t.Substring(0, t.Length - 1);
            }
            return t;
        }
    }

}
