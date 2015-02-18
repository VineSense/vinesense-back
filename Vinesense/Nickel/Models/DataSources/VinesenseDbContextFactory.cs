using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Nickel.Models
{
    class VinesenseDbContextFactory : IDbContextFactory
    {
        public DbContext CreateContext()
        {
            var context = new VinesenseContext();
            context.Database.Log = (log) => Debug.WriteLine(log);
            return context;
        }
    }
}