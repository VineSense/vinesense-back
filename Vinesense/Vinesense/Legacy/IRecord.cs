using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Legacy
{
    public interface IRecord
    {
        int Id { get; set; }

        string DateTime { get; set; }
        DateTime? Date { get; set; }
        TimeSpan? Time { get; set; }
    }
}
