﻿using Microsoft.Practices.Unity;
using Nickel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Vinesense.Model;

namespace Nickel.Controllers
{
    [EnableCors("*", "*", "*")]
    public class WeathersController : ApiController
    {
        IWeathersRepository WeathersRepository { get; set; }
        public WeathersController(IWeathersRepository weathersRepository)
        {
            WeathersRepository = weathersRepository;
        }

        public class WeathersControllerRequest
        {
            public IEnumerable<WeatherControllerRequestRange> Ranges { get; set; }
        }

        public class WeatherControllerRequestRange
        {
            public string Tag { get; set; }
            public DateTime? Begin { get; set; }
            public DateTime? End { get; set; }
            public int? Interval { get; set; }
            public string Column { get; set; }
        }

        private WeathersControllerSingleResult GetSingleResult(WeatherControllerRequestRange range)
        {
            DateTime begin = range.Begin ?? DateTime.MinValue;
            DateTime end = range.End ?? DateTime.MaxValue;

            var data = from w in WeathersRepository.GetRange(begin, end)
                       orderby w.Timestamp ascending
                       select w;

            return new WeathersControllerSingleResult
            {
                Tag = range.Tag,
                Data = WeathersRepository.FilterColumn(data.GroupBy(range.Interval ?? 0).ToList(), range.Column)
            };
        }

        /// <param name="begin">시작 날짜 및 시각(포함)</param>
        /// <param name="end">종료 날짜 및 시간(제외)</param>
        [HttpPost]
        [ActionName("GetMultiRange")]
        public WeathersControllerResponse GetMultiRange([FromBody]WeathersControllerRequest request)
        {
            if (request == null || request.Ranges == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var q = from range in request.Ranges
                    select GetSingleResult(range);

            return new WeathersControllerResponse
            {
                Result = q.ToList()
            };
        }

        public class WeathersControllerSingleResult
        {
            public IEnumerable<WeatherResult> Data { get; set; }
            public string Tag { get; set; }
        }

        public class WeathersControllerResponse
        {
            public IEnumerable<WeathersControllerSingleResult> Result { get; set; }
        }
    }

    public static class WeatherResultHelper
    {
        public static IEnumerable<WeatherValue> GroupBy(this IQueryable<WeatherValue> data, int interval = 0)
        {
            if (interval <= 0)
            {
                return data;
            }

            DateTime firstDay = data.Count() > 0 ? data.First().Timestamp : DateTime.MinValue;
            var q = from d in data
                    let groupNumber = VinesenseContext.DateDiff(d.Timestamp, firstDay).Value / interval
                    let groupNumberInteger = DbFunctions.Truncate((double)groupNumber, 0)
                    group d by (int)groupNumberInteger.Value into g
                    select new
                    {
                        GroupNumber = g.Key,
                        Temperature = g.Average((d) => d.Temperature),
                        Precipitation = g.Average((d) => d.Precipitation),
                        LeafWetnessCounts = g.Average((d) => d.LeafWetnessCounts),
                        LeafWetnessMinutes = g.Average((d) => d.LeafWetnessMinutes),
                        RelativeHumidity = g.Average((d) => d.RelativeHumidity),
                        SolarRadiation = g.Average((d) => d.SolarRadiation),
                        WindDirection = g.Average((d) => d.WindDirection),
                        WindGust = g.Average((d) => d.WindGust),
                        WindSpeed = g.Average((d) => d.WindSpeed)
                    };

            return from v in q.AsEnumerable()
                   select new WeatherValue
                   {
                       Timestamp = firstDay + TimeSpan.FromDays(interval * v.GroupNumber),
                       Temperature = v.Temperature,
                       Precipitation = v.Precipitation,
                       LeafWetnessCounts = v.LeafWetnessCounts,
                       LeafWetnessMinutes = v.LeafWetnessMinutes,
                       RelativeHumidity = v.RelativeHumidity,
                       SolarRadiation = v.SolarRadiation,
                       WindDirection = (int)v.WindDirection,
                       WindGust = v.WindGust,
                       WindSpeed = v.WindSpeed
                   };
        }
    }
}