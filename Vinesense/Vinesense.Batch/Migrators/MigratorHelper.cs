using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Legacy;

namespace Vinesense.Batch.Migrators
{
    static class MigratorHelper
    {
        public static DateTime ConvertDateTime(this IRecord record)
        {
            if (record.Date == null || record.Time == null)
            {
                throw new ArgumentException();
            }
            return (DateTime)record.Date + (TimeSpan)record.Time;
        }
    }
}
