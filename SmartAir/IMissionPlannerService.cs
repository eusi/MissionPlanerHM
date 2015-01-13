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
        /// This methods stops the loitering of the UAV and sets the next waypoint (if available).
        /// </summary>
        /// <returns>true, if the stopping process was sucessful.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool stopLoiter();

        /// <summary>
        /// This methods adds new waypoints to the mission planner.  
        /// </summary>
        /// <param name="waypoints">The waypoints to add.</param>
        /// <param name="append">Indicates, if the existing waypoints should be removed before getting the new waypoints the this route. True --> existing waypoints will not be removed.</param>
        /// <param name="objective">The objective of this route. e.g. lawnmower route, drop route</param>
        /// <param name="createdBy">The team (e.g. Search Group) creating the waypoints. </param>
        /// <returns>true, if the operation was sucessful.</returns>
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        bool setWayPoints(List<Locationwp> waypoints, bool append, SamTypes objective);  


        /// <summary>
        /// This method gets the current waypoints used by the mission planner.
        /// </summary>
        /// <returns>A list of waypoints.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat=WebMessageFormat.Json)]
        List<Locationwp> getWayPoints();              

        /// <summary>
        /// <summary>
        /// This methods sets new zones in the mission planner map. 
        /// </summary>
        /// <param name="zonePoints">The coordinates (Lat/Lng) of the zone with color and name.</param>
        /// <returns>true, if the operation was sucessful.</returns>
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        bool setZones(List<Zone> newZones);

        /// <summary>
        /// This method gets the current zones.
        /// </summary>
        /// <returns>A list of zones.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Dictionary<SamTypes, Zone> getZones();

        /// <summary>
        /// This method gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt and timestamp </returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        UAVPosition getUAVPosition();
         

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
        /// <returns>true, if the operation was sucessful.</returns>
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        bool setTargets(List<Target> targets);

        /// <summary>
        /// This method gets the targets.
        /// </summary>
        /// <returns>A list of targets.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Dictionary<SamTypes, List<Target>> getTargets();

        /// <summary>
        /// This method checks if the service is reachable.
        /// </summary>
        /// <returns>true, if connection is established</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool testConnection();

        /// <summary>
        /// This method creates test data including UAVPos History, Zones, Obstacles, Targets.
        /// </summary>       
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        void createTestData();

        /// <summary>
        /// Gets the next waypoint of the plane.
        /// </summary>
        /// <returns>The waypoint.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Locationwp getNextWaypoint();


        /// <summary>
        /// Gets the direction and velocity of the current wind.
        /// </summary>
        /// <returns>The wind.</returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Wind getWind();

    }
}
