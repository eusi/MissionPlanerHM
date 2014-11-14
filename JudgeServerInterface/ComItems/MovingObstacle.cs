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

        private float _latitude;

        /// <summary>
        /// The object latitude in floating point degrees.
        /// </summary>
        [DataMember]
        public float Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private float _longitude;

        /// <summary>
        /// The object longitude in floating point degrees.
        /// </summary>
        [DataMember]
        public float Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private float _altitude;

        /// <summary>
        /// The object's MSL altitude in floating point feet.
        /// </summary>
          [DataMember]
        public float Altitude
        {
            get { return _altitude; }
            set { _altitude = value; }
        }

        private float _sphereRadius;

        /// <summary>
        /// The object's sphere radius in floating point feet.
        /// </summary>
          [DataMember]
        public float SphereRadius
        {
            get { return _sphereRadius; }
            set { _sphereRadius = value; }
        }




    }
}
