using AutoMapper;
using MailWarehouse.Application.Models;
using MailWarehouse.ViewModels;

namespace MailWarehouse.Presentation.Mappings;

public class PresentationAutoMapperProfile : Profile
{
    public PresentationAutoMapperProfile()
    {
        CreateMap<PackageDto, PackageViewModel>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
    }
}