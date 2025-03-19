using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.Application.Models;

public class UserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ім'я обов'язкове для заповнення.")]
    [StringLength(50, ErrorMessage = "Ім'я не може бути довшим за 50 символів.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Прізвище обов'язкове для заповнення.")]
    [StringLength(50, ErrorMessage = "Прізвище не може бути довшим за 50 символів.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email обов'язковий для заповнення.")]
    [EmailAddress(ErrorMessage = "Некоректний формат email.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Номер телефону обов'язковий для заповнення.")]
    [Phone(ErrorMessage = "Некоректний формат номера телефону.")]
    public string PhoneNumber { get; set; }
}