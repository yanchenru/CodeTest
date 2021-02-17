using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeTest.Models
{
    public partial class DatabaseAContext : DbContext
    {
        public DatabaseAContext()
        {
        }

        public DatabaseAContext(DbContextOptions<DatabaseAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountA> AccountA { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DatabaseA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountA>(entity =>
            {
                entity.Property(e => e.AccountNo).HasMaxLength(10);

                entity.Property(e => e.WithdrawalDate).HasMaxLength(20);
            });
        }
    }
}
