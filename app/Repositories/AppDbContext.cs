using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<HeroInstance> HeroInstances { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
    }
}
