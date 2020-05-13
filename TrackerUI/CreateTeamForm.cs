using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = new List<PersonModel>();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>(); 

        public CreateTeamForm()
        {
            InitializeComponent();
            //CreateSampleData();
            AddTeamMembers();
        }

        public void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel() { FirstName = "Zack", LastName= "Moum"});
            availableTeamMembers.Add(new PersonModel() { FirstName = "Brooke", LastName = "Ram" });

            selectedTeamMembers.Add(new PersonModel() { FirstName = "John", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel() { FirstName = "First", LastName = "Staff" });

        }

        private void AddTeamMembers()
        {
            //this is how to pull member dropdown from form
            selectMemberDropDown.DataSource = availableTeamMembers;
            selectMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PersonModel p = new PersonModel();

                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.PhoneNumber = cellphoneValue.Text;

                GlobalConfig.Connection.CreatePerson(p);

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("This message has invalid information");
            }
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            //if (ValidateForm())
            //{
            //    TeamModel team = new TeamModel();

            //    team.TeamName = teamNameValue.Text;

            //    GlobalConfig.Connection.CreatePerson(p);

            //    teamNameValue.Text = "";
            //}
        }

        private bool ValidateForm()
        {
            bool output = true;

            if(firstNameValue.Text.Length == 0)
            {
                output = false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                output = false;
            }
            if (emailValue.Text.Length == 0)
            {
                output = false;
            }
            if (cellphoneValue.Text.Length == 0)
            {
                output = false;
            }
            return output;
        }

    }
}
