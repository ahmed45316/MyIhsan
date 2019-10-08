using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MyIhsan.Identity.Service.Dtos
{
    public class DecodingValidToken 
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
