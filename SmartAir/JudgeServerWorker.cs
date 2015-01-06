using JudgeServerInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace MissionPlanner.SmartAir
{ 
    /// <summary>
    /// This worker polls the jude server interface in the given intervall and calls the required methods.  
    /// </summary>
    public class JudgeServerWorker
    {
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

        // used for simulate moving
        //  int iMover = 0;
        // bool limitReached = false;

       

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
                
                //foreach (var moving in obstacles.MovingObstacles)
                //{
                //    moving.Latitude += 0.00001 * iMover;
                //    moving.Longitude += 0.00001 * iMover;
                //}
                //if (iMover >= 500 && !limitReached)
                //    limitReached = true;

                //if (iMover < 0 && limitReached)
                //    limitReached = false;
               
                //if(!limitReached)
                //    iMover++;

                //if (limitReached)
                //    iMover--;

                
                MissionPlanner.GCSViews.FlightPlanner.instance.drawObstacles(obstacles);
                SmartAir.SmartAirData.Instance.LatestObstacles = obstacles;

                var serverTime = js.GetServerInfo();
                MissionPlanner.GCSViews.FlightPlanner.instance.drawServerTime(serverTime);

                var position= SmartAir.SmartAirData.Instance.UAVPosition;
                js.setUASTelemetry(position.Lat,position.Lng,position.Alt,position.Yaw);

                Thread.Sleep(intervall);
                    
                iErrorCounter = 0;

               
                }
                catch (Exception)
                {
                    // to do logging
                    iErrorCounter++;
                    if(iMaxError==iErrorCounter) 
                        running = false;

                    
                    
                }
            }            
           
        }

    }
}
