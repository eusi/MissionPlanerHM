﻿using JudgeServerInterface;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class (singleton) is a container for smart air data. 
    /// </summary>
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

        UAVPosition _UAVPosition;

        Dictionary<SamTypes, Zone> zones = new Dictionary<SamTypes, Zone>();

        Dictionary<SamTypes, List<Target>> targets = new Dictionary<SamTypes, List<Target>>();

        float lastWPIndexFromAutopilot = 0;

        float nextWPIndexFromAutopilot = 0;

        List<Locationwp> wayPointsTableOfAutoPilot = new List<Locationwp>();

        bool autoLoadRoutes = true;

        Wind wind = null;

      

     

        

        #endregion

        #region Properties

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

        public Locationwp getNextWaypoint() {

            
                if (wayPointsTableOfAutoPilot != null && wayPointsTableOfAutoPilot.Count > this.nextWPIndexFromAutopilot)
                {
                    return wayPointsTableOfAutoPilot[(int)this.nextWPIndexFromAutopilot];


                }
                else
                    return null;
            
        
        
        }

        public List<Locationwp> WayPointsTableOfAutoPilot
        {
            get { return wayPointsTableOfAutoPilot; }
            set { wayPointsTableOfAutoPilot = value; }
        }

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
        /// This method works only when autopilot waypoint table is in sync with mission planner wp table
        /// </summary>
        /// <param name="nextWPIndex"></param>
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
                        if (currentWP != null&&currentWP.IsLoiterInterruptAllowed && currentWP.id == (byte)MAVLink.MAV_CMD.LOITER_UNLIM)
                        {
                            // skip loiter, jump to next wp and send it to autopilot 
                            MainV2.comPort.setWPCurrent((ushort)(iNextWPIndex + 1));                            

                        }

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                    
                }
            }

        
        }

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

                }
                catch (Exception ex)
                {
                    return false;
                }


            }

            return false;

        }


        public Dictionary<SamTypes,Zone> Zones
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

        public Dictionary<SamTypes, List<Target>> Targets
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



            zones = new Dictionary<SamTypes,Zone>();
            var temp = new List<GMap.NET.PointLatLng>();
            temp.Add(new GMap.NET.PointLatLng(1, 1));
            temp.Add(new GMap.NET.PointLatLng(2, 2));
            temp.Add(new GMap.NET.PointLatLng(3, 3));
            temp.Add(new GMap.NET.PointLatLng(4, 4));



            zones.Add(SamTypes.ZONE_NO_FLIGHT,new Zone() { ZonePoints = temp, Color = new SmartAirColor(255,255,255,255), ZoneType=SamTypes.ZONE_NO_FLIGHT });
            zones.Add(SamTypes.ZONE_EMERGENT, new Zone() { ZonePoints = temp, Color = new SmartAirColor(255, 255, 255, 255), ZoneType = SamTypes.ZONE_EMERGENT });
            zones.Add(SamTypes.ZONE_SEARCH_AREA, new Zone() { ZonePoints = temp, Color = new SmartAirColor(255, 255, 255, 255), ZoneType = SamTypes.ZONE_SEARCH_AREA });



            targets = new Dictionary<SamTypes, List<Target>>();
            targets.Add(SamTypes.TARGET_AIRDROP,new List<Target>() { new Target(){ Coordinates = new GMap.NET.PointLatLng(1, 1), TargetType = SamTypes.TARGET_AIRDROP, Color = new SmartAirColor(100, 0, 0, 0) }});
            targets.Add(SamTypes.TARGET_OFFAXIS, new List<Target>() { new Target() { Coordinates = new GMap.NET.PointLatLng(2, 2), TargetType = SamTypes.TARGET_OFFAXIS, Color = new SmartAirColor(100, 100, 100, 0) }, new Target() { Coordinates = new GMap.NET.PointLatLng(4, 4), TargetType = SamTypes.TARGET_OFFAXIS, Color = new SmartAirColor(100, 100, 100, 0) } });
            targets.Add(SamTypes.TARGET_SRIC, new List<Target>() { new Target(){ Coordinates = new GMap.NET.PointLatLng(3, 3), TargetType = SamTypes.TARGET_SRIC, Color = new SmartAirColor(100, 0, 100, 0) }});
           


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

   
    }
}
