using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MissionPlanner.SmartAir
{
    [DataContract]
    public class Target
    {
        GMap.NET.PointLatLng coordinates;

        [DataMember]    
        public GMap.NET.PointLatLng Coordinates
        {
            get { return coordinates; }
            set { coordinates = value; }
        }


        SamTypes targetType;

         [DataMember]
        public SamTypes TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

         SmartAirColor color;

         [DataMember]
        public SmartAirColor Color
         {
             get { return color; }
             set { color = value; }
         }
    }
}
