using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MissionPlanner.SmartAir
{
   public class MissionPlannerService:IMissionPlannerService     
    {


        public void setWayPoints(List<Locationwp> waypoints)
        {
            MissionPlanner.GCSViews.FlightPlanner.instance.SetNewWayPoints(waypoints);


        }


        public List<Locationwp> getWayPoints()
        {
            return MissionPlanner.GCSViews.FlightPlanner.instance.getWayPoints();
            //List<Locationwp> result = new List<Locationwp>();
            //for (int i = 0; i < 5; i++)
            //{
            //    result.Add(new Locationwp() { id = 16, alt = i, lat = 2, lng = 3, options = 1 });
            //}
            //return result;
        }
    }
}
