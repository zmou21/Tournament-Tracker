using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class Matchup
    {
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();
        public TeamModel Winner { get; }
        public int MatchupRound { get; set; }

    }
}
