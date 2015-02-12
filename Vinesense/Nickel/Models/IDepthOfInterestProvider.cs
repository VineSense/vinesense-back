using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Models
{
    interface IDepthOfInterestProvider
    {
        IEnumerable<float> Get();
    }
}
