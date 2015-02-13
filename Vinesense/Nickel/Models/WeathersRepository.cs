using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Vinesense.Model;

namespace Nickel.Models
{
    class WeathersRepository : GenericRepository<Weather>, IWeathersRepository
    {
        DbContext DbContext { get; set; }
        DbSet<Weather> DbSet { get; set; }

        public WeathersRepository(Lazy<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
            DbContext = unitOfWork.Value.Context;
            DbSet = unitOfWork.Value.Context.Set<Weather>();
        }

        public IQueryable<WeatherValue> GetRange(DateTime begin, DateTime end)
        {
            return from w in DbSet
                   where begin <= w.Timestamp && w.Timestamp < end
                   select new WeatherValue
                   {
                       LeafWetnessCounts = w.LeafWetnessCounts,
                       LeafWetnessMinutes = w.LeafWetnessMinutes,
                       Precipitation = w.Precipitation,
                       RelativeHumidity = w.RelativeHumidity,
                       SolarRadiation = w.SolarRadiation,
                       Temperature = w.Temperature,
                       Timestamp = w.Timestamp,
                       WindDirection = w.WindDirection,
                       WindGust = w.WindGust,
                       WindSpeed = w.WindSpeed
                   };
        }
    }
}