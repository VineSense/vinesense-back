using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Legacy;

namespace Vinesense.Batch.DataSources
{
    class LegacyContext : DbContext
    {
        public LegacyContext()
            : base("name=LegacyContext")
        {
        }

        public virtual DbSet<AldoStation> AldoStation { get; set; }
        public virtual DbSet<Site1> Site1 { get; set; }
        public virtual DbSet<Site2> Site2 { get; set; }
        public virtual DbSet<Site3> Site3 { get; set; }
        public virtual DbSet<Site4> Site4 { get; set; }
        public virtual DbSet<Site5> Site5 { get; set; }
        public virtual DbSet<Site6> Site6 { get; set; }
        public virtual DbSet<WeatherStation> WeatherStation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {    
        }
    }
}
