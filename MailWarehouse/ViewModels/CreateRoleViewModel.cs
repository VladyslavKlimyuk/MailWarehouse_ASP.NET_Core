using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels;

public class CreateRoleViewModel
{
    [Required(ErrorMessage = "Назва ролі є обов'язковою.")]
    public string RoleName { get; set; }
}