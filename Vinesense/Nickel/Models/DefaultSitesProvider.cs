using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Models
{
    class DefaultSitesProvider : ISitesProvider
    {
        IRecordFilter RecordFilter { get; set; }

        public Dictionary<int, Site> Sites { get; private set; }

        public DefaultSitesProvider()
        {
        }

        public DefaultSitesProvider(IRecordFilter recordFilter)
        {
            RecordFilter = recordFilter;

            Sites = new Dictionary<int, Site>();
            Sites[1] = new Site
            {
                Name = "site1",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site1.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (Site1)r
            };
            Sites[2] = new Site
            {
                Name = "site2",
                Depths = new List<int> { 1, 2, 3, 5, 7 },
                Query = (context, begin, end) => from r in context.Site2.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (Site2)r
            };
            Sites[3] = new Site
            {
                Name = "site3",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site3.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (Site3)r
            };
            Sites[4] = new Site
            {
                Name = "site4",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site4.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (Site6)r
            };
            Sites[5] = new Site
            {
                Name = "site5",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site5.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (Site5)r
            };
            Sites[6] = new Site
            {
                Name = "Biale Ranch",
                Depths = new List<int> { 1, 3, 5, 7, 9 },
                Query = (context, begin, end) => from r in context.Site6.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (Site6)r
            };
            Sites[7] = new Site
            {
                Name = "Aldo vineyard",
                Depths = new List<int> { 2, 5, 7, 9, 0 },
                Query = (context, begin, end) => from r in context.AldoStation.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select (AldoStation)r
            };
        }
    }
}