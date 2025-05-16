using System.Collections.Generic;
using MailWarehouse.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MailWarehouse.Domain.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    IEnumerable<ApplicationUser> GetAllIdentityUsers();
    User GetById(int id);
    User GetByUsername(string username);
    User GetByEmail(string email);
    void Add(User user);
    Task AddIdentityUser(ApplicationUser applicationUser, string password);
    void Update(User user);
    void Delete(int id);
    User GetByUsernameAndPassword(string username, string password);
}