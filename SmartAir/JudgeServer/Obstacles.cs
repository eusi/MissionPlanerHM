using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPlanner.SmartAir.JudgServerInterface
{
    /// <summary>
    /// This class includes the current informations of all obstacles.
    /// </summary>
    public class Obstacles
    {

        private List<StationaryObstacle> _stationaryObstacles = new List<StationaryObstacle>();

        /// <summary>
        /// Includes all stationary obstacles
        /// </summary>
        public List<StationaryObstacle> StationaryObstacles
        {
            get { return _stationaryObstacles; }
            set { _stationaryObstacles = value; }
        }

        private List<StationaryObstacle> _movingObstacles = new List<StationaryObstacle>();

        /// <summary>
        /// Includes all moving obstacles
        /// </summary>
        public List<StationaryObstacle> MovingObstacles
        {
            get { return _movingObstacles; }
            set { _movingObstacles = value; }
        }
    }
}
