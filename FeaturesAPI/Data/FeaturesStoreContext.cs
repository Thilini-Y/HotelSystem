using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesAPI.Data
{
    public class FeaturesStoreContext : DbContext
    {
        public FeaturesStoreContext(DbContextOptions<FeaturesStoreContext> options)
            :base(options)
        {
           
        }

        public DbSet<Features> Features { get; set; }
    }
}
