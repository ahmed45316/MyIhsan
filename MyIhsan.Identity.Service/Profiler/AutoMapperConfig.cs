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
           CreateMap<AspNetUsers, UserDto>().ReverseMap();
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
