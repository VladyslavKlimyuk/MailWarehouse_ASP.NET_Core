using System.Collections.Generic;
using System.Threading.Tasks;
using MailWarehouse.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace MailWarehouse.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task CreateUserAsync(UserDto userDto);
    Task UpdateUserAsync(UserDto userDto);
    Task DeleteUserAsync(string id);
    Task<UserDto> AuthenticateAsync(string username, string password);
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
}