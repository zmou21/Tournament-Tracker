using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class MatchupEntry
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="score">
        /// 
        /// </param>
        public MatchupEntry(double score)
        {
            Score = score;
        }

        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; }

        /// <summary>
        /// Represents the score for the particular team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup the team came from 
        /// as the winner
        /// </summary>
        public Matchup ParentMatchup { get; set; }

        //GlobalConfig connect = GlobalConfig.InitializeConnection();
    }
}
