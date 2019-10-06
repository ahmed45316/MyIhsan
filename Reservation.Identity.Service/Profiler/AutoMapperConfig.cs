using AutoMapper;
using MyIhsan.Identity.Entities.Entities;
using MyIhsan.Identity.Entities.Views;
using MyIhsan.Identity.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Identity.Service.Profiler
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            MappUsers();
            MappMenu();
            MappRole();
        }
        private void MappUsers()
        {
           CreateMap<AspNetUsers, UserDto>()
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.ID))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NAMEEN))
                .ForMember(dest => dest.EntityIdInfo, opt => opt.MapFrom(src => src.ENTITYIDINFO))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NAME))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CITYID))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.COUNTRYID))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GENDER))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ADDRESS))
                .ForMember(dest => dest.TelNumber, opt => opt.MapFrom(src => src.TELNUMBER))
                .ForMember(dest => dest.IsBlock, opt => opt.MapFrom(src => src.ISBLOCK))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.ISDELETED))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.USERNAME))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.ACCESSFAILEDCOUNT))
                .ForMember(dest => dest.LockOutEnabled, opt => opt.MapFrom(src => src.LOCKOUTENABLED))
                .ForMember(dest => dest.LockoutEndDateUtc, opt => opt.MapFrom(src => src.LOCKOUTENDDATEUTC))
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => src.TWOFACTORENABLED))
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => src.PHONENUMBERCONFIRMED))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PHONENUMBER))
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => src.SECURITYSTAMP))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PASSWORDHASH))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EMAILCONFIRMED))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMAIL))
                .ReverseMap();
        }
        private void MappMenu()
        {
           CreateMap<Menus, MenuDto>().ReverseMap();
        }
        private void MappRole()
        {
            CreateMap<AspNetRoles, RoleDto>().ReverseMap();
        }
    }
}
