using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInfoAPI.Data
{
    public class PropertyStoreContext : DbContext
    {
        public PropertyStoreContext(DbContextOptions<PropertyStoreContext> options)
            : base(options)
        {

        }

        public DbSet<Property> Property { get; set; }
    }
}
