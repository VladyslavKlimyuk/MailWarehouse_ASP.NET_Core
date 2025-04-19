using System.ComponentModel.DataAnnotations;
using MailWarehouse.Resources;

namespace MailWarehouse.ViewModels;

public class PackageViewModel
{
    public PackageViewModel() { }
    public int Id { get; }

    [Display(Name = "TrackingNumber", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public string TrackingNumber { get; }

    [Display(Name = "SenderId", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public int SenderId { get; }

    [Display(Name = "RecipientId", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public int RecipientId { get; }

    [Display(Name = "SenderAddress", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public string SenderAddress { get; }

    [Display(Name = "RecipientAddress", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public string RecipientAddress { get; }

    [Display(Name = "Weight", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    [Range(0.01, double.MaxValue, ErrorMessageResourceName = "RangeError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public decimal Weight { get; }

    [Display(Name = "Price", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    [Range(0, double.MaxValue, ErrorMessageResourceName = "RangeError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public decimal Price { get; }

    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public MailWarehouse.Domain.Enums.PackageStatus Status { get; }

    [Display(Name = "CreatedAt", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public System.DateTime CreatedAt { get; }

    [Display(Name = "DeliveredAt", ResourceType = typeof(PackageViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(PackageViewModelResource))]
    public System.DateTime? DeliveredAt { get; }

    public PackageViewModel(int id, string trackingNumber, int senderId, int recipientId,
                            string senderAddress, string recipientAddress, decimal weight,
                            decimal price, MailWarehouse.Domain.Enums.PackageStatus status,
                            System.DateTime createdAt, System.DateTime? deliveredAt)
    {
        Id = id;
        TrackingNumber = trackingNumber;
        SenderId = senderId;
        RecipientId = recipientId;
        SenderAddress = senderAddress;
        RecipientAddress = recipientAddress;
        Weight = weight;
        Price = price;
        Status = status;
        CreatedAt = createdAt;
        DeliveredAt = deliveredAt;
    }
}