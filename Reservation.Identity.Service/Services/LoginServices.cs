using AutoMapper;
using Reservation.Common.Core;
using Reservation.Common.Hasher;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.Parameters;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Dtos;
using Reservation.Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Services
{
    public class LoginServices : BaseService<AspNetUser,IUserDto>, ILoginServices
    {
        private readonly ITokenBusiness _tokenBusiness;
        public LoginServices(IBusinessBaseParameter<AspNetUser> businessBaseParameter, ITokenBusiness tokenBusiness) : base(businessBaseParameter)
        {
            _tokenBusiness = tokenBusiness;
        }
        public async Task<IResponseResult> Login(LoginParameters parameters)
        {
            var user = await _unitOfWork.Repository.FirstOrDefault(q => q.UserName == parameters.Username && !q.IsDeleted);
            if (user == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest,
                            message: "Wrong Username Or Password");
            bool rightPass = CreptoHasher.VerifyHashedPassword(user.PasswordHash, parameters.Password);
            if (!rightPass) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotFound, message: HttpStatusCode.NotFound.ToString());
            var refToken = Guid.NewGuid().ToString();
            var roles = user.AspNetUsersRole.Select(q => q.RoleId).ToList();
            var userDto = Mapper.Map<AspNetUser, UserDto>(user);
            var userLoginReturn = _tokenBusiness.GenerateJsonWebToken(userDto, string.Join(",", roles), refToken);
            return ResponseResult.GetRepositoryActionResult(userLoginReturn, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); ;
        }
    }
}
