using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

using JudgeServerInterface;

namespace JSUnitTests
{
    [TestClass]
    public class JsonsParserTests
    {
        [TestMethod]
        public void TestServerInfoParser()
        {
            String serverTime = "2015-01-05 11:45:41.867037";
            String serverMessage = "Hello SAM!";
            String messageTime = "2014-11-10 17:07:13.846320+00:00";
            String serverInfoJson = "{\"server_info\": {\"message_timestamp\": \"" + messageTime + "\", \"message\": \"" + serverMessage + "\"}, \"server_time\": \"" + serverTime + "\"}";
            ServerInfo si = JsonDeserializer.GetServerInfo(serverInfoJson);
            Assert.AreEqual<String>(si.MessageTimeStamp, messageTime);
            Assert.AreEqual<String>(si.ServerMessage, serverMessage);
            Assert.AreEqual<String>(si.ServerTime, serverTime);
        }

        [TestMethod]
        public void TestObstacleParser()
        {
            
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            nfi.NumberGroupSeparator = ",";
            
            Obstacles corrObs = new Obstacles();
            corrObs.StationaryObstacles.Add(new StationaryObstacle() { Latitude=47.261457, CylinderHeight=700, CylinderRadius=250, Longitude=11.358361});
            corrObs.StationaryObstacles.Add(new StationaryObstacle() { Latitude=47.25816, CylinderHeight=100, CylinderRadius=30, Longitude=11.347564});
            corrObs.MovingObstacles.Add(new MovingObstacle() { Latitude = 47.257825041439524, SphereRadius = 100, Altitude = 382.6521337257496, Longitude = 11.353860992816168 });
            corrObs.MovingObstacles.Add(new MovingObstacle() { Latitude = 47.2557324177191, SphereRadius = 80, Altitude = 807.772388953518, Longitude=11.343533031193452});
            String obstaclesJson = "{\"stationary_obstacles\": [{\"latitude\": " + corrObs.StationaryObstacles[0].Latitude.ToString(nfi) + ", \"cylinder_height\": " + corrObs.StationaryObstacles[0].CylinderHeight.ToString(nfi) + ", \"cylinder_radius\": " + corrObs.StationaryObstacles[0].CylinderRadius.ToString(nfi) + ", \"longitude\": " + corrObs.StationaryObstacles[0].Longitude.ToString(nfi) + "}, {\"latitude\": " + corrObs.StationaryObstacles[1].Latitude.ToString(nfi) + ", \"cylinder_height\": " + corrObs.StationaryObstacles[1].CylinderHeight.ToString(nfi) + ", \"cylinder_radius\": " + corrObs.StationaryObstacles[1].CylinderRadius.ToString(nfi) + ", \"longitude\": " + corrObs.StationaryObstacles[1].Longitude.ToString(nfi) + "}], \"moving_obstacles\": [{\"latitude\": " + corrObs.MovingObstacles[0].Latitude.ToString(nfi) + ", \"sphere_radius\": " + corrObs.MovingObstacles[0].SphereRadius.ToString(nfi) + ", \"altitude_msl\": " + corrObs.MovingObstacles[0].Altitude.ToString(nfi) + ", \"longitude\": " + corrObs.MovingObstacles[0].Longitude.ToString(nfi) + "}, {\"latitude\": " + corrObs.MovingObstacles[1].Latitude.ToString(nfi) + ", \"sphere_radius\": " + corrObs.MovingObstacles[1].SphereRadius.ToString(nfi) + ", \"altitude_msl\": " + corrObs.MovingObstacles[1].Altitude.ToString(nfi) + ", \"longitude\": " + corrObs.MovingObstacles[1].Longitude.ToString(nfi) + "}]}";

            Obstacles parObs = JsonDeserializer.GetObstacles(obstaclesJson);

            Assert.AreEqual<Double>(corrObs.StationaryObstacles[0].Longitude, parObs.StationaryObstacles[0].Longitude);
            Assert.AreEqual<Double>(corrObs.StationaryObstacles[0].Latitude, parObs.StationaryObstacles[0].Latitude);
            Assert.AreEqual<Double>(corrObs.StationaryObstacles[0].CylinderRadius, parObs.StationaryObstacles[0].CylinderRadius);
            Assert.AreEqual<Double>(corrObs.StationaryObstacles[0].CylinderHeight, parObs.StationaryObstacles[0].CylinderHeight);
        }
    }
}
