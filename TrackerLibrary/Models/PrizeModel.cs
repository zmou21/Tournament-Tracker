using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        public PrizeModel()
        {
        }

        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            PlaceName = placeName;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
            
        }

        /// <summary>
        /// A unique ID generated to keep track of all prizes
        /// </summary>
        public int Id{ get; set; }
        
        /// <summary>
        /// The numeric identifier for the place (1 for first,2 for second etc.)
        /// </summary>
        public int PlaceNumber { get; set; }
        
        /// <summary>
        /// User friendly name for the place
        /// </summary>
        public string PlaceName { get; set; }
        
        /// <summary>
        /// A fixed prize amount that a certain placenumber earns or 0 if unused
        /// </summary>
        public decimal PrizeAmount { get; set; }
        
        /// <summary>
        /// A percentage of the overall prize or zero if unusued
        /// </summary>
        public double PrizePercentage { get; set; }
    }

}
