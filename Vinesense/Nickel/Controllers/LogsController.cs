using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Nickel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Serialization;
using Vinesense.Model;

namespace Nickel.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LogsController : ApiController
    {
        IWeathersRepository WeathersRepository { get; set; }
        IGraphDataService GraphDataService { get; set; }
        ISitesRepository SitesRepository { get; set; }
        public LogsController(IGraphDataService graphDataService, ISitesRepository sitesRepository, IWeathersRepository weathersRepository)
        {
            GraphDataService = graphDataService;
            SitesRepository = sitesRepository;
            WeathersRepository = weathersRepository;
        }

        public class GetRangeBySiteIdRequest
        {
            public int SiteId { get; set; }
            public string SensorType { get; set; }
            public DateTime? Begin { get; set; }
            public DateTime? End { get; set; }
            public int? Interval { get; set; }
        }

        [HttpPost]
        [ActionName("GetRangeBySiteId")]
        public GetBySiteResponse GetRangeBySiteId([FromBody]GetRangeBySiteIdRequest request)
        {
            if (request == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            DateTime begin = request.Begin ?? DateTime.MinValue;
            DateTime end = request.End ?? DateTime.MaxValue;
            int siteId = request.SiteId;
            string sensorType = request.SensorType;
            int interval = request.Interval ?? 0;

            Site site = SitesRepository.GetById(siteId);
            if (site == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var result = GraphDataService.GetRangeBySiteId(begin, end, interval, sensorType, siteId);
            foreach (var graph in result)
            {
                graph.Data = graph.Data.ToList();
            }

            var dateTimes = from graph in result
                            from data in graph.Data
                            select data.Timestamp;

            IEnumerable<WeatherResult> weatherResult = null;

            if (dateTimes.Count() != 0)
            {
                DateTime weatherBegin = dateTimes.Min();
                DateTime weatherEnd = dateTimes.Max();

                var data = from w in WeathersRepository.GetRange(weatherBegin, weatherEnd)
                           orderby w.Timestamp ascending
                           select w;

                weatherResult = WeathersRepository.FilterColumn(data.GroupBy(interval).ToList(), "Precipitation");
            }

            return new GetBySiteResponse
            {
                Site = new SiteValue
                {
                    Id = site.Id,
                    Latitude = site.Latitude,
                    Longitude = site.Longitude,
                    Name = site.Name,
                    Number = site.Number
                },
                Result = result,
                Weather = weatherResult
            };
        }

        public class GetRangeByDepthRequest
        {
            public float Depth { get; set; }
            public string SensorType { get; set; }
            public DateTime? Begin { get; set; }
            public DateTime? End { get; set; }
            public int? Interval { get; set; }
        }

        [HttpPost]
        [ActionName("GetRangeByDepth")]
        public GetByDepthResponse GetRangeByDepth([FromBody]GetRangeByDepthRequest request)
        {
            if (request == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            DateTime begin = request.Begin ?? DateTime.MinValue;
            DateTime end = request.End ?? DateTime.MaxValue;
            float depth = request.Depth;
            string sensorType = request.SensorType;
            int interval = request.Interval ?? 0;

            var result = GraphDataService.GetRangeByDepth(begin, end, interval, sensorType, depth).ToList();
            foreach(var graph in result)
            {
                graph.Data = graph.Data.ToList();
            }
            
            var dateTimes = from graph in result
                            from data in graph.Data
                            select data.Timestamp;

            IEnumerable<WeatherResult> weatherResult = null;

            if (dateTimes.Count() != 0)
            {
                DateTime weatherBegin = dateTimes.Min();
                DateTime weatherEnd = dateTimes.Max();

                var data = from w in WeathersRepository.GetRange(weatherBegin, weatherEnd)
                           orderby w.Timestamp ascending
                           select w;

                weatherResult = WeathersRepository.FilterColumn(data.GroupBy(interval).ToList(), "Precipitation");
            }

            return new GetByDepthResponse
            {
                Depth = depth,
                Result = result,
                Weather = weatherResult
            };
        }

        public class GetBySiteResponse
        {
            public SiteValue Site { get; set; }
            public IEnumerable<Graph> Result { get; set; }
            public IEnumerable<WeatherResult> Weather { get; set; }
        }

        public class GetByDepthResponse
        {
            public float Depth { get; set; }
            public IEnumerable<Graph> Result { get; set; }
            public IEnumerable<WeatherResult> Weather { get; set; }
        }
    }
}
