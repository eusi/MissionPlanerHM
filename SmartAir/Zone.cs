using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MissionPlanner.SmartAir
{
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
        string zoneName;

        [DataMember]
        public string ZoneName
        {
            get { return zoneName; }
            set { zoneName = value; }
        }
    }
}
