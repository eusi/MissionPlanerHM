using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JudgeServerInterface;

namespace JSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IJudgeServer js = new JudgeServer();
            js.Connect("http://10.28.2.161:8080", "testuser", "testpass");

            js.GetServerInfo();

            DateTime dt0 = DateTime.Now;
            ServerInfo si = js.GetServerInfo();
            DateTime dt1 = DateTime.Now;

            Console.WriteLine("ServerMessage: " + si.ServerMessage);
            Console.WriteLine("MessageTimeStamp: " + si.MessageTimeStamp);
            Console.WriteLine("ServerTime: " + si.ServerTime);
            Console.WriteLine("dt: " + (dt1-dt0));
            Console.WriteLine("");

            dt0 = DateTime.Now;
            Obstacles obs = js.GetObstacles();
            dt1 = DateTime.Now;

            Console.WriteLine("Obstacles:");
            foreach (StationaryObstacle so in obs.StationaryObstacles)
                Console.WriteLine("\tSO: h=" + so.CylinderHeight.ToString()
                                    + "; r=" + so.CylinderRadius.ToString()
                                    + "; long=" + so.Longitude
                                    + "; lat=" + so.Latitude);
            Console.WriteLine("dt: " + (dt1 - dt0));
            Console.WriteLine("");

            dt0 = DateTime.Now;
            js.setUASTelemetry(38.14873256, -76.42888069, 82.45, 146.345);
            dt1 = DateTime.Now;

            Console.WriteLine("Transmitting UAS telemetry: done");
            Console.WriteLine("dt: " + (dt1 - dt0));

            Console.ReadKey();
        }
    }
}
