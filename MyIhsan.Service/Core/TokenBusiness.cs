using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyIhsan.Service.Dtos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MyIhsan.Service.Core
{
    public class TokenBusiness : ITokenBusiness
    {
        private readonly IConfiguration _config;
        private readonly UserLoginReturn _userLoginReturn;

        public TokenBusiness(IConfiguration config)
        { 
            _config = config;
            _userLoginReturn = new UserLoginReturn();
        }

        public UserLoginReturn GenerateJsonWebToken(UserDto userInfo, string roles, string refreshToken ="")
        {            
            var claims = new[] {
                new Claim( JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("UserId", userInfo.Id.ToString()),
                new Claim("Roles", roles)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));
            var expiryInHours = DateTime.Now.AddHours(Convert.ToDouble(_config["Jwt:ExpiryInHours"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Site"],
                audience: _config["Jwt:Site"],
                expires: expiryInHours,
                signingCredentials: credentials,
                claims: claims);
            _userLoginReturn.UserId = userInfo.Id.ToString();           
            _userLoginReturn.RefreshToken = refreshToken;
            _userLoginReturn.TokenValidTo = token.ValidTo;

            _userLoginReturn.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return _userLoginReturn;
        }
       
    }
}
