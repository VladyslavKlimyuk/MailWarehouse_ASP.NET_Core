using System.Collections.Generic;
using MailWarehouse.Application.Models;

namespace MailWarehouse.Application.Interfaces;

public interface IPackageService
{
    IEnumerable<PackageDto> GetAllPackages();
    PackageDto GetPackageById(int id);
    void CreatePackage(PackageDto packageDto);
    void UpdatePackage(PackageDto packageDto);
    void DeletePackage(int id);
}