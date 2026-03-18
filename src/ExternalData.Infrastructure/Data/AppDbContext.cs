using ExternalData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExternalData.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ExchangeRateRecord> ExchangeRateRecords => Set<ExchangeRateRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeRateRecord>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(x => x.CodeIn)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Bid)
                .HasPrecision(18, 6);

            entity.Property(x => x.Ask)
                .HasPrecision(18, 6);

            entity.Property(x => x.CreatedAt)
                .IsRequired();
        });
    }
}