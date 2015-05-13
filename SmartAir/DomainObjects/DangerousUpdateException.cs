using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir.DomainObjects
{
    public class DangerousUpdateException : Exception
    {
        public DangerousUpdateException(string errorMessage)
            : base(errorMessage)
        {



        }
    }
}
