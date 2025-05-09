using System.Collections.Generic;
using System.Threading.Tasks;
using MailWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MailWarehouse.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllIdentityUsersAsync();
    Task<User> GetByIdAsync(string id);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task AddIdentityUserAsync(User applicationUser, string password);
    Task UpdateIdentityUserAsync(User applicationUser);
    Task DeleteIdentityUserAsync(User applicationUser);
    Task<User> GetByUsernameAndPasswordAsync(string username, string password);
}