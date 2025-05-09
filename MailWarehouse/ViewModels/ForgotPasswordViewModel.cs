using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
    [EmailAddress]
    public string Email { get; set; }
}