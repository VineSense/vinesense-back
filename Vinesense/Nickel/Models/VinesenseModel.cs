namespace Nickel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class VinesenseModel : DbContext
    {
        public VinesenseModel()
            : base("name=VinesenseModel")
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
            modelBuilder.Entity<AldoStation>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Site1>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Site2>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Site3>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Site4>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Site5>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Site6>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<WeatherStation>()
                .Property(e => e.DateTime)
                .IsUnicode(false);
        }
    }
}
