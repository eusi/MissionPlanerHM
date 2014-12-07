using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JudgeServerInterface
{
    /// <summary>
    /// This class represents a stationary osbtacle.
    /// </summary>
       [DataContract]
    public class StationaryObstacle
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

          private double _cylinderRadius;

        /// <summary>
        /// The obstacle's cylinder radius in floating point feet.
        /// </summary>
          [DataMember]
          public double CylinderRadius
        {
            get { return _cylinderRadius; }
            set { _cylinderRadius = value; }
        }

          private double _cylinderHeight;

        /// <summary>
        /// The obstacle's cylinder height in floating point feet.
        /// </summary>
          [DataMember]
          public double CylinderHeight
        {
            get { return _cylinderHeight; }
            set { _cylinderHeight = value; }
        }


    }
}
