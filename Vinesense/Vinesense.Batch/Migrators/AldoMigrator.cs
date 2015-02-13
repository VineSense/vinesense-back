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
    class AldoMigrator : SiteMigrator<AldoStation>
    {
        public AldoMigrator(ISiteService siteService, ISensorService sensorService, ILogService logService)
            : base(siteService, sensorService, logService)
        {
        }

        public override int Number
        {
            get { return 7; }
        }

        public override string Name
        {
            get { return "Aldo"; }
        }

        public override double Latitude
        {
            get { return 0; }
        }

        public override double Longitude
        {
            get { return 0; }
        }

        public override void MigrateLogs(DbContext context, AldoStation site)
        {
            DateTime timestamp = site.ConvertDateTime();
            MigrateLog(context, timestamp, (float)site.P1Humidity, 2, SensorType.Humidity);
            MigrateLog(context, timestamp, (float)site.P1Temp, 2, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.P2WaterPotential, 5, SensorType.WaterPotential);
            MigrateLog(context, timestamp, (float)site.P2Temp, 5, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.P3Moisture, 7, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.P3Temp, 7, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.P4Moisture, 9, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.P4Temp, 9, SensorType.Temperature);
            MigrateLog(context, timestamp, (float)site.P5Moisture, 0, SensorType.Moisture);
            MigrateLog(context, timestamp, (float)site.P5Temp, 0, SensorType.Temperature);
        }
    }
}
