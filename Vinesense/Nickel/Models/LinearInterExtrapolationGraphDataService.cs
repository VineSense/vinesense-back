using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Vinesense.Model;

namespace Nickel.Models
{
    class LinearInterExtrapolationGraphDataService : IGraphDataService
    {
        DbSet<Log> Logs { get; set; }
        ILogsRepository LogsRepository { get; set; }
        ISensorsRepository SensorsRepository { get; set; }
        ISitesRepository SitesRepository { get; set; }
        IDepthOfInterestProvider DepthOfInterestProvider { get; set; }

        public LinearInterExtrapolationGraphDataService(IUnitOfWork unitOfWork, ILogsRepository logsRepository, ISitesRepository sitesRepository, ISensorsRepository sensorsRepository, IDepthOfInterestProvider depthOfInterestProvider)
        {
            Logs = unitOfWork.Context.Set<Log>();
            LogsRepository = logsRepository;
            SitesRepository = sitesRepository;
            SensorsRepository = sensorsRepository;
            DepthOfInterestProvider = depthOfInterestProvider;
        }

        private Tuple<float, float> GetDepthIndexPair(IEnumerable<float> depths, float target)
        {
            List<float> list = depths.ToList();
            list.Sort();

            int searchResult = list.BinarySearch(target);
            if (searchResult >= 0)
            {
                return null;
            }

            if (list.Count < 2)
            {
                throw new ArgumentException();
            }

            searchResult = ~searchResult;
            if (searchResult == 0)
            {
                return Tuple.Create(list[0], list[1]);
            }
            if (searchResult == list.Count)
            {
                return Tuple.Create(list[list.Count - 2], list[list.Count - 1]);
            }
            return Tuple.Create(list[searchResult - 1], list[searchResult]);
        }

        public IEnumerable<Graph> GetRangeByDepth(DateTime begin, DateTime end, int interval, string sensorType, float depth)
        {
            return from site in SitesRepository.Get().ToList()
                   let result = Get(site.Id, depth, sensorType)
                   select new Graph
                   {
                       SiteId = site.Id,
                       Depth = depth,
                       IsInterExtrapolated = result.Item1,
                       Data = result.Item2.FilterRange(begin, end).GroupBy(interval)
                   };
        }

        public IEnumerable<Graph> GetRangeBySiteId(DateTime begin, DateTime end, int interval, string sensorType, int siteId)
        {
            return from depth in DepthOfInterestProvider.Get()
                   let result = Get(siteId, depth, sensorType)
                   select new Graph
                   {
                       SiteId = siteId,
                       Depth = depth,
                       IsInterExtrapolated = result.Item1,
                       Data = result.Item2.FilterRange(begin, end).GroupBy(interval)
                   };
        }

        private IQueryable<GraphData> GetInterExtrapolated(int siteId, float depth, float depthA, float depthB, int sensorIdA, int sensorIdB)
        {
            var q = from log0 in Logs
                    where log0.SensorId == sensorIdA
                    from log1 in Logs
                    where log1.SensorId == sensorIdB
                    where log0.Timestamp == log1.Timestamp
                    select new GraphData
                    {
                        Timestamp = log0.Timestamp,
                        Value = log0.Value + (log1.Value - log0.Value) * (depth - depthA) / (depthB - depthA)
                    };

            return q;
        }

        private IQueryable<GraphData> GetAsIs(int siteId, float depth, int sensorId)
        {
            var q = from log in Logs
                    where log.SensorId == sensorId
                    select new GraphData
                    {
                        Timestamp = log.Timestamp,
                        Value = log.Value
                    };

            return q;
        }

        private Tuple<bool, IQueryable<GraphData>> Get(int siteId, float depth, string sensorType)
        {
            var sensors = from s in SensorsRepository.GetType(sensorType)
                                     where s.SiteId == siteId
                                     select s;

            var availableDepths = from s in sensors
                                  select s.Depth;
            availableDepths = availableDepths.Distinct();

            Func<float, int> getSensorId = (d) => sensors.Where((s) => s.Depth == d).First().Id;

            var depthPair = GetDepthIndexPair(availableDepths, depth);
            if (depthPair == null)
            {
                return Tuple.Create(false, GetAsIs(siteId, depth, getSensorId(depth)));
            }
            else
            {
                float depthA = depthPair.Item1;
                float depthB = depthPair.Item2;

                return Tuple.Create(true, GetInterExtrapolated(siteId, depth, depthA, depthB, getSensorId(depthA), getSensorId(depthB)));
            }
        }
    }
}