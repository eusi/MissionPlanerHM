

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
        List<PointLatLngAlt> UAVPositionsHistory = new List<PointLatLngAlt>();
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
        public void setCurrentUAVPosition(PointLatLngAlt currentPosition)
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
        /// Gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt </returns>
        public PointLatLngAlt getUAVPosition()
        {
            UAVPositionsHistory.Add(new PointLatLngAlt(123.23, 123.34, 654.45, "Guten Tag"));
            return UAVPositionsHistory.Last();
        }

        /// <summary>
        /// This methods retrieves the latest positions of the stationary and moving obstacles. 
        /// </summary>
        /// <returns>The obstacles.</returns>
        public Obstacles getObstacles()
        {
            Obstacles result = new Obstacles();

            result.MovingObstacles = new List<MovingObstacle>();

            result.MovingObstacles.Add(new MovingObstacle() { Altitude = 100, Latitude = 100, Longitude = 100, SphereRadius = 10 });
            result.MovingObstacles.Add(new MovingObstacle() { Altitude = 200, Latitude = 200, Longitude = 200, SphereRadius = 20 });
            result.MovingObstacles.Add(new MovingObstacle() { Altitude = 300, Latitude = 300, Longitude = 300, SphereRadius = 30 });
            result.MovingObstacles.Add(new MovingObstacle() { Altitude = 400, Latitude = 400, Longitude = 400, SphereRadius = 40 });
            result.MovingObstacles.Add(new MovingObstacle() { Altitude = 500, Latitude = 500, Longitude = 500, SphereRadius = 50 });


            result.StationaryObstacles = new List<StationaryObstacle>();

            result.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 100, Longitude = 100, CylinderRadius = 10, CylinderHeight = 50 });
            result.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 200, Longitude = 200, CylinderRadius = 20, CylinderHeight = 50 });
            result.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 300, Longitude = 300, CylinderRadius = 30, CylinderHeight = 50 });
            result.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 400, Longitude = 400, CylinderRadius = 40, CylinderHeight = 50 });
            result.StationaryObstacles.Add(new StationaryObstacle() { Latitude = 500, Longitude = 500, CylinderRadius = 50, CylinderHeight = 50 });


            return result;


        }

        /// <summary>
        /// This method sets a list of targets.
        /// </summary>
        /// <param name="targets">The coordinates with Lat/Lng. and name</param>       
        public void setTargets(List<Target> targets)
        {

            this.targets = targets;
        }

        #endregion
    }
}
