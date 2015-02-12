using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    class SensorService : ISensorService
    {
        public Sensor ResolveSensor(int siteId, float depth, SensorType sensorType)
        {
            using (var context = new NewContext())
            {
                var q = from r in context.Sensors
                        where r.SiteId == siteId && r.Depth == depth && r.SensorType == sensorType
                        select r;

                var sensor = q.FirstOrDefault();
                if (sensor != null)
                {
                    return sensor;
                }
                sensor = new Sensor
                {
                    SiteId = siteId,
                    Depth = depth,
                    SensorType = sensorType
                };
                context.Sensors.Add(sensor);
                context.SaveChanges();
                return sensor;
            }
        }
    }
}
