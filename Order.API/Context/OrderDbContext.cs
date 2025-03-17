using Microsoft.EntityFrameworkCore;

namespace Order.API.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Entities.Order> Order { get; set; }
        public DbSet<Entities.OrderOutbox> OrderOutbox { get; set; }
    }
}
