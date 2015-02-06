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
        /// <summary>
        /// 날짜 및 시각 [begin, end) 구간에 해당하는 기상 정보를 가져옵니다.
        /// </summary>
        /// <param name="begin">시작 날짜 및 시각(포함)입니다.</param>
        /// <param name="end">종료 날짜 및 시각(제외)입니다.</param>
        /// <returns>주어진 조건을 만족하면서 정보가 등록된 순서에 따라서 정렬된 배열입니다.</returns>
        public IEnumerable<WeatherStation> Get(DateTime begin, DateTime end)
        {
            using (var context = new VinesenseModel())
            {
                var q = from r in context.WeatherStation
                        where (begin.Date < r.Date.Value && r.Date.Value < end.Date) ||
                            (begin.Date != end.Date && begin.Date == r.Date.Value && begin.TimeOfDay <= r.Time) ||
                            (begin.Date != end.Date && r.Date.Value == end.Date && r.Time < end.TimeOfDay) ||
                            (begin.Date == end.Date && begin.TimeOfDay <= r.Time && r.Time < end.TimeOfDay)
                        orderby r.Id ascending
                        select r;

                return q.ToList();
            }
        }
    }
}