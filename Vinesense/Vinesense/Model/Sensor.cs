using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Model
{
    public class Sensor
    {
        public int Id { get; set; }
        public float Depth { get; set; }
        public SensorType SensorType { get; set; }

        public int SiteId { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
