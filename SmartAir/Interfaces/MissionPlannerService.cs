
using JudgeServerInterface;
using log4net;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;


namespace MissionPlanner.SmartAir
{
    public class MissionPlannerService : IMissionPlannerService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// This methods stops the loitering of the UAV and sets the next waypoint (if available).
        /// </summary>
        /// <returns>true, if the stopping process was sucessful.</returns>
        public bool stopLoiter()
        {
            try
            {
                var result = SmartAirContext.Instance.stopLoiter();
                if (result)
                    log.Info("Loitering successfully stopped");

                return result;
            }
            catch (Exception ex)
            {
                log.Error("Stop loiter failed",ex);
                return false;
            }

        }

        /// <summary>
        /// This methods adds new waypoints to the mission planner.  
        /// </summary>
        /// <param name="waypoints">The waypoints to add.</param>
        /// <param name="append">Indicates, if the existing waypoints should be removed before getting the new waypoints the this route. True --> existing waypoints will not be removed.</param>
        /// <param name="objective">The objective of this route. e.g. lawnmower route, drop route</param>
        /// <param name="createdBy">The team (e.g. Search Group) creating the waypoints. </param>
        /// <returns>true, if the operation was sucessful.</returns>
        public bool setWayPoints(List<Locationwp> waypoints, bool append, SamType objective)
        {
            try
            {

                foreach (var wp in waypoints)
                {
                    wp.objective = objective.ToString();
                    wp.samType = (int)objective;
                }
                
                var lastWP = waypoints.LastOrDefault();
                // clone last wp
                if (lastWP != null && ((MAVLink.MAV_CMD) lastWP.id) == MAVLink.MAV_CMD.WAYPOINT)
                {
                    
                    Locationwp clone = new Locationwp();
                    clone.IsLoiterInterruptAllowed = true;
                    clone.id = (byte) MAVLink.MAV_CMD.LOITER_UNLIM;
                    clone.lat = lastWP.lat;
                    clone.lng = lastWP.lng;
                    clone.alt = lastWP.alt;
                    clone.options = lastWP.options;
                    clone.p1 = lastWP.p1;
                    clone.p2 = lastWP.p2;
                    clone.p3 = lastWP.p3;
                    clone.p4 = lastWP.p4;
                    clone.objective = objective.ToString();
                    clone.samType = (int)objective;
                    waypoints.Add(clone);


                }


                SmartAirContext.Instance.ReceivedRoutes.Add(new Route() { WayPoints = waypoints, Append = append, Objective = objective });
                 
                MissionPlanner.GCSViews.FlightPlanner.instance.setNewWayPoints(waypoints, append);
                SmartAirContext.Instance.LoadNextRoute(SmartAirContext.Instance.NextWPIndexFromAutopilot);

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal("Importing received waypoints failed.",ex);
                return false;
            }
        }

        /// <summary>
        /// This method gets the current waypoints used by the mission planner.
        /// </summary>
        /// <returns>A list of waypoints.</returns>
        public List<Locationwp> getWayPoints()
        {
            try
            {
                
                var wayPoints =MissionPlanner.GCSViews.FlightPlanner.instance.getWayPoints();
                return wayPoints;
            }
            catch (Exception ex)
            {
                log.Error("Error getting waypoints",ex);
                return null;
            }
            

        }

        /// <summary>
        /// <summary>
        /// This methods sets new zones in the mission planner map. 
        /// </summary>
        /// <param name="zonePoints">The coordinates (Lat/Lng) of the zone with color and name.</param>
        /// <returns>true, if the operation was sucessful.</returns>
        public bool setZones(List<Zone> newZones)
        {
            try
            {
                
                foreach (var zone in newZones)
                {
                    if (SmartAirContext.Instance.Zones.ContainsKey(zone.ZoneType))
                        SmartAirContext.Instance.Zones[zone.ZoneType] = zone;
                    else
                    {
                        SmartAirContext.Instance.Zones.Add(zone.ZoneType, zone);
                    }
                }
                log.Info("New zones received: " + newZones.Select(x => x.ZoneType.ToString()));

                MissionPlanner.GCSViews.FlightPlanner.instance.drawZones(newZones);
               
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error setting zones.", ex);
                return false;
            }
        }

        /// <summary>
        /// This method gets the zones.
        /// </summary>
        /// <returns>A list of zones.</returns>
        public Dictionary<SamType,Zone> getZones()
        {
            try
            {
                return SmartAirContext.Instance.Zones;
            }
            catch (Exception ex)
            {
                log.Error("Get zones failed.", ex);               
                return null;
            }
            
        }

        /// <summary>
        /// Gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt </returns>
        public UAVPosition getUAVPosition()
        {
            try
            {
                
                return SmartAirContext.Instance.UAVPosition;
            }
            catch (Exception ex)
            {
              
                log.Error("Get UAV position failed.",ex);
                return null;
            }
        }             

        /// <summary>
        /// This methods retrieves the latest positions of the stationary and moving obstacles. 
        /// </summary>
        /// <returns>The obstacles.</returns>
        public Obstacles getObstacles()
        {
            try
            {


                return SmartAirContext.Instance.LatestObstacles;
            }
            catch (Exception ex)
            {

                log.Error("Get obstacles failed.", ex);
                return null;
            }
        }

        /// <summary>
        /// This method sets a list of targets.
        /// </summary>
        /// <param name="targets">The coordinates with Lat/Lng. and name</param>
        /// <returns>true, if the operation was sucessful.</returns>
        public bool setTargets(List<Target> targets)
        {
            try
            {
                // there can be more than one target each category eg off axis task --> group targets by type and save to dict            
                foreach (var targetsGroupedByType in targets.GroupBy(x => x.TargetType))
                {                    
                    if (SmartAirContext.Instance.Targets.ContainsKey(targetsGroupedByType.Key))
                    {
                        SmartAirContext.Instance.Targets[targetsGroupedByType.Key] = targetsGroupedByType.ToList();
                    }
                    else
                    {
                        SmartAirContext.Instance.Targets.Add(targetsGroupedByType.Key, targetsGroupedByType.ToList());
                    }
                }
                log.Info("New targets received: " + targets.Select(x => x.TargetType.ToString()));
                MissionPlanner.GCSViews.FlightPlanner.instance.drawTargets(targets);
                return true;
            }
            catch (Exception ex)
            {

                log.Error("Set targets failed", ex);
                return false;
            }
        }      

        /// <summary>
        /// This method gets the targets.
        /// </summary>
        /// <returns>A list of targets.</returns>
        public Dictionary<SamType,List<Target>> getTargets()
        {
            
            try
            {
                return SmartAirContext.Instance.Targets;
            }
            catch (Exception ex)
            {
                log.Error("Get targets failed.", ex);
                return null;
            }
        }

        /// <summary>
        /// This method checks if the service is reachable.
        /// </summary>
        /// <returns>true, if connection is established</returns>
        public bool testConnection()
        {
            return true;
        }

        /// <summary>
        /// This method creates test data including UAVPos History, Zones, Obstacles, Targets.
        /// </summary>       
        public void createTestData()
        {
            SmartAirContext.Instance.createTestData();
        }

        /// <summary>
        /// Gets the next waypoint of the plane.
        /// </summary>
        /// <returns>The waypoint.</returns>
        public Locationwp getNextWaypoint()
        {
            try
            {
                return SmartAirContext.Instance.getNextWaypoint();
            }
            catch (Exception ex)
            {
                log.Error("Get next waypoint failed.", ex); 
                return null;
            }
           
        }

        /// <summary>
        /// Gets the direction and velocity of the current wind.
        /// </summary>
        /// <returns>The wind.</returns>
        public Wind getWind()
        {
            try
            {
               
                return SmartAirContext.Instance.Wind;
            }
            catch (Exception ex)
            {
                log.Error("Get wind failed.", ex);
                return null;
            }
        }
    }
}
