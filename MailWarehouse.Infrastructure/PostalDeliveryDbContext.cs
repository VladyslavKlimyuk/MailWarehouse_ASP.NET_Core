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
            .HasForeignKey(p => p.SenderId);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Recipient)
            .WithMany(u => u.ReceivedPackages)
            .HasForeignKey(p => p.RecipientId);
    }
}
