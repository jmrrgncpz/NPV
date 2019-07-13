using NPV.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.DAL
{
    class NPVDatabaseInitializer : MigrateDatabaseToLatestVersion<NPVContext, NPV.Migrations.Configuration>
    {
        public NPVDatabaseInitializer() : base(Constants.DBContextName)
        {

        }
    }
}