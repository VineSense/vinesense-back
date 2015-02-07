using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Legacy;
using Vinesense.Model;
using Vinesense.Batch.Migrators;

namespace Vinesense.Batch.Services
{
    class WeatherMigrationManager : IMigrationManager
    {
        IWeatherService WeatherService { get; set; }
        IRecordRangeFilter RecordRangeFilter { get; set; }

        public WeatherMigrationManager(IWeatherService weatherService, IRecordRangeFilter recordRangeFilter)
        {
            WeatherService = weatherService;
            RecordRangeFilter = recordRangeFilter;
        }

        public void MigrateAll()
        {
            using (var context = new LegacyContext())
            {
                Func<DateTime, IEnumerable<WeatherStation>> query = (l) =>
                    from r in context.WeatherStation.Where(RecordRangeFilter.BuildDateTimeRangeFilter(l))
                    orderby r.Id ascending
                    select (WeatherStation)r;

                DateTime last = WeatherService.GetLastTimestamp();
                if (last != DateTime.MinValue)
                {
                    last -= new TimeSpan(1, 0, 0, 0);
                }
                foreach (var l in query(last))
                {
                    WeatherService.Update(new Weather
                        {
                            Timestamp = l.ConvertDateTime(),
                            WindDirection = (int)l.WindDirection,
                            WindGust = (float)l.WindGust,
                            WindSpeed = (float)l.WindSpeed,
                            SolarRadiation = (float)l.SolarRadiation,
                            RelativeHumidity = (float)l.RelativeHumidity,
                            Temperature = (float)l.Temperature,
                            Precipitation = (float)l.Precipitation,
                            LeafWetnessCounts = (float)l.LeafWetnessCounts,
                            LeafWetnessMinutes = (float)l.LeafWetnessMinutes
                        });
                }
            }
        }
    }
}
