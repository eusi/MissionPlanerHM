using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    /// <summary>
    /// Wind with direction and velocity.
    /// </summary>
   public class Wind
    {
        private float direction;

       /// <summary>
       /// The direction of the wind.
       /// </summary>
        public float Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        private float velocity;
        
        /// <summary>
        /// The velocity of the wind.
        /// </summary>
        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }


    }
}
