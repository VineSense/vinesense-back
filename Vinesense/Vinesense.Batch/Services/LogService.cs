using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    class LogService : ILogService
    {
        public void MigrateLog(DbContext context, int sensorId, DateTime timestamp, float value)
        {
            var logs = context.Set<Log>();
            var q = from l in logs
                    where l.SensorId == sensorId && l.Timestamp == timestamp
                    select l;
            if (q.Count() != 0)
            {
                return;
            }

            Log log = new Log
            {
                SensorId = sensorId,
                Timestamp = timestamp,
                Value = value
            };

            context.Set<Log>().Add(log);
        }
    }
}
