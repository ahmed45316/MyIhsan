using AutoMapper;
using MyIhsan.Identity.Entities.Entities;
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
           // CreateMap<AspNetUser, UserDto>().ReverseMap();
        }
        private void MappMenu()
        {
           // CreateMap<Menu, MenuDto>().ReverseMap();
        }
        private void MappRole()
        {
            CreateMap<AspNetRoles, RoleDto>().ReverseMap();
            CreateMap<AspNetRoles, GetRoleDto>().ReverseMap();
            CreateMap<AspNetRoles, UpdateRoleDto>().ReverseMap();
        }
    }
}
