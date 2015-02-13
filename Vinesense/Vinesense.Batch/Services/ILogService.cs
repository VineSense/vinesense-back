using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    interface ILogService
    {
        void MigrateLog(DbContext context, int sensorId, DateTime timestamp, float value);
    }
}
