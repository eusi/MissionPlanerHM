

using JudgeServerInterface;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace MissionPlanner.SmartAir
{
    public class MissionPlannerService : IMissionPlannerService
    {


        #region private attibutes
        Obstacles latestObstacles = new Obstacles();
        List<ProposedRoute> receivedRoutes = new List<ProposedRoute>();
        List<UAVPosition> UAVPositionsHistory = new List<UAVPosition>();
        List<Zone> zones = new List<Zone>();
        List<Target> targets = new List<Target>();
        #endregion

        #region public properties used by the gui
       
        /// <summary>
        /// History of all received routes. The last entry is the newest route.
        /// </summary>
        public List<ProposedRoute> ReceivedRoutes
        {
            get
            {
                lock (receivedRoutes)
                {
                    return receivedRoutes;
                }
            }

        }
        
        /// <summary>
        /// Set the current coordinates of the uav.
        /// </summary>
        /// <param name="currentPosition">The current position with lat/lng/alt.</param>
        public void setCurrentUAVPosition(UAVPosition currentPosition)
        {

            // to do: write to file and clear list
            UAVPositionsHistory.Add(currentPosition);
        }
        
        /// <summary>
        /// This method gets all received zones.
        /// </summary>
        public List<Zone> Zones
        {
            get { return zones; }
            set { zones = value; }
        }
        
        /// <summary>
        /// This method gets all received targets.
        /// </summary>
        public List<Target> Targets
        {
            get { return targets; }
            set { targets = value; }
        }
        #endregion

        #region public properties used by the judge server

        /// <summary>
        /// Sets the latest coordinates of the obstacles retrieved by the judge server.
        /// </summary>
        public Obstacles LatestObstacles
        {

            set { latestObstacles = value; }
        }

        #endregion

        # region interface to the mission control
        /// <summary>
        /// This method adds new waypoints to the mission planner.  
        /// </summary>
        /// <param name="waypoints">The waypoints to add.</param>
        /// <param name="append">Indicates, if the existing waypoints should be removed before getting the new waypoints the this route. True --> existing waypoints will not be removed.</param>
        /// <param name="objective">The objective of this route. e.g. lawnmower route, drop route</param>
        /// <param name="createdBy">The team (e.g. Search Group) creating the waypoints. </param>
        public void setWayPoints(List<Locationwp> waypoints, bool append, string objective, string createdBy)
        {
            try
            {
                lock (receivedRoutes)
                {
                    receivedRoutes.Add(new ProposedRoute() { WayPoints = waypoints, Append = append, Objective = objective, CreatedBy = createdBy });
                }
                MissionPlanner.GCSViews.FlightPlanner.instance.SetNewWayPoints(waypoints, append);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// This method gets the current waypoints used by the mission planner.
        /// </summary>
        /// <returns>A list of waypoints.</returns>
        public List<Locationwp> getWayPoints()
        {
            return MissionPlanner.GCSViews.FlightPlanner.instance.getWayPoints();

        }

        /// <summary>
        /// This methods sets a new zone in the mission planner map. 
        /// </summary>
        /// <param name="zonePoints">The coordinates (Lat/Lng) of the zone with color and name.</param>   
        public void setZones(List<Zone> newZones)
        {

            zones = newZones;

        }

        /// <summary>
        /// This method gets the zones.
        /// </summary>
        /// <returns>A list of zones.</returns>
        public List<Zone> getZones()
        {
            return zones;
        }


        /// <summary>
        /// Gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt </returns>
        public UAVPosition getUAVPosition()
        {           
            return UAVPositionsHistory.Last();
        }

        /// <summary>
        /// This method gets the UAV position history.
        /// </summary>
        /// <param name="resetHistory">If true --> history will be empty after method call.</param>
        /// <returns>A list of coordinates.</returns>
        public List<UAVPosition> getUAVPositionHistory(bool resetHistory)
        {
            var result = UAVPositionsHistory;
            if (resetHistory)
                UAVPositionsHistory.Clear();
            return result;
        }

        /// <summary>
        /// This methods retrieves the latest positions of the stationary and moving obstacles. 
        /// </summary>
        /// <returns>The obstacles.</returns>
        public Obstacles getObstacles()
        {
            return latestObstacles;
           
        }

        /// <summary>
        /// This method sets a list of targets.
        /// </summary>
        /// <param name="targets">The coordinates with Lat/Lng. and name</param>       
        public void setTargets(List<Target> targets)
        {

            this.targets = targets;
        }

        /// <summary>
        /// This method gets the targets.
        /// </summary>
        /// <returns>A list of targets.</returns>
        public List<Target> getTargets()
        {
            return targets;
        }

        /// <summary>
        /// This method creates test data including UAVPos History, Zones, Obstacles, Targets.
        /// </summary>       
        public void createTestData()
        {
            // create position test data
            UAVPositionsHistory.Add(new UAVPosition(123.23, 123.34, 654.45,  DateTime.Now.ToString("yyyyMMddHHmmssffff")));
            UAVPositionsHistory.Add(new UAVPosition(124.23, 124.34, 655.45,  DateTime.Now.AddSeconds(1).ToString("yyyyMMddHHmmssffff")));
            UAVPositionsHistory.Add(new UAVPosition(125.23, 125.34, 656.45,  DateTime.Now.AddSeconds(2).ToString("yyyyMMddHHmmssffff")));
            UAVPositionsHistory.Add(new UAVPosition(126.23, 126.34, 657.45,  DateTime.Now.AddSeconds(3).ToString("yyyyMMddHHmmssffff")));
            UAVPositionsHistory.Add(new UAVPosition(127.23, 127.34, 658.45,  DateTime.Now.AddSeconds(4).ToString("yyyyMMddHHmmssffff")));
            UAVPositionsHistory.Add(new UAVPosition(128.23, 128.34, 659.45,  DateTime.Now.AddSeconds(5).ToString("yyyyMMddHHmmssffff")));

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
            temp.Add(new GMap.NET.PointLatLng(1,1));
            temp.Add(new GMap.NET.PointLatLng(2,2));
            temp.Add(new GMap.NET.PointLatLng(3,3));
            temp.Add(new GMap.NET.PointLatLng(4,4));

            zones.Add(new Zone() { ZonePoints = temp, Color = new Color(100, 100, 100, 100), ZoneName = "No Fly Zone" });
            zones.Add(new Zone() { ZonePoints = temp, Color = new Color(200, 200, 200, 200), ZoneName = "Emergency Zone" });
            zones.Add(new Zone() { ZonePoints = temp, Color = new Color(100, 0, 0, 0), ZoneName = "Search Area" });
          
            List<Target> targets = new List<Target>();
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(1, 1), TargetName = "Drop Target", Color = new Color(100, 0, 0, 0) });
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(2, 2), TargetName = "Off Axis Target 1", Color = new Color(100, 100, 100, 0) });
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(3, 3), TargetName = "Off Axis Target 2", Color = new Color(100, 0, 100, 0) });
            targets.Add(new Target() { Coordinates = new GMap.NET.PointLatLng(4, 4), TargetName = "SRIC Target", Color = new Color(100, 255, 255, 255) });

            
        }

      
        #endregion





       
    }
}
