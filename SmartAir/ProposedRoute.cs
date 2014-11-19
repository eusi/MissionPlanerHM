using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class represents a route, proposed by the mission control. 
    /// </summary>
   public class ProposedRoute
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

       string objective;

       /// <summary>
       /// The objective of the route.
       /// </summary>
       public string Objective
       {
           get { return objective; }
           set { objective = value; }
       }

       string createdBy;

       /// <summary>
       /// The team name.
       /// </summary>
       public string CreatedBy
       {
           get { return createdBy; }
           set { createdBy = value; }
       }

       bool append;

       /// <summary>
       /// Indicates, if the existing waypoints should be removed before getting the new waypoints the this route.
       /// True --> existing waypoints will not be removed.
       /// </summary>
       public bool Append
       {
           get { return append; }
           set { append = value; }
       }
   
   }
}
