using Microsoft.Practices.Unity;
using Nickel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vinesense.Model;

namespace Nickel.Controllers
{
    public class WeathersController : ApiController
    {
        IWeathersRepository WeathersRepository { get; set; }
        public WeathersController(IWeathersRepository weathersRepository)
        {
            WeathersRepository = weathersRepository;
        }

        /// <param name="begin">시작 날짜 및 시각(포함)</param>
        /// <param name="end">종료 날짜 및 시간(제외)</param>
        [HttpGet]
        [ActionName("GetRange")]
        public WeathersControllerResponse GetRange(DateTime? begin = null, DateTime? end = null)
        {
            begin = begin ?? DateTime.MinValue;
            end = end ?? DateTime.MaxValue;

            var all = from w in WeathersRepository.GetRange(begin.Value, end.Value)
                      orderby w.Timestamp ascending
                      select new WeatherResult
                      {
                          Timestamp = w.Timestamp,
                          Temperature = w.Temperature
                      };

            var daily = from w in WeathersRepository.GetRangeDaily(begin.Value, end.Value)
                        orderby w.Timestamp ascending
                        select new WeatherResult
                        {
                            Timestamp = w.Timestamp,
                            Temperature = w.Temperature
                        };
            
            return new WeathersControllerResponse
            {
                All = all.ToList(),
                Daily = daily.ToList()
            };
        }

        public class WeathersControllerResponse
        {
            /// <summary>
            /// 전체 데이터
            /// </summary>
            public IEnumerable<WeatherResult> All { get; set; }

            /// <summary>
            /// 일별 데이터
            /// </summary>
            public IEnumerable<WeatherResult> Daily { get; set; }
        }
    }
}