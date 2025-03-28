namespace MailWarehouse.Application.Models;

public class PackageDto
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; }
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public string SenderAddress { get; set; }
    public string RecipientAddress { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}