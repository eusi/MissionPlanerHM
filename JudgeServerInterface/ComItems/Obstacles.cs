using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JudgeServerInterface
{
    /// <summary>
    /// This class includes the current informations of all obstacles.
    /// </summary>
    [DataContract]
    public class Obstacles
    {

        private List<StationaryObstacle> _stationaryObstacles = new List<StationaryObstacle>();

        /// <summary>
        /// Includes all stationary obstacles
        /// </summary>
          [DataMember]
        public List<StationaryObstacle> StationaryObstacles
        {
            get { return _stationaryObstacles; }
            set { _stationaryObstacles = value; }
        }

        private List<MovingObstacle> _movingObstacles = new List<MovingObstacle>();

        /// <summary>
        /// Includes all moving obstacles
        /// </summary>
          [DataMember]
        public List<MovingObstacle> MovingObstacles
        {
            get { return _movingObstacles; }
            set { _movingObstacles = value; }
        }
    }
}
