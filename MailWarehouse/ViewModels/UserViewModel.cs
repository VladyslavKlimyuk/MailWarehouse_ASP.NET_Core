using System.ComponentModel.DataAnnotations;
using MailWarehouse.Resources;

namespace MailWarehouse.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }

    [Display(Name = "FirstName", ResourceType = typeof(UserViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    [StringLength(50, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    public string FirstName { get; set; }

    [Display(Name = "LastName", ResourceType = typeof(UserViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    [StringLength(50, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    public string LastName { get; set; }

    [Display(Name = "Email", ResourceType = typeof(UserViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    [EmailAddress(ErrorMessageResourceName = "EmailAddressError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    public string Email { get; set; }

    [Display(Name = "PhoneNumber", ResourceType = typeof(UserViewModelResource))]
    [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    [Phone(ErrorMessageResourceName = "PhoneError", ErrorMessageResourceType = typeof(UserViewModelResource))]
    public string PhoneNumber { get; set; }

    public UserViewModel(int id, string firstName, string lastName, string email, string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}