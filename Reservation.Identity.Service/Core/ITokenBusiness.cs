using Microsoft.AspNetCore.Mvc;
using Reservation.Common.IdentityInterfaces;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Dtos;

namespace Reservation.Identity.Service.Core
{
    public interface ITokenBusiness
    {
        IUserLoginReturn GenerateJsonWebToken(UserDto userInfo, string roles, string childRoles,  string refreshToken="" );        
        IDecodingValidToken GetUserDataFromToken(ControllerBase controller);
    }
}
