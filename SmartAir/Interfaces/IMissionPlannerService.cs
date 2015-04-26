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
        /// <param name="insertionMode">Indicates how to process a imported route with its waypoints.</param>
        /// <param name="routingMode">Indicates how the auto routing mechanism will handle the new routes.</param> 
        /// <returns>true, if the operation was sucessful.</returns>
        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        bool setWayPoints(List<Locationwp> waypoints, RouteInsertionMode insertionMode, AutoRoutingMode routingMode);


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
        Dictionary<SamType, Zone> getZones();

        /// <summary>
        /// This method gets the current position of the UAV.
        /// </summary>
        /// <returns>The coordinates with Lat/Lng/Alt and timestamp </returns>
        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        UAVPosition getCurrentState();
         

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
        Dictionary<SamType, List<Target>> getTargets();

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

        

    }
}
