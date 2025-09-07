using call_center_service.Entities;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Interaction> Interactions => Set<Interaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Brand.cs
        modelBuilder.Entity<Brand>()
            .Property(property => property.BrandId)
            .HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Brand>()
            .Property(property => property.CreatedAt)
            .HasDefaultValueSql("NOW()");
        modelBuilder.Entity<Brand>()
            .Property(property => property.ReceiptCount)
            .HasDefaultValue(0);
        
        //Receipt.cs
        modelBuilder.Entity<Receipt>()
            .Property(property => property.ReceiptId)
            .HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Receipt>()
            .Property(property => property.CreatedAt)
            .HasDefaultValueSql("NOW()");
        modelBuilder.Entity<Receipt>()
            .Property(property => property.RedemptionCode)
            .HasDefaultValueSql("gen_unique_six_digit()");
        modelBuilder.Entity<Receipt>()
            .Property(property => property.IsValid)
            .HasDefaultValue(true);
        
        //Session.cs
        modelBuilder.Entity<Session>()
            .Property(property => property.SessionId)
            .HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Session>()
            .Property(property => property.SessionStart)
            .HasDefaultValueSql("NOW()");
        
        //Interaction.cs
        modelBuilder.Entity<Interaction>()
            .Property(property => property.InteractionId)
            .HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Interaction>()
            .Property(property => property.InteractionTime)
            .HasDefaultValueSql("NOW()");
        
        base.OnModelCreating(modelBuilder);
    }
}