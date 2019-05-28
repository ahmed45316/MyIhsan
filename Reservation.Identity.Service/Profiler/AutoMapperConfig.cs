using AutoMapper;
using Reservation.Common.IdentityInterfaces;
using Reservation.Identity.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Service.Profiler
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
            CreateMap<AspNetUser, IUserDto>().ReverseMap();
        }
        private void MappMenu()
        {
            CreateMap<Menu, IMenuDto>().ReverseMap();
        }
        private void MappRole()
        {
            CreateMap<AspNetRole, IRoleDto>().ReverseMap();
            CreateMap<AspNetRole, IGetRoleDto>().ReverseMap();
            CreateMap<AspNetRole, IUpdateRoleDto>().ReverseMap();
        }
    }
}
