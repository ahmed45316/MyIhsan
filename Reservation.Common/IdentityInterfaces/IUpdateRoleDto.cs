using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
    public interface IUpdateRoleDto
    {
         bool IsBlock { get; set; }
         string Name { get; set; }
         string Id { get; set; }
         int AspNetUsersRoleCount { get; set; }
    }
}
