using MailWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailWarehouse.Infrastructure;

public class PostalDeliveryDbContext : DbContext
{
    public PostalDeliveryDbContext(DbContextOptions<PostalDeliveryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Package> Packages { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User1");
            entity.Property(u => u.FirstName)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(u => u.LastName)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(u => u.Username).HasColumnName("Username");
            entity.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            entity.Property(u => u.Roles).HasColumnName("Roles");
        });

        modelBuilder.Entity<Package>()
            .Property(p => p.Weight)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Package>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");
    }
}