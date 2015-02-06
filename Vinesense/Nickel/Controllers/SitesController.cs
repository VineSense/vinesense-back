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

namespace Nickel.Controllers
{
    public class SitesController : ApiController
    {
        private Expression<Func<ISensorRecord, bool>> BuildFilter(DateTime begin, DateTime end)
        {
            return (r) =>
                (begin.Date < r.Date.Value && r.Date.Value < end.Date) ||
                (begin.Date != end.Date && begin.Date == r.Date.Value && begin.TimeOfDay <= r.Time) ||
                (begin.Date != end.Date && r.Date.Value == end.Date && r.Time < end.TimeOfDay) ||
                (begin.Date == end.Date && begin.TimeOfDay <= r.Time && r.Time < end.TimeOfDay);
        }

        public SitesController()
        {
            Sites = new Dictionary<int, Site>();
            Sites[1] = new Site
            {
                Name = "site1",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site1.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
            Sites[2] = new Site
            {
                Name = "site2",
                Depths = new List<int> { 1, 2, 3, 5, 7 },
                Query = (context, begin, end) => from r in context.Site1.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
            Sites[3] = new Site
            {
                Name = "site3",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site1.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
            Sites[4] = new Site
            {
                Name = "site4",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site1.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
            Sites[5] = new Site
            {
                Name = "site5",
                Depths = new List<int> { 1, 2, 3, 4, 5 },
                Query = (context, begin, end) => from r in context.Site1.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
            Sites[6] = new Site
            {
                Name = "Biale Ranch",
                Depths = new List<int> { 1, 3, 5, 7, 9 },
                Query = (context, begin, end) => from r in context.Site1.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
            Sites[7] = new Site
            {
                Name = "Aldo vineyard",
                Depths = new List<int> { 2, 5, 7, 9, 0 },
                Query = (context, begin, end) => from r in context.AldoStation.Where(BuildFilter(begin, end))
                                                 orderby r.Id ascending
                                                 select r
            };
        }
        
        /// <summary>
        /// siteId번 장소에 있는 센서의 날짜 및 시각 [begin, end) 구간에 해당하는 측정 정보를 가져옵니다.
        /// </summary>
        /// <param name="siteId">장소 번호입니다.</param>
        /// <param name="begin">시작 날짜 및 시각(포함)입니다.</param>
        /// <param name="end">종료 날짜 및 시각(제외)입니다.</param>
        /// <returns>주어진 조건을 만족하면서 정보가 등록된 순서에 따라서 정렬된 배열입니다.</returns>
        public SitesResult Get(int siteId, DateTime begin, DateTime end)
        {
            if (Sites.ContainsKey(siteId) == false)
            {
                return new SitesResult();
            };

            SitesResult result = new SitesResult { Site = Sites[siteId] };
            using (var context = new VinesenseModel())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                result.Records = result.Site.Query(context, begin, end).ToArray();
                return result;
            }
        }

        /// <summary>
        /// 장소 정보를 나타냅니다.
        /// </summary>
        public class Site
        {
            /// <summary>
            /// 사람이 알아볼 수 있도록 하는 장소 이름입니다.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 온습도 데이터와 같은 순서로 나열한 센서의 깊이입니다.
            /// </summary>
            public List<int> Depths { get; set; }

            [JsonIgnore]
            [XmlIgnore]
            public Func<VinesenseModel, DateTime, DateTime, IEnumerable<ISensorRecord>> Query { get; set; }
        }

        private Dictionary<int, Site> Sites { get; set; }

        /// <summary>
        /// 장소 정보와 장소와 관련된 측정 데이터를 함께 나타냅니다.
        /// </summary>
        public class SitesResult
        {
            public Site Site { get; set; }

            public IEnumerable<ISensorRecord> Records { get; set; }
        }
    }
}
