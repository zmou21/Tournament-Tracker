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
    public partial class createTournamentForm : Form, IPrizeRequestor, ITeamRequestor
    {
        private List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        private List<TeamModel> selectedTeams = new List<TeamModel>();
        private List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public createTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        private void WireUpLists()
        {
            //this is how to pull member dropdown from form
            //the null allows for a successful refresh when add and delete members are selected
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentPlayerListBox.DataSource = null;
            tournamentPlayerListBox.DataSource = selectedTeams;
            tournamentPlayerListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";

        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            var team = (TeamModel)selectTeamDropDown.SelectedItem;

            if(team != null)
            {
                selectedTeams.Add(team);
                availableTeams.Remove(team);

                //refresh
                WireUpLists();
            }
        }

        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            var team = (TeamModel)tournamentPlayerListBox.SelectedItem;

            if (team != null)
            {
                availableTeams.Add(team);
                selectedTeams.Remove(team);

                //refresh
                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            //call CreatePrizeForm
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        //get back the List from PrizeModel
        public void PrizeComplete(PrizeModel p)
        {
            //attach list to selctedPrize List
            selectedPrizes.Add(p);

            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        public void TeamRequestor(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }
    }
}
