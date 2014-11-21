using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JudgeServerInterface
{
    /// <summary>
    /// This class represents a moving osbtacle.
    /// </summary>
    [DataContract]
    public class MovingObstacle
    {

        private double _latitude;

        /// <summary>
        /// The object latitude in floating point degrees.
        /// </summary>
        [DataMember]
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private double _longitude;

        /// <summary>
        /// The object longitude in floating point degrees.
        /// </summary>
        [DataMember]
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private double _altitude;

        /// <summary>
        /// The object's MSL altitude in floating point feet.
        /// </summary>
          [DataMember]
        public double Altitude
        {
            get { return _altitude; }
            set { _altitude = value; }
        }

          private double _sphereRadius;

        /// <summary>
        /// The object's sphere radius in floating point feet.
        /// </summary>
          [DataMember]
          public double SphereRadius
        {
            get { return _sphereRadius; }
            set { _sphereRadius = value; }
        }




    }
}
