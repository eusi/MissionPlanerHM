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
        bool running = true;
        JudgeServer js;
        int intervall = 100;

        public int Intervall
        {
            get { return intervall; }
            set { intervall = value; }
        }


        /// <summary>
        /// Create a judge server worker for polling the jude server interface.
        /// </summary>
        /// <param name="url">The judge server url.</param>
        /// <param name="user">the user.</param>
        /// <param name="password">the password.</param>
        /// <param name="intervall">the polling intervall.</param>
        public JudgeServerWorker(string url, string user, string password,int intervall)
        {
            js = new JudgeServer();
            js.Connect(url, user, password);
            this.intervall = intervall;

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
        /// This worker polls the jude server interface in the given intervall and calls the methods GetObstacles, GetServerInfo, setUASTelemetry.
        /// </summary>
        public void GetAndSendInfo()
        {
           
            while (running)
            {
                try
                {
                 
                   
                // get, set and draw obstacles
                var obstacles = js.GetObstacles();
                log.Info("Judge server obstacles received.");
                MissionPlanner.GCSViews.FlightPlanner.instance.drawObstacles(obstacles);
                SmartAir.SmartAirContext.Instance.LatestObstacles = obstacles;

                var serverTime = js.GetServerInfo();
                log.Info("Judge server time received.");
                MissionPlanner.GCSViews.FlightPlanner.instance.drawServerTime(serverTime);    

                var position= SmartAir.SmartAirContext.Instance.UAVPosition;
                if (position != null)
                {
                    js.setUASTelemetry(position.Lat, position.Lng, position.Alt, position.Yaw);
                    log.Info("Current position was sent to judge server.");
                }
                    
                Thread.Sleep(intervall);
                    
                iErrorCounter = 0;

               
                }
                catch (Exception ex)
                {
                    log.Error("Connection to judge server failed.",ex);
                    iErrorCounter++;
                    if(iMaxError==iErrorCounter) 
                        running = false;
                    else
                        Thread.Sleep(intervall);
                    
                    
                    
                }
            }            
           
        }

    }
 
}
