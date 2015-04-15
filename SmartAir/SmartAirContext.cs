using JudgeServerInterface;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using log4net;
using System.Reflection;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class (singleton) includes smart air data and functions. 
    /// </summary>
    public class SmartAirContext
    {
        private static volatile SmartAirContext instance;
        private static object syncRoot = new Object();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private SmartAirContext()
        { 
        
        }
        /// <summary>
        /// Gets the current instance of the context.
        /// </summary>
        public static SmartAirContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SmartAirContext();
                    }
                }

                return instance;
            }
        }
        
        #region private attibutes

        /// <summary>
        /// Includes the latest obstacles received from the judge server.
        /// </summary>
        Obstacles latestObstacles = new Obstacles();

        /// <summary>
        /// A history of all routes(waypoints) received from mc.  
        /// </summary>
        List<Route> receivedRoutes = new List<Route>();

        /// <summary>
        /// Includes the latest position of the drone received from autopilot.
        /// </summary>
        UAVPosition _UAVPosition;

        /// <summary>
        /// Dictionary of all zones received from mc.
        /// </summary>
        Dictionary<SamType, Zone> zones = new Dictionary<SamType, Zone>();

        /// <summary>
        /// Dictionary of all targets received from mc.
        /// </summary>
        Dictionary<SamType, List<Target>> targets = new Dictionary<SamType, List<Target>>();

        /// <summary>
        /// Represents the last waypoint index received from autopilot.
        /// </summary>
        float lastWPIndexFromAutopilot = 0;

        /// <summary>
        /// Represents the next waypoint index received from autopilot.
        /// </summary>
        float nextWPIndexFromAutopilot = 0;

        /// <summary>
        /// Clone of the waypoint table of the Flight Plan view .
        /// </summary>
        List<Locationwp> wayPointsTableOfAutoPilot = new List<Locationwp>();

        /// <summary>
        /// Indicates if auto routing is activated. If activated waypoints with loiter unlimited and IsAutoInterrupt-Flag=true will be skipped automatically.  
        /// </summary>
        bool autoLoadRoutes = true;

        /// <summary>
        /// Includes the current wind data received from autopilot.
        /// </summary>
        Wind wind = null;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates if auto routing is activated. If activated waypoints with loiter unlimited and IsAutoInterrupt-Flag=true will be skipped automatically.  
        /// </summary>
        public bool AutoLoadRoutes
        {
            get { return autoLoadRoutes; }
            set
            {
                autoLoadRoutes = value;
                if (value == true)
                    LoadNextRoute(this.nextWPIndexFromAutopilot);
            }
        }

        /// <summary>
        /// Gets the next waypoint.
        /// </summary>
        /// <returns>The waypoint.</returns>
        public Locationwp getNextWaypoint() {


            if (wayPointsTableOfAutoPilot != null && wayPointsTableOfAutoPilot.Count > this.nextWPIndexFromAutopilot)
            {
                var nextWPOfTable = wayPointsTableOfAutoPilot[(int)this.nextWPIndexFromAutopilot];
             
                if (UAVPosition != null )
                    nextWPOfTable.distance = UAVPosition.Distance;

                return nextWPOfTable;
            }
            else
                return null;   
        }

        /// <summary>
        /// Clone of the waypoint list of the waypoint table of the view Flight Planner.
        /// </summary>
        public List<Locationwp> WayPointsTableOfAutoPilot
        {
            get { return wayPointsTableOfAutoPilot; }
            set { wayPointsTableOfAutoPilot = value; }
        }

        /// <summary>
        /// Represents the next waypoint index received from autopilot.
        /// </summary>
        public float NextWPIndexFromAutopilot
        {
            get { return nextWPIndexFromAutopilot; }
            set
            {

                // check if autopilot waypoint index has changed
                if (lastWPIndexFromAutopilot != nextWPIndexFromAutopilot)
                {
                    LoadNextRoute(nextWPIndexFromAutopilot);
                    MissionPlanner.GCSViews.FlightPlanner.instance.hideWaypoint((int)lastWPIndexFromAutopilot);
                    lastWPIndexFromAutopilot = nextWPIndexFromAutopilot;
                }
                nextWPIndexFromAutopilot = value;

            }
        }

        /// <summary>
        /// Dictionary of all zones received from mc.
        /// </summary>
        public Dictionary<SamType,Zone> Zones
        {
            get { return zones; }
            set { zones = value; }
        }

        /// <summary>
        /// Includes the latest obstacles received from the judge server.
        /// </summary>
        public Obstacles LatestObstacles
        {
            get { return latestObstacles; }
            set { latestObstacles = value; }
        }
        /// <summary>
        /// A history of all routes(waypoints) received from mc.  
        /// </summary>
        public List<Route> ReceivedRoutes
        {
            get { return receivedRoutes; }
            set { 
                
                receivedRoutes = value;                
            }
        }

        /// <summary>
        /// Dictionary of all targets received from mc.
        /// </summary>
        public Dictionary<SamType, List<Target>> Targets
        {
            get { return targets; }
            set { targets = value; }
        }

        /// <summary>
        /// Includes the latest position of the drone received from autopilot.
        /// </summary>
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



            zones = new Dictionary<SamType,Zone>();
            var temp = new List<GMap.NET.PointLatLng>();
            temp.Add(new GMap.NET.PointLatLng(1, 1));
            temp.Add(new GMap.NET.PointLatLng(2, 2));
            temp.Add(new GMap.NET.PointLatLng(3, 3));
            temp.Add(new GMap.NET.PointLatLng(4, 4));



            zones.Add(SamType.ZONE_NO_FLIGHT,new Zone() { ZonePoints = temp, Color = new SmartAirColor(255,255,255,255), ZoneType=SamType.ZONE_NO_FLIGHT });
            zones.Add(SamType.ZONE_EMERGENT, new Zone() { ZonePoints = temp, Color = new SmartAirColor(255, 255, 255, 255), ZoneType = SamType.ZONE_EMERGENT });
            zones.Add(SamType.ZONE_SEARCH_AREA, new Zone() { ZonePoints = temp, Color = new SmartAirColor(255, 255, 255, 255), ZoneType = SamType.ZONE_SEARCH_AREA });



            targets = new Dictionary<SamType, List<Target>>();
            targets.Add(SamType.TARGET_AIRDROP,new List<Target>() { new Target(){ Coordinates = new GMap.NET.PointLatLng(1, 1), TargetType = SamType.TARGET_AIRDROP }});
            targets.Add(SamType.TARGET_OFFAXIS, new List<Target>() { new Target() { Coordinates = new GMap.NET.PointLatLng(2, 2), TargetType = SamType.TARGET_OFFAXIS,  }, new Target() { Coordinates = new GMap.NET.PointLatLng(4, 4), TargetType = SamType.TARGET_OFFAXIS } });
            targets.Add(SamType.TARGET_SRIC, new List<Target>() { new Target(){ Coordinates = new GMap.NET.PointLatLng(3, 3), TargetType = SamType.TARGET_SRIC,  }});
           


        }

        /// <summary>
        /// Current wind.
        /// </summary>
        public Wind Wind
        {
            get { return wind; }
            set { wind = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// This method loads the next route if auto routings is activated. It works only when autopilot waypoint table is in sync with mission planner wp table.
        /// </summary>
        /// <param name="nextWPIndex">The next waypoint index of the autopilot.</param>
        public void LoadNextRoute(float nextWPIndex)
        {
            if (AutoLoadRoutes)
            {

                int iNextWPIndex = (int)nextWPIndex;

                // check if the waypoint has a following way point in the current table --> if not, loiter is not interrupted --> loiter until next WP is available
                if (wayPointsTableOfAutoPilot.Count > iNextWPIndex + 1)
                {
                    try
                    {

                        // check if current wp is loitering and is allowed to interrupt 
                        var currentWP = wayPointsTableOfAutoPilot[iNextWPIndex];
                        if (currentWP != null && currentWP.IsLoiterInterruptAllowed && currentWP.id == (byte)MAVLink.MAV_CMD.LOITER_UNLIM)
                        {
                            // skip loiter, jump to next wp and send it to autopilot 
                            MainV2.comPort.setWPCurrent((ushort)(iNextWPIndex + 1));
                            log.Info("Next route loaded successfully.");

                        }

                    }
                    catch (Exception ex)
                    {

                        log.Error("Error loading next route",ex);
                    }


                }
            }


        }

        /// <summary>
        /// Stops the loitering of the plane.
        /// </summary>
        /// <returns></returns>
        public bool stopLoiter()
        {

            // check if the waypoint has a following way point in the grid --> if not, loiter is not interrupted --> loiter until next WP is available
            if (wayPointsTableOfAutoPilot.Count > this.nextWPIndexFromAutopilot + 1)
            {
                try
                {
                    int iNextWPIndex = (int)nextWPIndexFromAutopilot;

                    var currentWP = wayPointsTableOfAutoPilot[iNextWPIndex];
                    if (currentWP != null && currentWP.id == (byte)MAVLink.MAV_CMD.LOITER_UNLIM)
                    {
                        // skip loiter, jump to next wp and send it to autopilot 
                        MainV2.comPort.setWPCurrent((ushort)(iNextWPIndex + 1));
                        return true;

                    }
                    else
                    {
                        log.Error("The drone does not loiter atm. Manual stop is not possible.");
                    }

                }
                catch (Exception ex)
                {
                    log.Error("Error stop loiter", ex);
                    return false;
                }


            }
            else
                log.Error("Loitering cannot be stopped, since there isn't any next waypoint available.");

            return false;

        }

        #endregion

    }
}
