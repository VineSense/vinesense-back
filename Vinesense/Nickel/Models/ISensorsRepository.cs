using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinesense.Model;

namespace Nickel.Models
{
    public interface ISensorsRepository : IGenericRepository<Sensor>
    {
        IQueryable<Sensor> GetType(string sensorType);
    }

    public class SensorValue : Sensor
    {
    }
}
