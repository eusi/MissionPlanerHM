using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// Indicates how the auto routing mechanism will handle the new routes.
    /// </summary>
    public enum AutoRoutingMode
    {
        /// <summary>
        /// The last waypoint of the complete wp list will be cloned and set to auto loiter. 
        /// </summary>
        InsertAutoLoiter = 0,
        /// <summary>
        /// The last waypoint of each route section (different routeId) will be cloned and set to auto loiter. 
        /// </summary>
        InsertAutoLoiterEachRouteId = 1,
        /// <summary>
        /// No auto routing waypoint will be inserted.
        /// </summary>
        NoAutoLoiter = 2      
        

    }
}
