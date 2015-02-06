using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Nickel.Models
{
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
        public Func<VinesenseModel, DateTime, DateTime, IEnumerable<ISiteRecord>> Query { get; set; }
    }
}