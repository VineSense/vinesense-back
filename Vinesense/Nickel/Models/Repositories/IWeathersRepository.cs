﻿using Newtonsoft.Json;
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
        IEnumerable<WeatherResult> FilterColumn(IEnumerable<WeatherValue> weatherValues, string column);
    }

    [JsonConverter(typeof(WeatherResultJsonConverter))]
    public class WeatherResult
    {
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }

    public class WeatherResultJsonConverter : JsonConverter
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
            WeatherResult weatherResult = value as WeatherResult;

            writer.WriteStartArray();
            writer.WriteValue(weatherResult.Timestamp);
            writer.WriteValue(weatherResult.Value);
            writer.WriteEndArray();
        }
    }

    public class WeatherValue : Weather
    {
    }
}
