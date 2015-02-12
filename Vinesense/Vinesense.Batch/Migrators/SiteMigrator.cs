using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinesense.Batch.Services;
using Vinesense.Legacy;
using Vinesense.Model;

namespace Vinesense.Batch.Migrators
{
    abstract class SiteMigrator<T>
        where T : ISiteRecord
    {
        public SiteMigrator(ISiteService siteService, ISensorService sensorService, ILogService logService)
        {
            SiteService = siteService;
            SensorService = sensorService;
            LogService = logService;
        }

        ISiteService SiteService { get; set; }
        ISensorService SensorService { get; set; }
        ILogService LogService { get; set; }

        public abstract int Number { get; }
        public abstract string Name { get; }
        public abstract double Latitude { get; }
        public abstract double Longitude { get; }

        public abstract void MigrateLogs(T site);

        public void MigrateSite()
        {
            SiteService.ResolveSite(Number);
            SiteService.UpdateSite(Number, Name, Latitude, Longitude);
        }

        protected void MigrateLog(DateTime timestamp, float value, float depth, SensorType sensorType)
        {
            int siteId = SiteService.ResolveSite(Number);
            Sensor sensor = SensorService.ResolveSensor(siteId, depth, sensorType);
            LogService.MigrateLog(sensor.Id, timestamp, value);
        }
    }
}
