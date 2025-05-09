using Microsoft.Extensions.Configuration;

namespace MailWarehouse.Application.Services;

public class AuthService
{
    private readonly string _username;
    private readonly string _password;

    public AuthService(IConfiguration configuration)
    {
        _username = configuration.GetSection("Authentication")["Username"];
        _password = configuration.GetSection("Authentication")["Password"];
    }

    public bool Authenticate(string username, string password)
    {
        return username == _username && password == _password;
    }
}
