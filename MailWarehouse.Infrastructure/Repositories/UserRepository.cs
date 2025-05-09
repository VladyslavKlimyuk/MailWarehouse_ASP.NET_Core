using MailWarehouse.Domain.Interfaces;
using MailWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailWarehouse.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostalDeliveryDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(PostalDeliveryDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IEnumerable<User>> GetAllIdentityUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task AddIdentityUserAsync(User applicationUser, string password)
    {
        var result = await _userManager.CreateAsync(applicationUser, password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Помилка створення користувача: {error.Description}");
            }
            throw new Exception("Не вдалося створити користувача.");
        }
    }

    public async Task UpdateIdentityUserAsync(User applicationUser)
    {
        var result = await _userManager.UpdateAsync(applicationUser);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Помилка оновлення користувача: {error.Description}");
            }
            throw new Exception("Не вдалося оновити користувача.");
        }
    }

    public async Task DeleteIdentityUserAsync(User applicationUser)
    {
        var result = await _userManager.DeleteAsync(applicationUser);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Помилка видалення користувача: {error.Description}");
            }
            throw new Exception("Не вдалося видалити користувача.");
        }
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User> GetByUsernameAndPasswordAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            return user;
        }
        return null;
    }
}