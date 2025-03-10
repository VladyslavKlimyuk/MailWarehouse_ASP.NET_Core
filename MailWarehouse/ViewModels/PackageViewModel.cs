namespace MailWarehouse.ViewModels;

public class PackageViewModel
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; }
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public string SenderAddress { get; set; }
    public string RecipientAddress { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public MailWarehouse.Domain.Enums.PackageStatus Status { get; set; }
    public System.DateTime CreatedAt { get; set; }
    public System.DateTime? DeliveredAt { get; set; }
}
