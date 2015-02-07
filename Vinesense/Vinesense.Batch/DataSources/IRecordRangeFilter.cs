using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Legacy;

namespace Vinesense.Batch.DataSources
{
    interface IRecordRangeFilter
    {
        Expression<Func<IRecord, bool>> BuildDateTimeRangeFilter(DateTime beginExcluded);
    }
}
