using JudgeServerInterface;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MissionPlanner.SmartAir
{
    public class SmartAirData
    {
        private static volatile SmartAirData instance;
        private static object syncRoot = new Object();

        private SmartAirData()
        { 
        
        }

        public static SmartAirData Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SmartAirData();
                    }
                }

                return instance;
            }
        }
        
        #region private attibutes
        Obstacles latestObstacles = new Obstacles();

        List<ProposedRoute> receivedRoutes = new List<ProposedRoute>();

        UAVPosition _UAVPosition = new UAVPosition();

        List<Zone> zones = new List<Zone>();

        List<Target> targets = new List<Target>();        

        #endregion

        #region Properties
        public List<Zone> Zones
        {
            get { return zones; }
            set { zones = value; }
        }

        public Obstacles LatestObstacles
        {
            get { return latestObstacles; }
            set { latestObstacles = value; }
        }

        public List<ProposedRoute> ReceivedRoutes
        {
            get { return receivedRoutes; }
            set { 
                
                receivedRoutes = value;                
            }
        }

        public List<Target> Targets
        {
            get { return targets; }
            set { targets = value; }
        }

        public UAVPosition UAVPosition
        {
            get { return _UAVPosition; }
            set { _UAVPosition = value; }
        }

        /// <summary>
        /// This method creates test data including UAVPos History, Zones, Obstacles, Targets.
        /// </summary>       
        public void createTestData()
        {
            // create position test data
            UAVPosition =new UAVPosition(123.23, 123.34, 654.45, DateTime.Now.ToString("yyyyMMddHHmmssffff"));
           
            latestObstacles = new Obstacles();

            latestObstacles.MovingObstacles = new List<MovingObstacle>();

            latestObstacles.MovingObstacles.Add(new MovingObstacle() { Altitude = 100, Latitude = 100, Longitude = 100, SphereRadius = 10 });
            latestObstacles.MovingObstacles.Add(new MovingObstacle() { Altitude = 200, Latitude = 200, Longitude = 200, SphereRadius = 20 });
            latestObstacles.MovingObstacles.Add(new MovingObstacle() { Altitude = 300, Latitude = 300, Longitude = 300, SphereRadius = 30 });
            latestObstacles.MovingObstacles.Add(new MovingObstacle() { Altitude = 400, Latitude = 400, Longitude = 400, SphereRadius = 40 });
            latestObstacles.MovingObstacles.Add(new MovingObstacle() { Altitude = 500, Latitude = 500, Longitude = 500, SphereRadius = 50 });


            latestObstacles.StationaryObstacles = new List<StationaryObstacle>();

            latestObstacles.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 100, Longitude = 100, CylinderRadius = 10, CylinderHeight = 50 });
            latestObstacles.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 200, Longitude = 200, CylinderRadius = 20, CylinderHeight = 50 });
            latestObstacles.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 300, Longitude = 300, CylinderRadius = 30, CylinderHeight = 50 });
            latestObstacles.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 400, Longitude = 400, CylinderRadius = 40, CylinderHeight = 50 });
            latestObstacles.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 500, Longitude = 500, CylinderRadius = 50, CylinderHeight = 50 });



            zones = new List<Zone>();
            var temp = new List<GMap.NET.PointLatLng>();
            temp.Add(new GMap.NET.PointLatLng(1, 1));
            temp.Add(new GMap.NET.PointLatLng(2, 2));
            temp.Add(new GMap.NET.PointLatLng(3, 3));
            temp.Add(new GMap.NET.PointLatLng(4, 4));

            zones.Add(new Zone() { ZonePoints = temp, Color = Color.Red, ZoneName = "No Fly Zone" });
            zones.Add(new Zone() { ZonePoints = temp, Color = Color.OrangeRed, ZoneName = "Emergency Zone" });
            zones.Add(new Zone() { ZonePoints = temp, Color = Color.Green, ZoneName = "Search Area" });

            targets = new List<Target>();
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(1, 1), TargetName = "Drop Target", Color = new SmartAirColor(100, 0, 0, 0) });
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(2, 2), TargetName = "Off Axis Target 1", Color = new SmartAirColor(100, 100, 100, 0) });
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(3, 3), TargetName = "Off Axis Target 2", Color = new SmartAirColor(100, 0, 100, 0) });
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(4, 4), TargetName = "SRIC Target", Color = new SmartAirColor(100, 255, 255, 255) });


        }

        #endregion

   
    }
}
