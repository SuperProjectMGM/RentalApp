using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace wypozyczalnia.Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tutaj możesz dodać dodatkowe DbSety, jeśli będziesz potrzebować
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
