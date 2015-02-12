using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.Services;
using Vinesense.Legacy;
using Vinesense.Model;

namespace Vinesense.Batch.Migrators
{
    class Site2Migrator : SiteMigrator<Site2>
    {
        public Site2Migrator(ISiteService siteService, ISensorService sensorService, ILogService logService)
            : base(siteService, sensorService, logService)
        {
        }

        public override int Number
        {
            get { return 2; }
        }

        public override string Name
        {
            get { return "Site2"; }
        }

        public override double Latitude
        {
            get { return 0; }
        }

        public override double Longitude
        {
            get { return 0; }
        }

        public override void MigrateLogs(Site2 site)
        {
            DateTime timestamp = site.ConvertDateTime();
            MigrateLog(timestamp, (float)site.S1Moist, 1, SensorType.Moisture);
            MigrateLog(timestamp, (float)site.S1Temp, 1, SensorType.Temperature);
            MigrateLog(timestamp, (float)site.S2WaterPotentialKpa, 2, SensorType.WaterPotential);
            MigrateLog(timestamp, (float)site.S2Temp, 2, SensorType.Temperature);
            MigrateLog(timestamp, (float)site.S3Moist, 3, SensorType.Moisture);
            MigrateLog(timestamp, (float)site.S3Temp, 3, SensorType.Temperature);
            MigrateLog(timestamp, (float)site.S4Moist, 5, SensorType.Moisture);
            MigrateLog(timestamp, (float)site.S4Temp, 5, SensorType.Temperature);
            MigrateLog(timestamp, (float)site.S5Moist, 7, SensorType.Moisture);
            MigrateLog(timestamp, (float)site.S5Temp, 7, SensorType.Temperature);
        }
    }
}
