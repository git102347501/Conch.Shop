using Microsoft.EntityFrameworkCore;

namespace Conch.Goods
{
    public class GoodsDBContext : DbContext
    {
        public DbSet<GoodsMaster> GoodsMaster { get; set; }

        public GoodsDBContext(DbContextOptions<GoodsDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
