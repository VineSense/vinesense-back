using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vinesense.Model;

namespace Nickel.Models
{
    class SitesRepository : GenericRepository<Site>, ISitesRepository
    {
        DbSet<Site> dbSet { get; set; }

        public SitesRepository(Lazy<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
            dbSet = unitOfWork.Value.Context.Set<Site>();
        }
    }
}