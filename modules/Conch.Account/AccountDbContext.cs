using Microsoft.EntityFrameworkCore;

namespace Conch.Account
{
    public class AccountDbContext : DbContext
    {
        public DbSet<AccountMaster> AccountMaster { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
