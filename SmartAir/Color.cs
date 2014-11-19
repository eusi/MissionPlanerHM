using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.SmartAir
{
    [Serializable]
    public class Color
    {
        public Color(int alpha, int red, int green, int blue)
        {
            this.alpha = alpha;
            this.red = red;
            this.green = green;
            this.blue = blue;
        
        }
        public int alpha = 0;
        public int red = 0;
        public int green = 0;
        public int blue = 0;


    }
}
