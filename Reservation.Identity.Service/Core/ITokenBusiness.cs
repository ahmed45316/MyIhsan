using Microsoft.AspNetCore.Mvc;
using MyIhsan.Identity.Service.Dtos;

namespace MyIhsan.Identity.Service.Core
{
    public interface ITokenBusiness
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, string roles,  string refreshToken="" );        
        DecodingValidToken GetUserDataFromToken(ControllerBase controller);
    }
}
