using Microsoft.EntityFrameworkCore;

namespace Stock.API.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<OrderInbox> OrderInbox { get; set; }
    }
}
