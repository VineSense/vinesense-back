using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.Services;
using Vinesense.Legacy;
using Vinesense.Model;

namespace Vinesense.Batch.Migrators
{
    class Site4Migrator : SiteMigrator<Site4>
    {
        public Site4Migrator(ISiteService siteService, ISensorService sensorService, ILogService logService)
            : base(siteService, sensorService, logService)
        {
        }

        public override int Number
        {
            get { return 4; }
        }

        public override string Name
        {
            get { return "Site4"; }
        }

        public override double Latitude
        {
            get { return 0; }
        }

        public override double Longitude
        {
            get { return 0; }
        }

        public override void MigrateLogs(DbContext context, Site4 site)
        {
            DateTime timestamp = site.ConvertDateTime();
            MigrateLog(context, timestamp, (float)site.S1Moist, 1, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S1Temp, 1, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S2Moist, 2, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S2Temp, 2, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S3Moist, 3, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S3Temp, 3, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S4Moist, 4, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S4Temp, 4, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S5Moist, 5, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S5Temp, 5, SensorType.Temperature);
        }
    }
}
