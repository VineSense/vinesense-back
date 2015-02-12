using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinesense.Model;

namespace Nickel.Models
{
    public interface IWeathersRepository : IGenericRepository<Weather>
    {
        IQueryable<WeatherValue> GetRange(DateTime begin, DateTime end);
        IQueryable<WeatherValue> GetRangeDaily(DateTime begin, DateTime end);
    }

    public class WeatherResult
    {
        public DateTime Timestamp { get; set; }
        public float Temperature { get; set; }
    }

    public class WeatherValue : Weather
    {
    }
}
