namespace MailWarehouse.Domain.Entities.ValueObjects;

public class Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }

    public Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Address)obj;
        return Street == other.Street &&
               City == other.City &&
               State == other.State &&
               ZipCode == other.ZipCode &&
               Country == other.Country;
    }

    public override int GetHashCode()
    {
        return Street.GetHashCode() ^
               City.GetHashCode() ^
               State.GetHashCode() ^
               ZipCode.GetHashCode() ^
               Country.GetHashCode();
    }
}
