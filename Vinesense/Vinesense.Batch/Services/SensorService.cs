using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    class SensorService : ISensorService
    {
        public Sensor ResolveSensor(DbContext context, int siteId, float depth, SensorType sensorType)
        {
            var sensors = context.Set<Sensor>();
            var q = from r in sensors
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
            sensors.Add(sensor);
            context.SaveChanges();
            return sensor;
        }
    }
}
