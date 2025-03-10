using MailWarehouse.Domain.Enums;

namespace MailWarehouse.Domain.Entities;

public class Package
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int RecipientId { get; set; }
    public User Recipient {  get; set; }
    public string SenderAddress { get; set; }
    public string RecipientAddress { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public PackageStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}
