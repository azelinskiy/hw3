using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldUniversityRankings.Data
{
    public class Context : DbContext
    {
        public DbSet<Year> Years { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Location> Locations { get; set; }

        public Context() : base("localsql")
        {
        }
    }
}
