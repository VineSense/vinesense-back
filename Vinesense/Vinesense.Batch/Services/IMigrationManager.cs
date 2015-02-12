using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinesense.Batch.Services
{
    interface IMigrationManager
    {
        void MigrateAll();
    }
}
