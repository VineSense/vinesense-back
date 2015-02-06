using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Models
{
    /// <summary>
    /// 추가적으로, 테이블의 원본 열과 데이터를 포함합니다.
    /// </summary>
    public interface ISiteRecord : IRecord
    {
        /// <summary>
        /// 이 장소에서, 여러 깊이에서 측정한 온도입니다.
        /// </summary>
        IEnumerable<float?> Temperatues { get; }

        /// <summary>
        /// 이 장소에서, 여러 깊이에서 측정한 습도입니다.
        /// </summary>
        IEnumerable<float?> Moistures { get; }
    }
}
