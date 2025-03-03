using System.Collections.Generic;
using MailWarehouse.Application.Models;

namespace MailWarehouse.Application.Interfaces;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    UserDto GetUserById(int id);
    void CreateUser(UserDto userDto);
    void UpdateUser(UserDto userDto);
    void DeleteUser(int id);
}