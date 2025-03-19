using System.ComponentModel.DataAnnotations;
using MailWarehouse.Domain.Enums;

namespace MailWarehouse.Application.Models;

public class PackageDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Трек-номер обов'язковий для заповнення.")]
    public string TrackingNumber { get; set; }

    [Required(ErrorMessage = "Відправник обов'язковий для заповнення.")]
    public int SenderId { get; set; }

    [Required(ErrorMessage = "Отримувач обов'язковий для заповнення.")]
    public int RecipientId { get; set; }

    [Required(ErrorMessage = "Адреса відправника обов'язкова для заповнення.")]
    public string SenderAddress { get; set; }

    [Required(ErrorMessage = "Адреса отримувача обов'язкова для заповнення.")]
    public string RecipientAddress { get; set; }

    [Required(ErrorMessage = "Вага обов'язкова для заповнення.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Вага повинна бути більшою за 0.")]
    public decimal Weight { get; set; }

    [Required(ErrorMessage = "Ціна обов'язкова для заповнення.")]
    [Range(0, double.MaxValue, ErrorMessage = "Ціна не може бути від'ємною.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Статус обов'язковий для заповнення.")]
    public int Status { get; set; }

    [Required(ErrorMessage = "Дата створення обов'язкова для заповнення.")]
    public DateTime CreatedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }
}