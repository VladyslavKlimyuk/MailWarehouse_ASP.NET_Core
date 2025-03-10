using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.Domain.Entities;

namespace MailWarehouse.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Package, PackageDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}