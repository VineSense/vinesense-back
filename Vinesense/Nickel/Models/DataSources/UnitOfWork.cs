using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Nickel.Models
{
    class UnitOfWork : IUnitOfWork, IDisposable
    {
        DbContext dbContext { get; set; }
        bool disposed = false;

        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            dbContext = dbContextFactory.CreateContext();
        }

        public DbContext Context
        {
            get { return dbContext; }
        }

        public void SaveChanges()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}