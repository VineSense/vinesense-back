using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    class WeatherService : IWeatherService
    {
        public DateTime GetLastTimestamp()
        {
            using (var context = new NewContext())
            {
                var q = from w in context.Weathers
                        orderby w.Timestamp descending
                        select w.Timestamp;

                return q.FirstOrDefault();
            }
        }

        public void Update(Weather weather)
        {
            try
            {
                using (var context = new NewContext())
                {
                    var q = from w in context.Weathers
                            where w.Timestamp == weather.Timestamp
                            select w;
                    if (q.Count() > 0)
                    {
                        return;
                    }

                    context.Weathers.Add(weather);
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                MySqlException innerException = e.InnerException.InnerException as MySqlException;
                if (innerException == null || innerException.Number != 1062)
                {
                    throw;
                }
            }
        }
    }
}
