﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class TournamentModel
    {

        public string TournamentName { get; set; }
        public double EntryFee { get; set; }
        public List<TeamModel> EnteredTeams { get; set; }
        public List<Prize> Prizes { get; set; }
        public List<List<Matchup>> Rounds { get; set; }
    }
}
