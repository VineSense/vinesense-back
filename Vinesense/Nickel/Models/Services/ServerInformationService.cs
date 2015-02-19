using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nickel.Models
{
    class ServerInformationService : IServerInformationService
    {
        public string GetHost()
        {
            return "http://vines.cloudapp.net/Nickel";
        }

        public string GetMinDate()
        {
            return "2014-05-24";
        }

        public int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }

        public int GetSiteNumber()
        {
            return 7;
        }

        public int GetDepthNumber()
        {
            return 5;
        }
    }
}