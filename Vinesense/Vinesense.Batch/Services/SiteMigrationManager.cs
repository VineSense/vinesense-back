using Microsoft.Practices.Unity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        IEnumerable<Task> MigrateAllTasks()
        {
            yield return MigrateSite(
                (context, last) =>
                    from r in context.Site1.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (Site1)r
                );
            yield return MigrateSite(
                (context, last) =>
                    from r in context.Site2.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (Site2)r
                );
            yield return MigrateSite(
                (context, last) =>
                    from r in context.Site3.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (Site3)r
                );
            yield return MigrateSite(
                (context, last) =>
                    from r in context.Site4.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (Site4)r
                );
            yield return MigrateSite(
                (context, last) =>
                    from r in context.Site5.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (Site5)r
                );
            yield return MigrateSite(
                (context, last) =>
                    from r in context.Site6.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (Site6)r
                );
            yield return MigrateSite(
                (context, last) =>
                    from r in context.AldoStation.Where(RecordRangeFilter.BuildDateTimeRangeFilter(last))
                    orderby r.Id ascending
                    select (AldoStation)r
                );
        }

        public void MigrateAll()
        {
            Task.WaitAll(MigrateAllTasks().ToArray());
        }

        Task MigrateSite<T>(Func<LegacyContext, DateTime, IEnumerable<T>> query)
            where T : ISiteRecord
        {
            Console.WriteLine(typeof(T).ToString());
            SiteMigrator<T> migrator = Container.Resolve<SiteMigrator<T>>();
            migrator.MigrateSite();

            DateTime last = SiteService.GetLastTimestamp(migrator.Number);
            if (last != DateTime.MinValue)
            {
                last -= new TimeSpan(0, 2, 0, 0);
            }

            IEnumerable<T> legacyData;
            using (var legacyContext = new LegacyContext())
            {
                legacyData = query(legacyContext, last).ToList();
            }

            return Task.Run(() =>
            {
                foreach (var l in legacyData)
                {
                    using (var context = new NewContext())
                    {
                        context.Configuration.AutoDetectChangesEnabled = false;
                        migrator.MigrateLogs(context, l);
                        context.ChangeTracker.DetectChanges();
                        context.SaveChanges();
                    }
                }
            });
        }
    }
}
