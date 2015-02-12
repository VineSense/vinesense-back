using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Model
{
    public class Log
    {
        public long Id { get; set; }
        public float Value { get; set; }
        public DateTime Timestamp { get; set; }
        public int SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}
