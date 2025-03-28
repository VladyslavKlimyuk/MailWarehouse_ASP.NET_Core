using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Domain.Entities;
using MailWarehouse.Domain.Interfaces;
using MailWarehouse.Infrastructure.Repositories;

namespace MailWarehouse.Application.Services;

public class PackageService : IPackageService
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMapper _mapper;

    public PackageService(IPackageRepository packageRepository, IMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PackageDto>> GetAllPackagesAsync()
    {
        var packages = await Task.Run(() => _packageRepository.GetAll());
        return _mapper.Map<IEnumerable<PackageDto>>(packages);
    }

    public async Task<PackageDto> GetPackageByIdAsync(int id)
    {
        var package = await Task.Run(() => _packageRepository.GetById(id));
        return _mapper.Map<PackageDto>(package);
    }

    public async Task CreatePackageAsync(PackageDto packageDto)
    {
        var package = _mapper.Map<Package>(packageDto);
        await Task.Run(() => _packageRepository.Add(package));
    }

    public async Task UpdatePackageAsync(PackageDto packageDto)
    {
        var package = _mapper.Map<Package>(packageDto);
        await Task.Run(() => _packageRepository.Update(package));
    }

    public async Task DeletePackageAsync(int id)
    {
        await Task.Run(() => _packageRepository.Delete(id));
    }
}

