using Microsoft.EntityFrameworkCore;
using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.Configurations
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

