
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MissionPlanner.SmartAir
{
   public class MissionPlannerService:IMissionPlannerService     
    {


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
            return MissionPlanner.GCSViews.FlightPlanner.instance.getWayPoints();
            
        }


        public void setZone(List<GMap.NET.PointLatLng> zonePoints, System.Drawing.Color color, string zoneName)
        {
            throw new NotImplementedException();
        }


        public PointLatLngAlt getUAVPosition()
        {
            throw new NotImplementedException();
        }

        //public JudgeServerInterface.Obstacles getObstacles()
        //{
        //    throw new NotImplementedException();
        //}

        public void setTarget(GMap.NET.PointLatLng target, string targetName)
        {
            throw new NotImplementedException();
        }
    }
}
