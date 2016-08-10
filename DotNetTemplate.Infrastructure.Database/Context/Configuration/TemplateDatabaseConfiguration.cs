using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTemplate.Infrastructure.Database.Context.Configuration
{
    internal class TemplateDatabaseConfiguration : DbConfiguration
    {
        public TemplateDatabaseConfiguration()
        {
            SetProviderServices("System.Data.SqlClient", 
                System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        }
    }
}
