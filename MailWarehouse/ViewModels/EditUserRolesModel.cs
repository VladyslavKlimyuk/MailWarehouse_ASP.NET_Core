namespace MailWarehouse.ViewModels;

public class EditUserRolesViewModel
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public List<UserRoleViewModel> Roles { get; set; }
}