using System.Collections.Generic;
using MailWarehouse.Application.Models;

namespace MailWarehouse.Application.Interfaces;

public interface IPackageService
{
    Task<IEnumerable<PackageDto>> GetAllPackagesAsync();
    Task<PackageDto> GetPackageByIdAsync(int id);
    Task CreatePackageAsync(PackageDto packageDto);
    Task UpdatePackageAsync(PackageDto packageDto);
    Task DeletePackageAsync(int id);
}