using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomInfoAPI.Data
{
    public class RoomStoreContext : DbContext
    {
        public RoomStoreContext(DbContextOptions<RoomStoreContext> options)
            :base(options)
        {
            
        }

        public DbSet<Room> Room { get; set; }
    }
}
