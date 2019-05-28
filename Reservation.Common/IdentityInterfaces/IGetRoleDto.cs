using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
    public interface IGetRoleDto
    {
        string Name { get; set; }
        string Id { get; set; }
        int AspNetUsersRoleCount { get; set; }
    }
}
