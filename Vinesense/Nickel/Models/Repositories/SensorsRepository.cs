using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vinesense.Model;

namespace Nickel.Models
{
    class SensorsRepository : GenericRepository<Sensor>, ISensorsRepository
    {
        DbSet<Sensor> dbSet { get; set; }

        public SensorsRepository(Lazy<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
            dbSet = unitOfWork.Value.Context.Set<Sensor>();
        }

        public IQueryable<Sensor> GetType(string sensorType)
        {
            if (sensorType == "temperature")
            {
                return from s in dbSet
                       where s.SensorType == SensorType.Temperature
                       select s;
            }

            if (sensorType == "moisture")
            {
                return from s in dbSet
                       where s.SensorType == SensorType.Moisture
                       select s;
            }

            return GetType("temperature");
        }
    }
}