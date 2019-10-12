using AutoMapper;
using MyIhsan.Service.Core;
using MyIhsan.Service.Dtos;
using MyIhsan.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyIhsan.Common.Parameters;
using MyIhsan.Common.Core;
using MyIhsan.Entities.Views;
using MyIhsan.Service.UnitOfWork;
using MyIhsan.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyIhsan.Service.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ITokenBusiness _tokenBusiness;
        private readonly IServiceBaseParameter<AspNetUsers> _serviceBaseParameter;
        private readonly IIdentityUnitOfWork<AspNetRoles> _roleUnitOfWork;
        private readonly IIdentityUnitOfWork<AspNetUsersRoles> _userRolesUnitOfWork;
        public LoginServices(IServiceBaseParameter<AspNetUsers> serviceBaseParameter,ITokenBusiness tokenBusiness, IIdentityUnitOfWork<AspNetRoles> roleUnitOfWork, IIdentityUnitOfWork<AspNetUsersRoles> userRolesUnitOfWork)
        {
            _tokenBusiness = tokenBusiness;
            _serviceBaseParameter = serviceBaseParameter;
            _roleUnitOfWork = roleUnitOfWork;
            _userRolesUnitOfWork = userRolesUnitOfWork;
        }
        public async Task<IResponseResult> Login(LoginParameters parameters)
        {
            var user = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName.ToLower() == parameters.Username.ToLower() &&q.PasswordHash == parameters.Password && q.IsDeleted!=true);
            if (user == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest,message: "Wrong Username Or Password");
           
            var refToken = Guid.NewGuid().ToString();
            var userRole =await _userRolesUnitOfWork.Repository.FindAsync(q=>q.UserId == user.Id.ToString());
            var roleIds = userRole.Select(q => q.RoleId).ToList();
            var role =await _roleUnitOfWork.Repository.FindAsync(q => roleIds.Contains(q.Id));
            var roles =role.Select(q=>q.Name).ToList();
            var userDto = Mapper.Map<UserDto>(user);
            var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, string.Join(",", roles), refToken);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); 
        }
    }
}
