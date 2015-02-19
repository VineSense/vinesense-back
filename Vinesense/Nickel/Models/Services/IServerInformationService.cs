using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickel.Models
{
    public interface IServerInformationService
    {
        string GetHost();
        string GetMinDate();
        int GetCurrentYear();
        int GetSiteNumber();
        int GetDepthNumber();
    }
}
