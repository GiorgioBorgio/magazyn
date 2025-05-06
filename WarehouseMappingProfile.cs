using AutoMapper;
using Magazyn.Entities;
using Magazyn.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn
{
    internal class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(m => m.Address, opt => opt.MapFrom(src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                    HouseNumber = src.HouseNumber,
                    ApartmentNumber = src.ApartmentNumber
                }))
                .ForMember(m => m.UserPermissions, opt => opt.MapFrom(src => new List<UserPermission>
                {
                    new UserPermission
                    {
                        PermissionId = 1,
                        UserId = src.Id
                    }
                }));
            CreateMap<User, CreateUserDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.Address.HouseNumber))
                .ForMember(dest => dest.ApartmentNumber, opt => opt.MapFrom(src => src.Address.ApartmentNumber));





            CreateMap<CreateUserDto, Address>();
        }
    }
}
