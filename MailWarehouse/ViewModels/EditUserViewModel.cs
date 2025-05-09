using System.ComponentModel.DataAnnotations;

namespace MailWarehouse.ViewModels
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "Електронна адреса є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної адреси.")]
        [Display(Name = "Електронна адреса")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ім'я є обов'язковим.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Прізвище є обов'язковим.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Невірний формат номеру телефону.")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }
    }
}