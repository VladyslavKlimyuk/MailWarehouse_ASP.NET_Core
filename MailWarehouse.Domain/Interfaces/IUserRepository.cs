using System.Collections.Generic;
using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Domain.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Add(User user);
    void Update(User user);
    void Delete(int id);
}