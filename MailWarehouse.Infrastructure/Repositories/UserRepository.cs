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
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(PostalDeliveryDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IEnumerable<ApplicationUser> GetAllIdentityUsers()
    {
        return _userManager.Users.ToList();
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users
            .Select(u => new User
            {
                Id = int.Parse(u.Id),
                Username = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            })
            .ToList();
    }

    public async Task AddIdentityUser(ApplicationUser applicationUser, string password)
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

    public User GetById(int id)
    {
        var applicationUser = _context.Users.Find(id.ToString());
        if (applicationUser == null) return null;

        return new User
        {
            Id = int.Parse(applicationUser.Id),
            Username = applicationUser.UserName,
            Email = applicationUser.Email,
            PhoneNumber = applicationUser.PhoneNumber
        };
    }

    public void Add(User user)
    {
        var applicationUser = new ApplicationUser
        {
            Id = user.Id.ToString(),
            UserName = user.Username,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };

        _context.Users.Add(applicationUser);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        var existingUser = _context.Users.Find(user.Id.ToString());
        if (existingUser != null)
        {
            existingUser.UserName = user.Username;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var user = _context.Users.Find(id.ToString());
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public User GetByUsername(string username)
    {
        var applicationUser = _context.Users.FirstOrDefault(u => u.UserName == username);
        if (applicationUser == null) return null;

        return new User
        {
            Id = int.Parse(applicationUser.Id),
            Username = applicationUser.UserName,
            Email = applicationUser.Email,
            PhoneNumber = applicationUser.PhoneNumber
        };
    }

    public User GetByEmail(string email)
    {
        var applicationUser = _context.Users.FirstOrDefault(u => u.Email == email);
        if (applicationUser == null) return null;

        return new User
        {
            Id = int.Parse(applicationUser.Id),
            Username = applicationUser.UserName,
            Email = applicationUser.Email,
            PhoneNumber = applicationUser.PhoneNumber
        };
    }

    public User GetByUsernameAndPassword(string username, string password)
    {
        var applicationUser = _context.Users.FirstOrDefault(u => u.UserName == username);
        if (applicationUser != null)
        {
            var result = _userManager.CheckPasswordAsync(applicationUser, password).Result;
            if (result)
            {
                return new User
                {
                    Id = int.Parse(applicationUser.Id),
                    Username = applicationUser.UserName,
                    Email = applicationUser.Email,
                    PhoneNumber = applicationUser.PhoneNumber
                };
            }
        }
        return null;
    }
}