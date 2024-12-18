using Microsoft.EntityFrameworkCore;

namespace wypozyczalnia.Server.Models;

public class RentalsContext : DbContext
{
    public RentalsContext(DbContextOptions<RentalsContext> options) : base(options)
    {
        
    }

    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
}