namespace Nickel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using Vinesense.Model;
    using System.Data.Entity.Infrastructure.Annotations;
    using MySql.Data.Entity;
    using System.Data.Entity.Infrastructure;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class VinesenseContext : DbContext
    {
        public VinesenseContext()
            : base("name=VinesenseModel")
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
