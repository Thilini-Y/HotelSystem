using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceEngineAPI.Data
{
    public class PriceStoreContext :DbContext
    {
        public PriceStoreContext(DbContextOptions<PriceStoreContext> options)
            :base(options)
        {

        }

        public DbSet<Price> Price { get; set; }
    }
}
