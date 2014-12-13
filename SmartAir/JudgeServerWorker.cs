using JudgeServerInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
      //  public static string times = "";
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
            //List<TimeSpan> timetable = new List<TimeSpan>();
            while (running)
            {
                try
                {
                    //Stopwatch stopWatch = new Stopwatch();
                    //stopWatch.Start();
                   
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

                //stopWatch.Stop();
                //timetable.Add(stopWatch.Elapsed);
                }
                catch (Exception)
                {
                    // to do logging
                    iErrorCounter++;
                    if(iMaxError==iErrorCounter) 
                        running = false;

                    
                    
                }
            }
            
            //foreach (var ts in timetable)
            //{
            //      // Format and display the TimeSpan value.

            //    times += ts.ToString() + "|||";
                
            //}
        }

    }
}
