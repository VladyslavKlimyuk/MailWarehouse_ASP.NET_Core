using System.Collections.Generic;
using MailWarehouse.Application.Models;
using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Application.Interfaces;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    UserDto GetUserById(int id);
    void CreateUser(UserDto userDto);
    void UpdateUser(UserDto userDto);
    void DeleteUser(int id);
    User Authenticate(string username, string password);
    User GetByUsername(string username);
}