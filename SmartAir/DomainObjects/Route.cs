using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class represents a route, received from mission control. 
    /// </summary>
   public class Route
   {
       List<Locationwp> wayPoints = new List<Locationwp>();

       /// <summary>
       /// The waypoints of the route.
       /// </summary>
       public List<Locationwp> WayPoints
       {
           get { return wayPoints; }
           set { wayPoints = value; }
       }

       SamType objective;




       RouteInsertionMode insertionMode = RouteInsertionMode.ClearPendingRoutes;

       /// <summary>
       /// Indicates how to process the imported route with its waypoints.
       /// </summary>
       public RouteInsertionMode InsertionMode
       {
           get { return insertionMode; }
           set { insertionMode = value; }
       }


       AutoRoutingMode routingMode = AutoRoutingMode.InsertAutoLoiter;

       /// <summary>
       /// Indicates how the auto routing mechanism will handle the new routes.
       /// </summary>
       public AutoRoutingMode RoutingMode
       {
           get { return routingMode; }
           set { routingMode = value; }
       }
   
   }
}
