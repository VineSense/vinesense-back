using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Model
{
    public class Weather
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int WindDirection { get; set; }
        public float WindGust { get; set; }
        public float WindSpeed { get; set; }
        public float SolarRadiation { get; set; }
        public float RelativeHumidity { get; set; }
        public float Temperature { get; set; }
        public float Precipitation { get; set; }
        public float LeafWetnessCounts { get; set; }
        public float LeafWetnessMinutes { get; set; }
    }
}
