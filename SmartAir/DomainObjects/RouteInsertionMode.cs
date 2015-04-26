using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// Indicates how to process a imported route with its waypoints.
    /// </summary>
    public enum RouteInsertionMode
    {
        /// <summary>
        /// All pending routes will be deleted before the new waypoints are imported. The completed and the current routes will not be deleted.
        /// </summary>
        ClearPendingRoutes = 0,
        /// <summary>
        /// No waypoints will be deleted. The new waypoints will be added at the end of the existing wp list.
        /// </summary>
        Append = 1,
        /// <summary>
        /// Removes all existing waypoints before inserting the new ones.
        /// </summary>
        ClearAllRoutes = 2      
        


    }
}
