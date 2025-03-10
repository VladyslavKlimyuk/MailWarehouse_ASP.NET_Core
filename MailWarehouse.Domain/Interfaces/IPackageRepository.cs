using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Domain.Interfaces;

public interface IPackageRepository
{
    IEnumerable<Package> GetAll();
    Package GetById(int id);
    void Add(Package package);
    void Update(Package package);
    void Delete(int id);
}