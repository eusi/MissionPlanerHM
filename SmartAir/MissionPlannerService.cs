

using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace MissionPlanner.SmartAir
{
   public class MissionPlannerService:IMissionPlannerService     
    {

       [Serializable]
       public class Color
       {
           public int alpha = 0;
           public int red = 0;
           public int green = 0;
           public int blue = 0;

       }
      

        public void setWayPoints(List<Locationwp> waypoints,bool append)
        {
            try
            {


                MissionPlanner.GCSViews.FlightPlanner.instance.SetNewWayPoints(waypoints, append);

            }
            catch (Exception)
            {

               
            }
        }


        public List<Locationwp> getWayPoints()
        {
            return  MissionPlanner.GCSViews.FlightPlanner.instance.getWayPoints();
            
        }


        public void setZone(List<GMap.NET.PointLatLng> zonePoints, Color color, string zoneName)
        {
            System.Drawing.Color.FromArgb(color.alpha, color.red, color.green, color.blue);
        }


        public PointLatLngAlt getUAVPosition()
        {
          return  new PointLatLngAlt(123.23, 123.34,654.45, "Guten Tag");
        }

        //public JudgeServerInterface.Obstacles getObstacles()
        //{
        //    throw new NotImplementedException();
        //}

        public void setTarget(GMap.NET.PointLatLng target, string targetName)
        {
            
        }


       
    }
}
