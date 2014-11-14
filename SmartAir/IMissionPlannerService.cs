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

        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setWayPoints(List<Locationwp> waypoints,bool append); // HomePoint, WPRadius (45), LoiterRadius(45), Default alt (100) 

        

        [OperationContract()]
        [WebGet(ResponseFormat=WebMessageFormat.Json)]
        List<Locationwp> getWayPoints();

        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setZone(List<PointLatLng> zonePoints, MissionPlanner.SmartAir.MissionPlannerService.Color color, string zoneName);

        [OperationContract()]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        PointLatLngAlt getUAVPosition();

        //JudgeServerInterface.Obstacles getObstacles();

        [OperationContract()]
        [WebInvoke(
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setTarget(PointLatLng target, string targetName);

    }
}
