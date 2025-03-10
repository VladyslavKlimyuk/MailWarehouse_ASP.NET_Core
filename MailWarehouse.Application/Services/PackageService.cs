using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Domain.Entities;
using MailWarehouse.Domain.Interfaces;

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

    public IEnumerable<PackageDto> GetAllPackages()
    {
        var packages = _packageRepository.GetAll();
        return _mapper.Map<IEnumerable<PackageDto>>(packages);
    }

    public PackageDto GetPackageById(int id)
    {
        var package = _packageRepository.GetById(id);
        return _mapper.Map<PackageDto>(package);
    }

    public void CreatePackage(PackageDto packageDto)
    {
        var package = _mapper.Map<Package>(packageDto);
        _packageRepository.Add(package);
    }

    public void UpdatePackage(PackageDto packageDto)
    {
        var package = _mapper.Map<Package>(packageDto);
        _packageRepository.Update(package);
    }

    public void DeletePackage(int id)
    {
        _packageRepository.Delete(id);
    }
}