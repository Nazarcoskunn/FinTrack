using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock>()
                .HasMany(s => s.Prices)
                .WithOne(p => p.Stock)
                .HasForeignKey(p => p.StockId);

            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.Symbol)
                .IsUnique();

            modelBuilder.Entity<Stock>().HasData(
   new Stock { Id = 1, Symbol = "AAPL", Name = "Apple", CreatedAt= new DateTime(2026,1,1) },
   new Stock { Id = 2, Symbol = "MSFT", Name = "Microsoft" , CreatedAt= new DateTime(2026,1,1) },
   new Stock { Id = 3, Symbol = "TSLA", Name = "Tesla", CreatedAt= new DateTime(2026,1,1) }
);
        }
    }
}