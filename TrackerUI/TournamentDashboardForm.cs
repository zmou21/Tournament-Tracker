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
    public partial class TournamentDashboardForm : Form, ITournamentRequestor
    {
        List<TournamentModel> availableTournament = GlobalConfig.Connection.GetTournament_All();
        List<TournamentModel> selectedTournament = new List<TournamentModel>();

        public TournamentDashboardForm()
        {
            InitializeComponent();
        }

        private void WireUpLists()
        {
            loadExistingTournamentDropDown.DataSource = null;
            loadExistingTournamentDropDown.DataSource = "";
            loadExistingTournamentDropDown.DisplayMember = "TournamentName";

            //TODO - add tournament name
        }

        public void TournamentRequestor(TournamentModel model)
        {
            selectedTournament.Add(model);

        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            //createTournamentForm frm = new createTournamentForm(this);
            //frm.Show();
        }
    }
}
