﻿using System;
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
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequestor callingForm;

        public CreatePrizeForm(IPrizeRequestor caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNumberValue.Text,
                    placeNameValue.Text, 
                    placeAmountValue.Text, 
                    prizePercentValue.Text);

                model = GlobalConfig.Connection.CreatePrize(model);

                //this model is being passed back through the constructor for use by any class that calls it
                callingForm.PrizeComplete(model);
                //closes form
                this.Close();
            }
            else
            {
                MessageBox.Show("This message has invalid information");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            decimal prizeAmount = 0;
            double prizePercent = 0;

            if (!int.TryParse(placeNumberValue.Text, out placeNumber))
            {
                output = false;
                //TODO set an error messages to be output
            }
            if (placeNumber <= 0)
            {
                output = false;
            }
            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }
            if (!decimal.TryParse(placeAmountValue.Text, out prizeAmount) || !double.TryParse(prizePercentValue.Text, out prizePercent))
            {
                output = false;
            }
            if (prizeAmount <= 0 && prizePercent <= 0 )
            {
                output = false;
            }
            if (prizePercent < 0 || prizePercent > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
