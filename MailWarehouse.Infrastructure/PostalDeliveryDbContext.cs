using MailWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MailWarehouse.Infrastructure;

public class PostalDeliveryDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public PostalDeliveryDbContext(DbContextOptions<PostalDeliveryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Package> Packages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Sender)
            .WithMany(u => u.SentPackages)
            .HasForeignKey(p => p.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Recipient)
            .WithMany(u => u.ReceivedPackages)
            .HasForeignKey(p => p.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Package>()
            .HasIndex(p => p.SenderId);

        modelBuilder.Entity<Package>()
            .HasIndex(p => p.RecipientId);

        modelBuilder.Entity<Package>()
            .Property(p => p.Weight)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Package>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<User>()
            .HasMany(u => u.SentPackages)
            .WithOne(p => p.Sender)
            .HasForeignKey(p => p.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ReceivedPackages)
            .WithOne(p => p.Recipient)
            .HasForeignKey(p => p.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}