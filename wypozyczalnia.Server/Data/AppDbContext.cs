using Microsoft.EntityFrameworkCore;

namespace wypozyczalnia.Server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Rental> Rentals { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<ClientInfo> ClientInfos { get; set; }
    public DbSet<Employee> Employees { get; set; }
}