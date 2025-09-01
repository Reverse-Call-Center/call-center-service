using call_center_service.Entities;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Receipt> Receipts => Set<Receipt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>()
            .Property(property => property.BrandId)
            .HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Brand>()
            .Property(property => property.CreatedAt)
            .HasDefaultValueSql("NOW()");
        modelBuilder.Entity<Brand>()
            .Property(property => property.ReceiptCount)
            .HasDefaultValue(0);
        
        modelBuilder.Entity<Receipt>()
            .Property(property => property.ReceiptId)
            .HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Receipt>()
            .Property(property => property.CreatedAt)
            .HasDefaultValueSql("NOW()");
        
        base.OnModelCreating(modelBuilder);
    }
}