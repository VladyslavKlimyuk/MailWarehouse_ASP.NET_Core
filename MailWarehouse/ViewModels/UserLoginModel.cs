using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels;

public class UserLoginModel
{
    [Required(ErrorMessage = "Будь ласка, введіть ім'я користувача")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Будь ласка, введіть пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
