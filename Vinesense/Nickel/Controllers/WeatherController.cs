using Microsoft.Practices.Unity;
using Nickel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Nickel.Controllers
{
    public class WeatherController : ApiController
    {
        IRecordFilter RecordFilter { get; set; }

        public WeatherController(IRecordFilter recordFilter)
        {
            RecordFilter = recordFilter;
        }

        /// <summary>
        /// 날짜 및 시각 [begin, end) 구간에 해당하는 기상 정보를 가져옵니다.
        /// </summary>
        /// <param name="begin">시작 날짜 및 시각(포함)입니다.</param>
        /// <param name="end">종료 날짜 및 시각(제외)입니다.</param>
        /// <returns>주어진 조건을 만족하면서 정보가 등록된 순서에 따라서 정렬된 배열입니다.</returns>
        public IEnumerable<IRecord> Get(DateTime begin, DateTime end)
        {
            using (var context = new VinesenseModel())
            {
                var q = from r in context.WeatherStation.Where(RecordFilter.BuildDateTimeRangeFilter(begin, end))
                        orderby r.Id ascending
                        select (WeatherStation)r;

                return q.ToList();
            }
        }
    }
}