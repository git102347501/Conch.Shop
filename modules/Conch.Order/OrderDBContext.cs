using Microsoft.EntityFrameworkCore;

namespace Conch.Order
{
    public class OrderDBContext : DbContext
    {
        public DbSet<OrderMaster> OrderMaster { get; set; }

        public DbSet<OrderGoods> OrderGoods { get; set; }

        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderGoods>()
                .HasKey(c => new { c.OrderMasterId, c.GoodsId });
        }
    }
}
