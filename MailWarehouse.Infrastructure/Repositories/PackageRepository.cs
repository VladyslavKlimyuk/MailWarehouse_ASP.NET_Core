using MailWarehouse.Domain.Entities;
using MailWarehouse.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailWarehouse.Infrastructure.Repositories;

public class PackageRepository : IPackageRepository
{
    private readonly PostalDeliveryDbContext _context;

    public PackageRepository(PostalDeliveryDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Package> GetAll()
    {
        return _context.Packages.ToList();
    }

    public Package GetById(int id)
    {
        return _context.Packages.Find(id);
    }

    public void Add(Package package)
    {
        _context.Packages.Add(package);
        _context.SaveChanges();
    }

    public void Update(Package package)
    {
        _context.Entry(package).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var package = _context.Packages.Find(id);
        if (package != null)
        {
            _context.Packages.Remove(package);
            _context.SaveChanges();
        }
    }
}
