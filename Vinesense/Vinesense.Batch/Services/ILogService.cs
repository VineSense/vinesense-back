using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    interface ILogService
    {
        void MigrateLog(int sensorId, DateTime timestamp, float value);
    }
}
