﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class represents a smart air target eg air drop.
    /// </summary>
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


        SamType targetType;

         [DataMember]
        public SamType TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        
    }
}