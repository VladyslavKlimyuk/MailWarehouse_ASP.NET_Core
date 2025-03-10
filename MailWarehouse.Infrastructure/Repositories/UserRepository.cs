using MailWarehouse.Domain.Interfaces;
using MailWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailWarehouse.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostalDeliveryDbContext _context;

    public UserRepository(PostalDeliveryDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}