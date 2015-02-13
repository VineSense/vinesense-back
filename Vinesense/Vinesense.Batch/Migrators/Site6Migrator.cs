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
    class Site6Migrator : SiteMigrator<Site6>
    {
        public Site6Migrator(ISiteService siteService, ISensorService sensorService, ILogService logService)
            : base(siteService, sensorService, logService)
        {
        }

        public override int Number
        {
            get { return 6; }
        }

        public override string Name
        {
            get { return "Site6"; }
        }

        public override double Latitude
        {
            get { return 0; }
        }

        public override double Longitude
        {
            get { return 0; }
        }

        public override void MigrateLogs(DbContext context, Site6 site)
        {
            DateTime timestamp = site.ConvertDateTime();
            MigrateLog(context, timestamp, (float)site.S1Moist, 1, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S1Temp, 1, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S2Moist, 3, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S2Temp, 3, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S3Moist, 5, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S3Temp, 5, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S4Moist, 7, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S4Temp, 7, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.S5Moist, 9, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.S5Temp, 9, SensorType.Temperature);
        }
    }
}
