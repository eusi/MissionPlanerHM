using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MissionPlanner.Utilities
{
    /// <summary>
    /// Struct as used in Ardupilot
    /// </summary>
    [Serializable]
    public class Locationwp
    {
        public int wpId;
        public byte id;				// command id
        public byte options;
        public float p1;				// param 1
        public float p2;				// param 2
        public float p3;				// param 3
        public float p4;				// param 4
        public double lat;				// Lattitude * 10**7
        public double lng;				// Longitude * 10**7
        public float alt;				// Altitude in centimeters (meters * 100)
        [NonSerialized]
        public bool IsLoiterInterruptAllowed;
        [NonSerialized]
        public string objective = "MANUAL";
        [OptionalFieldAttribute]
        public int samType = 91;
        [OptionalFieldAttribute]
        public float distance = 0;
        
       
    };
}
