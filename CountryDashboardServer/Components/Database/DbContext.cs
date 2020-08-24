using CountryDashboardServer.Components.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryDashboardServer.Components.Database
{
    public partial class DbContexts : DbContext
    {
        public DbContexts()
        {
        }

        public DbContexts(DbContextOptions<DbContexts> options)
            : base(options)
        {
        }

        public virtual DbSet<Countries> Countries { get; set; }
    }
}
