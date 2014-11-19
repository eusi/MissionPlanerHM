using JudgeServerInterface;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    public class SmartAirContext
    {
        private static volatile SmartAirContext instance;
        private static object syncRoot = new Object();

        private SmartAirContext()
        { 
        
        }

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
         Obstacles latestObstacles = new Obstacles();
         static List<ProposedRoute> receivedRoutes = new List<ProposedRoute>();
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
                return receivedRoutes;
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
        /// Gets the current coordinates of the uav.
        /// </summary>
        /// <returns>The current position with lat/lng/alt.</returns> 
        public UAVPosition getCurrentUAVPosition( )       {
        
            return UAVPositionsHistory.Last();
        }

        /// <summary>
        /// Gets the current coordinates of the uav.
        /// </summary>
        /// <returns>The current position with lat/lng/alt.</returns> 
        public List<UAVPosition> getUAVPositionHistory(bool resetHistory)
        {
             var result = UAVPositionsHistory;
            if (resetHistory)
                UAVPositionsHistory.Clear();
            return result;
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

                receivedRoutes.Add(new ProposedRoute() { WayPoints = waypoints, Append = append, Objective = objective, CreatedBy = createdBy });

                MissionPlanner.GCSViews.FlightPlanner.instance.SetNewWayPoints(waypoints, append);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }


       

    }
}
