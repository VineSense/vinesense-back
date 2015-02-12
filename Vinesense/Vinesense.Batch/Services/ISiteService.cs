using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    interface ISiteService
    {
        int ResolveSite(int number);
        void UpdateSite(int number, string name, double latitude, double longitude);
        DateTime GetLastTimestamp(int number);
    }
}
