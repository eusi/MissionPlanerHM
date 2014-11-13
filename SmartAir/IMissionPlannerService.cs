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
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json
            )]
        void setWayPoints(List<Locationwp> waypoints,bool append); // HomePoint, WPRadius (45), LoiterRadius(45), Default alt (100) 

        [OperationContract()]
        [WebGet(ResponseFormat=WebMessageFormat.Json)]           
        List<Locationwp> getWayPoints();

        void setZone(List<PointLatLng> zonePoints, System.Drawing.Color color, string zoneName);

        PointLatLngAlt getUAVPosition();

        //JudgeServerInterface.Obstacles getObstacles();

        void setTarget(PointLatLng target, string targetName);

    }
}
