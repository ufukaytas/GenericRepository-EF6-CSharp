using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationName.EF.Context
{
    public class EntitiesContext : DbContext
    {
        static EntitiesContext()
        {
            Database.SetInitializer<EntitiesContext>(null);
        }

        public EntitiesContext()
            : base("Name=EntitiesContextConnection")
        {
        }

        // Add tables here!
    }
}
