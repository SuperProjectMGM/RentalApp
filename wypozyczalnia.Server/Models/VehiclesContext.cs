﻿using Microsoft.EntityFrameworkCore;

namespace wypozyczalnia.Server.Models
{
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicles> Vehicles { get; set; }
    }
}
