﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        public TeamModel()
        {
        }

        public TeamModel(List<PersonModel> teamMembers, string teamName)
        {
            TeamMembers = teamMembers;
            TeamName = teamName;
        }

        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        public string TeamName { get; set; }
        public int TeamModelID { get; set; }

    }
}
