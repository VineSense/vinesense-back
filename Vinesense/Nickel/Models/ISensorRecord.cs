using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Models
{
    /// <summary>
    /// 특정 시각, 특정 장소에서의 측정 데이터를 나타냅니다. 추가적으로, 테이블의 원본 열과 데이터도 포함합니다.
    /// </summary>
    public interface ISensorRecord
    {
        /// <summary>
        /// 측정 번호입니다.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 날짜를 나타냅니다. 시간은 의미를 가지지 않습니다.
        /// </summary>
        DateTime? Date { get; set; }

        /// <summary>
        /// 하루 중 시각을 나타냅니다.
        /// </summary>
        TimeSpan? Time { get; set; }

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
