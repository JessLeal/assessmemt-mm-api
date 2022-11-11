using Microsoft.EntityFrameworkCore;
using MoneyMeAPI.Model;
using System.Reflection.Emit;

namespace MoneyMeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AccountModel> Accounts => Set<AccountModel>();
        public DbSet<LoanModel> Loans => Set<LoanModel>();
        public DbSet<RepaymentsModel> Repayments => Set<RepaymentsModel>();
        public DbSet<BlacklistModel> Blacklists => Set<BlacklistModel>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AccountModel>()
                .HasIndex(a => new { a.FirstName, a.LastName, a.DateOfBirth })
                .IsUnique();
            builder.Entity<LoanModel>()
                .HasIndex(a => a.AccountId).IsUnique();
            builder.Entity<RepaymentsModel>()
                .HasIndex(a => a.AccountId).IsUnique();
        }
    }
}
