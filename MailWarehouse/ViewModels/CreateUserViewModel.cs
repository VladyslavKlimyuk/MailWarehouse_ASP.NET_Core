using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Ім'я є обов'язковим.")]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Прізвище є обов'язковим.")]
    [Display(Name = "Прізвище")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Логін є обов'язковим.")]
    [Display(Name = "Логін")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
    [EmailAddress]
    [Display(Name = "Електронна пошта")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль є обов'язковим.")]
    [StringLength(100, ErrorMessage = "Пароль має містити від {2} до {1} символів.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}