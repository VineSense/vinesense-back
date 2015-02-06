using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Nickel.Models
{
    class DefaultRecordFilter : IRecordFilter
    {
        public Expression<Func<IRecord, bool>> BuildDateTimeRangeFilter(DateTime begin, DateTime end)
        {
            return (r) =>
                (begin.Date < r.Date.Value && r.Date.Value < end.Date) ||
                (begin.Date != end.Date && begin.Date == r.Date.Value && begin.TimeOfDay <= r.Time) ||
                (begin.Date != end.Date && r.Date.Value == end.Date && r.Time < end.TimeOfDay) ||
                (begin.Date == end.Date && begin.TimeOfDay <= r.Time && r.Time < end.TimeOfDay);
        }
    }
}