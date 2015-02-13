using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    interface ISiteService
    {
        int ResolveSite(DbContext context, int number);
        void UpdateSite(int number, string name, double latitude, double longitude);
        DateTime GetLastTimestamp(int number);
    }
}
