using JudgeServerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MissionPlanner.SmartAir
{ 
    
    public class JudgeServerWorker
    {
        bool running = true;
        JudgeServer js;
        int intervall = 100;
        public JudgeServerWorker(string url, string user, string password,int intervall)
        {
            js = new JudgeServer();
            js.Connect(url, user, password);
            this.intervall = intervall;

        }

        public void Stop()
        {
            running = false;
        }
        int iMaxError = 5;
        int iErrorCounter = 0;
        public void GetAndSendInfo()
        {
            while (running)
            {
                try
                {

              
                // get, set and draw obstacles
                var obstacles = js.GetObstacles();
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
