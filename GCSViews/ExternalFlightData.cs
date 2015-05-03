using MissionPlanner.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MissionPlanner.GCSViews
{
    public partial class ExternalFlightData : Form
    {
     
        public ExternalFlightData()
        {
            InitializeComponent();
   
        }

        private void ExternalFlightData_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainV2.instance.FlightData.threadrun = false;
            MainV2.instance.FlightData = new GCSViews.FlightData();
            MainV2.instance.MyView.RemoveScreen("FlightData");
            MainV2.instance.MyView.AddScreen(new MainSwitcher.Screen("FlightData", MainV2.instance.FlightData, true));
            
            
           MainV2.instance.FlightData.Width = MainV2.instance.MyView.Width;

           MainV2.instance.SimulateFlightDataClick();
            
            MainV2.instance.MyView.ShowScreen("FlightData");
            MainV2.instance.enableFlightDataButton();
        }
    }
}
