using Microsoft.EntityFrameworkCore;

namespace Conch.Account
{
    public class AccountDbContext : DbContext
    {
        public DbSet<AccountMaster> AccountMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
