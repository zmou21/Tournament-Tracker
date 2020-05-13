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
        //public interface ISaveToText
        //{
        //    List<T> ConvertToModel<T>(List<string> data);
        //}

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


        public static List<PersonModel> ConverttoPersonModel(this List<string> personModel)
        {
            List<PersonModel> people = new List<PersonModel>();

            foreach (var lines in personModel)
            {
                //split the CSV file and push values into a new array 
                string[] cols = lines.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.PhoneNumber = cols[4];

                people.Add(p);
            }
            return people;
        }

        //convert the prizes to list<string> and save list<string> to txt file
        public static void SaveToPersonFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.PhoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        //public static void SaveToFile<T>(this List<T> models, string fileName, string data)
        //{
        //    List<string> lines = new List<string>();

        //    foreach (var p in models)
        //    {
        //        lines.Add(data);
        //    }
        //    File.WriteAllLines(fileName.FullFilePath(), lines);
        //}
    }

}
