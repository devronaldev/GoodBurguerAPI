using GoodBurguerAPI.Configurations.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;
using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.Configurations
{
    /// <summary>
    /// Represents the application's database context for accessing and managing entities.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the collection of <see cref="Item"/> entities in the database.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Order"/> entities in the database.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
    }
}