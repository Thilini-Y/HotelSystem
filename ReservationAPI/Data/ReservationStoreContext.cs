using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Data
{
    public class ReservationStoreContext : DbContext 
    {
        public ReservationStoreContext(DbContextOptions<ReservationStoreContext> options)
            :base(options)
        {

        }

        public DbSet<Reservations> Reservations { get; set; }
    }
}
