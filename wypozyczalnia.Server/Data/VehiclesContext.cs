using Microsoft.EntityFrameworkCore;

namespace wypozyczalnia.Server.Models
{
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions<VehiclesContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
