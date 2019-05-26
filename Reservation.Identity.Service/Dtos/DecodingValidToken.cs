using Reservation.Common.IdentityInterfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Reservation.Identity.Service.Dtos
{
    public class DecodingValidToken : IDecodingValidToken
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
