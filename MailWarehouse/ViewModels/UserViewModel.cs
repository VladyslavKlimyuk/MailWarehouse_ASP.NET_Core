using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels;

public class UserViewModel
{
    public int Id { get; }

    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Ім'я обов'язкове для заповнення.")]
    [StringLength(50, ErrorMessage = "Ім'я не може бути довшим за 50 символів.")]
    public string FirstName { get; }

    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Прізвище обов'язкове для заповнення.")]
    [StringLength(50, ErrorMessage = "Прізвище не може бути довшим за 50 символів.")]
    public string LastName { get; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email обов'язковий для заповнення.")]
    [EmailAddress(ErrorMessage = "Некоректний формат email.")]
    public string Email { get; }

    [Display(Name = "Номер телефону")]
    [Required(ErrorMessage = "Номер телефону обов'язковий для заповнення.")]
    [Phone(ErrorMessage = "Некоректний формат номера телефону.")]
    public string PhoneNumber { get; }

    public UserViewModel(int id, string firstName, string lastName, string email, string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
