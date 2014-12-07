

using JudgeServerInterface;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;


namespace MissionPlanner.SmartAir
{
    public class MissionPlannerService : IMissionPlannerService
    {
               
        # region interface to the mission control
        /// <summary>
        /// This method adds new waypoints to the mission planner.  
        /// </summary>
        /// <param name="waypoints">The waypoints to add.</param>
        /// <param name="append">Indicates, if the existing waypoints should be removed before getting the new waypoints the this route. True --> existing waypoints will not be removed.</param>
        /// <param name="objective">The objective of this route. e.g. lawnmower route, drop route</param>
        /// <param name="createdBy">The team (e.g. Search Group) creating the waypoints. </param>
        public void setWayPoints(List<Locationwp> waypoints, bool append, SAM_TYPES objective)
        {
            try
            {

                foreach (var wp in waypoints)
                {
                    wp.objective = objective.ToString();
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
                    waypoints.Add(clone);


                }


                SmartAirData.Instance.ReceivedRoutes.Add(new ProposedRoute() { WayPoints = waypoints, Append = append, Objective = objective });
                 
                MissionPlanner.GCSViews.FlightPlanner.instance.SetNewWayPoints(waypoints, append);
                MissionPlanner.GCSViews.FlightPlanner.instance.NewWaypointReachedEvent( SmartAirData.Instance.NextWPIndex);
                
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
            lock (SmartAirData.Instance.Zones)
            {
                foreach (var zone in newZones)
                {
                    if (SmartAirData.Instance.Zones.ContainsKey(zone.ZoneType))
                        SmartAirData.Instance.Zones[zone.ZoneType] = zone;
                    else
                    {
                        SmartAirData.Instance.Zones.Add(zone.ZoneType, zone);
                    }
                }
            }
            MissionPlanner.GCSViews.FlightPlanner.instance.drawZones(newZones);

        }

        /// <summary>
        /// This method gets the zones.
        /// </summary>
        /// <returns>A list of zones.</returns>
        public Dictionary<SAM_TYPES,Zone> getZones()
        {
            return SmartAirData.Instance.Zones;
        }


        /// <summary>
        /// Gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt </returns>
        public UAVPosition getUAVPosition()
        {        
            return SmartAirData.Instance.UAVPosition;          
        }
             

        /// <summary>
        /// This methods retrieves the latest positions of the stationary and moving obstacles. 
        /// </summary>
        /// <returns>The obstacles.</returns>
        public Obstacles getObstacles()
        {
            return SmartAirData.Instance.LatestObstacles;
        }

        /// <summary>
        /// This method sets a list of targets.
        /// </summary>
        /// <param name="targets">The coordinates with Lat/Lng. and name</param>       
        public void setTargets(List<Target> targets)
        {
            // there can be more than one target each category eg off axis task --> group targets by type and save to dict            
            foreach (var targetsGroupedByType in targets.GroupBy(x => x.TargetType))
            {
                lock (SmartAirData.Instance.Targets)
                {

                    if (SmartAirData.Instance.Targets.ContainsKey(targetsGroupedByType.Key))
                    {                        
                        SmartAirData.Instance.Targets[targetsGroupedByType.Key] = targetsGroupedByType.ToList();
                    }
                    else
                    {
                        SmartAirData.Instance.Targets.Add(targetsGroupedByType.Key, targetsGroupedByType.ToList());
                    }
                    
                }
            }
            MissionPlanner.GCSViews.FlightPlanner.instance.drawTargets(targets);
        }

        /// <summary>
        /// This methods stops the loitering of the UAV and sets the next waypoint (if available).
        /// </summary>
        /// <returns>Indicates if the stopping process was sucessful.</returns>
        public bool stopLoiter()
        {            
           return MissionPlanner.GCSViews.FlightPlanner.instance.stopLoiter();
        }

        /// <summary>
        /// This method gets the targets.
        /// </summary>
        /// <returns>A list of targets.</returns>
        public Dictionary<SAM_TYPES,List<Target>> getTargets()
        {
            return SmartAirData.Instance.Targets;
        }

        public void createTestData()
        {
            SmartAirData.Instance.createTestData();
        }

      
        #endregion


    }
}
