using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// This class includes the coordinates of the uav. (lat,lng,alt,timeStamp)
    /// </summary>
    [Serializable]
    public class UAVPosition : PointLatLngAlt
    {
        public UAVPosition()
        { }
        public UAVPosition(double lat, double lng, double alt, string timeStamp)
        {
            this.Lat = lat;
            this.Lng = lng;
            this.Alt = alt;            
            this.Ts = timeStamp;
        }
        [NonSerialized]
        public string Ts;

     
        public float Distance;
        
        public float Yaw;

        public float Airspeed;
        public float WindDirection;
        public float WindVelocity;
        public int wpId;
        public int routeId;
        public int SamType;



    }
}
