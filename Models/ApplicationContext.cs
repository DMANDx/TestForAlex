using Microsoft.EntityFrameworkCore;
using System;

namespace Avto1Test.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Url> Urls { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
           // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>()
                .HasIndex(p => new { p.MainURL, p.TinyURL})
                .IsUnique(true);
        }
    }
}
