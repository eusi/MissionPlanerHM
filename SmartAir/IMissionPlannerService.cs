using GMap.NET;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace MissionPlanner.SmartAir
{
    [ServiceContract]
    public interface IMissionPlannerService
    {

        /// <summary>
        /// This methods adds new waypoints to the mission planner.  
        /// </summary>
        /// <param name="waypoints">The waypoints to add.</param>
        /// <param name="append">Indicates, if the existing waypoints should be removed before getting the new waypoints the this route. True --> existing waypoints will not be removed.</param>
        /// <param name="objective">The objective of this route. e.g. lawnmower route, drop route</param>
        /// <param name="createdBy">The team (e.g. Search Group) creating the waypoints. </param>
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setWayPoints(List<Locationwp> waypoints, bool append, string objective, string createdBy); // HomePoint, WPRadius (45), LoiterRadius(45), Default alt (100) 


        /// <summary>
        /// This method gets the current waypoints used by the mission planner.
        /// </summary>
        /// <returns>A list of waypoints.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat=WebMessageFormat.Json)]
        List<Locationwp> getWayPoints();

        /// <summary>
        /// <summary>
        /// This methods sets a new zone in the mission planner map. 
        /// </summary>
        /// <param name="zonePoints">The coordinates (Lat/Lng) of the zone with color and name.</param>   
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setZones(List<Zone> newZones);

        /// <summary>
        /// Gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt </returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        PointLatLngAlt getUAVPosition();

        /// <summary>
        /// This methods retrieves the latest positions of the stationary and moving obstacles. 
        /// </summary>
        /// <returns>The obstacles.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        JudgeServerInterface.Obstacles getObstacles();

        /// <summary>
        /// This method sets a list of targets.
        /// </summary>
        /// <param name="targets">The coordinates with Lat/Lng. and name</param>       
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setTargets(List<Target> targets);

    }
}
