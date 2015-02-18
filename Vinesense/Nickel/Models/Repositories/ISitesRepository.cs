using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Nickel.Models
{
    public interface ISitesRepository : IGenericRepository<Site>
    {
    }

    public class SiteValue
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
