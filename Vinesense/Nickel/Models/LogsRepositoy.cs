using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vinesense.Model;

namespace Nickel.Models
{
    class LogsRepositoy : GenericRepository<Log>, ILogsRepository
    {
        DbSet<Log> dbSet { get; set; }

        public LogsRepositoy(Lazy<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
            dbSet = unitOfWork.Value.Context.Set<Log>();
        }
    }
}