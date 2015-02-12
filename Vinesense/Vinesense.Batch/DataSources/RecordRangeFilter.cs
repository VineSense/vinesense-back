using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Legacy;

namespace Vinesense.Batch.DataSources
{
    class RecordRangeFilter : IRecordRangeFilter
    {
        public Expression<Func<IRecord, bool>> BuildDateTimeRangeFilter(DateTime beginExcluded)
        {
            return (r) =>
                (beginExcluded.Date < r.Date.Value) ||
                (beginExcluded.Date == r.Date.Value && beginExcluded.TimeOfDay < r.Time.Value);
        }
    }
}
