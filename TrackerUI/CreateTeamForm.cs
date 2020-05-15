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
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        ITeamRequestor callingForm;

        public CreateTeamForm(ITeamRequestor caller)
        {
            InitializeComponent();
            //CreateSampleData();
            WireUpLists();
            callingForm = caller;
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel() { FirstName = "Zack", LastName= "Moum"});
            availableTeamMembers.Add(new PersonModel() { FirstName = "Brooke", LastName = "Ram" });

            selectedTeamMembers.Add(new PersonModel() { FirstName = "John", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel() { FirstName = "First", LastName = "Staff" });

        }

        private void WireUpLists()
        {
            //this is how to pull member dropdown from form
            //the null allows for a successful refresh when add and delete members are selected
            selectMemberDropDown.DataSource = null;

            selectMemberDropDown.DataSource = availableTeamMembers;
            selectMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

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

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

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
            //on click of button
            //take selected drop down item
            //add selected item to list<selectedTeamMembers>
            //remove from drop down
            var selectedItem = (PersonModel)selectMemberDropDown.SelectedItem;

            if(selectedItem != null)
            {
                selectedTeamMembers.Add(selectedItem);
                availableTeamMembers.Remove(selectedItem);

                //refresh
                WireUpLists();
            }
        }

        private void RemoveSelectedMemberButton_Click(object sender, EventArgs e)
        {
            var selectedItem = (PersonModel)teamMembersListBox.SelectedItem;

            if(selectedItem != null)
            {
                selectedTeamMembers.Remove(selectedItem);
                availableTeamMembers.Add(selectedItem);

                //refresh
                WireUpLists();
            }
        }


        private void createTeamButton_Click(object sender, EventArgs e)
        {

            TeamModel tm = new TeamModel();

            //will take in team name
            tm.TeamName = teamNameValue.Text;
            //will take in list of selected team
            tm.TeamMembers = selectedTeamMembers;
            //will add list of selected team to DB

            GlobalConfig.Connection.CreateTeam(tm);

            callingForm.TeamRequestor(tm);
            //close out form
            this.Close();
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
