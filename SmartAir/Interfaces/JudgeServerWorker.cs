using JudgeServerInterface;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace MissionPlanner.SmartAir
{ 
    /// <summary>
    /// This worker polls the jude server interface in the given intervall and calls the required methods.  
    /// </summary>
    public class JudgeServerWorker
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       static bool running = true;

        public bool Running
        {
            get { return running; }
           
        }
        JudgeServer js;

        int obstacleintervall = 100;

        public int Obstacleintervall
        {
            get { return obstacleintervall; }
            set { obstacleintervall = value; }
        }

        int serverInfoIntervall = 100;

        public int ServerInfoIntervall
        {
            get { return serverInfoIntervall; }
            set { serverInfoIntervall = value; }
        }

        int _UAVPosIntervall = 100;

        public int UAVPosIntervall
        {
            get { return _UAVPosIntervall; }
            set { _UAVPosIntervall = value; }
        }


        /// <summary>
        /// Create a judge server worker for polling the jude server interface.
        /// </summary>
        /// <param name="url">The judge server url.</param>
        /// <param name="user">the user.</param>
        /// <param name="password">the password.</param>
        /// <param name="intervall">the polling intervall.</param>
        public JudgeServerWorker(string url, string user, string password,int obstacleintervall, int serverInfoIntervall, int UAVPosIntervall)
        {
            js = new JudgeServer();
            
            js.Connect(url, user, password);
           
            running = true;
            this._UAVPosIntervall = UAVPosIntervall;
            this.serverInfoIntervall = serverInfoIntervall;
            this.obstacleintervall = obstacleintervall;

        }

        /// <summary>
        /// Stops the worker.
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        int iMaxError = 5;
        int iErrorCounter = 0;



        /// <summary>
        /// Polls the jude server interface for server information.
        /// </summary>
        public void GetServerInfo()
        {

            while (running)
            {
                try
                {

                    var serverTime = js.GetServerInfo();
                  //  log.Info("Judge server time received. " + DateTime.Now.ToLongTimeString());
                    MissionPlanner.GCSViews.FlightPlanner.instance.drawServerTime(serverTime);                   

                    Thread.Sleep(serverInfoIntervall);

                    iErrorCounter = 0;

                }
                catch (Exception ex)
                {
                    log.Fatal("Connection to judge server failed. Cannot retrieve server info. ", ex);
                    iErrorCounter++;
                    if (iMaxError == iErrorCounter)
                        running = false;
                    else
                        Thread.Sleep(serverInfoIntervall);



                }
            }

        }

        /// <summary>
        /// Polls the jude server interface for obstacles information.
        /// </summary>
        public void GetObstacles()
        {

            while (running)
            {
                try
                {

                    // get, set and draw obstacles
                    var obstacles = js.GetObstacles();

                 //   log.Info("Judge server obstacles received. " + DateTime.Now.ToLongTimeString());
                    MissionPlanner.GCSViews.FlightPlanner.instance.drawObstacles(obstacles);
                    SmartAir.SmartAirContext.Instance.LatestObstacles = obstacles;

                    Thread.Sleep(obstacleintervall);

                    iErrorCounter = 0;

                }
                catch (Exception ex)
                {
                    log.Fatal("Connection to judge server failed. Cannot retrieve obstacles. ", ex);
                    iErrorCounter++;
                    if (iMaxError == iErrorCounter)
                        running = false;
                    else
                        Thread.Sleep(obstacleintervall);



                }
            }

        }

        /// <summary>
        /// Sends UAV position to the jude server interface.
        /// </summary>
        public void SetUAVPosition()
        {

            while (running)
            {
                try
                {

                    var position = SmartAir.SmartAirContext.Instance.UAVPosition;
                    if (position != null)
                    {
                        js.setUASTelemetry(position.Lat, position.Lng, position.Alt, position.Yaw);
                      //  log.Info("Current position was sent to judge server. " + DateTime.Now.ToLongTimeString());
                    }

                    Thread.Sleep(_UAVPosIntervall);

                    iErrorCounter = 0;

                }
                catch (Exception ex)
                {
                    log.Fatal("Connection to judge server failed. Cannot send current position.", ex);
                    iErrorCounter++;
                    if (iMaxError == iErrorCounter)
                        running = false;
                    else
                        Thread.Sleep(_UAVPosIntervall);



                }
            }

        }


       

    }
 
}
