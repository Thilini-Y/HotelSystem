using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Data
{
    public class ContactStoreContext : DbContext
    {
        public ContactStoreContext(DbContextOptions<ContactStoreContext> options)
            : base(options)
        {
                
        }

        public DbSet<Contacts> Contacts { get; set; }
    }
}
