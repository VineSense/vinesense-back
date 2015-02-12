using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Vinesense.Batch.DataSources
{
    class NewContext : DbContext
    {
        public NewContext()
            : base("name=NewContext")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Weather> Weathers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .Property(l => l.Timestamp)
                .HasColumnAnnotation
                    (
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("TimestampSensorId", 1) { IsUnique = true })
                    );
            modelBuilder.Entity<Log>()
                .Property(l => l.SensorId)
                .HasColumnAnnotation
                    (
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("TimestampSensorId", 2) { IsUnique = true })
                    );
            modelBuilder.Entity<Weather>()
                .Property(l => l.Timestamp)
                .HasColumnAnnotation
                    (
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("DateTime", 1) { IsUnique = true })
                    );
        }
    }
}
