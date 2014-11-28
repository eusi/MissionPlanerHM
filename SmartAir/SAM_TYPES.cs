﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    public enum SAM_TYPES
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
        TASK_SEARCH_AREA = 21,
        TASK_EMERGENT = 22,
        TASK_AIRDROP = 23,
        TASK_OFFAXIS = 24,
        TASK_IR = 25,
        ENUM_END = 666


    }
}
