using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Models
{
    public interface IGraphDataService
    {
        IEnumerable<Graph> GetRangeByDepth(DateTime begin, DateTime end, int interval, string sensorType, float depth);
        IEnumerable<Graph> GetRangeBySiteId(DateTime begin, DateTime end, int interval, string sensorType, int siteId);
    }

    static class GraphDataHelper
    {
        public static IQueryable<GraphData> FilterRange(this IQueryable<GraphData> graphData, DateTime begin, DateTime end)
        {
            return from d in graphData
                   where begin <= d.Timestamp && d.Timestamp < end
                   select new GraphData
                   {
                       Timestamp = d.Timestamp,
                       Value = d.Value
                   };
        }

        private class GraphDataGroup
        {
            public int Year { get; set; }
            public int DayOfYear { get; set; }
        };

        public static IEnumerable<GraphData> GroupBy(this IQueryable<GraphData> graphData, int interval = 0)
        {
            if (interval <= 0)
            {
                return graphData;
            }

            DateTime firstDay = graphData.First().Timestamp;
            var q = from d in graphData
                    let groupNumber = DbFunctions.DiffDays(d.Timestamp, firstDay).Value / interval
                    let groupNumberInteger = DbFunctions.Truncate((double)groupNumber, 0)
                    group d by (int)groupNumberInteger.Value into g
                    select new
                    {
                        GroupNumber = g.Key,
                        Value = g.Average((d) => d.Value)
                    };

            return from v in q.AsEnumerable()
                   select new GraphData
                   {
                       Timestamp = firstDay + TimeSpan.FromDays(interval * v.GroupNumber),
                       Value = v.Value
                   };
        }
    }

    public class Graph
    {
        public int SiteId { get; set; }
        public bool IsInterExtrapolated { get; set; }
        public float Depth { get; set; }

        public IEnumerable<GraphData> Data { get; set; }
    }

    [JsonConverter(typeof(GraphDataJsonConverter))]
    public class GraphData
    {
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }

    public class GraphDataJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            GraphData data = value as GraphData;

            writer.WriteStartArray();
            writer.WriteValue(data.Timestamp);
            writer.WriteValue(data.Value);
            writer.WriteEndArray();
        }
    }
}
