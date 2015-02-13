using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Batch.DataSources;
using Vinesense.Model;

namespace Vinesense.Batch.Services
{
    class SiteService : ISiteService
    {
        public int ResolveSite(DbContext context, int number)
        {
            var sites = context.Set<Site>();
            var q = from s in sites
                    where s.Number == number
                    select s;

            Site site = q.FirstOrDefault();
            if (site != null)
            {
                return site.Id;
            }
            site = sites.Add(new Site { Number = number });
            context.SaveChanges();
            return site.Id;
        }

        public void UpdateSite(int number, string name, double latitude, double longitude)
        {
            using (var context = new NewContext())
            {
                long siteId = ResolveSite(context, number);
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
            using (var context = new NewContext())
            {
                long siteId = ResolveSite(context, number);
                var q = from log in context.Logs
                        where log.Sensor.Site.Id == siteId
                        orderby log.Timestamp descending
                        select log.Timestamp;

                return q.FirstOrDefault();
            }
        }
    }
}
