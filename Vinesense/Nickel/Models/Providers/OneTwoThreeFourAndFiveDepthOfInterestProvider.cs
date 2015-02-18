using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Models
{
    class OneTwoThreeFourAndFiveDepthOfInterestProvider : IDepthOfInterestProvider
    {
        public IEnumerable<float> Get()
        {
            return new[] { 1f, 2f, 3f, 4f, 5f };
        }
    }
}