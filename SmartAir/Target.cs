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


        string targetName;

         [DataMember]    
        public string TargetName
        {
            get { return targetName; }
            set { targetName = value; }
        }

         Color color;

         [DataMember]
        public Color Color
         {
             get { return color; }
             set { color = value; }
         }
    }
}
