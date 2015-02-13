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
using System.Xml.Serialization;
using Vinesense.Model;

namespace Nickel.Controllers
{
    public class LogsController : ApiController
    {
        IGraphDataService GraphDataService { get; set; }
        ISitesRepository SitesRepository { get; set; }
        public LogsController(IGraphDataService graphDataService, ISitesRepository sitesRepository)
        {
            GraphDataService = graphDataService;
            SitesRepository = sitesRepository;
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
                Result = GraphDataService.GetRangeBySiteId(begin, end, interval, sensorType, siteId)
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

            return new GetByDepthResponse
            {
                Depth = depth,
                Result = GraphDataService.GetRangeByDepth(begin, end, interval, sensorType, depth)
            };
        }

        public class GetBySiteResponse
        {
            public SiteValue Site { get; set; }
            public IEnumerable<Graph> Result { get; set; }
        }

        public class GetByDepthResponse
        {
            public float Depth { get; set; }
            public IEnumerable<Graph> Result { get; set; }
        }
    }
}
