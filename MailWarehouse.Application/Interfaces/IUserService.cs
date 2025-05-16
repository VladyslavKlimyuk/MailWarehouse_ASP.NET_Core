using System.Collections.Generic;
using MailWarehouse.Application.Models;

namespace MailWarehouse.Application.Interfaces;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    UserDto GetUserById(int id);
    UserDto GetUserByEmail(string email);
    void CreateUser(UserDto userDto);
    void UpdateUser(UserDto userDto);
    void DeleteUser(int id);
    Domain.Entities.User Authenticate(string username, string password);
    Domain.Entities.User GetByUsername(string username);
}