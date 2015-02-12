using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Model
{
    public class Site
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
