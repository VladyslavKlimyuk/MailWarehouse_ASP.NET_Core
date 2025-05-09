using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MailWarehouse.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Package> SentPackages { get; set; }
    public ICollection<Package> ReceivedPackages { get; set; }

    //public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    //public virtual ICollection<IdentityRole> Roles { get; set; }

    public User()
    {
        SentPackages = new List<Package>();
        ReceivedPackages = new List<Package>();
        //UserRoles = new List<IdentityUserRole<string>>();
        //Roles = new List<IdentityRole>();
    }
}