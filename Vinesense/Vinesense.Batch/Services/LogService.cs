using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        public void MigrateLog(int sensorId, DateTime timestamp, float value)
        {
            try
            {
                using (var context = new NewContext())
                {
                    Log log = new Log
                    {
                        SensorId = sensorId,
                        Timestamp = timestamp,
                        Value = value
                    };
                    context.Logs.Add(log);
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                MySqlException innerException = e.InnerException.InnerException as MySqlException;
                if (innerException == null || innerException.Number != 1062)
                {
                    throw;
                }
            }
        }
    }
}
