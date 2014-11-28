

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
                SmartAirData.Instance.ReceivedRoutes.Add(new ProposedRoute() { WayPoints = waypoints, Append = append, Objective = objective });
                 
                MissionPlanner.GCSViews.FlightPlanner.instance.SetNewWayPoints(waypoints, append);
                //MissionPlanner.GCSViews.FlightPlanner.instance.drawProposedRoute(SmartAirData.Instance.ReceivedRoutes.LastOrDefault());
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

            SmartAirData.Instance.Zones = newZones;
            MissionPlanner.GCSViews.FlightPlanner.instance.drawZones(newZones);

        }

        /// <summary>
        /// This method gets the zones.
        /// </summary>
        /// <returns>A list of zones.</returns>
        public List<Zone> getZones()
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

            SmartAirData.Instance.Targets = targets;
            MissionPlanner.GCSViews.FlightPlanner.instance.drawTargets(targets);
        }

        /// <summary>
        /// This method gets the targets.
        /// </summary>
        /// <returns>A list of targets.</returns>
        public List<Target> getTargets()
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
