using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Models
{
    public interface IRecordFilter
    {
        Expression<Func<IRecord, bool>> BuildDateTimeRangeFilter(DateTime begin, DateTime end);
    }
}
