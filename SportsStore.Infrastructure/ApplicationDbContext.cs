using Microsoft.EntityFrameworkCore;
using SportsStore.Domain;

namespace SportsStore.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ModelProduct> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelProduct>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
