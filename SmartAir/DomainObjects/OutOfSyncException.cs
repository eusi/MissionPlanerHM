using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir.DomainObjects
{
    public class OutOfSyncException : Exception
    {
        public OutOfSyncException(string errorMessage)
            : base(errorMessage)
        {



        }
    }
}
