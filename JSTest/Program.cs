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
            js.Connect("testuser", "testpass");

            DateTime dt0 = DateTime.Now;
            ServerInfo si = js.GetServerInfo();
            DateTime dt1 = DateTime.Now;


            Console.WriteLine("ServerMessage: " + si.ServerMessage);
            Console.WriteLine("dt: " + (dt1-dt0));

            Console.ReadKey();
        }
    }
}
