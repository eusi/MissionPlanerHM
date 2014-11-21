using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeServerInterface
{
    /// <summary>
    /// Interface for communicating with the judge server.
    /// </summary>
    public interface IJudgeServer
    {
        /// <summary>
        /// This method connects with the judge server.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void Connect(string server, string userName, string password);

        /// <summary>
        /// Teams make requests to obtain server information for purpose of displaying the information.
        /// </summary>
        /// <returns>The server info.</returns>
        ServerInfo GetServerInfo();

        /// <summary>
        /// Teams make requests to obtain obstacle information for purpose of displaying the information and for avoiding the obstacles.
        /// </summary>
        /// <returns></returns>
        Obstacles GetObstacles();

        /// <summary>
        /// Teams make requests to upload the UAS telemetry to the competition server.
        /// </summary>
        /// <param name="latitude"> The latitude of the aircraft as a floating point degree value. Valid values are: -90 <= latitude <= 90.</param>
        /// <param name="longitude">The longitude of the aircraft as a floating point degree value. Valid values are: -180 <= longitude <= 180.</param>
        /// <param name="altitude">The MSL altitutde of the aircraft in feet as a floating point value.</param>
        /// <param name="heading">The heading of the aircraft as a floating point degree value. Valid values are: 0 <= uas_heading <= 360.</param>
        /// <returns>flag if submiting was successfull</returns>
        bool setUASTelemetry(double latitude, double longitude, double altitude, double heading);
    }

 

    

  

    
}
