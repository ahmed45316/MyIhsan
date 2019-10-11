using AutoMapper;
using MyIhsan.Entities.Entities;
using MyIhsan.Entities.Views;
using MyIhsan.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Service.Profiler
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
