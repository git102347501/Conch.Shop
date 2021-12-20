using Microsoft.EntityFrameworkCore;

namespace Conch.Stock
{
    public class StockDBContext : DbContext
    {
        public DbSet<StockMaster> StockMaster { get; set; }

        public DbSet<StockGoods> StockGoods { get; set; }

        public StockDBContext()
        {

        }

        public StockDBContext(DbContextOptions<StockDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockGoods>()
                .HasKey(c => new { c.StockId, c.GoodsId });
        }
    }
}
