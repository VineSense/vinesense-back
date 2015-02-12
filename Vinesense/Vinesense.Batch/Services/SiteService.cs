using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    class SiteService : ISiteService
    {
        public int ResolveSite(int number)
        {
            using (var context = new NewContext())
            {
                var q = from s in context.Sites
                        where s.Number == number
                        select s;

                Site site = q.FirstOrDefault();
                if (site != null)
                {
                    return site.Id;
                }
                site = context.Sites.Add(new Site { Number = number });
                context.SaveChanges();
                return site.Id;
            }
        }

        public void UpdateSite(int number, string name, double latitude, double longitude)
        {
            long siteId = ResolveSite(number);
            using (var context = new NewContext())
            {
                var q = from s in context.Sites
                        where s.Id == siteId
                        select s;
                Site site = q.First();
                site.Name = name;
                site.Latitude = latitude;
                site.Longitude = longitude;
                context.SaveChanges();
            }
        }

        public DateTime GetLastTimestamp(int number)
        {
            long siteId = ResolveSite(number);
            using (var context = new NewContext())
            {
                var q = from log in context.Logs
                        where log.Sensor.Site.Id == siteId
                        orderby log.Timestamp descending
                        select log.Timestamp;

                return q.FirstOrDefault();
            }
        }
    }
}
