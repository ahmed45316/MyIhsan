using AutoMapper;
using Reservation.Common.Core;
using Reservation.Common.Hasher;
using Reservation.Common.Parameters;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Services
{
    public class LoginServices :BaseService<AspNetUser>, ILoginServices
    {
        private readonly ITokenBusiness _tokenBusiness;
        public LoginServices(IBusinessBaseParameter<AspNetUser> businessBaseParameter, ITokenBusiness tokenBusiness) : base(businessBaseParameter)
        {
             _tokenBusiness=tokenBusiness;
    }
        public async Task<IResponseResult> Login(LoginParameters parameters)
        {
            var user = await _unitOfWork.Repository.FirstOrDefault(q => q.UserName == parameters.Username && !q.IsDeleted);
            if (user == null) return ResponseResult.GetRepositoryActionResult(status:HttpStatusCode.BadRequest,
                            message: "Wrong Username Or Password");
            bool rightPass = CreptoHasher.VerifyHashedPassword(user.PasswordHash, parameters.Password);
            if (!rightPass) return ResponseResult.GetRepositoryActionResult(status:HttpStatusCode.NotFound, message:HttpStatusCode.NotFound.ToString());
            //var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(user, _accountService.GetGeneralPermissionType(roles), string.Join(",", roles), _accountService.GetUserChildRoles(roles), refToken);
            //var dataReturned = new UserLoginDto()
            //{
            //    CanLogin = true,
            //    UserId = user.Id,
            //    UserName = user.UserName,
            //    Token = res
            //};
            //return new RepositoryActionResult<UserLoginDto>(dataReturned, HttpStatusCode.OK);
            return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotFound, message: HttpStatusCode.NotFound.ToString()); ;
        }
    }
}
