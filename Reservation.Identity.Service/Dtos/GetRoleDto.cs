using Reservation.Common.IdentityInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Service.Dtos
{
    public class GetRoleDto : IGetRoleDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int AspNetUsersRoleCount { get; set; }
    }
}
