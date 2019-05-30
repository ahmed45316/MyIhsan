using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.Parameters
{
    public class AssignUserOnRoleParameters
    {
        public string RoleId { get; set; }
        public string[] AssignedUser { get; set; }
    }
}
