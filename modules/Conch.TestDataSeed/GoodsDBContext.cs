using Microsoft.EntityFrameworkCore;

namespace Conch.Goods
{
    public class GoodsDBContext : DbContext
    {
        public DbSet<GoodsMaster> GoodsMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
