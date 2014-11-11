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
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/setWayPoints")]
        void setWayPoints(List<Locationwp> waypoints);

        [OperationContract()]
        [WebGet(ResponseFormat=WebMessageFormat.Json)]           
        List<Locationwp> getWayPoints();
    }
}
