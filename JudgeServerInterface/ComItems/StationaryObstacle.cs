using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeServerInterface
{
    /// <summary>
    /// This class represents a stationary osbtacle.
    /// </summary>
    public class StationaryObstacle
    {

        private float _latitude;

        /// <summary>
        /// The object latitude in floating point degrees.
        /// </summary>
        public float Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private float _longitude;

        /// <summary>
        /// The object longitude in floating point degrees.
        /// </summary>
        public float Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private float _cylinderRadius;

        /// <summary>
        /// The obstacle's cylinder radius in floating point feet.
        /// </summary>
        public float CylinderRadius
        {
            get { return _cylinderRadius; }
            set { _cylinderRadius = value; }
        }

        private float _cylinderHeight;

        /// <summary>
        /// The obstacle's cylinder height in floating point feet.
        /// </summary>
        public float CylinderHeight
        {
            get { return _cylinderHeight; }
            set { _cylinderHeight = value; }
        }


    }
}
