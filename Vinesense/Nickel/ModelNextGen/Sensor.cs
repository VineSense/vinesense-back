using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.ModelNextGen
{
    public class Sensor
    {
        public long Id { get; set; }
        public double Depth { get; set; }

        public SensorType SensorType { get; set; }
        public virtual List<Log> Logs { get; set; }
    }
}