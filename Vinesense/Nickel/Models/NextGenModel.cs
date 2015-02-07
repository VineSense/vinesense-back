using Nickel.ModelNextGen;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Nickel.Models
{
    public class NextGenModel : DbContext
    {
        public NextGenModel()
            : base("name=VinesenseModel")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
//        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<Nickel.ModelNextGen.Site> Sites { get; set; }
    }
}
