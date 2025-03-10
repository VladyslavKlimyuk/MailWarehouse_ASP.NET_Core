namespace MailWarehouse.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Package> SentPackages { get; set; }
    public ICollection<Package> ReceivedPackages { get; set; }
}
