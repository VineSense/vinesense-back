using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
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

        public IEnumerable<WeatherResult> FilterColumn(IEnumerable<WeatherValue> weatherValues, string column)
        {
            column = column ?? "";

            Dictionary<string, Func<WeatherValue, float>> functions = new Dictionary<string, Func<WeatherValue, float>>();            
            functions["Temperature"] = (v) => v.Temperature;
            functions["LeafWetnessCounts"] = (v) => v.LeafWetnessCounts;
            functions["LeafWetnessMinutes"] = (v) => v.LeafWetnessMinutes;
            functions["Precipitation"] = (v) => v.Precipitation;
            functions["RelativeHumidity"] = (v) => v.RelativeHumidity;
            functions["SolarRadiation"] = (v) => v.SolarRadiation;
            functions["WindDirection"] = (v) => v.WindDirection;
            functions["WindGust"] = (v) => v.WindGust;
            functions["WindSpeed"] = (v) => v.WindSpeed;
            if (functions.ContainsKey(column) == false)
            {
                column = "Temperature";
            }

            var func = functions[column];


            return from v in weatherValues
                   select new WeatherResult
                   {
                       Timestamp = v.Timestamp,
                       Value = func(v)
                   };
        }
    }
}