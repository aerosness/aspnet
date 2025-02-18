using Microsoft.EntityFrameworkCore;
using FirstApp.Model;

namespace FirstApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<Fruit> Fruits { get; set; }
    }
}
