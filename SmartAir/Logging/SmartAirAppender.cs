using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;
using log4net.Appender;

namespace MissionPlanner.SmartAir.Logging
{
   
    public class SmartAirAppender : AppenderSkeleton
    {
      

       
        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {


            if (MainV2.instance != null && MainV2.instance.FlightData != null )
            {
                MainV2.instance.FlightData.drawLogEntry(loggingEvent);
             


            }

        }
    }
}
