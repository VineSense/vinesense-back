using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    interface IWeatherService
    {
        DateTime GetLastTimestamp();
        void Update(Weather weather);
    }
}
