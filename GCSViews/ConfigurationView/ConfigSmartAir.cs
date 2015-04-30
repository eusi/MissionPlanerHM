using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DirectShowLib;
using MissionPlanner.Controls.BackstageView;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using log4net;
using MissionPlanner.SmartAir;

namespace MissionPlanner.GCSViews.ConfigurationView
{
    public partial class ConfigSmartAir : UserControl, IActivate
    {
        private bool startup = false;
        private static readonly ILog log = LogManager.GetLogger(typeof(FlightPlanner));

      

        public ConfigSmartAir()
        {
            InitializeComponent();
         
        }


        // This load handler now only contains code that should execute once
        // on start up. See Activate() for the remainder
        private void ConfigSmartAir_Load(object sender, EventArgs e)
        {
            startup = true;


            startup = false;
        }

     

     
        // Called every time that this control is made current in the backstage view
        public void Activate()
        {
            startup = true; // flag to ignore changes while we programatically populate controls


            string judgeURL = "http://192.168.1.14:8080";
            if (MainV2.config["judgeURL"] != null)
                judgeURL = MainV2.config["judgeURL"].ToString();
            this.txtJSUrl.Text = judgeURL;

            string judgeUser = "testadmin";
            if (MainV2.config["judgeUser"] != null)
                judgeUser = MainV2.config["judgeUser"].ToString();
            this.txtJSUser.Text = judgeUser;

            string judgePW = "testpass";
            if (MainV2.config["judgePW"] != null)
                judgePW = MainV2.config["judgePW"].ToString();
            this.txtJSPassword.Text = judgePW;


            if (MainV2.config["judgeIntervall"] != null)
                this.numIntervall.Value = int.Parse(MainV2.config["judgeIntervall"].ToString());
          

            string samRESTURL = "http://localhost:8000/MissionPlannerService";
            if (MainV2.config["samRESTUrl"] != null)
                samRESTURL = MainV2.config["samRESTUrl"].ToString();
            this.txtWSUrl.Text = samRESTURL;           
            startup = false;
        }



  

        private void numIntervall_ValueChanged(object sender, EventArgs e)
        {
            if (!startup)
                if (MainV2.config.ContainsKey("judgeIntervall"))
                MainV2.config["judgeIntervall"] = numIntervall.Value.ToString();
                else
                    MainV2.config.Add("judgeIntervall", numIntervall.Value.ToString());
        }

        private void txtWSUrl_TextChanged(object sender, EventArgs e)
        {
            if (!startup)
                if (MainV2.config.ContainsKey("samRESTUrl"))
                    MainV2.config["samRESTUrl"] = txtWSUrl.Text;
                else
                    MainV2.config.Add("samRESTUrl", txtWSUrl.Text);
        }

        private void txtJSUrl_TextChanged(object sender, EventArgs e)
        {
            if (!startup)
                if (MainV2.config.ContainsKey("judgeURL"))
                    MainV2.config["judgeURL"] = txtJSUrl.Text;
                else
                    MainV2.config.Add("judgeURL", txtJSUrl.Text);
        }

        private void txtJSUser_TextChanged(object sender, EventArgs e)
        {
            if (!startup)
                if (MainV2.config.ContainsKey("judgeUser"))
                    MainV2.config["judgeUser"] = txtJSUser.Text;
                else
                    MainV2.config.Add("judgeUser", txtJSUser.Text);
        }

        private void txtJSPassword_TextChanged(object sender, EventArgs e)
        {
            if (!startup)
                if (MainV2.config.ContainsKey("judgePW"))
                    MainV2.config["judgePW"] = txtJSPassword.Text;
                else
                    MainV2.config.Add("judgePW", txtJSPassword.Text);
        }

        private void btnStartWS_Click(object sender, EventArgs e)
        {
            try
            {
                if (FlightPlanner.instance.smartAirWSHost != null && FlightPlanner.instance.smartAirWSHost.State == CommunicationState.Faulted)
                {

                    FlightPlanner.instance.smartAirWSHost.Abort();
                    FlightPlanner.instance.smartAirWSHost = null;
                }

                if (FlightPlanner.instance.smartAirWSHost == null)
                {
                    FlightPlanner.instance.smartAirWSHost = new WebServiceHost(typeof(MissionPlanner.SmartAir.MissionPlannerService), new Uri(this.txtWSUrl.Text));

                    var binding = new WebHttpBinding();
                    binding.MaxReceivedMessageSize = 2147000000;
                    //binding.OpenTimeout = new TimeSpan(0, 10, 0);
                    //binding.CloseTimeout = new TimeSpan(0, 10, 0);
                    //binding.SendTimeout = new TimeSpan(0, 10, 0);
                    //binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                    ServiceEndpoint ep = FlightPlanner.instance.smartAirWSHost.AddServiceEndpoint(typeof(SmartAir.IMissionPlannerService), binding, "");

                    ServiceDebugBehavior stp = FlightPlanner.instance.smartAirWSHost.Description.Behaviors.Find<ServiceDebugBehavior>();
                    stp.HttpHelpPageEnabled = false;
                    stp.IncludeExceptionDetailInFaults = true;
                    FlightPlanner.instance.smartAirWSHost.Open();

                    this.btnStopWS.Enabled = true;
                    this.btnStartWS.Enabled = false;
                    log.Info("SAM REST service started.");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Starting REST service failed.", ex);
            }
        }

        private void btnStopWS_Click(object sender, EventArgs e)
        {
            try
            {
                if (FlightPlanner.instance.smartAirWSHost != null && FlightPlanner.instance.smartAirWSHost.State == CommunicationState.Faulted)
                {
                    FlightPlanner.instance.smartAirWSHost.Abort();
                }
                else if (FlightPlanner.instance.smartAirWSHost != null && FlightPlanner.instance.smartAirWSHost.State == CommunicationState.Opened)
                {
                    FlightPlanner.instance.smartAirWSHost.Close();

                }
                FlightPlanner.instance.smartAirWSHost = null;
                this.btnStopWS.Enabled = false;
                this.btnStartWS.Enabled = true;
                log.Info("SAM REST service stopped.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Error stopping REST service.", ex);
            }
        }

        private void btnJSStart_Click(object sender, EventArgs e)
        {
            try
            {
                FlightPlanner.instance.JSWorker = new JudgeServerWorker(this.txtJSUrl.Text, this.txtJSUser.Text, this.txtJSPassword.Text, (int)(this.numIntervall.Value));
                Thread JSWorkerThread = new Thread(new ThreadStart(FlightPlanner.instance.JSWorker.GetAndSendInfo));

                JSWorkerThread.Start();
                btnJSStart.Enabled = false;
                btnJSStop.Enabled = true;
                log.Info("Connection to judge server established");
            }
            catch (Exception ex)
            {

                log.Error("Judge Server connection error.", ex);
                MessageBox.Show(ex.Message);

            }
        }

        private void btnJSStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (FlightPlanner.instance.JSWorker != null)
                    FlightPlanner.instance.JSWorker.Stop();

                btnJSStart.Enabled = true;
                btnJSStop.Enabled = false;
                log.Info("Connection to judge server closed.");
            }
            catch (Exception ex)
            {

                log.Error("Error stopping judge server.", ex);
                MessageBox.Show(ex.Message);

            }
        }

        private void btnOpenExternalFlightData_Click(object sender, EventArgs e)
        {
            // Open Flight Data Window
            var FlightData = new GCSViews.FlightData();

            ExternalFlightData fd = new ExternalFlightData();
            fd.Show();
            fd.TopLevel = true;
            fd.Visible = true;

            var FlightDataSwitcher = new MainSwitcher(fd);

            FlightDataSwitcher.AddScreen(new MainSwitcher.Screen("FlightData", FlightData, true));
            FlightDataSwitcher.ShowScreen("FlightData");
        }

      

     

        
    }
}
