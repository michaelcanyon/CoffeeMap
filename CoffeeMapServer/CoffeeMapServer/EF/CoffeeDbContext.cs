using Microsoft.EntityFrameworkCore;

namespace CoffeeMapServer.EF
{
    public partial class CoffeeDbContext : DbContext
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}