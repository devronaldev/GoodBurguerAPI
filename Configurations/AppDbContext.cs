using Microsoft.EntityFrameworkCore;
using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.Configurations
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        DbSet<Item> Items { get; set; }
        DbSet<Order> Orders { get; set; }
    }
}

