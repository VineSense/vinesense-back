using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Batch.Migrators;
using Vinesense.Legacy;

namespace Vinesense.Batch.Services
{
    class SiteMigrationManager : IMigrationManager
    {
        ISiteService SiteService { get; set; }
        IUnityContainer Container { get; set; }
        IRecordRangeFilter RecordRangeFilter { get; set; }

        public SiteMigrationManager(ISiteService siteService, IUnityContainer container, IRecordRangeFilter recordRangeFilter)
        {
            SiteService = siteService;
            Container = container;
            RecordRangeFilter = recordRangeFilter;
        }

        public void MigrateAll()
        {
            using (var context = new LegacyContext())
            {
                MigrateSite(
                    (last) =>
                        from r in context.Site1.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (Site1)r
                    );
                MigrateSite(
                    (last) =>
                        from r in context.Site2.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (Site2)r
                    );
                MigrateSite(
                    (last) =>
                        from r in context.Site3.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (Site3)r
                    );
                MigrateSite(
                    (last) =>
                        from r in context.Site4.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (Site4)r
                    );
                MigrateSite(
                    (last) =>
                        from r in context.Site5.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (Site5)r
                    );
                MigrateSite(
                    (last) =>
                        from r in context.Site6.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (Site6)r
                    );
                MigrateSite(
                    (last) =>
                        from r in context.AldoStation.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                        orderby r.Id ascending
                        select (AldoStation)r
                    );
            }
        }

        void MigrateSite<T>(Func<DateTime, IEnumerable<T>> query)
            where T : ISiteRecord
        {
            Console.WriteLine(typeof(T).ToString());
            SiteMigrator<T> migrator = Container.Resolve<SiteMigrator<T>>();
            migrator.MigrateSite();

            DateTime last = SiteService.GetLastTimestamp(migrator.Number);
            if (last != DateTime.MinValue)
            {
                last -= new TimeSpan(1, 0, 0, 0);
            }
            foreach (var l in query(last))
            {
                migrator.MigrateLogs(l);
            }
        }
    }
}
