using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeTest.Models
{
    public partial class DatabaseBContext : DbContext
    {
        public DatabaseBContext()
        {
        }

        public DatabaseBContext(DbContextOptions<DatabaseBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountB> AccountB { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DatabaseB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountB>(entity =>
            {
                entity.Property(e => e.AccountNo).HasMaxLength(10);

                entity.Property(e => e.WithdrawalDate).HasMaxLength(20);
            });
        }
    }
}
