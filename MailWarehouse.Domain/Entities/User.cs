using Microsoft.AspNetCore.Identity;

namespace MailWarehouse.Domain.Entities;

public class User : IdentityUser
{
    public new int Id { get; set; }
    public string FirstName { get; set; }
    public new string LastName { get; set; }
    public new string Email { get; set; }
    public new string PhoneNumber { get; set; }
    public new string Username { get; set; }
    public new string PasswordHash { get; set; }
    public List<string> Roles { get; set; }
    public ICollection<Package> SentPackages { get; set; }
    public ICollection<Package> ReceivedPackages { get; set; }
}