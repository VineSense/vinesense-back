using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Batch.Migrators;
using Vinesense.Batch.Services;
using Vinesense.Legacy;

namespace Vinesense.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigUnityContainer().Resolve<Program>().Start();
        }

        static IUnityContainer ConfigUnityContainer()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType<IRecordRangeFilter, RecordRangeFilter>()
                .RegisterType<SiteMigrator<AldoStation>, AldoMigrator>()
                .RegisterType<SiteMigrator<Site1>, Site1Migrator>()
                .RegisterType<SiteMigrator<Site2>, Site2Migrator>()
                .RegisterType<SiteMigrator<Site3>, Site3Migrator>()
                .RegisterType<SiteMigrator<Site4>, Site4Migrator>()
                .RegisterType<SiteMigrator<Site5>, Site5Migrator>()
                .RegisterType<SiteMigrator<Site6>, Site6Migrator>()
                .RegisterType<ILogService, LogService>()
                .RegisterType<ISensorService, SensorService>()
                .RegisterType<ISiteService, SiteService>()
                .RegisterType<IWeatherService, WeatherService>();

            container.RegisterInstance<IUnityContainer>(container);
            return container;
        }

        public Queue<IMigrationManager> WorkQueue { get; set; }

        public Program(IUnityContainer container)
        {
            WorkQueue = new Queue<IMigrationManager>();
            WorkQueue.Enqueue(container.Resolve<SiteMigrationManager>());
            WorkQueue.Enqueue(container.Resolve<WeatherMigrationManager>());
        }

        void Start()
        {
            DeleteAndCreateNewDatabase(false);
            MigrateAll();
        }

        void DeleteAndCreateNewDatabase(bool delete)
        {
            using (var context = new NewContext())
            {
                if (delete)
                {
                    context.Database.Delete();
                }
                context.Database.CreateIfNotExists();
            }
        }

        void MigrateAll()
        {
            int size = WorkQueue.Count;
            int i = 0;

            while (WorkQueue.Count > 0)
            {
                Console.WriteLine("{0} / {1}", ++i, size);
                var manager = WorkQueue.Dequeue();
                manager.MigrateAll();
            }
        }
    }
}
