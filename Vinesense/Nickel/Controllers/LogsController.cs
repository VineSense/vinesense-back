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

        [HttpGet]
        [ActionName("GetRangeBySiteId")]
        public GetBySiteResponse GetRangeBySiteId(int siteId, string sensorType, DateTime? begin = null, DateTime? end = null, int interval = 0)
        {
            begin = begin ?? DateTime.MinValue;
            end = end ?? DateTime.MaxValue;

            Site site = SitesRepository.GetById(siteId);
            if (site == null)
            {
                throw new ArgumentException();
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
                Result = GraphDataService.GetRangeBySiteId(begin.Value, end.Value, interval, sensorType, siteId)
            };
        }

        [HttpGet]
        [ActionName("GetRangeByDepth")]
        public GetByDepthResponse GetRangeByDepth(float depth, string sensorType, DateTime? begin = null, DateTime? end = null, int interval = 0)
        {
            begin = begin ?? DateTime.MinValue;
            end = end ?? DateTime.MaxValue;

            return new GetByDepthResponse
            {
                Depth = depth,
                Result = GraphDataService.GetRangeByDepth(begin.Value, end.Value, interval, sensorType, depth)
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
