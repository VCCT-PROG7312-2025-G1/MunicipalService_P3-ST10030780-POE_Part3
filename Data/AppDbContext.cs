using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MunicipalService_P3.Models;

namespace MunicipalService_P3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DateTime today = DateTime.Today;

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Community Cleanup",
                    Description = "Come help us keep our town clean!",
                    Category = "Environment",
                    EventDate = today.AddDays(10),
                    Location = "Vyeboom"
                },
                new Event
                {
                    Id = 2,
                    Title = "Church Bazaar",
                    Description = "Join us for a lovely Church Bazaar to support the community.",
                    Category = "Market",
                    EventDate = today.AddDays(5),
                    Location = "Vyeboom Church"
                },
                new Event
                {
                    Id = 3,
                    Title = "Park Run",
                    Description = "Support our first ever park run!",
                    Category = "Sport",
                    EventDate = today.AddDays(15),
                    Location = "Vyeboom Park"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "Admin@123", // For demo only; use hashing in production
                    Role = "Admin"
                }
            );
        }
    }
}