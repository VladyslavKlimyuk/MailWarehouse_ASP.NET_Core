using System.Collections.Generic;
using MailWarehouse.ViewModels;

namespace MailWarehouse.ViewModels;

public class EditUserRolesViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<string> UserRoles { get; set; }
    public List<RoleViewModel> AllRoles { get; set; }
}