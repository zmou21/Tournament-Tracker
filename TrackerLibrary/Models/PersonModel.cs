﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int PeopleID { get; set; }
        public string FullName
        {
            get
            {
                var name = $"{FirstName} {LastName}";
                return name;
            }
        } 
    }
}
