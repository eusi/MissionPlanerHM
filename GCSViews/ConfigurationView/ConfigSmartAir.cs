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

         
            if (SmartAir.SmartAirContext.Instance.IsSAMServiceRunning)
            {
                this.btnStopWS.Enabled = true;
                this.btnStartWS.Enabled = false;
            }

            if (SmartAir.SmartAirContext.Instance.IsJudgeServerRunning)
            {
                btnJSStart.Enabled = false;
                btnJSStop.Enabled = true;
            }
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

                SmartAir.SmartAirContext.Instance.StartSAMService(this.txtWSUrl.Text);
                this.btnStopWS.Enabled = true;
                this.btnStartWS.Enabled = false;
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
                SmartAir.SmartAirContext.Instance.StopSAMService();
                this.btnStopWS.Enabled = false;
                this.btnStartWS.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error("Stopping REST service failed.", ex);
            }
        }

        private void btnJSStart_Click(object sender, EventArgs e)
        {
            try
            {
                SmartAir.SmartAirContext.Instance.StartJudgeServer(this.txtJSUrl.Text, this.txtJSUser.Text, this.txtJSPassword.Text, (int)(this.numIntervall.Value));
                btnJSStart.Enabled = false;
                btnJSStop.Enabled = true;
                
            }
            catch (Exception ex)
            {
                SmartAir.SmartAirContext.Instance.StopSAMService();
                log.Error("Judge Server connection error.", ex);
                MessageBox.Show(ex.Message);

            }
        }

        private void btnJSStop_Click(object sender, EventArgs e)
        {
            try
            {

                SmartAir.SmartAirContext.Instance.StopJudgeServer();
                btnJSStart.Enabled = true;
                btnJSStop.Enabled = false;
               
            }
            catch (Exception ex)
            {

                log.Error("Error stopping judge server.", ex);
                MessageBox.Show(ex.Message);

            }
        }

        private void btnOpenExternalFlightData_Click(object sender, EventArgs e)
        {
            
        }

      

     

        
    }
}
