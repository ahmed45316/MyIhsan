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
using MyIhsan.Identity.Entities.Views;

namespace MyIhsan.Identity.Service.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ITokenBusiness _tokenBusiness;
        private readonly IServiceBaseParameter<AspNetUsers> _serviceBaseParameter;
        public LoginServices(IServiceBaseParameter<AspNetUsers> serviceBaseParameter,ITokenBusiness tokenBusiness)
        {
            _tokenBusiness = tokenBusiness;
            _serviceBaseParameter = serviceBaseParameter;
        }
        public async Task<IResponseResult> Login(LoginParameters parameters)
        {
            var user = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName.ToLower() == parameters.Username.ToLower() &&q.PasswordHash == parameters.Password && q.IsDeleted!=1);
            if (user == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest,message: "Wrong Username Or Password");
           
            var refToken = Guid.NewGuid().ToString();
            var roles ="";
            var userDto = Mapper.Map<UserDto>(user);
            var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, string.Join(",", roles), refToken);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); 
        }
    }
}
