using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyIhsan.Identity.Service.Dtos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MyIhsan.Identity.Service.Core
{
    public class TokenBusiness : ITokenBusiness
    {
        private readonly IConfiguration _config;
        private readonly UserLoginReturn _userLoginReturn;
        private readonly DecodingValidToken _decodingValidToken;

        public TokenBusiness(IConfiguration config)
        { 
            _config = config;
            _userLoginReturn = new UserLoginReturn();
            _decodingValidToken = new DecodingValidToken();
        }

        public UserLoginReturn GenerateJsonWebToken(UserDto userInfo, string roles, string refreshToken ="")
        {            
            var claims = new[] {
                new Claim( JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("UserId", userInfo.Id),
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

            _userLoginReturn.AdminId = userInfo.AdminId;
            _userLoginReturn.VendorId = userInfo.VendorId;
            _userLoginReturn.ClientId = userInfo.ClientId;
            _userLoginReturn.UserId = userInfo.Id;           
            _userLoginReturn.RefreshToken = refreshToken;
            _userLoginReturn.TokenValidTo = token.ValidTo;

            _userLoginReturn.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return _userLoginReturn;
        }

       
        public DecodingValidToken GetUserDataFromToken(ControllerBase controller)
        {
            var hasValue = controller.Request.Headers.TryGetValue("Authorization", out var bearerToken);
            if (!hasValue) return null;
            var split = bearerToken.ToString().Split(' ');
            var token = split[1];
            var result = ValidateAndDecodeToken(token);
            return result;
        }
        private TokenValidationParameters TokenValidationParameters()
        {
            string secret = _config["Jwt:SigningKey"];
            var key = Encoding.ASCII.GetBytes(secret);
            return new TokenValidationParameters
            {
                // Clock skew compensates for server time drift.
                ClockSkew = TimeSpan.FromMinutes(5),
                // Specify the key used to sign the token:
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                // Ensure the token was issued by a trusted authorization server (default true):
                ValidIssuer = _config["Jwt:Site"],
                ValidateIssuer = true,
                // Ensure the token audience matches our audience value (default true):
                ValidAudience = _config["Jwt:Site"],
                ValidateAudience = true,
                // Ensure the token hasn't expired:
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
        }

        private DecodingValidToken ValidateAndDecodeToken(string jwtToken)
        {
            var validationParameters = TokenValidationParameters();
            var handler = new JwtSecurityTokenHandler();
            try
            {
                _decodingValidToken.ClaimsPrincipal = handler.ValidateToken(jwtToken, validationParameters, out var rawValidatedToken);

                //_decodingValidToken.JwtSecurityToken = (JwtSecurityToken)rawValidatedToken;

                return _decodingValidToken;
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                // TODO: Log it or display an error.
                throw new Exception($"Token failed validation: {stvex.Message}");
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                // TODO: Log it or display an error.
                throw new Exception($"Token was invalid: {argex.Message}");
            }
        }
    }
}
