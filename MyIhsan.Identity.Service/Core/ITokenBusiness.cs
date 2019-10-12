using Microsoft.AspNetCore.Mvc;
using MyIhsan.Service.Dtos;

namespace MyIhsan.Service.Core
{
    public interface ITokenBusiness
    {
        UserLoginReturn GenerateJsonWebToken(UserDto userInfo, string roles,  string refreshToken="" );        
    }
}
