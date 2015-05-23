
using JudgeServerInterface;
using log4net;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace MissionPlanner.SmartAir
{
     //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
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
        /// <param name="insertionMode">Indicates how to process a imported route with its waypoints.</param>
        /// <param name="routingMode">Indicates how the auto routing mechanism will handle the new routes.</param> 
        /// <param name="currentRouteId">The currentRouteId of mission control.</param>
        /// <returns>true, if the operation was sucessful.</returns>
        public bool setWayPoints(List<Locationwp> waypoints, RouteInsertionMode insertionMode, AutoRoutingMode routingMode, int currentRouteId)
        {
            try
            {
                

                if (routingMode == AutoRoutingMode.InsertAutoLoiter)                
                   waypoints= AddClone(waypoints);
                else if (routingMode == AutoRoutingMode.InsertAutoLoiterEachRouteId)
                {

                    var wpsGrpById = waypoints.GroupBy(x => x.routeId);
                    waypoints.Clear();
                    foreach (var route in wpsGrpById)
                    {
                        waypoints.AddRange(route);
                        waypoints= AddClone(waypoints);

                    }

                }

                SmartAirContext.Instance.ReceivedRoutes.Add(new Route() { WayPoints = waypoints, InsertionMode = insertionMode,RoutingMode=routingMode });

                MissionPlanner.GCSViews.FlightPlanner.instance.setNewWayPoints(waypoints, insertionMode, currentRouteId);
                SmartAirContext.Instance.LoadNextRoute(SmartAirContext.Instance.NextWPIndexFromAutopilot);
                
                return true;
            }
            catch (Exception ex)
            {
                if (ex is MissionPlanner.SmartAir.DomainObjects.OutOfSyncException || ex is MissionPlanner.SmartAir.DomainObjects.DangerousUpdateException)
                    throw new WebFaultException<String>("OutOfSync",System.Net.HttpStatusCode.Conflict);
                log.Fatal("Importing received waypoints failed.",ex);
                return false;
            }
        }

        private List<Locationwp> AddClone(List<Locationwp> route)
        {
            var lastWP = route.LastOrDefault();
            // clone last wp
            if (lastWP != null && ((MAVLink.MAV_CMD)lastWP.id) == MAVLink.MAV_CMD.WAYPOINT)
            {

                Locationwp clone = new Locationwp();
                clone.IsLoiterInterruptAllowed = true;
                clone.id = (byte)MAVLink.MAV_CMD.LOITER_UNLIM;
                clone.lat = lastWP.lat;
                clone.lng = lastWP.lng;
                clone.alt = lastWP.alt;
                clone.options = lastWP.options;
                clone.p1 = lastWP.p1;
                clone.p2 = lastWP.p2;
                clone.p3 = lastWP.p3;
                clone.p4 = lastWP.p4;
                clone.routeId = lastWP.routeId;
                clone.SamType = lastWP.SamType;
                
                route.Add(clone);


            }
            return route;


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
                string sZones="";
                var zones = newZones.Select(x => x.ZoneType.ToString());
                foreach (var zone in zones)
	                sZones += zone + " ";
                log.Info("New zones received: " + sZones);

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
        public UAVPosition getCurrentState()
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
                string sTargets = "";
                var stargets = targets.Select(x => x.TargetType.ToString());
                foreach (var target in stargets)
                    sTargets += target + " ";

                log.Info("New targets received: " + sTargets);
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
        /// <returns>homeLocation as PointLatLngAlt, if connection is established</returns>
        public PointLatLngAlt testConnection()
        {
            // get home location
            if (MainV2.comPort.MAV.cs.HomeLocation.Lat != 0 && MainV2.comPort.MAV.cs.HomeLocation.Lng != 0)
            {
                return new PointLatLngAlt( MainV2.comPort.MAV.cs.HomeLocation.Lat,
                                           MainV2.comPort.MAV.cs.HomeLocation.Lng );
            }

            return null;
        }

        /// <summary>
        /// This method creates test data including UAVPos History, Zones, Obstacles, Targets.
        /// </summary>       
        public void createTestData()
        {
            SmartAirContext.Instance.createTestData();
        }

        

    }
}
