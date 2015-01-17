using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Drawing;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class represents a smartair zone eg no fly zone.
    /// </summary>
    [DataContract]
    public class Zone
    {
        List<GMap.NET.PointLatLng> zonePoints;

        [DataMember]
        public List<GMap.NET.PointLatLng> ZonePoints
        {
            get { return zonePoints; }
            set { zonePoints = value; }
        }
        SmartAirColor color;

        [DataMember]
        public SmartAirColor Color
        {
            get { return color; }
            set { color = value; }
        }
        SamType zoneType;

        [DataMember]
        public SamType ZoneType
        {
            get { return zoneType; }
            set { zoneType = value; }
        }

      

       
    }
}
