using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Reservation.Common.IdentityInterfaces
{
    public interface IDecodingValidToken
    {
        ClaimsPrincipal ClaimsPrincipal { get; set; }
        //JwtSecurityToken JwtSecurityToken { get; set; }
    }
}
