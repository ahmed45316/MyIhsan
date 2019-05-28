using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
   public interface IRoleDto
    {
        string Id { get; set; }
        string Name { get; set; }
        bool IsDeleted { get; set; }
        bool IsBlock { get; set; } 
    }
}
