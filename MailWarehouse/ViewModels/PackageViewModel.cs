using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels;

public class PackageViewModel
{
    public int Id { get; }

    [Display(Name = "Трек-номер", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Трек-номер обов'язковий для заповнення.")]
    public string TrackingNumber { get; }

    [Display(Name = "Відправник", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Відправник обов'язковий для заповнення.")]
    public int SenderId { get; }

    [Display(Name = "Отримувач", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Отримувач обов'язковий для заповнення.")]
    public int RecipientId { get; }

    [Display(Name = "Адреса відправника", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Адреса відправника обов'язкова для заповнення.")]
    public string SenderAddress { get; }

    [Display(Name = "Адреса отримувача", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Адреса отримувача обов'язкова для заповнення.")]
    public string RecipientAddress { get; }

    [Display(Name = "Вага", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Вага обов'язкова для заповнення.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Вага повинна бути більшою за 0.")]
    public decimal Weight { get; }

    [Display(Name = "Ціна", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Ціна обов'язкова для заповнення.")]
    [Range(0, double.MaxValue, ErrorMessage = "Ціна не може бути від'ємною.")]
    public decimal Price { get; }

    [Required(ErrorMessage = "Статус обов'язковий для заповнення.")]
    public MailWarehouse.Domain.Enums.PackageStatus Status { get; }

    [Display(Name = "Дата відправлення", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Дата відправлення обов'язкова для заповнення.")]
    public System.DateTime CreatedAt { get; }

    [Display(Name = "Дата доставлення", ResourceType = typeof(PackageViewModel))]
    [Required(ErrorMessage = "Дата доставлення обов'язкова для заповнення.")]
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
