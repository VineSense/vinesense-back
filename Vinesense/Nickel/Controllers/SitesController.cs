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

namespace Nickel.Controllers
{
    public class SitesController : ApiController
    {
        ISitesProvider SitesProvider { get; set; }

        public SitesController()
        {
        }

        public SitesController(ISitesProvider sitesProvider)
        {
            SitesProvider = sitesProvider;
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
            if (SitesProvider.Sites.ContainsKey(siteId) == false)
            {
                return new SitesResult();
            };

            SitesResult result = new SitesResult { Site = SitesProvider.Sites[siteId] };
            using (var context = new VinesenseModel())
            {
                result.Records = result.Site.Query(context, begin, end).ToArray();
                return result;
            }
        }

        /// <summary>
        /// 장소 정보와 장소와 관련된 측정 데이터를 함께 나타냅니다.
        /// </summary>
        public class SitesResult
        {
            public Site Site { get; set; }

            public IEnumerable<ISiteRecord> Records { get; set; }
        }
    }
}
