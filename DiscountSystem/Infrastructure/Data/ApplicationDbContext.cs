using DiscountSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DiscountSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<DiscountCode> DiscountCodes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscountCode>()
                .HasIndex(dc => dc.Code)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}