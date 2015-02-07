using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.ModelNextGen
{
    public class Log
    {
        public long Id { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}