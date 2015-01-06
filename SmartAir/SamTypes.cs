using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// Includes all types of smart air project. Has to be in sync with mission control.
    /// </summary>
    public enum SamTypes
    {
        UNKNOWN = 0,
        ZONE_NO_FLIGHT = 1,
        ZONE_SEARCH_AREA = 2,
        ZONE_EMERGENT = 3,
        TARGET_SRIC = 11,
        TARGET_AIRDROP = 12,
        TARGET_OFFAXIS = 13,
        TARGET_IR_STATIC = 14,
        TARGET_IR_DYNAMIC = 15,
        TARGET_EMERGENT = 16,
        TASK_SEARCH_AREA = 21,
        TASK_EMERGENT = 22,
        TASK_AIRDROP = 23,
        TASK_OFFAXIS = 24,
        TASK_IR = 25,
        TASK_WAYPOINT = 26,
        TASK_SRIC = 27,
        ENUM_END = 666


    }
}
