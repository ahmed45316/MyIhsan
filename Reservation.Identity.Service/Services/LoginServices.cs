using AutoMapper;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyIhsan.Common.Parameters;
using MyIhsan.Common.Core;

namespace MyIhsan.Identity.Service.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ITokenBusiness _tokenBusiness;
        public LoginServices(ITokenBusiness tokenBusiness)
        {
            _tokenBusiness = tokenBusiness;
        }
        public async Task<IResponseResult> Login(LoginParameters parameters)
        {
            //var user = await _unitOfWork.Repository.FirstOrDefault(q => q.UserName == parameters.Username && !q.IsDeleted);
            //if (user == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest,
            //                message: "Wrong Username Or Password");
            //bool rightPass = user.PasswordHash == parameters.Password;
            //if (!rightPass) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotFound, message: HttpStatusCode.NotFound.ToString());
            //var refToken = Guid.NewGuid().ToString();
            //var roles = user.AspNetUsersRole.Select(q => q.RoleId).ToList();
            //var userDto = Mapper.Map<UserDto, UserDto>(user);
            //var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, string.Join(",", roles), refToken);
            //return ResponseResult.GetRepositoryActionResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); 
            return null;
        }
    }
}
